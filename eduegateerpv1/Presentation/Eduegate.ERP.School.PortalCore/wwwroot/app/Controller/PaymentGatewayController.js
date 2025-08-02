app.controller("PaymentGatewayController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {

    $scope.IsShowLogoLoader = true;

    console.log("PaymentGatewayController Loaded");

    $scope.Init = function (feePaymentModeID) {
        $scope.IsShowLogoLoader = true;

        $scope.FeePaymentModeID = feePaymentModeID;

        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "QPAY_PAYMENT_MODE_ID",
        }).then(function (result) {
            $scope.QPAYPaymentMode = result.data;

        }).then(function (result) {
            $scope.StartPayment();
        });

        //$http({
        //    method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "FEECOLLECTIONPAYMENTMODE_ONLINE",
        //}).then(function (result) {
        //    $scope.OnlinePaymentMode = result.data;
        //}).then(function (result) {
        //    $scope.StartPayment();
        //});
    };

    $scope.CancelPayment = function () {
        window.location.replace(utility.myHost + "PaymentGateway/Cancel");
    }

    $scope.retryPayment = function () {
        redirectToCardPayment();
    }

    $scope.GetMasterSession = function () {
        window.location.replace(utility.myHost + "PaymentGateway/InitiatePayment");
    };

    $scope.StartPayment = function () {

        if ($scope.FeePaymentModeID == $scope.QPAYPaymentMode) {
            window.location.replace(utility.myHost + "PaymentGateway/InitiateQPAYPayment");
        }
        else {
            window.location.replace(utility.myHost + "PaymentGateway/InitiateCardFeePayment");
        }
    };

    function redirectToCardPayment() {
        window.location.replace(utility.myHost + "PaymentGateway/RetryPayment");
    }

}]);