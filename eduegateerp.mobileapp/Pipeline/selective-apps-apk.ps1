# Accepting arguments for $appType and $newVersion
param (
    [string]$appType,
    [string]$environment,
	[string]$client
)


# Step 0: basic variables
$originalPath = Get-Location

# Check if arguments are provided
if (-not $appType -or -not $environment -or -not $client) {
    Write-Error "Error: Missing required arguments.

Usage:
    ./selective-apps-apk.ps1 -appType <AppType> -environment <environment>

Description:
    -appType    Specify the type of application ('parent' , 'staff' , 'student' or 'visitor').
    -environment Specify the environment ('test' , 'staging' , 'live' or 'linux').
	-client Specify the client ('pearl' or 'eduegate')
	
Example:
    ./selective-apps-apk.ps1 -appType 'parent' -environment 'test'"
    exit 1
}

# Automatically setting $appName and $Path based on $appType
switch ($appType) {
    "parent" {
        $appName = "Parent Mobile App"
        $Path = "../Eduegate.ParentApp"
    }
    "staff" {
        $appName = "Staff Mobile App"
        $Path = "../Eduegate.StaffApp"
    }
	"student" {
        $appName = "Student Mobile App"
        $Path = "../Eduegate.StudentApp"
    }
    "visitor" {
        $appName = "Visitor Mobile App"
        $Path = "../Eduegate.VisitorApp"
    }
    default {
        Write-Error "Invalid appType specified. Please specify either 'parent' , 'staff' , 'student' or 'visitor'."
        exit
    }
}

#Correct the path by merging Local absolute and relative paths
$projectPath = Resolve-Path -Path (Join-Path $originalPath $Path)
$keystorePath = "$projectPath/eduegate.parentapp.keystore"

$configXmlPath = Resolve-Path -Path (Join-Path -Path $projectPath -ChildPath "config.xml")

