import os
import sys
import re
import subprocess
import shutil
import threading
from pathlib import Path
import xml.etree.ElementTree as ET
from paths_assignment import get_available_clients


def convert_powershell_to_python():
    """
    Converts the provided PowerShell script to Python, using pathlib as requested.
    """
    # Step 0: Basic variables
    original_path = Path().cwd()
    environment = "test"
    clients = get_available_clients()
    client = clients

    # Check if environment and client arguments are provided (appType is no longer required)
    if not environment or not client: # Removed check for app_type
        print(f"Error: Missing required arguments.", file=sys.stderr)
        print(f"\nUsage:", file=sys.stderr)
        print(f"    python script_name.py -environment <environment> -client <client>", file=sys.stderr) # Updated usage
        print(f"\nDescription:", file=sys.stderr)
        print(f"    -environment Specify the environment ('test' , 'staging' , 'live' or 'linux').", file=sys.stderr)
        print(f"    -client Specify the client ('pearl' or 'eduegate')", file=sys.stderr)
        print(f"\nExample:", file=sys.stderr)
        print(f"    python script_name.py -environment 'test' -client 'pearl'", file=sys.stderr) # Updated example
        sys.exit(1)
    def force_delete(path):
        if path.exists():
            try:
                shutil.rmtree(path)
                print(f"Deleted {path}")
            except Exception as e:
                print(f"Error deleting {path}: {e}", file=sys.stderr)
                if sys.platform == "win32":
                    subprocess.run(f"rd /s /q \"{path}\"", shell=True, check=False)
                else:
                    subprocess.run(f"rm -rf \"{path}\"", shell=True, check=False)
    def run_cordova_commands(commands):
        """Executes a list of Cordova commands and prints warnings on failure."""
        for cmd in commands:
            print(f"Running: {cmd}")
            process = subprocess.run(cmd, shell=True, check=False)
            if process.returncode != 0:
                print(f"⚠️ Warning: Command failed -> {cmd}")

    def remove_activity_recognition_permission(project_path):
        """Removes ACTIVITY_RECOGNITION permission from background-geolocation plugin.xml for staff app."""
        file_path = Path(project_path, "plugins", "cordova-background-geolocation-plugin", "plugin.xml").resolve()
        if file_path.exists():
            print("Removing permission.ACTIVITY_RECOGNITION lines from plugin.xml for staff app type...")
            with open(file_path, 'r') as f:
                file_content = f.read()
            file_content = file_content.replace(
                '<uses-permission android:name="com.google.android.gms.permission.ACTIVITY_RECOGNITION" />', ""
            ).replace(
                '<uses-permission android:name="android.permission.ACTIVITY_RECOGNITION" />', ""
            )
            with open(file_path, 'w') as f:
                f.write(file_content)
            print("Permissions removed from plugin.xml for staff app type.")

    def get_common_cordova_commands():
        """Returns a list of common Cordova commands for initial setup."""
        return [
            "npm install cordova@latest",
            "cordova platform rm android",
            "cordova plugin rm cordova-android-play-services-gradle-release",
            "cordova plugin add cordova-android-play-services-gradle-release@latest",
            "cordova plugin add cordova-plugin-wkwebview-engine@latest",
            "cordova plugin add cordova-plugin-wkwebviewxhrfix@latest",
            "npm install"
        ]

    def get_plugin_commands(plugins):
        """Generates Cordova commands to add a list of plugins."""
        plugin_cmds = []
        for plugin in plugins:
            plugin_cmds.append(f"cordova plugin add {plugin}@latest")
        return plugin_cmds

    def process_cordova_commands(app_type, project_path):
        """Processes Cordova commands based on app type."""
        print(f"Running Cordova commands for {app_type}...")

        common_commands = get_common_cordova_commands()
        run_cordova_commands(common_commands)

        if app_type == "parent":
            plugins = [
                "cordova-plugin-firebase-ml-kit-barcode-scanner",
                "cordova-background-geolocation-plugin"
            ]
            plugin_commands = get_plugin_commands(plugins)
            run_cordova_commands(plugin_commands)
            build_commands = [
                "cordova platform add android@latest", 
                "cordova build android"
            ]
            run_cordova_commands(build_commands)

        elif app_type == "staff":
            plugins = [
                "cordova-plugin-firebase-ml-kit-barcode-scanner",
                "cordova-background-geolocation-plugin"
            ]
            plugin_commands = get_plugin_commands(plugins)
            run_cordova_commands(plugin_commands)
            remove_activity_recognition_permission(project_path) # Staff-specific permission removal
            build_commands = [
                "cordova platform add android@latest", 
                "cordova build android"
                ] # platform add AFTER permission edit
            run_cordova_commands(build_commands)

        elif app_type == "student":
            build_commands = [
                "cordova platform add android@latest",
                "cordova build android"
            ] # Student app does not build in this snippet
            run_cordova_commands(build_commands)

        elif app_type == "visitor":
            plugins = [
                "cordova-plugin-firebase-ml-kit-barcode-scanner"
            ]
            plugin_commands = get_plugin_commands(plugins)
            run_cordova_commands(plugin_commands)
            build_commands = [
                "cordova platform add android@latest", 
                "cordova build android"
            ]
            run_cordova_commands(build_commands)
        else:
            print(f"Unknown app type: {app_type}")
    # Automatically setting app_name and path based on app_type
    app_types_to_process = ["parent", "staff", "student", "visitor"] # Example list

    for app_type in app_types_to_process:
        # Add this print statement at the VERY BEGINNING of the loop:
        
        if app_type == "parent":
            app_name = "Parent Mobile App"
            path = "../../../eduegateerp.mobileapp/Eduegate.ParentApp"
        elif app_type == "staff":
            app_name = "Staff Mobile App"
            path = "../../../eduegateerp.mobileapp/Eduegate.StaffApp"
        elif app_type == "student":
            app_name = "Student Mobile App"
            path = "../../../eduegateerp.mobileapp/Eduegate.StudentApp"
        elif app_type == "visitor":
            app_name = "Visitor Mobile App"
            path = "../../../eduegateerp.mobileapp/Eduegate.VisitorApp"
        else:
            app_name= "Invalid"
            path= "Invalid"
        print(f"--- Loop Start: app_type = '{app_type}' ---")
        # Now you can work with app_name and path for each valid app_type
        print(f"App Type: {app_type}, App Name: {app_name}, Path: {path}")

        # Correct the path by merging local absolute and relative paths
        project_path = Path(original_path, path).resolve()
        keystore_path = Path(project_path, "eduegate.parentapp.keystore").resolve().as_posix()

        # Set output paths
        output_folder_path = Path(project_path, "platforms/android/app/build/outputs/apk/debug").resolve().as_posix()
        output_apk_path = Path(output_folder_path, "app-debug.apk").resolve().as_posix()

        config_xml_path = Path(project_path, "config.xml").resolve().as_posix()
        print(f"Resolved config Path: {config_xml_path}")
        # Step 4: Confirm Selection
        print(f"Resolved Project Path: {project_path.as_posix()}")
        print(f"You selected App: {app_name}, Client: {client}, and Environment: {environment}")

        # Change to project directory
        os.chdir(project_path)
        # Path.chdir(project_path).resolve()

        current_directory_path = Path.cwd().as_posix()  # Get current working directory as a Path object
        print(f"Current directory (using pathlib.Path.cwd()): {current_directory_path}")
        # Invoke-Expression "tf get"  # Not translated - Assuming this is a TFS command and not needed in Python script

        # Step 5: Update app.js based on selections
        app_js_path = Path(project_path, "www/apps/app.js").resolve().as_posix()
        try:
            with open(app_js_path, 'r') as f:
                content = f.read()
        except FileNotFoundError:
            print(f"Error: {app_js_path} not found.", file=sys.stderr)
            sys.exit(1)

        # Update client setting
        clients = args.client
        if client in clients:  # Validate client
            # 1. Uncomment all client lines if they are commented
            for cli in clients:
                content = re.sub(r'// var client = "' + re.escape(cli) + r'";', r'var client = "' + cli + r'";', content)

            # 2. Now, selectively comment out all *other* client lines
            for cli in clients:
                if cli != client:  # If it's NOT the current client
                    content = re.sub(r'var client = "' + re.escape(cli) + r'";', r'// var client = "' + cli + r'";', content)

        # Update environment setting
        environments = ["live", "staging", "test", "linux", "local"]
        if environment in environments:  # Validate environment
            # 1. Uncomment all environment lines if they are commented
            for env in environments:
                content = re.sub(r'// var environment = "' + re.escape(env) + r'";', r'var environment = "' + env + r'";', content)

            # 2. Now, selectively comment out all *other* environment lines
            for env in environments:
                if env != environment:  # If it's NOT the current environment
                    content = re.sub(r'var environment = "' + re.escape(env) + r'";', r'// var environment = "' + env + r'";', content)

        # Save the modified content back to app.js
        try:
            with open(app_js_path, 'w') as f:
                f.write(content)
        except Exception as e:
            print(f"Error writing to {app_js_path}: {e}", file=sys.stderr)
            sys.exit(1)

        

        # Step 6: Set sourceIconPath based on app type and client
        
        # Resolve sourceIconPath - Client specific path - Moved outside inner if/else
        if client in clients:  # Validate client - Still need to validate client
            icon_relative_path = f"../../../eduegateerp.mobileapp/Resources/icons/{client.lower()}/{app_name.replace(' ', '_')}"
            resolved_icon_path = Path(original_path, icon_relative_path).resolve()
            source_icon_path = resolved_icon_path.as_posix()
            print(f"Resolved sourceIcon Path for AppType '{app_type}': {source_icon_path}")

        # Step 7: Update config.xml based on client selection
        try:
            with open(config_xml_path, 'r', encoding='utf-8') as f:
                config_content = f.read()
        except FileNotFoundError:
            print(f"Error: {config_xml_path} not found.", file=sys.stderr)
            sys.exit(1)

        # **Modify config.xml only for the selected client**
        app_type_lower_first_upper = client.capitalize() + " "
        config_content = re.sub(
            r"<name>\s*.*</name>",
            f"<name>{app_type_lower_first_upper}{app_type.capitalize()}</name>",
            config_content
        )
        config_content = re.sub(
            r"<description>\s*.*</description>",
            f"<description>{app_type_lower_first_upper}{app_name}</description>",
            config_content
        )
        config_content = re.sub(
            r'(<widget [^>]*version=")([^"]+)(")',
            rf'\g<1>1.0.0\g<3>',  
            config_content
        )

        # Save the modified content back to config.xml
        try:
            with open(config_xml_path, 'w', encoding='utf-8') as f:
                f.write(config_content)
        except Exception as e:
            print(f"Error writing to {config_xml_path}: {e}", file=sys.stderr)
            sys.exit(1)


        # Step 7: Replace icons based on client selection
        # Set destination icon path
        destination_icon_path = Path(project_path, "res").resolve().as_posix()
        # Ensure the destination path exists
        Path(destination_icon_path).mkdir(parents=True, exist_ok=True)

        # Copy new icons from the selected client
        print("Copying new icons from the selected client...")

        src_path = Path(source_icon_path)
        dst_path = Path(destination_icon_path)
        if src_path.exists():
            try:
                for item in src_path.iterdir():
                    if item.is_file():
                        shutil.copy(item, dst_path)
                    elif item.is_dir():
                        dest_dir = dst_path / item.name
                        dest_dir.mkdir(parents=True, exist_ok=True)
                        # Copy the entire directory in one call instead of iterating over its contents
                        shutil.copytree(item, dest_dir, dirs_exist_ok=True)
                print("Done copying new icons from the selected client")
            except Exception as e:
                print(f"Error copying icons: {e}", file=sys.stderr)
        else:
            print("The source path does not exist.")

        # Step 8: Change to project directory and run Cordova commands (already in project_path)
        os.chdir(project_path)
        # Remove the package-lock.json
        if sys.platform == "win32":
            subprocess.run("taskkill /F /IM node.exe /T", shell=True, check=False)
            subprocess.run("taskkill /F /IM npm.exe /T", shell=True, check=False)
        else:
            subprocess.run("pkill -f node", shell=True, check=False)

        # time.sleep(2)  # Wait to ensure processes are stopped

        # Function to force delete folders
        

        # Remove package-lock.json
        package_lock_path = project_path / "package-lock.json"
        if package_lock_path.exists():
            try:
                package_lock_path.unlink()
                print("package-lock.json has been removed.")
            except Exception as e:
                print(f"Error removing package-lock.json: {e}", file=sys.stderr)

        # Multi-threaded deletion of node_modules and plugins
        node_modules_path = project_path / "node_modules"
        plugins_path = project_path / "plugins"

        threads = [
            threading.Thread(target=force_delete, args=(node_modules_path,)),
            threading.Thread(target=force_delete, args=(plugins_path,))
        ]
        for thread in threads:
            thread.start()
        for thread in threads:
            thread.join()

        # Function to run Cordova commands
        

        # Determine Cordova commands based on app_type
        
        process_cordova_commands(app_type, project_path)

        # apk rename
        print("Renaming the apk...")
        new_apk_name = f"{app_type}-app-{client}-{environment}.apk"
        new_apk_path = Path(output_folder_path, new_apk_name).resolve().as_posix()
        original_apk_path = Path(output_apk_path) # create Path object for original apk path for rename operation

        try:
            original_apk_path.rename(new_apk_path) # use Path.rename
            print(f"Access the apk here {output_folder_path}/{new_apk_name}...")
        except FileNotFoundError:
            print(f"Error: Original APK not found at {output_apk_path}", file=sys.stderr)
            sys.exit(1)
        except Exception as e:
            print(f"Error renaming APK: {e}", file=sys.stderr)
            sys.exit(1)


        # Step 9: Return to the original path
        os.chdir(original_path.as_posix())

        # Copy the renamed APK to the destination
        destination_path = Path(original_path).parent.parent.joinpath(new_apk_name).resolve().as_posix() # Adjusted destination path using pathlib
        source_apk_for_copy = new_apk_path # source_apk_for_copy is already a string path

        if os.path.exists(source_apk_for_copy): # keep os.path.exists for string path
            try:
                shutil.copy2(source_apk_for_copy, destination_path)
                print(f"Successfully copied APK to: {destination_path}")
            except Exception as e:
                print(f"Failed to copy APK to destination. Error: {e}", file=sys.stderr)
                sys.exit(1)
        else:
            print(f"Renamed APK not found at: {source_apk_for_copy}", file=sys.stderr)
            sys.exit(1)

    sys.exit(0)

if __name__ == "__main__":
    convert_powershell_to_python()