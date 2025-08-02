app.controller("WorkFlowController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {

    $scope.Model;

    $scope.windowName;

    $scope.QuotaTypes = [];

    $scope.VisaCompany = [];

    $scope.StatusAction = [];

    //$scope.VisaCompanySelected = { Key: "", Value: "" };
    //$scope.QuotaTypeSelected = { Key: "", Value: "" };
    //$scope.ActionSelected = { Key: "", Value: "" };

    var windowContainer = null;
    $scope.init = function (windowName, model) {
        $scope.Model = model;
        $scope.windowName = windowName;
        windowContainer = '#' + windowName;

        

        //if ($scope.Model.MasterViewModel.QuotaType != null && $scope.Model.MasterViewModel.QuotaType != undefined && $scope.Model.MasterViewModel.QuotaType.Key != '') {
        //    $scope.QuotaTypeSelected.Key = $scope.Model.MasterViewModel.QuotaType.Key;
        //    $scope.QuotaTypeSelected.Value = $scope.Model.MasterViewModel.QuotaType.Value;
        //}

        //if ($scope.Model.MasterViewModel.VisaCompany != null && $scope.Model.MasterViewModel.VisaCompany != undefined && $scope.Model.MasterViewModel.VisaCompany.Key != '') {
        //    $scope.VisaCompanySelected.Key = $scope.Model.MasterViewModel.VisaCompany.Key;
        //    $scope.VisaCompanySelected.Value = $scope.Model.MasterViewModel.VisaCompany.Value;
        //}

        //if ($scope.Model.MasterViewModel.EmpProcessRequestStatus != null && $scope.Model.MasterViewModel.EmpProcessRequestStatus != undefined && $scope.Model.MasterViewModel.EmpProcessRequestStatus.Key != '') {
        //    $scope.ActionSelected.Key = $scope.Model.MasterViewModel.EmpProcessRequestStatus.Key;
        //    $scope.ActionSelected.Value = $scope.Model.MasterViewModel.EmpProcessRequestStatus.Value;
        //}

        $scope.GetQuotaType();

        $scope.GetVisaCompany();

        $scope.GetProcessStatus();
    }

    $scope.GetQuotaType = function () {
        $.ajax({
            url: "EmploymentRequest/GetQuotaType",
            type: 'GET',
            success: function (result) {
                $timeout(function () {
                    $scope.QuotaTypes = result;
                });

            }
        })
    }

    $scope.GetProcessStatus = function () {
        $.ajax({
            url: "EmploymentRequest/GetEmploymentRequestStatus?empReqNo=" + $scope.Model.MasterViewModel.EMP_REQ_NO,
            type: 'GET',
            success: function (result) {
                $timeout(function () {
                    $scope.StatusAction = result;
                });

            }
        })
    }

    $scope.GetVisaCompany = function () {
        $.ajax({
            url: "EmploymentRequest/GetVisaCompany?deptNo=" + $scope.Model.MasterViewModel.Shop.Key,
            type: 'GET',
            success: function (result) {
                $scope.VisaCompany = result;
            }
        })
    }

    $scope.Save = function (event) {
       
        var form = $(event.currentTarget).closest("form");
        if (!form.validateForm())
            return false;
        $('#gridpopupoverlay').show();
        $.ajax({
            type: "POST",
            url: 'EmploymentRequest/SaveWorkFlow',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify($scope.Model),
            success: function (result) {
                if (result.IsError) {
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                    return;
                }
                else {
                    $().showMessage($scope, $timeout, false, "Sucessfully saved.");
                }
            },
            error: function (error) {
                $().showMessage($scope, $timeout, true, error.responseText);
            },
            complete: function () {
                $('#gridpopupoverlay').show();
            }
        });
    }


    //$scope.OnVisaCompanyChange = function (object) {
    //    if (object == null || object == undefined) {
    //        $scope.Model.MasterViewModel.VisaCompany.Key = "";
    //        $scope.Model.MasterViewModel.VisaCompany.Value = "";
    //    }
    //    else {
    //        $scope.Model.MasterViewModel.VisaCompany.Key = object.Key;
    //        $scope.Model.MasterViewModel.VisaCompany.Value = object.Value;
    //    }
    //}

    //$scope.OnQuotaTypeChange = function (object) {
    //    if (object == null || object == undefined) {
    //        $scope.Model.MasterViewModel.QuotaType.Key = "";
    //        $scope.Model.MasterViewModel.QuotaType.Value = "";
    //    }
    //    else {
    //        $scope.Model.MasterViewModel.QuotaType.Key = object.Key;
    //        $scope.Model.MasterViewModel.QuotaType.Value = object.Value;
    //    }
    //}

    //$scope.OnStatusChange = function (object) {
    //    if (object == null || object == undefined) {
    //        $scope.Model.MasterViewModel.EmpProcessRequestStatus.Key = "";
    //        $scope.Model.MasterViewModel.EmpProcessRequestStatus.Value = "";
    //    }
    //    else {
    //        $scope.Model.MasterViewModel.EmpProcessRequestStatus.Key = object.Key;
    //        $scope.Model.MasterViewModel.EmpProcessRequestStatus.Value = object.Value;
    //    }
    //}

}]);