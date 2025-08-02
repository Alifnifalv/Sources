app.controller("ExamController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.SubjectTypeChange = function ($event, $element, gridModel) {
        if (gridModel.SubjectType == null || gridModel.SubjectType == "") return false;
        showOverlay();
        var model = gridModel;
        model.Subject = null;
        var url = "Schools/School/GetSubjectByType?subjectTypeID=" + model.SubjectType.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Subject = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);