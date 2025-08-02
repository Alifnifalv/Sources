#!/bin/bash

# Define variables
REMOTE_PATH="/home/ubuntu/publish/Signup"
ZIP_FILE=$(ls "$REMOTE_PATH/CoreSignupPublish_"*.zip 2> /dev/null)

# Remove any existing CoreSignupPublish* directories
echo "Removing existing CoreSignupPublish* directories..."
find "$REMOTE_PATH" -maxdepth 1 -type d -name 'CoreSignupPublish*' -exec rm -rf {} +

# Check if the CoreSignupPublish directory exists, if not, create it
if [ ! -d "$REMOTE_PATH/CoreSignupPublish" ]; then
    echo "Creating CoreSignupPublish directory..."
    mkdir -p "$REMOTE_PATH/CoreSignupPublish" && chmod 777 "$REMOTE_PATH/CoreSignupPublish"
fi

# Check if the ZIP file exists
if [ -z "$ZIP_FILE" ]; then
    echo "No zip file found in $REMOTE_PATH."
    exit 1
fi

# Extract the new zip file
echo "Extracting $ZIP_FILE into $REMOTE_PATH/CoreSignupPublish..."
unzip -o "$ZIP_FILE" -d "$REMOTE_PATH/CoreSignupPublish"
cp "$REMOTE_PATH/appsettings.json" "$REMOTE_PATH/CoreSignupPublish/"
echo "Extraction and cleanup completed successfully."
"$REMOTE_PATH/rebuild_rerun_signup.sh"
docker ps -a
