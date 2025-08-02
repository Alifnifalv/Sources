app.controller("ViewControllerJobProfile", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller)
{
    console.log("ViewControllerJobProfile");
        var IID = null;
        angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window : $window, $location: $location, $route: $route }));

        $scope.init = function (model, ViewIID) {

        }

        $scope.ChangeJobProfileStatusToArchive = function (event) {
            var IDs = new JSLINQ($scope.$$childTail.RowModel.rows)
                            .Where(function (item) {
                                return item.IsRowSelected == true;
                            });

            var IDString = '';
            $(IDs.items).each(function (index) {
                IDString = IDString + ',' + IDs.items[index].ID;
            });

            $http({ method: 'Get', url: 'HR/JobProfile/MoveToArchive?id=' + IDString })
             .then(function (result) {
                 $().showMessage($scope, $timeout, false, "Applied job moved to archived.");
             });
        }

        $scope.ChangeJobProfileStatusToActive = function (event) {
            var IDs = new JSLINQ($scope.$$childTail.RowModel.rows)
                            .Where(function (item) {
                                return item.IsRowSelected == true;
                            });

            var IDString = '';
            $(IDs.items).each(function (index) {
                IDString = IDString + ',' + IDs.items[index].ID;
            });

            $http({ method: 'Get', url: 'HR/JobProfile/MoveToActive?id=' + IDString })
             .then(function (result) {
                 $().showMessage($scope, $timeout, false, "Applied job made active.");
             });
        }

        $scope.ChangeJobProfileStatusToSelected = function (event) {
            var IDs = new JSLINQ($scope.$$childTail.RowModel.rows)
                            .Where(function (item) {
                                return item.IsRowSelected == true;
                            });

            var IDString = '';
            $(IDs.items).each(function (index) {
                IDString = IDString + ',' + IDs.items[index].ID;
            });

            $http({ method: 'Get', url: 'HR/JobProfile/MoveToSelected?id=' + IDString })
             .then(function (result) {
                 $().showMessage($scope, $timeout, false, "Applied job made selected.");
             });
        }
    }]);