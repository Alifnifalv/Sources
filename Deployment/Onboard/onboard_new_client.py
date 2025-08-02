import argparse
import getpass
import shutil
import socket
import subprocess
import pyodbc
import os
from pathlib import Path

def interactive_input(prompt, default=None):
    """Helper function to get interactive input with an optional default value."""
    prompt = f"{prompt} [{default}]: " if default else f"{prompt}: "
    inp = input(prompt)
    return inp.strip() or default

def is_port_available(host, port, timeout=1):
    """
    Check if a port is available on a REMOTE HOST (updated for remote checks).
    Returns True if available (connection refused), False if in use or error.
    """
    try:
        with socket.create_connection((host, port), timeout=timeout):
            return False  # Port is open/in use
    except (ConnectionRefusedError, socket.timeout):
        return True  # Port is closed/available
    except Exception as e:
        print(f"Error checking {host}:{port} - {str(e)}")
        return False

def is_range_fully_available(host, port_range):
    """
    Check if ALL ports in the range are available.
    Returns True if all ports are free, False if any port is in use.
    """
    return all(is_port_available(host, port) for port in port_range)

def assign_port(host, port_ranges, assigned_ports):
    for port_range in port_ranges:
        if is_range_fully_available(host, port_range):
            for port in port_range:
                if port not in assigned_ports:
                    assigned_ports.append(port)
                    return port
    return None

