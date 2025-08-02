app.controller("LSProvisionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {

    console.log("LSProvisionController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });
    $scope.LSProvisionDetail = [];
    $scope.AllEmployees = [];
    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }
    $scope.GetEmployeeByBranch = function (crudModel) {
       
        var model = crudModel;
        $scope.CRUDModel.ViewModel.Department = [];
        $scope.CRUDModel.ViewModel.Department.Key = [];
        $scope.CRUDModel.ViewModel.EmployeeList = [];
        $scope.LookUps.ActiveEmployees = [];
    };
    $scope.GenerateLeaveSalaryProvision = function (department, type) {

        if ($scope.CRUDModel.ViewModel.Branch == null || $scope.CRUDModel.ViewModel.Branch == undefined || $scope.CRUDModel.ViewModel.Department == null || $scope.CRUDModel.ViewModel.Department.Value == null || $scope.CRUDModel.ViewModel.EntryDateToString == null) {
            $().showMessage($scope, $timeout, true, "Enter  required fields!");
            return false;
        } else {
            showOverlay();
            $scope.LSProvisionDetail = [];
            $scope.CRUDModel.ViewModel.LSProvisionTab.LSProvisionDetail = []
            $.ajax({

                url: "Payroll/LSProvision/GenerateLeaveSalaryProvision",
                type: "POST",
                data: {
                    "EntryDateToString": $scope.CRUDModel.ViewModel.EntryDateToString,
                    "Department": $scope.CRUDModel.ViewModel.Department,
                    "EmployeeList": $scope.CRUDModel.ViewModel.EmployeeList,
                    "BranchID": $scope.CRUDModel.ViewModel.Branch,
                    "IsRegenerate": type == 1 ? true : false,
                    "SalaryComponentID": $scope.CRUDModel.ViewModel.LSSalaryComponentID,
                    "EmployeeLSProvisionHeadIID": $scope.CRUDModel.ViewModel.EmployeeLSProvisionHeadIID
                },
                success: function (result) {
                    if (result != null && result.IsError != true) {

                        if (result.Response != null && result.Response.LSProvisionTab.LSProvisionDetail) {
                            $scope.$apply(function () {
                                result.Response.LSProvisionTab.LSProvisionDetail.forEach(x => {
                                    $scope.LSProvisionDetail.push(
                                        {
                                            LastLeaveSalaryDateString: x.LastLeaveSalaryDateString,
                                            DOJString: x.DOJString,
                                            BasicSalary: x.BasicSalary,
                                            Category: x.Category,
                                            Department: x.Department,
                                            EmployeeCode: x.EmployeeCode,
                                            EmployeeName: x.EmployeeName,
                                            DOJ: x.DOJ,
                                            NoofLeaveSalaryDays: x.NoofLeaveSalaryDays,
                                            LeaveSalaryAmount: x.LeaveSalaryAmount,
                                            LastLeaveSalaryDate: x.LastLeaveSalaryDate,
                                            Balance: x.Balance,
                                            OpeningAmount: x.OpeningAmount,
                                            EmployeeLSProvisionHeadID: x.EmployeeLSProvisionHeadID,
                                            EmployeeLSProvisionDetailIID: x.EmployeeLSProvisionDetailIID,
                                        }
                                    );
                                });
                                $scope.CRUDModel.ViewModel.EntryNumber = result.Response.EntryNumber;
                                $scope.CRUDModel.ViewModel.EmployeeLSProvisionHeadIID = result.Response.EmployeeLSProvisionHeadIID;
                                $scope.CRUDModel.ViewModel.LSProvisionTab.LSProvisionDetail = $scope.LSProvisionDetail;

                            });
                            $().showMessage($scope, $timeout, false, 'Generated and Saved successfully');
                        }
                    }
                    else {
                        $().showMessage($scope, $timeout, true, result.Response);
                        return false;
                    }
                },
                complete: function (result) {
                    hideOverlay();
                }
            });
        }
    }

    $scope.GetEmployeesByDepartmentBranch = function (crudModel) {
        showOverlay();
        var model = crudModel;
        if (($scope.CRUDModel.ViewModel.Branch != null && $scope.CRUDModel.ViewModel.Branch != "" ) && ($scope.CRUDModel.ViewModel.Department == null || $scope.CRUDModel.ViewModel.Department == undefined || $scope.CRUDModel.ViewModel.Department.Value == null)) {
            $().showMessage($scope, $timeout, true, "Please select department!");
            hideOverlay();
            // return false;
        }
        if ($scope.CRUDModel.ViewModel.Branch == null || $scope.CRUDModel.ViewModel.Branch == undefined || $scope.CRUDModel.ViewModel.Branch == "") {
            $().showMessage($scope, $timeout, true, "Please select branch!");
            hideOverlay();
            return false;
        }
        if (($scope.CRUDModel.ViewModel.Department != null && $scope.CRUDModel.ViewModel.Department.Value != null) && ($scope.CRUDModel.ViewModel.Branch != null ||  $scope.CRUDModel.ViewModel.Branch != "")) {
            var url = "Payroll/LSProvision/GetEmployeesByDepartmentBranch?departmentID=" + $scope.CRUDModel.ViewModel.Department.Key + "&branchID=" + $scope.CRUDModel.ViewModel.Branch;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $scope.LookUps.ActiveEmployees = result.data;
                    hideOverlay();

                }, function () {

                    hideOverlay();
                });
        }
    };

    $scope.ViewLSProvisionReport = function (model) {

        if (model.EmployeeLSProvisionHeadIID === undefined || model.EmployeeLSProvisionHeadIID === null || model.EmployeeLSProvisionHeadIID === 0) {
            $().showGlobalMessage($root, $timeout, true, "Head ID is required to view report!");
            return false;
        }

        var reportName = "LSProvisionReport";
        var reportHeader = "LS Provisionl Report";

        if ($scope.ShowWindow(reportName, reportHeader, reportName) || model == null)
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var reportingService = $root.ReportingService;

        if (reportingService == "ssrs") {


            var parameter = "EmployeeLSProvisionHeadIID=" + model.EmployeeLSProvisionHeadIID;

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
            //SSRS report viewer end
        }
        else if (reportingService == 'bold') {

            //Bold reports viewer start
            var url = utility.myHost + 'ReportViewer/Index?reportName=' + reportName;
            $http({ method: 'Get', url })
                .then((result) => {
                    $('#' + reportName, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow(reportName, reportHeader, reportName);
                });
            //Bold reports viewer end
        }
        else {
            //New Report viewer start
            var parameterObject = {
                "EmployeeLSProvisionHeadIID": model.EmployeeLSProvisionHeadIID
            };

            // Convert to JSON format
            let parameterString = JSON.stringify(parameterObject);

            var reportUrl = utility.myHost + 'Reports/ReportView/ViewReports?reportName=' + reportName + "&parameter=" + parameterString;
            $.ajax({
                url: reportUrl,
                type: 'GET',
                success: function (result) {
                    $('#' + windowName, '#LayoutContentSection').replaceWith($compile(result)($scope))
                }
            });
            //New Report viewer end
        }
    };
    $scope.ViewJournalLS = function (model) {

        if (model.EmployeeLSProvisionHeadIID === undefined || model.EmployeeLSProvisionHeadIID === null || model.EmployeeLSProvisionHeadIID === 0) {
            $().showGlobalMessage($root, $timeout, true, "Head ID is required to view report!");
            return false;
        }

        var reportName = "JournalLSD";
        var reportHeader = "Journal - LS ";

        if ($scope.ShowWindow(reportName, reportHeader, reportName) || model == null)
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var reportingService = $root.ReportingService;

        if (reportingService == "ssrs") {


            var parameter = "HeadIID=" + model.EmployeeLSProvisionHeadIID;

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
            //SSRS report viewer end
        }
        else if (reportingService == 'bold') {

            //Bold reports viewer start
            var url = utility.myHost + 'ReportViewer/Index?reportName=' + reportName;
            $http({ method: 'Get', url })
                .then((result) => {
                    $('#' + reportName, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow(reportName, reportHeader, reportName);
                });
            //Bold reports viewer end
        }
        else {
            //New Report viewer start
            var parameterObject = {
                "HeadIID": model.EmployeeLSProvisionHeadIID
            };

            // Convert to JSON format
            let parameterString = JSON.stringify(parameterObject);

            var reportUrl = utility.myHost + 'Reports/ReportView/ViewReports?reportName=' + reportName + "&parameter=" + parameterString;
            $.ajax({
                url: reportUrl,
                type: 'GET',
                success: function (result) {
                    $('#' + windowName, '#LayoutContentSection').replaceWith($compile(result)($scope))
                }
            });
            //New Report viewer end
        }
    };
    $scope.AccountPosting = function (model) {

        if (model.EmployeeLSProvisionHeadIID != undefined && model.EmployeeLSProvisionHeadIID != null && model.EmployeeLSProvisionHeadIID != 0) {

            showOverlay();
            var referenceID = model.EmployeeLSProvisionHeadIID;
            $http({
                method: 'GET',
                url: utility.myHost + "Payroll/LSProvision/AccountPosting",
                params: {
                    referenceIDs: referenceID,
                    documentTypeID: model.DocumentType != null ? model.DocumentType.Key : null
                }
            }).then(function (result) {
                if (result.data) {
                    $().showMessage($scope, $timeout, false, "Account posting has been completed!");

                    hideOverlay();
                    return false;
                }
                else {
                    $().showMessage($scope, $timeout, true, "Account posting has failed!");

                    hideOverlay();
                    return false;
                }
            }, function () {
                hideOverlay();
            });
        }
        else {
            $().showMessage($scope, $timeout, true, "Account posting only possible if the entries are saved!", 2000);
            return false;
        }
    };

}]);