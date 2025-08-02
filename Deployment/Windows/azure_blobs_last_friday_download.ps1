# Configure these variables
$storageAccountName = "pearlbackup"
$sasToken = "sv=2022-11-02&ss=b&srt=sco&sp=rwdlaciytfx&se=2025-06-10T15:28:23Z&st=2023-05-10T07:28:23Z&spr=https&sig=9X5qms1T53rq7yUf5W7RumAuWL65shjIpeH44SPcwMQ%3D" # Ensure this SAS token has READ permissions for blobs
$containerName = "sqlbackup"
$downloadPath = "F:\Last_Friday_Backup" # --- SPECIFY YOUR DOWNLOAD FOLDER HERE ---

# --- Create download directory if it doesn't exist ---
if (-not (Test-Path $downloadPath)) {
    Write-Host "Download path '$downloadPath' does not exist. Creating it..."
    try {
        New-Item -ItemType Directory -Path $downloadPath -Force | Out-Null
        Write-Host "Successfully created download directory: $downloadPath" -ForegroundColor Green
    } catch {
        Write-Error "Failed to create download directory: $downloadPath. Please check permissions or create it manually. Error: $($_.Exception.Message)"
        exit 1
    }
}

# Create a storage context
$context = New-AzStorageContext -StorageAccountName $storageAccountName -SasToken $sasToken

# --- 1. Calculate the Previous Month and its Last Friday ---
$today = Get-Date
# First day of the current month
$firstDayOfCurrentMonth = Get-Date -Year $today.Year -Month $today.Month -Day 1
# Last day of the previous month
$lastDayOfPreviousMonth = $firstDayOfCurrentMonth.AddDays(-1)
# Find the last Friday of the previous month
$targetDate = $lastDayOfPreviousMonth
while ($targetDate.DayOfWeek -ne [System.DayOfWeek]::Friday) {
    $targetDate = $targetDate.AddDays(-1)
    # Safety break in case the month doesn't have a Friday (impossible for a full month but good for short ranges)
    # or if $targetDate goes before the beginning of that previous month
    if ($targetDate.Month -ne $lastDayOfPreviousMonth.Month) {
        Write-Warning "Could not find a Friday in the month of $($lastDayOfPreviousMonth.ToString('MMMM yyyy')) before or on $($lastDayOfPreviousMonth.ToString('yyyy-MM-dd'))."
        $targetDate = $null # Signal that no valid date was found
        break
    }
}

if (-not $targetDate) {
    Write-Error "Failed to determine the target Friday for the previous month. Exiting."
    exit 1
}

Write-Host "Targeting backups from: $($targetDate.ToString('yyyy-MM-dd')) (Last Friday of $($lastDayOfPreviousMonth.ToString('MMMM yyyy')))"

# --- 2. Construct the expected blob name pattern ---
# Based on your original filter: 'EduEgate_2022_backup_YYYY_MM_DD*.bak'
# The '2022' seems to be a fixed part of your naming scheme.
# The date part (YYYY_MM_DD) will come from $targetDate.
$blobNamePrefix = "EduEgate_2022_backup_$($targetDate.ToString('yyyy_MM_dd'))"
Write-Host "Expected blob name prefix: $blobNamePrefix"

# --- 3. List all blobs in the container and filter ---
Write-Host "Listing blobs in container '$containerName'..."
$blobs = Get-AzStorageBlob -Container $containerName -Context $context

# Filter blobs that start with the prefix for the target Friday and end with .bak
# We also sort by LastModified descending in case there are multiple backups on that same day
# and we want the absolute latest one.
$targetBlobToDownload = $blobs | Where-Object { $_.Name -like "$blobNamePrefix*" -and $_.Name -like "*.bak" } | Sort-Object LastModified -Descending | Select-Object -First 1

# --- 4. Download the found blob ---
if ($targetBlobToDownload) {
    $blobNameToDownload = $targetBlobToDownload.Name
    $destinationFile = Join-Path -Path $downloadPath -ChildPath $blobNameToDownload
    Write-Host "Found target blob: $blobNameToDownload"
    Write-Host "Attempting to download to: $destinationFile"

    try {
        Get-AzStorageBlobContent -Container $containerName -Blob $blobNameToDownload -Destination $destinationFile -Context $context -Force
        Write-Host "Successfully downloaded '$blobNameToDownload' to '$destinationFile'" -ForegroundColor Green
    } catch {
        Write-Error "Failed to download blob '$blobNameToDownload'. Error: $($_.Exception.Message)"
    }
} else {
    Write-Warning "No blob found matching the pattern '$($blobNamePrefix)*.bak' for the target date $($targetDate.ToString('yyyy-MM-dd'))."
}

Write-Host "Script finished."