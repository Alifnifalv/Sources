app.controller("BudgetingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("BudgetingController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.Budgets = [];
    $scope.AccountGroups = [];
    $scope.BudgetTypes = [];
    $scope.BudgetSuggestions = [];
    $scope.CostCenters = [];

    $scope.SearchMastersData = {
        "Budget": { "Key": null, "Value": null },
        "BudgetType": { "Key": null, "Value": null },
        "BudgetSuggestion": { "Key": null, "Value": null },
        "AccountGroup": { "Key": null, "Value": null },
        "CostCenter": { "Key": null, "Value": null },
        "FromDateString": null,
        "ToDateString": null,
        "SuggestedFromDateString": null,
        "SuggestedToDateString": null,
        "AccountGroupList": [],
        "PercentageValue": 0,
    };

    $scope.BudgetEntries = [];

    function showOverlay() {
        $('.preload-overlay', $('#Budgeting')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#Budgeting')).hide();
    }

    $scope.init = function (model, windowname) {

        $scope.BudgetingModel = model;
        $scope.IsEditScreen = model.IsEdit;

        $scope.OldHeaderTitles = $scope.BudgetingModel.HeaderTitles;
        $scope.OldBudgetModel = $scope.BudgetingModel;

        //Budgets
        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Budgets&defaultBlank=false'
        }).then(function (result) {
            $scope.Budgets = result.data;

            if ($scope.BudgetingModel.BudgetID != null && $scope.BudgetingModel.BudgetID != 0) {

                var budgetFilter = $scope.Budgets.length > 0 ? $scope.Budgets.find(x => x.Key == $scope.BudgetingModel.BudgetID) : null;

                if (budgetFilter) {
                    $scope.SearchMastersData.Budget.Key = budgetFilter.Key;
                    $scope.SearchMastersData.Budget.Value = budgetFilter.Value;

                    $scope.FillBudgetEntrybyBudgetID($scope.SearchMastersData.Budget.Key);
                }
            }
        });

        //AccountGroup
        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=AccountGroup&defaultBlank=false'
        }).then(function (result) {
            $scope.AccountGroups = result.data;
        });

        //Account
        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Accounts&defaultBlank=false'
        }).then(function (result) {
            $scope.Accounts = result.data;
            $scope.GroupAccounts = result.data;
        });

        //BudgetTypes
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=BudgetTypes&defaultBlank=false",
        }).then(function (result) {
            $scope.BudgetTypes = result.data;

            $scope.GetBudgetTypeSettingValues();
        });

        //BudgetSuggestions
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=BudgetSuggestions&defaultBlank=false",
        }).then(function (result) {
            $scope.BudgetSuggestions = result.data;
        });

        //Week Days
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=CostCenterDetails&defaultBlank=false",
        }).then(function (result) {
            $scope.CostCenters = result.data;
        });
        //---End: Fetching masters data on load.        

        $scope.LoadEntryGrid();
    };

    $scope.GetBudgetTypeSettingValues = function () {

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "BUDGET_TYPE_MONTHLY",
        }).then(function (result) {
            $scope.MonthlyBudgetTypeID = result.data;

        }).then(function () {
            $scope.FillDefaultBudgetType();
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "BUDGET_TYPE_YEARLY",
        }).then(function (result) {
            $scope.YearlyBudgetTypeID = result.data;
        });
    };

    $scope.FillDefaultBudgetType = function () {

        if ($scope.BudgetTypes.length > 0) {

            var type = $scope.BudgetTypes.find(x => x.Key == $scope.MonthlyBudgetTypeID);

            if (type != null) {
                $scope.SearchMastersData.BudgetType = {
                    "Key": type.Key,
                    "Value": type.Value
                };
            }
        }
    };

    $scope.GetBudgetEntries = function () {

        $scope.BudgetingData = [];
        $scope.AmountTitles = [];

        $.ajax({

            url: "Accounts/Budgeting/GetBudgetEntries",
            type: "GET",
            success: function (result) {
                if (!result.IsError) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.BudgetEntries = result.Response;

                        });
                    });
                }
            },
            complete: function (result) {

                hideOverlay();
            }
        });
    };

    //#region Entry row codes
    $scope.InsertRow = function (index) {

        var model = $scope.BudgetEntries;
        var row = $scope.BudgetingGrid[0];

        model.splice(index + 1, 0, angular.copy(row));
    };

    $scope.RemoveRow = function (index) {

        if ($scope.BudgetEntries.length == 1) {
            if (index == 1) {
                $scope.LoadEntryGrid();
            }
        }
        else {

            var model = $scope.BudgetEntries;
            var row = $scope.BudgetingGrid[0];

            model.splice(index, 1);

            if (index === 0) {
                $scope.InsertRow(index, row, model);
            }
        }
    };

    $scope.LoadEntryGrid = function () {

        $scope.BudgetingModel = $scope.OldBudgetModel;

        $scope.BudgetEntries = [];

        $scope.BudgetingGrid = [];

        $scope.BudgetEntries.push(JSON.parse(JSON.stringify($scope.BudgetingModel)));

        $scope.BudgetingGrid = JSON.parse(JSON.stringify($scope.BudgetEntries));
    };

    $scope.ClearEntryGrid = function () {

        $scope.BudgetingModel = $scope.OldBudgetModel;

        $scope.BudgetingModel.HeaderTitles = $scope.OldHeaderTitles;

        $scope.BudgetEntries = [];

        $scope.BudgetingGrid = [];

        $scope.BudgetEntries.push(JSON.parse(JSON.stringify($scope.BudgetingModel)));

        $scope.BudgetingGrid = JSON.parse(JSON.stringify($scope.BudgetEntries));
    };
    //#endregion Entry row codes end

    $scope.ChangeRowEntryDate = function (row) {
        if (!row.EndDateString) {
            if (row.StartDateString) {
                row.EndDateString = row.StartDateString;
            }
        }

        $scope.CheckRowEntryDateRanges(row);
    };

    $scope.CheckRowEntryDateRanges = function (row) {
        if (row.StartDateString && row.EndDateString) {
            if (row.StartDateString > row.EndDateString) {
                $().showGlobalMessage($root, $timeout, true, "Please ensure that the 'date to' is greater than or equal to the 'date from'.", 3500);
                row.EndDateString = null;
                return false;
            }
        }
    };

    $scope.AccountGroupChanges = function (row) {

        row.BudgetingAccountDetail.GroupDefaultSide = null;
        row.BudgetingAccountDetail.Account = {};

        var accountGroupID = row.BudgetingAccountDetail?.AccountGroup?.Key;
        if (accountGroupID) {
            showOverlay();

            //var url = utility.myHost + "Schools/School/GetAccountByGroupID?groupID=" + accountGroupID;
            //$http({ method: 'Get', url: url })
            //    .then(function (result) {
            //        $scope.GroupAccounts = result.data;

            //    }, function () {

            //    });

            var url2 = utility.myHost + "Schools/School/GetAccountGroupDataByID?groupID=" + accountGroupID;
            $http({ method: 'Get', url: url2 })
                .then(function (result) {
                    row.BudgetingAccountDetail.GroupDefaultSide = result.data.Default_Side;

                }, function () {

                });

            hideOverlay();
        }
        else {
            $scope.GroupAccounts = $scope.Accounts;

            hideOverlay();
        }
    };

    $scope.AccountChanges = function (budget) {

        var selectedAccount = budget.BudgetingAccountDetail?.Account;
        var isDuplicate = checkForDuplicateAccount(selectedAccount, budget);
        if (isDuplicate) {
            alert('This account has already been selected in another entry.');
            budget.BudgetingAccountDetail.Account = {};
            return false;

        }
    };

    function checkForDuplicateAccount(selectedAccount, currentBudget) {
        var isDuplicate = false;


        for (var i = 0; i < $scope.BudgetEntries.length; i++) {

            if (currentBudget === $scope.BudgetEntries[i]) continue;


            if (selectedAccount.Key === $scope.BudgetEntries[i].BudgetingAccountDetail.Account.Key) {
                isDuplicate = true;
                break;
            }
        }

        return isDuplicate;
    }



    $scope.SaveBudgetingEntries = function () {

        if (!$scope.SearchMastersData.Budget.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select a Budget!");
            return false;
        }
        if ($scope.BudgetEntries.length > 0) {

            var BudgetEntriesForSaving = [];

            var isError = false;
            var errorMessage = "";
            var messageTimeSec = 1000;

            $scope.BudgetEntries.forEach(entry => {
                messageTimeSec = 1000;
                var budgetAllocations = entry.Allocations.filter(x => x.Amount != null);
                if (budgetAllocations.length != 0) {
                    entry.Budget = $scope.SearchMastersData.Budget;
                    entry.BudgetType = $scope.SearchMastersData.BudgetType;
                    entry.BudgetSuggestion = $scope.SearchMastersData.BudgetSuggestion;
                    entry.AccountGroup = $scope.SearchMastersData.AccountGroup;
                    entry.CostCenter = $scope.SearchMastersData.CostCenter;
                    entry.FromDateString = $scope.SearchMastersData.FromDateString;
                    entry.ToDateString = $scope.SearchMastersData.ToDateString;

                    var totalMonthAmount = entry.Allocations.reduce((total, alloc) => {
                        // Check if alloc.Amount is not null and not NaN, otherwise treat it as 0
                        const amount = alloc.Amount !== null && !isNaN(alloc.Amount) ? parseFloat(alloc.Amount) : 0;
                        return total + amount;
                    }, 0);

                    if (!entry.BudgetingAccountDetail.AccountGroup.Key) {
                        isError = true;
                        errorMessage = "An account group in the grid is required!";
                        messageTimeSec = 3000;
                    }
                    else if (!entry.BudgetingAccountDetail.Account.Key) {
                        isError = true;
                        errorMessage = "An account in the grid is required!";
                        messageTimeSec = 3000;
                    }
                    else if (totalMonthAmount != entry.Amount) {
                        isError = true;
                        errorMessage = "The budgeted amount and the sum of monthly amounts should be equal!";
                        messageTimeSec = 5000;
                    }
                    else {
                        BudgetEntriesForSaving.push(entry);
                    }
                }
                else {
                    isError = true;
                    errorMessage = "Need at least one amount for saving row!";
                    messageTimeSec = 3500;
                }
            });

            if (isError) {
                $().showGlobalMessage($root, $timeout, true, errorMessage, messageTimeSec);
                TimesheetsForSaving = [];
                return false;
            }

            if (BudgetEntriesForSaving.length > 0) {
                showOverlay();

                var url = utility.myHost + "Accounts/Budgeting/SaveBudgettingEntries";
                $http({
                    method: 'Post',
                    url: url,
                    data: BudgetEntriesForSaving
                }).then(function (result) {
                    if (result.data.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.data.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, result.data.Response);

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.ClearMasterFilterValues();
                                $scope.LoadEntryGrid();
                            });
                        }, 1000);
                    }
                    hideOverlay();
                    return false;
                }, function () {
                    hideOverlay();
                });
            }
            else {
                $().showGlobalMessage($root, $timeout, true, "Need atleast one entry for saving!");
                return false;
            }

        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Need atleast one entry for saving!");
            return false;
        }
    };

    $scope.ClearMasterFilterValues = function () {
        $scope.SearchMastersData = {
            "Budget": { "Key": null, "Value": null },
            "BudgetType": { "Key": null, "Value": null },
            "BudgetSuggestion": { "Key": null, "Value": null },
            "AccountGroup": { "Key": null, "Value": null },
            "CostCenter": { "Key": null, "Value": null },
            "FromDateString": null,
            "ToDateString": null,
            "SuggestedFromDateString": null,
            "SuggestedToDateString": null,
            "AccountGroupList": [],
            "PercentageValue": 0,
        };
    };

    $scope.ClearFilters = function () {
        $scope.SearchMastersData = {
            "BudgetSuggestion": { "Key": null, "Value": null },
            "AccountGroup": { "Key": null, "Value": null },
            "CostCenter": { "Key": null, "Value": null },
            "SuggestedFromDateString": null,
            "SuggestedToDateString": null,
            "AccountGroupList": [],
            "PercentageValue": 0,
        };
    };

    $scope.BudgetChanges = function (budget) {

        var budgetID = budget?.Key;
        if (budgetID) {
            $scope.FillBudgetEntrybyBudgetID(budgetID);
        }
        else {
            hideOverlay();
        }
    };

    $scope.FillBudgetEntrybyBudgetID = function (budgetID) {

        showOverlay();
        var url = utility.myHost + "Accounts/Budgeting/GetBudgetDetailsByID?budgetID=" + budgetID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.SearchMastersData.FromDateString = result.data.PeriodStartDateString;
                $scope.SearchMastersData.ToDateString = result.data.PeriodEndDateString;
                //$scope.SearchMastersData.BudgetType = result.data.BudgetType;

                $scope.FillBudgetEntriesByID(budgetID);

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillBudgetEntriesByID = function (budgetID) {

        if (budgetID) {
            showOverlay();
            var url = utility.myHost + "Accounts/Budgeting/FillBudgetEntriesByID?budgetID=" + budgetID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    if (result.data.Response.BudgetListModel.length > 0) {
                        $scope.BudgetEntries = result.data.Response.BudgetListModel;
                        $scope.BudgetingModel.HeaderTitles = result.data.Response.HeaderTitles;
                    }
                    else {
                        $scope.ClearEntryGrid();
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        else {
            hideOverlay();
        }
    };

    $scope.FillGridsFromGroup = function () {

        var budgetID = $scope.SearchMastersData.Budget?.Key;
        var groupID = $scope.SearchMastersData.AccountGroup?.Key;
        var dateFrom = $scope.SearchMastersData.FromDateString;
        var dateTo = $scope.SearchMastersData.ToDateString;

        if ($scope.SearchMastersData.AccountGroupList.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "At least one group is required to fill the grid!", 2000);
            return false;
        }
        else if (!budgetID) {
            $().showGlobalMessage($root, $timeout, true, "Select Budget!", 2000);
            return false;
        }
        else if (!dateFrom || dateFrom == "?") {
            $().showGlobalMessage($root, $timeout, true, "Date from is required to fill the grid!", 2500);
            return false;
        }
        else if (!dateTo || dateTo == "?") {
            $().showGlobalMessage($root, $timeout, true, "Date to is required to fill the grid!", 2500);
            return false;
        }

        showOverlay();
        var url = utility.myHost + "Accounts/Budgeting/FillBudgetingGridByMaster";
        $http({
            method: 'POST',
            url: url,
            data: $scope.SearchMastersData
        }).then(function (result) {
            if (result.data.IsError) {
                $().showGlobalMessage($root, $timeout, true, "Something went wrong filling the grid. Please try again later.");
            }
            else {
                $timeout(function () {
                    $scope.$apply(function () {

                        if (result.data.Response.BudgetListModel.length > 0) {
                            $scope.BudgetEntries = result.data.Response.BudgetListModel;
                            $scope.BudgetingModel.HeaderTitles = result.data.Response.HeaderTitles;
                        }

                        $scope.FillRowAccountsByGroupID(groupID);
                    });
                }, 1000);
            }
            hideOverlay();
            return false;
        }, function () {
            hideOverlay();
        });
    };

    $scope.FillRowAccountsByGroupID = function (groupID) {

        if (groupID) {
            showOverlay();
            var url = utility.myHost + "Schools/School/GetAccountByGroupID?groupID=" + groupID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.GroupAccounts = result.data;
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
        else {
            $scope.GroupAccounts = $scope.Accounts;

            hideOverlay();
        }
    };

    $scope.GetSuggestionGrid = function () {

        var budgetID = SearchMastersData.BudgetSuggestion?.Key;       
        var dateFrom = $scope.SearchMastersData.SuggestedFromDateString;
        var dateTo = $scope.SearchMastersData.SuggestedToDateString;
        if ($scope.SearchMastersData.AccountGroupList.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "At least one group is required to fill the grid!", 2000);
            return false;
        }
        else if (!budgetID) {
            $().showGlobalMessage($root, $timeout, true, "Select Budget Suggestion!", 2000);
            return false;
        }
        else if (!dateFrom || dateFrom == "?") {
            $().showGlobalMessage($root, $timeout, true, "Date from is required to fill the grid!", 2500);
            return false;
        }
        else if (!dateTo || dateTo == "?") {
            $().showGlobalMessage($root, $timeout, true, "Date to is required to fill the grid!", 2500);
            return false;
        }


        var url = utility.myHost + "Accounts/Budgeting/GetBudgetSuggestionValue";
        $http({
            method: 'POST',
            url: url,
            data: $scope.SearchMastersData
        }).then(function (result) {
            if (result.data.IsError) {
                $().showGlobalMessage($root, $timeout, true, "Something went wrong filling the grid. Please try again later.");
            }
            else {
                $timeout(function () {
                    $scope.$apply(function () {

                        //if (result.data.Response.BudgetListModel.length > 0) {
                        //    $scope.BudgetEntries = result.data.Response.BudgetListModel;
                        //    $scope.BudgetingModel.HeaderTitles = result.data.Response.HeaderTitles;
                        //}

                        //$scope.FillRowAccountsByGroupID(groupID);
                    });
                }, 1000);
            }
            hideOverlay();
            return false;
        }, function () {
            hideOverlay();
        });
    };


    $scope.ShowAllocationDetails = function (title, row, parentIndexNo, $event) {

        $event.preventDefault();
        $("[data-original-title]").popover('dispose');

        $($event.currentTarget).popover({
            container: 'body',
            placement: 'left',
            html: true,
            title: title,
            content: function () {
                return '<label></label>';
            }
        });

        $($event.currentTarget).popover('show');

        $scope.AllocationDetail = row;
        $scope.AllocationDetail.ParentRowIndexNo = parentIndexNo;

        var htmlContent = $('#AllocationDetailsTemplate').html();
        var content = $compile(htmlContent)($scope);
        $('#' + $($event.currentTarget).attr('aria-describedby')).find('.popover-body').html(content);
    };

    $scope.ShowAccountDetails = function (title, row, $event) {

        $event.preventDefault();
        $("[data-original-title]").popover('dispose');

        $($event.currentTarget).popover({
            container: 'body',
            placement: 'left',
            html: true,
            title: title,
            content: function () {
                return '<label></label>';
            }
        });

        $($event.currentTarget).popover('show');
        $scope.BudgetDetail = row;
        var htmlContent = $('#BudgetDetailsTemplate').html();
        var content = $compile(htmlContent)($scope);
        $('#' + $($event.currentTarget).attr('aria-describedby')).find('.popover-body').html(content);
    };

    $scope.HeaderPercentageChanges = function () {

        var percentage = $scope.SearchMastersData.PercentageValue != null && $scope.SearchMastersData.PercentageValue != "" ? $scope.SearchMastersData.PercentageValue : 0;

        if ($scope.BudgetEntries.length > 0) {

            $scope.BudgetEntries.forEach(entry => {

                entry.Percentage = percentage;

                $scope.RowPercentageChanges(entry);
            });
        }
    };

    $scope.RowPercentageChanges = function (row) {

        var percentage = row.Percentage != null && row.Percentage != "" ? row.Percentage : 0;

        var entrySugVal = row.SuggestedValue != null ? row.SuggestedValue : 0;
        //var entrySugVal = 20;

        row.Amount = entrySugVal + (entrySugVal * (percentage / 100));

        if (row.Allocations.length > 0) {
            row.Allocations.forEach(alloc => {

                var allocSugVal = alloc.SuggestedValue != null ? alloc.SuggestedValue : 0;
                //var allocSugVal = 5;

                alloc.Percentage = percentage;
                alloc.EstimateValue = allocSugVal + (allocSugVal * (percentage / 100));
                alloc.Amount = allocSugVal + (allocSugVal * (percentage / 100));
            });
        }
    };

    $scope.FillSuggestions = function () {

        var budgetID = $scope.SearchMastersData.Budget?.Key;
        var suggestionID = $scope.SearchMastersData.BudgetSuggestion?.Key;
        var dateFrom = $scope.SearchMastersData.SuggestedFromDateString;
        var dateTo = $scope.SearchMastersData.SuggestedToDateString;
        if (!suggestionID) {
            $().showGlobalMessage($root, $timeout, true, "Select suggestion!", 2000);
            return false;
        }
        else if (!dateFrom || dateFrom == "?") {
            $().showGlobalMessage($root, $timeout, true, "From date is required to fill suggestion!", 2500);
            return false;
        }
        else if (!dateTo || dateTo == "?") {
            $().showGlobalMessage($root, $timeout, true, "To date is required to fill the suggestion!", 2500);
            return false;
        }
       else if ($scope.BudgetEntries.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "At least one group is required to fill the suggestion!", 2000);
            return false;
        }
        else if ($scope.BudgetEntries.length > 0) {
            var groupIDs = [];
            var budgetingAccount = { "AccountListModel": [] };

            $scope.BudgetEntries.forEach(entry => {

                var accountGroup = entry.BudgetingAccountDetail?.AccountGroup;

                if (accountGroup != null && accountGroup.Key && !groupIDs.includes(accountGroup.Key)) {
                    groupIDs.push(accountGroup.Key);
                    //$scope.SearchMastersData.AccountGroupList.push(accountGroup);
                }

                budgetingAccount.AccountListModel.push({
                    AccountGroup: entry.BudgetingAccountDetail?.AccountGroup,
                    Account: entry.BudgetingAccountDetail?.Account,
                });
            });

            $scope.SearchMastersData.BudgetingAccountDetail = budgetingAccount;

            if (groupIDs.length <= 0) {

                $().showGlobalMessage($root, $timeout, true, "At least one group is required to fill the suggestion!", 2000);
                return false;
            }
        }
        else if (!budgetID) {
            $().showGlobalMessage($root, $timeout, true, "Select an budget!", 2000);
            return false;
        }
        

        showOverlay();
        var url = utility.myHost + "Accounts/Budgeting/GetBudgetSuggestionValue";
        $http({
            method: 'POST',
            url: url,
            data: $scope.SearchMastersData,
        }).then(function (result) {
            if (result.data.IsError) {
                $().showGlobalMessage($root, $timeout, true, "Something went wrong filling the grid. Please try again later.");
            }
            else {
                $timeout(function () {
                    $scope.$apply(function () {

                        if (result.data.Response.BudgetListModel.length > 0) {
                            $scope.BudgetEntries = result.data.Response.BudgetListModel;
                            $scope.BudgetingModel.HeaderTitles = result.data.Response.HeaderTitles;
                        }
                    });
                }, 1000);
            }
            hideOverlay();
            return false;
        }, function () {
            hideOverlay();
        });
    };

    $scope.SuggestionChanges = function () {

        var budgetID = $scope.SearchMastersData.Budget?.Key;
        var suggestionID = $scope.SearchMastersData.BudgetSuggestion?.Key;

        if (!budgetID) {
            $().showGlobalMessage($root, $timeout, true, "Select an budget!", 2000);
            return false;
        }

        if (suggestionID) {
            if ($scope.SearchMastersData.BudgetSuggestion.Value.toLowerCase().includes("previous") && $scope.SearchMastersData.BudgetSuggestion.Value.toLowerCase().includes("year")) {
                //Using Budget From Date
                var fromDateString = $scope.SearchMastersData.FromDateString;

                // Extract month, day, and year
                var parts = fromDateString.split("/");
                var day = parts[0];
                var month = parts[1];
                var year = parts[2];

                // Decrement the year by 1
                year = parseInt(year) - 1;

                // Concatenate with the updated year
                $scope.SearchMastersData.SuggestedFromDateString = day + "/" + month + "/" + year;


                //Using Budget To Date
                var toDateString = $scope.SearchMastersData.ToDateString;
                var parts = toDateString.split("/");
                var day = parts[0];
                var month = parts[1];
                var year = parts[2];
                year = parseInt(year) - 1;
                $scope.SearchMastersData.SuggestedToDateString = day + "/" + month + "/" + year;
            }
        }
    };

    $scope.ApplyAmountMonthValues = function (row) {

        if (row.Amount > 0) {

            const monthCounts = row.Allocations.length;
            var remainder = 0;

            if (row.SuggestedValue) {
                row.Allocations.slice(0, monthCounts - 1).forEach(alloc => {

                    // Calculate the amount based on the proportion of the suggested value
                    var columnAmount = (alloc.SuggestedValue / row.SuggestedValue) * row.Amount;

                    // Round down to the nearest integer
                    alloc.Amount = Math.floor(columnAmount);

                    // Calculate the remainder and store it
                    remainder += alloc.Amount - Math.floor(alloc.Amount);
                });

                var lastRow = row.Allocations[row.Allocations.length - 1];
                lastRow.Amount += remainder;

                var difference = row.Amount - row.Allocations.reduce((total, alloc) => total + parseFloat(alloc.Amount), 0);

                lastRow.Amount += difference;

                roundedNumber = lastRow.Amount.toFixed(2)

                lastRow.Amount = parseFloat(roundedNumber);
            }
            else {
                // Calculate the integer amount for the first 11 months
                const integerAmountPerMonth = Math.floor(row.Amount / monthCounts);

                // Calculate the remainder
                const remainder = row.Amount % monthCounts;

                // Set the integer amount for the first 11 months
                row.Allocations.slice(0, monthCounts - 1).forEach(alloc => {
                    alloc.Amount = integerAmountPerMonth;
                });

                // Add the remainder to the last month
                row.Allocations[monthCounts - 1].Amount = integerAmountPerMonth + remainder;
            }
        }
    };

    $scope.MonthAmountChanges = function (row) {

        // Assuming row.Allocations is an array of objects with an Amount property as string
        var sumofAmount = row.Allocations.reduce((total, alloc) => {
            // Parse the string to float before adding
            var amount = parseFloat(alloc.Amount);
            // Check if the parsed value is a valid number
            if (!isNaN(amount)) {
                return total + amount;
            } else {
                return total; // Ignore invalid entries
            }
        }, 0);

        row.Amount = sumofAmount;
    };

    $scope.MonthWisePercentageChanges = function (monthColumn) {

        var percentage = monthColumn.Percentage != null && monthColumn.Percentage != "" ? monthColumn.Percentage : 0;
        var allocSugVal = monthColumn.SuggestedValue != null ? monthColumn.SuggestedValue : 0;

        monthColumn.EstimateValue = allocSugVal + (allocSugVal * (percentage / 100));
        monthColumn.Amount = allocSugVal + (allocSugVal * (percentage / 100));

        var parentRow = $scope.BudgetEntries[monthColumn.ParentRowIndexNo];
        $scope.MonthAmountChanges(parentRow);
    };

    $scope.ViewBudgetReport = function (row) {

        if (row.Budget == null && !row.Budget.Key) {
            $().showGlobalMessage($root, $timeout, true, "Budget ID is required to view report!");
            return false;
        }

        var reportName = "BudgetingReport";
        var reportHeader = "Budgeting Report";

        //var parameter = "BudgetID=" + row.Budget.Key + '&AsOnDate=' + Row.DateToday)"
        var parameter = "BUDGETID=" + row.Budget.Key;

        if ($scope.ShowWindow(reportName, reportHeader, reportName) || row == null)
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var reportUrl = "";

        $.ajax({
            url: utility.myHost + "Reports/Report/GetReportUrlandParameters?reportName=" + reportName,
            type: 'GET',
            success: function (result) {
                if (result.Response) {
                    reportUrl = result.Response + "&" + parameter;
                    var loadContent = "<center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>";
                    $('#' + windowName).html(loadContent + '<iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
                    var $iFrame = $('iframe[reportname=' + reportName + ']');
                    $iFrame.ready(function () {
                        setTimeout(function () {
                            $("#Load", $('#' + windowName)).hide();
                        }, 1000);
                    });
                }
            }
        });
    };
    $scope.LoadAccount = function (row) {
        var accountGroupID = row.BudgetingAccountDetail?.AccountGroup?.Key;
        if (accountGroupID) {
            showOverlay();

            var url = utility.myHost + "Schools/School/GetAccountByGroupID?groupID=" + accountGroupID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.GroupAccounts = result.data;

                }, function () {

                });          

            hideOverlay();
        }
        else {
            $scope.GroupAccounts = $scope.Accounts;

            hideOverlay();
        }
    }

}]);