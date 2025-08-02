app.controller("UploadFileController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("UploadFileController");
    $scope.uploadedDocuments = [];

    $scope.Init = function (window, model) {
        windowContainer = '#' + window;
        $scope.Model = model;      
        $scope.InitializeDropZonePlugin();
    }

    $scope.InitializeDropZonePlugin = function () {
        Dropzone.autoDiscover = false;
        $timeout(function () {
            $("#fileuploaddropzone").dropzone({
                previewsContainer: "#previews",
                addRemoveLinks: true,
                uploadMultiple: true,
                success: function (response, file) {
                    $.each(file.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + "?" + item.FileName.split('.')[0] + Math.random();
                        $scope.uploadedDocuments.push(item);
                    }); 
                    console.log("uploaded successfully" + response);
                },
                multiplesuccess: function (file, response) {
                    console.log("uploaded successfully" + response);
                }
            });
        });
    }

    $scope.TriggerUploadFile = function () {      
        angular.element('#UploadFile').trigger('click');
    }
    
    $scope.UploadDocumentFiles = function (uploadfiles) {        
        var url = 'Documents/DocManagement/UploadDocument';
        var xhr = new XMLHttpRequest();
        var fd = new FormData();
        for (i = 0; i < uploadfiles.files.length; i++) {
            fd.append(uploadfiles.files[i].name, uploadfiles.files[i]);
        }

        xhr.open("POST", url, true);
        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response);
                if (result.Success == true && result.FileInfo.length > 0) {
                    $.each(result.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + "?" + item.FileName.split('.')[0] + Math.random();
                        $scope.uploadedDocuments.push(item);
                    });                  
                }
            }
        }
        xhr.send(fd);
    }

    $scope.DeleteUploadedDcuments = function (DocInfo) {
        var fileName = DocInfo != null && DocInfo != undefined ? DocInfo.FileName : ""
        $.ajax({
            url: "Documents/DocManagement/DeleteUploadedDocument?fileName=" + fileName,
            type: "POST",
            success: function (result) {
                if (result.Success) {
                    if (fileName != null) {
                        $scope.uploadedDocuments = $.grep($scope.uploadedDocuments, function (e) {
                            return e.FileName != DocInfo.FileName;
                        });                        
                    }
                }
            }
        })
    }   

    $scope.SaveUploadedFiles = function ($event) {     
        var data = { files : $scope.uploadedDocuments };
        $.ajax({
            url: "Documents/DocManagement/SaveUploadedFiles",
            type: "POST",   
            dataType: "json",
            data: data,
            success: function (result) {
                if (result.Success) {
                    $scope.uploadedDocuments = [];
                    $().showMessage($scope, $timeout, true, "Files uploaded successfully.");
                    $('.dropzone')[0].dropzone.files.forEach(function (file) {
                        file.previewElement.remove();
                    });

                    $('.dropzone').removeClass('dz-started');
                    $scope.ReloadGrid();
                }
            },
            error: function (error) {
                $().showMessage($scope, $timeout, true, "Upload failed");
            },
        })
    }   
}]);