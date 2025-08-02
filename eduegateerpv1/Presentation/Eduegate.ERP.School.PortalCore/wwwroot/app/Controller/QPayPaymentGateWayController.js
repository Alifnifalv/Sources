app.controller("QPayPaymentGateWayController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {

    console.log("QPayPaymentGateWayController Loaded");

    $scope.Init = function () {

       
            $scope.IsShowLogoLoader = true;
            $scope.LoadPaymentValidateAlert();
            $scope.ValidatePayment();
       

    };

    $scope.ValidatePayment = function () {

        $.ajax({
            type: "GET",
            url: utility.myHost + "PaymentGateway/ValidateQPayPaymentResponse",
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

                        if (result.Response.toLowerCase().includes("pending")) {
                            window.location.replace(utility.myHost + "PaymentGateway/Pending");
                        }

                        if (result.Response.toLowerCase().includes("success")) {
                            window.location.replace(utility.myHost + "PaymentGateway/Success");
                           // $scope.UpdatePaymentStatus();
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

    $scope.UpdatePaymentStatus = function () {

        $.ajax({
            type: "GET",
            url: utility.myHost + "Fee/UpdatePaymentStatus",
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

}]);