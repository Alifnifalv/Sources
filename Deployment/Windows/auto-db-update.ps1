# Check if Az.Storage module is installed, install if not
if (-not (Get-Module -ListAvailable -Name Az.Storage)) {
    Write-Output "Az.Storage module not found. Installing..."
    Install-Module -Name Az.Storage -Force -AllowClobber
} else {
    Write-Output "Az.Storage module is already installed."
}

# Check if SqlServer module is installed, install if not
if (-not (Get-Module -ListAvailable -Name SqlServer)) {
    Write-Output "SqlServer module not found. Installing..."
    Install-Module -Name SqlServer -Force -AllowClobber
} else {
    Write-Output "SqlServer module is already installed."
}

# Import necessary modules (with error handling)
try {
    Import-Module Az.Storage -ErrorAction Stop
    Import-Module SqlServer -ErrorAction Stop
    Write-Output "Modules imported successfully."
} catch {
    Write-Error "Failed to import required modules: $_"
    exit 1
}

# Variables for Azure Blob Storage
$storageAccountName = "pearlbackup"
$sasToken = "sv=2022-11-02&ss=b&srt=sco&sp=rwdlaciytfx&se=2025-06-10T15:28:23Z&st=2023-05-10T07:28:23Z&spr=https&sig=9X5qms1T53rq7yUf5W7RumAuWL65shjIpeH44SPcwMQ%3D"
$containerName = "sqlbackup"
# Get the current working directory using Get-Location
$currentDirectory = Get-Location

# Variables for SQL Server connection and restore
$serverInstance = "localhost"
$sqlUsername = "eduegateuser"
$sqlPassword = "eduegate@123"
$dbName = "Pearl_Live_Test"
function Invoke-SqlCommandSecure {
    param (
        [string]$Query
    )
    Invoke-Sqlcmd -ServerInstance $serverInstance `
                  -Username $sqlUsername `
                  -Password $sqlPassword `
                  -Query $Query `
                  -EncryptConnection -TrustServerCertificate
}

# Then keep existing code but add validation:
function Get-SqlServerProperty {
    param (
        [string]$propertyName
    )
    $query = "SELECT SERVERPROPERTY('$propertyName');"
    
    try {
        $result = Invoke-SqlCommandSecure -Query $query | Select-Object -ExpandProperty Column1
        if (-not $result) {
            throw "Failed to retrieve SQL Server property '$propertyName'"
        }
        return $result
    } catch {
        Write-Error "Error getting SQL Server property: $_"
        exit 1
    }
}

# Add path validation after getting default backup directory
$defaultBackupDir = Get-SqlServerProperty -propertyName 'InstanceDefaultBackupPath'
if (-not (Test-Path $defaultBackupDir)) {
    Write-Error "Invalid backup directory path: $defaultBackupDir"
    exit 1
}

# Add verification before using downloadPath
Write-Output "Validated backup directory exists: $defaultBackupDir"

# Set downloadPath to the SQL Server default backup directory
$downloadPath = $defaultBackupDir

# Regular expression pattern to match backup file names
$pattern = '^EduEgate_2022_backup_(\d{4})_(\d{2})_(\d{2}).*\.bak'

# Create an Azure Storage context using the SAS token
$context = New-AzStorageContext -StorageAccountName $storageAccountName -SasToken $sasToken

# Retrieve blobs from the specified container
$blobs = Get-AzStorageBlob -Container $containerName -Context $context

# Filter blobs that match the backup naming pattern
$filteredBlobs = $blobs | Where-Object { $_.Name -match $pattern }
if ($filteredBlobs.Count -eq 0) {
    Write-Error "No backup blobs found matching the pattern: $pattern"
    exit 1
}

# Select the latest blob based on the LastModified property
$latestBlob = $filteredBlobs | Sort-Object -Property LastModified -Descending | Select-Object -First 1
Write-Output "Latest backup blob identified: $($latestBlob.Name)"

# Download the latest backup blob to the specified folder (only if it doesn't already exist)
$localFile = Join-Path -Path $downloadPath -ChildPath $latestBlob.Name

if (Test-Path -Path $localFile) {
    # Compare against cloud version
    if ((Get-Item $localFile).Name -ne $latestBlob.Name) {
        try {
            Write-Output "Newer backup available ($($latestBlob.Name)), removing outdated local copy"
            Remove-Item -Path $localFile -Force -ErrorAction Stop
            
            # Verify deletion succeeded
            if (Test-Path -Path $localFile) {
                throw "Failed to remove outdated backup file"
            }
        }
        catch {
            Write-Error "Could not clean up old backup: $_" 
            exit 1
        }
    }
    else {
        Write-Output "Local backup matches latest cloud version"
    }
}

if (Test-Path -Path $localFile) {
    Write-Output "Backup file already exists locally: $localFile. Skipping download."
} else {
    Write-Output "Downloading blob to: $localFile"
    Get-AzStorageBlobContent -Blob $latestBlob.Name -Container $containerName -Destination $localFile -Context $context
}

# Helper function to run SQL commands with the required connection parameters
# function Invoke-SqlCommandSecure {
#     param (
#         [string]$Query
#     )
#     Invoke-Sqlcmd -ServerInstance $serverInstance `
#                   -Username $sqlUsername `
#                   -Password $sqlPassword `
#                   -Query $Query `
#                   -EncryptConnection -TrustServerCertificate
# }

# Function to retrieve logical names of files in the backup
function Get-BackupFileList {
    param (
        [string]$localFile
    )
    $query = @"
    RESTORE FILELISTONLY
    FROM DISK = '$localFile'
"@
    Write-Output "Executing Get-BackupFileList query: $($query)"  # Debug output
    return Invoke-SqlCommandSecure -Query $query
}

