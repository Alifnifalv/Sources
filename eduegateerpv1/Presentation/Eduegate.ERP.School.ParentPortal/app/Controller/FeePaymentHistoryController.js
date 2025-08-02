app.controller("FeePaymentHistoryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

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
    };

    $scope.GetSchoolsList = function () {

        $scope.SelectedSchool = {};
        $scope.Schools = [];

        showOverlay();

        $.ajax({
            type: "GET",
            data: { loginID: $rootScope.LoginID },
            url: utility.myHost + "/Home/GetSchoolsByParentLoginID?loginID",
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
                    if ($scope.Schools[0].Key != null) {
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

        var schoolID = $scope.SelectedSchool?.Key;

        $scope.AcademicYears.push({
            "Key": 0,
            "Value": "Current"
        });

        if (schoolID != 0) {

            $.ajax({
                type: "GET",
                data: { schoolID: schoolID },
                url: utility.myHost + "/Home/GetAllAcademicYearBySchoolID?schoolID",
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
                        if ($scope.AcademicYears[0].Key != null) {
                            $scope.SelectedAcademicYear.Key = $scope.AcademicYears[0].Key;
                            $scope.SelectedAcademicYear.Value = $scope.AcademicYears[0].Value;

                            $scope.GetFeeCollectionHistory();
                        };
                    }
                    else {
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

                $scope.GetFeeCollectionHistory();
            };
            hideOverlay();
        }

    };

    $scope.GetFeeCollectionHistory = function () {

        $scope.ShowPreLoader = true;

        showOverlay();

        //if ($scope.SelectedSchool)

        var schoolID = $scope.SelectedSchool.Key;
        var academicYearID = $scope.SelectedAcademicYear.Key;

        $.ajax({
            type: "GET",
            url: utility.myHost + "/Fee/GetFeeCollectionHistories?schoolID=" + schoolID + "&academicYearID=" + academicYearID,
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

    $scope.DuePaymentClick = function () {

        window.location.replace(utility.myHost + "/Fee/FeePayment");
    };

    $scope.RetryPaymentTransaction = function (model) {

        showOverlay();
        if (model.Amount > 0) {
            var transactionNo = model.TransactionNumber;

            var url = utility.myHost + "/Fee/CheckFeeCollectionExistingStatus?transactionNumber=" + transactionNo;

            $http({
                url: url,
                method: "GET",
            }).then(function (result) {

                if (!result.data.IsError) {
                    $scope.RetryPayment(transactionNo);
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

    $scope.RetryPayment = function (transactionNo) {

        var url = utility.myHost + "/PaymentGateway/RetryPayment?transactionNumber=" + transactionNo;

        $http({
            url: url,
            method: "POST",
        }).then(function (result) {

            if (!result.data.IsError && result.data.Response !== null) {
                window.location.replace(utility.myHost + "/PaymentGateway/Initiate");
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

            var url = utility.myHost + "/Fee/ResendMailReceipt?transactionNumber=" + $scope.SelectedTransactionDetails.TransactionNumber + "&mailID=" + $scope.SelectedTransactionDetails.ParentEmailID;

            $http({
                url: url,
                method: "POST",
            }).then(function (result) {

                if (result.data) {
                    hideOverlay();
                    $('#MailConformationModal').modal('hide');
                    callToasterPlugin('success', "Mail sended successfully!");
                }
                else {
                    hideOverlay();
                    $('#MailConformationModal').modal('hide');
                    callToasterPlugin('error', "Mail sending failed!");
                }

            });
        }
        else {
            hideOverlay();
            $('#MailConformationModal').modal('hide');
            callToasterPlugin('error', "Unable to send mail in this time, try again after some time!");
        }
        
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
    }
    $scope.OpenPopup = function () {
        $("#ItemPopup").fadeIn("fast");
        $(".gridItemOverlay").show();
    }
    $scope.SubmitPopup = function () {
        $("#ItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    }

}]);