app.controller("AssetController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.AssetCategoryChanges = function (viewModel) {

        var assetCategoryID = viewModel.AssetCategory;
        if (assetCategoryID) {
            showOverlay();
            var url = utility.myHost + "Asset/GetAssetCategoryDetailsByID?assetCategoryID=" + assetCategoryID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    if (result.data) {
                        if (!result.data.CategoryPrefix) {
                            $().showMessage($scope, $timeout, true, "A prefix is required for the selected category. Please add one to proceed.", 3000);
                        }
                        viewModel.AssetCode = result.data.CategoryPrefix + "-" + ((result.data.LastSequenceNumber != null ? result.data.LastSequenceNumber : 0) + 1);
                        viewModel.AssetCategoryPrefix = result.data.CategoryPrefix;
                    }

                    hideOverlay();
                }, function (error) {
                    hideOverlay();
                })
        }
    };

}]);