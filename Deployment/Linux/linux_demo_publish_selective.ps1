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
	tf get $solutionPath

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
	
	# Prompt user for input before proceeding
    #Read-Host -Prompt "Press Enter to continue with transferring the zip file to the remote machine"

    # Transfer the zip file to the remote machine using ssh and scp
    $privateKey = "./linux_connect"
    $remoteUser = "sudishnch"
    $remoteHost = "34.171.6.4"
	# Prompt user for input before proceeding
    #Read-Host -Prompt "Press Enter to continue with the remote machine"
	# Append cleanup command to sshCommand
    ssh -i $privateKey ${remoteUser}@${remoteHost} "rm -rf $remotePath/Core*.zip"
	# Prompt user for input before proceeding
    #Read-Host -Prompt "Press Enter to continue with the remote machine"
    scp -i $privateKey "$publishPath/$zipFileName" "${remoteUser}@${remoteHost}:$remotePath"
	# Prompt user for input before proceeding
    #Read-Host -Prompt "Press Enter to continue with the remote machine"
    # Execute SSH command
    ssh -i $privateKey ${remoteUser}@${remoteHost} $sshCommand
}

# Display menu
Write-Host "Select the project to process:"
Write-Host "1. Eduegate.ERP.AdminCore"
Write-Host "2. Eduegate.ERP.School.PortalCore"
Write-Host "3. Eduegate.Public.Api"
Write-Host "4. Eduegate.Hub"
Write-Host "5. Eduegate.Signup.PortalCore"
Write-Host "6. Eduegate.Vendor.PortalCore"
Write-Host "7. Eduegate.OnlineExam.PortalCore"
$selection = Read-Host "Enter the number of your choice"

# Handle user selection
switch ($selection) {
    1 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.ERP.AdminCore/Eduegate.ERP.AdminCore.csproj"
        $publishPath = "../../../../Linux-Publish/CoreERPPublish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreERPPublish"
        $remotePath = "/home/sudishnch/Demo/ERP"
        $sshCommand = 'bash /home/sudishnch/Demo/ERP/extract_erp_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
    }
    2 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.ERP.School.PortalCore/Eduegate.ERP.School.PortalCore.csproj"
        $publishPath = "../../../../Linux-Publish/CoreParentPortalPublish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreParentPortalPublish"
        $remotePath = "/home/sudishnch/Demo/ParentPortal"
        $sshCommand = 'bash /home/sudishnch/Demo/ParentPortal/extract_parent_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
	}
    3 {
        $solutionPath = "../../../"
		$projectPath = "../../Services/Apis/Eduegate.Public.Api/Eduegate.Public.Api.csproj"
        $publishPath = "../../../../Linux-Publish/CoreMobileApi_Publish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreApiPublish"
		$remotePath = "/home/sudishnch/Demo/API"
        $sshCommand = 'bash /home/sudishnch/Demo/API/extract_api_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
    }
	4 {
        $solutionPath = "../../../"
		$projectPath = "../../Services/Apis/Eduegate.Hub/Eduegate.Hub/Eduegate.Hub.csproj"
        $publishPath = "../../../../Linux-Publish/CoreChatHub_Publish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreChatHubPublish"
		$remotePath = "/home/sudishnch/Demo/ChatHub"
        $sshCommand = 'bash /home/sudishnch/Demo/ChatHub/extract_chat_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
    }
	5 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.Signup.PortalCore/Eduegate.Signup.PortalCore.csproj"
        $publishPath = "../../../../Linux-Publish/CoreSignupPublish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreSignupPublish"
		$remotePath = "/home/sudishnch/Demo/Signup"
        $sshCommand = 'bash /home/sudishnch/Demo/Signup/extract_signup_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
	}
	6 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.Vendor.PortalCore/Eduegate.Vendor.PortalCore.csproj"
        $publishPath = "../../../../Linux-Publish/CoreVendorPublish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreVendorPublish"
        $remotePath = "/home/sudishnch/Demo/Vendor"
        $sshCommand = 'bash /home/sudishnch/Demo/Vendor/extract_vendor_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
     }
	7 {
        $solutionPath = "../../../"
		$projectPath = "../../Presentation/Eduegate.OnlineExam.PortalCore/Eduegate.OnlineExam.PortalCore.csproj"
        $publishPath = "../../../../Linux-Publish/CoreExamPortalPublish"
			if (-not (Test-Path -Path $publishPath)) {
				New-Item -Path $publishPath -ItemType Directory -Force
			}
		$projectName = "CoreExamPublish"
        $remotePath = "/home/sudishnch/Demo/Exam"
        $sshCommand = 'bash /home/sudishnch/Demo/Exam/extract_exam_publish.sh'
        Process-Project -projectPath $projectPath -publishPath $publishPath -projectName $projectName -remotePath $remotePath -sshCommand $sshCommand
    }
    default {
        Write-Host "Invalid selection. Please enter 1, 2, 3, 4, 5, 6 or 7."
    }
}