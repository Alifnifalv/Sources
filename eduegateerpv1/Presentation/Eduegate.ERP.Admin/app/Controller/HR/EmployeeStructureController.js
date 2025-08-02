app.controller("EmployeeStructureController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    console.log("EmployeeStructureController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });



    $scope.SalaryStructureChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Payroll/Employee/GetEmployeeComponents?SalaryStructureID=" + model.SalaryStructure.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.SalaryComponents = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.GenerateSalarySlip = function (department, type) {

        if ($scope.CRUDModel.ViewModel.Department == null || $scope.CRUDModel.ViewModel.SlipDateString == null) {
            $().showMessage($scope, $timeout, true, "Enter  required fields!");
            return false;
        } else {
            showOverlay();
            $.ajax({

                //url: utility.myHost + "Payroll/Employee/SaveSalarySlip",
                url: "Payroll/Employee/GenerateSalarySlip",
                type: "POST",
                data: {
                    "SalarySlipIID": $scope.CRUDModel.ViewModel.SalarySlipIID,
                    "SlipDateString": $scope.CRUDModel.ViewModel.SlipDateString,
                    "Department": $scope.CRUDModel.ViewModel.Department,
                    "Employee": $scope.CRUDModel.ViewModel.Employee,
                    "IsRegenerate": type == 1 ? true : false
                },
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $().showMessage($scope, $timeout, result.IsFailed, result.Message);
                    }
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    }

    $scope.GetTotalSalaryAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }
        var sum = 0;

        for (var i = data.length - 1; i >= 0; i--) {
            if ((data[i].Earnings)) {
                sum += parseFloat(data[i].Earnings);
            }

            if ((data[i].Deduction)) {
                sum += parseFloat(data[i].Deduction) * -1;
            }
        }
        sum = sum.toFixed(2);
        return sum;
    };

    $scope.ModifySalarySlip = function (model) {

        if ($scope.CRUDModel.ViewModel.SlipDateString == null) {
            $().showMessage($scope, $timeout, true, "Enter required fields!");
            return false;
        } else {
            showOverlay();

            if (model.SlipComponents.length > 0) {
                var mainDataList = {};
                var componentList = [];

                model.SlipComponents.forEach(comp => {

                    componentList.push({
                        "SalarySlipIID" : comp.SalarySlipIID,
                        "SalaryComponentID": comp.SalaryComponentID,                       
                        "Deduction": comp.Deduction,
                        "Earnings": comp.Earnings,
                        "Description": comp.Description,
                        "NoOfDays": comp.NoOfDays,
                        "NoOfHours": comp.NoOfHours,
                        "ReportContentID": comp.ReportContentID,
                    });

                });
                //mainDataList{
                //    "SalarySlipIID": $scope.CRUDModel.ViewModel.SalarySlipIID,
                //   // "SlipDateString": $scope.CRUDModel.ViewModel.SlipDateString,
                //    "EmployeeID": $scope.CRUDModel.ViewModel.EmployeeID,
                //    "SalaryComponent": componentList
                //});
            }
            $.ajax({

                //url: utility.myHost + "Payroll/Employee/SaveSalarySlip",
                url: "Payroll/Employee/ModifySalarySlip",
                type: "POST",
                data: {
                    "SalarySlipIID": $scope.CRUDModel.ViewModel.SalarySlipIID,
                    "SlipDateString": $scope.CRUDModel.ViewModel.SlipDateString,
                    "EmployeeID": $scope.CRUDModel.ViewModel.EmployeeID,
                    "EmployeeCode": $scope.CRUDModel.ViewModel.EmployeeCode,
                    "BranchID": $scope.CRUDModel.ViewModel.BranchID,
                    "SalaryComponent": componentList
                },
                success: function (result) {
                    if (!result.IsError && result != null) {

                        $().showMessage($scope, $timeout, result.IsFailed, result.Message);
                    }
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    }


    $scope.GetTotalAmount = function (data) {
        if (typeof (data) === 'undefined') {
            return 0;
        }
        var sum = 0;

        for (var i = data.length - 1; i >= 0; i--) {
            if ((data[i].Earnings)) {
                sum += parseFloat(data[i].Earnings);
            }

            if ((data[i].Deduction)) {
                sum += parseFloat(data[i].Deduction) * -1;
            }
        }
        sum = sum.toFixed(2);
        return sum;
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);
