app.controller("SummaryViewControllerJobProfile", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("SummaryViewControllerJobProfile");
        var IID = null;
        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.SummaryViewInitChild = function (window, viewName, iid, defaultDynamicView) {
            IID = iid;
            this.SummaryViewInit(window, viewName, iid, defaultDynamicView);
        }

        $scope.ChangeJobProfileStatusToArchive = function (event) {
            $http({ method: 'Get', url: 'HR/JobProfile/MoveToArchive?id=' + IID })
             .then(function (result) {
                 $().showMessage($scope, $timeout, false, "Applied job moved to archived."); 
             });
        }

        $scope.ChangeJobProfileStatusToSelected = function (event) {
            $http({ method: 'Get', url: 'HR/JobProfile/MoveToSelected?id=' + IID })
             .then(function (result) {
                 $().showMessage($scope, $timeout, false, "Applied job moved to selected.");
             });
        }
    }]);