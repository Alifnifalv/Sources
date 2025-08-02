$originalPath = Get-Location
# Define configurations for each app
$appConfigs = @(
    @{
        Name = "Parent Mobile App"
        Path = "..\..\..\eduegateerp.mobileapp\Eduegate.ParentApp"
        AppType = "parent"
    },
    @{
        Name = "Staff Mobile App"
        Path = "..\..\..\eduegateerp.mobileapp\Eduegate.StaffApp"
        AppType = "staff"
    },
    @{
        Name = "Student Mobile App"
        Path = "..\..\..\eduegateerp.mobileapp\Eduegate.StudentApp"
        AppType = "student"
    },
    @{
        Name = "Visitor Mobile App"
        Path = "..\..\..\eduegateerp.mobileapp\Eduegate.VisitorApp"
        AppType = "visitor"
    }
)


# Define common variables
$client = "pearl"        # Predefined client, e.g., "pearl" or "eduegate"
$environment = "staging"  # Predefined environment, e.g., "staging" or "live"
# Loop through each app configuration and build the app
foreach ($appConfig in $appConfigs) {
    $appName = $appConfig.Name
    $relativePath = $appConfig.Path
	# Correct the path by merging local absolute and relative paths
    $resolvedPath = Resolve-Path -Path (Join-Path $originalPath $relativePath)
    $projectPath = $resolvedPath.Path
    $appType = $appConfig.AppType

    Write-Host "Building $appName..."
	# Define paths for APK output and output folder
	$outputAPKPath = "$projectPath\platforms\android\app\build\outputs\apk\debug\app-debug.apk"
	$outputFolderPath = "$projectPath\platforms\android\app\build\outputs\apk\debug"
	
	#Get Latest form source
	cd $projectPath
	Invoke-Expression "tf get"
	
	# Set sourceIconPath dynamically and resolve it
    $iconRelativePath = "..\..\..\eduegateerp.mobileapp\Resources\icons\Pearl\$($appName -replace ' ', '_')"
    $resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
    $sourceIconPath = $resolvedIconPath.Path
		
	# Update app.js with predefined client and environment
	$appJsPath = "$projectPath\www\apps\app.js"
	$content = Get-Content -Path $appJsPath -Raw
	$content = $content -replace '// var client = "eduegate";', 'var client = "eduegate";'
	$content = $content -replace 'var client = "eduegate";', '// var client = "eduegate";'
	$content = $content -replace '// var client = "pearl";', 'var client = "pearl";'


	$content = $content -replace '// var environment = "live";', 'var environment = "live";'
	$content = $content -replace 'var environment = "live";', '// var environment = "live";'

	$content = $content -replace '// var environment = "staging";', 'var environment = "staging";'

	$content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
	$content = $content -replace 'var environment = "linux";', '// var environment = "linux";'

	$content = $content -replace '// var environment = "local";', 'var environment = "local";'
	$content = $content -replace 'var environment = "local";', '// var environment = "local";'
	Set-Content -Path $appJsPath -Value $content

	# Update config.xml with predefined client-specific information
	$configXmlPath = "$projectPath\config.xml"
	$configContent = Get-Content -Path $configXmlPath -Raw

	$configContent = $configContent -replace "<!-- <name>Pearl Parent</name> -->", "<name>Pearl Parent</name>"
	$configContent = $configContent -replace "<!-- <description>Pearl $appName</description> -->", "<description>Pearl $appName</description>"
	$configContent = $configContent -replace "<!-- <name>Eduegate Parent</name> -->", "<name>Eduegate Parent</name>"
	$configContent = $configContent -replace "<!-- <description>Eduegate $appName</description> -->", "<description>Eduegate $appName</description>"
	$configContent = $configContent -replace "<name>Eduegate Parent</name>", "<!-- <name>Eduegate Parent</name> -->"
	$configContent = $configContent -replace "<description>Eduegate $appName</description>", "<!-- <description>Eduegate $appName</description> -->"


	Set-Content -Path $configXmlPath -Value $configContent

	# Copy icons to the destination path
	$destinationIconPath = "$projectPath\res"
	if (-not (Test-Path -Path $destinationIconPath)) {
		New-Item -Path $destinationIconPath -ItemType Directory
	}
	Copy-Item -Path "$sourceIconPath\*" -Destination $destinationIconPath -Recurse -Force

	# Execute Cordova commands to build the app
	cd $projectPath
	Invoke-Expression "cordova platform rm android"
	Invoke-Expression "cordova platform add android@latest"
	Invoke-Expression "cordova build android"

	# Rename the APK and open the output folder
	$newAPKName = "$appType-app-$client-$environment.apk"
	Rename-Item -Path $outputAPKPath -NewName $newAPKName
	Start-Process -FilePath $outputFolderPath
}

Write-Host "All apps have been built successfully."
Set-Location -Path $originalPath