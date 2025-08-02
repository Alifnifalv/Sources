app.controller("OrderChangeRequestController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller", "$rootScope",
function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller, $rootScope) {

    console.log("OrderChangeRequestController loaded.");

        angular.extend(this, $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService, $rootScope : $rootScope }));

        $scope.ExchangeActionChange = function () {
            console.log('disable/enable search box using action');
        }
    }]);