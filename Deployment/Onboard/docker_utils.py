# docker_utils.py
import subprocess
import os
from pathlib import Path



def pre_docker_compose_checks(client_name, docker_projects, dry_run=True):
    """

    Args:
        client_name (str): Client identifier, used to name containers.
        docker_projects (list of dict): List of Docker project configurations (names).
        dry_run (bool, optional): If True, only prints actions, doesn't actually stop/remove containers. Defaults to True.
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
