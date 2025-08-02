# ContinuousHeartbeat.ps1
# Configuration
$apiUrl = "http://localhost:5000/health"
$logFile = "D:/amar/Staging_AI_Api/heartbeat.log"
$intervalMinutes = 1
$timeoutSeconds = 30

# Ensure log directory exists
$logDir = Split-Path $logFile -Parent
if (!(Test-Path $logDir)) {
    New-Item -ItemType Directory -Path $logDir -Force | Out-Null
}

function Write-Log {
    param ([string]$message)
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    "$timestamp - $message" | Out-File -FilePath $logFile -Append
}

Write-Log "Starting continuous heartbeat service (interval: $intervalMinutes minutes)"

while ($true) {
    try {
        Write-Log "Sending heartbeat request to $apiUrl"
        $response = Invoke-RestMethod -Uri $apiUrl -Method GET -TimeoutSec $timeoutSeconds
        Write-Log "Heartbeat successful. Status: $($response.status)"
    }
    catch {
        Write-Log "Heartbeat failed. Error: $_"
    }
    
    # Wait for the specified interval
    Start-Sleep -Seconds ($intervalMinutes * 60)
}