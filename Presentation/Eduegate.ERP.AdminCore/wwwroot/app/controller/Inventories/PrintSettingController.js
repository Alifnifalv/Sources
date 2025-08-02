app.controller("PrintSettingController", ["$scope", "$timeout", "$window", "$http", "$compile", "$sce", "$q",
    function ($scope, $timeout, $window, $http, $compile, $sce, $q) {
        console.log('PrintSettingController controller loaded.');

        $scope.init = function (model, window) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;
            $scope.LoadBoilerPlates();
        };

    }
    ]);

