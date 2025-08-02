app.controller("PaymentValidationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("PaymentValidationController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    //function showOverlay() {
    //    $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    //}

    //function hideOverlay() {
    //    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    //}

    $scope.IsEnableLoader = false;

    $scope.FillData = function (data) {
        $scope.IsEnableLoader = true;
        showOverlay();

        if (data.TransactionNumber) {
            $scope.FeeCollections = data.FeeCollections;
            $scope.TransactionNumber = data.TransactionNumber;
            $scope.FeeCollectionStatusID = data.FeeCollectionStatusID;
            $scope.PendingCollectionStatusID = data.PendingFeeCollectionStatusID;
            $scope.BankCollectedAmount = data.TotalAmount;
            $scope.PaymentValidationStatus = data.PaymentValidationStatus != null ? data.PaymentValidationStatus : "Not available";
            $scope.CreatedBy = data.FeeCollections.length > 0 ? data.FeeCollections[0].CreatedBy : null;
            $scope.PaymentModeID = data.FeeCollections.length > 0 ? data.FeeCollections[0].FeePaymentModeID : null;
            $scope.PaymentModeName = data.FeeCollections.length > 0 ? data.FeeCollections[0].FeePaymentMode : null;
        }

        $scope.IsEnableLoader = false;
        hideOverlay();
    };

    $scope.CheckAndUpdateFeeCollectionStatus = function () {
        showOverlay();
        var url = "Schools/School/CheckFeeCollectionExistingStatusByTransNo?transactionNumber=" + $scope.TransactionNumber;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, "Error: Fee already collected for the same month or type.", 3000);
                    hideOverlay();

                    $("[data-original-title]").popover('dispose');
                }
                else {
                    $scope.UpdateFeeCollectionsStatus();
                }
            }, function () {
                hideOverlay();
            });
    };


    $scope.UpdateFeeCollectionsStatus = function () {
        showOverlay();
        var url = "Schools/School/UpdateFeeCollectionStatusByTransactionNo?groupTransNumber=" + $scope.TransactionNumber + "&parentLoginID=" + $scope.CreatedBy + "&paymentModeID=" + $scope.PaymentModeID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, "Error: Unable to update fee collection status.", 2500);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, "Fee collection status updated successfully", 2500);

                    $scope.$parent.RowModel.ReLoad();
                }

                $("[data-original-title]").popover('dispose');

                hideOverlay();
                return false;
            }, function () {
                hideOverlay();
            });
    };

    function showOverlay() {
        $scope.IsEnableLoader = true;
        $timeout(function () {
            $scope.$apply(function () {
                $("#PaymentValidationOverlay").show();
            });
        });
    };

    function hideOverlay() {
        $scope.IsEnableLoader = false;
        $timeout(function () {
            $scope.$apply(function () {
                $("#PaymentValidationOverlay").hide();
            });
        });
    };

}]);