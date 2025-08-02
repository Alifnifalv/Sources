app.controller("SalarySlipPublishController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("Salary slip publish controller Loaded");

    $scope.VerifiedStatuses = [];
    $scope.SalarySlips = [];
    $scope.SelectedMonth = {};
    $scope.SelectedYear = {};
    $scope.SelectedDepartment = {};

    $scope.Init = function (model, windowname, type) {
        $scope.type = type;

        $scope.GetLookUpDatas();
    };

    $scope.GetLookUpDatas = function () {

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Months&defaultBlank=false",
        }).then(function (result) {
            $scope.Months = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Years&defaultBlank=false",
        }).then(function (result) {
            $scope.Years = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Department&defaultBlank=false",
        }).then(function (result) {
            $scope.Departments = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=SalaryStatus&defaultBlank=false",
        }).then(function (result) {
            $scope.SalarySlipStatuses = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "SALARYSLIPAPPROVED_ID",
        }).then(function (result) {
            $scope.ApprovedSlipStatusID = result.data;
        });

        $http({
            method: 'Get', url: "Settings/Setting/GetSettingValueByKey?settingKey=" + "DEFAULT_DECIMALPOINTS",
        }).then(function (result) {
            $scope.DefaultDecimalPoint = result.data;
        });

        $scope.VerifiedStatuses.push(
            { "Key": 1, "Value": "ALL" },
            { "Key": 2, "Value": "Verified" },
            { "Key": 3, "Value": "Not verified" }
        );

        $scope.VerifiedStatus = $scope.VerifiedStatuses[0];
    };

    $scope.GetEmployeesSalarySlip = function () {

        var checkBox = document.getElementById("IsSelectedToPublish");
        checkBox.checked = false;

        var departmentID = $scope.SelectedDepartment?.Key;
        var Month = $scope.SelectedMonth?.Key;
        var Year = $scope.SelectedYear?.Key;

        if (!Month) {
            $().showGlobalMessage($root, $timeout, true, "Please select Month!");
            return false;
        }
        if (!Year) {
            $().showGlobalMessage($root, $timeout, true, "Please select Year!");
            return false;
        }
        if (!departmentID) {
            departmentID = "0";
        }

        showOverlay();
        var url = "Payroll/Employee/GetSalarySlipEmployeeData?departmentID=" + departmentID + "&Month=" + Month + "&Year=" + Year;
        $http({
            method: 'Get',
            url: url
        }).then(function (result) {
            $scope.SalarySlips = result.data;
            $scope.FullSalarySlipDatas = result.data;
            hideOverlay();
        }, function () {
            hideOverlay();
        });
    };

    $scope.ChangeVerifiedDropdownData = function () {

        if ($scope.FullSalarySlipDatas.length > 0) {
            showOverlay();
            var filteredSalarySlipData = [];

            $scope.FullSalarySlipDatas.forEach(x => {

                if ($scope.VerifiedStatus.Key == 2) {
                    if (x.IsVerified) {
                        filteredSalarySlipData.push(x);
                    }
                }
                else if ($scope.VerifiedStatus.Key == 3) {
                    if (!x.IsVerified) {
                        filteredSalarySlipData.push(x);
                    }
                }
                else {
                    if ($scope.VerifiedStatus.Value == "ALL") {
                        filteredSalarySlipData.push(x);
                    }
                }
            });

            $timeout(function () {
                $scope.$apply(function () {
                    $scope.SalarySlips = filteredSalarySlipData;
                    hideOverlay();
                });
            }, 1000);
        }
    };

    $scope.IsVerifiedStatusChanges = function (detail) {
        showOverlay();
        var url = "Payroll/Employee/UpdateSalarySlipIsVerifiedData?salarySlipID=" + detail.SalarySlipIID + "&isVerifiedStatus=" + detail.IsVerified;
        $http({
            method: 'Post',
            url: url
        }).then(function (result) {
            if (result.data.IsError) {
                $().showGlobalMessage($root, $timeout, true, result.data.Response);
            }
            else {
                $().showGlobalMessage($root, $timeout, false, result.data.Response);

                $timeout(function () {
                    $scope.$apply(function () {
                        $scope.GetEmployeesSalarySlip();
                    });
                }, 1000);
            }
            hideOverlay();
            return false;
        }, function () {
            hideOverlay();
        });
    };

    $scope.PublishSalarySlips = function () {
        if ($scope.SalarySlips.length > 0) {

            var SalarySlipsForPublishing = [];

            $scope.SalarySlips.forEach(s => {
                if (s.IsSelected == true) {
                    SalarySlipsForPublishing.push(s);
                }
            });

            if (SalarySlipsForPublishing.length > 0) {
                showOverlay();

                var url = "Payroll/Employee/PublishSalarySlips";
                $http({
                    method: 'Post',
                    url: url,
                    data: SalarySlipsForPublishing
                }).then(function (result) {
                    if (result.data.IsError) {
                        $().showGlobalMessage($root, $timeout, true, result.data.Response);
                    }
                    else {
                        $().showGlobalMessage($root, $timeout, false, result.data.Response);

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.GetEmployeesSalarySlip();
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
                $().showGlobalMessage($root, $timeout, true, "Select atleast one salary slip for publish!");
            }

        }
        else {
            $().showGlobalMessage($root, $timeout, true, "Need atleast one salary slip for publish!");
        }
    };

    $scope.SendSalarySlipMail = function () {

        var SalarySlipsForPublishing = [];

        $scope.SalarySlips.forEach(s => {
            if (s.IsSelected == true) {
                var slipData = $scope.FullSalarySlipDatas.find(f => f.SalarySlipIID == s.SalarySlipIID);
                if (slipData.PublishStatusID == $scope.ApprovedSlipStatusID) {
                    SalarySlipsForPublishing.push(s);
                }
            }
        });
        if (SalarySlipsForPublishing.length > 0) {
            showOverlay();

            var url = "Payroll/Employee/EmailSalarySlips";
            $http({
                method: 'Post',
                url: url,
                data: SalarySlipsForPublishing
            }).then(function (result) {
                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, "Mail sent failed!");
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, result.data.Response);
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.GetEmployeesSalarySlip();
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
            $().showGlobalMessage($root, $timeout, true, "Select atleast one issued salary slip for mailing!");
        }
    };

    $scope.ApplyAndUpdateSlipData = function (salarySlip) {
        if (salarySlip.SalarySlipIID > 0) {
            showOverlay();

            var url = "Payroll/Employee/ApplyAndUpdateSlipData";
            $http({
                method: 'Post',
                url: url,
                data: salarySlip
            }).then(function (result) {
                if (result.data.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.data.Response);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, result.data.Response);

                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.GetEmployeesSalarySlip();
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
            $().showGlobalMessage($root, $timeout, true, "Something went wrong, try again later!");
        }
    };

    $scope.SelectedHeaderCheckBox = function () {
        var checkBox = document.getElementById("IsSelectedToPublish");
        if (checkBox.checked) {
            $scope.SalarySlips.forEach(s => s.IsSelected = true);
        }
        else {
            $scope.SalarySlips.forEach(s => s.IsSelected = false);
        }
    };

    $scope.EditSalarySlip = function (salarySlipIID) {
        var windowName = 'EditSalarySlip';
        var viewName = 'EditSalarySlip';

        if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + viewName + "&ID=" + salarySlipIID;

        $http({
            method: 'Get',
            url: editUrl
        }).then(function (result) {
            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
            $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        });

    };

    $scope.ViewSalarySlip = function (slipData) {

        if (!slipData.ReportContentID) {
            $().showGlobalMessage($root, $timeout, true, "No data available!");
            return false;
        }

        $('#globalRightDrawer').modal('show');
        $('#globalRightDrawer').find('.modal-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
        $('#globalRightDrawer').find('.modal-title').html("Salary Slip");
        $('#globalRightDrawer').find('.modal-body').html('<iframe id="myFrame" src="Content/ReadContentsByIDWithoutAttachment?contentID=' + slipData.ReportContentID + '" style="height: 638px; width: 100%; frameborder="0"></iframe>');
    };

    $scope.ShowSalarySlipReport = function (slipData) {
        var reportName = "SalarySlipReport";
        var reportHeader = "Salary Slip";

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var iidColumn = GetColumnName(slipData, 'IID');
        var parameter = "";

        if (iidColumn) {
            parameter = iidColumn + "=" + slipData[iidColumn];
        }

        var reportUrl = "Home/ViewReports?reportName=" + reportName + "&" + parameter;
        $('#' + windowName).append('<script>function onLoadComplete() { }</script><center></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
        var $iFrame = $('iframe[reportname=' + reportName + ']');
        $iFrame.on('load', function () {
            $("#Load", $('#' + windowName)).hide();
        });
    };

    function GetColumnName(object, column) {
        for (var key in object) {
            if (key.indexOf(column) != -1) {
                return key;
            }
        }
    };

    function showOverlay() {
        $('.preload-overlay', $('#SalarySlipPublish')).attr('style', 'display:block');
    };

    function hideOverlay() {
        $('.preload-overlay', $('#SalarySlipPublish')).hide();
    };

}]);