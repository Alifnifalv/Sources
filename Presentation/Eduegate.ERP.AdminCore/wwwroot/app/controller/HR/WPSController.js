app.controller("WPSController", ["commonService", "$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function (commonService, $scope, $http, $compile, $window, $timeout, $location, $route, $root) {
        console.log("WPS Controller Loaded");

    var vm = this;
    vm.windowContainer = null;
    vm.ViewName = 'WPS';
    vm.HasFilters = false;
    $scope.TotalSalaries = 0;
    $scope.LookUps = [];
    $scope.ShowSpinner = false;
    var viewFullPath = null;
    $scope.RowModel = {};
    $scope.RowModel.rows = [];
    $scope.AdvanceSearchRunTimeFilter = null
    $scope.TotalRecords = 0;
    $scope.TotalPages = 0;
    $scope.GoToPage = '';
    $scope.CurrentPage = 0;
    $scope.PageSize = 0;
    $scope.SortBy = null;
    $scope.QuickFilter = [];
    $scope.selectedSchoolID = null;
    $scope.selectedMonthID = null;
    $scope.selectedBankID = null;
    $scope.ShowBankCard = false;
    $scope.selectedYear = null;
    $scope.payerBankDetails = null;
    $scope.FilterCondtion = "Equals";
    $scope.IsWayPointAttached = false;
    $scope.IsWayPointInProcess = false;
    var xhrFilterRequest = null;

    $scope.Init = function (window, model) {
        vm = this;
        vm.commonService = commonService;
        vm.windowContainer = '#' + window;
        $scope.Model = JSON.parse(model);
        $scope.ShowBankCard = false;
        FitlerSuccess();
    };

    $scope.GenerateWPS = function () {

        var headerList = null;
        var formattedDate = null;
        var salaryYearMonth = null;
        var formattedTime = null;


        if ($scope.selectedBankID == null || $scope.selectedBankID == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Payer bank details not filled. please select school and bank !");
            return false;
        }

        if ($scope.selectedYear == null || $scope.selectedYear == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Please select year !");
            return false;
        }

        if ($scope.selectedMonthID == null || $scope.selectedMonthID == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Please select month !");
            return false;
        }

        showOverlay();

        salaryYearMonth = $scope.selectedYear + $scope.selectedMonthID.toString().padStart(2, '0');

        var today = new Date();
        var year = today.getFullYear();
        var month = (today.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 to month as it is zero-based, and padStart ensures two digits
        var day = today.getDate().toString().padStart(2, '0'); // padStart ensures two digits

        formattedDate = year + month + day;

        var hours = today.getHours().toString().padStart(2, '0'); // padStart ensures two digits
        var minutes = today.getMinutes().toString().padStart(2, '0'); // padStart ensures two digits

        formattedTime = hours + minutes;

        //TODO --- headers passing as like hardcode need to fix later
        headerList = [
            { Key: "Employer EID", Value: $scope.payerBankDetails[0].EmployerEID },
            { Key: "File Creation Date", Value: formattedDate.toString() },
            { Key: "File Creation Time", Value: formattedTime.toString() },
            { Key: "Payer EID", Value: $scope.payerBankDetails[0].PayerEID },
            { Key: "Payer QID", Value: $scope.payerBankDetails[0].PayerQID },
            { Key: "Payer Bank Short Name", Value: $scope.payerBankDetails[0].PayerBankShortName }, 
            { Key: "Payer IBAN", Value: $scope.payerBankDetails[0].PayerIBAN },
            { Key: "Salary Year And Month", Value: salaryYearMonth.toString() },
            { Key: "Total Salaries", Value: $scope.TotalSalaries.toString() },
            { Key: "Total Records", Value: $scope.TotalRecords.toString() },
        ];

        var fileName = "SIF_" + $scope.payerBankDetails[0].EmployerEID + "_" + $scope.payerBankDetails[0].PayerBankShortName + "_" + formattedDate + "_" + formattedTime;

        $.ajax({
            url: "Payroll/WPS/GenerateWPS",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                CsvFileHeaders: headerList,
                PayerBankDetailIID: $scope.payerBankDetails[0].PayerBankDetailIID,
                FileName: fileName,
                SalaryYear: $scope.selectedYear,
                SalaryMonth: $scope.selectedMonthID,
                TotalSalaries: $scope.TotalSalaries,
                TotalRecords: $scope.TotalRecords,
            }),
            success: function (contentID) {
                if (!contentID) {
                    $().showGlobalMessage($root, $timeout, true, "Something went wrong.please try again..");
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, "WPS Generated Successfully !");
                    $scope.DownloadContentFile(contentID);
                }
            },
            complete: function (contentID) {
                hideOverlay();
            }
        });
    };


    $scope.DownloadContentFile = function (contentID) {
        var url = "Content/ReadContentsByID?contentID=" + contentID;
        var link = document.createElement("a");
        link.href = utility.myHost + url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    $scope.ResetSearchClick = function () {
        $scope.RowModel.CurrentPage = "1";
        console.log($scope.Model);
        $scope.Model.forEach(function (index) {
            index.FilterValue = null;
            index.FilterValue2 = null;
            index.FilterValue3 = null;
        });
        $scope.QuickFilter = {};
        $scope.selectedBankID = null;
        $scope.payerBankDetails = null;
        $scope.LookUps = [];
        $scope.ShowBankCard = false;
        $scope.SearchClick();
    }

    $scope.SearchClick = function () {
        $('.load', $(vm.windowContainer)).show();
        $scope.RowModel.rows = [];
        SaveFilter();
    }

    function SaveFilter() {
        $('.preload-overlay', $(vm.windowContainer)).show();
        $('.preloader', $(vm.windowContainer)).show();
        if (xhrFilterRequest != null) {
            xhrFilterRequest.abort();
        }
        xhrFilterRequest =
            $.ajax({
                type: "POST",
                url: 'Mutual/SaveFilter',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify($scope.Model),
                success: FitlerSuccess,
            });
    }

    $scope.CurrentDate = null;

    function FitlerSuccess() {
        $scope.TotalRecords = 0;
        $scope.TotalPages = 0;
        $scope.CurrentPage = 0;
        $scope.PageSize = 0;
        $scope.IsWayPointInProcess = false;
        $scope.SortBy = null;
        $scope.RowModel.rows = [];
        $('.load', $(vm.windowContainer)).show();
        if ($scope.$parent.AdvanceSearchRunTimeFilter != null) {
            $scope.AdvanceSearchRunTimeFilter = $scope.$parent.AdvanceSearchRunTimeFilter
        }

        $('.popup-wrap .load', $(vm.windowContainer)).show();
        vm.commonService.get('Frameworks/Search/SearchData?view=' + "WPS" + '&currentPage=' + ($scope.CurrentPage + 1) + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
    }

    function GetDBSynchTime() {
        var newDateTime = new Date();
        return newDateTime.getDate() + "/" + newDateTime.getMonthName() + "/" + newDateTime.getFullYear() + " " + newDateTime.toLocaleTimeString();
    }

    function LoadSuccess(response) {
        var datas;
        if (response.data.NewData == undefined)
            datas = JSON.parse(response.data);
        else
            datas = JSON.parse(response.data.NewData);
        $.each(datas.Datas, function (index, row) {
            $scope.RowModel.rows.push(row);
        });

        $scope.TotalRecords = datas.TotalRecords;
        $scope.CurrentPage = datas.CurrentPage;
        $scope.TotalPages = datas.TotalPages;
        $scope.PageSize = datas.PageSize;

        if ($scope.TotalRecords > 0) {

            FillSummaryData();

            $scope.IsWayPointInProcess = false;
            $("table.listing", $(vm.windowContainer)).next("p").remove();
        }
        else {
            /* when table has no data then we have to show the message */
            $("table.listing", $(vm.windowContainer)).next("p").remove();
            $("table.listing", $(vm.windowContainer)).after("<p><br>Result not found.</p>");
            $scope.TotalSalaries = 0;
        }

        $timeout(function () {
            if (!$scope.IsWayPointAttached) {
            }
            else {
                $.waypoints('refresh');
            }
        }, 100);

        $('.popup-wrap .load', $(vm.windowContainer)).hide();
        $('.load', $(vm.windowContainer)).hide();
    }


    function FillSummaryData() {
        showOverlay();
        $.ajax({
            type: "GET",
            url: "Payroll/WPS/GetTotalNetSalaries",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (result) {
                        $scope.TotalSalaries = result;
                        hideOverlay();
                    }
                    else {
                        $scope.TotalSalaries = 0;
                        hideOverlay();
                    }
                });
            }
        });
    }

    $scope.LoadLookups = function (lookupName, isLazyLoad, $select) {

        $scope.LookUps[lookupName] = [];
        $scope.LookUps[lookupName].push({ Key: "", Value: "Loading.." });

        var loadUrl;

        if (!isLazyLoad) {
            loadUrl = "Mutual/GetLookUpData?lookType=" + lookupName;
        }
        else {
            loadUrl = "Mutual/GetLazyLookUpData?lookType=" + lookupName + "&lookupName=" + lookupName + "&searchText=" + $select.search;
        }

        $.ajax({
            type: "GET",
            url: loadUrl,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (result.Data == undefined) {
                        $scope.LookUps[lookupName] = result;
                    }
                    else {
                        $scope.LookUps[lookupName] = result.Data;
                    }

                    // Create a map to store unique values based on the 'Value' property
                    var map = new Map();
                    for (var item of result) {
                        map.set(item.Value, item);
                    }

                    // Convert the map back to an array of objects
                    $scope.LookUps[lookupName] = Array.from(map.values());
                });
            }
        });
    };

    function loadBanks(selectedSchoolID) {
        $scope.LookUps.BankName = [];
        $scope.payerBankDetails = null;


        if ($scope.SchoolBankPayerDetails.length == 1) {
            $scope.ShowBankCard = false;
            $scope.selectedBankID = $scope.SchoolBankPayerDetails[0].Bank.Key;
            $scope.onRadioSelectionChange($scope.selectedBankID);
        }
        else if ($scope.SchoolBankPayerDetails.length > 1) {
            $scope.SchoolBankPayerDetails.forEach(x => {
                $scope.LookUps.BankName.push({
                    "Key": x.Bank.Key,
                    "Value": x.Bank.Value,
                });
            });
            $scope.ShowBankCard = true;
        }
    };

    function loadRelatedLooKupDatas(selectedFilterItem, lookup) {
        $scope.LookUps.BankShortNames = [];

        if (lookup == "School") {
            $scope.selectedSchoolID = selectedFilterItem.Key;
            $scope.SchoolChanges(selectedFilterItem.Key);
        }
        else if (lookup == "MonthNames") {
            $scope.selectedMonthID = selectedFilterItem.Key;
        }
        else if (lookup == "AcademicYearCode") {
            $scope.selectedYear = selectedFilterItem.Value;
        }
    }


    $scope.DropDownOnChange = function (index, valuefield, lookup) {
        var selectedID = null;

        if (valuefield == 1) {
            $scope.Model[index].IsDirty = $scope.QuickFilter[index].FilterValue.length != 0 && $scope.QuickFilter[index].FilterValue3.Key.length != 0;
        }
        else if (valuefield == 2) {
            $scope.Model[index].IsDirty = ($scope.QuickFilter[index].FilterValue3 != null &&
                $scope.QuickFilter[index].FilterValue3.Key != null && $scope.QuickFilter[index].FilterValue3.Key.length != 0);

            if ($scope.Model[index].IsDirty) {
                $scope.Model[index].FilterValue3 = { Key: $scope.QuickFilter[index].FilterValue3.Key, Value: $scope.QuickFilter[index].FilterValue3.Value };

                var selectedFilterItem = $scope.Model[index].FilterValue3;
                loadRelatedLooKupDatas(selectedFilterItem, lookup)
            }
        }
    };

    $scope.SchoolChanges = function (selectedSchoolID) {
        showOverlay();
        $scope.ShowBankCard = false;
        $scope.selectedBankID = null;
        var loadUrl;

        loadUrl = "Schools/School/FillPayerBanksBySchoolID?schoolID=" + selectedSchoolID;

        $.ajax({
            type: "GET",
            url: loadUrl,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    }
                    else {
                        $scope.SchoolBankPayerDetails = result;

                        loadBanks(selectedSchoolID);
                    }
                });
            }
        });
    };

    $scope.FirstPage = function (event) {
        if ($scope.CurrentPage != 1) {
            $scope.RowModel.CurrentPage = "1";
            $scope.RowModel.rows = [];
            $('.tablewrapper .content-load', vm.WindowContainer).show();

            vm.commonService.get('Frameworks/Search/SearchData?view=' + "WPS" + '&currentPage=' + "1" + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
        }
    };

    $scope.PreviousPage = function (event) {
        if ($scope.CurrentPage != 1) {
            $scope.RowModel.CurrentPage = $scope.CurrentPage - 1;
            $scope.RowModel.rows = [];
            $('.tablewrapper .content-load', vm.windowContainer).show();
            vm.commonService.get('Frameworks/Search/SearchData?view=' + "WPS" + '&currentPage=' + ($scope.CurrentPage - 1) + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
        }
    };

    $scope.NextPage = function (event) {
        if ($scope.CurrentPage < $scope.TotalPages) {
            $scope.RowModel.CurrentPage = $scope.CurrentPage + 1;
            $scope.RowModel.rows = [];
            $('.tablewrapper .content-load', vm.windowContainer).show();
            vm.commonService.get('Frameworks/Search/SearchData?view=' + "WPS" + '&currentPage=' + ($scope.CurrentPage + 1) + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
        }
    };

    $scope.LastPage = function (event) {
        if ($scope.CurrentPage != $scope.TotalPages) {
            $scope.RowModel.CurrentPage = $scope.TotalPages;
            $scope.RowModel.rows = [];
            $('.tablewrapper .content-load', vm.windowContainer).show();
            vm.commonService.get('Frameworks/Search/SearchData?view=' + "WPS" + '&currentPage=' + $scope.TotalPages + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
        }
    }

    $scope.GoToPage = function (event) {

        if ($scope.GoToPageNo >= 1 && $scope.GoToPageNo <= $scope.TotalPages) {
            $scope.RowModel.CurrentPage = $scope.GoToPageNo;
            $scope.RowModel.rows = [];
            $('.tablewrapper .content-load', vm.windowContainer).show();
            vm.commonService.get('Frameworks/Search/SearchData?view=' + "WPS" + '&currentPage=' + $scope.GoToPageNo + "&orderby=" + $scope.SortBy + "&runtimeFilter=" + "" + "&startDate=" + GetDBSynchTime(), $scope.param, LoadSuccess);
        }
    }

    $scope.onRadioSelectionChange = function (bankID) {
        showOverlay();

        if (bankID != null || bankID != undefined) {
            $scope.selectedBankID = bankID;

            if ($scope.SchoolBankPayerDetails.length > 0) {
                $scope.ShowSpinner = true;
                $scope.FilterPayerDetails($scope.SchoolBankPayerDetails, $scope.selectedBankID);
            }
        }
        hideOverlay();
    }

    $scope.FilterPayerDetails = function (datas, bankID) {

        $scope.payerBankDetails = datas.filter(f => f.BankID == bankID);
        $scope.ShowSpinner = false;
    }


    function showOverlay() {
        $('.preload-overlay', $(vm.windowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $(vm.windowContainer)).hide();
    }

}]);
