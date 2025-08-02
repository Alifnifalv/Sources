app.controller("MyAssignmentsController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("MyAssignmentsController  Loaded");
    $scope.colors = ['#57A886', '#5E94D4', '#65A8B7'];

    $scope.Assignments = [];


    $scope.Init = function (model, screenType, AssignmentID) {
        $scope.Assignment = model;
        const defaultStudentID = localStorage.getItem('defaultStudentID');
        $scope.Assignment.Student = defaultStudentID;
        $scope.getAssignment(AssignmentID);
    };



    $scope.getMonth = function (dateString) {
        const date = new Date(dateString);
        const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        return months[date.getMonth()];
    }

    $scope.getDayOfMonth = function  (dateString) {
        const date = new Date(dateString);
        return date.getDate();
    }



    $scope.getAssignment = function (AssignmentIID) {
        //showOverlay();
        $.ajax({
            type: "GET",
            data: { AssignmentID: AssignmentIID },
            url: utility.myHost + "Lms/GetAssignmentByAssignmentID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.Assignments = result.Response;
                        $scope.Assignment = $scope.Assignments;

                       
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.getBackgroundColor = function (index) {
        var rotatedIndex = (index + 3) % 3; // Rotate colors starting from the fourth one
        return $scope.colors[rotatedIndex];
    }
    Dropzone.autoDiscover = false; 

    var myDropzone = new Dropzone("#my-dropzone", {
        url: "/fake-url", // Dummy URL required
        autoProcessQueue: false,
        init: function () {
            this.on("addedfile", function (file) {
                // Manually handle file upload
                $scope.uploadFile(file, myDropzone);
            });

            this.on("error", function (file, errorMessage) {
                console.error("Upload error:", errorMessage);
            });
        }
    });


    $scope.uploadFile = function (file, myDropzone) {
        var formData = new FormData();
        formData.append("ImageType", "Documents");
        formData.append(file.name, file);

        $http.post(utility.myHost + "Content/UploadContents", formData, {
            transformRequest: angular.identity,
            headers: {
                "Content-Type": undefined,
                Accept: "application/json;charset=UTF-8",
                CallContext: JSON.stringify(),
            }
        }).then(function (response) {
            console.log("File uploaded:", response.data);

            if (response.data.Success && response.data.FileInfo.length > 0) {
                var fileName = response.data.FileInfo[0].ContentFileName; // Get file name from response
                var fileInfo = response.data.FileInfo[0];

                // ✅ Store file details in $scope
                $scope.AttachmentReferenceId = fileInfo.ContentFileIID; // Use ContentFileIID as AttachmentReferenceId
                $scope.AttachmentName = fileInfo.ContentFileName; // Use ContentFileName
                // Show the file name in the UI
                document.getElementById("uploaded-file-info").innerHTML =
                    `<p class="text-success font-bold">Uploaded File: ${fileName}</p>`;
            }

            myDropzone.removeFile(file);
            //alert("File Uploaded successfully");
        }).catch(function (error) {
            console.error("Upload error:", error);
            //alert("File Upload Failed");
        });
    };


    $scope.saveAssignment = function () {

        $scope.Assignment.StudentAssignmentMaps.push({
            StudentId: localStorage.getItem('defaultStudentID'),
            AssignmentID: $scope.Assignment.AssignmentIID,
            DateOfSubmission: new Date(),
            Remarks: $scope.AssignmentRemarks,
            AttachmentReferenceId: $scope.AttachmentReferenceId,
            AttachmentName: $scope.AttachmentName 
        });
        $.ajax({
            url: utility.myHost + "Lms/SubmitAssignment",
            type: "POST",
            data: $scope.Assignment,
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Response);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, "Slot request successful!");

                }
            },
            error: function (result) {
                $scope.IsError = false;
                $scope.ErrorMessage = result;
            }
        });
    }



}]);