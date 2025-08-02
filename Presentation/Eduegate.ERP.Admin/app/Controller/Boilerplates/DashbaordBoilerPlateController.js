app.controller("DashbaordBoilerPlateController", ["$scope", "$timeout", "$window", "$http", "$compile", "$sce", "$q",
    function ($scope, $timeout, $window, $http, $compile, $sce, $q) {
        console.log('DashbaordBoilerPlateController controller loaded.');
        $scope.runTimeParameter = null;
        $scope.Model = null;
        $scope.window = "";
        $scope.TransactionCount = 0;

        $scope.init = function (model, window) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;
        };

    }]);

