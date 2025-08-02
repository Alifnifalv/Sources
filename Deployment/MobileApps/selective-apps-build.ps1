# Step 0: Save the original path
$originalPath = Get-Location

# Step 1: Prompt for Project Selection
Write-Host "Select the app to build:"
Write-Host "1. Parent Mobile App"
Write-Host "2. Staff Mobile App"
Write-Host "3. Student Mobile App"
Write-Host "4. Visitor Mobile App"
$appChoice = Read-Host "Enter 1, 2, 3, or 4"

# Set app based on selection
switch ($appChoice) {
    "1" { $appName = "Parent Mobile App" }
    "2" { $appName = "Staff Mobile App" }
    "3" { $appName = "Student Mobile App" }
    "4" { $appName = "Visitor Mobile App" }
    default {
        Write-Host "Invalid choice. Exiting."
        exit
    }
}

# Set project path based on selection and determine app type
switch ($appChoice) {
    "1" {
        $Path = "..\..\..\..\eduegateerp.mobileapp\Eduegate.ParentApp"
        $appType = "parent"
    }
    "2" {
        $Path = "..\..\..\..\eduegateerp.mobileapp\Eduegate.StaffApp"
        $appType = "staff"
    }
    "3" {
        $Path = "..\..\..\..\eduegateerp.mobileapp\Eduegate.StudentApp"
        $appType = "student"
    }
    "4" {
        $Path = "..\..\..\..\eduegateerp.mobileapp\Eduegate.VisitorApp"
        $appType = "visitor"
    }
    default {
        Write-Host "Invalid choice. Exiting."
        exit
    }
}

#Correct the path by merging Local absolute and relative paths
$projectPath = Resolve-Path -Path (Join-Path $originalPath $Path)

# Set output paths
$outputAPKPath = "$projectPath\platforms\android\app\build\outputs\apk\debug\app-debug.apk"
$outputFolderPath = "$projectPath\platforms\android\app\build\outputs\apk\debug"

# Step 2: Prompt for Client Selection
Write-Host "Select the client:"
Write-Host "1. Pearl"
Write-Host "2. Eduegate"
$clientChoice = Read-Host "Enter 1 or 2"

# Set client based on selection
switch ($clientChoice) {
    "1" {
        $client = "pearl"
    }
    "2" {
        $client = "eduegate"
    }
    default {
        Write-Host "Invalid choice. Exiting."
        exit
    }
}

# Step 3: Prompt for Environment Selection
Write-Host "Select the environment:"
Write-Host "1. Live"
Write-Host "2. Staging"
Write-Host "3. Test"
Write-Host "4. Linux"
Write-Host "5. Local"
$environmentChoice = Read-Host "Enter 1, 2, 3, 4, or 5"

# Set environment based on selection
switch ($environmentChoice) {
    "1" { $environment = "live" }
    "2" { $environment = "staging" }
    "3" { $environment = "test" }
    "4" { $environment = "linux" }
    "5" { $environment = "local" }
    default { Write-Host "Invalid choice. Exiting."; exit }
}

# Step 4: Confirm Selection
Write-Host "Resolved Project Path: $projectPath"
Write-Host "You selected App: $appName, Client: $client, and Environment: $environment"
$confirm = Read-Host "Confirm selection? (y/n)"
if ($confirm -ne 'y') {
    Write-Host "Operation cancelled."
    exit
}

# Change to project directory and run tf get
cd $projectPath
Invoke-Expression "tf get"

# Step 5: Update app.js based on selections
$appJsPath = "$projectPath\www\apps\app.js"
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
$configXmlPath = "$projectPath\config.xml"
$configContent = Get-Content -Path $configXmlPath -Raw

# Step 6: Set sourceIconPath based on app type and client
switch ($appType) {
    "parent" {
		if ($client -eq "pearl") {
            $iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Pearl\$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Eduegate\$($appName -replace ' ', '_')"
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
            $iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Pearl\$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Eduegate\$($appName -replace ' ', '_')"
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
            $iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Pearl\$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Eduegate\$($appName -replace ' ', '_')"
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
            $iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Pearl\$($appName -replace ' ', '_')"
			$resolvedIconPath = Resolve-Path -Path (Join-Path $originalPath $iconRelativePath)
			$sourceIconPath = $resolvedIconPath.Path
			Write-Host "Resolved sourceIcon Path: $sourceIconPath"
        } elseif ($client -eq "eduegate") {
			$iconRelativePath = "..\..\..\..\eduegateerp.mobileapp\Resources\icons\Eduegate\$($appName -replace ' ', '_')"
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
$destinationIconPath = "$projectPath\res\"
# Ensure the destination path exists
if (-not (Test-Path -Path $destinationIconPath)) {
    New-Item -Path $destinationIconPath -ItemType Directory
}
# Copy new icons from the selected client
Write-Host "Copying new icons from the selected client..."

if (Test-Path $sourceIconPath) {
	Copy-Item -Path "$sourceIconPath\*" -Destination $destinationIconPath -Recurse -Force
	Write-Host "Done copying new icons from the selected client"
} else {
    Write-Host "The source path does not exist."
}

# Step 8: Change to project directory and run Cordova commands
cd $projectPath
Remove-Item -Path "$projectPath\plugins" -Recurse -Force

Invoke-Expression "cordova platform rm android"
Invoke-Expression "cordova platform add android@latest"
Invoke-Expression "cordova build android"

$newAPKName = "$appType-app-$client-$environment.apk"
Rename-Item -Path $outputAPKPath -NewName $newAPKName
Start-Process -FilePath $outputFolderPath

# Step 9: Return to the original path
Set-Location -Path $originalPath
