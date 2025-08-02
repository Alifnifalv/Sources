#!/bin/bash

# Accepting arguments for $appType and $newVersion
appType=$1
client=$2
environment=$3
changedVersion=$4

originalPath=$(cd ../ && pwd)

# Check if arguments are provided
if [[ -z "$appType" && -z "$client" && -z "$environment" && -z "$changedVersion" ]]; then
    echo "Error: Missing required arguments.

Usage:
    ./ios-app-build-script.sh <AppType> <client> <environment> <VersionNumber>

Description:
    - AppType: Specify the type of application ('parent', 'staff', 'student', or 'visitor').
    - Client: Specify the client of application ('pearl' or 'eduegate').
    - Environment: Specify the environment of application ('live' or 'staging').
    - VersionNumber: Specify the version number (e.g., '1.2.3').

Example:
    ./ios-app-build-script.sh 'parent' 'pearl' 'live' '1.2.3'"
    exit 1
fi

# Automatically setting $appName and $Path based on $appType
# Check if appType is valid
if [[ "$appType" == "parent" ]]; then
    appName="Parent Mobile App"
    Path="iOS/Eduegate.ParentApp"
    wwwPath="Eduegate.ParentApp/www"
elif [[ "$appType" == "staff" ]]; then
    appName="Staff Mobile App"
    Path="iOS/Eduegate.StaffApp"
    wwwPath="Eduegate.StaffApp/www"
elif [[ "$appType" == "student" ]]; then
    appName="Student Mobile App"
    Path="iOS/Eduegate.StudentApp"
    wwwPath="Eduegate.StudentApp/www"
elif [[ "$appType" == "visitor" ]]; then
    appName="Visitor Mobile App"
    Path="iOS/Eduegate.VisitorApp"
    wwwPath="Eduegate.VisitorApp/www"
else
    echo "Invalid appType specified. Please specify either 'parent', 'staff', 'student', or 'visitor'."
    exit 1
fi

# Retrieve app name and path
echo "App Name: $appName"
echo "App Path: $Path"

# Resolve the project path
projectPath=$(realpath "$originalPath/$Path")

# Define source WWW path and destination project path
sourceWWWPath="$originalPath/$wwwPath"
destinationWWWPath="$projectPath"

# Copy www folder to $projectPath
echo "Copying www folder from $sourceWWWPath to $destinationWWWPath"
if [ -d "$sourceWWWPath" ]; then
    echo "Removing existing content in $destinationWWWPath/www if it exists..."
    if [ -d "$destinationWWWPath/www" ]; then
        rm -rf "$destinationWWWPath/www"
        if [[ $? -ne 0 ]]; then
            echo "Error: Failed to remove existing directory: $destinationWWWPath/www"
            exit 1 # Exit if removal fails, as copy might also fail or be incomplete
        fi
        echo "Successfully removed existing content in $destinationWWWPath/www"
    else
        echo "No existing directory found at $destinationWWWPath/www. Nothing to remove."
    fi
    cp -R "$sourceWWWPath" "$destinationWWWPath"
    echo "Successfully copied $sourceWWWPath folder to $destinationWWWPath"
else
    echo "Error: Source www path does not exist: $sourceWWWPath"
    exit 1
fi

# Update version in config.xml
configXmlPath="$projectPath/config.xml"
configContent=$(cat "$configXmlPath")
# Check if changedVersion is not empty
if [ -n "$changedVersion" ]; then
  # Update current version
  configContent=$(echo "$configContent" | sed -E "s/(<widget[^>]*version=\")([^\"]+)\"/\1$changedVersion\"/")
fi

echo "$configContent" > "$configXmlPath"

# Navigate to project directory
cd "$projectPath" || exit

buildPath="$projectPath/build.json"
buildContent=$(cat "$buildPath")
# Remove the specific line
buildContent=$(echo "$buildContent" | sed '/"authenticationKeyPath":/d')

# Add the new line with the updated path
buildContent=$(echo "$buildContent" | sed "s#\(\"automaticProvisioning\": false,\)#\1\n            \"authenticationKeyPath\": \"${projectPath}/private_keys/AuthKey_Z43S4C4D5H.p8\",#")

