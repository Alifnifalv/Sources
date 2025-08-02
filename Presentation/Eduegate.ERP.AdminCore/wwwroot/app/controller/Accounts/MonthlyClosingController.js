
app.controller("MonthlyClosingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
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
    $scope.isWizardVisible = false;
    $scope.IsDisabeleDate = false;
    $scope.IsFeeAccountsEmpty = 0;
    $scope.IsFeeAccountsMatching = 0;    
    $scope.Companies = [];
    $scope.SearchCompany = {};

    function showOverlay() {
        $('.preload-overlay', $('#MonthlyClosing')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#MonthlyClosing')).hide();
    }
    $scope.Init = function (window, model) {
        windowContainer = '#' + window;
        $scope.Model = model;
        // $scope.loadStepsWizard();
        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Company&defaultBlank=false'
        }).then(function (result) {
            $scope.Companies = result.data;
            $scope.GetDefaultCompany();
        });
    }
    $scope.GetDefaultCompany = function () {
        $.ajax({
            url: "Account/GetContextDetails",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.contextCompany = result.Response.CompanyID;
                        var filteredCompany = Object.values($scope.Companies).find(function (company) {
                            return company.Key === String($scope.contextCompany);
                        });
                        $scope.SearchCompany.Key = filteredCompany.Key;
                        $scope.SearchCompany.Value = filteredCompany.Value;
                        $scope.GetMonthlyClosingDate();

                    });
                }
            }
        });
    }

    $scope.GetMonthlyClosingDate = function () {
        $.ajax({
            url: "Accounts/MonthlyClosing/GetMonthlyClosingDate?companyID=" + $scope.SearchCompany?.Key,
            type: "GET",
            success: function (result) {
                if (result != null && result.Data.StartDateString != null) {
                    $scope.$apply(function () {

                        $scope.FromDateString = result.Data.StartDateString;
                        $scope.IsDisabeleDate = true;

                    });
                }
                else {
                    $scope.$apply(function () {
                        $scope.FromDateString = null;
                        $scope.IsDisabeleDate = false;
                    });
                }
            }
        });
    }
    $scope.ViewMonthlyClosing = function (PeriodClosingTranHeadIID, type) {
        var windowName = 'MonthlyClosing';
        var viewName = 'Monthly Closing';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + PeriodClosingTranHeadIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
            });
    };

    $scope.ViewFeeDueFeeTypeDetails = function () {

        FillFeeDueGeneral();
    };
    $scope.ViewFeeVSAccountsFeeTypeDetails = function () {

        FillFeevsAccounts();
    };
    $scope.ViewInventoryTransDetails = function () {

        FillInventoryGeneral();
    };
    $scope.ViewStockType = function () {

        FillStock();
    };
    $scope.ViewFeeCancel = function () {

        FillFeeCancelled();
    };
    $scope.ViewAccountsCancel = function () {

        FillAccountCancelled();
    };
    $scope.ViewInventoryCancel = function () {

        FillInventoryCancelled();
    };
    $scope.ViewMismatchFees = function () {

        FillFeeMismatched();
    };
    $scope.ViewMismatchAccounts = function () {

        FillAccountMismatched();
    };
    $scope.ViewAccountGeneral = function () {

        FillAccountsGeneral();
    };

    $scope.CheckDateRanges = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        if (fromDate != undefined && toDate != undefined) {

            if (fromDate > toDate) {
                $().showGlobalMessage($root, $timeout, true, "Please ensure that the 'From Date' is greater than or equal to the 'To Date'.", 3500);
                toDate = null;
                return false;
            }
            ClearAll();
        }
    };
    $scope.CompanyChanges = function (selected) {
        $scope.SearchCompany = selected;
        $scope.GetMonthlyClosingDate();
    };
    ClearAll = function () {

        $scope.FromDateString = null;
        $scope.ToDateString = null;
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
        $scope.GetMonthlyClosingDate();
    };

    FillFeeDueGeneral = function () {
        showOverlay();

        //var fromDate = $scope.CRUDModel.ViewModel.StartDateString;
        //var toDate = $scope.CRUDModel.ViewModel.EndDateString;
        //var companyID = $scope.CRUDModel.ViewModel.Company;
        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;
        if (!companyID) {
            $().showGlobalMessage($root, $timeout, true, "Please select company !");
            hideOverlay();
            return false;
        }
        if (!fromDate) {
            $().showGlobalMessage($root, $timeout, true, "Please select from date !");
            hideOverlay();
            return false;
        }
        if (!toDate) {
            $().showGlobalMessage($root, $timeout, true, "Please select to date !");
            hideOverlay();
            return false;
        }

        const fromDateObj = moment(fromDate, "DD/MM/YYYY");
        const toDateObj = moment(toDate, "DD/MM/YYYY");

        if (fromDateObj.isAfter(toDateObj)) {
            $().showGlobalMessage($root, $timeout, true, "Please ensure that the 'From Date' is greater than or equal to the 'To Date'.", 3500);
            $scope.ToDateString = null;
            hideOverlay();
            return false;
        }

        $scope.isOpenedTab1 = 1;
        $scope.FeeDueGeneral = [];
        // $scope.url = "Accounts/MonthlyClosing/GetFeeGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        var url = "Accounts/MonthlyClosing/GetFeeGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                result.data.forEach(x => {
                    $scope.FeeDueGeneral.push(
                        {
                            IsExpand: false,
                            //FeeTypeID: x.FeeTypeID,
                            FeeTypeName: x.FeeTypeName,
                            ClosingAmount: x.ClosingAmount,
                            //ClosingCredit: x.ClosingCredit,
                            //ClosingDebit: x.ClosingDebit,
                            OpeningAmount: x.OpeningAmount,
                            //OpeningCredit: x.OpeningCredit,
                            //OpeningDebit: x.OpeningDebit,
                            //TransactionAmount: x.TransactionAmount,
                            TransactionCredit: x.TransactionCredit,
                            TransactionDebit: x.TransactionDebit,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridFeeDueFeeCycle: x.MCGridFeeDueFeeCycle != null ? x.MCGridFeeDueFeeCycle.map(y => y) : null

                        }
                    );


                });
                /*  $timeout(function () {*/
                //$scope.FeeDueGeneral = [
                //    {
                //        FeeTypeName: 'Tuition Fee',
                //        OpeningAmount: 1000,
                //        TransactionDebit: 200,
                //        TransactionCredit: 100,
                //        ClosingAmount: 900,
                //        DifferenceAmount: 100,
                //        DifferencePlusKPI: 120,
                //        IsExpand: false,
                //        MCGridFeeDueFeeCycle: []
                //    }
                //];
                //    $scope.isLoading = false;
                //}, 2000);
                //console.log($scope.FeeDueGeneral);

                FillFeevsAccounts();
            }, function () {

            });

    };

    $scope.loadStepsWizard = function () {
        $timeout(() => {

            if ($("#example-basic").data('steps')) {
                $("#example-basic").steps("destroy");
            }

            $("#example-basic").steps({
                headerTag: "h3",
                bodyTag: "section",
                transitionEffect: "slideLeft",
                autoFocus: true,
                stepsOrientation: "vertical",

                onStepChanging: function (event, currentIndex, newIndex) {

                    return true; // Allow moving for other cases
                },
                onStepChanged: function (event, currentIndex, priorIndex) {

                    if (currentIndex === 1) {
                        displayLoadedData();
                    }
                    if (currentIndex === 1 && priorIndex === 2) {
                        displayLoadedData();

                    }
                },
                onFinished: function (event, currentIndex) {

                    $scope.SubmitMothlyClosing();
                    $scope.$apply();
                }
            });
            $scope.isWizardVisible = true;
            $scope.$apply();
            hideOverlay();
        });
    };


    //$("#example-async").steps({
    //    headerTag: "h3",
    //    bodyTag: "section",
    //    transitionEffect: "slide",
    //    onStepChanging: function (event, currentIndex, newIndex) {
    //        // Prevent moving to the next step until data is loaded
    //        if (newIndex > currentIndex) {
    //            // Only handle the specific step where you want to load data
    //            if (currentIndex === 0) { // Example: Step 1 is loading data for Step 2
    //                return loadDataForNextStep().then(() => {
    //                    return true; // Allow moving to the next step after loading
    //                }).catch(() => {
    //                    return false; // Prevent moving to the next step if loading fails
    //                });
    //            }
    //        }
    //        return true; // Allow moving for other cases
    //    },
    //    onStepChanged: function (event, currentIndex, priorIndex) {
    //        if (currentIndex === 1) { // Example: Step 2
    //            displayLoadedData(); // Render or display the loaded data
    //        }
    //    }
    //});

    // Function to load data for FillFeeMismatchedthe next step
    function loadDataForNextStep() {
        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;
        if (!companyID) {
            $().showGlobalMessage($root, $timeout, true, "Please select company !");
            hideOverlay();
            return false;
        }
        if (!fromDate) {
            $().showGlobalMessage($root, $timeout, true, "Please select from date !");
            hideOverlay();
            return false;
        }
        if (!toDate) {
            $().showGlobalMessage($root, $timeout, true, "Please select to date !");
            hideOverlay();
            return false;
        }
        $scope.FeeVSAccounts = [];
        var url = "Accounts/MonthlyClosing/GetFeeAccountCompareMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;;
        return new Promise((resolve, reject) => {
            // Simulate an API call to fetch data

            $.ajax({
                url: url,
                method: "GET",
                success: function (data) {
                    // Store the data for use in the next step
                    window.stepData = data;
                    resolve();
                },
                error: function (error) {
                    console.error("Error loading data:", error);
                    reject();
                }
            });
        });
    }

    // Function to display loaded data
    function displayLoadedData() {
        /*const dataContainer = $("#step-2-container"); // Replace with your step's container ID*/
        $scope.$apply(() => {
            $scope.FeeVSAccounts = window.stepData;
        });

        // Render the data into the step
        //dataContainer.empty(); // Clear existing content
        //data.forEach(item => {
        //    dataContainer.append(`<div>${item.name}</div>`); // Example rendering logic
        //});
    }
    FillFeevsAccounts = function () {
        //showOverlay();

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;
        //if (!companyID) {
        //     $().showGlobalMessage($root, $timeout, true, "Please select company !");
        //    hideOverlay();
        //    return false;
        //}
        //if (!fromDate) {
        //     $().showGlobalMessage($root, $timeout, true, "Please select from date !");
        //    hideOverlay();
        //    return false;
        //}
        //if (!toDate) {
        //     $().showGlobalMessage($root, $timeout, true, "Please select to date !");
        //    hideOverlay();
        //    return false;
        //}
        $scope.FeeVSAccounts = [];
        $scope.isOpenedTab2 = 1;
        var url = "Accounts/MonthlyClosing/GetFeeAccountCompareMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;;
        $http({ method: 'Get', url: url })
            .then(function (result1) {

                result1.data.forEach(x => {
                    $scope.FeeVSAccounts.push(
                        {
                            IsExpand: false,
                            FeeTypeID: x.FeeTypeID,
                            FeeTypeName: x.FeeTypeName,
                            FeeAmount: x.FeeAmount,
                            AccountAmount: x.AccountAmount,
                            DifferenceAmount: x.DifferenceAmount,
                            DifferencePlusKPI: x.DifferencePlusKPI,
                            MCGridFeeVSAccountsFeeCycle: x.MCGridFeeVSAccountsFeeCycle != null ? x.MCGridFeeVSAccountsFeeCycle.map(y => y) : null

                        }
                    );

                });


                if ($scope.FeeVSAccounts == undefined || $scope.FeeVSAccounts.length == 0) {
                    $scope.IsFeeAccountsEmpty = 1;
                }

                $scope.FeeVSAccounts.forEach(element => {
                    if (
                        element.FeeAmount !== undefined && element.FeeAmount !== null && element.FeeAmount !== 0 &&
                        element.AccountAmount !== undefined && element.AccountAmount !== null &&
                        element.FeeAmount !== element.AccountAmount
                    ) {
                        $scope.IsFeeAccountsMatching = 1;
                    }
                });
                FillInventoryGeneral();

            }, function () {

            });
        //$scope.FeeVSAccounts = [
        //    {
        //        FeeTypeName: 'Tuition Fee',
        //        FeeAmount: 1000,
        //        AccountAmount: 200,
        //        TransactionCredit: 100,
        //        ClosingAmount: 900,
        //        DifferenceAmount: 100,
        //        DifferencePlusKPI: 120,
        //        IsExpand: false,
        //        MCGridFeeDueFeeCycle: []
        //    }
        //];


    };

    FillFeeCancelled = function () {
        //showOverlay();
        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;
        //if (!companyID) {
        //     $().showGlobalMessage($root, $timeout, true, "Please select company !");
        //    hideOverlay();
        //    return false;
        //}
        //if (!fromDate) {
        //     $().showGlobalMessage($root, $timeout, true, "Please select from date !");
        //    hideOverlay();
        //    return false;
        //}
        //if (!toDate) {
        //     $().showGlobalMessage($root, $timeout, true, "Please select to date !");
        //    hideOverlay();
        //    return false;
        //}
        $scope.FeeCancelled = [];
        $scope.isOpenedTab5 = 1;
        var url = "Accounts/MonthlyClosing/GetFeeCancelMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.FeeCancelled.push(
                        {
                            IsExpand: false,
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

                FillAccountCancelled();
            }, function () {

            });
        hideOverlay();
    };

    FillInventoryGeneral = function () {
        //showOverlay();
        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;

        $scope.InventoryGeneral = [];
        $scope.isOpenedTab3 = 1;
        var url = "Accounts/MonthlyClosing/GetInventoryGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.InventoryGeneral.push(
                        {
                            IsExpand: false,
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

                FillStock();
            }, function () {

            });

    };

    FillInventoryCancelled = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;
        $scope.InventoryCancelled = [];
        $scope.isOpenedTab7 = 1;
        var url = "Accounts/MonthlyClosing/GetInventoryCancelMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.InventoryCancelled.push(
                        {
                            IsExpand: false,
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
                FillFeeMismatched();

            }, function () {

            });

    };

    FillAccountCancelled = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;

        $scope.AccountsCancelled = [];
        $scope.isOpenedTab6 = 1;
        var url = "Accounts/MonthlyClosing/GetAccountCancelMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.AccountsCancelled.push(
                        {
                            IsExpand: false,
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
                console.log('AccountsCancelled');
                console.log($scope.AccountsCancelled);
                FillInventoryCancelled();
            }, function () {

            });

    };

    FillStock = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;
        $scope.Stock = [];
        $scope.isOpenedTab4 = 1;
        var url = "Accounts/MonthlyClosing/GetStockMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.Stock.push(
                        {
                            IsExpand: false,
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

                FillFeeCancelled();
            }, function () {

            });
    };

    FillFeeMismatched = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;

        $scope.FeeMismatched = [];
        $scope.isOpenedTab8 = 1;
        var url = "Accounts/MonthlyClosing/GetFeeMismatchedMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
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
                FillAccountMismatched();

            }, function () {

            });
    };

    FillAccountMismatched = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;

        $scope.AccountMismatched = [];
        $scope.isOpenedTab9 = 1;
        var url = "Accounts/MonthlyClosing/GetAccountsMismatchedMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;
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
                FillAccountsGeneral();
            }, function () {

            });

    };

    FillAccountsGeneral = function () {

        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;

        $scope.AccountsGeneral = [];
        $scope.isOpenedTab10 = 1;
        var url = "Accounts/MonthlyClosing/GetAccountsGeneralMonthlyClosing?dateFromString=" + fromDate + "&dateToString=" + toDate + "&companyID=" + companyID;;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                result.data.forEach(x => {
                    $scope.AccountsGeneral.push(
                        {
                            IsExpand: false,
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

                console.log('AccountsGeneral');
                console.log($scope.AccountsGeneral);
                $scope.loadStepsWizard();

            }, function () {

            });

    };


    $scope.SubmitMothlyClosing = function () {
        showOverlay();
        var fromDate = $scope.FromDateString;
        var toDate = $scope.ToDateString;
        var companyID = $scope.SearchCompany?.Key;

        if ($scope.IsFeeAccountsEmpty == 1) {           
            $().showGlobalMessage($root, $timeout, true, "Fill Fee & Accounts Amounts.Please check Tab Fee V/s Accounts!");
            hideOverlay();
            return false;
        }


        if ($scope.IsFeeAccountsMatching == 1) {
            $().showGlobalMessage($root, $timeout, true, "Fee & Accounts Amounts are not equal. Please check Tab Fee V/s Accounts!");
            hideOverlay();
            return false;
        }


        $.ajax({

            url: "Accounts/MonthlyClosing/SubmitMonthlyClosing",
            type: "POST",
            data: {

                "dateFromString": fromDate,
                "dateToString": toDate,
                "companyID": companyID

            },
            success: function (result) {
                if (result != null) {
                    // alert(result.Message);
                    $().showGlobalMessage($root, $timeout, !result.IsSuccess, result.Message);
                    hideOverlay();
                    ClearAll();
                }
            },
            complete: function (result) {


            }
        });
        hideOverlay();
    }


}]);

