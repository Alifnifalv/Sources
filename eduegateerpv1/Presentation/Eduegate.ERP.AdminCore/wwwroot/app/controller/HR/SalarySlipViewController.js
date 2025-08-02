app.controller("SalarySlipViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("SalarySlipViewController Loaded");

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));
    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.EditSalarySlip = function (salarySlipIID) {
        var windowName = 'EditSalarySlip';
        var viewName = 'EditSalarySlip';

        if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + viewName + "&ID=" + salarySlipIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
            });

    };

    $scope.OtherComponentsChanges = function ($event, $element, gridModel) {
        //showOverlay();
        var model = gridModel;
        if (model.OtherComponents[0].Value != null) {
            model.OtherComponentString = model.OtherComponents[0].Value;
        }
        else {
            return false;
        }
    }

    $scope.OperatorsChanges = function ($event, $element, gridModel) {
        //showOverlay();
        var model = gridModel;
        if (model.Operators[0].Value != null) {
            model.OperatorString = model.Operators[0].Value;
        }
        else {
            return false;
        }
    }

    $scope.VariablesChanges = function ($event, $element, gridModel) {
        //showOverlay();
        var model = gridModel;
        if (model.Variables[0].Value != null) {
            model.VariablesString = model.Variables[0].Value;
            model.Expression = model.OtherComponentString + model.OperatorString + model.VariablesString;
            model.OperatorString = null;
            model.OtherComponentString = null;
            model.VariablesString = null;
        }
        else {
            return false;
        }
    }

    $scope.DepartmentChange = function ($event, $element, crudModel) {
        var model = crudModel;
        var departmentId = $scope.CRUDModel.ViewModel.Department?.Key;
        var Month = model.Months.Key;
        var Year = model.Years.Key;
        if (Month == undefined || Month == null || Month == "") {
            $().showMessage($scope, $timeout, true, "Please select Month !");
            return false;
        }
        if (Year == undefined || Year == null || Year == "") {
            $().showMessage($scope, $timeout, true, "Please select Year !");
            return false;
        }
        if (departmentId == undefined || departmentId == null || departmentId == "")
        {
            var departmentId = "0";
        }
        var url = "Payroll/Employee/GetSalarySlipEmployeeData?departmentId=" + departmentId + "&Month=" + Month + "&Year=" + Year;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.EmployeeList = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.PrintSlipReport = function () {

        var headIID = null;
        var reportFullName = '%2fEduegate.Inventory.Reports%2fSalarySlipReport';
        var model = $scope.CRUDModel.ViewModel.EmployeeList;
        for (i = 0; i < model.length; i++) {
            if (model[i].IsSelected) {
                headIID = model[i].SalarySlipIID;
                $http({ method: 'GET', url: "Home/ViewReports?returnFileBytes=true&SalarySlipIID=" + headIID + "&reportName=" + reportFullName })
                    .then(function (filename) {
                        var w = window.open();
                        w.document.write('<iframe onload="isLoaded()" id="pdf" name="pdf" src="' + filename.data + '"></iframe><script>function isLoaded(){window.frames[\"pdf\"].print();}</script>');
                    });
            }
        }


    };


    $scope.MailReport = function () {

        var reportFullName = '%2fEduegate.Inventory.Reports%2fSalarySlipReport';
        var headIID = null;
        var emailID = null;
        var model = $scope.CRUDModel.ViewModel.EmployeeList ;
        for (i = 0; i < model.length; i++) {
            if (model[i].IsSelected) {
                headIID = model[i].SalarySlipIID;
                emailID = emailID = model[i].EmailAddress;

                if (emailID == null || emailID == '-') {
                    $().showMessage($scope, $timeout, true, "Please update with valid Email ID");
                    return false;
                }
                else {
                    
                }
            }
        }
    }

}]);
