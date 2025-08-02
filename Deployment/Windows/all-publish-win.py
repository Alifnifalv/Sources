import os
import shutil
import subprocess
from datetime import datetime
import argparse

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
    
    # Remove appsettings and web.config files
    # for filename in os.listdir(publish_path):
    #     if filename.startswith("appsetting") or filename == "web.config":
    #         os.remove(os.path.join(publish_path, filename))
    # print(f"Removed appsetting* and web.config files from {publish_path}")
    
def main(): # <--- Encapsulate main logic in main() function (Good practice - but MISSING argparse logic)
    parser = argparse.ArgumentParser(description='Publish .NET Projects with Client Name') # <--- Add argument parser
    parser.add_argument('--client-name', required=True, help='Client identifier to use in publish paths') # <--- Define --client-name argument
    args = parser.parse_args() # <--- Parse arguments
    client_name = args.client_name # <--- Extract client_name from arguments

    # List of projects to process - use f-strings to format publish_path
    projects = [
        ("../../../", "../../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj", f"../../../{client_name}/CoreERPPublish", "CoreERPPublish"), # <--- Use f-string with client_name
        ("../../../", "../../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj", f"../../../{client_name}/CoreParentPortalPublish", "CoreParentPortalPublish"), # <--- Use f-string with client_name
        ("../../../", "../../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj", f"../../../{client_name}/CoreMobileApi_Publish", "CoreApiPublish"), # <--- Use f-string with client_name
        ("../../../", "../../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj", f"../../../{client_name}/CoreSignupPublish", "CoreSignupPublish"), # <--- Use f-string with client_name
        ("../../../", "../../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj", f"../../../{client_name}/CoreVendorPublish", "CoreVendorPublish"), # <--- Use f-string with client_name
        ("../../../", "../../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj", f"../../../{client_name}/CoreExamPortalPublish", "CoreExamPublish"), # <--- Use f-string with client_name
    ]

    # Process all projects
    for solution_path, project_path, publish_path, project_name in projects:
        if not os.path.exists(publish_path):
            os.makedirs(publish_path, exist_ok=True)
        process_project(solution_path, project_path, publish_path, project_name)

if __name__ == "__main__": # <--- Ensure main() is called when script is run
    main()