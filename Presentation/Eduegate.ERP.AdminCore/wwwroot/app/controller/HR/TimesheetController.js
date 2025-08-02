app.controller("TimesheetController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("TimesheetController Loaded");

    $scope.CalendarEntryDetails = [];

    $scope.FillTimesheetsData = function (model) {

        var empid = model?.Employee?.Key;

        if (!empid) {
            $().showMessage($scope, $timeout, true, "Select an employee!");
            return false;
        }

        if (!model.CollectionDateFromString || !model.CollectionDateToString) {
            $().showMessage($scope, $timeout, true, "Fill out required fields Date From & Date To!");
            return false;
        }

        showOverlay();
        var url = "Payroll/Employee/GetCollectTimeSheetsData?employeeID=" + empid + "&fromDate=" + model.CollectionDateFromString + "&toDate=" + model.CollectionDateToString;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                model.TimeSheets = result.data.Response;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.EmployeeChanges = function (model) {

        var empid = model.Employee.Key;

        if (!empid) {
            $().showMessage($scope, $timeout, true, "Please select employee!");
            return false;
        }

        showOverlay();
        var url = "Payroll/Employee/GetWorkingHourDetailsByEmployee?employeeID=" + empid;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.data.IsError) {
                    $scope.CalendarEntryDetails = result.data.Response;
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.TimesheetTimeChanges = function (masterModel, gridModel) {

        gridModel.NormalHours = 0;
        gridModel.OTHours = 0;

        gridModel.OldNormalHours = 0;
        gridModel.OldOTHours = 0;

        if (!masterModel.Employee || !masterModel.Employee.Key) {
            $().showMessage($scope, $timeout, true, "Please select an employee!");
            return false;
        }
        else if (!gridModel.TimesheetDateString) {
            $().showMessage($scope, $timeout, true, "Please select date!");
            return false;
        }
        else if (!gridModel.FromTimeString) {
            $().showMessage($scope, $timeout, true, "Please select from time!");
            return false;
        }
        else if (!gridModel.ToTimeString) {
            $().showMessage($scope, $timeout, true, "Please select to time!");
            return false;
        }
        else {
            // Two timestamp values
            const startTime = new Date("01/01/1970 " + gridModel.FromTimeString);
            const endTime = new Date("01/01/1970 " + gridModel.ToTimeString);

            // Calculate the time difference in milliseconds
            const timeDiffMs = endTime.getTime() - startTime.getTime();

            // Calculate the time difference in hours
            const timeDiffHours = timeDiffMs / (1000 * 60 * 60);

            if (timeDiffHours < 0) {

                gridModel.NormalHours = 0;
                gridModel.OTHours = 0;

                gridModel.OldNormalHours = 0;
                gridModel.OldOTHours = 0;

                $().showMessage($scope, $timeout, true, "Please select an end time that is greater than the start time.");
                return false;
            }
            else {
                $().hideMessage();
            }

            if ($scope.CalendarEntryDetails == null || $scope.CalendarEntryDetails.length == 0) {

                $scope.GetWorkingHourDetailsAndFillHours(masterModel, gridModel, timeDiffHours);
            }
            else {
                $scope.FillHoursAndDetails(masterModel, gridModel, timeDiffHours);
            }

            return false;
        }

    };

    $scope.GetWorkingHourDetailsAndFillHours = function (masterModel, gridModel, timeDiffHours) {

        var empid = masterModel.Employee.Key;

        var url = "Payroll/Employee/GetWorkingHourDetailsByEmployee?employeeID=" + empid;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.data.IsError) {
                    $scope.CalendarEntryDetails = result.data.Response;

                    $scope.FillHoursAndDetails(masterModel, gridModel, timeDiffHours);
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillHoursAndDetails = function (masterModel, gridModel, timeDiffHours) {

        var selectedDateCalendarData = $scope.CalendarEntryDetails.find(x => x.CalendarDateString == gridModel.TimesheetDateString);
        var dateNormalHrs = 0;
        if (selectedDateCalendarData != null) {
            var workingHr = selectedDateCalendarData.NoofHours;

            if (masterModel.TimeSheets.length > 1) {

                masterModel.TimeSheets.forEach(sheet => {
                    if (sheet.TimesheetDateString == gridModel.TimesheetDateString) {
                        dateNormalHrs += sheet.NormalHours;
                    }
                });
            }

            if (workingHr == dateNormalHrs) {
                gridModel.OTHours = timeDiffHours;
            }
            else {
                var balanceworkingHr = workingHr - dateNormalHrs;

                var lastTimeDiff = 0;

                if (balanceworkingHr >= timeDiffHours) {
                    lastTimeDiff = timeDiffHours;
                }
                else {
                    lastTimeDiff = balanceworkingHr - timeDiffHours;
                }

                if (timeDiffHours != 0) {
                    if (lastTimeDiff >= 0) {
                        gridModel.NormalHours = parseFloat(lastTimeDiff.toFixed(2));

                        gridModel.OldNormalHours = parseFloat(lastTimeDiff.toFixed(2));
                    }
                    else {
                        gridModel.NormalHours = parseFloat(balanceworkingHr.toFixed(2));
                        gridModel.OTHours = parseFloat(Math.abs(lastTimeDiff.toFixed(2)));

                        gridModel.OldNormalHours = parseFloat(balanceworkingHr.toFixed(2));
                        gridModel.OldOTHours = parseFloat(Math.abs(lastTimeDiff.toFixed(2)));
                    }
                }
                else {
                    gridModel.NormalHours = 0;
                    gridModel.OTHours = 0;

                    gridModel.OldNormalHours = 0;
                    gridModel.OldOTHours = 0;
                }
            }

        }

        if (gridModel.TimesheetTimeType) {
            if (gridModel.TimesheetTimeType == masterModel.SpecialOTTimeTypeID) {
                $scope.TimesheetTimeTypeChanges(masterModel, gridModel);
            }
        }
    }

    $scope.TimesheetTimeTypeChanges = function (masterModel, gridModel) {
        if (gridModel.TimesheetTimeType) {
            if (gridModel.TimesheetTimeType == masterModel.SpecialOTTimeTypeID) {
                gridModel.OTHours += gridModel.NormalHours;
                gridModel.NormalHours = 0;
            }
            else {
                gridModel.NormalHours = gridModel.OldNormalHours;
                gridModel.OTHours = gridModel.OldOTHours;
            }
        }
        else {
            gridModel.NormalHours = gridModel.OldNormalHours;
            gridModel.OTHours = gridModel.OldOTHours;
        }
    }

    $scope.FillEmployeeData = function (masterModel) {

        if (masterModel.IsSelf) {
            var employeeID = masterModel?.Employee?.Key;

            if (!employeeID) {
                showOverlay();
                var url = "Payroll/Employee/GetEmployeeDatasByLogin";
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        if (!result.data.IsError) {
                            if (result.data.Response.EmployeeIID != 0) {
                                masterModel.Employee.Key = result.data.Response.EmployeeIID;
                                masterModel.Employee.Value = result.data.Response.EmployeeCode + " - " + result.data.Response.EmployeeName;
                            }
                        }
                        hideOverlay();
                    }, function () {
                        hideOverlay();
                    });
            }
        }
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    };

}]);