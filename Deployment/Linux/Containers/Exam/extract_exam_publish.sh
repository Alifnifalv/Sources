#!/bin/bash

# Define variables
REMOTE_PATH="/home/ubuntu/publish/Exam"
ZIP_FILE=$(ls "$REMOTE_PATH/CoreExamPublish_"*.zip 2> /dev/null)

# Remove any existing CoreExamPortalPublish* directories
echo "Removing existing CoreExamPortalPublish* directories..."
find "$REMOTE_PATH" -maxdepth 1 -type d -name 'CoreExamPortalPublish*' -exec rm -rf {} +

# Check if the CoreExamPortalPublish directory exists, if not, create it
if [ ! -d "$REMOTE_PATH/CoreExamPortalPublish" ]; then
    echo "Creating CoreExamPortalPublish directory..."
    mkdir -p "$REMOTE_PATH/CoreExamPortalPublish" && chmod 777 "$REMOTE_PATH/CoreExamPortalPublish"
fi

# Check if the ZIP file exists
if [ -z "$ZIP_FILE" ]; then
    echo "No zip file found in $REMOTE_PATH."
    exit 1
fi

# Extract the new zip file
echo "Extracting $ZIP_FILE into $REMOTE_PATH/CoreExamPortalPublish..."
unzip -o "$ZIP_FILE" -d "$REMOTE_PATH/CoreExamPortalPublish"
cp "$REMOTE_PATH/appsettings.json" "$REMOTE_PATH/CoreExamPortalPublish/"
echo "Extraction and cleanup completed successfully."
"$REMOTE_PATH/rebuild_rerun_exam.sh"
