#!/bin/bash

# Define variables
CONTAINER_NAME="exam_portal"
IMAGE_NAME="examdockerfile"
DOCKERFILE_PATH="/home/ubuntu/publish/Exam/Dockerfile"
CONTEXT_PATH="/home/ubuntu/publish/Exam"
NETWORK_NAME="linux_docker"
HOST_PORT=8086
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

# Build the Docker image
echo "Building Docker image: $IMAGE_NAME"
docker build -t $IMAGE_NAME -f $DOCKERFILE_PATH $CONTEXT_PATH

# Run the Docker container
echo "Running Docker container: $CONTAINER_NAME"
docker run -d -p $HOST_PORT:$CONTAINER_PORT --name $CONTAINER_NAME --network $NETWORK_NAME $IMAGE_NAME
docker update --restart=always $CONTAINER_NAME
docker ps -a
