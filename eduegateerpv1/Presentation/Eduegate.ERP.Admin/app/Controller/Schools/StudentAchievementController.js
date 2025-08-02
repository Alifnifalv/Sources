app.controller("StudentAchievementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.StudentChanges = function (viewModel) {

        if (!viewModel.Student.Key) {
            return false;
        };

        showOverlay();
        model = viewModel;

        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.Class = result.data.ClassName;
                model.Section = result.data.SectionName;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

    };

}]);