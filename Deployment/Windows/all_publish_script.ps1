# Define a function to handle the project processing
function Process-Project {
    param (
		[string]$solutionPath,
        [string]$projectPath,
        [string]$publishPath,
        [string]$projectName
    )
	
	# Get latest version of files
	# tf get $solutionPath
	
    # Clean the solution
    dotnet clean $projectPath

    # Remove contents of publish directory
    Remove-Item -Path "$publishPath/*" -Recurse -Force

    # Rebuild the solution
    dotnet build $projectPath --configuration Release --framework net9.0 --runtime win-x64

    # Publish the project
    dotnet publish $projectPath --configuration Release --framework net9.0 --runtime win-x64 --output $publishPath

	# Remove appsetting* and web config files
    # Remove-Item -Path "$publishPath/appsetting*", "$publishPath/web.config" -Recurse -Force
    # Write-Host "appsetting* and web config files have been removed from $publishPath"
	
	# Compress the Publish folder into a zip file using 7zip app
    # $zipFileName = "${projectName}_$(Get-Date -Format 'dd-MM-yyyy').zip"
    # & "C:/Program Files/7-Zip/7z.exe" a -tzip "$publishPath/$zipFileName" "$publishPath/*"
	
	# $privateKey = "~/.ssh/staging"
	# $remoteUser = "Administrator"
	# $remoteHost = "192.168.29.100"

    # Prompt the user before SCP command
    # Read-Host -Prompt "Press Enter to continue with the remote machine and enter your password when prompted"

    # SCP copy to remote server
    # $scpCommand = "scp -i $privateKey -r $publishPath/$zipFileName ${remoteUser}@${remoteHost}:$remotePath"
    # Write-Host "Executing SCP command: $scpCommand"
    # Invoke-Expression $scpCommand
}

# Process all projects sequentially

# Project 1: Eduegate.ERP.AdminCore
$solutionPath = "../../../"
$projectPath = "../../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj"
$publishPath = "../../../../CoreERPPublish"
$remotePath = "C:/Softop_Core/Pearl/ERP"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreERPPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName

# Project 2: Eduegate.ERP.School.PortalCore
$solutionPath = "../../../"
$projectPath = "../../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj"
$publishPath = "../../../../CoreParentPortalPublish"
$remotePath = "C:/Softop_Core/Pearl/Parent_Portal"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreParentPortalPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName

# Project 3: Eduegate.Public.Api
$solutionPath = "../../../"
$projectPath = "../../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj"
$publishPath = "../../../../CoreMobileApi_Publish"
$remotePath = "C:/Softop_Core/Pearl/MobileAppService"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreApiPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName

# Project 4: Eduegate.Signup.PortalCore
$solutionPath = "../../../"
$projectPath = "../../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj"
$publishPath = "../../../../CoreSignupPublish"
$remotePath = "C:/Softop_Core/Pearl/MeetingPortal"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreSignupPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName

# Project 5: Eduegate.Vendor.PortalCore
$solutionPath = "../../../"
$projectPath = "../../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj"
$publishPath = "../../../../CoreVendorPublish"
$remotePath = "C:/Softop_Core/Pearl/VendorPortal"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreVendorPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName

# Project 6: Eduegate.OnlineExam.PortalCore
$solutionPath = "../../../"
$projectPath = "../../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj"
$publishPath = "../../../../CoreExamPortalPublish"
$remotePath = "C:/Softop_Core/Pearl/Exam_Portal"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreExamPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName

# Project 7: Eduegate.OnlineExam.PortalCore
$solutionPath = "../../../"
$projectPath = "../../Presentation/Esuegate.LMS.Portal/Eduegate.LMS.Portal.csproj"
$publishPath = "../../../../CoreLMSPortalPublish"
$remotePath = "C:/Softop_Core/Pearl/LMS_Portal"
if (-not (Test-Path -Path $publishPath)) {
    New-Item -Path $publishPath -ItemType Directory -Force
}
$projectName = "CoreLMSPublish"
Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName
