# docker_operations.py
import os
from pathlib import Path
import subprocess
from .docker_utils import pre_docker_compose_checks

def handle_docker_operations(args, script_directory, ports):
    """
    Handles Dockerfile creation, Docker Compose file generation, and Docker Compose execution.

    Args:
        args: Parsed command-line arguments.
        script_directory: Directory where the main script is located.
        ports: Dictionary of assigned ports for services.
    """
    dry_run = args.dry_run.lower() == 'yes'
    published_apps_base_dir = Path(Path(script_directory, "../../../", args.client).as_posix()).resolve() # Base dir for publish outputs, now client-specific

    # Define projects and their output directories and DLL names (adjust DLL names as needed)
    docker_projects = [
        {"name": "erp", "output_dir": Path(published_apps_base_dir, "CoreERPPublish").resolve().as_posix(), "dll_name": "Eduegate.ERP.AdminCore.dll"},  # Adjust DLL name
        {"name": "parent", "output_dir": Path(published_apps_base_dir, "CoreParentPortalPublish").resolve().as_posix(), "dll_name": "Eduegate.ERP.School.PortalCore.dll"},  # Adjust DLL name
        {"name": "api", "output_dir": Path(published_apps_base_dir, "CoreMobileApi_Publish").resolve().as_posix(), "dll_name": "Eduegate.Public.Api.dll"},  # Adjust DLL name
        {"name": "signup", "output_dir": Path(published_apps_base_dir, "CoreSignupPublish").resolve().as_posix(), "dll_name": "Eduegate.Signup.PortalCore.dll"},  # Adjust DLL name
        {"name": "vendor", "output_dir": Path(published_apps_base_dir, "CoreVendorPublish").resolve().as_posix(), "dll_name": "Eduegate.Vendor.PortalCore.dll"},  # Adjust DLL name
        {"name": "exam", "output_dir": Path(published_apps_base_dir, "CoreExamPortalPublish").resolve().as_posix(), "dll_name": "Eduegate.OnlineExam.PortalCore.dll"},  # Adjust DLL name
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

        dockerfile_path = Path(output_dir, "Dockerfile").resolve().as_posix()
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

    docker_compose_file_path = Path(script_directory, "../../", args.client, "docker-compose.yml").resolve().as_posix() # Save docker-compose.yml in client folder
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
        client_dir = Path(script_directory, "../../../", args.client).resolve().as_posix()
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