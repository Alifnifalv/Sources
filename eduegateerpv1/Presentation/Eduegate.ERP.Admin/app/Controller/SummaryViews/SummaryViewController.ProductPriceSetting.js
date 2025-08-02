app.controller("SummaryViewProductPriceSettingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("SummaryViewProductPriceSettingController");

        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.SummaryViewInitChild = function (window, viewName, iid, defaultDynamicView, modelQuantity, modelCustomerGroup) {
            this.SummaryViewInit(window, viewName, iid, defaultDynamicView, modelQuantity, modelCustomerGroup);

            $scope.LoadProductPriceLists();
            $scope.GetProductPriceLists();
            // To get Branches
            $scope.LoadProductBranchLists();
            $scope.GetProductBranchLists();
            $scope.GetBranches();
        }
       
    }]);