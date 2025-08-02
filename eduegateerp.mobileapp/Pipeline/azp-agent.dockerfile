# Use a lightweight base image
FROM ubuntu:20.04

# Set environment variables for Azure DevOps Agent
ENV AZP_URL=https://eduegate.visualstudio.com/
ENV AZP_TOKEN=D5rLFiGuqt03FdemHBqPkTVR2XtZEM9x8mpF3AYro4vzAZGptfONJQQJ99AKACAAAAAAAAAAAAASAZDO3gnO
ENV AZP_AGENT_NAME="Docker-Agent"
ENV AZP_POOL="Apple_Pool"

# Install dependencies and configure timezone inline
RUN ln -fs /usr/share/zoneinfo/Asia/Kolkata /etc/localtime && \
    apt-get update && \
    DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
    curl \
    net-tools \
    iputils-ping \
    ca-certificates \
	unzip \
	openjdk-8-jdk \
    tzdata && \
    dpkg-reconfigure -f noninteractive tzdata && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Create a user for running the agent (non-root)
RUN useradd -m azureuser

# Create working directory for the agent and set ownership
RUN mkdir -p /azp/myagent && chown azureuser:azureuser /azp/myagent
WORKDIR /azp/myagent

# Switch to the created user
USER azureuser


# Download and extract TEE-CLC (Team Explorer Everywhere)
RUN curl -L https://github.com/microsoft/team-explorer-everywhere/releases/download/14.139.0/TEE-CLC-14.139.0.zip -o TEE-CLC-14.139.0.zip && \
    unzip TEE-CLC-14.139.0.zip -d /azp/myagent/tee-clc && \
    rm TEE-CLC-14.139.0.zip
	
# Download and extract the Azure DevOps agent
RUN curl -L https://vstsagentpackage.azureedge.net/agent/4.248.0/vsts-agent-linux-x64-4.248.0.tar.gz -o vsts-agent-linux-x64-4.248.0.tar.gz && \
    tar zxvf vsts-agent-linux-x64-4.248.0.tar.gz && \
    rm vsts-agent-linux-x64-4.248.0.tar.gz

# Switch back to root user for running dependencies installation
USER root

# Install dependencies required for the agent
RUN ./bin/installdependencies.sh

RUN mkdir -p /azp/myagent/externals/tee
# Set path to TEE-CLC so that `tf` can be used system-wide
#RUN ln -s /azp/myagent/externals/tee /usr/local/bin/tf

RUN cp -r /azp/myagent/tee-clc/TEE-CLC-14.139.0/* /azp/myagent/externals/tee/
#RUN export PATH="/azp/myagent/externals/tee:$PATH"

# Switch to the created user
USER azureuser
ENV PATH="/azp/myagent/externals/tee:$PATH"
RUN ./config.sh --unattended \
    --url $AZP_URL \
    --auth PAT \
    --token $AZP_TOKEN \
    --pool $AZP_POOL \
    --agent $AZP_AGENT_NAME \
	--acceptTeeEula \
    --replace
RUN tf eula -accept
CMD ["./run.sh"]