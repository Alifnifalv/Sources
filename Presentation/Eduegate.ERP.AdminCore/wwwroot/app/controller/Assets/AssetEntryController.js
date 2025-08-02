app.controller("AssetEntryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.QuantityChanges = function (viewModel) {
        var gridModel = angular.copy(viewModel.AssetSerialEntry.SerialEntryGrids[0]);
        gridModel.DepreciationRate = viewModel.AssetCategoryDepreciationRate;
        var quantity = viewModel != null && viewModel.AssetSerialEntry != null ? viewModel.AssetSerialEntry.Quantity : 1;
        quantity = quantity <= 0 ? 1 : quantity;

        viewModel.AssetSerialEntry.SerialEntryGrids = [angular.copy(viewModel.AssetSerialEntry.SerialEntryGrids[0])];

        var assetID = viewModel.Asset != null ? viewModel.Asset.Key : null;
        if (!assetID) {
            $().showMessage($scope, $timeout, true, "Please select an asset first!", 1000);
            viewModel.AssetSerialEntry.Quantity = null;

            return false;
        }

        // Check if quantity is a valid positive integer using a regex
        if (!/^\d+$/.test(quantity)) {
            $().showMessage($scope, $timeout, true, "Please enter a valid positive number with no special characters or letters.", 1000);
            viewModel.AssetSerialEntry.Quantity = null;

            return false;
        }
        if (viewModel.AssetSerialEntry.Quantity <= 0) {
            $().showMessage($scope, $timeout, true, "Minimum 1 quantity is required!", 1000);
            viewModel.AssetSerialEntry.Quantity = null;

            return false;
        }      

        for (i = 1; i <= quantity; i++) {
            gridModel.SLNo = i;
            gridModel.IsRowDisabled = false;
            gridModel.AssetSequenceCode = viewModel.AssetPrefix != null ? viewModel.AssetPrefix + "-" +
                ((viewModel.AssetLastSequenceNumber != null ? viewModel.AssetLastSequenceNumber : 0) + i) : "No-" + i;

            viewModel.AssetSerialEntry.SerialEntryGrids.push(JSON.parse(JSON.stringify(gridModel)));
        }

        if (viewModel.AssetEntryHeadIID == 0) {
            $scope.GridCostAmountChanges(viewModel);
        }
    };

    $scope.AssetChanges = function (viewModel) {
        var assetID = viewModel.Asset != null ? viewModel.Asset.Key : null;

        if (assetID) {
            var url = utility.myHost + "Asset/GetAssetDetailsByID?assetID=" + assetID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    var assetDetail = result.data;
                    if (assetDetail != null) {
                        viewModel.IsRequiredSerialNumber = assetDetail.IsRequiredSerialNumber;
                        viewModel.AssetDescription = assetDetail.Description;
                        viewModel.AssetPrefix = assetDetail.AssetPrefix;
                        viewModel.AssetLastSequenceNumber = assetDetail.LastSequenceNumber;
                        viewModel.AssetCategoryID = assetDetail.AssetCategoryID;
                        viewModel.AssetCategoryName = assetDetail.AssetCategoryName;
                        viewModel.AssetCategoryDepreciationRate = assetDetail.AssetCategoryDepreciationRate;

                        viewModel.AssetSerialEntry.SerialEntryGrids.forEach((x, index) => {
                            if (index === 0) return; // Skip the first row
                            x.DepreciationRate = assetDetail.AssetCategoryDepreciationRate;
                        });
                    }
                    else {
                        viewModel.IsRequiredSerialNumber = false;
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                })
        }
    };

    $scope.GridCostAmountChanges = function (masterModel) {

        const total = masterModel.AssetSerialEntry.SerialEntryGrids.reduce((sum, x) => sum + parseFloat(x.CostPrice || 0), 0);
        masterModel.CostAmount = total;
        masterModel.Amount = total;
    }

}]);