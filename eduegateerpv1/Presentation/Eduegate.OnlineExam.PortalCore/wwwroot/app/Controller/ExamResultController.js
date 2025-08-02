app.controller("ExamResultController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("ExamResultController Loaded");

    $scope.ExamResultDatas = [];

    $scope.Init = function (examsResultLists) {

        $scope.ExamResultDatas = examsResultLists;
    };

    $scope.ViewResult = function () {
        debugger;
    };

}]);