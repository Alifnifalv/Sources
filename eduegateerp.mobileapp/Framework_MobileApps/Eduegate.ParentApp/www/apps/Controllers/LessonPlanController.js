app.controller("LessonPlanController", ["$scope", "$http", "$state", "rootUrl", "$location", "$rootScope", "$stateParams", "GetContext", "$sce", "$timeout", function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce, $timeout) {
  console.log("Lesson plan controller loaded.");

  var dataService = rootUrl.SchoolServiceUrl;
  var context = GetContext.Context();

  $scope.LessonPlanList = [];

  $rootScope.ShowLoader = true;
  $rootScope.ShowPreLoader = true;

  $scope.ParentUrlService = rootUrl.ParentUrl;

  $scope.StudentID = $stateParams.studentID;

  $scope.init = function () {
    $scope.FillLessonPlans($scope.StudentID);
  };

  $scope.toggleGrid = function (event) {
    toggleHeader = $(event.currentTarget)
      .closest(".toggleContainer")
      .find(".toggleHeader");
    toggleContent = $(event.currentTarget)
      .closest(".toggleContainer")
      .find(".toggleContent");
    toggleHeader.toggleClass("active");
    if (toggleHeader.hasClass("active")) {
      toggleContent.slideDown("fast");
    } else {
      toggleContent.slideUp("fast");
    }
  };

  //To Get LessonPlans
  $scope.FillLessonPlans = function (studentID) {
    $scope.LessonPlanList = [];

    $http({
      method: 'GET',
      url: dataService + '/GetLessonPlanListByStudentID?studentID=' + $scope.StudentID,
      headers: {
        "Accept": "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).success(function (result) {
      $scope.LessonPlanList = result;

      $rootScope.ShowLoader = false;
      $rootScope.ShowPreLoader = false;
    }).error(function () {
      $rootScope.ShowLoader = false;
    });

  };
  //End To Get LessonPlans


  var downLoadFile;
  var permissions;

  $scope.DownloadURL = function (referenceID) {

    $rootScope.ShowLoader = true;
    $http({
      url: $scope.ParentUrlService + "Content/ReadContentsByIDForMobile?contentID=" + referenceID,
      method: 'GET',
      headers: {
        "Accept": "application/json;charset=UTF-8", "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).then(function (result) {
      result = result.data;
      downLoadFile = result;
      permissions = cordova.plugins.permissions;
      if(device.platform.toLowerCase() == "android" && device.version < 13)
      {
      permissions.checkPermission(permissions.READ_EXTERNAL_STORAGE, checkPermissionCallback, null);
      }
      else
      {
        permissions.checkPermission(permissions.READ_MEDIA_IMAGES, checkPermissionCallback, null);
      }
      $rootScope.ShowLoader = false;
    }
      , function (err) {
        $rootScope.ShowLoader = false;
      });
  }

  function downloadFile(fileUrl) {
    var fileTransfer = new FileTransfer();
    var uri = encodeURI(fileUrl);
    const fileExtension = fileUrl.substr(fileUrl.lastIndexOf('.'))
    // const fileName = fileUrl.substr(fileUrl.lastIndexOf('_')).replaceAll('_', '')
    const fileName = fileUrl.substr(fileUrl.lastIndexOf('_'))
    var FileLocalPath = cordova.file.externalApplicationStorageDirectory ? cordova.file.externalApplicationStorageDirectory
      : cordova.file.documentsDirectory;
    var localPath = FileLocalPath + 'Podar' + fileName;

    //check the file name has html, open in a window
    if (fileExtension.includes('.html')) {
      cordova.InAppBrowser.open(fileUrl);
    }
    else {
      fileTransfer.download(
        uri, localPath, function (entry) {
          if (fileExtension == ".pdf") {
            cordova.plugins.fileOpener2.open(
              localPath,
              'application/pdf',
              {
                error: function (e) {
                  console.log('Error status: ' + e.status + ' - Error message: ' + e.message);
                },
                success: function () {
                  console.log('file opened successfully');
                }
              }
            );
          }

          else if (fileExtension == ".doc" || fileExtension == ".rtf" || fileExtension == ".docx") {
            cordova.plugins.fileOpener2.open(
              localPath,
              'application/msword',
              {
                error: function (e) {
                  console.log('Error status: ' + e.status + ' - Error message: ' + e.message);
                },
                success: function () {
                  console.log('file opened successfully');
                }
              }
            );
          }

          else if (fileExtension == ".xls" || fileExtension == ".xlsx") {
            cordova.plugins.fileOpener2.open(
              localPath,
              'application/x-msexcel',
              {
                error: function (e) {
                  console.log('Error status: ' + e.status + ' - Error message: ' + e.message);
                },
                success: function () {
                  console.log('file opened successfully');
                }
              }
            );
          }

          else if (fileExtension == ".gif") {
            cordova.plugins.fileOpener2.open(
              localPath,
              'image/GIF',
              {
                error: function (e) {
                  console.log('Error status: ' + e.status + ' - Error message: ' + e.message);
                },
                success: function () {
                  console.log('file opened successfully');
                }
              }
            );
          }

          else if (fileExtension == ".txt") {
            cordova.plugins.fileOpener2.open(
              localPath,
              'text/plain',
              {
                error: function (e) {
                  console.log('Error status: ' + e.status + ' - Error message: ' + e.message);
                },
                success: function () {
                  console.log('file opened successfully');
                }
              }
            );
          }

          else if (fileExtension == ".jpeg" || fileExtension == ".jfif" || fileExtension == ".webp") {
            cordova.plugins.fileOpener2.open(
              localPath,
              'image/jpeg',
              {
                error: function (e) {
                  console.log('Error status: ' + e.status + ' - Error message: ' + e.message);
                },
                success: function () {
                  console.log('file opened successfully');
                }
              }
            );
          }

          $rootScope.ShowToastMessage('File downloaded successfully in' + FileLocalPath , 'success');
        },

        function (error) {
          $rootScope.ShowToastMessage("Something went wrong, please try later" , 'error');
        },

        false
      );
    }
  }


  function checkPermissionCallback(status) {
    if (device.platform.toLowerCase() == "android" && device.version < 13) {
      if (!status.hasPermission) { // does not get permission
        var errorCallback = function () {
          console.warn('Storage permission is not turned on');
        }
        if (!status.hasPermission) {
          permissions.requestPermission(
            permissions.READ_EXTERNAL_STORAGE,
            function (status) {
              if (!status.hasPermission) {
                errorCallback();
              } else {
                // continue with downloading/ Accessing operation 
                downloadFile(downLoadFile);
              }
            },
            errorCallback);
        }

      } else {
        downloadFile(downLoadFile);
      }
    }

    else {
      if (!status.hasPermission) { // does not get permission
        var errorCallback = function () {
          console.warn('Storage permission is not turned on');
        }
        if (!status.hasPermission) {
          permissions.requestPermission(
            permissions.READ_MEDIA_IMAGES,
            function (status) {
              if (!status.hasPermission) {
                errorCallback();
              } else {
                // continue with downloading/ Accessing operation 
                downloadFile(downLoadFile);
              }
            },
            errorCallback);
        }

      } else {
        downloadFile(downLoadFile);
      }
    }
  }

  // $scope.init();
}]);