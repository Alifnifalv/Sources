#!/bin/bash

# Define variables
CURRENT_PATH="/home/ubuntu/Azure_Pipeline/pipeline/1/s/Deployment/Linux/Containers/ERP"
CONTAINER_NAME="erp_portal"
IMAGE_NAME="erpdockerfile"
CONTEXT_PATH="/home/ubuntu/Azure_Pipeline/pipeline/PublishArtifacts/ERP"
DOCKERFILE_PATH="$CONTEXT_PATH/Dockerfile"
NETWORK_NAME="linux_docker"
HOST_PORT=8081
CONTAINER_PORT=8080

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
if [ -n "$CONTEXT_PATH/ERP_PublishArtifact" ] && [ -d "$CONTEXT_PATH/ERP_PublishArtifact" ]; then
    rm "$CONTEXT_PATH/ERP_PublishArtifact"/appsetting*
else
    echo "Error: CONTEXT_PATH is undefined or not a directory."
fi

#Copy desired appsetting file and dockerfile to te published folder
cp $CURRENT_PATH/appsettings.json $CONTEXT_PATH/ERP_PublishArtifact
cp $CURRENT_PATH/Dockerfile $CONTEXT_PATH

# Build the Docker image
echo "Building Docker image: $IMAGE_NAME"
docker build -t $IMAGE_NAME -f $DOCKERFILE_PATH $CONTEXT_PATH

# Run the Docker container
echo "Running Docker container: $CONTAINER_NAME"
docker run -d -p $HOST_PORT:$CONTAINER_PORT --name $CONTAINER_NAME --network $NETWORK_NAME $IMAGE_NAME
docker update --restart=always $CONTAINER_NAME
docker ps -a
