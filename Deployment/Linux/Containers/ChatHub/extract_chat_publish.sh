#!/bin/bash

# Define variables
REMOTE_PATH="/home/ubuntu/publish/ChatHub"
ZIP_FILE=$(ls "$REMOTE_PATH/CoreChatHubPublish_"*.zip 2> /dev/null)

# Remove any existing CoreChatHubPublish* directories
echo "Removing existing CoreChatHubPublish* directories..."
find "$REMOTE_PATH" -maxdepth 1 -type d -name 'CoreChatHubPublish_' -exec rm -rf {} +

# Check if the CoreChatHubPublish directory exists, if not, create it
if [ ! -d "$REMOTE_PATH/CoreChatHubPublish" ]; then
    echo "Creating CoreChatHubPublish directory..."
    mkdir -p "$REMOTE_PATH/CoreChatHubPublish" && chmod 777 "$REMOTE_PATH/CoreChatHubPublish"
fi

# Check if the ZIP file exists
if [ -z "$ZIP_FILE" ]; then
    echo "No zip file found in $REMOTE_PATH."
    exit 1
fi

# Extract the new zip file
echo "Extracting $ZIP_FILE into $REMOTE_PATH/CoreChatHubPublish..."
unzip -o "$ZIP_FILE" -d "$REMOTE_PATH/CoreChatHubPublish"
echo "Extraction completed successfully."
cp "$REMOTE_PATH/appsettings.json" "$REMOTE_PATH/CoreChatHubPublish/"
"$REMOTE_PATH/rebuild_rerun_chat.sh"
