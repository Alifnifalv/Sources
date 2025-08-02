# config_updater.py

import re
import sys
from pathlib import Path
from .paths_assignment import get_available_clients


environment = "test"
clients = get_available_clients()
client = clients

def update_config_xml(config_xml_path, client, app_type, app_name):
    """
    Updates config.xml based on client selection.
    
    Parameters:
        config_xml_path (str): The path to config.xml.
        app_type (str): The app type (e.g., "parent", "staff", etc.).
        app_name (str): The human-readable app name.
    """
    try:
        with open(config_xml_path, 'r', encoding='utf-8') as f:
            config_content = f.read()
    except FileNotFoundError:
        print(f"Error: {config_xml_path} not found.", file=sys.stderr)
        sys.exit(1)
    
    # **Modify config.xml only for the selected client**
    # Modify app name and description
    app_name_formatted = f"{client.capitalize()} {app_type.capitalize()}"
    config_content = re.sub(r"<name>.*?</name>", f"<name>{app_name_formatted}</name>", config_content)
    config_content = re.sub(r"<description>.*?</description>", f"<description>{app_name_formatted} {app_name}</description>", config_content)
    config_content = re.sub(
        r'(<widget [^>]*version=")([^"]+)(")',
        rf'\g<1>1.0.0\g<3>',
        config_content
    )
    
    try:
        with open(config_xml_path, 'w', encoding='utf-8') as f:
            f.write(config_content)
    except Exception as e:
        print(f"Error writing to {config_xml_path}: {e}", file=sys.stderr)
        sys.exit(1)

def update_app_js(app_js_path, client, environment):
    """
    Updates app.js based on the client and environment selections.
    
    Parameters:
        app_js_path (str): The path to app.js.
    """
    try:
        with open(app_js_path, 'r') as f:
            content = f.read()
    except FileNotFoundError:
        print(f"Error: {app_js_path} not found.", file=sys.stderr)
        sys.exit(1)
    
    # Update client setting
    

    if client in clients:
        # Activate only the selected client
        content = re.sub(r'// var client = "' + re.escape(client) + r'";', r'var client = "' + client + r'";', content)

        # Deactivate all other clients
        for cli in clients:
            if cli != client:
                content = re.sub(r'var client = "' + re.escape(cli) + r'";', r'// var client = "' + cli + r'";', content)
    else:
        print(f"Error: Selected client '{client}' is not in the available clients list.", file=sys.stderr)
        sys.exit(1)

    # Update environment setting
    environments = ["live", "staging", "test", "linux", "local"]
    if environment in environments:
        for env in environments:
            content = re.sub(r'// var environment = "' + re.escape(env) + r'";', r'var environment = "' + env + r'";', content)
        for env in environments:
            if env != environment:
                content = re.sub(r'var environment = "' + re.escape(env) + r'";', r'// var environment = "' + env + r'";', content)
    
    try:
        with open(app_js_path, 'w') as f:
            f.write(content)
    except Exception as e:
        print(f"Error writing to {app_js_path}: {e}", file=sys.stderr)
        sys.exit(1)

def update_appsettings_js(appsettings_js_path, client, environment, args, ports):
    try:
        with open(appsettings_js_path, 'a') as f:
            snippet = f"""
"{client}-{environment}": {{
        "RootUrl": "http://{args.client_ip}:{ports['api_port']}/api/appdata",
        "SchoolServiceUrl": "http://{args.client_ip}:{ports['api_port']}/api/school",
        "SecurityServiceUrl": "http://{args.client_ip}:{ports['api_port']}/api/security",
        "UserServiceUrl": "http://{args.client_ip}:{ports['api_port']}/api/useraccount",
        "ContentServiceUrl": "http://{args.client_ip}:{ports['api_port']}/api/content",
        "SignalRhubURL": "http://{args.client_ip}:{ports['chathub_port']}/api/communication",
        "CommunicationServiceUrl": "http://{args.client_ip}:{ports['api_port']}/api/communication",
        "ErpUrl": "http://{args.client_ip}:{ports['erp_port']}", # Using client_ip and erp_port
        "ParentUrl": "http://{args.client_ip}:{ports['parent_port']}", # Using client_ip and parent_port
        "CurrentAppVersion": "1.0.0"
    }},
    """
            f.write(snippet)
        print(f"✅ Successfully added configuration snippet for '{client}-{environment}' to {appsettings_js_path}")

    except FileNotFoundError:
        print(f"Error: {appsettings_js_path} not found.", file=sys.stderr)
        sys.exit(1)
    except Exception as e:
        print(f"Error writing to {appsettings_js_path}: {e}", file=sys.stderr)
        sys.exit(1)

if __name__ == "__main__":
    print("config_updater module loaded. Use update_config_xml() and update_app_js() as needed.")
