app.controller("SectorTicketFareController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.GenerateTravelSector = function ($event, $element, vModel) {
        if (vModel.Airport == null || vModel.Airport.Key == null || vModel.ReturnAirport == null || vModel.ReturnAirport.Key == null) return false;

      
        const match = vModel.Airport.Value.match(/\(([^)]+)\)/);
        const matchReturn = vModel.ReturnAirport.Value.match(/\(([^)]+)\)/);
        if (match) {
            const shortAirportName = match[1];
            const shortReturnAirportName = matchReturn[1];
            if (vModel.IsTwoWay)
                vModel.GenerateTravelSector = shortAirportName + "_" + shortReturnAirportName + "_" + shortAirportName;
            else
                vModel.GenerateTravelSector = shortAirportName + "_" + shortReturnAirportName;
        }
    };


}]);
