app.controller("PaymentGatewayStatusController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {

    console.log("PaymentGatewayStatusController Loaded");

    $scope.Init = function (statusType, transactionNumber, paymentModeID) {

        if (!transactionNumber) {
            transactionNumber = null;
        }
        if (!paymentModeID) {
            paymentModeID = null;
        }

        if (statusType == "Validate") {
            $scope.IsShowLogoLoader = true;
            $scope.LoadPaymentValidateAlert();
            $scope.ValidatePayment(transactionNumber, paymentModeID);
        }

        if (statusType == "Success") {
            $scope.LoadPaymentSuccessAlert();
        }

        if (statusType == "Fail") {
            $scope.LoadPaymentFailAlert();
        }

        if (statusType == "Cancel") {
            $scope.LoadPaymentCancelAlert();
        }

        if (statusType == "RetryValidate") {
            $scope.LoadRetryPaymentValidateAlert();
            $scope.UpdatePaymentStatus(transactionNumber, paymentModeID);
        }

    };

    $scope.ValidatePayment = function (transactionNumber, paymentModeID) {

        $.ajax({
            type: "GET",
            url: utility.myHost + "PaymentGateway/ValidatePayment",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result) {
                    if (result.IsError) {
                        window.location.replace(utility.myHost + "PaymentGateway/Fail");

                    }
                    else {
                        if (result.Response.toLowerCase().includes("fail")) {
                            window.location.replace(utility.myHost + "PaymentGateway/Fail");
                        }
                        if (result.Response.toLowerCase().includes("success")) {
                            $scope.UpdatePaymentStatus(transactionNumber, paymentModeID);
                        }
                        else {
                            window.location.replace(utility.myHost + "PaymentGateway/Pending");
                        }
                    }
                }
                else {
                    return false;
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });

    }

    $scope.UpdatePaymentStatus = function (transactionNumber, paymentModeID) {

        $.ajax({
            type: "GET",
            url: utility.myHost + "Fee/UpdatePaymentStatus?transactionNumber=" + transactionNumber + "&paymentModeID=" + paymentModeID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result) {
                    window.location.replace(utility.myHost + "PaymentGateway/Success");
                }
                else {
                    window.location.replace(utility.myHost + "PaymentGateway/Fail");
                }
            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    }

    $scope.LoadPaymentValidateAlert = function () {
        $('#paymentValidateAlert').modal('show');
    };

    $scope.LoadPaymentSuccessAlert = function () {
        $('#paymentSuccessAlert').modal('show');
    };

    $scope.LoadPaymentCancelAlert = function () {
        $('#paymentCancelAlert').modal('show');
    };

    $scope.LoadPaymentFailAlert = function () {
        $('#paymentFailAlert').modal('show');
    };

    $scope.LoadRetryPaymentValidateAlert = function () {
        $('#retryPaymentValidateAlert').modal('show');
    };

}]);