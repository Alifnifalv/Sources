#!/bin/bash

# Define variables
REMOTE_PATH="/home/ubuntu/publish/ParentPortal"
ZIP_FILE=$(ls "$REMOTE_PATH/Core"*.zip 2> /dev/null)

# Remove any existing CoreParentPortalPublish* directories
echo "Removing existing CoreParentPortalPublish* directories..."
find "$REMOTE_PATH" -maxdepth 1 -type d -name 'CoreParentPortalPublish*' -exec rm -rf {} +

# Check if the CoreParentPortalPublish directory exists, if not, create it
if [ ! -d "$REMOTE_PATH/CoreParentPortalPublish" ]; then
    echo "Creating CoreParentPortalPublish directory..."
    mkdir -p "$REMOTE_PATH/CoreParentPortalPublish" && chmod 777 "$REMOTE_PATH/CoreParentPortalPublish"
fi

# Check if the ZIP file exists
if [ -z "$ZIP_FILE" ]; then
    echo "No zip file found in $REMOTE_PATH."
    exit 1
fi

# Extract the new zip file
echo "Extracting $ZIP_FILE into $REMOTE_PATH/CoreParentPortalPublish..."
unzip -o "$ZIP_FILE" -d "$REMOTE_PATH/CoreParentPortalPublish"
cp "$REMOTE_PATH/appsettings.json" "$REMOTE_PATH/CoreParentPortalPublish/"
echo "Extraction completed successfully."

# Run the rebuild script
echo "Running rebuild script..."
chmod +x "$REMOTE_PATH/rebuild_rerun_parent.sh"
"$REMOTE_PATH/rebuild_rerun_parent.sh"

docker ps -a