# Generate MOVE clauses dynamically
function Generate-MoveClauses {
    param (
        [string]$dbName
    )
    $restoreDataDir = "C:\SQLData\$dbName"
    if (-not (Test-Path $restoreDataDir)) {
        New-Item -Path $restoreDataDir -ItemType Directory | Out-Null
    }

    $fileList = Get-BackupFileList -localFile $localFile
    Write-Verbose "Retrieved $($fileList.Count) file entries from backup."

    $moveClauses = @()
    foreach ($row in $fileList | Where-Object { $_.LogicalName -and $_.LogicalName.Trim() -ne "" }) {
        $originalLogical = $row.LogicalName
        if ($row.Type -eq 'D') {
            $newFilename = "$dbName.mdf"
        } elseif ($row.Type -eq 'L') {
            $newFilename = "$dbName`_log.ldf"
        }
        $newPhysical = Join-Path $restoreDataDir $newFilename
        Write-Verbose "Moving [$originalLogical] to [$newPhysical]"
        $moveClauses += "MOVE N'$originalLogical' TO N'$newPhysical'"
    }
    Write-Verbose "Generated MOVE Clauses: $($moveClauses -join ', ')"
    return $moveClauses
}



# Drop the target database if it already exists (ensuring a clean restore)
$dropDbQuery = @"
IF EXISTS (SELECT name FROM sys.databases WHERE name = '$dbName')
BEGIN
    ALTER DATABASE [$dbName] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$dbName];
END
"@
Write-Output "Dropping existing database (if exists) and preparing for restore..."
Invoke-SqlCommandSecure -Query $dropDbQuery

# Generate MOVE clauses for the restore operation
$moveClauses = Generate-MoveClauses -dbName $dbName
# Restore database with dynamically generated MOVE clauses
function Restore-DatabaseWithMove {
    param (
        [string]$dbName,
        [array]$MoveClauses
    )
    Write-Output "Restoring database [$dbName] from backup..."

    try {
        $moveClause = ($MoveClauses -join ", ")
        $restoreCommand = @"
            RESTORE DATABASE [$dbName]
            FROM DISK='$localFile'
            WITH REPLACE,
            $moveClause
"@
        Write-Output "Executing Restore Command: $($restoreCommand)" # Debug output
        Invoke-SqlCommandSecure -Query $restoreCommand
    } catch { throw $_ }
}

# Restore the database with the generated MOVE clauses
Restore-DatabaseWithMove -dbName $dbName -MoveClauses $moveClauses

# Define the SQL update queries to run after the restore
$updateQueries = @"
DELETE FROM [$dbName].setting.Settings;
INSERT INTO [$dbName].setting.Settings
SELECT * FROM [EduEgate_Test].setting.Settings;

UPDATE [$dbName].setting.Settings SET SettingValue = 'http://stagingapi.pearlschool.org/ExternalPages/Mastercard/mastercardpayment4.html' WHERE SettingCode = 'MERCHANTCARDURL';
UPDATE [$dbName].setting.Settings SET SettingValue = 'https://test-cbq.mtf.gateway.mastercard.com/api/rest/version/82/merchant/{merchant_ID}/session' WHERE SettingCode = 'MERCHANTGATEWAY';
UPDATE [$dbName].setting.Settings SET SettingValue = 'https://test-cbq.mtf.gateway.mastercard.com/api/rest/version/82' WHERE SettingCode = 'MERCHANTGATEWAY2';
UPDATE [$dbName].setting.Settings SET SettingValue = 'https://test-cbq.mtf.gateway.mastercard.com/static/checkout/checkout.min.js' WHERE SettingCode = 'MERCHANTGATEWAYCHECKOUTJSLINK';
UPDATE [$dbName].setting.Settings SET SettingValue = 'https://parent.pearlschool.org/img/PaymentLogo.png' WHERE SettingCode = 'MERCHANTGATEWAYLOGOURL';
UPDATE [$dbName].setting.Settings SET SettingValue = 'TESTcbq_Podar' WHERE SettingCode = 'MERCHANTID';
UPDATE [$dbName].setting.Settings SET SettingValue = 'Podar Pearl' WHERE SettingCode = 'MERCHANTNAME';
UPDATE [$dbName].setting.Settings SET SettingValue = '00d25fa68afe2979597c15eeca6731bd' WHERE SettingCode = 'MERCHANTPASSWORD';

UPDATE [$dbName].setting.Settings SET SettingValue = 'https://demoparentv2.eduegate.com/PaymentGateway/QPAYResponse' WHERE SettingCode = 'QPAY-EXTRAFIELDS_F14';
UPDATE [$dbName].setting.Settings SET SettingValue = 'https://pguat.qcb.gov.qa/qcb-pg/api/gateway/2.0' WHERE SettingCode = 'QPAY-MERCHANTGATEWAY';
UPDATE [$dbName].setting.Settings SET SettingValue = '1JV3EA/9HHWI8xAC' WHERE SettingCode = 'QPAY-SECRETKEY';

UPDATE [$dbName].setting.Settings SET SettingValue = 'Local' WHERE SettingCode = 'HOST_NAME';

DELETE FROM [$dbName].mutual.UserDeviceMaps;

UPDATE [$dbName].setting.Settings SET SettingValue = 'softoptestmail@gmail.com' WHERE SettingCode = 'TICKETING_CC_MAILS';

UPDATE [$dbName].setting.Settings SET SettingValue = 'eduegate' WHERE SettingCode = 'REPORTING_SERVICE';
UPDATE [$dbName].setting.Settings SET SettingValue = 'dd/MM/yyyy' WHERE SettingCode = 'ReportDateFormat';
"@

Write-Output "Executing post-restore SQL update queries..."
Invoke-SqlCommandSecure -Query $updateQueries

Write-Output "Process completed successfully."