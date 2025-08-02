app.controller('ListRowExpandBoilerPlateController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$route", "$controller",
    function ($scope, $http, $compile, $window, $location, $timeout, $route, $controller) {
        console.log('ListRowExpandBoilerPlateController controller loaded.');
        $scope.CircularList = [];
        $scope.init = function (model, window, viewName) {
            $scope.runTimeParameter = model.RuntimeParameters;
            $scope.Model = model;
            $scope.window = window;
            $scope.StudentID = getParameterValue('REFERENCEID', 'number');
            $scope.activeStudentID = $scope.StudentID;
            var pagNo = $scope.gridPageNo;
            FillCircularList($scope.StudentID);
        }
        FillCircularList = function (studentId) {
           
            $.ajax({
                type: "GET",
                data: { StudentID: studentId },
                url: "Schools/School/GetCircularListByStudentID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (!result.IsError && result !== null) {

                            $scope.CircularList = result.Response;
                        }
                    });
                },
                error: function () {

                },
                complete: function (result) {
                    
                }
            });
        }
        $scope.onTabChange = function (tabId) {
            if (tabId == "Tab_01") {
                $scope.TabName = "Tab_01";

                $("#Tab_01").show();

                $("#Tab_02").hide();  
                $("#Tab_03").hide();
           
            }
            else if (tabId == "Tab_02") {
                $scope.TabName = "Tab_02";
                $("#Tab_02").show();
                $("#Tab_01").hide();
                $("#Tab_03").hide();
               
            }
            else if (tabId == "Tab_03") {
                $scope.TabName = "Tab_03";
                $("#Tab_03").show();
                $("#Tab_01").hide();
                $("#Tab_02").hide();
              
            }
           
        };
        $scope.DownloadURL = function (url) {
            var link = document.createElement("a");
            link.href = url;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            delete link;
        };
        $scope.GetExpiryDateDifference = function (circularData) {

            var currentDate = new Date();
            var circularExpiryDate = new Date(moment(circularData.ExpiryDate, 'DD/MM/YYYY'));

            const diffTime = Math.abs(circularExpiryDate - currentDate);
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

            return diffDays;
        };
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
    }]);