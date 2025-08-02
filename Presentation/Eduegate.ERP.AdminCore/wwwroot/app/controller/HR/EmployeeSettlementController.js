app.controller("EmployeeSettlementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {

    console.log("EmployeeSettlementController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });



    $scope.CalculationDateChanges = function (model, employeeSalarySettlementTypeID) {
        showOverlay();

        var url = "Payroll/EmployeeLeaveSettlement/GetSettlementDateDetails?salaryCalculationDateString=" + model.SalaryCalculationDateString + "&employeeSalarySettlementTypeID=" + employeeSalarySettlementTypeID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result != null && result.data != null && result.data.IsError != true) {
                    if (employeeSalarySettlementTypeID == 2) {
                        model.EndofServiceDays = result.data.Response.EndofServiceDays;
                        model.NoofDaysInTheMonthLS = result.data.Response.NoofDaysInTheMonthLS;
                        model.NoofDaysInTheMonthEoSB = result.data.Response.NoofDaysInTheMonthEoSB;
                    }
                    else {
                        model.NoofDaysInTheMonthLS = result.data.Response.NoofDaysInTheMonthLS;
                    }
                    model.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth = result.data.Response.NoofDaysInTheMonth;

                }
                else {
                    $().showMessage($scope, $timeout, true, result.data.Response);
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
        return true;
    };


    $scope.FillDetails = function (gridModel, employeeSalarySettlementTypeID) {

        var model = gridModel;
        $scope.SalaryComponents = [];
        if (model.NoofDaysInTheMonth == null)
            model.NoofDaysInTheMonth = 0;

        if (model.SalaryCalculationDateString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary Calculation Date!");
            return false;
        }
        if (model.LeaveSalarySettlementDateToString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary Settlement Date!");
            return false;
        }
        if (model.Employee.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select employee!");

            return false;
        }
        else {
            showOverlay();
            var url = "Payroll/EmployeeLeaveSettlement/GetEmployeeDetailsToSettlement?employeeID=" + model.Employee.Key + "&salaryCalculationDateString=" + model.SalaryCalculationDateString + "&noofDaysInTheMonth=" + model.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth + "&noofDaysInTheMonthLS=" + model.NoofDaysInTheMonthLS + "&employeeSalarySettlementTypeID=" + employeeSalarySettlementTypeID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result != null && result.data != null && result.data.IsError != true) {

                        model.BranchID = result.data.Response.BranchID;
                        model.DateOfJoining = result.data.Response.DateOfJoining;
                        model.DateOfJoiningString = result.data.Response.DateOfJoiningString;
                        model.EmployeeCode = result.data.Response.EmployeeCode;
                        model.Remarks = result.data.Response.Remarks;
                        model.MCTabEmployeeLeaveDatails.DateOfJoining = result.data.Response.MCTabEmployeeLeaveDatails.DateOfJoining;
                        model.MCTabEmployeeLeaveDatails.DateOfJoiningString = result.data.Response.MCTabEmployeeLeaveDatails.DateOfJoiningString;
                        model.MCTabEmployeeLeaveDatails.LeaveDueFrom = result.data.Response.MCTabEmployeeLeaveDatails.LeaveDueFrom;
                        model.MCTabEmployeeLeaveDatails.LeaveStartDate = result.data.Response.MCTabEmployeeLeaveDatails.LeaveStartDate;
                        model.MCTabEmployeeLeaveDatails.LeaveEndDate = result.data.Response.MCTabEmployeeLeaveDatails.LeaveEndDate;
                        model.MCTabEmployeeLeaveDatails.LeaveDueFromString = result.data.Response.MCTabEmployeeLeaveDatails.LeaveDueFromString;
                        model.MCTabEmployeeLeaveDatails.LeaveStartDateString = result.data.Response.MCTabEmployeeLeaveDatails.LeaveStartDateString;
                        model.MCTabEmployeeLeaveDatails.LeaveEndDateString = result.data.Response.MCTabEmployeeLeaveDatails.LeaveEndDateString;
                        model.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements = result.data.Response.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements;
                        model.MCTabEmployeeLeaveDatails.EarnedLeave = result.data.Response.MCTabEmployeeLeaveDatails.EarnedLeave;
                        model.MCTabEmployeeLeaveDatails.LossofPay = result.data.Response.MCTabEmployeeLeaveDatails.LossofPay;
                        model.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth = result.data.Response.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth;
                        model.MCTabEmployeeLeaveDatails.NoofSalaryDays = result.data.Response.MCTabEmployeeLeaveDatails.NoofSalaryDays;
                        result.data.Response.MCTabSalaryStructure.SalaryComponents.forEach(x => {
                            $scope.SalaryComponents.push(
                                {
                                    Deduction: x.Deduction,
                                    Earnings: x.Earnings,
                                    Description: x.Description,
                                    NoOfDays: x.NoOfDays,
                                    SalaryComponentID: x.SalaryComponentID,
                                    SalaryComponent: x.SalaryComponent,
                                }
                            );
                        });

                        model.MCTabSalaryStructure.SalaryComponents = $scope.SalaryComponents;
                    }
                    else {
                        $().showMessage($scope, $timeout, true, result.data.Response);
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };

    $scope.FillFinalSettlementDetails = function (gridModel, employeeSalarySettlementTypeID) {

        var model = gridModel;
        $scope.SalaryComponents = [];
        if (model.NoofDaysInTheMonth == null)
            model.NoofDaysInTheMonth = 0;

        if (model.SalaryCalculationDateString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary Calculation Date!");
            return false;
        }
        if (model.LeaveSalarySettlementDateToString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary Settlement Date!");
            return false;
        }
        if (model.DateOfLeavingString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Date Of Leaving!");
            return false;
        }
        if (model.Employee.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select employee!");
            return false;
        }
        else {
            showOverlay();
            var url = "Payroll/EmployeeLeaveSettlement/GetEmployeeDetailsToFinalSettlement?employeeID=" + model.Employee.Key + "&salaryCalculationDateString=" + model.SalaryCalculationDateString + "&dateOfLeavingString=" + model.DateOfLeavingString + "&endofServiceDays=" + model.EndofServiceDays + "&noofDaysInTheMonth=" + model.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth + "&noofDaysInTheMonthLS=" + model.NoofDaysInTheMonthLS + "&noofDaysInTheMonthEoSB=" + model.NoofDaysInTheMonthEoSB + "&employeeSalarySettlementTypeID=" + employeeSalarySettlementTypeID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (result != null && result.data != null && result.data.IsError != true) {

                        model.BranchID = result.data.Response.BranchID;
                        model.EmployeeCode = result.data.Response.EmployeeCode;
                        model.Remarks = result.data.Response.Remarks;

                        model.MCTabEmployeeLeaveDatails.DateOfJoining = result.data.Response.MCTabEmployeeLeaveDatails.DateOfJoining;
                        model.MCTabEmployeeLeaveDatails.DateOfJoiningString = result.data.Response.MCTabEmployeeLeaveDatails.DateOfJoiningString;
                        model.MCTabEmployeeLeaveDatails.LeaveDueFrom = result.data.Response.MCTabEmployeeLeaveDatails.LeaveDueFrom;
                        model.MCTabEmployeeLeaveDatails.LeaveStartDate = result.data.Response.MCTabEmployeeLeaveDatails.LeaveStartDate;
                        model.MCTabEmployeeLeaveDatails.LeaveEndDate = result.data.Response.MCTabEmployeeLeaveDatails.LeaveEndDate;
                        model.MCTabEmployeeLeaveDatails.LeaveDueFromString = result.data.Response.MCTabEmployeeLeaveDatails.LeaveDueFromString;
                        model.MCTabEmployeeLeaveDatails.LeaveStartDateString = result.data.Response.MCTabEmployeeLeaveDatails.LeaveStartDateString;
                        model.MCTabEmployeeLeaveDatails.LeaveEndDateString = result.data.Response.MCTabEmployeeLeaveDatails.LeaveEndDateString;
                        model.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements = result.data.Response.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements;
                        model.MCTabEmployeeLeaveDatails.EarnedLeave = result.data.Response.MCTabEmployeeLeaveDatails.EarnedLeave;
                        model.MCTabEmployeeLeaveDatails.LossofPay = result.data.Response.MCTabEmployeeLeaveDatails.LossofPay;
                        model.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth = result.data.Response.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth;
                        model.MCTabEmployeeLeaveDatails.NoofSalaryDays = result.data.Response.MCTabEmployeeLeaveDatails.NoofSalaryDays;
                        result.data.Response.MCTabSalaryStructure.SalaryComponents.forEach(x => {
                            $scope.SalaryComponents.push(
                                {
                                    Deduction: x.Deduction,
                                    Earnings: x.Earnings,
                                    Description: x.Description,
                                    NoOfDays: x.NoOfDays,
                                    SalaryComponentID: x.SalaryComponentID,
                                    SalaryComponent: x.SalaryComponent,
                                }
                            );
                        });

                        model.MCTabSalaryStructure.SalaryComponents = $scope.SalaryComponents;
                    }
                    else {
                        $().showMessage($scope, $timeout, true, result.data.Response);
                    }
                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };
    $scope.SaveSalarySettlement = function (gridModel, employeeSalarySettlementTypeID) {

        var model = gridModel;
        if (model.SalaryCalculationDateString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary Calculation Date!");
            return false;
        }
        if (model.LeaveSalarySettlementDateToString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary settlement Date!");
            return false;
        }
        if (model.Employee.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select employee!");
            return false;
        }
        if (model.Employee.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select employee!");
            return false;
        }
        if (model.MCTabSalaryStructure.SalaryComponents.length == 0 || model.MCTabSalaryStructure.SalaryComponents[0].SalaryComponentID == null) {
            $().showMessage($scope, $timeout, true, "Fill the Salary components!");
            return false;
        }
        else {
            showOverlay();

            if (model.MCTabSalaryStructure.SalaryComponents.length > 0) {
                var mainDataList = {};
                var componentList = [];
                model.MCTabSalaryStructure.SalaryComponents.forEach(comp => {
                    componentList.push({

                        "SalaryComponentID": comp.SalaryComponentID,
                        "DeductingAmount": comp.Deduction,
                        "EarningAmount": comp.Earnings,
                        "Description": comp.Description,
                        "NoOfDays": comp.NoOfDays,
                    });

                });
            }
            $.ajax({

                url: "Payroll/EmployeeLeaveSettlement/SaveSalarySettlement",
                type: "POST",
                data: {
                    "BranchID": $scope.CRUDModel.ViewModel.BranchID,
                    "EmployeeCode": $scope.CRUDModel.ViewModel.EmployeeCode,
                    "EmployeeID": $scope.CRUDModel.ViewModel.Employee.Key,
                    "SalaryCalculationDateString": $scope.CRUDModel.ViewModel.SalaryCalculationDateString,
                    "EmployeeSettlementDateString": $scope.CRUDModel.ViewModel.LeaveSalarySettlementDateToString,
                    "LeaveDueFromString": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LeaveDueFromString,
                    "LeaveStartDateString": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LeaveStartDateString,
                    "LeaveEndDateString": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LeaveEndDateString,
                    "AnnualLeaveEntitilements": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements,
                    "EarnedLeave": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.EarnedLeave,
                    "LossofPay": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LossofPay,
                    "NoofDaysInTheMonth": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth,
                    "NoofSalaryDays": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.NoofSalaryDays,
                    "SalarySlipDTOs": componentList,
                    "EmployeeSettlementTypeID": employeeSalarySettlementTypeID
                },
                success: function (result) {
                    console.log('Redirecting is successful:', result); // Log the success result

                    if (result != null) {
                        $().showGlobalMessage($root, $timeout, result.IsFailed, result.Message);
                    }
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    }

    $scope.SaveFinalSalarySettlement = function (gridModel, employeeSalarySettlementTypeID) {

        var model = gridModel;
        if (model.SalaryCalculationDateString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary Calculation Date!");
            return false;
        }
        if (model.LeaveSalarySettlementDateToString == null) {
            $().showMessage($scope, $timeout, true, "Please enter Salary settlement Date!");
            return false;
        }
        if (model.Employee.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select employee!");
            return false;
        }
        if (model.MCTabSalaryStructure.SalaryComponents.length == 0 || model.MCTabSalaryStructure.SalaryComponents[0].SalaryComponentID == null) {
            $().showMessage($scope, $timeout, true, "Fill the Salary components!");
            return false;
        }
        else {
            showOverlay();

            if ($scope.CRUDModel.ViewModel.MCTabSalaryStructure.SalaryComponents.length > 0) {
                var mainDataList = {};
                var componentList = [];
                model.MCTabSalaryStructure.SalaryComponents.forEach(comp => {
                    componentList.push({

                        "SalaryComponentID": comp.SalaryComponentID,
                        "DeductingAmount": comp.Deduction,
                        "EarningAmount": comp.Earnings,
                        "Description": comp.Description,
                        "NoOfDays": comp.NoOfDays,
                    });

                });
            }
            $.ajax({

                url: "Payroll/EmployeeLeaveSettlement/SaveSalarySettlement",
                type: "POST",
                data: {
                    "BranchID": $scope.CRUDModel.ViewModel.BranchID,
                    "EmployeeCode": $scope.CRUDModel.ViewModel.EmployeeCode,
                    "EmployeeID": $scope.CRUDModel.ViewModel.Employee.Key,
                    "DateOfLeavingString": $scope.CRUDModel.ViewModel.DateOfLeavingString,
                    "NoofDaysInTheMonthEoSB": $scope.CRUDModel.ViewModel.NoofDaysInTheMonthEoSB,
                    "EndofServiceDaysPerYear": $scope.CRUDModel.ViewModel.EndofServiceDays,
                    "SalaryCalculationDateString": $scope.CRUDModel.ViewModel.SalaryCalculationDateString,
                    "EmployeeSettlementDateString": $scope.CRUDModel.ViewModel.LeaveSalarySettlementDateToString,
                    "LeaveDueFromString": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LeaveDueFromString,
                    "LeaveStartDateString": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LeaveStartDateString,
                    "LeaveEndDateString": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LeaveEndDateString,
                    "AnnualLeaveEntitilements": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements,
                    "EarnedLeave": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.EarnedLeave,
                    "LossofPay": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.LossofPay,
                    "NoofDaysInTheMonth": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth,
                    "NoofDaysInTheMonthLS": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.NoofDaysInTheMonthLS,
                    "NoofSalaryDays": $scope.CRUDModel.ViewModel.MCTabEmployeeLeaveDatails.NoofSalaryDays,
                    "SalarySlipDTOs": componentList,
                    "EmployeeSettlementTypeID": employeeSalarySettlementTypeID
                },
                success: function (result) {
                    if (!result.IsError && result != null) {
                        $().showGlobalMessage($root, $timeout, result.IsFailed, result.Message);
                    }
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    }
    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);