# Write the updated content back to the JSON file
echo "$buildContent" > "$buildPath"

# Update app.js based on selections
appJsPath="$projectPath/www/apps/app.js"
content=$(cat "$appJsPath")

# Update client setting
if [[ "$client" == "pearl" ]]; then
    content=$(echo "$content" | sed 's#// var client = "eduegate";#var client = "eduegate";#')
    content=$(echo "$content" | sed 's#var client = "eduegate";#// var client = "eduegate";#')
    content=$(echo "$content" | sed 's#// var client = "pearl";#var client = "pearl";#')
    # content=$(echo "$content" | sed 's#var client = "pearl";#// var client = "pearl";#')
elif [[ "$client" == "eduegate" ]]; then
    content=$(echo "$content" | sed 's#// var client = "eduegate";#var client = "eduegate";#')
    # content=$(echo "$content" | sed 's#var client = "eduegate";#// var client = "eduegate";#')
    content=$(echo "$content" | sed 's#// var client = "pearl";#var client = "pearl";#')
    content=$(echo "$content" | sed 's#var client = "pearl";#// var client = "pearl";#')
else
    echo "Invalid client specified. Please specify either 'pearl' or 'eduegate'."
    exit 1
fi

# Update environment setting
if [[ "$environment" == "live" ]]; then
    content=$(echo "$content" | sed 's#// var environment = "live";#var environment = "live";#')
    # content=$(echo "$content" | sed 's#var environment = "live";#// var environment = "live";#')
    content=$(echo "$content" | sed 's#// var environment = "staging";#var environment = "staging";#')
    content=$(echo "$content" | sed 's#var environment = "staging";#// var environment = "staging";#')
    content=$(echo "$content" | sed 's#// var environment = "test";#var environment = "test";#')
    content=$(echo "$content" | sed 's#var environment = "test";#// var environment = "test";#')
    content=$(echo "$content" | sed 's#// var environment = "linux";#var environment = "linux";#')
    content=$(echo "$content" | sed 's#var environment = "linux";#// var environment = "linux";#')
    content=$(echo "$content" | sed 's#// var environment = "local";#var environment = "local";#')
    content=$(echo "$content" | sed 's#var environment = "local";#// var environment = "local";#')
elif [[ "$environment" == "staging" ]]; then
    content=$(echo "$content" | sed 's#// var environment = "live";#var environment = "live";#')
    content=$(echo "$content" | sed 's#var environment = "live";#// var environment = "live";#')
    content=$(echo "$content" | sed 's#// var environment = "staging";#var environment = "staging";#')
    # content=$(echo "$content" | sed 's#var environment = "staging";#// var environment = "staging";#')
    content=$(echo "$content" | sed 's#// var environment = "test";#var environment = "test";#')
    content=$(echo "$content" | sed 's#var environment = "test";#// var environment = "test";#')
    content=$(echo "$content" | sed 's#// var environment = "linux";#var environment = "linux";#')
    content=$(echo "$content" | sed 's#var environment = "linux";#// var environment = "linux";#')
    content=$(echo "$content" | sed 's#// var environment = "local";#var environment = "local";#')
    content=$(echo "$content" | sed 's#var environment = "local";#// var environment = "local";#')
elif [[ "$environment" == "test" ]]; then
    content=$(echo "$content" | sed 's#// var environment = "live";#var environment = "live";#')
    content=$(echo "$content" | sed 's#var environment = "live";#// var environment = "live";#')
    content=$(echo "$content" | sed 's#// var environment = "staging";#var environment = "staging";#')
    content=$(echo "$content" | sed 's#var environment = "staging";#// var environment = "staging";#')
    content=$(echo "$content" | sed 's#// var environment = "test";#var environment = "test";#')
    # content=$(echo "$content" | sed 's#var environment = "test";#// var environment = "test";#')
    content=$(echo "$content" | sed 's#// var environment = "linux";#var environment = "linux";#')
    content=$(echo "$content" | sed 's#var environment = "linux";#// var environment = "linux";#')
    content=$(echo "$content" | sed 's#// var environment = "local";#var environment = "local";#')
    content=$(echo "$content" | sed 's#var environment = "local";#// var environment = "local";#')
