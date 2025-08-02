app.controller("SummaryViewSKUController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("SummaryViewSKUController");

        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.SummaryViewInitChild = function (window, viewName, iid, defaultDynamicView, tagModel) {
            this.SummaryViewInit(window, viewName, iid, defaultDynamicView);

            //this.LoadProductPriceLists();
            //this.GetProductPriceLists();
            //this.GetPriceSettingStatus();
            // To get Branches
            //this.LoadProductBranchLists();
            //this.GetProductBranchLists();
            this.GetBranches();

            if (tagModel != null || tagModel != undefined) {
                $scope.TagModel = angular.copy(tagModel);
                $scope.TagModel.AvailableTags = [];
                $scope.TagModel.SelectedTags = [];
                $scope.TagModel.ProductSKUIDs = [];
                $scope.GetSKUTagLists();
            }
        }       
    }]);