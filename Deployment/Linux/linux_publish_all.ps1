# Get latest version of files
Write-Host "Getting latest changes from the repo..."
tf get $solutionPath
# Define a function to handle the project processing
function Process-Project {
    param (
		[string]$solutionPath,
        [string]$projectPath,
        [string]$publishPath,
        [string]$projectName,
        [string]$remotePath,
        [string]$sshCommand
    )
	
	# Get latest version of files
	#tf get $solutionPath
	
    # Clean the solution
    dotnet clean $projectPath

    # Remove contents of publish directory
    Remove-Item -Path "$publishPath/*" -Recurse -Force

    # Rebuild the solution
    dotnet build $projectPath --configuration Release --self-contained true --runtime linux-x64

    # Publish the project
    dotnet publish $projectPath --configuration Release --self-contained true --runtime linux-x64 --output $publishPath

    # Compress the Publish folder into a zip file using 7zip app
    $zipFileName = "${projectName}_$(Get-Date -Format 'dd-MM-yyyy').zip"
    & "C:/Program Files/7-Zip/7z.exe" a -tzip "$publishPath/$zipFileName" "$publishPath/*" -xr!appsetting*

    # Transfer the zip file to the remote machine using ssh and scp
    $privateKey = "./ubuntu"
    $remoteUser = "ubuntu"
    $remoteHost = "34.171.6.4"

    # Remove existing zip files on remote machine
    ssh -i $privateKey ${remoteUser}@${remoteHost} "rm -rf $remotePath/Core*.zip"

    # Copy the zip file to the remote machine
    scp -i $privateKey "$publishPath/$zipFileName" "${remoteUser}@${remoteHost}:$remotePath"

    # Execute SSH command on remote machine
    ssh -i $privateKey ${remoteUser}@${remoteHost} $sshCommand
}

# Process all projects sequentially

# Project 1: Eduegate.ERP.AdminCore
$projectPath = "../../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj"
$publishPath = "../../../../Linux-Publish/CoreERPPublish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreERPPublish"
$remotePath = "/home/ubuntu/publish/ERP"
$sshCommand = 'bash /home/ubuntu/publish/ERP/extract_erp_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand

# Project 2: Eduegate.ERP.School.PortalCore
$projectPath = "../../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj"
$publishPath = "../../../../Linux-Publish/CoreParentPortalPublish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreParentPortalPublish"
$remotePath = "/home/ubuntu/publish/ParentPortal"
$sshCommand = 'bash /home/ubuntu/publish/ParentPortal/extract_parent_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand

# Project 3: Eduegate.Public.Api
$projectPath = "../../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj"
$publishPath = "../../../../Linux-Publish/CoreMobileApi_Publish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreApiPublish"
$remotePath = "/home/ubuntu/publish/API"
$sshCommand = 'bash /home/ubuntu/publish/API/extract_api_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand

# Project 4: Eduegate.Public.Api
$projectPath = "../../Services/Apis/Eduegate.Hub/Eduegate.Hub/Eduegate.Hub.csproj"
$publishPath = "../../../../Linux-Publish/CoreChatHub_Publish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreChatHubPublish"
$remotePath = "/home/ubuntu/publish/ChatHub"
$sshCommand = 'bash /home/ubuntu/publish/ChatHub/extract_chat_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand

# Project 5: Eduegate.Signup.PortalCore
$projectPath = "../../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj"
$publishPath = "../../../../Linux-Publish/CoreSignupPublish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreSignupPublish"
$remotePath = "/home/ubuntu/publish/Signup"
$sshCommand = 'bash /home/ubuntu/publish/Signup/extract_signup_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand

# Project 6: Eduegate.Vendor.PortalCore
$projectPath = "../../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj"
$publishPath = "../../../../Linux-Publish/CoreVendorPublish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreVendorPublish"
$remotePath = "/home/ubuntu/publish/Vendor"
$sshCommand = 'bash /home/ubuntu/publish/Vendor/extract_vendor_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand

# Project 7: Eduegate.OnlineExam.PortalCore
$projectPath = "../../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj"
$publishPath = "../../../../Linux-Publish/CoreExamPortalPublish"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreExamPublish"
$remotePath = "/home/ubuntu/publish/Exam"
$sshCommand = 'bash /home/ubuntu/publish/Exam/extract_exam_publish.sh'
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
