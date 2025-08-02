app.controller("TimeTableExtensionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("TimeTableExtensionController Loaded");

    $scope.init = function () {

    };


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.SubjectTypeChanges = function ($event, $element, model) {
        showOverlay();
        if (model.SubjectType.Key != null) {
            var url = "Schools/School/GetSubjectBySubjectTypeID?subjectTypeID=" + model.SubjectType.Key;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.Subject = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        hideOverlay();
    }

    $scope.SubjectChanges = function ($event, $element, model) {
        showOverlay();

        var classIds = model.Class.map(item => item.Key).join(',');
        var subjectIDs = model.Subject.map(item => item.Key).join(',');

        if (classIds.length == 0 || classIds == 'undefined') {
            $().showMessage($scope, $timeout, true, "Please select atleast one class");
            model.Subject = null;

            hideOverlay();

            return;
        }

        if (subjectIDs != "" && classIds != "") {
            var url = "Schools/School/GetTeacherByClassAndSubject?classIDs=" + classIds + "&subjectIDs=" + subjectIDs;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.Teacher = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        else {
            //Classes
            $http({
                method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Teacher&defaultBlank=false",
            }).then(function (result) {
                $scope.LookUps.Teacher = result.data;
            });
        }
        hideOverlay();
    }

    

}]);

