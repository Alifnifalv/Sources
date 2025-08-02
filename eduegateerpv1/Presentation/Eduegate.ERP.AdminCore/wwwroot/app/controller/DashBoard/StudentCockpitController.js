app.controller("FeePaymentHistoryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    console.log("Fee Payment History Controller Loaded");

    $scope.CurrentDate = new Date();

    $scope.ShowPreLoader = true;

    $scope.IsEnableLoader = true;

    $scope.FeePaymentHistory = [];
    $scope.Schools = [];
    $scope.AcademicYears = [];
    $scope.FeeCollectionHistories = [];

    $scope.SelectedSchool = {};
    $scope.SelectedAcademicYear = {};

    $scope.init = function () {      

        $scope.GetFeeCollectionHistory();
    };

    $scope.GetFeeCollectionHistory = function () {

        $scope.FeeCollectionHistories = [];
       

        $.ajax({
            type: "GET",
            url: utility.myHost + "Fee/GetFeeCollectionHistory",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.FeeCollectionHistories = result.Response;
                    });
                }

            },
            complete: function (result) {

            },
            error: function () {
               
            }
        });
    };

    $scope.FilterButtonClick = function () {
        $scope.GetFeeCollectionHistory();
    };

    $scope.ClearFilterClick = function () {
        $scope.GetSchoolsList();
        $scope.FillLastCollectionHistories();
    };

    $scope.DuePaymentClick = function () {

        window.location.replace(utility.myHost + "Fee/FeePayment");
    };

    $scope.RetryPaymentTransaction = function (model) {

        showOverlay();
        if (model.Amount > 0) {
            var transactionNo = model.TransactionNumber;
            var paymentModeID = model.FeePaymentModeID;

            var url = utility.myHost + "Fee/CheckFeeCollectionExistingStatus?transactionNumber=" + transactionNo;

            $http({
                url: url,
                method: "GET",
            }).then(function (result) {

                if (!result.data.IsError) {
                    $scope.RetryPayment(transactionNo, paymentModeID);
                }
                else {
                    hideOverlay();
                    callToasterPlugin('error', "Fee already paid for the same month or type!");
                }

            });
        }
        else {
            hideOverlay();
            callToasterPlugin('error', "An amount greater than zero is required to retry payment!");
        }

    };

    $scope.RetryPayment = function (transactionNo, paymentModeID) {

        showOverlay();
        var url = utility.myHost + "Fee/CheckTransactionPaymentStatus?transactionNumber=" + transactionNo + "&paymentModeID=" + paymentModeID;

        $http({
            url: url,
            method: "GET",
        }).then(function (result) {

            if (result.data.IsError) {
                $scope.InitiateRetryPayment(transactionNo, paymentModeID);
            }
            else {
                window.location.replace(utility.myHost + "PaymentGateway/RetryValidate?transactionNumber=" + transactionNo + "&paymentModeID=" + paymentModeID);
                hideOverlay();
            }

        });
    };

    $scope.InitiateRetryPayment = function (transactionNo, paymentModeID) {

        showOverlay();
        var url = utility.myHost + "PaymentGateway/RetryPayment?transactionNumber=" + transactionNo + "&paymentModeID=" + paymentModeID;

        $http({
            url: url,
            method: "POST",
        }).then(function (result) {

            if (!result.data.IsError && result.data.Response !== null) {
                window.location.replace(utility.myHost + "PaymentGateway/Initiate?paymentModeID=" + paymentModeID);
                hideOverlay();
            }
            else {
                hideOverlay();
                callToasterPlugin('error', "Something went wrong, try again later!");
            }

        });
    };

    $scope.ResendMail = function (model) {
        $scope.SelectedTransactionDetails = model;
        $('#MailConformationModal').modal('show');
    };

    $scope.SendMailReceipt = function () {
        showOverlay();
        if ($scope.SelectedTransactionDetails.TransactionNumber) {

            var url = utility.myHost + "Fee/ResendMailReceipt?transactionNumber=" + $scope.SelectedTransactionDetails.TransactionNumber + "&mailID=" + $scope.SelectedTransactionDetails.ParentEmailID;

            $http({
                url: url,
                method: "POST",
            }).then(function (result) {

                if (result.data) {
                    hideOverlay();
                    $('#MailConformationModal').modal('hide');
                    callToasterPlugin('success', "Email has been sent successfully!");
                }
                else {
                    hideOverlay();
                    $('#MailConformationModal').modal('hide');
                    callToasterPlugin('error', "Failed to Send Mail!");
                }

            });
        }
        else {
            hideOverlay();
            $('#MailConformationModal').modal('hide');
            callToasterPlugin('error', "Unable to send email right now. Please try again later!");
        }

    };

    $scope.FeePaymentTabClick = function () {
        window.location.replace(utility.myHost + "Fee/Index");
    };

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeePaymentHistoryOverlay").fadeIn();
            });
        });
    };

    function hideOverlay() {
        $scope.IsEnableLoader = false;
        $timeout(function () {
            $scope.$apply(function () {
                $("#FeePaymentHistoryOverlay").fadeOut();
            });
        });
    };

    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];
        var $groupRow = $(event.currentTarget).closest('tr').next();

        if (model[field]) {
            $groupRow.show();
        } else {
            $groupRow.hide();
        }
    };

    $scope.ClosePopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    };

    $scope.OpenPopup = function () {
        $("#ItemPopup").fadeIn("fast");
        $(".gridItemOverlay").show();
    };

    $scope.SubmitPopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    };

}]);