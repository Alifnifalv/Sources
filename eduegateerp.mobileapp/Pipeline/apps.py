import os
import sys
import argparse
import re
import subprocess
import shutil
import glob
from pathlib import Path

def convert_powershell_to_python():
    """
    Converts the provided PowerShell script to Python, using pathlib as requested.
    """

    # Step 0: Basic variables
    original_path = Path().cwd()

    # Argument parsing
    parser = argparse.ArgumentParser(description="Build selective app APKs.")
    parser.add_argument("-appType", type=str, help="Type of application ('parent', 'staff', 'student' or 'visitor')")
    parser.add_argument("-environment", type=str, help="Environment ('test', 'staging', 'live' or 'linux')")
    parser.add_argument("-client", type=str, help="Client ('pearl' or 'eduegate')")

    args = parser.parse_args()

    app_type = args.appType
    environment = args.environment
    client = args.client

    # Check if arguments are provided
    if not app_type or not environment or not client:
        print(f"Error: Missing required arguments.", file=sys.stderr)
        print(f"\nUsage:", file=sys.stderr)
        print(f"    python script_name.py -appType <AppType> -environment <environment> -client <client>", file=sys.stderr)
        print(f"\nDescription:", file=sys.stderr)
        print(f"    -appType    Specify the type of application ('parent' , 'staff' , 'student' or 'visitor').", file=sys.stderr)
        print(f"    -environment Specify the environment ('test' , 'staging' , 'live' or 'linux').", file=sys.stderr)
        print(f"    -client Specify the client ('pearl' or 'eduegate')", file=sys.stderr)
        print(f"\nExample:", file=sys.stderr)
        print(f"    python script_name.py -appType 'parent' -environment 'test' -client 'pearl'", file=sys.stderr)
        sys.exit(1)

    # Automatically setting app_name and path based on app_type
    if app_type == "parent":
        app_name = "Parent Mobile App"
        path = "../Eduegate.ParentApp"
    elif app_type == "staff":
        app_name = "Staff Mobile App"
        path = "../Eduegate.StaffApp"
    elif app_type == "student":
        app_name = "Student Mobile App"
        path = "../Eduegate.StudentApp"
    elif app_type == "visitor":
        app_name = "Visitor Mobile App"
        path = "../Eduegate.VisitorApp"
    else:
        print(f"Invalid appType specified. Please specify either 'parent' , 'staff' , 'student' or 'visitor'.", file=sys.stderr)
        sys.exit(1)

    # Correct the path by merging local absolute and relative paths
    project_path = Path(original_path, path).resolve()
    keystore_path = Path(project_path, "eduegate.parentapp.keystore").resolve().as_posix()

    # Set output paths
    output_folder_path = Path(project_path, "platforms/android/app/build/outputs/apk/debug").resolve().as_posix()
    output_apk_path = Path(output_folder_path, "app-debug.apk").resolve().as_posix()

    config_xml_path = Path(project_path, "config.xml").resolve().as_posix()

    # Step 4: Confirm Selection
    print(f"Resolved Project Path: {project_path.as_posix()}")
    print(f"You selected App: {app_name}, Client: {client}, and Environment: {environment}")

    # Change to project directory
    os.chdir(project_path.as_posix())
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
    clients = ["pearl", "eduegate"]
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

    # Step 7: Update config.xml based on client selection
    try:
        with open(config_xml_path, 'r') as f:
            config_content = f.read()
    except FileNotFoundError:
        print(f"Error: {config_xml_path} not found.", file=sys.stderr)
        sys.exit(1)

    # Step 6: Set sourceIconPath based on app type and client
    app_types = ["parent", "staff", "student", "visitor"]  # Define appTypes array

    # Loop through each appType
    for current_app_type in app_types: # Renamed to avoid shadowing outer app_type
        # Resolve sourceIconPath - Client specific path - Moved outside inner if/else
        if client in clients:  # Validate client - Still need to validate client
            icon_relative_path = f"../Resources/icons/{client.lower()}/{app_name.replace(' ', '_')}"
            resolved_icon_path = Path(original_path, icon_relative_path).resolve()
            source_icon_path = resolved_icon_path.as_posix()
            print(f"Resolved sourceIcon Path for AppType '{current_app_type}': {source_icon_path}")

        # Configure App Name and Description in config_content - Using clients array and "Uncomment First" logic
        # 1. Uncomment both client entries for the current AppType
        for cli in clients:
            app_type_lower_first_upper = cli + current_app_type.lower().capitalize()
            config_content = re.sub(r"<!-- <name>" + re.escape(app_type_lower_first_upper) + r"</name> -->", r"<name>" + app_type_lower_first_upper + r"</name>", config_content)
            config_content = re.sub(r"<!-- <description>" + re.escape(app_type_lower_first_upper) + r" Mobile App</description> -->", r"<description>" + app_type_lower_first_upper + r" Mobile App</description>", config_content)

        # 2. Comment out entry for the *other* client
        for cli in clients:
            if cli != client:
                app_type_lower_first_upper = cli + current_app_type.lower().capitalize()
                config_content = re.sub(r"<name>" + re.escape(app_type_lower_first_upper) + r"</name>", r"<!-- <name>" + app_type_lower_first_upper + r"</name> -->", config_content)
                config_content = re.sub(r"<description>" + re.escape(app_type_lower_first_upper) + r" Mobile App</description>", r"<!-- <description>" + app_type_lower_first_upper + r" Mobile App</description> -->", config_content)


    # Save the modified content back to config.xml
    try:
        with open(config_xml_path, 'w') as f:
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

    if os.path.exists(source_icon_path): # keep os.path.exists for source_icon_path as it is still a string path
        try:
            for file_path in glob.glob(os.path.join(source_icon_path, '*')): # keep os.path.join here as glob needs string path
                if os.path.isfile(file_path): # keep os.path.isfile here
                    shutil.copy(file_path, destination_icon_path)
                elif os.path.isdir(file_path): # keep os.path.isdir here
                     dest_dir = Path(destination_icon_path).joinpath(os.path.basename(file_path)).as_posix() # use pathlib for dest_dir
                     Path(dest_dir).mkdir(parents=True, exist_ok=True) # use pathlib for mkdir
                     for sub_file_path in glob.glob(os.path.join(file_path, '*')): # keep os.path.join here
                         shutil.copy(sub_file_path, dest_dir)

            print("Done copying new icons from the selected client")
        except Exception as e:
            print(f"Error copying icons: {e}", file=sys.stderr)
    else:
        print("The source path does not exist.")

    # Step 8: Change to project directory and run Cordova commands (already in project_path)

    # Lets start building and signing the app
    # Step 0: Remove the package-lock.json
    print("Removing package-lock.json...")
    package_lock_path = Path(project_path, "package-lock.json").resolve().as_posix()
    if os.path.exists(package_lock_path): # keep os.path.exists for string path
        try:
            os.remove(package_lock_path)
            print("package-lock.json has been removed.")
        except Exception as e:
            print(f"Error removing package-lock.json: {e}", file=sys.stderr)
    else:
        print("package-lock.json does not exist. Nothing to remove.")

    # Step 1: Remove the node_modules folder
    print("Removing node_modules folder...")
    node_modules_path = Path(project_path, "node_modules").resolve().as_posix()
    if os.path.exists(node_modules_path): # keep os.path.exists for string path
        try:
            shutil.rmtree(node_modules_path)
            print("node_modules folder has been removed.")
        except Exception as e:
            print(f"Error removing node_modules folder: {e}", file=sys.stderr)
    else:
        print("node_modules folder does not exist. Nothing to remove.")

    # Step 2: Remove the plugins folder
    print("Removing plugins folder...")
    plugins_path = Path(project_path, "plugins").resolve().as_posix()
    if os.path.exists(plugins_path): # keep os.path.exists for string path
        try:
            shutil.rmtree(plugins_path)
            print("Plugins folder has been removed.")
        except Exception as e:
            print(f"Error removing plugins folder: {e}", file=sys.stderr)
    else:
        print("Plugins folder does not exist. Nothing to remove.")


    # Step 3: Execute Cordova commands based on app_type
    if app_type in ["parent", "staff", "visitor"]:
        print(f"Running Cordova commands for {app_type}...")
        try:
            subprocess.run(["cordova", "platform", "rm", "android"], check=True)
            subprocess.run(["cordova", "plugin", "rm", "cordova-android-play-services-gradle-release"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-android-play-services-gradle-release@latest"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-plugin-wkwebview-engine@latest"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-plugin-firebase-ml-kit-barcode-scanner@latest"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-plugin-wkwebviewxhrfix@latest"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-background-geolocation-plugin@latest"], check=True)
            subprocess.run(["npm", "install"], check=True)

            # Remove permission.ACTIVITY_RECOGNITION lines from plugin.xml if the app type is 'staff'
            if app_type == "staff":
                file_path = Path(project_path, "plugins", "cordova-background-geolocation-plugin", "plugin.xml").resolve().as_posix()

                if os.path.exists(file_path): # keep os.path.exists for string path
                    print("Removing permission.ACTIVITY_RECOGNITION lines from plugin.xml for staff app type...")

                    try:
                        with open(file_path, 'r') as f:
                            file_content = f.read()

                        lines_to_remove = [
                            '<uses-permission android:name="com.google.android.gms.permission.ACTIVITY_RECOGNITION" />',
                            '<uses-permission android:name="android.permission.ACTIVITY_RECOGNITION" />'
                        ]

                        for line in lines_to_remove:
                            file_content = file_content.replace(line, "")

                        with open(file_path, 'w') as f:
                            f.write(file_content)

                        print("permission.ACTIVITY_RECOGNITION lines removed from plugin.xml for staff app type.")

                    except Exception as e:
                        print(f"Error processing plugin.xml for staff app: {e}", file=sys.stderr)
                else:
                    print(f"Error: plugin.xml not found at {file_path}. Skipping line removal.", file=sys.stderr)

            subprocess.run(["cordova", "platform", "add", "android@latest"], check=True)
            subprocess.run(["cordova", "build", "android"], check=True)

        except subprocess.CalledProcessError as e:
            print(f"Error running Cordova commands: {e}", file=sys.stderr)
            sys.exit(1)


    elif app_type == "student":
        print(f"Running Cordova commands for {app_type}...")
        try:
            subprocess.run(["cordova", "platform", "rm", "android"], check=True)
            subprocess.run(["cordova", "plugin", "rm", "cordova-android-play-services-gradle-release"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-android-play-services-gradle-release@latest"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-plugin-wkwebview-engine@latest"], check=True)
            subprocess.run(["cordova", "plugin", "add", "cordova-plugin-wkwebviewxhrfix@latest"], check=True)
            subprocess.run(["npm", "install"], check=True)
            subprocess.run(["cordova", "platform", "add", "android@latest"], check=True)
            subprocess.run(["cordova", "build", "android"], check=True)
        except subprocess.CalledProcessError as e:
            print(f"Error running Cordova commands: {e}", file=sys.stderr)
            sys.exit(1)

    else:
        print("Unsupported app type. Please specify a valid app type.", file=sys.stderr)
        sys.exit(1)

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
