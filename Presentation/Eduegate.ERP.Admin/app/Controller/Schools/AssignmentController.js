app.controller("AssignmentController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("AssignmentController Loaded");

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
        var url = "Schools/School/GetSubjectByClass?classID=" + model.Class.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Subject = result.data;
                
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };


    $scope.FillClassWiseSubjects = function ($event, $element, crudModel) {
        var model = crudModel;

        if (crudModel.Section == null || crudModel.Section.Key == 0 || crudModel.Section.Key == "" || crudModel.Section.Key == null) {
            return false;
        }

        if (crudModel.Class == null || crudModel.Class.Key == 0 || crudModel.Class.Key == "" || crudModel.Class.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select class!");
            model.Section = null;
            return false;
        }

        showOverlay();
        var url = "Schools/School/FillClassWiseSubjects?classID=" + model.Class.Key + "&sectionID=" + model.Section.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.OtherClassTeachers = result.data;
                $scope.fillSubjectLookUp(model.Class.Key, model.Section.Key);
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };


    $scope.fillSubjectLookUp = function (classID, sectionID) {
        $scope.LookUps.Subject = [];
        var url = "Schools/School/FillClassWiseSubjects?classID=" + classID + "&sectionID=" + sectionID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                result.data.forEach(x => {
                    $scope.LookUps.Subject.push({
                        "Key": x.Subject.Key,
                        "Value": x.Subject.Value,
                    });
                });
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    }
}]);