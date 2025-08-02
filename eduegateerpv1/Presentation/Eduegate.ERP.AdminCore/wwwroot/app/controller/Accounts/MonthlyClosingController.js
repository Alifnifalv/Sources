
app.controller("MonthlyClosingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("MonthlyClosingController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });
    $scope.FeeDueGeneral = [];
    $scope.FeeVSAccounts = [];
    $scope.InventoryGeneral = [];
    $scope.FeeCancelled = [];
    $scope.InventoryCancelled = [];
    $scope.AccountsCancelled = [];
    $scope.Stock = [];
    $scope.FeeMismatched = [];
    $scope.AccountsGeneral = [];
    $scope.AccountMismatched = [];

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.ViewMothlyClosingDetails = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;
        if (!fromDate) {
            $().showMessage($scope, $timeout, true, "Please select from date !");
        
            return false;
        }
        if (!toDate) {
            $().showMessage($scope, $timeout, true, "Please select to date !");
        }

        if ($scope.CRUDModel.ViewModel.StartDateString > $scope.CRUDModel.ViewModel.EndDateString) {
            $().showMessage($scope, $timeout, true, "Please ensure that the 'From Date' is greater than or equal to the 'To Date'.", 3500);
            $scope.CRUDModel.ViewModel.EndDateString = null;
            return false;
        }
        showOverlay();
        FillFeeDueGeneral();
        FillFeevsAccounts();
        FillInventoryGeneral();
        FillStock();
        FillFeeCancelled();
        FillAccountCancelled();
        FillInventoryCancelled();
        FillAccountsGeneral();
        FillFeeMismatched();
        FillAccountMismatched();
        hideOverlay();


    };

    $scope.CheckDateRanges = function () {
        if ($scope.CRUDModel.ViewModel.StartDateString != undefined && $scope.CRUDModel.ViewModel.EndDateString != undefined) {
            if ($scope.CRUDModel.ViewModel.StartDateString > $scope.CRUDModel.ViewModel.EndDateString) {
                $().showMessage($root, $timeout, true, "Please ensure that the 'From Date' is greater than or equal to the 'To Date'.", 3500);
                $scope.CRUDModel.ViewModel.EndDateString = null;
                return false;
            }
            ClearAll();
        }
       
    };

    ClearAll = function () {
        $scope.FeeDueGeneral = [];
        $scope.FeeVSAccounts = [];
        $scope.InventoryGeneral = [];
        $scope.FeeCancelled = [];
        $scope.InventoryCancelled = [];
        $scope.AccountsCancelled = [];
        $scope.Stock = [];
        $scope.FeeMismatched = [];
        $scope.AccountsGeneral = [];
        $scope.AccountMismatched = [];
        $scope.CRUDModel.ViewModel.MCTabFeeDueGeneral.MCGridFeeDueFeeType = $scope.FeeDueGeneral;
        $scope.CRUDModel.ViewModel.MCTabFeeVSAccounts.MCGridFeeVSAccountsFeeType = $scope.FeeVSAccounts;
        $scope.CRUDModel.ViewModel.MCTabFeeCancelled.MCGridFeeCancelFeeType = $scope.FeeCancelled;
        $scope.CRUDModel.ViewModel.MCTabInventory.MCGridInventoryTransType = $scope.InventoryGeneral;
        $scope.CRUDModel.ViewModel.MCTabInventoryCancelled.MCGridInventoryCancel = $scope.InventoryCancelled;
        $scope.CRUDModel.ViewModel.MCTabAccountsCancelled.MCGridAccountCancel = $scope.AccountsCancelled;
        $scope.CRUDModel.ViewModel.MCTabStock.MCGridStockType = $scope.Stock;
        $scope.CRUDModel.ViewModel.MCTabMisMatchedFees.MCGridMismatchFees = $scope.FeeMismatched;
        $scope.CRUDModel.ViewModel.MCTabMisMatchedAccounts.MCGridMismatchAccounts = $scope.AccountMismatched;
        $scope.CRUDModel.ViewModel.MCTabFeeDueGeneral.MCGridAccountGeneralMainGroup = $scope.AccountsGeneral;
    };

    FillFeeDueGeneral = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;

        $scope.FeeDueGeneral = [];
        var url = "Accounts/MonthlyClosing/GetFeeGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.FeeDueGeneral.push(
                        {
                            FeeTypeID: x.FeeTypeID,
                            FeeTypeName: x.FeeTypeName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridFeeDueFeeCycle: x.MCGridFeeDueFeeCycle != null ? x.MCGridFeeDueFeeCycle.map(y => y) : null

                        }
                    );

                });

                $scope.CRUDModel.ViewModel.MCTabFeeDueGeneral.MCGridFeeDueFeeType = $scope.FeeDueGeneral;


            }, function () {

            });

    };

    FillFeevsAccounts = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;

        $scope.FeeVSAccounts = [];
        var url = "Accounts/MonthlyClosing/GetFeeAccountCompareMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result1) {

                result1.data.forEach(x => {
                    $scope.FeeVSAccounts.push(
                        {
                            FeeTypeID: x.FeeTypeID,
                            FeeTypeName: x.FeeTypeName,
                            FeeAmount: x.FeeAmount,
                            AccountAmount: x.AccountAmount,
                            ClosingDebit: x.ClosingDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridFeeVSAccountsFeeCycle: x.MCGridFeeVSAccountsFeeCycle != null ? x.MCGridFeeVSAccountsFeeCycle.map(y => y) : null

                        }
                    );

                });

                $scope.CRUDModel.ViewModel.MCTabFeeVSAccounts.MCGridFeeVSAccountsFeeType = $scope.FeeVSAccounts;


            }, function () {

            });
    };

    FillFeeCancelled = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;

        $scope.FeeCancelled = [];
        var url = "Accounts/MonthlyClosing/GetFeeCancelMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.FeeCancelled.push(
                        {
                            FeeTypeID: x.FeeTypeID,
                            FeeTypeName: x.FeeTypeName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridFeeDueFeeCycle: x.MCGridFeeDueFeeCycle != null ? x.MCGridFeeDueFeeCycle.map(y => y) : null

                        }
                    );

                });

                $scope.CRUDModel.ViewModel.MCTabFeeCancelled.MCGridFeeCancelFeeType = $scope.FeeCancelled;


            }, function () {

            });
    };

    FillInventoryGeneral = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;


        $scope.InventoryGeneral = [];
        var url = "Accounts/MonthlyClosing/GetInventoryGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.InventoryGeneral.push(
                        {
                            TransactionTypeID: x.TransactionTypeID,
                            TransactionTypeName: x.TransactionTypeName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridInventoryDocumentType: x.MCGridInventoryDocumentType != null ? x.MCGridInventoryDocumentType.map(y => y) : null

                        }
                    );

                });
                $scope.CRUDModel.ViewModel.MCTabInventory.MCGridInventoryTransType = $scope.InventoryGeneral;

            }, function () {

            });

    };

    FillInventoryCancelled = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;

        $scope.InventoryCancelled = [];
        var url = "Accounts/MonthlyClosing/GetInventoryCancelMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.InventoryCancelled.push(
                        {
                            TransactionTypeID: x.TransactionTypeID,
                            TransactionTypeName: x.TransactionTypeName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridInventoryDocumentType: x.MCGridInventoryDocumentType != null ? x.MCGridInventoryDocumentType.map(y => y) : null

                        }
                    );

                });

                $scope.CRUDModel.ViewModel.MCTabInventoryCancelled.MCGridInventoryCancel = $scope.InventoryCancelled;

            }, function () {

            });

    };

    FillAccountCancelled = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;

        var url = "Accounts/MonthlyClosing/GetAccountCancelMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.AccountsCancelled.push(
                        {
                            TransactionTypeID: x.TransactionTypeID,
                            TransactionTypeName: x.TransactionTypeName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridInventoryDocumentType: x.MCGridInventoryDocumentType != null ? x.MCGridInventoryDocumentType.map(y => y) : null

                        }
                    );

                });
                $scope.CRUDModel.ViewModel.MCTabAccountsCancelled.MCGridAccountCancel = $scope.AccountsCancelled;

            }, function () {

            });

    };

    FillStock = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;

        $scope.Stock = [];
        var url = "Accounts/MonthlyClosing/GetStockMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.Stock.push(
                        {
                            TypeID: x.TypeID,
                            TypeName: x.TypeName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridStockAccount: x.MCGridStockAccount != null ? x.MCGridStockAccount.map(y => y) : null

                        }
                    );

                });
                $scope.CRUDModel.ViewModel.MCTabStock.MCGridStockType = $scope.Stock;

            }, function () {

            });

    };

    FillFeeMismatched = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;


        $scope.FeeMismatched = [];
        var url = "Accounts/MonthlyClosing/GetFeeMismatchedMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.FeeMismatched.push(
                        {
                            FeeTypeID: x.FeeTypeID,
                            FeeName: x.FeeName,
                            Remarks: x.Remarks,
                            InvoiceNo: x.InvoiceNo,
                            Amount: x.Amount,
                            FeeTypeName: x.FeeTypeName,
                            SchoolName: x.SchoolName,
                            StudentName: x.StudentName,
                            AdmissionNumber: x.AdmissionNumber,
                            InvoiceDateString: x.InvoiceDateString
                        }
                    );

                });

                $scope.CRUDModel.ViewModel.MCTabMisMatchedFees.MCGridMismatchFees = $scope.FeeMismatched;


            }, function () {

            });
    };

    FillAccountMismatched = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;
        $scope.AccountMismatched = [];
        var url = "Accounts/MonthlyClosing/GetAccountsMismatchedMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.AccountMismatched.push(
                        {
                            TranTypeName: x.TranTypeName,
                            DocumentTypeName: x.DocumentTypeName,
                            Branch: x.Branch,
                            TranNo: x.TranNo,
                            Narration: x.Narration,
                            VoucherNo: x.VoucherNo,
                            Remarks: x.Remarks,
                            Amount: x.Amount,
                            TranDateString: x.TranDateString
                        }
                    );
                });

                $scope.CRUDModel.ViewModel.MCTabMisMatchedAccounts.MCGridMismatchAccounts = $scope.AccountMismatched;


            }, function () {

            });
    };

    FillAccountsGeneral = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;


        $scope.AccountsGeneral = [];
        var url = "Accounts/MonthlyClosing/GetAccountsGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.AccountsGeneral.push(
                        {
                            MainGroupID: x.MainGroupID,
                            MainGroupName: x.MainGroupName,
                            ClosingAmount: x.ClosingAmount,
                            ClosingCredit: x.ClosingCredit,
                            ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            OpeningCredit: x.OpeningCredit,
                            OpeningDebit: x.OpeningDebit,
                            TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridAccountGeneralSubGroup: x.MCGridAccountGeneralSubGroup != null ? x.MCGridAccountGeneralSubGroup.map(y => y) : null

                        }
                    );

                });

                $scope.CRUDModel.ViewModel.MCTabFeeDueGeneral.MCGridAccountGeneralMainGroup = $scope.AccountsGeneral;


            }, function () {

            });

    };


    $scope.SubmitMothlyClosing = function () {

        var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        var toDate = $scope.CRUDModel.ViewModel.EndDateString;
        if (!fromDate) {

            $().showMessage($scope, $timeout, true, "Please select from date !");
            return false;
        }
        if (!toDate) {
            $().showMessage($scope, $timeout, true, "Please select to date !");
            return false;
        }
        if ($scope.FeeVSAccounts == undefined || $scope.FeeVSAccounts.length == 0) {
            $().showMessage($scope, $timeout, true, "Fill Fee & Accounts Amounts.Please check Tab Fee V/s Accounts!");
            return false;
        }
        $scope.FeeVSAccounts.forEach(element => {
            if (element.FeeAmount != element.AccountAmount) {
                $().showMessage($scope, $timeout, true, "Fee & Accounts Amounts are not equal.Please check Tab Fee V/s Accounts!");
                return false;
            }
        });

        $.ajax({

            url: "Accounts/MonthlyClosing/SubmitMonthlyClosing",
            type: "POST",
            data: {

                "dateFromString": fromDate,
                "dateToString": toDate

            },
            success: function (result) {
                if (result != null) {
                    $().showMessage($scope, $timeout, !result.IsSuccess, result.Message);
                    ClearAll();
                }
            },
            complete: function (result) {


            }
        });

    }


}]);

