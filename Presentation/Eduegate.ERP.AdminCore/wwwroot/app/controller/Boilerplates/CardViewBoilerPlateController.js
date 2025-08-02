app.controller('CardViewBoilerPlateController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$route", "$controller",
    function ($scope, $http, $compile, $window, $location, $timeout, $route,$controller) {
        console.log('CardViewBoilerPlateController controller loaded.');
       
        $scope.init = function (model, window, viewName) {
            $scope.runTimeParameter = model.RuntimeParameters;
            $scope.Model = model;
            $scope.window = window;
            $scope.StudentID = getParameterValue('REFERENCEID', 'number');
            $scope.activeStudentID = $scope.StudentID;
            var pagNo = $scope.gridPageNo;
            LoadData(pagNo, viewName);
        }
        function LoadData(toPage, viewName) {

            if (viewName == null || viewName == "" || viewName == undefined) {
                return null;
            }
            var runtimeFilter = "";

            if ($scope.StudentID != null)
                runtimeFilter = "" + "StudentID=" + $scope.StudentID + "";


            var url = 'Frameworks/Search' + '/SearchData?view=' + viewName + '&currentPage=1&runtimeFilter=' + runtimeFilter + ' &pageSize=' + toPage;
            contentType: "application/json;charset=utf-8",
                $http({ method: 'Get', url: url })
                    .then(function (result) {
                        if (result == null || result.data.length == 0) {
                            return false;
                        }

                        $scope.DataResults = JSON.parse(result.data).Datas;                        
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
    }]);