def get_dynamic_port_from_ranges(host, list_of_ranges, assigned_ports, range_width=10, max_port=65535):
    """
    Allocates ports efficiently by caching available ranges and utilizing all ports
    within a valid range before moving to the next range.

    Args:
        host: Target host to check ports on
        list_of_ranges: Initial list of preconfigured ranges to try
        assigned_ports: List of already assigned ports
        range_width: Width of each range to try
        max_port: Maximum allowable port number

    Returns:
        An available port number or raises ValueError if none found
    """
    # Static variables to maintain state across function calls
    if not hasattr(get_dynamic_port_from_ranges, 'current_range'):
        get_dynamic_port_from_ranges.current_range = None
        get_dynamic_port_from_ranges.available_ports = []

    def check_range_availability(range_to_check):
        """
        Checks if ALL ports in a given range are available.
        Returns list of available ports if range is valid, None otherwise.
        """
        print(f"Checking range {range_to_check.start}-{range_to_check.stop-1}")
        available = []

        for port in range_to_check:
            if not is_port_available(host, port):
                print(f"Port {port} is unavailable - skipping entire range")
                return None
            available.append(port)

        return available

    # If we have cached available ports, use them first
    if get_dynamic_port_from_ranges.available_ports:
        port = get_dynamic_port_from_ranges.available_ports[0]
        if port not in assigned_ports:
            assigned_ports.append(port)
            get_dynamic_port_from_ranges.available_ports.pop(0)
            print(f"Found available port {port} in dynamic range")
            return port

    # If no cached ports, search for a new valid range
    current_start = (max(assigned_ports) if assigned_ports else 900)
    current_start = (current_start // range_width) * range_width  # Align to range boundary

    while current_start <= max_port:
        current_end = min(current_start + range_width - 1, max_port)
        current_range = range(current_start, current_end + 1)

        available_ports = check_range_availability(current_range)
        if available_ports:
            # Cache the available ports for future use
            get_dynamic_port_from_ranges.available_ports = available_ports
            get_dynamic_port_from_ranges.current_range = current_range

            # Use the first available port
            port = get_dynamic_port_from_ranges.available_ports.pop(0)
            assigned_ports.append(port)
            print(f"Found available port {port} in dynamic range")
            return port

        # Move to the start of the next range
        current_start = current_end + 1

    raise ValueError(f"No fully available port ranges found up to {max_port}")

def parse_range(range_str):
    """
    Enhanced range parser that ensures ranges align with the desired width.
    """
    start, end = map(int, range_str.split("-"))
    if not (0 <= start <= 65535) or not (0 <= end <= 65535):
        raise ValueError("Ports must be between 0 and 65535")
    return range(start, end + 1)

def parse_args():
    parser = argparse.ArgumentParser(description='Interactive Client Onboarding Script')

    parser.add_argument('--client', help='Client identifier (e.g., "podarpearl")')
    parser.add_argument('--dbname', help='Target database name (e.g., "podarpearl_db")')
    parser.add_argument('--client-ip', help='Client host/IP (e.g., "192.168.29.100")')
    parser.add_argument('--sql-server', help='SQL Server URL or hostname')
    parser.add_argument('--sql-username', help='SQL Server username')
    parser.add_argument('--dry-run', help='Dry run mode (yes/no)', choices=['yes', 'no'])

    args = parser.parse_args()

    if not args.client:
        args.client = interactive_input("Enter client identifier")
    if not args.dbname:
        args.dbname = interactive_input("Enter target database name")
    if not args.client_ip:
        args.client_ip = interactive_input("Enter client IP/hostname") 
    if args.client_ip and not (args.client_ip.startswith("http://") or args.client_ip.startswith("https://")):
        args.client_ip = "http://" + args.client_ip
    if not args.sql_server:
        args.sql_server = interactive_input("Enter SQL Server URL/hostname")
    if not args.sql_username:
        args.sql_username = interactive_input("Enter SQL Server username")
        args.sql_password = getpass.getpass("Enter SQL Server password: ")
    if not args.dry_run:
        args.dry_run = interactive_input("Dry run? (yes/no)", default="yes").lower()

    return args

def restore_database(conn, args, backup_path="F:/DB_Backups/Eduegate_Blank.bak"):
    """Execute RESTORE DATABASE command with dynamic file relocation and connection termination."""
    cursor = conn.cursor()
    target_db = args.dbname

    try:
        # Terminate all active connections to the target database forcefully
        print(f"Terminating active connections to database '{target_db}'...")
        conn.autocommit = True # Set autocommit to True for KILL command
        cursor.execute(f"""
            USE master; -- Switch to master to kill connections to target_db
            DECLARE @SQL varchar(max) = '';
            SELECT @SQL += 'KILL ' + CAST(session_id AS VARCHAR(10)) + ';'
            FROM sys.dm_exec_sessions
            WHERE database_id = DB_ID('{target_db}') AND session_id <> @@SPID; -- Exclude current session
            EXEC(@SQL);

            IF DB_ID('{target_db}') IS NOT NULL
            BEGIN
                ALTER DATABASE [{target_db}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            END
        """)
        # conn.autocommit = False # Revert autocommit

        # Get file list from backup
        cursor.execute(f"RESTORE FILELISTONLY FROM DISK = N'{backup_path}'")
        files = cursor.fetchall()

        # Generate MOVE clauses to relocate files to a new directory
        move_clauses = []
        restore_data_dir = Path("F:\\DB_Restore", target_db).as_posix() # Define a new directory for restored files
        os.makedirs(restore_data_dir, exist_ok=True) # Ensure the directory exists

        for row in files:
            original_logical = row.LogicalName
            original_physical = row.PhysicalName
            file_type = "Data" if row.Type == 'D' else "Log" # 'D' for data, 'L' for log (character types)
            new_filename = f"{target_db}_{file_type}.{file_type.lower()[0]}df" # e.g., Eduegate_Blank_Data.mdf, Eduegate_Blank_Log.ldf
            new_physical = Path(restore_data_dir, new_filename).as_posix()
            move_clauses.append(f"MOVE '{original_logical}' TO N'{new_physical}'") # N prefix for Unicode paths

        move_clause = ', '.join(move_clauses)

        # Execute RESTORE command
        restore_command = f"""
            RESTORE DATABASE [{target_db}]
            FROM DISK = N'{backup_path}'
            WITH {move_clause},
            REPLACE, STATS = 10;
        """
        print(f"Executing restore command:\n{restore_command}") # Print the command for debugging
        cursor.execute(restore_command)
        while cursor.nextset():
            pass
        print("Restore completed successfully.")

    except pyodbc.Error as e:
        print(f"Restore failed: {e}")
        raise
    finally:
        if conn: # Corrected connection check - just check if conn is not None
            conn.rollback()

def backup_database(conn, backup_dir="F:/DB_Backups", backup_filename="Eduegate_Blank.bak"):
    """
    Takes a backup of the target database before restore.
    """
    cursor = conn.cursor()
    backup_db = "Eduegate_Blank"
    backup_path = Path(backup_dir, backup_filename).as_posix()

    # Ensure backup directory exists
    os.makedirs(backup_dir, exist_ok=True)

    try:
        print(f"Taking backup of database '{backup_db}' to '{backup_path}'...")
        # Explicitly set autocommit to True before backup
        conn.autocommit = True
        backup_command = f"""
            BACKUP DATABASE [{backup_db}]
            TO DISK = N'{backup_dir}'
            WITH FORMAT, INIT, STATS = 10
        """ # FORMAT and INIT will overwrite existing backup
        cursor.execute(backup_command)
        while cursor.nextset():
            pass
        print("Backup completed successfully.")
        conn.autocommit = False # Revert autocommit to default (False - if that's your general preference, otherwise you can omit this line)
    except pyodbc.Error as e:
        print(f"Backup failed: {e}")
        raise

def create_client_folders(client_id, script_dir, dry_run=True):
    """
    Creates client-specific folders by copying 'eduegate' template in predefined paths.

    Args:
        client_id (str): The client identifier, used as the folder name.
        script_dir (str): The directory where the script is located.
        dry_run (bool): If True, print folder creation actions without actually creating folders.
    """
    base_path = Path(Path(script_dir, r"../../Presentation").as_posix()).resolve() # Navigate up to 'Presentation'
    client_folder_name = client_id
    folder_paths = [
        Path(base_path, "Eduegate.ERP.AdminCore", "wwwroot", "Clients").as_posix(),
        Path(base_path, "Eduegate.ERP.School.PortalCore", "wwwroot", "Clients").as_posix(),
        Path(base_path, "Eduegate.OnlineExam.PortalCore", "wwwroot", "Clients").as_posix(),
        Path(base_path, "Eduegate.Signup.PortalCore", "wwwroot", "Clients").as_posix(),
        Path(base_path, "Eduegate.Vendor.PortalCore", "wwwroot", "Clients").as_posix(),
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
                actual_template_folder_path = Path(parent_folder, item_name).as_posix() # Use the actual name from listing
                template_folder_found = True
                break # Found it, no need to check other items in the directory

        destination_folder = Path(parent_folder, client_folder_name).as_posix()

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

def process_project(solution_path, project_path, publish_path, project_name):
    """
    Processes a .NET project: cleans, builds, publishes, and removes unnecessary files.
    """
    subprocess.run(["dotnet", "restore", project_path], check=True)
    # Clean the solution
    subprocess.run(["dotnet", "clean", project_path], check=True)
    
    # Remove contents of publish directory
    if os.path.exists(publish_path):
        shutil.rmtree(publish_path)
    os.makedirs(publish_path, exist_ok=True)
    
    # Rebuild the solution
    subprocess.run(["dotnet", "build", project_path, "--configuration", "Release", "--self-contained", "true", "--runtime", "linux-x64"], check=True)
    
    # Publish the project
    subprocess.run(["dotnet", "publish", project_path, "--configuration", "Release", "--self-contained", "true", "--runtime", "linux-x64", "--output", publish_path], check=True)
    

def build_docker_image(app_name, publish_output_dir, tag, dry_run=True):
    image_name = f"client-{tag}-{app_name}"
    dockerfile_path = Path(publish_output_dir, "Dockerfile").as_posix()
    build_command = ["docker", "build", "-t", image_name, "-f", dockerfile_path, publish_output_dir]
    print(f"Building Docker image: {' '.join(build_command)}")
    if not dry_run:
        subprocess.run(build_command, check=True)
    else:
        print("[Dry Run] Would execute:", ' '.join(build_command))
    return image_name

def run_docker_container(image_name, host_port, container_port, environment_vars=None, dry_run=True):
    run_command = ["docker", "run", "-d", "-p", f"{host_port}:{container_port}", "--name", f"{image_name}-{host_port}"]
    if environment_vars:
        for key, value in environment_vars.items():
            run_command.extend(["-e", f"{key}={value}"])
    run_command.append(image_name)
    print(f"Running Docker container: {' '.join(run_command)}")
    if not dry_run:
        subprocess.run(run_command, check=True)
    else:
        print("[Dry Run] Would execute:", ' '.join(run_command))

def pre_docker_compose_checks(client_name, docker_projects, dry_run=True):
    """
    Checks for and handles existing Docker containers before running docker-compose up.
    """
    client_tag = client_name.lower()

    for project in docker_projects:
        project_name = project["name"].lower() # Docker Compose service names are lowercase
        container_name = f"{project_name}-service-{client_tag}" # Consistent container naming

        # Check for existing container
        existing_containers_output = subprocess.run(
            ["docker", "ps", "-a", "-q", "--filter", f"name={container_name}"],
            capture_output=True, text=True
        ).stdout.strip()

        if existing_containers_output:
            print(f"Existing container found for service '{project_name}': {container_name}")
            if not dry_run:
                print(f"Stopping and removing existing container: {container_name}...")
                try:
                    subprocess.run(["docker", "stop", container_name], check=True, capture_output=True)
                    subprocess.run(["docker", "rm", container_name], check=True, capture_output=True)
                    print(f"Existing container '{container_name}' stopped and removed.")
                except subprocess.CalledProcessError as e:
                    print(f"Error stopping/removing container '{container_name}': {e}")
                    print("Please check Docker manually.")
            else:
                print(f"[Dry Run] Would stop and remove existing container: {container_name}")
        else:
            print(f"No existing container found for service '{project_name}'.")


def main():
    args = parse_args()
    backup_path="F:/DB_Backups/Eduegate_Blank.bak"
    backup_dir="F:/DB_Backups" # Define backup directory
    backup_filename="Eduegate_Blank.bak" # Define backup filename

    print("\nConfiguration Summary:")
    print(f"Client: {args.client}")
    print(f"Database Name: {args.dbname}")
    print(f"Client IP: {args.client_ip}")
    print(f"SQL Server: {args.sql_server}")
    print(f"SQL Username: {args.sql_username}")
    print(f"Dry Run: {args.dry_run}")

    dry_run = args.dry_run.lower() == 'yes'

    # Configure port ranges
    preconfigured_ranges = []
    try:
        preconfigured_ranges.append(parse_range("940-950"))
    except ValueError as e:
        print(f"Error configuring port ranges: {e}")
        return

    # Clean host for port checks
    host_for_port_check = args.client_ip.split("//")[-1].split(":")[0]

    assigned_ports = []

    try:
        # Assign ports for all required services
        ports = {
            'bid_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'meeting_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'parent_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'ai_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'api_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'erp_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'exam_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'chathub_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'lms_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports),
            'vendor_port': get_dynamic_port_from_ranges(host_for_port_check, preconfigured_ranges, assigned_ports)
        }
    except ValueError as e:
        print(f"Port assignment failed: {e}")
        return

    print("\nAssigned Ports:")
    for name, port in ports.items():
        print(f"{name.replace('_', ' ').title()}: {port}")

    # Build SQL updates using dictionary unpacking
    sql_updates = {
        'CLIENT_APPSTORELOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/App_Store_Badge.png",
        'CLIENT_BID_LOGIN_LINK': f"{args.client_ip}:{ports['bid_port']}/bid/bidlogin",
        'CLIENT_FBLOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/fb-logo.png",
        'CLIENT_FOOTERLOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/footer-logo.png",
        'CLIENT_HEADERIMAGE': f"{args.client_ip}:{ports['parent_port']}/Images/logo/email-header.png",
        'CLIENT_INSTALOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/insta-logo.png",
        'CLIENT_LINKED_CONTENT': f"{args.client_ip}:{ports['erp_port']}/Content/ReadContentsByID?contentID=",
        'CLIENT_LINKEDINLOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/linkedin-logo.png",
        'CLIENT_MAILLOGO': f"{args.client_ip}:{ports['parent_port']}/img/podarlogo_mails.png",
        'CLIENT_MEETING_PORTAL': f"{args.client_ip}:{ports['meeting_port']}",
        'CLIENT_PARENT_PORTAL': f"{args.client_ip}:{ports['parent_port']}",
        'CLIENT_PARENT_PORTAL_LOGIN_LINK': f"{args.client_ip}:{ports['parent_port']}/Account/Login",
        'CLIENT_PLAYSTORELOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/google-play-badge.png",
        'CLIENT_VENDOR_LOGIN_LINK': f"{args.client_ip}:{ports['bid_port']}/account/login",
        'CLIENT_WELCOME_MAIL_DOC_GUIDELINES': f"{args.client_ip}:{ports['parent_port']}/Documents/SchoolGeneralGuidelines.pdf",
        'CLIENT_WELCOME_MAIL_DOC_TRANSPORT': f"{args.client_ip}:{ports['parent_port']}/Documents/DocumentForOwnTransportStudentsOnly.pdf",
        'CLIENT_WHATSAPPLOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/whatsapp.png",
        'CLIENT_YOUTUBELOGO': f"{args.client_ip}:{ports['parent_port']}/Images/logo/youtube-logo.png",
        'CLIENTINSTANCE': f"{args.client}",
        'AI_SCORE_PREDICTION_URL': f"{args.client_ip}:{ports['ai_port']}",
        'ApiRootUrl': f"{args.client_ip}:{ports['api_port']}",
        'ChatHubRootUrl': f"{args.client_ip}:{ports['chathub_port']}",
        'ERPRootUrl': f"{args.client_ip}:{ports['erp_port']}/UploadImages/",
        'ExamRootUrl': f"{args.client_ip}:{ports['exam_port']}",
        'ImageHostUrl': f"{args.client_ip}:{ports['erp_port']}/UploadImages/",
        'LMSRootUrl': f"{args.client_ip}:{ports['lms_port']}",
        'MeetingRootUrl': f"{args.client_ip}:{ports['meeting_port']}",
        'ParentRootUrl': f"{args.client_ip}:{ports['parent_port']}",
        'VendorRootUrl': f"{args.client_ip}:{ports['vendor_port']}",
        'RootUrl': f"{args.client_ip}:{ports['parent_port']}",
        'RootUrlForMobile': f"{args.client_ip}:{ports['parent_port']}",
        'STUDENT_PROFILE_URL': f"{args.client_ip}:{ports['erp_port']}/UploadImages/StudentProfile/"
    }

    print("\nGenerated SQL Updates:")
    for setting_code, value in sql_updates.items():
        print(f"UPDATE setting.Settings SET SettingValue = '{value}' WHERE SettingCode = '{setting_code}';")
    
    # Get the directory where the script is located
    script_directory = os.path.dirname(Path(__file__).resolve())

    # Create client folders
    create_client_folders(args.client, script_directory, dry_run)

    

    if dry_run:
        print("\nDry run complete. No changes applied.")
        print(f"Would backup database {args.dbname} to {Path(backup_dir, backup_filename).as_posix()} (no actual backup).")
        print(f"Would restore database from {backup_path} to {args.dbname} (no actual restore).")
        # Optionally, print the generated SQL or restore command here
    else:
        print("\nApplying database changes...")
        conn_str = f"DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={args.sql_server};UID={args.sql_username};PWD={args.sql_password}"
        conn = pyodbc.connect(conn_str)

        # Backup database before restore
        backup_database(conn, backup_dir, backup_filename)
        conn.close()
        conn = pyodbc.connect(conn_str)
        restore_database(conn, args, backup_path)
        
        # Switch to the restored database
        cursor = conn.cursor()
        target_db = args.dbname
        print(f"Switching database context to '{target_db}'...")
        try:
            cursor.execute(f"USE [{target_db}]") # Explicitly use the target database
            conn.commit() # Commit the USE database command
            print(f"Successfully switched to database '{target_db}'.")
        except pyodbc.Error as use_db_error:
            print(f"Error switching to database '{target_db}': {use_db_error}")
            conn.close() # Close connection if switching fails
            return # Exit if database switching fails
    
    # Execute SQL UPDATE queries after restore
    cursor = conn.cursor()
    print("\nExecuting SQL Update Queries...")
    try:
        for setting_code, value in sql_updates.items():
            update_query = f"UPDATE setting.Settings SET SettingValue = ? WHERE SettingCode = ?" # Use parameterized query to avoid SQL injection
            cursor.execute(update_query, value, setting_code) # Pass value and setting_code as parameters
            print(f"Executed: UPDATE setting.Settings SET SettingValue = '{value}' WHERE SettingCode = '{setting_code}';")
        conn.commit() # Commit all updates
        print("SQL Updates applied successfully.")
    except pyodbc.Error as sql_e:
        print(f"Error executing SQL Updates: {sql_e}")
        conn.rollback() # Rollback if any update fails
    finally:
        conn.close() # Close connection in finally block to ensure it's always closed
    # Create client folders
    create_client_folders(args.client, script_directory, dry_run)
    if args.dry_run.lower() != 'yes':
        print("\nPhase: Publishing .NET Projects...")
        published_apps_base_dir = Path(Path(script_directory, "../../../", args.client).as_posix()).resolve() # Base dir for publish outputs, now client-specific

        # Define projects and their details for publishing
        projects = [
            ("../../../", "../../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj", Path(published_apps_base_dir, "CoreERPPublish").as_posix(), "CoreERPPublish"),
            ("../../../", "../../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj", Path(published_apps_base_dir, "CoreParentPortalPublish").as_posix(), "CoreParentPortalPublish"),
            ("../../../", "../../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj", Path(published_apps_base_dir, "CoreMobileApi_Publish").as_posix(), "CoreApiPublish"),
            ("../../../", "../../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj", Path(published_apps_base_dir, "CoreSignupPublish").as_posix(), "CoreSignupPublish"),
            ("../../../", "../../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj",  Path(published_apps_base_dir, "CoreVendorPublish").as_posix(), "CoreVendorPublish"),
            ("../../../", "../../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj", Path(published_apps_base_dir, "CoreExamPortalPublish").as_posix(), "CoreExamPublish"),
        ]

        for solution_path, project_path, publish_path, project_name in projects:
            print(f"Processing project: {project_name}...")
            if not os.path.exists(publish_path):
                os.makedirs(publish_path, exist_ok=True)
            process_project(solution_path, project_path, publish_path, project_name)
            print(f"Project {project_name} processing completed.")
        print("\nPhase: .NET Projects Publishing Complete.\n")
    else:
        print("\n[Dry Run] .NET Projects Publishing process would be executed (but skipped in dry run).")
    # execute_publish_script(script_directory, args.client, args.dry_run.lower() == 'yes')
    print("docker image building") # adding to check docker image building
    

    # Docker creation build and run moved to here from Dockerfile creation in non-dry run
    if args.dry_run.lower() != 'yes':
        # --- Dockerfile Creation Logic ---
        published_apps_base_dir = Path(Path(script_directory, "../../../", args.client).as_posix()).resolve() # Base dir for publish outputs, now client-specific

        # Define projects and their output directories and DLL names (adjust DLL names as needed)
        docker_projects = [
        {"name": "erp", "output_dir": Path(published_apps_base_dir, "CoreERPPublish").as_posix(), "dll_name": "Eduegate.ERP.AdminCore.dll"},  # Adjust DLL name
        {"name": "parent", "output_dir": Path(published_apps_base_dir, "CoreParentPortalPublish").as_posix(), "dll_name": "Eduegate.ERP.School.PortalCore.dll"},  # Adjust DLL name
        {"name": "api", "output_dir": Path(published_apps_base_dir, "CoreMobileApi_Publish").as_posix(), "dll_name": "Eduegate.Public.Api.dll"},  # Adjust DLL name
        {"name": "signup", "output_dir": Path(published_apps_base_dir, "CoreSignupPublish").as_posix(), "dll_name": "Eduegate.Signup.PortalCore.dll"},  # Adjust DLL name
        {"name": "vendor", "output_dir": Path(published_apps_base_dir, "CoreVendorPublish").as_posix(), "dll_name": "Eduegate.Vendor.PortalCore.dll"},  # Adjust DLL name
        {"name": "exam", "output_dir": Path(published_apps_base_dir, "CoreExamPortalPublish").as_posix(), "dll_name": "Eduegate.OnlineExam.PortalCore.dll"},  # Adjust DLL name
    ]

    # Define Dockerfile contents for each service based on provided files
    dockerfile_templates = {
        "api": """#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    COPY . /app
    USER root
    RUN chmod -R 777 /app
    ENTRYPOINT ["dotnet", "Eduegate.Public.Api.dll"]""",
        "erp": """#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    COPY . /app
    USER root
    RUN apt-get update && apt-get install -y wkhtmltopdf
    USER app
    ENTRYPOINT ["dotnet", "Eduegate.ERP.AdminCore.dll"]""",
        "parent": """#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    COPY . /app
    ENTRYPOINT ["dotnet", "Eduegate.ERP.School.PortalCore.dll"]""",
        "signup": """#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    COPY . /app
    ENTRYPOINT ["dotnet", "Eduegate.Signup.PortalCore.dll"]""",
        "vendor": """#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    COPY . /app
    ENTRYPOINT ["dotnet", "Eduegate.OnlineExam.PortalCore.dll"]""", # Note: Using "Eduegate.OnlineExam.PortalCore.dll" as per vendorDockerfile content, verify if correct
        "exam": """#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    COPY . /app
    ENTRYPOINT ["dotnet", "Eduegate.OnlineExam.PortalCore.dll"]""" # Default Dockerfile for exam, adjust if needed
    }


    print("\nPhase 1: Creating Dockerfiles...")
    for project in docker_projects:
        project_name = project["name"] # Get project name
        output_dir = project["output_dir"]
        dll_name = project["dll_name"]

        # Retrieve Dockerfile content from templates, use default if not found (unlikely in this case, but good practice)
        dockerfile_content = dockerfile_templates.get(project_name, f"""FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    RUN apt update && apt upgrade -y
    WORKDIR /app
    COPY . .
    ENTRYPOINT ["dotnet", "{dll_name}"]""") # Fallback to default if template is missing

        dockerfile_path = Path(output_dir, "Dockerfile").as_posix()
        if not dry_run:
            os.makedirs(output_dir, exist_ok=True)  # Ensure output dir exists - might already exist from publish script
            with open(dockerfile_path, "w") as f:
                f.write(dockerfile_content)
            print(f"Dockerfile created at: {dockerfile_path} for service: {project_name}") # Added service name to print
        else:
            print(f"[Dry Run] Dockerfile would be created at: {dockerfile_path} for service: {project_name}\nContent:\n{dockerfile_content}") # Added service name to dry run message
    print("\nPhase 1: Dockerfile creation complete.\n")


    # Phase 2: Generate Docker Compose file (after Dockerfiles are created)
    print("Phase 2: Generating Docker Compose file with 'build' context...")
    docker_compose_content = """version: "3.9"
services:
"""

    for project in docker_projects:
        project_name = project["name"].lower() # Service names in docker-compose should be lowercase and dash-separated
        service_port = ports.get(f"{project_name}_port") # Get assigned port
        output_dir = project["output_dir"] # Get output directory for build context

        if service_port:
            docker_compose_content += f"""
  {project_name}-service:
    build:
      context: {output_dir} # Use build context instead of image
    ports:
      - "{service_port}:80"
"""
        else:
            print(f"Warning: No port assigned for service '{project_name}'. Skipping port mapping in docker-compose.")
            docker_compose_content += f"""
  {project_name}-service:
    build:
      context: {output_dir} # Use build context instead of image
"""

    docker_compose_file_path = Path(script_directory, "../../../", args.client, "docker-compose.yml").as_posix() # Save docker-compose.yml in client folder
    if not dry_run:
        os.makedirs(os.path.dirname(docker_compose_file_path), exist_ok=True) # Ensure client folder exists
        with open(docker_compose_file_path, "w") as f:
            f.write(docker_compose_content)
        print(f"Docker Compose file created at: {docker_compose_file_path}")
    else:
        print(f"[Dry Run] Docker Compose file would be created at: {docker_compose_file_path}\nContent:\n{docker_compose_content}")
    print("\nPhase 3: Docker Compose file generation complete.\n")


    # --- Instructions for Docker Compose ---
    print("\nDocker Compose file generated.")

    
    if not dry_run: # Only automate in non-dry-run mode
        client_dir = Path(script_directory, "../../../", args.client).as_posix()
        print(f"\nPerforming pre-Docker Compose checks in directory: {client_dir}")
        pre_docker_compose_checks(args.client, docker_projects, dry_run) # Execute pre-checks

        print(f"\nAttempting to start services using Docker Compose from directory: {client_dir}")
        try:
            os.chdir(client_dir) # Change directory to client directory
            subprocess.run(["docker-compose", "up", "-d"], check=True) # Run docker-compose up -d
            print("Docker Compose up command executed successfully.")
        except FileNotFoundError:
            print("Error: docker-compose command not found. Make sure Docker Compose is installed and in your PATH.")
        except subprocess.CalledProcessError as e:
            print(f"Error executing docker-compose up -d: {e}")
        except Exception as e:
            print(f"An unexpected error occurred while trying to run docker-compose up: {e}")
        
       
    else:
        print("\n[Dry Run] Docker Compose up command would be executed (but skipped in dry run).")
        print("Instructions to start services manually are printed above.")

    print("\nDry run complete." if dry_run else "\nOnboarding process with Dockerization complete.")

if __name__ == '__main__':
    main()