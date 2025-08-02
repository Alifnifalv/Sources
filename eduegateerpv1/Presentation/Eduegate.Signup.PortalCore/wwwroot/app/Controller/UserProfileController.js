app.controller("UserProfileController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("User Profile Controller Loaded");

    $scope.Init = function () {

        $scope.ShowPreLoader = true;
    };

}]);