elif [[ "$environment" == "linux" ]]; then
    content=$(echo "$content" | sed 's#// var environment = "live";#var environment = "live";#')
    content=$(echo "$content" | sed 's#var environment = "live";#// var environment = "live";#')
    content=$(echo "$content" | sed 's#// var environment = "staging";#var environment = "staging";#')
    content=$(echo "$content" | sed 's#var environment = "staging";#// var environment = "staging";#')
    content=$(echo "$content" | sed 's#// var environment = "test";#var environment = "test";#')
    content=$(echo "$content" | sed 's#var environment = "test";#// var environment = "test";#')
    content=$(echo "$content" | sed 's#// var environment = "linux";#var environment = "linux";#')
    # content=$(echo "$content" | sed 's#var environment = "linux";#// var environment = "linux";#')
    content=$(echo "$content" | sed 's#// var environment = "local";#var environment = "local";#')
    content=$(echo "$content" | sed 's#var environment = "local";#// var environment = "local";#')
else
    echo "Invalid environment specified. Valid options: live, staging, test, linux."
    exit 1
fi

# Save the modified content back to app.js
echo "$content" > "$appJsPath"

# Confirm selection
echo "Resolved Project Path: $projectPath"
echo "You selected App: $appName, App Version: $changedVersion, Client: $client, and Environment: $environment"

# Path to appsettings.js
appSettingsPath="$projectPath/www/apps/appsettings.js"
settingsContent=$(cat "$appSettingsPath")

# Check and update version in appsettings.js
if [ -n "$changedVersion" ]; then
  # Update current version
  settingsContent=$(echo "$settingsContent" | sed "s/CurrentAppVersion: .*\"/CurrentAppVersion: \"$changedVersion\"/")
fi

# Save the modified content back to appsettings.js
echo "$settingsContent" > "$appSettingsPath"

# Update config.xml based on $appType and $client
configContent=$(<"$configXmlPath")

# Set sourceIconPath based on app type and client

case "$appType" in
    "parent")
        if [[ "$client" == "pearl" ]]; then
            iconRelativePath="Resources/icons/Pearl/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        elif [[ "$client" == "eduegate" ]]; then
            iconRelativePath="Resources/icons/Eduegate/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        fi

        if [[ "$client" == "pearl" ]]; then
            # Uncomment Pearl entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Parent<\/name> -->/<name>Pearl Parent<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Parent Mobile App<\/description> -->/<description>Pearl Parent Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Parent<\/name> -->/<name>Eduegate Parent<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Parent Mobile App<\/description> -->/<description>Eduegate Parent Mobile App<\/description>/')
            
            # Comment out Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<name>Eduegate Parent<\/name>/<!-- <name>Eduegate Parent<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Eduegate Parent Mobile App<\/description>/<!-- <description>Eduegate Parent Mobile App<\/description> -->/')
        elif [[ "$client" == "eduegate" ]]; then
            # Uncomment Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Parent<\/name> -->/<name>Eduegate Parent<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Parent Mobile App<\/description> -->/<description>Eduegate Parent Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Parent<\/name> -->/<name>Pearl Parent<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Parent Mobile App<\/description> -->/<description>Pearl Parent Mobile App<\/description>/')
            
            # Comment out Pearl entries
            configContent=$(echo "$configContent" | sed 's/<name>Pearl Parent<\/name>/<!-- <name>Pearl Parent<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Pearl Parent Mobile App<\/description>/<!-- <description>Pearl Parent Mobile App<\/description> -->/')
        fi
        ;;
    "staff")
        if [[ "$client" == "pearl" ]]; then
            iconRelativePath="Resources/icons/Pearl/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        elif [[ "$client" == "eduegate" ]]; then
            iconRelativePath="Resources/icons/Eduegate/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        fi

        # Comment or uncomment app name and description for Staff App
        if [[ "$client" == "pearl" ]]; then
            # Uncomment Pearl entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Staff<\/name> -->/<name>Pearl Staff<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Staff Mobile App<\/description> -->/<description>Pearl Staff Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Staff<\/name> -->/<name>Eduegate Staff<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Staff Mobile App<\/description> -->/<description>Eduegate Staff Mobile App<\/description>/')
            
            # Comment out Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<name>Eduegate Staff<\/name>/<!-- <name>Eduegate Staff<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Eduegate Staff Mobile App<\/description>/<!-- <description>Eduegate Staff Mobile App<\/description> -->/')
        elif [[ "$client" == "eduegate" ]]; then
            # Uncomment Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Staff<\/name> -->/<name>Eduegate Staff<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Staff Mobile App<\/description> -->/<description>Eduegate Staff Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Staff<\/name> -->/<name>Pearl Staff<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Staff Mobile App<\/description> -->/<description>Pearl Staff Mobile App<\/description>/')
            
            # Comment out Pearl entries
            configContent=$(echo "$configContent" | sed 's/<name>Pearl Staff<\/name>/<!-- <name>Pearl Staff<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Pearl Staff Mobile App<\/description>/<!-- <description>Pearl Staff Mobile App<\/description> -->/')
        fi
        ;;
    "student")
        if [[ "$client" == "pearl" ]]; then
            iconRelativePath="Resources/icons/Pearl/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        elif [[ "$client" == "eduegate" ]]; then
            iconRelativePath="Resources/icons/Eduegate/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        fi

        # Comment or uncomment app name and description for Student App
        if [[ "$client" == "pearl" ]]; then
            # Uncomment Pearl entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Student<\/name> -->/<name>Pearl Student<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Student Mobile App<\/description> -->/<description>Pearl Student Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Student<\/name> -->/<name>Eduegate Student<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Student Mobile App<\/description> -->/<description>Eduegate Student Mobile App<\/description>/')
            
            # Comment out Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<name>Eduegate Student<\/name>/<!-- <name>Eduegate Student<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Eduegate Student Mobile App<\/description>/<!-- <description>Eduegate Student Mobile App<\/description> -->/')
        elif [[ "$client" == "eduegate" ]]; then
            # Uncomment Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Student<\/name> -->/<name>Eduegate Student<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Student Mobile App<\/description> -->/<description>Eduegate Student Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Student<\/name> -->/<name>Pearl Student<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Student Mobile App<\/description> -->/<description>Pearl Student Mobile App<\/description>/')
            
            # Comment out Pearl entries
            configContent=$(echo "$configContent" | sed 's/<name>Pearl Student<\/name>/<!-- <name>Pearl Student<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Pearl Student Mobile App<\/description>/<!-- <description>Pearl Student Mobile App<\/description> -->/')
        fi
        ;;
    "visitor")
        if [[ "$client" == "pearl" ]]; then
            iconRelativePath="Resources/icons/Pearl/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        elif [[ "$client" == "eduegate" ]]; then
            iconRelativePath="Resources/icons/Eduegate/$(echo $appName | sed 's/ /_/g')"
            resolvedIconPath=$(realpath "$originalPath/$iconRelativePath")
            sourceIconPath="$resolvedIconPath"
            echo "Resolved sourceIcon Path: $sourceIconPath"
        fi

        # Comment or uncomment app name and description for Visitor App
        if [[ "$client" == "pearl" ]]; then
            # Uncomment Pearl entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Visitor<\/name> -->/<name>Pearl Visitor<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Visitor Mobile App<\/description> -->/<description>Pearl Visitor Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Visitor<\/name> -->/<name>Eduegate Visitor<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Visitor Mobile App<\/description> -->/<description>Eduegate Visitor Mobile App<\/description>/')
            
            # Comment out Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<name>Eduegate Visitor<\/name>/<!-- <name>Eduegate Visitor<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Eduegate Visitor Mobile App<\/description>/<!-- <description>Eduegate Visitor Mobile App<\/description> -->/')
        elif [[ "$client" == "eduegate" ]]; then
            # Uncomment Eduegate entries
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Eduegate Visitor<\/name> -->/<name>Eduegate Visitor<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Eduegate Visitor Mobile App<\/description> -->/<description>Eduegate Visitor Mobile App<\/description>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <name>Pearl Visitor<\/name> -->/<name>Pearl Visitor<\/name>/')
            configContent=$(echo "$configContent" | sed 's/<!-- <description>Pearl Visitor Mobile App<\/description> -->/<description>Pearl Visitor Mobile App<\/description>/')
            
            # Comment out Pearl entries
            configContent=$(echo "$configContent" | sed 's/<name>Pearl Visitor<\/name>/<!-- <name>Pearl Visitor<\/name> -->/')
            configContent=$(echo "$configContent" | sed 's/<description>Pearl Visitor Mobile App<\/description>/<!-- <description>Pearl Visitor Mobile App<\/description> -->/')
        fi
        ;;
    *)
        echo "Unknown app type: $appType"
        exit 1
        ;;
