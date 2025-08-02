# project.py
import subprocess
import os
import shutil
from pathlib import Path

def process_project(solution_path, project_path, publish_path, project_name):
    """

    Args:
        solution_path (str): Path to the root solution directory (not directly used in this function, but might be relevant for context).
        project_path (str): Path to the .csproj project file.
        publish_path (str): Directory where the published output should be placed.
        project_name (str): Name of the project (for logging and identification).
    
    """
    project_path = Path(project_path).resolve()
    publish_path = Path(publish_path).resolve()

    # Validate paths
    if not project_path.exists():
        raise FileNotFoundError(f"Project file not found: {project_path}")
    if not project_path.is_file():
        raise ValueError(f"Project path must be a .csproj file: {project_path}")

    # Ensure publish directory exists before cleanup
    os.makedirs(publish_path, exist_ok=True)

    try:
        # Clean restore
        subprocess.run(["dotnet", "restore", project_path], check=True)
        
        # Clean solution
        subprocess.run(["dotnet", "clean", project_path], check=True)

        # Clear publish directory safely
        if os.path.exists(publish_path):
            shutil.rmtree(publish_path)
        os.makedirs(publish_path, exist_ok=True)

        # Rebuild and publish
        build_args = [
            "dotnet", "build", project_path,
            "-p:TargetFramework=net9.0",
            "--configuration", "Release",
            "--runtime", "win-x64"
        ]
        subprocess.run(build_args, check=True)

        publish_args = [
            "dotnet", "publish", project_path,
            "-p:TargetFramework=net9.0",
            "--configuration", "Release",
            "--runtime", "win-x64",
            "--output", str(publish_path)
        ]
        subprocess.run(publish_args, check=True)
        
        print(f"  Project '{project_name}' processing completed.")
    except subprocess.CalledProcessError as e:
        raise RuntimeError(f"Failed to process project {project_name}") from e
    
def publish_projects(script_directory, args, dry_run):
    if not dry_run:
        print("\nPhase: Publishing .NET Projects...")
        published_apps_base_dir = Path(Path(script_directory, "../../../", args.client).as_posix()).resolve() # Base dir for publish outputs, now client-specific

        # Define projects and their details for publishing
        projects = [
            ("../../", "../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj", Path(published_apps_base_dir, "CoreERPPublish").resolve().as_posix(), "CoreERPPublish"),
            ("../../", "../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj", Path(published_apps_base_dir, "CoreParentPortalPublish").resolve().as_posix(), "CoreParentPortalPublish"),
            ("../../", "../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj", Path(published_apps_base_dir, "CoreMobileApi_Publish").resolve().as_posix(), "CoreApiPublish"),
            ("../../", "../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj", Path(published_apps_base_dir, "CoreSignupPublish").resolve().as_posix(), "CoreSignupPublish"),
            ("../../", "../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj",  Path(published_apps_base_dir, "CoreVendorPublish").resolve().as_posix(), "CoreVendorPublish"),
            ("../../", "../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj", Path(published_apps_base_dir, "CoreExamPortalPublish").resolve().as_posix(), "CoreExamPublish"),
        ]

        for solution_path, project_path, publish_path, project_name in projects:
            process_project(solution_path, project_path, publish_path, project_name) # Call process_project for each project
        print("\nPhase: .NET Projects Publishing Complete.\n")
    else:
        print("\n[Dry Run] .NET Projects Publishing process would be executed (but skipped in dry run).")