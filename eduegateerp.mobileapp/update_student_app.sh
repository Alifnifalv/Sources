#!/bin/bash


# Define the base directory
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
BASE_DIR="$SCRIPT_DIR/Eduegate.StudentApp"

echo "Starting script to update Eduegate.StudentApp..."

# Remove lines from package.json
PACKAGE_FILE="$BASE_DIR/package.json"
if [ -f "$PACKAGE_FILE" ]; then
    echo "Cleaning up $PACKAGE_FILE..."
    sed -i '' '/"cordova-plugin-wkwebview-engine": "^1.2.2",/d' "$PACKAGE_FILE"
    sed -i '' '/"cordova-plugin-wkwebviewxhrfix": "^0.1.0",/d' "$PACKAGE_FILE"
    echo "Cleaned up $PACKAGE_FILE."
else
    echo "File not found: $PACKAGE_FILE"
fi

# Remove specified folders and files
FILES_TO_REMOVE=(
    "$BASE_DIR/package-lock.json"
    "$BASE_DIR/plugins"
    "$BASE_DIR/node_modules"
)

for FILE in "${FILES_TO_REMOVE[@]}"; do
    if [ -e "$FILE" ]; then
        echo "Removing $FILE..."
        rm -rf "$FILE"
        echo "$FILE removed."
    else
        echo "File or directory not found: $FILE"
    fi
done

# Run Cordova command
cd "$BASE_DIR" || exit 1
echo "Removing iOS platform from Cordova..."
cordova platform rm ios
cordova plugin add cordova-plugin-barcodescanner@latest --force
cordova plugin add @ahovakimyan/cordova-plugin-wkwebviewxhrfix@latest
cordova platform add ios@latest

IOS_PLATFORM_DIR="$BASE_DIR/platforms/ios"
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

echo "Script execution completed!"
