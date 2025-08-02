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
    Remove-Item -Path "$publishPath/appsetting*", "$publishPath/web.config" -Recurse -Force
    Write-Host "appsetting* and web config files have been removed from $publishPath"
	
	# Compress the Publish folder into a zip file using 7zip app
    $zipFileName = "${projectName}_$(Get-Date -Format 'dd-MM-yyyy').zip"
    & "C:/Program Files/7-Zip/7z.exe" a -tzip "$publishPath/$zipFileName" "$publishPath/*"
	
	$privateKey = "~/.ssh/staging"
	$remoteUser = "Administrator"
	$remoteHost = "192.168.29.100"

    # Prompt the user before SCP command
    #Read-Host -Prompt "Press Enter to continue with the remote machine and enter your password when prompted"

    # SCP copy to remote server
    $scpCommand = "scp -i $privateKey -r $publishPath/$zipFileName ${remoteUser}@${remoteHost}:$remotePath"
    Write-Host "Executing SCP command: $scpCommand"
    Invoke-Expression $scpCommand
}

# Display menu
Write-Host "Select the project to process:"
Write-Host "1. Eduegate.ERP.AdminCore"
Write-Host "2. Eduegate.ERP.School.PortalCore"
Write-Host "3. Eduegate.Public.Api"
Write-Host "4. Eduegate.Signup.PortalCore"
Write-Host "5. Eduegate.Vendor.PortalCore"
Write-Host "6. Eduegate.OnlineExam.PortalCore"
Write-Host "7. Eduegate.Hub"
Write-Host "8. Eduegate.LMS.Portal"

$selection = Read-Host "Enter the number of your choice"

# Handle user selection
switch ($selection) {
    1 {
		$solutionPath = "../../../"
        $projectPath = "../../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj"
        $publishPath = "../../../../CoreERPPublish"
		$remotePath = "C:/Softop_Core/Pearl/ERP"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreERPPublish"
    }
    2 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj"
        $publishPath = "../../../../CoreParentPortalPublish"
		$remotePath = "C:/Softop_Core/Pearl/Parent_Portal"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreParentPortalPublish"
    }
    3 {
        $solutionPath = "../../../"
		$projectPath = "../../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj"
        $publishPath = "../../../../CoreMobileApi_Publish"
		$remotePath = "C:/Softop_Core/Pearl/MobileAppService"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreApiPublish"
    }
	4 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj"
        $publishPath = "../../../../CoreSignupPublish"
		$remotePath = "C:/Softop_Core/Pearl/MeetingPortal"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreSignupPublish"
    }
	5 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj"
        $publishPath = "../../../../CoreVendorPublish"
		$remotePath = "C:/Softop_Core/Pearl/VendorPortal"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreVendorPublish"
    }
	6 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj"
        $publishPath = "../../../../CoreExamPortalPublish"
		$remotePath = "C:/Softop_Core/Pearl/Exam_Portal"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreExamPublish"
    }
	7 {
        $solutionPath = "../../../"
		$projectPath = "../../Services/Apis/Eduegate.Hub/Eduegate.Hub/Eduegate.Hub.csproj"
        $publishPath = "../../../../CoreHub"
		$remotePath = "C:/Softop_Core/Pearl/ChatHub"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "CoreHub"
    }
	8 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Esuegate.LMS.Portal/Eduegate.LMS.Portal.csproj"
        $publishPath = "../../../../LMS"
		$remotePath = "C:/Softop_Core/Pearl/LMS"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName "LMS"
    }
    default {
        Write-Host "Invalid selection. Please enter 1, 2, 3, 4, 5, 6, 7 or 8."
    }
}