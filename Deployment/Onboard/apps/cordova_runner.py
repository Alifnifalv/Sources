# cordova_runner.py

import os
import subprocess
import sys
from pathlib import Path

def run_cordova_commands(commands):
    """
    Executes a list of Cordova commands and prints warnings on failure.

    Parameters:
        commands (list): List of Cordova shell commands.
    """
    for cmd in commands:
        print(f"Running: {cmd}")
        process = subprocess.run(cmd, shell=True, check=False)
        if process.returncode != 0:
            print(f"⚠️ Warning: Command failed -> {cmd}")

def remove_activity_recognition_permission(project_path):
    """
    Removes ACTIVITY_RECOGNITION permission from plugin.xml in the staff app.

    Parameters:
        project_path (str): Path to the Cordova project.
    """
    file_path = Path(project_path, "plugins", "cordova-background-geolocation-plugin", "plugin.xml").resolve()
    
    if file_path.exists():
        print("Removing ACTIVITY_RECOGNITION permission from plugin.xml for staff app...")
        
        try:
            with open(file_path, "r", encoding="utf-8") as f:
                content = f.read()

            content = content.replace(
                '<uses-permission android:name="com.google.android.gms.permission.ACTIVITY_RECOGNITION" />', ""
            ).replace(
                '<uses-permission android:name="android.permission.ACTIVITY_RECOGNITION" />', ""
            )

            with open(file_path, "w", encoding="utf-8") as f:
                f.write(content)

            print("✅ Permissions removed from plugin.xml.")
        except Exception as e:
            print(f"⚠️ Error modifying plugin.xml: {e}", file=sys.stderr)
    else:
        print(f"⚠️ Warning: {file_path} not found. Skipping permission removal.")

def get_common_cordova_commands():
    """
    Returns a list of common Cordova commands for setup.
    """
    return [
        "npm install cordova@latest",
        "cordova platform rm android",
        "cordova plugin rm cordova-android-play-services-gradle-release",
        "cordova plugin rm cordova-plugin-firebasex",
        "cordova plugin add cordova-plugin-firebasex@latest",
        "cordova plugin add cordova-android-play-services-gradle-release@latest",
        "cordova plugin add cordova-plugin-wkwebview-engine@latest",
        "cordova plugin add cordova-plugin-wkwebviewxhrfix@latest",
        
    ]

def get_plugin_commands(plugins):
    """
    Generates Cordova commands to add a list of plugins.

    Parameters:
        plugins (list): List of plugin names.

    Returns:
        list: List of plugin installation commands.
    """
    return [f"cordova plugin add {plugin}@latest" for plugin in plugins]

def process_cordova_commands(app_type, project_path):
    """
    Runs Cordova commands based on the app type.

    Parameters:
        app_type (str): The type of the app (parent, staff, student, visitor).
        project_path (str): The Cordova project directory.
    """
    os.chdir(project_path)
    print(f"🚀 Running Cordova commands for {app_type}...")

    run_cordova_commands(get_common_cordova_commands())

    build_commands = ["npm install", "cordova platform add android@13", "cordova build android"]

    if app_type == "parent":
        plugins = ["cordova-plugin-firebase-ml-kit-barcode-scanner", "cordova-background-geolocation-plugin"]
        run_cordova_commands(get_plugin_commands(plugins))
        run_cordova_commands(build_commands)

    elif app_type == "staff":
        plugins = ["cordova-plugin-firebase-ml-kit-barcode-scanner", "cordova-background-geolocation-plugin"]
        run_cordova_commands(get_plugin_commands(plugins))
        remove_activity_recognition_permission(project_path)  # Staff-specific permission removal
        run_cordova_commands(build_commands)

    elif app_type == "student":
        run_cordova_commands(build_commands)  # No plugins for student

    elif app_type == "visitor":
        plugins = ["cordova-plugin-firebase-ml-kit-barcode-scanner"]
        run_cordova_commands(get_plugin_commands(plugins))
        run_cordova_commands(build_commands)

    else:
        print(f"❌ Unknown app type: {app_type}")

    print(f"✅ Cordova build completed for {app_type}.")

if __name__ == "__main__":
    print("Cordova Commands Module loaded. Use process_cordova_commands(app_type, project_path) to build apps.")
