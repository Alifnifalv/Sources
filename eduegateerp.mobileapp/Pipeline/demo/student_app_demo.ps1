$originalPath = Get-Location
# Define configurations
$appName = "Student Mobile App"
$projectPath = "../../Eduegate.StudentApp"
$appType = "student"
$client = "eduegate"
$environment = "live"
# Set sourceIconPath dynamically and resolve it
$sourceIconPath = "../../Resources/icons/Eduegate/$($appName -replace ' ', '_')"
# Define paths for APK output and output folder
$outputFolderPath = "$projectPath/platforms/android/app/build/outputs/apk/debug"
$outputAPKPath = "$projectPath/platforms/android/app/build/outputs/apk/debug/app-debug.apk"

# Update app.js with predefined client and environment
$appJsPath = "$projectPath/www/apps/app.js"
$content = Get-Content -Path $appJsPath -Raw

$content = $content -replace '// var client = "eduegate";', 'var client = "eduegate";'
$content = $content -replace 'var client = "eduegate";', '// var client = "eduegate";'
$content = $content -replace '// var client = "eduegate";', 'var client = "eduegate";'


$content = $content -replace '// var environment = "live";', 'var environment = "live";'

$content = $content -replace 'var environment = "staging";', '//var environment = "staging";'
$content = $content -replace '// var environment = "staging";', 'var environment = "staging";'

$content = $content -replace '// var environment = "test";', 'var environment = "test";'
$content = $content -replace 'var environment = "test";', '//var environment = "test";'

$content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
$content = $content -replace 'var environment = "linux";', '// var environment = "linux";'

$content = $content -replace '// var environment = "local";', 'var environment = "local";'
$content = $content -replace 'var environment = "local";', '// var environment = "local";'

Set-Content -Path $appJsPath -Value $content

# Update config.xml with predefined client-specific information
$configXmlPath = "$projectPath/config.xml"
$configContent = Get-Content -Path $configXmlPath -Raw

$configContent = $configContent -replace "<!-- <name>Pearl Student</name> -->", "<name>Pearl Student</name>"
$configContent = $configContent -replace "<!-- <description>Pearl $appName</description> -->", "<description>Pearl $appName</description>"
$configContent = $configContent -replace "<!-- <name>Eduegate Student</name> -->", "<name>Eduegate Student</name>"
$configContent = $configContent -replace "<!-- <description>Eduegate $appName</description> -->", "<description>Eduegate $appName</description>"
$configContent = $configContent -replace "<name>Pearl Student</name>", "<!-- <name>Pearl Student</name> -->"
$configContent = $configContent -replace "<description>Pearl $appName</description>", "<!-- <description>Pearl $appName</description> -->"


Set-Content -Path $configXmlPath -Value $configContent

# Copy icons to the destination path
$destinationIconPath = "$projectPath/res"
if (-not (Test-Path -Path $destinationIconPath)) {
	New-Item -Path $destinationIconPath -ItemType Directory
}
Copy-Item -Path "$sourceIconPath/*" -Destination $destinationIconPath -Recurse -Force

# Execute Cordova commands to build the app
cd $projectPath
Invoke-Expression "cordova platform rm android"
Invoke-Expression "cordova platform add android@latest"
Invoke-Expression "cordova build android"

cd $originalPath

# Rename the APK
$newAPKName = "$appType-app-$client-$environment.apk"
if (Test-Path -Path $outputAPKPath) {
    try {
        Rename-Item -Path $outputAPKPath -NewName $newAPKName -ErrorAction Stop
        Write-Host "Successfully renamed APK to: $newAPKName"
    } catch {
        Write-Error "Failed to rename APK. Error: $($_.Exception.Message)"
        exit 1
    }
} else {
    Write-Error "APK not found at path: $outputAPKPath"
    exit 1
}

# Copy the renamed APK to the destination
$destinationPath = "../$newAPKName"
if (Test-Path -Path "$outputFolderPath/$newAPKName") {
    try {
        Copy-Item -Path "$outputFolderPath/$newAPKName" -Destination $destinationPath -ErrorAction Stop
        Write-Host "Successfully copied APK to: $destinationPath"
    } catch {
        Write-Error "Failed to copy APK to destination. Error: $($_.Exception.Message)"
        exit 1
    }
} else {
    Write-Error "Renamed APK not found at: $outputFolderPath/$newAPKName"
    exit 1
}

#Start-Process -FilePath $outputFolderPath

Write-Host "App have been built successfully."
Set-Location -Path $originalPath

exit