esac

echo "$configContent" > "$configXmlPath"

# Replace icons based on client selection
# Set destination icon path
destinationIconPath="$projectPath/res/"

# Ensure the destination path exists
if [ ! -d "$destinationIconPath" ]; then
    mkdir -p "$destinationIconPath"
fi

# Copy new icons from the selected client
echo "Copying new icons from the selected client..."

if [ -d "$sourceIconPath" ]; then
    cp -R "$sourceIconPath/"* "$destinationIconPath"
    echo "Done copying new icons from the selected client"
else
    echo "The source path does not exist."
fi


# Change to project directory and run Cordova commands
cd "$projectPath" || { echo "Project directory not found."; exit 1; }

# Remove the package-lock.json file
echo "Removing package-lock.json..."
if [ -f "$projectPath/package-lock.json" ]; then
    rm -rf "$projectPath/package-lock.json"
    echo "package-lock.json has been removed."
else
    echo "package-lock.json does not exist. Nothing to remove."
fi

# Remove the node_modules folder
echo "Removing node_modules folder..."
if [ -d "$projectPath/node_modules" ]; then
    rm -rf "$projectPath/node_modules"
    echo "node_modules folder has been removed."
else
    echo "node_modules folder does not exist. Nothing to remove."
fi

# Remove the plugins folder
echo "Removing plugins folder..."
if [ -d "$projectPath/plugins" ]; then
    rm -rf "$projectPath/plugins"
    echo "Plugins folder has been removed."
else
    echo "Plugins folder does not exist. Nothing to remove."
fi
# Ensure correct UTF-8 encoding before anything else
export LANG=en_US.UTF-8
export LC_ALL=en_US.UTF-8
# Execute Cordova commands based on $appType
if [[ "$appType" == "parent" || "$appType" == "staff" || "$appType" == "visitor" ]]; then
    echo "Running Cordova commands for $appType..."
    cordova platform rm ios
    cordova plugin rm cordova-plugin-firebasex
    cordova plugin add cordova-plugin-firebasex@latest
    cordova plugin add cordova-plugin-barcodescanner
    cordova plugin add @ahovakimyan/cordova-plugin-wkwebviewxhrfix
    if [[ "$appType" == "staff" ]]; then
        # Add the Cordova plugin
        if ! cordova plugin add cordova-plugin-background-mode; then
            echo "Error: Failed to add cordova-plugin-background-mode. Exiting."
            exit 1
        fi

        # Define plugin path
        pluginPath="$projectPath/plugins/cordova-plugin-background-mode/plugin.xml"

        # Check if plugin.xml exists
        if [[ ! -f "$pluginPath" ]]; then
            echo "Error: plugin.xml not found at $pluginPath. Exiting."
            exit 1
        fi

        # Read the content of plugin.xml
        pluginContent=$(<"$pluginPath")
        if [[ -z "$pluginContent" ]]; then
            echo "Error: Failed to read plugin.xml or file is empty. Exiting."
            exit 1
        fi

        # Remove the specific <config-file> block
        updatedContent=$(echo "$pluginContent" | sed '/<config-file target="\*-Info.plist" parent="UIBackgroundModes">/,/<\/config-file>/d')

        # Check if the block was successfully removed
        if [[ "$pluginContent" == "$updatedContent" ]]; then
            echo "Warning: No matching "UIBackgroundModes" block found in plugin.xml. No changes made."
        else
            # Overwrite plugin.xml with the updated content
            echo "$updatedContent" > "$pluginPath"
            if [[ $? -ne 0 ]]; then
                echo "Error: Failed to update plugin.xml. Exiting."
                exit 1
            fi
            echo "Successfully removed "UIBackgroundModes" block and updated plugin.xml."
        fi
    else
        echo "Info: appType is not 'staff'. Skipping plugin modification."
    fi
    cordova platform add ios@latest
