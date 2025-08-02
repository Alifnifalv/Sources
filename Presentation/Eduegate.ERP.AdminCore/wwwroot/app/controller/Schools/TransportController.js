app.controller("TransportController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.RouteGroupChanges = function ($event, $element, viewModel) {
        var model = viewModel;

        if (!model.RouteGroup)
        {
            return false;
        }

        showOverlay();
        model.Routes = null;
        var url = "Schools/School/GetRoutesByRouteGroupID?routeGroupID=" + model.RouteGroup;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Route = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.RouteChanges = function ($event, $element, viewModel) {
        var model = viewModel;

        if (!model.Routes) {
            return false;
        }

        showOverlay();

        var url = "Schools/School/GetVehiclesByRoute?routeID=" + model.Routes.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.VehicleDetails = result.data;

                model.Vehicle.Key = $scope.LookUps.VehicleDetails[0]?.Key;
                model.Vehicle.Value = $scope.LookUps.VehicleDetails[0]?.Value;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

}]);