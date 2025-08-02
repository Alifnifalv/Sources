#!/bin/bash

# Unified Deployment Script with Dynamic Configuration
# Usage: ./deploy.sh --service <name> [--server <ip>] [--main-catalog <name>] [--content-catalog <name>] [--log-catalog <name>]
# Available services: erp, parent, api, chat, signup, exam

# Base paths
AZURE_PIPELINE_PATH="/home/ubuntu/Azure_Pipeline"
PUBLISH_ARTIFACTS_ROOT="$AZURE_PIPELINE_PATH/sharedrepo"
DEPLOYMENT_ROOT="$AZURE_PIPELINE_PATH/sharedrepo/s/Deployment/Linux/Containers"

# Common variables
DOCKER_NETWORK="linux_docker"

# Database configuration defaults
SERVER="10.128.0.5"
MAIN_CATALOG="EduEgate_Test"
CONTENT_CATALOG="EduEgate_2022_Contents"
LOG_CATALOG="EduEgate_2022_Log"

# Service configuration function
set_service_vars() {
    case $1 in
        erp)
            SERVICE_NAME="ERP"
            CONTAINER_NAME="erp_portal"
            IMAGE_NAME="erpdockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.ERP.AdminCore"
            HOST_PORT=8081
            CONTAINER_PORT=8080
            ;;
        parent)
            SERVICE_NAME="ParentPortal"
            CONTAINER_NAME="parent_portal"
            IMAGE_NAME="parentdockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.ERP.School.PortalCore"
            HOST_PORT=8082
            CONTAINER_PORT=8080
            HOST_DOCUMENTS_PATH="/home/ubuntu/publish/ParentPortal/CoreParentPortalPublish/wwwroot/Documents"
            CONTAINER_DOCUMENTS_PATH="/app/wwwroot/Documents"
            ;;
        api)
            SERVICE_NAME="API"
            CONTAINER_NAME="mobile_api"
            IMAGE_NAME="apidockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.Public.Api"
            HOST_PORT=8083
            CONTAINER_PORT=8080
            ;;
        chat)
            SERVICE_NAME="ChatHub"
            CONTAINER_NAME="mobile_chat"
            IMAGE_NAME="chatdockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.Hub"
            HOST_PORT=8087
            CONTAINER_PORT=8080
            ;;
        signup)
            SERVICE_NAME="Signup"
            CONTAINER_NAME="signup_portal"
            IMAGE_NAME="signupdockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.Signup.PortalCore"
            HOST_PORT=8084
            CONTAINER_PORT=8080
            ;;
        exam)
            SERVICE_NAME="Exam"
            CONTAINER_NAME="exam_portal"
            IMAGE_NAME="examdockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.OnlineExam.PortalCore"
            HOST_PORT=8086
            CONTAINER_PORT=8080
            ;;
		vendor)
            SERVICE_NAME="Vendor"
            CONTAINER_NAME="vendor_portal"
            IMAGE_NAME="vendordockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.Vendor.PortalCore"
            HOST_PORT=8085
            CONTAINER_PORT=8080
            ;;
		recruitment)
            SERVICE_NAME="Recruitment"
            CONTAINER_NAME="recruitment_portal"
            IMAGE_NAME="recruitmentdockerfile"
            CONTEXT_PATH="$PUBLISH_ARTIFACTS_ROOT/Eduegate.Recruitment.Portal"
            HOST_PORT=8088
            CONTAINER_PORT=8080
            ;;
        *)
            echo "Invalid service: $1"
            echo "Available services: erp, parent, api, chat, signup, exam, vendor, recruitment"
            exit 1
            ;;
    esac
    
    DOCKERFILE_SOURCE="$DEPLOYMENT_ROOT/${SERVICE}Dockerfile"
    DOCKERFILE_TARGET="$CONTEXT_PATH/${SERVICE}Dockerfile"
    APPSETTINGS_TARGET_DIR="$CONTEXT_PATH"
}

