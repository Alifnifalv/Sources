# paths_assignment.py

from pathlib import Path

def get_available_clients():
    
    # If clients are predefined in a config file, fetch from there
    from ..config import parse_args
    args = parse_args()
    client = args.client
    # client = "test"
    return client

def assign_global_paths(original_path):
    original_path = Path.cwd()
    # In this example, we only pass along the original path.
    return {
        "original_path": original_path,
    }

def assign_app_paths(app_type, original_path, client):
    
    # Map app_type to its corresponding app name and project folder.
    if app_type.lower() == "parent":
        app_name = "Parent Mobile App"
        project_folder = "../../eduegateerp.mobileapp/Eduegate.ParentApp"
    elif app_type.lower() == "staff":
        app_name = "Staff Mobile App"
        project_folder = "../../eduegateerp.mobileapp/Eduegate.StaffApp"
    elif app_type.lower() == "student":
        app_name = "Student Mobile App"
        project_folder = "../../eduegateerp.mobileapp/Eduegate.StudentApp"
    elif app_type.lower() == "visitor":
        app_name = "Visitor Mobile App"
        project_folder = "../../eduegateerp.mobileapp/Eduegate.VisitorApp"
    else:
        raise ValueError(f"Invalid app type provided: {app_type}")

    # Resolve the project path based on the original path and project folder.
    project_path = Path(original_path, project_folder).resolve()

    # Define paths for app.js, config.xml, and appsettings.
    app_js_path = Path(project_path, "www", "apps", "app.js").resolve()
    config_xml_path = Path(project_path, "config.xml").resolve()
    appsettings_path = Path(project_path, "www", "apps", "appsettings.json").resolve()  # Adjust as necessary

    # Build the icon path using the provided structure.
    valid_clients = get_available_clients()
    if client.lower() not in valid_clients:
        raise ValueError(f"Invalid client provided: {client}")
    icon_relative_path = f"../../eduegateerp.mobileapp/Resources/icons/{client.lower()}/{app_name.replace(' ', '_')}"
    icon_path = Path(original_path, icon_relative_path).resolve()

    # Define APK output paths based on the assumed folder structure.
    output_folder_path = Path(project_path, "platforms", "android", "app", "build", "outputs", "apk", "debug").resolve()
    output_apk_path = Path(output_folder_path, "app-debug.apk").resolve()

    return {
        "app_name": app_name,
        "project_path": project_path,
        "app_js_path": app_js_path,
        "config_xml_path": config_xml_path,
        "appsettings_path": appsettings_path,
        "icon_path": icon_path,
        "output_folder_path": output_folder_path,
        "output_apk_path": output_apk_path,
    }

if __name__ == "__main__":
    # Example usage:
    # original_path = Path().cwd()
    print("\n paths assigning")
    
