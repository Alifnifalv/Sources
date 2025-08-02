#!/bin/bash

# Define variables
REMOTE_PATH="/home/ubuntu/publish/ERP"
ZIP_FILE=$(ls "$REMOTE_PATH/CoreERPPublish_"*.zip 2> /dev/null)

# Remove any existing CoreERPPublish* directories
echo "Removing existing CoreERPPublish* directories..."
find "$REMOTE_PATH" -maxdepth 1 -type d -name 'CoreERPPublish*' -exec rm -rf {} +

# Check if the CoreERPPublish directory exists, if not, create it
if [ ! -d "$REMOTE_PATH/CoreERPPublish" ]; then
    echo "Creating CoreERPPublish directory..."
    mkdir -p "$REMOTE_PATH/CoreERPPublish" && chmod 777 "$REMOTE_PATH/CoreERPPublish"
fi

# Check if the ZIP file exists
if [ -z "$ZIP_FILE" ]; then
    echo "No zip file found in $REMOTE_PATH."
    exit 1
fi

# Extract the new zip file
echo "Extracting $ZIP_FILE into $REMOTE_PATH/CoreERPPublish..."
unzip -o "$ZIP_FILE" -d "$REMOTE_PATH/CoreERPPublish"
cp "$REMOTE_PATH/appsettings.json" "$REMOTE_PATH/CoreERPPublish/"
echo "Extraction and cleanup completed successfully."
"$REMOTE_PATH/rebuild_rerun_erp.sh"
