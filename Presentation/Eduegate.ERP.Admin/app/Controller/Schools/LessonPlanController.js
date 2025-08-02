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
        var url = "Schools/School/GetSubjectByClass?classID=" + model.Class.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Subject = result.data;
                
                hideOverlay();
            }, function () {
                hideOverlay();
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