#!/bin/bash

# Define variables
REMOTE_PATH="/home/ubuntu/publish/API"
ZIP_FILE=$(ls "$REMOTE_PATH/CoreApiPublish_"*.zip 2> /dev/null)

# Remove any existing CoreMobileApi_Publish* directories
echo "Removing existing CoreApiPublish* directories..."
find "$REMOTE_PATH" -maxdepth 1 -type d -name 'CoreApiPublish*' -exec rm -rf {} +

# Check if the CoreMobileApi_Publish directory exists, if not, create it
if [ ! -d "$REMOTE_PATH/CoreMobileApi_Publish" ]; then
    echo "Creating CoreMobileApi_Publish directory..."
    mkdir -p "$REMOTE_PATH/CoreMobileApi_Publish" && chmod 777 "$REMOTE_PATH/CoreMobileApi_Publish"
fi

# Check if the ZIP file exists
if [ -z "$ZIP_FILE" ]; then
    echo "No zip file found in $REMOTE_PATH."
    exit 1
fi

# Extract the new zip file
echo "Extracting $ZIP_FILE into $REMOTE_PATH/CoreMobileApi_Publish..."
unzip -o "$ZIP_FILE" -d "$REMOTE_PATH/CoreMobileApi_Publish"
echo "Extraction completed successfully."
cp "$REMOTE_PATH/appsettings.json" "$REMOTE_PATH/CoreMobileApi_Publish/"
"$REMOTE_PATH/rebuild_rerun_api.sh"