elif [[ "$appType" == "student" ]]; then
    echo "Running Cordova commands for $appType..."
    cordova platform rm ios
    cordova plugin add @ahovakimyan/cordova-plugin-wkwebviewxhrfix
    cordova platform add ios@latest
else
    echo "Unsupported app type. Please specify a valid app type."
    exit 1
fi
# Podfile update and install
IOS_PLATFORM_DIR="$projectPath/platforms/ios"
if [ -d "$IOS_PLATFORM_DIR" ]; then
    echo "Cleaning up CocoaPods files in $IOS_PLATFORM_DIR..."
    
    # Remove Podfile.lock and Pods folder
    PODFILE_LOCK="$IOS_PLATFORM_DIR/Podfile.lock"
    PODS_DIR="$IOS_PLATFORM_DIR/Pods"

    if [ -f "$PODFILE_LOCK" ]; then
        echo "Removing $PODFILE_LOCK..."
        rm -f "$PODFILE_LOCK"
        echo "$PODFILE_LOCK removed."
    else
        echo "File not found: $PODFILE_LOCK"
    fi

    if [ -d "$PODS_DIR" ]; then
        echo "Removing $PODS_DIR..."
        rm -rf "$PODS_DIR"
        echo "$PODS_DIR removed."
    else
        echo "Directory not found: $PODS_DIR"
    fi

    # Run CocoaPods commands
    echo "Running CocoaPods commands..."
    cd "$IOS_PLATFORM_DIR" || exit 1
    pod repo update
    pod install
    echo "CocoaPods commands completed."
else
    echo "iOS platform directory not found: $IOS_PLATFORM_DIR"
fi
cd $projectPath

# Build the app for the specified app type
cordova build ios --device --release --buildConfig build.json

# Define the app name based on appType
case "$appType" in
  "parent")
    appName="Pearl Parent"
    ;;
  "staff")
    appName="Pearl Staff"
    ;;
  "student")
    appName="Pearl Student"
    ;;
  "visitor")
    appName="Pearl Visitor"
    ;;
  *)
    echo "Invalid appType: $appType"
    exit 1
    ;;
esac

# Path to the generated IPA file
ipaFilePath="$IOS_PLATFORM_DIR/build/Release-iphoneos/$appName.ipa"

# Validate and upload the IPA file using xcrun
if [[ -f "$ipaFilePath" ]]; then
    echo "Validating $ipaFilePath..."
    cd $projectPath
    chmod 600 private_keys/AuthKey_Z43S4C4D5H.p8
    xcrun altool --validate-app -f "$ipaFilePath" -t ios --apiKey Z43S4C4D5H --apiIssuer 0f2818d3-d109-48a5-a38d-a45106d693a6
    
    if [[ $? -eq 0 ]]; then
        echo "Validation successful. Uploading $ipaFilePath..."
        cd $projectPath
        chmod 600 private_keys/AuthKey_Z43S4C4D5H.p8
        xcrun altool --upload-app -f "$ipaFilePath" -t ios --apiKey Z43S4C4D5H --apiIssuer 0f2818d3-d109-48a5-a38d-a45106d693a6
        
        if [[ $? -eq 0 ]]; then
            echo "$appName uploaded successfully."
        else
            echo "Failed to upload $ipaFilePath."
        fi
    else
        echo "Validation failed for $ipaFilePath."
    fi
else
    echo "$ipaFilePath not found. Build might have failed."
    exit 1
fi

# Return to the original path
cd "$originalPath"

echo "Script completed successfully."   