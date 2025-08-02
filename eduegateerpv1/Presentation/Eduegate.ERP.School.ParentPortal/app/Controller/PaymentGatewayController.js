app.controller("PaymentGatewayController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {

    $scope.IsShowLogoLoader = true;

    console.log("PaymentGatewayController Loaded");

    $scope.Init = function () {
        $scope.IsShowLogoLoader = true;

        $scope.StartPayment();
    };

    $scope.CancelPayment = function () {
        window.location.replace(utility.myHost + "/PaymentGateway/Cancel");
    }

    $scope.retryPayment = function () {
        redirectToCardPayment();
    }

    $scope.GetMasterSession = function () {
        window.location.replace(utility.myHost + "/PaymentGateway/InitiatePayment");
    };

    $scope.StartPayment = function () {
        window.location.replace(utility.myHost + "/PaymentGateway/InitiateCardFeePayment");
    };

    function redirectToCardPayment() {
        window.location.replace(utility.myHost + "/PaymentGateway/RetryPayment");
    }

}]);