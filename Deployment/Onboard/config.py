# config.py
import argparse
import getpass

def interactive_input(prompt, default=None):
    """
    Gets interactive input from the user with an optional default value.

    Imagine asking a user a question where they can type in an answer.
    This function does that, and if they just press 'Enter' without typing,
    it uses a pre-set answer if you've provided one.

    For example, if you ask 'What's your name? [User]: ' and the user types 'Alice',
    this function returns 'Alice'. If you ask 'Are you sure? [yes]: ' and the user
    just presses 'Enter', it returns 'yes'.

    Args:
        prompt (str): The question to ask the user.
        default (str, optional): The answer to use if the user doesn't type anything. Defaults to None.

    Returns:
        str: The user's input, or the default value if the user provided no input.
    """
    prompt = f"{prompt} [{default}]: " if default else f"{prompt}: "
    inp = input(prompt)
    return inp.strip() or default

def parse_args():
    """
    Parses command line arguments for the onboarding script.

    Think of this as setting up the script to understand instructions you give it
    when you run it.  Like telling a robot what to do before it starts working.

    It looks for special keywords (like '--client' or '--dry-run') when you run the script.
    If it finds them, it saves the values you provide next to those keywords.
    If you don't provide some keywords, it will ask you for the information interactively.

    Returns:
        argparse.Namespace: An object containing all the arguments provided by the user, either via command line or interactive input.
    """
    parser = argparse.ArgumentParser(description='Interactive Client Onboarding Script')

    parser.add_argument('--client', help='Client identifier (e.g., "softop")')
    parser.add_argument('--dbname', help='Target database name (e.g., "softop_test")')
    parser.add_argument('--client-ip', help='Client host/IP (e.g., "192.168.29.100")')
    parser.add_argument('--sql-server', help='SQL Server URL or hostname eg: localhost')
    parser.add_argument('--sql-username', help='SQL Server username eg: eduegateuser')
    parser.add_argument('--sql-password', help='SQL Server password eg: eduegate@123')
    parser.add_argument('--target_dir', help='target_dir of db-backup path eg: C:\DB_Backups')
    parser.add_argument('--dry-run', help='Dry run mode (yes/no)', choices=['yes', 'no'])

    args = parser.parse_args()

    # if not args.client:
    #     # args.client = interactive_input("Enter client identifier")
    #     args.client = "test"
    # if not args.dbname:
    #     # args.dbname = interactive_input("Enter target database name")
    #     args.dbname = "test"
    # if not args.client_ip:
    #     # args.client_ip = interactive_input("Enter client IP/hostname")
    #     args.client_ip = "192.168.29.100"
    # if args.client_ip and not (args.client_ip.startswith("http://") or args.client_ip.startswith("https://")):
    #     args.client_ip = "http://" + args.client_ip
    # if not args.sql_server:
    #     # args.sql_server = interactive_input("Enter SQL Server URL/hostname")
    #     args.sql_server = "localhost"
    # if not args.sql_username:
    #     # args.sql_username = interactive_input("Enter SQL Server username")
    #     args.sql_username = "eduegateuser"
    #     # args.sql_password = getpass.getpass("Enter SQL Server password: ")
    #     args.sql_password = "eduegate@123"
    # if not args.target_dir:
    #     # args.target_dir = interactive_input("Enter the target_dir path")
    #     args.target_dir = "F:\DB_Backups"
    # if not args.dry_run:
    #     # args.dry_run = interactive_input("Dry run? (yes/no)", default="yes").lower()
    #     args.dry_run = "no"

    return args
