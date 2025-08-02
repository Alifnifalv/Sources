app.controller("BoilerPlateQuickLanuchController", ["$scope", "$compile", "$http", "$timeout", "$controller", '$rootScope', function ($scope, $compile, $http, $timeout, $controller, $root) {
    console.log("BoilerPlateQuickLanuchController");

    angular.extend(this, $controller('BoilerPlateController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout }));

    $scope.Create = function (view, event, title, windowName, menuParameters) {
        $root.Create(view, event, title, windowName, menuParameters);
    };

    $scope.List = function (parameters, event, title, menuParameters, isSortable = false) {
        if (parameters === null) return;

        var params = parameters.split(',');

        var runtimeParam2;

        if (params[5]) {
            runtimeParam2 = params[5];
        }

        $root.List(params[1].trim(), event, params[3].trim(), runtimeParam2, isSortable);
    };

}]);