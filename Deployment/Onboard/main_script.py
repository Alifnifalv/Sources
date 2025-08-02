# main_script.py
import os
from pathlib import Path
import subprocess
import pyodbc
from config import parse_args
from database import backup_database, restore_database
from folders import create_client_folders
from project import publish_projects
from docker_operations import handle_docker_operations
from config_operations import configure_ports_and_sql_updates
from apps.paths_assignment import assign_global_paths
from apps.main import main_apps

def main():
    args = parse_args()
    # backup_path="F:/DB_Backups/Eduegate_Blank.bak"
    # backup_dir="F:/DB_Backups" # Define backup directory
    # backup_filename="Eduegate_Blank.bak" # Define backup filename

    print("\nConfiguration Summary:")
    print(f"Client: {args.client}")
    print(f"Database Name: {args.dbname}")
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
    
    original_path = Path.cwd() # <--- Original path calculation (already present in your code)
    global_paths = assign_global_paths(original_path) # <--- Global paths calculation (already present)


    # if dry_run:
    #     print("\nDry run complete. No changes applied.")
    #     print(f"Would backup database {args.dbname} to {Path(backup_dir, backup_filename).as_posix()} (no actual backup).")
    #     print(f"Would restore database from {backup_path} to {args.dbname} (no actual restore).")
    #     # Optionally, print the generated SQL or restore command here
    # else:
    #     print("\nApplying database changes...")
    #     conn_str = f"DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={args.sql_server};UID={args.sql_username};PWD={args.sql_password}"
    #     conn = pyodbc.connect(conn_str)

    #     # Backup database before restore
    #     backup_database(conn, backup_dir, backup_filename)
    #     conn.close()
    #     conn = pyodbc.connect(conn_str)
    #     restore_database(conn, args, backup_path)

    #     # Switch to the restored database
    #     cursor = conn.cursor()
    #     target_db = args.dbname
    #     print(f"Switching database context to '{target_db}'...")
    #     try:
    #         cursor.execute(f"USE [{target_db}]") # Explicitly use the target database
    #         conn.commit() # Commit the USE database command
    #         print(f"Successfully switched to database '{target_db}'.")
    #     except pyodbc.Error as use_db_error:
    #         print(f"Error switching to database '{target_db}': {use_db_error}")
    #         conn.close() # Close connection if switching fails
    #         return # Exit if database switching fails

    # # Execute SQL UPDATE queries after restore
    # if args.dry_run.lower() != 'yes':
    #     cursor = conn.cursor()
    #     print("\nExecuting SQL Update Queries...")
    #     try:
    #         for setting_code, value in sql_updates.items():
    #             update_query = f"UPDATE setting.Settings SET SettingValue = ? WHERE SettingCode = ?" # Use parameterized query to avoid SQL injection
    #             cursor.execute(update_query, value, setting_code) # Pass value and setting_code as parameters
    #             print(f"Executed: UPDATE setting.Settings SET SettingValue = '{value}' WHERE SettingCode = '{setting_code}';")
    #         conn.commit() # Commit all updates
    #         print("SQL Updates applied successfully.")
    #     except pyodbc.Error as sql_e:
    #         print(f"Error executing SQL Updates: {sql_e}")
    #         conn.rollback() # Rollback if any update fails
    #     finally:
    #         conn.close() # Close connection in finally block to ensure it's always closed
    # else:
    #     print("\n[Dry Run] Skipping SQL Update Queries.")

    # if args.dry_run.lower() != 'yes':
    #     publish_projects(script_directory, args, dry_run) # Call the function from project.py
    # else:
    #     print("\n[Dry Run] .NET Projects Publishing process would be executed (but skipped in dry run).")
    # execute_publish_script(script_directory, args.client, args.dry_run.lower() == 'yes')
    # print("docker image building") # adding to check docker image building

    # if args.dry_run.lower() != 'yes':
    #     handle_docker_operations(args, script_directory, ports) # Call the function to handle docker operations

    # print("\nDry run complete." if dry_run else "\nOnboarding process with Dockerization complete.")
    
    if args.dry_run.lower() != 'yes':
        # --- Call convert_powershell_to_python here, after publishing (or wherever appropriate) ---
        print("\nPhase: Building Mobile Apps...")
        main_apps(global_paths, original_path) # Pass the 'ports' dictionary
        print("\nPhase: Mobile App Build Completed.\n")
    else:
        print("\n Mobile Apps build failed.")

if __name__ == '__main__':
    main()
