# database.py
import os
import subprocess
from pathlib import Path
from.config import parse_args
args = parse_args()
target_dir = args.target_dir
def backup_database(sql_server, dbname, sql_username, sql_password, backup_path):
    """Backs up a database using sqlcmd CLI tool to a .bak file and returns the file path."""
    # Convert backup_path to a Path object and ensure the directory exists
    backup_file = os.path.join(backup_path, "Eduegate_Blank.bak")
    # backup_file = Path(backup_path, "Eduegate_Blank.bak").as_posix()
    backup_dir = os.path.dirname(backup_file)  # Get the parent directory
    # backup_dir = Path(backup_file).parent  # Get the parent directory
    os.makedirs(backup_dir, exist_ok=True)  # Create directories if they don't exist
    # backup_dir.mkdir(parents=True, exist_ok=True)


    # Build the SQL command for backup
    backup_sql = (
        f"BACKUP DATABASE [Eduegate_Blank] "
        f"TO DISK = N'{backup_file}' "
        f"WITH NOFORMAT, NOINIT, "
        f"NAME = N'Eduegate_Blank-Full Database Backup', "
        f"SKIP, NOREWIND, NOUNLOAD, STATS = 10"
    )
    
    print(f"Executing SQL:\n{backup_sql}")
    
    # Construct the sqlcmd command with necessary parameters
    cmd = [
        "sqlcmd",
        "-S", sql_server,
        "-U", sql_username,
        "-P", sql_password,
        "-Q", backup_sql
    ]
    
    try:
        # Run the sqlcmd command
        result = subprocess.run(cmd, capture_output=True, text=True, check=True)
        print("Database backup completed successfully.")
        print("Output:", result.stdout)
        print("Error:", result.stderr) 
        
        # Optionally check that the backup file exists after execution
        if not os.path.exists(backup_file):
            raise RuntimeError(f"Backup file {backup_file} was not created")
            
        return backup_file
        
    except subprocess.CalledProcessError as e:
        print("Database backup failed.")
        print("Error Output:", e.stderr)
        return None

# if __name__ == "__main__":
    # Example usage:
    # backup_database("localhost", "Eduegate_Blank", "eduegateuser", "eduegate@123", r"F:\DB_Backups\test.bak")
def get_restore_path(sql_server, sql_username, sql_password):
    cmd = [
        "sqlcmd",
        "-S", sql_server,
        "-U", sql_username,
        "-P", sql_password,
        "-Q", "SET NOCOUNT ON; SELECT SERVERPROPERTY('InstanceDefaultDataPath'), SERVERPROPERTY('InstanceDefaultLogPath')",
        "-s", "|", "-W", "-h", "-1", "-k", "1"
    ]
    try:
        result = subprocess.run(cmd, capture_output=True, text=True, check=True)
        output_line = [line for line in result.stdout.split('\n') if line.strip()][0]
        data_path, log_path = output_line.split('|')
        return data_path.strip(), log_path.strip()
    except subprocess.CalledProcessError as e:
        print(f"Error getting InstanceDefaultBackupPath: {e.stderr}")
        return None

def restore_database(sql_server, dbname, sql_username, sql_password, backup_path):
    """Restores a database from a backup using sqlcmd CLI tool."""
    data_path, log_path = get_restore_path(sql_server, sql_username, sql_password)
    if not data_path or not log_path:
        return False    
    backup_file = os.path.abspath(backup_path)
    print(f"started restoring {backup_file}")
    # Get file list from backup using proper delimiters
    filelist_cmd = [
        "sqlcmd",
        "-S", sql_server,
        "-U", sql_username,
        "-P", sql_password,
        "-Q", f"RESTORE FILELISTONLY FROM DISK=N'{backup_file}';",
        "-s", "|",
        "-W",
        "-h", "-1"
    ]
    
    result = subprocess.run(filelist_cmd, capture_output=True, text=True, check=True)
    print(f"{filelist_cmd}")
    lines = [line.split('|') for line in result.stdout.strip().split('\n') if line.strip()]

    move_clauses = []
    for cols in lines:
        if len(cols) >= 3:
            logical_name = cols[0].strip()
            file_type_id = cols[2].strip()
            
            if file_type_id == 'D':
                target_dir = data_path
                file_type = 'Data'
                ext = "mdf"
            else:
                target_dir = log_path
                file_type = 'Log'
                ext = "ldf"
            
            os.makedirs(target_dir, exist_ok=True)
            new_path = os.path.join(target_dir, f"{dbname}_{file_type}.{ext}")
            move_clauses.append(f"MOVE '{logical_name}' TO '{new_path}'")
    move_clause = ', '.join(move_clauses)
    
    restore_sql = (
        f"ALTER DATABASE [{dbname}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; "
        f"RESTORE DATABASE [{dbname}] "
        f"FROM DISK = N'{backup_file}' "
        f"WITH {move_clause}, "
        f"REPLACE, STATS = 10; "
        f"ALTER DATABASE [{dbname}] SET MULTI_USER;"
    )
    
    print(f"Executing SQL:\n{restore_sql}")
    
    cmd = [
        "sqlcmd",
        "-S", sql_server,
        "-U", sql_username,
        "-P", sql_password,
        "-Q", restore_sql
    ]
    
    try:
        restore_result = subprocess.run(cmd, capture_output=True, text=True, check=True)

        if restore_result.returncode == 0:  # Check if restore was successful
            print("Database restore completed successfully.")
            print("Output:", restore_result.stdout)

            try:
                os.remove(backup_file)  # Delete backup file
                print(f"Backup file {backup_file} deleted successfully.")
            except FileNotFoundError:
                print(f"Backup file {backup_file} does not exist.")
            except Exception as e:
                print(f"Error deleting backup file: {e}")
        else:
            print("Database restore failed.")
            print("Error Output:", restore_result.stderr)
        
    except subprocess.CalledProcessError as e:
        print("Database restore failed.")
        print("Error Output:", e.stderr)
        return None
