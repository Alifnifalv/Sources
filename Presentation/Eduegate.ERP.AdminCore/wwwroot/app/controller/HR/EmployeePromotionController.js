app.controller("EmployeePromotionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    console.log("EmployeePromotionController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });
       
    $scope.EmployeeChanges = function ($event, $element, viewvModel) {

        var model = viewvModel;
        //model.student = {};
        //model.Parent = {};
        //model.CurentBranch.Key = null;
        //model.CurentBranch.Value = null;
        //$scope.LookUps.CurentBranch = [];
        var url = "Schools/School/GetEmployeeDetailsByEmployeeID?employeeID=" + model.Employee.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
               
                if (result.data.length > 0) {
                    if (result.data[0].NewBranch != null) {
                        model.OldBranch.Key = result.data[0].NewBranch.Key;
                        model.OldBranch.Value = result.data[0].NewBranch.Value;
                        model.NewBranch.Key = result.data[0].NewBranch.Key;
                        model.NewBranch.Value = result.data[0].NewBranch.Value;
                        model.OldBranchID = Number(result.data[0].NewBranch.Key);
                        model.NewBranchID = Number(result.data[0].NewBranch.Key);                      
                    }

                    if (result.data[0].NewDesignation != null) {

                        model.OldDesignation.Key = result.data[0].NewDesignation.Key;
                        model.OldDesignation.Value = result.data[0].NewDesignation.Value;
                        model.NewDesignation.Key = result.data[0].NewDesignation.Key;
                        model.NewDesignation.Value = result.data[0].NewDesignation.Value;
                        model.OldDesignationID = Number(result.data[0].NewDesignation.Key);
                        model.NewDesignationID = Number(result.data[0].NewDesignation.Key);                     
                    }                   
                    if (result.data[0].SalaryStructure.PayrollFrequency != null) {
                        model.PayrollFrequency.Key = result.data[0].SalaryStructure.PayrollFrequency.Key;
                        model.PayrollFrequency.Value = result.data[0].SalaryStructure.PayrollFrequency.Value;
                    }
                    if (result.data[0].SalaryStructure.PaymentMode != null) {
                        model.PaymentMode.Key = result.data[0].SalaryStructure.PaymentMode.Key;
                        model.PaymentMode.Value = result.data[0].SalaryStructure.PaymentMode.Value;
                    }
                    if (result.data[0].SalaryStructure.Account != null) {
                        model.Account.Key = result.data[0].SalaryStructure.Account.Key;
                        model.Account.Value = result.data[0].SalaryStructure.Account.Value;
                    }
                    if (result.data[0].SalaryStructure.EmployeeSalaryStructure != null) {
                        model.EmployeeSalaryStructure.Key = result.data[0].SalaryStructure.EmployeeSalaryStructure.Key;
                        model.EmployeeSalaryStructure.Value = result.data[0].SalaryStructure.EmployeeSalaryStructure.Value;
                    }
                    if (result.data[0].NewLeaveGroup != null && result.data[0].NewLeaveGroup.Key !=null) {
                        model.PromotionLeaveDetails.OldLeaveGroup.Key = result.data[0].NewLeaveGroup.Key;
                        model.PromotionLeaveDetails.OldLeaveGroup.Value = result.data[0].NewLeaveGroup.Value;
                        model.PromotionLeaveDetails.NewLeaveGroup.Key = result.data[0].NewLeaveGroup.Key;
                        model.PromotionLeaveDetails.NewLeaveGroup.Value = result.data[0].NewLeaveGroup.Value;
                     
                    }
                    model.PromotionLeaveDetails.LeaveTypes = result.data[0].EmployeePromotionLeaveAllocs;

                    model.SalaryComponents = result.data[0].SalaryStructure.SalaryComponents;

                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

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

    $scope.LeaveGroupChanges = function ($event, $element, employeeModel) {
        var model = employeeModel;

        if (employeeModel.NewLeaveGroup == null || employeeModel.NewLeaveGroup == "" || model.NewLeaveGroup.Key ==null ) return false;
        showOverlay();

        if (model.IsOverrideLeaveGroup == false) {
            employeeModel.IsListDisable = false;
        }
        else {
            employeeModel.IsListDisable = true;
        }
        var url = "Payroll/Employee/LeaveGroupChanges?leaveGroupID=" + model.NewLeaveGroup?.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                employeeModel.LeaveTypes = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);
