
app.controller("LiveViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Live list controller");
    var windowContainer = null;
    var ViewName = null;
    var IID = null;
    $scope.ViewModels = [];
    var DefaultDynamicView = null;
    $scope.SelectedIds = [];

    $scope.init = function (window, viewName,iid, defaultDynamicView) {
        windowContainer = '#' + window;
        ViewName = viewName;
    }

    $scope.ShowDetailedView = function (viewName, actionType, ID) {
        var editUrl = viewName + "/" + actionType + "?IIDs=" + $scope.SelectedIds.join();
        $('.pagecontent', windowContainer).addClass('summaryview detail-panel minimize-fields');
        $("#summarypanel", windowContainer).html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        $http({ method: 'Get', url: editUrl })
        .then(function (result) {
            $("#summarypanel", windowContainer).html($compile(result)($scope));
        });
    }
}]);
