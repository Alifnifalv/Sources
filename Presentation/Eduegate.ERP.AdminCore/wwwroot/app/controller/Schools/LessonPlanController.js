app.controller("LessonPlanController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("LessonPlanController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.ClassChanges = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var url = "Schools/School/GetSubjectByClass?classID=" + model.StudentClass.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Subject = result.data;
                
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    
    $scope.SaveUploadedFiles = function ($event, $element, crudModel) {
        var url = "Schools/School/ExtractUploadedFiles"; // Your URL
        showOverlay(); // Show loading indicator if needed

        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.extractedData = result.data; // Data from your URL
                console.log("Data Received from ExtractUploadedFiles:", $scope.extractedData);

                // **Crucial:  Process and assign the data to crudModel.ViewModel**
                if ($scope.extractedData) {
                    crudModel.LessonPlanIID = $scope.extractedData.LessonPlanIID;
                    crudModel.Title = $scope.extractedData[0].Title;
                    crudModel.AllignmentToVisionAndMission = $scope.extractedData.AllignmentToVisionAndMission;


                    //Handle teachingaidid and other
                    //For the lists, just assign empty arrays for now to see what would happen.
                    crudModel.Class = [];
                    crudModel.Section = [];

                    // And so on for each mapping
                }
                else {
                    console.warn("No data received from URL!");
                }

                hideOverlay(); // Hide loading indicator
            }, function (error) {
                console.error("Error fetching data:", error);
                hideOverlay(); // Hide loading indicator even on error
            });
    };



    $scope.SyllabusCompleteCheckBox = function ($event, $element, crudModel) {

        var checkBox = crudModel.IsSyllabusCompleted;

        if (checkBox == true) {
            crudModel.HideActionPlan = true;
            return false;
        }
        else {
            crudModel.HideActionPlan = false;
            return false;
        }

    }

}]);