app.controller("ViewCustomizeController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Customize View");
    $scope.SelectedColumn = null;
    $scope.AvailableColumns = [];

    $scope.Init = function (view) {
        $http({ method: 'Get', url: "Mutual/AvailableViewColumns?view=" + view })
      .then(function (result) {
          $scope.AvailableColumns = result;
      });
    }

    $scope.Close = function () {
        $("#customizeView").fadeOut(500);
        $("#customizeView").remove();
        $(".overlaydiv").fadeOut(500);
    }
}]);