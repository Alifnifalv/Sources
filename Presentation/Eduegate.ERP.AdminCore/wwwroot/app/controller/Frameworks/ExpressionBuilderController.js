app.controller("ExpressionBuilderController", ["$scope", "$compile", "$http", "$timeout", function ($scope, $compile, $http, $timeout) {
    $scope.Operators = [];
    $scope.Variables = [];

    $scope.Init = function (model) {
        $scope.Model = model;

        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Variables'
        })
        .then(function (result) {
            $scope.Variables = result.data;
        });

        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=Operators'
        })
        .then(function (result) {
            $scope.Operators = result.data;
        });
    }
}]);