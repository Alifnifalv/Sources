app.controller('RouteDetailsController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('RouteDetailsController loaded.');
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
     
    $scope.onRouteDetails = false;
    $scope.VehiclesDetails = [];
    $scope.RoutesDetails = [];
    $scope.RouteStopsDetails = [];
    $scope.CurrentDate = new Date();

    $rootScope.ShowLoader = true;

    $scope.init = function () {

    }

    $scope.toggleGrid = function (event, model, type) {

        if (type == 'Vehicle') {
            toggleHeader = $(event.currentTarget).closest(".vehicleToggleContainer").find(".vehicleToggleHeader");
            toggleContent = $(event.currentTarget).closest(".vehicleToggleContainer").find(".vehicleToggleContent");
            toggleHeader.toggleClass("active");
            if (toggleHeader.hasClass('active')) {
                toggleContent.slideDown("fast");
                $scope.GetRoutesInfo(model);
            }
            else {
                toggleContent.slideUp("fast");
            }
        }

        if (type == 'Route') {
            toggleHeader = $(event.currentTarget).closest(".vehicleToggleContainer").find(".routeToggleHeader");
            toggleContent = $(event.currentTarget).closest(".vehicleToggleContainer").find(".routeToggleContent");
            toggleHeader.toggleClass("active");
            if (toggleHeader.hasClass('active')) {
                toggleContent.slideDown("fast");
                $scope.GetRouteStopsInfo(model);
            }
            else {
                toggleContent.slideUp("fast");
            }
        }
    }


    $scope.LoadVehicleInfo = function () {
        $http({
            method: 'GET',
            url: dataService + '/GetVehicleDetailsByEmployeeLoginID',
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.VehiclesDetails = result;
            $rootScope.ShowLoader = false;
        }).error(function (){
            $rootScope.ShowLoader = false;
        });
    }

    $scope.GetRoutesInfo = function (data) {
        $http({
            method: 'GET',
            url: dataService + '/GetRoutesByVehicleID?vehicleID=' + data.VehicleIID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.RoutesDetails = result;
        }).error(function (){
            $rootScope.ShowLoader = false;
        });
    }

    $scope.GetRouteStopsInfo = function (data) {
        $http({
            method: 'GET',
            url: dataService + '/GetRouteStopsByRouteID?routeID=' + data.RouteID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.RouteStopsDetails = result;
        }).error(function (){
            $rootScope.ShowLoader = false;
        });
    }

    $scope.init();
}]);