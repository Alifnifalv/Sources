# cleanup.py

import os
import sys
import shutil
import subprocess
import threading
from pathlib import Path
from .paths_assignment import assign_app_paths, assign_global_paths

global_paths = assign_global_paths(None) # Pass None as it's overwritten anyway in assign_global_paths
original_path = global_paths["original_path"]

def force_delete(path):
    """
    Recursively deletes a folder, handling platform-specific permission issues.
    
    Parameters:
        path (str): The path to delete.
    """
    path = Path(path).resolve()
    if path.exists():
        try:
            shutil.rmtree(path)
            print(f"Deleted: {path}")
        except PermissionError:
            print(f"Permission error while deleting: {path}, attempting platform-specific fix.", file=sys.stderr)
            if sys.platform == "win32":
                subprocess.run(["rmdir", "/s", "/q", str(path)], shell=True)
            else:
                subprocess.run(["rm", "-rf", str(path)])
            print(f"Force-deleted: {path}")

def stop_services():
    """
    Stops running Node.js processes based on the operating system.
    """
    try:
        if sys.platform == "win32":
            subprocess.run(["taskkill", "/F", "/IM", "node.exe"], stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        else:
            subprocess.run(["pkill", "-f", "node"], stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        print("Stopped Node.js services.")
    except Exception as e:
        print(f"Failed to stop services: {e}", file=sys.stderr)

def clean_project(project_path):
    """
    Deletes package-lock.json, node_modules, and plugins folder using multi-threaded deletion.

    Parameters:
        project_path (str): The root project directory.
    """
    project_path = Path(project_path).resolve()
    
    paths_to_delete = [
        project_path / "package-lock.json",
        project_path / "node_modules",
        project_path / "plugins"
    ]
    
    # Remove package-lock.json first (file deletion is simpler)
    if paths_to_delete[0].exists():
        try:
            paths_to_delete[0].unlink()
            print(f"Deleted: {paths_to_delete[0]}")
        except Exception as e:
            print(f"Error deleting {paths_to_delete[0]}: {e}", file=sys.stderr)
    
    # Multi-threaded deletion for directories
    threads = []
    for path in paths_to_delete[1:]:
        thread = threading.Thread(target=force_delete, args=(path,))
        thread.start()
        threads.append(thread)

    for thread in threads:
        thread.join()

if __name__ == "__main__":
    print("Cleanup module loaded. Use clean_project() to remove unwanted files and stop_services() to stop processes.")