if ($environment -eq "live") {
    # Read the content of the config.xml
    [xml]$configXml = Get-Content -Path $configXmlPath

    # Get the current version from the widget element's version attribute
    $currentVersion = $configXml.widget.Attributes["version"].Value

    # Prompt the user for a new version
    Write-Host "Current version: $currentVersion"
    $newVersion = Read-Host "Enter new version (or press Enter to keep the current version)"

    # # If the user has entered a new version, update the version
    if ($newVersion -ne "") {
        $changedVersion = $newVersion
        $configXml.widget.Attributes["version"].Value = $newVersion
        Write-Host "Version updated to: $newVersion"
    } else {
        $changedVersion = $currentVersion
        Write-Host "Version remains unchanged."
    }

    # Save the updated XML back to the config file
    $configXml.Save($configXmlPath) 

    # Update appsettings.js based on arguments
    $appSettingsPath = Resolve-Path -Path (Join-Path -Path $projectPath -ChildPath "www/apps/appsettings.js")
    $settingsContent = Get-Content -Path $appSettingsPath -Raw

    # Update current version
    $settingsContent = $settingsContent -replace 'CurrentAppVersion:\s*"\d+\.\d+\.\d+"', "CurrentAppVersion: `"$changedVersion`""


    # Save the modified content back to app.js
    Set-Content -Path $appSettingsPath -Value $settingsContent
}

# Step 4: Confirm Selection
Write-Host "Resolved Project Path: $projectPath"
if ($environment -eq "live") {
    Write-Host "You selected App: $appName, Version: $changedVersion, Client: $client, and Environment: $environment"
} else {
    Write-Host "You selected App: $appName, Client: $client, and Environment: $environment"
}

# Change to project directory and run tf get
Set-Location $projectPath
# Invoke-Expression "tf get"

# Step 5: Update app.js based on selections
$appJsPath = Resolve-Path -Path (Join-Path -Path $projectPath -ChildPath "www/apps/app.js")
$content = Get-Content -Path $appJsPath -Raw

# Update client setting
if ($client -eq "pearl") {
	$content = $content -replace '// var client = "eduegate";', 'var client = "eduegate";'
    $content = $content -replace 'var client = "eduegate";', '// var client = "eduegate";'
    $content = $content -replace '// var client = "pearl";', 'var client = "pearl";'
} elseif ($client -eq "eduegate") {
	$content = $content -replace '// var client = "pearl";', 'var client = "pearl";'
    $content = $content -replace 'var client = "pearl";', '// var client = "pearl";'
    $content = $content -replace '// var client = "eduegate";', 'var client = "eduegate";'
}

# Update environment setting
if ($environment -eq "live") {
	$content = $content -replace '// var environment = "live";', 'var environment = "live";'
    $content = $content -replace '// var environment = "staging";', 'var environment = "staging";'
    $content = $content -replace 'var environment = "staging";', '// var environment = "staging";'
	$content = $content -replace '// var environment = "test";', 'var environment = "test";'
	$content = $content -replace 'var environment = "test";', '// var environment = "test";'
    $content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
    $content = $content -replace 'var environment = "linux";', '// var environment = "linux";'
    $content = $content -replace '// var environment = "local";', 'var environment = "local";'
    $content = $content -replace 'var environment = "local";', '// var environment = "local";'
    
} elseif ($environment -eq "staging") {
    $content = $content -replace '// var environment = "live";', 'var environment = "live";'
    $content = $content -replace 'var environment = "live";', '// var environment = "live";'
    $content = $content -replace '// var environment = "staging";', 'var environment = "staging";'
	$content = $content -replace '// var environment = "test";', 'var environment = "test";'
	$content = $content -replace 'var environment = "test";', '// var environment = "test";'
    $content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
    $content = $content -replace 'var environment = "linux";', '// var environment = "linux";'
    $content = $content -replace '// var environment = "local";', 'var environment = "local";'
    $content = $content -replace 'var environment = "local";', '// var environment = "local";'
} elseif ($environment -eq "test") {
    $content = $content -replace '// var environment = "live";', 'var environment = "live";'
    $content = $content -replace 'var environment = "live";', '// var environment = "live";'
    $content = $content -replace '// var environment = "staging";', 'var environment = "staging";'
	$content = $content -replace 'var environment = "staging";', '// var environment = "staging";'
	$content = $content -replace '// var environment = "test";', 'var environment = "test";'
    $content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
    $content = $content -replace 'var environment = "linux";', '// var environment = "linux";'
    $content = $content -replace '// var environment = "local";', 'var environment = "local";'
    $content = $content -replace 'var environment = "local";', '// var environment = "local";'
} elseif ($environment -eq "linux") {
	$content = $content -replace '// var environment = "live";', 'var environment = "live";'
    $content = $content -replace 'var environment = "live";', '// var environment = "live";'
    $content = $content -replace '// var environment = "staging";', 'var environment = "staging";'
    $content = $content -replace 'var environment = "staging";', '// var environment = "staging";'
	$content = $content -replace '// var environment = "test";', 'var environment = "test";'
	$content = $content -replace 'var environment = "test";', '// var environment = "test";'
    $content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
    $content = $content -replace '// var environment = "local";', 'var environment = "local";'
    $content = $content -replace 'var environment = "local";', '// var environment = "local";'
} elseif ($environment -eq "local") {
	$content = $content -replace '// var environment = "live";', 'var environment = "live";'
    $content = $content -replace 'var environment = "live";', '// var environment = "live";'
    $content = $content -replace '// var environment = "staging";', 'var environment = "staging";'
    $content = $content -replace 'var environment = "staging";', '// var environment = "staging";'
	$content = $content -replace '// var environment = "test";', 'var environment = "test";'
	$content = $content -replace 'var environment = "test";', '// var environment = "test";'
    $content = $content -replace '// var environment = "linux";', 'var environment = "linux";'
    $content = $content -replace 'var environment = "linux";', '// var environment = "linux";'
    $content = $content -replace '// var environment = "local";', 'var environment = "local";'
}

# Save the modified content back to app.js
Set-Content -Path $appJsPath -Value $content

# Step 7: Update config.xml based on client selection
$configContent = Get-Content -Path $configXmlPath -Raw

# Step 6: Set sourceIconPath based on app type and client
switch ($appType) {
    "parent" {
		if ($client -eq "pearl") {
            $iconRelativePath = "../Resources/icons/Pearl/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "../Resources/icons/Eduegate/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        }
        if ($client -eq "pearl") {
			# Uncomment Pearl entries
			$configContent = $configContent -replace '<!-- <name>Pearl Parent</name> -->', '<name>Pearl Parent</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Parent Mobile App</description> -->', '<description>Pearl Parent Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Eduegate Parent</name> -->', '<name>Eduegate Parent</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Parent Mobile App</description> -->', '<description>Eduegate Parent Mobile App</description>'
			
			# Comment out Eduegate entries
			$configContent = $configContent -replace '<name>Eduegate Parent</name>', '<!-- <name>Eduegate Parent</name> -->'
			$configContent = $configContent -replace '<description>Eduegate Parent Mobile App</description>', '<!-- <description>Eduegate Parent Mobile App</description> -->'
		} elseif ($client -eq "eduegate") {
			# Uncomment Eduegate entries
			$configContent = $configContent -replace '<!-- <name>Eduegate Parent</name> -->', '<name>Eduegate Parent</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Parent Mobile App</description> -->', '<description>Eduegate Parent Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Pearl Parent</name> -->', '<name>Pearl Parent</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Parent Mobile App</description> -->', '<description>Pearl Parent Mobile App</description>'
			
			# Comment out Pearl entries
			$configContent = $configContent -replace '<name>Pearl Parent</name>', '<!-- <name>Pearl Parent</name> -->'
			$configContent = $configContent -replace '<description>Pearl Parent Mobile App</description>', '<!-- <description>Pearl Parent Mobile App</description> -->'
		}
    }
    "staff" {
        if ($client -eq "pearl") {
            $iconRelativePath = "../Resources/icons/Pearl/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "../Resources/icons/Eduegate/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        }

        # Comment or uncomment app name and description for Staff App
        if ($client -eq "pearl") {
			# Uncomment Pearl entries
			$configContent = $configContent -replace '<!-- <name>Pearl Staff</name> -->', '<name>Pearl Staff</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Staff Mobile App</description> -->', '<description>Pearl Staff Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Eduegate Staff</name> -->', '<name>Eduegate Staff</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Staff Mobile App</description> -->', '<description>Eduegate Staff Mobile App</description>'
			
			# Comment out Eduegate entries
			$configContent = $configContent -replace '<name>Eduegate Staff</name>', '<!-- <name>Eduegate Staff</name> -->'
			$configContent = $configContent -replace '<description>Eduegate Staff Mobile App</description>', '<!-- <description>Eduegate Staff Mobile App</description> -->'
		} elseif ($client -eq "eduegate") {
			# Uncomment Eduegate entries
			$configContent = $configContent -replace '<!-- <name>Eduegate Staff</name> -->', '<name>Eduegate Staff</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Staff Mobile App</description> -->', '<description>Eduegate Staff Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Pearl Staff</name> -->', '<name>Pearl Staff</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Staff Mobile App</description> -->', '<description>Pearl Staff Mobile App</description>'
			# Comment out Pearl entries
			$configContent = $configContent -replace '<name>Pearl Staff</name>', '<!-- <name>Pearl Staff</name> -->'
			$configContent = $configContent -replace '<description>Pearl Staff Mobile App</description>', '<!-- <description>Pearl Staff Mobile App</description> -->'
		}
    }
    "student" {
        if ($client -eq "pearl") {
            $iconRelativePath = "../Resources/icons/Pearl/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "../Resources/icons/Eduegate/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        }

        # Comment or uncomment app name and description for Student App
        if ($client -eq "pearl") {
			# Uncomment Pearl entries
			$configContent = $configContent -replace '<!-- <name>Pearl Student</name> -->', '<name>Pearl Student</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Student Mobile App</description> -->', '<description>Pearl Student Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Eduegate Student</name> -->', '<name>Eduegate Student</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Student Mobile App</description> -->', '<description>Eduegate Student Mobile App</description>'
			
			# Comment out Eduegate entries
			$configContent = $configContent -replace '<name>Eduegate Student</name>', '<!-- <name>Eduegate Student</name> -->'
			$configContent = $configContent -replace '<description>Eduegate Student Mobile App</description>', '<!-- <description>Eduegate Student Mobile App</description> -->'
		} elseif ($client -eq "eduegate") {
			# Uncomment Eduegate entries
			$configContent = $configContent -replace '<!-- <name>Eduegate Student</name> -->', '<name>Eduegate Student</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Student Mobile App</description> -->', '<description>Eduegate Student Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Pearl Student</name> -->', '<name>Pearl Student</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Student Mobile App</description> -->', '<description>Pearl Student Mobile App</description>'
			# Comment out Pearl entries
			$configContent = $configContent -replace '<name>Pearl Student</name>', '<!-- <name>Pearl Student</name> -->'
			$configContent = $configContent -replace '<description>Pearl Student Mobile App</description>', '<!-- <description>Pearl Student Mobile App</description> -->'
		}
    }
    "visitor" {
        if ($client -eq "pearl") {
            $iconRelativePath = "../Resources/icons/Pearl/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "../Resources/icons/Eduegate/$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        }

        # Comment or uncomment app name and description for Visitor App
        if ($client -eq "pearl") {
			# Uncomment Pearl entries
			$configContent = $configContent -replace '<!-- <name>Pearl Visitor</name> -->', '<name>Pearl Visitor</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Visitor Mobile App</description> -->', '<description>Pearl Visitor Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Eduegate Visitor</name> -->', '<name>Eduegate Visitor</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Visitor Mobile App</description> -->', '<description>Eduegate Visitor Mobile App</description>'
			
			# Comment out Eduegate entries
			$configContent = $configContent -replace '<name>Eduegate Visitor</name>', '<!-- <name>Eduegate Visitor</name> -->'
			$configContent = $configContent -replace '<description>Eduegate Visitor Mobile App</description>', '<!-- <description>Eduegate Visitor Mobile App</description> -->'
		} elseif ($client -eq "eduegate") {
			# Uncomment Eduegate entries
			$configContent = $configContent -replace '<!-- <name>Eduegate Visitor</name> -->', '<name>Eduegate Visitor</name>'
			$configContent = $configContent -replace '<!-- <description>Eduegate Visitor Mobile App</description> -->', '<description>Eduegate Visitor Mobile App</description>'
			$configContent = $configContent -replace '<!-- <name>Pearl Visitor</name> -->', '<name>Pearl Visitor</name>'
			$configContent = $configContent -replace '<!-- <description>Pearl Visitor Mobile App</description> -->', '<description>Pearl Visitor Mobile App</description>'
			# Comment out Pearl entries
			$configContent = $configContent -replace '<name>Pearl Visitor</name>', '<!-- <name>Pearl Visitor</name> -->'
			$configContent = $configContent -replace '<description>Pearl Visitor Mobile App</description>', '<!-- <description>Pearl Visitor Mobile App</description> -->'
		}
    }
    default {
        Write-Host "Invalid app type. Exiting."
        exit
    }
}

# Save the modified content back to config.xml
Set-Content -Path $configXmlPath -Value $configContent



# Step 7: Replace icons based on client selection
# Set destination icon path
$destinationIconPath = "$projectPath/res/"
# Ensure the destination path exists
if (-not (Test-Path -Path $destinationIconPath)) {
    New-Item -Path $destinationIconPath -ItemType Directory
}
# Copy new icons from the selected client
Write-Host "Copying new icons from the selected client..."

if (Test-Path $sourceIconPath) {
	Copy-Item -Path "$sourceIconPath/*" -Destination $destinationIconPath -Recurse -Force
	Write-Host "Done copying new icons from the selected client"
} else {
    Write-Host "The source path does not exist."
}

# Step 8: Change to project directory and run Cordova commands
Set-Location -Path $projectPath
# Lets start building and signing the app
# Step 0: Remove the package-lock.json
Write-Host "Removing package-lock.json..."
if (Test-Path -Path "$projectPath/package-lock.json") {
    Remove-Item -Recurse -Force -Path "$projectPath/package-lock.json"
    Write-Host "package-lock.json has been removed."
} else {
    Write-Host "package-lock.json does not exist. Nothing to remove."
}
# Step 1: Remove the node_modules folder
Write-Host "Removing node_modules folder..."
if (Test-Path -Path "$projectPath/node_modules") {
    Remove-Item -Recurse -Force -Path "$projectPath/node_modules"
    Write-Host "node_modules folder has been removed."
} else {
    Write-Host "node_modules folder does not exist. Nothing to remove."
}
# Step 2: Remove the plugins folder
Write-Host "Removing plugins folder..."
if (Test-Path -Path "$projectPath/plugins") {
    Remove-Item -Recurse -Force -Path "$projectPath/plugins"
    Write-Host "Plugins folder has been removed."
} else {
    Write-Host "Plugins folder does not exist. Nothing to remove."
}


# Step 3: Execute Cordova commands based on $appType

# First, validate the appType
if ($appType -notin "parent", "staff", "visitor", "student") {
    Write-Host "Unsupported app type '$appType'. Please specify a valid app type."
    exit 1
}

Write-Host "Running common Cordova setup for $appType..."

# --- Common Commands (executed for all valid app types) ---
cordova platform rm android
cordova plugin rm cordova-android-play-services-gradle-release
cordova plugin add cordova-android-play-services-gradle-release@latest
cordova plugin add cordova-plugin-wkwebview-engine@latest
cordova plugin add cordova-plugin-wkwebviewxhrfix@latest

# --- App Type Specific Plugin Additions ---
if ($appType -in "parent", "staff", "visitor") {
    Write-Host "Adding specific plugins for $appType..."
    cordova plugin add cordova-plugin-firebase-ml-kit-barcode-scanner@latest

    # --- Parent-Specific Modification ---
    if ($appType -eq "parent") {
        Write-Host "Adding specific plugins for $appType..."
        cordova plugin add cordova-background-geolocation-plugin@latest
        $filePath = "$projectPath\plugins\cordova-background-geolocation-plugin\plugin.xml" # Specify the correct path

        if (Test-Path -Path $filePath) {
            Write-Host "Removing permission.ACTIVITY_RECOGNITION lines from plugin.xml for parent app type..."

            # Read the content of the file
            $fileContent = Get-Content -Path $filePath -Raw

            # Define the lines to remove
            $linesToRemove = @(
                '<uses-permission android:name="com.google.android.gms.permission.ACTIVITY_RECOGNITION" />',
                '<uses-permission android:name="android.permission.ACTIVITY_RECOGNITION" />'
                '<uses-permission android:name="android.permission.FOREGROUND_SERVICE" />'
                '<uses-permission android:name="android.permission.FOREGROUND_SERVICE_LOCATION" />'
            )

            # Loop through the lines to remove and replace them
            foreach ($line in $linesToRemove) {
                # Use -replace with [regex]::Escape for literal string matching
                $fileContent = $fileContent -replace [regex]::Escape($line), ""
            }

            # Write the updated content back to the file
            Set-Content -Path $filePath -Value $fileContent

            Write-Host "permission.ACTIVITY_RECOGNITION lines removed from plugin.xml for parent app type."
        } else {
            Write-Host "Error: plugin.xml not found at $filePath. Skipping line removal."
            # Consider if this should be a failing condition depending on requirements
            # exit 1
        }
    }
    # --- Staff-Specific Modification ---
    if ($appType -eq "staff") {
        Write-Host "Adding specific plugins for $appType..."
        cordova plugin add cordova-background-geolocation-plugin@latest
        cordova plugin add cordova-plugin-camera-preview@latest
        $filePath1 = "$projectPath\plugins\cordova-background-geolocation-plugin\plugin.xml" # Specify the correct path
        $filePath2 = "$projectPath\plugins\cordova-plugin-camera-preview\plugin.xml" # Specify the correct path

        if (Test-Path -Path $filePath1) {
            Write-Host "Removing permission.ACTIVITY_RECOGNITION lines from plugin.xml for staff app type..."

            # Read the content of the file
            $fileContent1 = Get-Content -Path $filePath1 -Raw

            # Define the lines to remove
            $linesToRemove1 = @(
                '<uses-permission android:name="com.google.android.gms.permission.ACTIVITY_RECOGNITION" />',
                '<uses-permission android:name="android.permission.ACTIVITY_RECOGNITION" />'
                '<uses-permission android:name="android.permission.FOREGROUND_SERVICE" />'
                '<uses-permission android:name="android.permission.FOREGROUND_SERVICE_LOCATION" />'
                
            )

            # Loop through the lines to remove and replace them
            foreach ($line1 in $linesToRemove1) {
                # Use -replace with [regex]::Escape for literal string matching
                $fileContent1 = $fileContent1 -replace [regex]::Escape($line1), ""
            }

            # Write the updated content back to the file
            Set-Content -Path $filePath1 -Value $fileContent1

            Write-Host "permission.ACTIVITY_RECOGNITION lines removed from plugin.xml for staff app type."
        } else {
            Write-Host "Error: plugin.xml not found at $filePath1. Skipping line removal."
            # Consider if this should be a failing condition depending on requirements
            # exit 1
        }
        if (Test-Path -Path $filePath2) {
            Write-Host "Removing uses-feature android:name='android.hardware.camera' lines from plugin.xml for staff app type..."

            # Read the content of the file
            $fileContent2 = Get-Content -Path $filePath2 -Raw

            # Define the lines to remove
            $linesToRemove2 = @(
                '<uses-feature android:name="android.hardware.camera" android:required="false"/>'
            )

            # Loop through the lines to remove and replace them
            foreach ($line2 in $linesToRemove2) {
                # Use -replace with [regex]::Escape for literal string matching
                $fileContent2 = $fileContent2 -replace [regex]::Escape($line2), ""
            }

            # Write the updated content back to the file
            Set-Content -Path $filePath2 -Value $fileContent2

            Write-Host "permission.ACTIVITY_RECOGNITION lines removed from plugin.xml for staff app type."
        } else {
            Write-Host "Error: plugin.xml not found at $filePath2. Skipping line removal."
            # Consider if this should be a failing condition depending on requirements
            # exit 1
        }
    }
}
# No specific plugins needed for "student" beyond the common ones

# --- Common Final Commands (executed for all valid app types) ---
Write-Host "Running common final steps for $appType..."
npm install
cordova platform add android@13
cordova build android

Write-Host "Cordova commands completed successfully for $appType."

# apk rename
Write-Host "Renaming the apk..."
$newAPKName = "$appType-app-$client-$environment.apk"
$outputFolderPath = Resolve-Path -Path (Join-Path -Path $projectPath -ChildPath "platforms/android/app/build/outputs/apk/debug")
$outputAPKPath = Resolve-Path -Path (Join-Path -Path $outputFolderPath -ChildPath "app-debug.apk")
Rename-Item -Path $outputAPKPath -NewName $newAPKName
Write-Host "Access the apk here $(Join-Path -Path $outputFolderPath -ChildPath $newAPKName)..."

# Step 4: Copy renamed APK to original path
$sourcePath = Join-Path -Path $outputFolderPath -ChildPath $newAPKName
$destinationPath = Join-Path -Path $originalPath -ChildPath $newAPKName

if (Test-Path $sourcePath) {
    try {
        Copy-Item -Path $sourcePath -Destination $destinationPath -Force -ErrorAction Stop
        Write-Host "Successfully copied APK to: $destinationPath"
    } catch {
        Write-Error "Failed to copy APK to destination. Error: $($_.Exception.Message)"
        exit 1
    }
} else {
    Write-Error "Renamed APK not found at: $sourcePath"
    exit 1
}


if ($environment -eq "live") {
    Set-Location -Path $projectPath
    Write-Host "Building and signing the Cordova project..."
	cordova build android --release -- --keystore=$keystorePath --storePassword=$keyPassword --alias=$keyAlias --password=$keyPassword --packageType=bundle
    $releaseFolderPath = Resolve-Path -Path (Join-Path -Path $projectPath -ChildPath "platforms/android/app/build/outputs/bundle/release")
    $outputAABPath = Resolve-Path -Path (Join-Path -Path $releaseFolderPath -ChildPath "app-release.aab")
    $destinationPath = $destinationPath = Join-Path -Path $originalPath -ChildPath (Split-Path $outputAABPath -Leaf)
    if (Test-Path $outputAABPath) {
        try {
            Copy-Item -Path $outputAABPath -Destination $destinationPath -Force -ErrorAction Stop
            Write-Host "Successfully copied aab bundle to: $destinationPath"
        } catch {
            Write-Error "Failed to copy aab bundle to destination. Error: $($_.Exception.Message)"
            exit 1
        }
    } else {
        Write-Error "aab bundle not found at: $outputAABPath"
        exit 1
    }
}

# Step 9: Return to the original path
Set-Location -Path $originalPath
exit
