# icon_manager.py

import shutil
import sys
from pathlib import Path

def ensure_test_client_icons(icon_path, client): # New function to create "test" icons if needed
    """
    Ensures that the 'test' client icons folder exists by copying from 'Eduegate' if necessary.
    Parameters:
        icon_path (str): Path to the 'test' client's icon folder.
    """
    capitalized_client = client.capitalize() # Capitalize the client name dynamically
    destination_client_icons_path_str = Path(Path(icon_path).resolve().parent.parent, capitalized_client) # Use capitalized client name
    destination_client_icons_path = Path(destination_client_icons_path_str)
    if not Path(destination_client_icons_path).exists(): # Check if "test" icons folder exists
        print(f"Creating 'test' client icons folder by copying from 'Eduegate'...")
        source_eduegate_icons_path = Path(Path(icon_path).parent.parent, "Eduegate").resolve().as_posix() # Path to Eduegate icons (relative to icon_path)
        try:
            shutil.copytree(source_eduegate_icons_path, destination_client_icons_path) # Copy Eduegate icons to "test" icons folder
            print(f"✅ 'test' client icons folder created at: {destination_client_icons_path}")
        except Exception as e:
            print(f"❌ Error copying 'Eduegate' icons to 'test' client folder: {e}", file=sys.stderr)
            sys.exit(1)

def copy_icons(icon_path, project_path):
    """
    Copies icons from the source folder to the destination `res` folder inside the project.

    Parameters:
        source_icon_path (str): Path to the source folder containing the icons.
        project_path (str): Path to the project where icons should be copied.

    Returns:
        None
    """
    destination_icon_path = Path(project_path, "res").resolve()
    destination_icon_path.mkdir(parents=True, exist_ok=True)  # Ensure destination exists

    print("🔄 Copying new icons from the selected client...")

    src_path = Path(icon_path)
    dst_path = destination_icon_path
    

    if src_path.exists():
        try:
            for item in src_path.iterdir():
                if item.is_file():
                    shutil.copy(item, dst_path)
                elif item.is_dir():
                    dest_dir = dst_path / item.name
                    dest_dir.mkdir(parents=True, exist_ok=True)
                    shutil.copytree(item, dest_dir, dirs_exist_ok=True)  # Copy entire directory
            print("✅ Icons copied successfully!")
        except Exception as e:
            print(f"❌ Error copying icons: {e}", file=sys.stderr)
            sys.exit(1)
    else:
        print("❌ The source icon path does not exist.", file=sys.stderr)
        sys.exit(1)
    # return {"destination_icon_path": dst_path}

if __name__ == "__main__":
    print("Icon Manager Module loaded. Use copy_icons() function.")
