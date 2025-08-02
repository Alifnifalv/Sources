# __main__.py
import os
from pathlib import Path
from .config import parse_args
from .database import backup_database, restore_database
from .db_operations import handle_database_operations
from .folders import create_client_folders
from .project import publish_projects
from .docker_operations import handle_docker_operations
from .config_operations import configure_ports_and_sql_updates
from .apps.paths_assignment import assign_global_paths
from .apps.main import main_apps
import time


def main():
    args = parse_args()
    try:
        print("\n=== Creating Database Backup ===")
        backup_path = backup_database(args.sql_server, args.dbname, args.sql_username, args.sql_password, args.target_dir)
        time.sleep(10)
        # Restore from the new backup
        print("\n=== Restoring Client Database ===")
        # 2. Perform Restore (if backup succeeded)
        if backup_path:
            print(f"Backup file path: {backup_path}") # Debugging line
            restore_database(args.sql_server, args.dbname, args.sql_username, args.sql_password, backup_path)
        else:
            print("Backup failed. Restore operation skipped.")

        print(f"\nClient: {args.client}")
        print(f"New Database Name: {args.dbname}")
        print(f"Client IP: {args.client_ip}")
        print(f"SQL Server: {args.sql_server}")
        print(f"SQL Username: {args.sql_username}")
        print(f"Dry Run: {args.dry_run}")

        dry_run = args.dry_run.lower() == 'yes'

        ports, sql_updates = configure_ports_and_sql_updates(args)
        if ports is None or sql_updates is None:
            return

        # Get the directory where the script is located
        script_directory = os.path.dirname(Path(__file__).resolve())

        # Create client folders
        create_client_folders(args.client, script_directory, dry_run)
        
        original_path = Path.cwd()
        global_paths = assign_global_paths(original_path)

        # Handle database operations using sqlcmd
        handle_database_operations(args, dry_run, sql_updates)

        # Uncomment these sections when ready to implement
        if not dry_run:
            publish_projects(script_directory, args, dry_run)
        else:
            print("\n[Dry Run] .NET Projects Publishing process would be executed (but skipped in dry run).")

        handle_docker_operations(args, script_directory, ports)

        if not dry_run:
            print("\nPhase: Building Mobile Apps...")
            main_apps(global_paths, original_path)
            print("\nPhase: Mobile App Build Completed.\n")
        else:
            print("\n Mobile Apps build process would be executed (skipped in dry run)")

        print("\nDry run complete." if dry_run else "\nOnboarding process complete.")
    
    except Exception as e:  # Now covers all onboarding steps
        print(f"Fatal error during process: {str(e)}")
        return 1

if __name__ == '__main__':
    main()