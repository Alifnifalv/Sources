#!/bin/bash

# Define variables
CONTAINER_NAME="parent_portal"
IMAGE_NAME="parentdockerfile"
CONTEXT_PATH="/home/ubuntu/Azure_Pipeline/pipeline/Publish/ParentPortal/a"
DOCKERFILE_PATH="$CONTEXT_PATH/Dockerfile"
NETWORK_NAME="linux_docker"
HOST_PORT=8082
CONTAINER_PORT=8080
HOST_DOCUMENTS_PATH="/home/ubuntu/publish/ParentPortal/CoreParentPortalPublish/wwwroot/Documents"
CONTAINER_DOCUMENTS_PATH="/app/wwwroot/Documents"

# Stop and remove the existing container if it exists
if [ "$(docker ps -a -f name=$CONTAINER_NAME)" ]; then
    echo "Stopping and removing existing container: $CONTAINER_NAME"
    docker stop $CONTAINER_NAME
    docker rm -f $CONTAINER_NAME
fi

# Remove the existing Docker image if it exists
if [ "$(docker images -q $IMAGE_NAME)" ]; then
    echo "Removing existing image: $IMAGE_NAME"
    docker rmi $IMAGE_NAME
fi

#Remove the existing appsetting files from context path
if [ -n "$CONTEXT_PATH/Eduegate.ERP.AdminCore" ] && [ -d "$CONTEXT_PATH/Eduegate.ERP.AdminCore" ]; then
    rm "$CONTEXT_PATH/Eduegate.ERP.AdminCore"/appsetting*
else
    echo "Error: CONTEXT_PATH is undefined or not a directory."
fi

#Copy desired appsetting file and dockerfile to te published folder
cp $CURRENT_PATH/appsettings.json $CONTEXT_PATH/Eduegate.ERP.AdminCore
cp $CURRENT_PATH/Dockerfile $CONTEXT_PATH

# Build the Docker image
echo "Building Docker image: $IMAGE_NAME"
docker build -t $IMAGE_NAME -f $DOCKERFILE_PATH $CONTEXT_PATH

# Run the Docker container with volume mounting
echo "Running Docker container: $CONTAINER_NAME"
docker run -d \
  -p $HOST_PORT:$CONTAINER_PORT \
  --name $CONTAINER_NAME \
  --network $NETWORK_NAME \
  -v $HOST_DOCUMENTS_PATH:$CONTAINER_DOCUMENTS_PATH \
  $IMAGE_NAME

# Optional: Set the container to restart automatically
docker update --restart=always $CONTAINER_NAME

# List all containers
docker ps -a
