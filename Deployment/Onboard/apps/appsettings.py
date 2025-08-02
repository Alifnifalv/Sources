from ..config import parse_args  # Assuming parse_args is in config.py

def mobile_appsettings(ports):
    """
    Generates a configuration string (like appsettings.json section) for mobile apps.

    Args:
        ports (dict): Dictionary of assigned ports.

    Returns:
        str: A string containing the mobile app configuration section.
    """
    args = parse_args() # Parse arguments to get client_ip
    
    config_string = f"""
    "pearl-test": {{
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
    return config_string