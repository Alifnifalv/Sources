app.controller("AcademicSchoolMapController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("AcademicSchoolMapController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.SchoolChanges = function ($event, $element, viewModel) {
        if (viewModel.School == null || viewModel.School == "") return false;
        showOverlay();
        var model = viewModel;
        model.SchoolAcademicyear = null;
        model.Class = null;
        var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.School;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.AcademicYear = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.AcademicYearChanges = function ($event, $element, viewModel) {
        showOverlay();
        var model = viewModel;
        //if (!model.AcademicYear) {
        //    hideOverlay();
        //    return false;
        //}
        var url = "Schools/School/GetMonthAndYearByAcademicYearIDForAcademicSchoolMap?academicYearID=" + model.AcademicYear;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.SchoolWorkingDayMaps = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);