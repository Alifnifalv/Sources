app.controller("DriverLocationController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller", 'NgMap',
function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller, NgMap) {
        console.log("DriverLocationController");
        var vm = this;
        vm.DriverPositions = null;

        vm.message = 'You can not hide. :)';
      
        NgMap.getMap("map").then(function (map) {
            vm.map = map;
        });

        vm.SetPositions = function (driverPosition) {
            vm.DriverPositions = angular.copy(driverPosition);
        };

        vm.currentIndex = 0;

        vm.callbackFunc = function (param) {
            $.ajax({
                url: 'Distributions/Driver/CurrentLocations',
                type: 'GET',
                success: function (result) {
                    vm.SetPositions(result.Locations);
                },
                error: function (request, status, message, b) {
                    $().showGlobalMessage($root, $timeout, true, request.responseText);
                }
            })
        };
    }]);