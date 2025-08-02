app.controller("MissionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$interval", function ($scope, $http, $compile, $window, $timeout, $location, $route, $interval) {
    console.log("Processing Mission controller");

    var windowContainer = null;
    var ViewName = null;
    var IIDs = null;
    $scope.ViewModels = [];
    var DefaultDynamicView = null;
    $scope.ShowTime = null;
    $scope.Model = null;

    $interval(function () {
        $scope.ShowTime = Date.now();
    }, 1000);

    $scope.ShowRemainingHours = function (date) {
        return utility.getRemainingHoursText(date, $scope.ShowTime);
    }

    $scope.Init = function (window, viewName, iids) {
        IIDs = iids;
        windowContainer = '#' + window;
        ViewName = viewName;
    }
    
    $scope.CloseSummaryPanel = function (event) {
        $(event.currentTarget, $(windowContainer)).closest('.pagecontent').removeClass('summaryview detail-panel minimize-fields');
        $(".preload-overlay", $(windowContainer)).css("display", "none");
        $(windowContainer).find("#summarypanel").html('');
    }

}]);
