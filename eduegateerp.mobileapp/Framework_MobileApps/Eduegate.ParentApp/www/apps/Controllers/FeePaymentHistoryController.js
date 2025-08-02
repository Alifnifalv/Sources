app.controller('FeePaymentHistoryController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', '$timeout', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce, $timeout) {
    console.log('FeePaymentHistoryController loaded.');
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    $scope.Schools = [];

    $scope.init = function () {
        $rootScope.ShowPreLoader = true;
        $rootScope.ShowLoader = true;

        $scope.GetSchools();
    }

    $scope.GetSchools = function () {

        $scope.SelectedSchool = {};
        $scope.Schools = [];

        $http({
            method: 'GET',
            url: dataService + '/GetSchoolsByParent',
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            // $timeout(function() {
            //     $scope.$apply(function() {
            //         $scope.Schools = result;
            //     });
            // });
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

            if ($scope.Schools.length > 0) {
                if ($scope.Schools[0].Key != null) {
                    $scope.SelectedSchool.Key = $scope.Schools[0].Key;
                    $scope.SelectedSchool.Value = $scope.Schools[0].Value;

                    $scope.SchoolChanges($scope.SelectedSchool.Key);
                };
            }

            // $rootScope.ShowPreLoader = false;
            // $rootScope.ShowLoader = false;
        }).error(function () {
            $rootScope.ShowPreLoader = false;
            $rootScope.ShowLoader = false;
        });
    }

    $scope.SchoolChanges = function () {

        $scope.SelectedAcademicYear = {};
        $scope.AcademicYears = [];

        var schoolID = $scope.SelectedSchool?.Key;

        $scope.AcademicYears.push({
            "Key": 0,
            "Value": "Current"
        });

        $http({
            method: 'GET',
            url: dataService + '/GetAcademicYearBySchool?schoolID=' + schoolID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {

            var academicYearDatas = result;

            if (academicYearDatas.length > 0) {
                academicYearDatas.forEach(x => {
                    $scope.AcademicYears.push({
                        "Key": x.Key,
                        "Value": x.Value
                    });
                });
            }

            if ($scope.AcademicYears.length > 0) {
                if ($scope.AcademicYears[0].Key != null) {
                    $scope.SelectedAcademicYear.Key = $scope.AcademicYears[0].Key;
                    $scope.SelectedAcademicYear.Value = $scope.AcademicYears[0].Value;

                    $scope.GetFeeCollectionHistories();
                };
            }

            // $rootScope.ShowPreLoader = false;
            // $rootScope.ShowLoader = false;
        }).error(function () {
            $rootScope.ShowPreLoader = false;
            $rootScope.ShowLoader = false;
        });
    }

    $scope.GetFeeCollectionHistories = function () {

        $scope.FeeCollectionHistories = [];
        var schoolID = $scope.SelectedSchool.Key;
        var academicYearID = $scope.SelectedAcademicYear.Key;

        $http({
            method: 'GET',
            url: dataService + '/GetFeeCollectionHistories?schoolID=' + schoolID + "&academicYearID=" + academicYearID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {

            // $scope.$apply(function () {
            $scope.FeeCollectionHistories = result;
            // });

            $rootScope.ShowPreLoader = false;
            $rootScope.ShowLoader = false;
        }).error(function () {
            $rootScope.ShowPreLoader = false;
            $rootScope.ShowLoader = false;
        });
    }

    $scope.ResendMail = function (model) {

        document.getElementById("mailsendbutton").disabled = false;

        $scope.SelectedTransactionDetails = model;
        $('#MailConformationModal').modal('show');
    };

    $scope.SendMailReceipt = function () {

        document.getElementById("mailsendbutton").disabled = true;

        if ($scope.SelectedTransactionDetails.GroupTransactionNumber) {

            var url = dataService + "/ResendReceiptMail?transactionNumber=" + $scope.SelectedTransactionDetails.GroupTransactionNumber + "&mailID=" + $scope.SelectedTransactionDetails.EmailID;

            $http({
                url: url,
                method: "POST",
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).success(function (result) {

                if (result.operationResult == 1) {
                    $rootScope.ShowToastMessage("Mail sent successfully!", 'success');
                }
                else {
                    $rootScope.ShowToastMessage("Mail sending failed!", 'error');
                }

                $('#MailConformationModal').modal('hide');

            }).error(function () {

                $('#MailConformationModal').modal('hide');

                $rootScope.ShowToastMessage("Mail sending failed!", 'error');
            });

        }
        else {

            $('#MailConformationModal').modal('hide');

            $rootScope.ShowToastMessage("Unable to send mail in this time, try again after some time!", 'error');
        }
    };

    $scope.RetryPaymentTransaction = function (model) {

        $rootScope.ShowPreLoader = true;
        $rootScope.ShowLoader = true;

        if (model.Amount > 0) {

            var transactionNo = model.GroupTransactionNumber;

            $http({
                method: 'GET',
                url: dataService + "CheckFeeCollectionExistingStatus?transactionNumber=" + transactionNo,
                headers: {
                    "Accept": "application/json;charset=UTF-8",
                    "Content-type": "application/json; charset=utf-8",
                    "CallContext": JSON.stringify(context)
                }
            }).success(function (result) {

                if (result.operationResult == 1) {

                    $scope.RetryPayment(transactionNo);
                }
                else {
                    $rootScope.ShowPreLoader = false;
                    $rootScope.ShowLoader = false;

                    $rootScope.ShowToastMessage("Fee already paid for the same month or type!", 'error');
                }

            }).error(function () {
                $rootScope.ShowPreLoader = false;
                $rootScope.ShowLoader = false;

                $rootScope.ShowToastMessage("Unable to retry transaction in this time, try again later!", 'error');
            });

        }
        else {
            $rootScope.ShowPreLoader = false;
            $rootScope.ShowLoader = false;
            $rootScope.ShowToastMessage("An amount greater than zero is required to retry payment!", 'error');
        }
    };

    $scope.RetryPayment = function (transactionNo) {

        $http({
            method: 'POST',
            url: dataService + "RetryPayment?transactionNumber=" + transactionNo,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {

            if (result.operationResult == 1) {

                $state.go("initiatefeepayment");
            }
            else {
                $rootScope.ShowPreLoader = false;
                $rootScope.ShowLoader = false;

                $rootScope.ShowToastMessage("Fee already paid for the same month or type!", 'error');
            }

        }).error(function () {
            $rootScope.ShowPreLoader = false;
            $rootScope.ShowLoader = false;

            $rootScope.ShowToastMessage("Unable to retry transaction in this time, try again later!", 'error');
        });
    };

    $scope.TransactionDetailClick = function (transactionNumber) {
        $state.go("feepaymenthistorydetails", { transactionNumber: transactionNumber });
    }

}]);