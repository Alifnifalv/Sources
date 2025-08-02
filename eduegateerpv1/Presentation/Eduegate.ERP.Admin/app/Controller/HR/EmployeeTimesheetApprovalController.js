app.controller("EmployeeTimesheetApprovalController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("EmployeeTimesheetApprovalController Loaded");

    $scope.PendingTimeSheetsDatas = [];
    $scope.ApprovedTimeSheetsDatas = [];
    $scope.ManualEntryTimeSheetDatas = [];

    $scope.Employees = [];
    $scope.ManualEntryEmployees = [];

    $scope.Tasks = [];
    $scope.TimeTypes = [];
    $scope.ApprovalStatuses = [];
    $scope.SpecialOTTimeTypeID = null;
    $scope.ApprovedStatusID = null;

    $scope.PendingMaster = {
        "SelectedEmployee": {},
        "SelectedStatus": {},
        "SelectedFromDate": null,
        "SelectedToDate": null
    };

    $scope.ApprovedMaster = {
        "SelectedEmployee": {},
        "SelectedStatus": {},
        "SelectedFromDate": null,
        "SelectedToDate": null
    };

    $scope.ManualEntryMaster = {
        "SelectedStatus": {},
    };

    $scope.Init = function (model, windowname, type) {
        $scope.type = type;
        $scope.ApprovalModel = model;

        $scope.GetLookUpDatas();

    };

    $scope.GetLookUpDatas = function () {

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=AllEmployee&defaultBlank=false",
        }).then(function (result) {
            $scope.Employees = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Task&defaultBlank=false",
        }).then(function (result) {
            $scope.Tasks = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=TimesheetTimeType&defaultBlank=false",
        }).then(function (result) {
            $scope.TimeTypes = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=TimesheetApprovalStatus&defaultBlank=false",
        }).then(function (result) {
            $scope.ApprovalStatuses = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "SPECIAL_OT_TIME_TYPE_ID",
        }).then(function (result) {
            $scope.SpecialOTTimeTypeID = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "TIMESHEET_APPROVAL_APPROVED_STATUS_ID",
        }).then(function (result) {
            $scope.ApprovedStatusID = result.data;
        });
    };

    $scope.ExpandCollapase = function (event, model, field) {
        model[field] = !model[field];

    };

    //#region Tab open and close
    $scope.onTimesheetViewTab = function (tabId) {
        if (tabId == "Tab_01") {
            $scope.TabName = "Tab_01";

            $("#Timesheet_Pending_Tab").show();

            $("#Timesheet_Approved_Tab").hide();
            $("#Timesheet_Entry_Tab").hide();

            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element.classList.add("active");

            //Deactivate tabs
            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element.classList.remove("active");

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element.classList.remove("active");
        }
        else if (tabId == "Tab_02") {
            $scope.TabName = "Tab_02";

            $("#Timesheet_Approved_Tab").show();

            $("#Timesheet_Pending_Tab").hide();
            $("#Timesheet_Entry_Tab").hide();

            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element.classList.add("active");

            //Deactivate tabs
            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element.classList.remove("active");

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element.classList.remove("active");
        }
        else if (tabId == "Tab_03") {
            $scope.TabName = "Tab_03";

            $("#Timesheet_Entry_Tab").show();

            $("#Timesheet_Pending_Tab").hide();
            $("#Timesheet_Approved_Tab").hide();

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element.classList.add("active");

            //Deactivate tabs
            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element.classList.remove("active");

            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element.classList.remove("active");

            $scope.LoadManualEntry();
        }
    };
    //#endregion End tab open and close

    //#region Pending tab codes start
    $scope.ChangePendingFromDate = function () {
        if (!$scope.PendingMaster.SelectedToDate) {
            if ($scope.PendingMaster.SelectedFromDate) {
                $scope.PendingMaster.SelectedToDate = $scope.PendingMaster.SelectedFromDate;
            }
        }

        $scope.CheckPendingDateRanges();
    };

    $scope.CheckPendingDateRanges = function () {
        if ($scope.PendingMaster.SelectedFromDate && $scope.PendingMaster.SelectedToDate) {
            if ($scope.PendingMaster.SelectedFromDate > $scope.PendingMaster.SelectedToDate) {
                $().showGlobalMessage($root, $timeout, true, "Please ensure that the 'date to' is greater than or equal to the 'date from'.", 3500);
                $scope.PendingMaster.SelectedToDate = null;
                return false;
            }
        }
    };

    $scope.GetEmployeePendingTimesheetsData = function () {

        var checkBox = document.getElementById("IsSelectedToPublish");
        if (checkBox && checkBox.checked) {
            checkBox.checked = false;
        }

        $scope.PendingMaster.SelectedStatus = {};

        var empid = $scope.PendingMaster.SelectedEmployee?.Key;
        var fromDate = $scope.PendingMaster.SelectedFromDate;
        var toDate = $scope.PendingMaster.SelectedToDate;

        if (!empid) {
            $().showGlobalMessage($root, $timeout, true, "Select an employee!");
            return false;
        }
        if (!fromDate) {
            $().showGlobalMessage($root, $timeout, true, "Select date from!");
            return false;
        }
        if (!toDate) {
            toDate = $scope.PendingMaster.SelectedFromDate;
        }

        showPendingOverlay();
        var url = "Payroll/Employee/GetPendingTimeSheetsDatas?employeeID=" + empid + "&dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.PendingTimeSheetsDatas = result.data;

                hidePendingOverlay();
            }, function () {
                hidePendingOverlay();
            });
    };

    $scope.PendingTimesheetTypeChanges = function (timesheet) {

        if (timesheet.TimesheetTimeType) {
            if (timesheet.TimesheetTimeType.Key) {
                if (timesheet.TimesheetTimeType.Key == $scope.SpecialOTTimeTypeID) {
                    timesheet.ApprovedOTHours = (parseFloat(timesheet.ApprovedOTHours) + parseFloat(timesheet.ApprovedNormalHours)).toFixed(2);
                    timesheet.ApprovedNormalHours = 0;
                }
            }
        }
    };

    $scope.SelectedPendingSheetHeaderCheckBox = function () {
        var checkBox = document.getElementById("IsSelectedToPublish");
        if (checkBox.checked) {
            $scope.PendingTimeSheetsDatas.forEach(s => s.IsSelected = true);
        }
        else {
            $scope.PendingTimeSheetsDatas.forEach(s => s.IsSelected = false);
        }
    };

    $scope.UpdatePendingTimeSheetsStatus = function () {

        if ($scope.PendingMaster.SelectedStatus && $scope.PendingMaster.SelectedStatus.Key) {
            if ($scope.PendingTimeSheetsDatas.length > 0) {
                $scope.PendingTimeSheetsDatas.forEach(s => {
                    s.TimesheetApprovalStatus.Key = $scope.PendingMaster.SelectedStatus.Key;
                    s.TimesheetApprovalStatus.Value = $scope.PendingMaster.SelectedStatus.Value;
                });
            }
            else {
                $().showGlobalMessage($root, $timeout, true, "Need atleast one timesheet for status change!");
                $scope.PendingMaster.SelectedStatus = {};
            }
        }
    };

    $scope.SavePendingTimesheetApprovalData = function (timesheet) {
        if (timesheet.EmployeeTimeSheetID > 0) {

            if (!timesheet.ApprovedNormalHours && !timesheet.ApprovedOTHours) {
                $().showGlobalMessage($root, $timeout, true, "Need normal hours or OT hours for save entry!", 2000);
                return false;
            }

            if (!timesheet.TimesheetApprovalStatus.Key) {
                $().showGlobalMessage($root, $timeout, true, "Need status for save entry!", 2000);
                return false;
            }

            showPendingOverlay();

            var url = "Payroll/Employee/SavePendingTimesheetApprovalData";
            $http({
                method: 'Post',
                url: url,
                data: timesheet
            }).then(function (result) {
                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.data.Response);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, result.data.Response);

                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.GetEmployeePendingTimesheetsData();
                        });
                    }, 1000);
                }
                hidePendingOverlay();
                return false;
            }, function () {
                hidePendingOverlay();
            });
        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Something went wrong, try again later!");
        }
    };

    $scope.PublishPendingTimesheets = function () {
        if ($scope.PendingTimeSheetsDatas.length > 0) {

            var TimesheetsForPublishing = [];

            var isError = false;
            var errorMessage = "";
            var messageTimeSec = 1000;

            $scope.PendingTimeSheetsDatas.forEach(s => {
                messageTimeSec = 1000;
                if (s.IsSelected == true) {
                    if (s.ApprovedNormalHours || s.ApprovedOTHours) {
                        TimesheetsForPublishing.push(s);
                    }
                    else {
                        isError = true;
                        errorMessage = "Please provide Approved 'Normal hours' or 'OT hours' to save the selected entry, or uncheck that entry.";
                        messageTimeSec = 5000;
                    }

                    if (!s.TimesheetApprovalStatus.Key) {
                        isError = true;
                        errorMessage = "Need status for save the selected entry, or uncheck that entry.";
                        messageTimeSec = 3000;
                    }
                }
            });

            if (isError) {
                $().showGlobalMessage($root, $timeout, true, errorMessage, messageTimeSec);
                TimesheetsForPublishing = [];
                return false;
            }

            if (TimesheetsForPublishing.length > 0) {
                showPendingOverlay();

                var url = "Payroll/Employee/PublishPendingTimesheets";
                $http({
                    method: 'Post',
                    url: url,
                    data: TimesheetsForPublishing
                }).then(function (result) {
                    if (result.data.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.data.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, result.data.Response);

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.GetEmployeePendingTimesheetsData();
                            });
                        }, 1000);
                    }
                    hidePendingOverlay();
                    return false;
                }, function () {
                    hidePendingOverlay();
                });
            }
            else {
                $().showGlobalMessage($root, $timeout, true, "Select atleast one timesheet for publish!");
                return false;
            }

        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Need atleast one timesheet for publish!");
            return false;
        }
    };
    //#endregion Pending tab codes end

    //#region Approved tab codes start
    $scope.GetApprovedTimeSheetsDatas = function () {

        var checkBox = document.getElementById("IsSelectedToPublish");
        if (checkBox && checkBox.checked) {
            checkBox.checked = false;
        }

        $scope.ApprovedMaster.SelectedStatus = {};

        var empid = $scope.ApprovedMaster.SelectedEmployee?.Key;
        var fromDate = $scope.ApprovedMaster.SelectedFromDate;
        var toDate = $scope.ApprovedMaster.SelectedToDate;

        if (!empid) {
            $().showGlobalMessage($root, $timeout, true, "Select an employee!");
            return false;
        }
        if (!fromDate) {
            $().showGlobalMessage($root, $timeout, true, "Select date from!");
            return false;
        }
        if (!toDate) {
            toDate = $scope.ApprovedMaster.SelectedFromDate;
        }

        showApprovedOverlay();
        var url = "Payroll/Employee/GetApprovedTimeSheetsDatas?employeeID=" + empid + "&dateFromString=" + fromDate + "&dateToString=" + toDate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.ApprovedTimeSheetsDatas = result.data;

                hideApprovedOverlay();
            }, function () {
                hideApprovedOverlay();
            });
    };

    $scope.ChangeApprovedFromDate = function () {
        if (!$scope.ApprovedMaster.SelectedToDate) {
            if ($scope.ApprovedMaster.SelectedFromDate) {
                $scope.ApprovedMaster.SelectedToDate = $scope.ApprovedMaster.SelectedFromDate;
            }
        }

        $scope.CheckApprovedDateRanges();
    };

    $scope.CheckApprovedDateRanges = function () {
        if ($scope.ApprovedMaster.SelectedFromDate && $scope.ApprovedMaster.SelectedToDate) {
            if ($scope.ApprovedMaster.SelectedFromDate > $scope.ApprovedMaster.SelectedToDate) {
                $().showGlobalMessage($root, $timeout, true, "Please ensure that the 'date to' is greater than or equal to the 'date from'.", 3500);
                $scope.ApprovedMaster.SelectedToDate = null;
                return false;
            }
        }
    };
    //#endregion Approved tab codes end

    //#region Manual entry tab codes start
    $scope.InsertEntryRow = function (index) {

        var model = $scope.ManualEntryTimeSheetDatas;
        var row = $scope.ManualEntryGrid[0];

        model.splice(index + 1, 0, angular.copy(row));
    };

    $scope.RemoveEntryRow = function (index) {

        if ($scope.ManualEntryTimeSheetDatas.length == 1) {
            if (index == 1) {
                $scope.LoadManualEntry();
            }
        }
        else {

            var model = $scope.ManualEntryTimeSheetDatas;
            var row = $scope.ManualEntryGrid[0];

            model.splice(index, 1);

            if (index === 0) {
                $scope.InsertEntryRow(index, row, model);
            }
        }
    };

    $scope.LoadManualEntry = function () {

        $scope.ManualEntryTimeSheetDatas = [];

        if ($scope.ManualEntryEmployees.length == 0) {
            if ($scope.Employees.length > 0) {
                $scope.Employees.forEach(e => {
                    if (e.Key > 0) {
                        $scope.ManualEntryEmployees.push({
                            "Key": e.Key,
                            "Value": e.Value
                        })
                    }
                })
            }
        }

        var checkBox = document.getElementById("IsSelectedToSave");
        if (checkBox && checkBox.checked) {
            checkBox.checked = false;
        }

        $scope.ManualEntryGrid = [];
        $scope.ManualEntryMaster.SelectedStatus = {};

        $scope.ManualEntryTimeSheetDatas.push(JSON.parse(JSON.stringify($scope.ApprovalModel)));

        if ($scope.ManualEntryTimeSheetDatas.length > 0) {
            if ($scope.ApprovedStatusID) {

                var status = $scope.ApprovalStatuses.find(x => x.Key == $scope.ApprovedStatusID);

                if (status) {
                    $scope.ManualEntryTimeSheetDatas.forEach(x => {
                        x.TimesheetApprovalStatus.Key = status.Key;
                        x.TimesheetApprovalStatus.Value = status.Value;
                    });
                }
            }
        }

        $scope.ManualEntryGrid = JSON.parse(JSON.stringify($scope.ManualEntryTimeSheetDatas));
    };

    $scope.ChangeManualEntryFromDate = function (sheet) {
        if (!sheet.DateToString) {
            if (sheet.DateFromString) {
                sheet.DateToString = sheet.DateFromString;
            }
        }

        $scope.CheckManualEntryDateRanges(sheet);
    };

    $scope.CheckManualEntryDateRanges = function (sheet) {
        if (sheet.DateFromString && sheet.DateToString) {
            if (sheet.DateFromString > sheet.DateToString) {
                $().showGlobalMessage($root, $timeout, true, "Please ensure that the 'date to' is greater than or equal to the 'date from'.", 3500);
                sheet.DateToString = null;
                return false;
            }
        }
    };

    $scope.ManualEntryTypeChanges = function (timesheet) {

        if (timesheet.TimesheetTimeType) {
            if (timesheet.TimesheetTimeType.Key) {
                if (timesheet.TimesheetTimeType.Key == $scope.SpecialOTTimeTypeID) {
                    timesheet.ApprovedOTHours = (parseFloat(timesheet.ApprovedOTHours) + parseFloat(timesheet.ApprovedNormalHours)).toFixed(2);
                    timesheet.ApprovedNormalHours = 0;
                }
            }
        }
    };

    $scope.ManualEntryHeaderCheckBoxChanges = function () {
        var checkBox = document.getElementById("IsSelectedToSave");
        if (checkBox.checked) {
            $scope.ManualEntryTimeSheetDatas.forEach(s => s.IsSelected = true);
        }
        else {
            $scope.ManualEntryTimeSheetDatas.forEach(s => s.IsSelected = false);
        }
    };

    $scope.UpdateManualEntriesStatus = function () {

        if ($scope.ManualEntryMaster.SelectedStatus && $scope.ManualEntryMaster.SelectedStatus.Key) {
            if ($scope.ManualEntryTimeSheetDatas.length > 0) {
                $scope.ManualEntryTimeSheetDatas.forEach(s => {
                    s.TimesheetApprovalStatus.Key = $scope.ManualEntryMaster.SelectedStatus.Key;
                    s.TimesheetApprovalStatus.Value = $scope.ManualEntryMaster.SelectedStatus.Value;
                });
            }
            else {
                $().showGlobalMessage($root, $timeout, true, "Need atleast one timesheet for status change!");
                $scope.ManualEntryMaster.SelectedStatus = {};
            }
        }
    };

    $scope.SaveManualEntriesTimesheetData = function () {
        if ($scope.ManualEntryTimeSheetDatas.length > 0) {

            var TimesheetsForSaving = [];

            var isError = false;
            var errorMessage = "";
            var messageTimeSec = 1000;

            $scope.ManualEntryTimeSheetDatas.forEach(s => {
                messageTimeSec = 1000;

                if (!s.Employee.Key) {
                    isError = true;
                    errorMessage = "Please select an employee!";
                    messageTimeSec = 2000;
                }
                else if (!s.DateFromString || !s.DateToString) {
                    isError = true;
                    errorMessage = "Please select dates!";
                    messageTimeSec = 2000;
                }
                else if (!s.ApprovedNormalHours && !s.ApprovedOTHours) {
                    isError = true;
                    errorMessage = "Please provide 'Normal hours' or 'OT hours' to save the selected entry, or uncheck that entry.";
                    messageTimeSec = 5000;
                }
                else if (!s.TimesheetApprovalStatus.Key) {
                    isError = true;
                    errorMessage = "Need status for save the selected entry, or uncheck that entry.";
                    messageTimeSec = 3000;
                }
                else {
                    TimesheetsForSaving.push(s);
                }
            });

            if (isError) {
                $().showGlobalMessage($root, $timeout, true, errorMessage, messageTimeSec);
                TimesheetsForSaving = [];
                return false;
            }

            if (TimesheetsForSaving.length > 0) {
                showManualEntryOverlay();

                var url = "Payroll/Employee/SaveSelectedTimesheetEntries";
                $http({
                    method: 'Post',
                    url: url,
                    data: TimesheetsForSaving
                }).then(function (result) {
                    if (result.data.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.data.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, result.data.Response);

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.LoadManualEntry();
                            });
                        }, 1000);
                    }
                    hideManualEntryOverlay();
                    return false;
                }, function () {
                    hideManualEntryOverlay();
                });
            }
            else {
                $().showGlobalMessage($root, $timeout, true, "Select atleast one timesheet for saving!");
                return false;
            }

        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Need atleast one selected entry for saving!");
            return false;
        }
    };
    //#endregion Manual entry tab codes end

    //#region Save manual entry data for each row, but currently, it is not being used - START
    $scope.SaveManualEntryRowData = function (timesheet) {
        if (!timesheet.Employee.Key) {
            $().showGlobalMessage($root, $timeout, true, "Please select an employee!");
            return false;
        }
        if (!timesheet.DateFromString || !timesheet.DateToString) {
            $().showGlobalMessage($root, $timeout, true, "Please select dates!");
            return false;
        }
        if (!timesheet.ApprovedNormalHours && !timesheet.ApprovedOTHours) {
            $().showGlobalMessage($root, $timeout, true, "Need normal hours or OT hours for save entry!", 2000);
            return false;
        }
        if (!timesheet.TimesheetApprovalStatus.Key) {
            $().showGlobalMessage($root, $timeout, true, "Need status for save entry!", 2000);
            return false;
        }

        showManualEntryOverlay();

        var url = "Payroll/Employee/SaveTimesheetManualEntryData";
        $http({
            method: 'Post',
            url: url,
            data: timesheet
        }).then(function (result) {
            if (result.data.IsError) {
                $().showGlobalMessage($root, $timeout, true, result.data.Response);
            }
            else {
                $().showGlobalMessage($root, $timeout, false, result.data.Response);

                $timeout(function () {
                    $scope.$apply(function () {
                        $scope.LoadManualEntry();
                    });
                }, 1000);
            }
            hideManualEntryOverlay();
            return false;
        }, function () {
            hideManualEntryOverlay();
        });
    };
    //#endregion manual entry data for each row saving - END


    //#region Overlay code Start
    function showPendingOverlay() {
        $('.preload-overlay', $('#Timesheet_Pending_Tab')).attr('style', 'display:block');
    };

    function hidePendingOverlay() {
        $('.preload-overlay', $('#Timesheet_Pending_Tab')).hide();
    };

    function showApprovedOverlay() {
        $('.preload-overlay', $('#Timesheet_Approved_Tab')).attr('style', 'display:block');
    };

    function hideApprovedOverlay() {
        $('.preload-overlay', $('#Timesheet_Approved_Tab')).hide();
    };

    function showManualEntryOverlay() {
        $('.preload-overlay', $('#Timesheet_Manual_Entry_Tab')).attr('style', 'display:block');
    };

    function hideManualEntryOverlay() {
        $('.preload-overlay', $('#Timesheet_Manual_Entry_Tab')).hide();
    };
    //#endregion Overlay code end

}]);