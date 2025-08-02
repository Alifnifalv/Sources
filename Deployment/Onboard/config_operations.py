# config_operations.py
from .ports import parse_range, get_dynamic_port_from_ranges

def configure_ports_and_sql_updates(args):
    """
    Configures port ranges, assigns dynamic ports, and generates SQL update statements.

    Args:
        args: Parsed command-line arguments.

    Returns:
        tuple: A tuple containing:
            - ports (dict): Dictionary of assigned ports.
            - sql_updates (dict): Dictionary of SQL update statements.
    """
    # Configure port ranges
    preconfigured_ranges = []
    try:
        preconfigured_ranges.append(parse_range("940-950"))
    except ValueError as e:
        print(f"Error configuring port ranges: {e}")
        return None, None  # Indicate failure

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
        return None, None # Indicate failure

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

    return ports, sql_updates