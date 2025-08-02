app.controller('ListLinkBoilerplateController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$rootScope",
    function ($scope, $http, $compile, $window, $location, $timeout, $root) {
        console.log('ListLinkBoilerplateController controller loaded.');
        $scope.ReportList = [];
        /*  $scope.ReportList = [{ ReportID: 1, ReportName: 'Fee' }, { ReportID: 2, ReportName: 'Attendance' }, { ReportID:3, ReportName: 'StudentClassHistory' }];*/
        $scope.BoilerPlateID = 0;
        $scope.PageBoilerPlateMapIID = 0;
        $scope.StudentTransferID = 0;
        $scope.init = function (model, window, viewName) {
            $scope.runTimeParameter = model.RuntimeParameters;
            $scope.Model = model;
            $scope.window = window;
            $scope.BoilerPlateID = model.BoilerPlateID;
            $scope.PageBoilerPlateMapIID = model.BoilerPlateMapIID;
            $scope.StudentID = getParameterValue('REFERENCEID', 'number');
            LoadReportData();
        }
        $scope.ViewReports = function (reportName, reportHeader, parameterName, refernceID) {

            var reportName = reportName;
            var reportHeader = reportHeader;
            if (reportName == "StudentTCDiscReport") {
                $scope.FillStudentTransferData(reportName, reportHeader, $scope.StudentID);              

            }
            else {

                if (refernceID == undefined)
                    refernceID = $scope.StudentID;
                $scope.ViewReport(reportName, reportHeader, parameterName, refernceID);
            }           
          
        };

        $scope.ViewReport = function (reportName, reportHeader, parameterName, refernceID) {

            if (refernceID == undefined || refernceID == 0) {          
              /*  $().showMessage($scope, $timeout, true, 'There are no records available for display!');*/
                alert('There are no records available for display!');
                return;
            }
            var parameter = parameterName + "=" + refernceID;
            var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

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

        }
        function LoadReportData() {

            var url = "/CMS/Boilerplate/";
            $.ajax({
                type: "GET",
                data: { boilerPlateID: $scope.BoilerPlateID, pageBoilerPlateMapIID: $scope.PageBoilerPlateMapIID },
                url: "/CMS/Boilerplate/GetBoilerPlateReports",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.length != 0) {
                            $scope.ReportList = result;
                        }
                    });
                },
                error: function () {

                },
                complete: function (result) {
                 

                }
            });
        }

        function getParameterValue(parameterName, type) {
            if (!$scope.runTimeParameter) {
                return getDefault(type);
            }

            let keyValueViewModel = $scope.runTimeParameter.find(a => a.Key === parameterName);
            if (!keyValueViewModel || !keyValueViewModel.Value) {
                return getDefault(type);
            }

            return convertType(keyValueViewModel.Value, type);
        }

        function getDefault(type) {
            switch (type) {
                case 'string':
                    return '';
                case 'number':
                    return 0;
                case 'boolean':
                    return false;
                case 'object':
                    return null;
                default:
                    return null;
            }
        }

        function convertType(value, type) {
            switch (type) {
                case 'string':
                    return String(value);
                case 'number':
                    return Number(value);
                case 'boolean':
                    return value.toLowerCase() === 'true';
                case 'object':
                    try {
                        return JSON.parse(value);
                    } catch (e) {
                        return null;
                    }
                default:
                    return value;
            }
        }
        $scope.FillStudentTransferData = function (reportName, reportHeader, studentId) {
            $.ajax({
                type: "GET",
                data: { StudentID: studentId },
                url: "Schools/School/FillStudentTransferData",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null && result.StudentTransferRequestIID != 0) {
                        $scope.$apply(function () {
                            $scope.StudentTransferID = result.StudentTransferRequestIID;
                        });
                    }

                },
                error: function () {

                },
                complete: function (result) {

                    var refernceID = $scope.StudentTransferID;
                    var parameterName = "StudentTransferRequestIID";

                    $scope.ViewReport(reportName, reportHeader, parameterName, refernceID);

                }
            });
        }

    }]);