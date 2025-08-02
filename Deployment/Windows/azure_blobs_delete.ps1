# Configure these variables
$storageAccountName = "pearlbackup"
$sasToken = "sv=2022-11-02&ss=b&srt=sco&sp=rwdlaciytfx&se=2025-06-10T15:28:23Z&st=2023-05-10T07:28:23Z&spr=https&sig=9X5qms1T53rq7yUf5W7RumAuWL65shjIpeH44SPcwMQ%3D"
$containerName = "sqlbackup"

# Create a storage context
$context = New-AzStorageContext -StorageAccountName $storageAccountName -SasToken $sasToken

# List all blobs in the container
$blobs = Get-AzStorageBlob -Container $containerName -Context $context

# Filter blobs that start with 'EduEgate_2022_backup_' and are from October 2024
# $filteredBlobs = $blobs | Where-Object { $_.Name -like 'EduEgate_2022_backup_*' }
$filteredBlobs = $blobs | Where-Object { $_.Name -match '^EduEgate_2022_backup_(\d{4})_(\d{2})_(\d{2}).*\.bak' }

# Parse the blob names and group by the day of the month (extract day from filename)
$groupedByDate = $filteredBlobs | ForEach-Object {
    $dateString = ($_).Name -replace '^EduEgate_2022_backup_(\d{4})_(\d{2})_(\d{2}).*\.bak', '$1,$2,$3'
    $dateParts = $dateString.Split(',')

    [PSCustomObject]@{
        BlobName = $_.Name
        Year = [int]$dateParts[0]   # Year is the first part
        Month = [int]$dateParts[1]  # Month is the second part
        Day = [int]$dateParts[2]    # Day is the third part
    }
}

# Define the default start and end dates for October 2024
$defaultStart = Get-Date "2024-10-01"
$defaultEnd = Get-Date "2024-10-31"

# Prompt user to adjust start and end dates
Write-Host "Current Start Date: $($defaultStart.ToString('yyyy-MM-dd'))"
Write-Host "Current End Date: $($defaultEnd.ToString('yyyy-MM-dd'))"

$startDateInput = Read-Host "Enter a new start date (yyyy-MM-dd) or press Enter to keep the default"
$endDateInput = Read-Host "Enter a new end date (yyyy-MM-dd) or press Enter to keep the default"

# Validate and assign start date
if (-not [string]::IsNullOrWhiteSpace($startDateInput)) {
    try {
        $octoberStart = Get-Date $startDateInput
    } catch {
        Write-Host "Invalid start date format. Using the default value." -ForegroundColor Yellow
        $octoberStart = $defaultStart
    }
} else {
    $octoberStart = $defaultStart
}

# Validate and assign end date
if (-not [string]::IsNullOrWhiteSpace($endDateInput)) {
    try {
        $octoberEnd = Get-Date $endDateInput
    } catch {
        Write-Host "Invalid end date format. Using the default value." -ForegroundColor Yellow
        $octoberEnd = $defaultEnd
    }
} else {
    $octoberEnd = $defaultEnd
}

Write-Host "Using Start Date: $($octoberStart.ToString('yyyy-MM-dd'))"
Write-Host "Using End Date: $($octoberEnd.ToString('yyyy-MM-dd'))"

# Divide the month into 4 periods
$periods = @(
    @{ StartDate = $octoberStart; EndDate = $octoberStart.AddDays(7) },
    @{ StartDate = $octoberStart.AddDays(8); EndDate = $octoberStart.AddDays(15) },
    @{ StartDate = $octoberStart.AddDays(16); EndDate = $octoberStart.AddDays(22) },
    @{ StartDate = $octoberStart.AddDays(23); EndDate = $octoberEnd }
)

# Function to get the latest blob in a time period
function Get-LatestBlobInPeriod($period, $groupedBlobs) {
    $start = $period.StartDate
    $end = $period.EndDate

    $blobsInPeriod = $groupedBlobs | Where-Object { 
    $blobDate = Get-Date "$($_.Year)-$($_.Month)-$($_.Day)"
        $blobDate -ge $start -and $blobDate -le $end
    }

    # Sort blobs by day (descending) to get the latest blob
    $latestBlob = $blobsInPeriod | Sort-Object { $_.Day } -Descending | Select-Object -First 1
    return $latestBlob
}

# Loop through the periods and get the latest blob for each
$finallatestBlobs = $periods | ForEach-Object {
    $latestBlob = Get-LatestBlobInPeriod -period $_ -groupedBlobs $groupedByDate
    $latestBlob.BlobName
}

# Display the latest blobs for each period
$finallatestBlobs

# Now, list all blobs in the periods combined except the finallatestBlobs
$allBlobsToDelete = $periods | ForEach-Object {
    $period = $_
    $start = $period.StartDate
    $end = $period.EndDate

    # Get all blobs in the period
    $blobsInPeriod = $groupedByDate | Where-Object { 
        $blobDate = Get-Date "$($_.Year)-$($_.Month)-$($_.Day)"
        $blobDate -ge $start -and $blobDate -le $end
    }

    # Get the latest blob in the period
    $latestBlob = Get-LatestBlobInPeriod -period $period -groupedBlobs $groupedByDate

    # Exclude the latest blob and return the others
    $blobsInPeriod | Where-Object { $_.BlobName -ne $latestBlob.BlobName }
}

# Display all blobs except the latest ones
$allBlobsToDelete | ForEach-Object {
    Write-Host "Blob to delete: $($_.BlobName)"
}
# Prompt user for confirmation before deleting
$confirmation = Read-Host "Are you sure you want to delete all blobs listed above? Type 'yes' to confirm or press Enter to cancel"

if ($confirmation -eq 'yes') {
    # Proceed with the deletion
    $allBlobsToDelete | ForEach-Object {
        Remove-AzStorageBlob -Container $containerName -Blob $_.BlobName -Context $context
        Write-Host "Deleted blob: $($_.BlobName)"
    }
} else {
    Write-Host "Deletion cancelled."
}