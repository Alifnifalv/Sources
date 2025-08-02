app.controller("SignupController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("SignupController Loaded");

    $scope.ExamQuestionDatas = [];
    $scope.ExamDuration = 0;
    $scope.IsAutoSave = false;

    $scope.Init = function (questionList, examMaximumDuration) {

    };
}]);