# Handle arguments
while [[ $# -gt 0 ]]; do
    case "$1" in
        --service)
            SERVICE="$2"
            shift 2
            ;;
        --server)
            SERVER="$2"
            shift 2
            ;;
        --main-catalog)
            MAIN_CATALOG="$2"
            shift 2
            ;;
        --content-catalog)
            CONTENT_CATALOG="$2"
            shift 2
            ;;
        --log-catalog)
            LOG_CATALOG="$2"
            shift 2
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

# Validate service argument
if [[ -z "$SERVICE" ]]; then
    echo "Error: You must specify a service using --service"
    exit 1
fi

# Set service-specific variables
set_service_vars "$SERVICE"

# Deployment workflow
echo "=== Deploying $SERVICE_NAME Service ==="
echo "Database Server: $SERVER"
echo "Main Catalog: $MAIN_CATALOG"
echo "Content Catalog: $CONTENT_CATALOG"
echo "Log Catalog: $LOG_CATALOG"

# Check/Create Docker network
echo "Checking Docker network: $DOCKER_NETWORK"
if ! docker network inspect "$DOCKER_NETWORK" >/dev/null 2>&1; then
    echo "Creating missing Docker network: $DOCKER_NETWORK"
    docker network create "$DOCKER_NETWORK"
else
    echo "Network already exists: $DOCKER_NETWORK"
fi

# Cleanup existing container
if docker ps -a --format '{{.Names}}' | grep -q "^$CONTAINER_NAME$"; then
    echo "Removing existing container: $CONTAINER_NAME"
    docker stop "$CONTAINER_NAME" >/dev/null
    docker rm -f "$CONTAINER_NAME" >/dev/null
fi

# Remove existing image
if docker images --format '{{.Repository}}' | grep -q "^$IMAGE_NAME$"; then
    echo "Removing existing image: $IMAGE_NAME"
    docker rmi -f "$IMAGE_NAME" >/dev/null
fi

# Process configuration files
echo "Processing configuration files..."
if [ -d "$APPSETTINGS_TARGET_DIR" ]; then
    # Clean existing appsettings
    rm -f "$APPSETTINGS_TARGET_DIR"/appsettings.*
    
    # Process and copy appsettings.json with replacements
    sed -e "s/{SERVER}/$SERVER/g" \
        -e "s/{MAIN_CATALOG}/$MAIN_CATALOG/g" \
        -e "s/{CONTENT_CATALOG}/$CONTENT_CATALOG/g" \
        -e "s/{LOG_CATALOG}/$LOG_CATALOG/g" \
        "$DEPLOYMENT_ROOT/appsettings.json" > "$APPSETTINGS_TARGET_DIR/appsettings.json"

    # Copy Dockerfile from deployment root to context
    echo "Copying Dockerfile: $DOCKERFILE_SOURCE to $DOCKERFILE_TARGET"
    cp "$DOCKERFILE_SOURCE" "$DOCKERFILE_TARGET"
else
    echo "Error: Target directory $APPSETTINGS_TARGET_DIR not found!"
    exit 1
fi

# Create documents directory for parent service
if [[ "$SERVICE" == "parent" ]]; then
    echo "Creating documents directory..."
    mkdir -p "$HOST_DOCUMENTS_PATH"
fi

# Build Docker image
echo "Building Docker image: $IMAGE_NAME"
docker build -t "$IMAGE_NAME" -f "$DOCKERFILE_TARGET" "$CONTEXT_PATH"

# Run container command
RUN_CMD=(docker run -d \
    -p "$HOST_PORT:$CONTAINER_PORT" \
    --name "$CONTAINER_NAME" \
    --network "$DOCKER_NETWORK")

# Add volume mount for parent service
if [[ "$SERVICE" == "parent" ]]; then
    RUN_CMD+=(-v "$HOST_DOCUMENTS_PATH:$CONTAINER_DOCUMENTS_PATH")
fi

RUN_CMD+=("$IMAGE_NAME")

echo "Starting container: $CONTAINER_NAME"
"${RUN_CMD[@]}"

# Set restart policy
echo "Configuring automatic restart..."
docker update --restart=always "$CONTAINER_NAME"

# Verify deployment
echo "Deployment completed. Container status:"
docker ps -a --filter "name=$CONTAINER_NAME"