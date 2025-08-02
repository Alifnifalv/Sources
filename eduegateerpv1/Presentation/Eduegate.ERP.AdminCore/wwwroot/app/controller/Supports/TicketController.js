app.controller("TicketController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.FillEmployees = function (model) {
        showOverlay();

        $scope.LookUps.OldSupportingManagers = $scope.LookUps.SupportingManagers;

        var url = utility.myHost + "Home/GetEmployeesByDepartment?departmentID=" + model.Department;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.LookUps.SupportingManagers = result.data;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    }

}]);