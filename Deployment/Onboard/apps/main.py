import os
import sys
import shutil
import subprocess
import threading
import re
from pathlib import Path
from .paths_assignment import assign_app_paths, assign_global_paths, get_available_clients
from .config_updater import update_config_xml, update_app_js
from .cleanup import clean_project
from .icon_manager import ensure_test_client_icons, copy_icons
from .cordova_runner import process_cordova_commands
from .apk_processor import rename_apk, copy_apk

def main_apps(global_paths, original_path):
    
    app_types_to_process = ["parent", "staff", "student", "visitor"]
    
    # Dynamically retrieve available clients and use the first one
    clients = get_available_clients()
    if not clients:
        print("Error: No available clients found.", file=sys.stderr)
        sys.exit(1)
    client = clients
    print(f"Initial client value BEFORE loop: '{client}'")
    environment = "test"  # Example environment
    # original_path = Path.cwd() # Get original path *FIRST* - ASSIGN VALUE HERE
    global_paths = assign_global_paths(original_path) # Now original_path is defined

    for app_type in app_types_to_process:
        print(f"--- Loop Start: app_type = '{app_type}', client = '{client}' ---")
        paths = assign_app_paths(app_type, original_path, client) # Get app-specific paths (dictionary)
        global_paths = assign_global_paths(original_path) # Get global paths (dictionary)
        paths.update(global_paths) # Merge global_paths into paths dictionary

        original_path = paths["original_path"]
        app_name = paths["app_name"]
        project_path = paths["project_path"]
        app_js_path = paths["app_js_path"]
        appsettings_path = paths["appsettings_path"]
        config_xml_path = paths["config_xml_path"]
        icon_path = paths["icon_path"]
        output_folder_path = paths["output_folder_path"]
        output_apk_path = paths["output_apk_path"]
        
        os.chdir(project_path) 
        
        print(f"Resolved Project Path: {project_path}")
        print(f"You selected App: {app_name}, Client: {client}, and Environment: {environment}")
        print(f"Resolved icon_path BEFORE copy_icons: {icon_path}")
        
        
        
        update_config_xml(config_xml_path, client, app_type, app_name)

        update_app_js(app_js_path, client, environment)
        
        ensure_test_client_icons(icon_path, client)

        copy_icons(icon_path, project_path)
        
        clean_project(project_path)
        
        process_cordova_commands(app_type, project_path)
        
        new_apk_path = rename_apk(output_apk_path, output_folder_path, app_type, client, environment) # Rename APK
        new_apk_name = f"{app_type}-app-{client}-{environment}.apk" # Reconstruct new_apk_name (or use the one from rename_apk return if you modified rename_apk to return it)
        destination_path = Path(original_path).parent.parent.joinpath(new_apk_name).resolve().as_posix() # Calculate destination path
        copy_apk(new_apk_path, destination_path)
        
        os.chdir(original_path)

    sys.exit(0)

if __name__ == "__main__":
    main_apps()