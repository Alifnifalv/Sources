app.controller("AcademicClassMapController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("AcademicClassMapController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.AcademicYearChanges = function ($event, $element, viewModel) {
        showOverlay();
        var model = viewModel;
        var url = "Schools/School/GetMonthAndYearByAcademicYearIDForAcademicClassMap?academicYearID=" + model.AcademicYear.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.WorkingDayMaps = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);