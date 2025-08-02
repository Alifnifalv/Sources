# folders.py
import os
import shutil
from pathlib import Path

def create_client_folders(client_id, script_dir, dry_run=True):
    """
    Creates client-specific folders by copying a template.

    Imagine setting up a new branch of a store. You have a 'template store' that has the basic setup.
    This function copies that 'template store' to create a new store for a specific 'client'.

    It creates folders for the new client in several different 'locations' (predefined paths),
    copying the 'template' into each of these new client folders.

    Args:
        client_id (str): The name of the client, which will be used as the new folder name.
        script_dir (str): The directory where the script is running from. This helps find the 'template store'.
        dry_run (bool, optional): If True, it will only print what it *would* do, without actually creating folders. Defaults to True (dry run mode).
    """
    base_path = Path(Path(script_dir, r"../../Presentation").as_posix()).resolve() # Navigate up to 'Presentation'
    client_folder_name = client_id
    folder_paths = [
        Path(base_path, "Eduegate.ERP.AdminCore", "wwwroot", "Clients").resolve().as_posix(),
        Path(base_path, "Eduegate.ERP.School.PortalCore", "wwwroot", "Clients").resolve().as_posix(),
        Path(base_path, "Eduegate.OnlineExam.PortalCore", "wwwroot", "Clients").resolve().as_posix(),
        Path(base_path, "Eduegate.Signup.PortalCore", "wwwroot", "Clients").resolve().as_posix(),
        Path(base_path, "Eduegate.Vendor.PortalCore", "wwwroot", "Clients").resolve().as_posix(),
    ]
    # folder_paths = [path.replace("\\", "/") for path in folder_paths]
    template_folder_name_lower = "eduegate"  # Lowercase template folder name for comparison

    print("\nClient Folder Creation (Copying Template - Case Insensitive Check):")
    print(f"Debug: Base path (base_path): {base_path}")

    for parent_folder in folder_paths:
        template_folder_found = False
        actual_template_folder_path = None
        parent_folder_contents = os.listdir(parent_folder) if os.path.exists(parent_folder) else [] # List contents only if parent exists

        for item_name in parent_folder_contents:
            if item_name.lower() == template_folder_name_lower:
                actual_template_folder_path = Path(parent_folder, item_name).resolve().as_posix() # Use the actual name from listing
                template_folder_found = True
                break # Found it, no need to check other items in the directory

        destination_folder = Path(parent_folder, client_folder_name).resolve().as_posix()

        if not template_folder_found:
            print(f"Warning: Template folder 'eduegate' (case-insensitive) not found in '{parent_folder}'. Skipping copy for {destination_folder}.")
            continue  # Skip to the next folder if template is missing

        if dry_run:
            print(f"[Dry Run] Would copy template from '{actual_template_folder_path}' to '{destination_folder}'")
        else:
            try:
                shutil.copytree(actual_template_folder_path, destination_folder) # Use actual path for copy
                print(f"Copied template from '{actual_template_folder_path}' to '{destination_folder}'")
            except FileExistsError:
                print(f"Warning: Destination folder '{destination_folder}' already exists. Skipping copy.")
            except Exception as e:
                print(f"Error copying folder from '{actual_template_folder_path}' to '{destination_folder}': {e}")

    print("Client folder creation process completed.")
