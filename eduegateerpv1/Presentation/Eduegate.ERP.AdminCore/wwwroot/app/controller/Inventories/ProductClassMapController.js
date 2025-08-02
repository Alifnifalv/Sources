app.controller("ProductClassMapController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }


    $scope.GetSubjectBySubjectType = function (gridModel) {

        if (gridModel.SubjectType == null || gridModel.SubjectType == "")
            return false;
        showOverlay();

        if (gridModel.SubjectType.Key == 1) {
            $scope.LookUps.Subject = null;
            hideOverlay();

        }
        else {
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
        }
    };

}]);