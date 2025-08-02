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
        $scope.ShowPreLoader = true;

        $scope.GetSchoolsList();

        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "QPAY_PAYMENT_MODE_ID",
        }).then(function (result) {
            $scope.QPAYPaymentMode = result.data;

        });

        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "FEECOLLECTIONPAYMENTMODE_ONLINE",
        }).then(function (result) {
            $scope.OnlinePaymentMode = result.data;
        });

        $scope.FillLastCollectionHistories();
    };

    $scope.GetSchoolsList = function () {

        $scope.SelectedSchool = {};
        $scope.Schools = [];

        showOverlay();

        $.ajax({
            type: "GET",
            data: { loginID: $rootScope.LoginID },
            url: utility.myHost + "Home/GetSchoolsByParentLoginID?loginID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                var schoolDatas = result;

                if (schoolDatas.length > 0) {
                    $scope.Schools.push({
                        "Key": 0,
                        "Value": "All"
                    });
                    schoolDatas.forEach(x => {
                        $scope.Schools.push({
                            "Key": x.Key,
                            "Value": x.Value
                        });
                    });
                }
            },
            complete: function (result) {
                if ($scope.Schools.length > 0) {
                    if ($scope.Schools.length == 2) {
                        if ($scope.Schools[1].Key != null) {
                            $scope.SelectedSchool.Key = $scope.Schools[1].Key;
                            $scope.SelectedSchool.Value = $scope.Schools[1].Value;
                        };
                        $scope.SchoolChanges();
                    }
                    else if ($scope.Schools[0].Key != null) {
                        $scope.SelectedSchool.Key = $scope.Schools[0].Key;
                        $scope.SelectedSchool.Value = $scope.Schools[0].Value;

                        $scope.SchoolChanges();
                    };
                }

                hideOverlay();
            },
            error: function () {
                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });

    };

    $scope.SchoolChanges = function () {

        $scope.FeeCollectionHistories = [];

        $scope.ShowPreLoader = true;

        $scope.SelectedAcademicYear = {};
        $scope.AcademicYears = [];

        showOverlay();

        var schoolID = $scope.SelectedSchool != null ? $scope.SelectedSchool.Key : null;

        $scope.AcademicYears.push({
            "Key": 0,
            "Value": "Current"
        });

        if (schoolID != 0) {

            $.ajax({
                type: "GET",
                data: { schoolID: schoolID },
                url: utility.myHost + "Home/GetAllAcademicYearBySchoolID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    var academicYearDatas = result.Response;
                    if (academicYearDatas.length > 0) {
                        academicYearDatas.forEach(x => {
                            $scope.AcademicYears.push({
                                "Key": x.Key,
                                "Value": x.Value
                            });
                        });
                    }
                },
                complete: function (result) {

                    if ($scope.AcademicYears.length > 0) {
                        if ($scope.AcademicYears.length == 2) {
                            if ($scope.AcademicYears[1].Key != null) {
                                $scope.SelectedAcademicYear.Key = $scope.AcademicYears[1].Key;
                                $scope.SelectedAcademicYear.Value = $scope.AcademicYears[1].Value;
                            };
                        }
                        else if ($scope.AcademicYears[0].Key != null) {
                            $scope.SelectedAcademicYear.Key = $scope.AcademicYears[0].Key;
                            $scope.SelectedAcademicYear.Value = $scope.AcademicYears[0].Value;
                        };
                    }

                    hideOverlay();
                },
                error: function () {
                    hideOverlay();
                    $scope.ShowPreLoader = false;
                }
            });
        }
        else {
            if ($scope.AcademicYears[0].Key != null) {
                $scope.SelectedAcademicYear.Key = $scope.AcademicYears[0].Key;
                $scope.SelectedAcademicYear.Value = $scope.AcademicYears[0].Value;
            };

            hideOverlay();
        }
    };

    $scope.GetFeeCollectionHistory = function () {

        $scope.FeeCollectionHistories = [];
        $scope.ShowPreLoader = true;

        showOverlay();

        var schoolID = $scope.SelectedSchool.Key;
        var academicYearID = $scope.SelectedAcademicYear.Key;

        $.ajax({
            type: "GET",
            url: utility.myHost + "Fee/GetFeeCollectionHistories?schoolID=" + schoolID + "&academicYearID=" + academicYearID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.FeeCollectionHistories = result.Response;
                    });
                }

                $scope.ShowPreLoader = false;
            },
            complete: function (result) {

                hideOverlay();
            },
            error: function () {
                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });
    };

    $scope.FillLastCollectionHistories = function () {

        $scope.FeeCollectionHistories = [];
        $scope.ShowPreLoader = true;

        showOverlay();

        $.ajax({
            type: "GET",
            url: utility.myHost + "Fee/GetLastTenFeeCollectionHistories",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.FeeCollectionHistories = result.Response;
                    });
                }

                $scope.ShowPreLoader = false;
            },
            complete: function (result) {

                hideOverlay();
            },
            error: function () {
                hideOverlay();
                $scope.ShowPreLoader = false;
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

            var feeReceiptNo = $scope.SelectedTransactionDetails.StudentHistories.length == 1 ? $scope.SelectedTransactionDetails.StudentHistories[0].FeeReceiptNo : null;

            var url = utility.myHost + "Fee/ResendMailReceipt?transactionNumber=" + $scope.SelectedTransactionDetails.TransactionNumber
                + "&mailID=" + $scope.SelectedTransactionDetails.ParentEmailID + "&feeReceiptNo=" + feeReceiptNo;

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