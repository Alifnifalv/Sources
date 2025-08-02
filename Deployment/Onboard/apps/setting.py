import json
import os
import shutil
from config import parse_args
from config_operations import configure_ports_and_sql_updates

args = parse_args()
ports = configure_ports_and_sql_updates(args)
def update_appsettings(appsettings_path, client, client_ip, ports, behavior='update'):
    """
    Appends or updates client configuration in appsettings.js.

    Args:
        appsettings_path (str): Path to the appsettings.js file.
        client (str): Client identifier (e.g., args.client).
        client_ip (str): Client IP address (e.g., args.client_ip).
        ports (dict): Dictionary of ports (e.g., ports).
        behavior (str, optional):  Behavior when client config exists.
            'update': Update existing config (default).
            'skip': Skip appending if config exists and log a warning.
            'error': Error out if config exists.
    """
    try:
        # Backup the appsettings.js file
        backup_path = appsettings_path + ".backup"
        shutil.copyfile(appsettings_path, backup_path)
        print(f"Backup created at: {backup_path}")

        # Read and parse appsettings.js
        with open(appsettings_path, 'r') as f:
            appsettings_content = f.read()

        # Attempt to parse as JSON. If it's a Javascript Object Literal, ensure it's valid JSON-like
        try:
            appsettings_json = json.loads(appsettings_content)
        except json.JSONDecodeError as e:
            raise Exception(f"Error parsing appsettings.js as JSON: {e}. Please ensure it's a valid JSON or JSON-like object literal.") from e

        # Construct the new client configuration
        new_client_config = {
            {client}: {
                "RootUrl": f"http://{client_ip}:{ports['api_port']}/api/appdata",
                "SchoolServiceUrl": f"http://{client_ip}:{ports['api_port']}/api/school",
                "SecurityServiceUrl": f"http://{client_ip}:{ports['api_port']}/api/security",
                "UserServiceUrl": f"http://{client_ip}:{ports['api_port']}/api/useraccount",
                "ContentServiceUrl": f"http://{client_ip}:{ports['api_port']}/api/content",
                "SignalRhubURL": f"http://{client_ip}:{ports['chathub_port']}/api/communication",
                "CommunicationServiceUrl": f"http://{client_ip}:{ports['api_port']}/api/communication",
                "ErpUrl": f"http://{client_ip}:{ports['erp_port']}/",
                "ParentUrl": f"http://{client_ip}:{ports['parent_port']}/",
                "CurrentAppVersion": "1.5.7"
            }
        }

        client_key_to_add = client  # Using args.client as the key

        if client_key_to_add in appsettings_json:
            if behavior == 'update':
                appsettings_json[client_key_to_add] = new_client_config[client_key_to_add]
                print(f"Configuration updated for client: {client_key_to_add}")
            elif behavior == 'skip':
                print(f"Warning: Configuration for client '{client_key_to_add}' already exists. Skipping update.")
                return  # Exit function without modifying file
            elif behavior == 'error':
                raise ValueError(f"Error: Configuration for client '{client_key_to_add}' already exists.")
        else:
            # Append the new client configuration
            appsettings_json.update(new_client_config)
            print(f"Configuration appended for client: {client_key_to_add}")


        # Write the updated JSON back to appsettings.js
        with open(appsettings_path, 'w') as f:
            json.dump(appsettings_json, f, indent=4) # Use indent for pretty formatting

        print(f"appsettings.js updated successfully.")

    except FileNotFoundError:
        raise FileNotFoundError(f"Error: appsettings.js file not found at: {appsettings_path}")
    except json.JSONDecodeError as e:
        raise Exception(f"Error decoding JSON in appsettings.js: {e}")
    except ValueError as e:
        raise ValueError(e) # Re-raise ValueError for behavior='error' case
    except Exception as e:
        raise Exception(f"An unexpected error occurred: {e}")

