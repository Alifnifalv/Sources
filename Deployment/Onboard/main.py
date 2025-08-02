import os
import sys
import shutil
import subprocess
import threading
import re
from pathlib import Path
from paths_assignment import assign_app_paths, assign_global_paths
from config_updater import update_config_xml, update_app_js, get_available_clients
from cleanup import clean_project, force_delete
from icon_manager import copy_icons
from cordova_runner import process_cordova_commands
from apk_processor import copy_apk

def main():
    
    app_types_to_process = ["parent", "staff", "student", "visitor"]
    
    # Dynamically retrieve available clients and use the first one
    clients = get_available_clients()
    if not clients:
        print("Error: No available clients found.", file=sys.stderr)
        sys.exit(1)
    # client = clients[0]
    client = 
    environment = "test"  # Example environment
    global_paths = assign_global_paths(original_path)
    for app_type in app_types_to_process:
        print(f"--- Loop Start: app_type = '{app_type}' ---")
        
        paths = assign_app_paths(app_type, original_path, client), assign_global_paths(original_path)
        paths.update(global_paths)

        original_path = paths["original_path"]
        app_name = paths["app_name"]
        project_path = paths["project_path"]
        app_js_path = paths["app_js_path"]
        appsettings_path = paths["appsettings_path"]
        config_xml_path = paths["config_xml_path"]
        icon_path = paths["icon_path"]
        output_folder_path = paths["output_folder_path"]
        output_apk_path = paths["output_apk_path"]
        destination_icon_path = paths["icon_path"]  # Adjust if different
        
        print(f"Resolved Project Path: {project_path}")
        print(f"You selected App: {app_name}, Client: {client}, and Environment: {environment}")
        
        os.chdir(project_path)
        
        update_config_xml(config_xml_path, client, app_type, app_name)

        update_app_js(app_js_path)
        
        copy_icons(icon_path, destination_icon_path)
        
        clean_project(project_path)
        
        process_cordova_commands(app_type, project_path)
        
        copy_apk(output_apk_path, app_type, client, environment, original_path)
        
        os.chdir(original_path)

    sys.exit(0)

if __name__ == "__main__":
    main()