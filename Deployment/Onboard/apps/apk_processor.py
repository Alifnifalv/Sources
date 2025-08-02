# apk_processor.py

import os
import shutil
import sys
from pathlib import Path

def rename_apk(original_apk_path, output_folder_path, app_type, client, environment):
    """
    Renames the generated APK file based on app type, client, and environment.

    Parameters:
        original_apk_path (str): Path to the original APK file.
        output_folder_path (str): Folder where the renamed APK should be placed.
        app_type (str): Type of the app (e.g., parent, staff, student, visitor).
        client (str): Client name.
        environment (str): Deployment environment (e.g., dev, prod).

    Returns:
        str: New APK file path.
    """
    print("🔄 Renaming the APK...")

    new_apk_name = f"{app_type}-app-{client}-{environment}.apk"
    new_apk_path = Path(output_folder_path, new_apk_name).resolve()
    
    try:
        Path(original_apk_path).rename(new_apk_path)
        print(f"✅ APK renamed successfully: {new_apk_path}")
        return new_apk_path.as_posix()
    except FileNotFoundError:
        print(f"❌ Error: Original APK not found at {original_apk_path}", file=sys.stderr)
        sys.exit(1)
    except Exception as e:
        print(f"❌ Error renaming APK: {e}", file=sys.stderr)
        sys.exit(1)

def copy_apk(new_apk_path, destination_path):
    """
    Copies the renamed APK to the final destination.

    Parameters:
        new_apk_path (str): Path to the renamed APK.
        destination_path (str): Target path where the APK should be copied.
    """
    print("📂 Copying APK to destination...")

    if os.path.exists(new_apk_path):
        try:
            shutil.copy2(new_apk_path, destination_path)
            print(f"✅ APK successfully copied to: {destination_path}")
        except Exception as e:
            print(f"❌ Failed to copy APK. Error: {e}", file=sys.stderr)
            sys.exit(1)
    else:
        print(f"❌ Error: Renamed APK not found at {new_apk_path}", file=sys.stderr)
        sys.exit(1)

if __name__ == "__main__":
    print("APK Processor Module loaded. Use rename_apk() and copy_apk() functions.")
