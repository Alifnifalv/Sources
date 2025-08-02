app.controller("AssetPurchaseInvoiceController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $rootScope) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.FillAssetButton = function (detailModel, masterModel) {
        detailModel.Asset = {
            "Key": null,
            "Value": null
        };

        var branchID = masterModel.Branch != null && masterModel.Branch.Key != null ? masterModel.Branch.Key : 0;
        if (branchID == 0) {
            $().showMessage($scope, $timeout, true, "Please select a 'Branch' first!", 2000);
            return false;
        }

        var productSKUID = detailModel.SKUID != null && detailModel.SKUID.Key != null ? detailModel.SKUID.Key : 0;
        if (productSKUID > 0) {
            showOverlay();

            $http({
                method: 'Get',
                url: utility.myHost + "Asset/GetAssetsByProductSKUID?productSKUID=" + productSKUID,
            }).then(function (result) {

                $scope.LookUps.Assets = result.data;

                if ($scope.LookUps.Assets.length == 1) {
                    $timeout(function () {
                        $scope.$apply(function () {

                            detailModel.Asset = $scope.LookUps.Assets[0];

                            $scope.AssetChanges(detailModel, masterModel);
                        });
                    });
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            })
        }
        else {
            $().showMessage($scope, $timeout, true, "Please select a 'Product' first!", 2000);
            return false;
        }
    };

    $scope.AssetChanges = function (detailModel, masterModel) {

        var branchID = masterModel.Branch != null && masterModel.Branch.Key != null ? masterModel.Branch.Key : 0;
        if (branchID == 0) {
            $().showMessage($scope, $timeout, true, "Please select a 'Branch' first!", 2000);

            detailModel.Asset = {
                "Key": null,
                "Value": null
            };
            return false;
        }

        var productSKUID = detailModel.SKUID != null && detailModel.SKUID.Key != null ? detailModel.SKUID.Key : null;
        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;
        if (assetID) {
            showOverlay();

            $http({
                method: 'Get',
                url: utility.myHost + "Asset/GetAssetDetailsByID?assetID=" + assetID
            }).then(function (result) {

                var assetDetail = result.data;

                detailModel.IsRequiredSerialNumber = false;
                if (assetDetail != null) {

                    if (assetDetail.AssetProductMapDTOs.length == 0) {
                        $().showMessage($scope, $timeout, true, "The selected asset is not mapped to the selected product. Click the 'Fill Asset' button to load assets mapped to the selected product.", 2000);

                        detailModel.Asset = {
                            "Key": null,
                            "Value": null
                        };
                        hideOverlay();
                        return false;
                    }
                    else {
                        var mappedProductData = assetDetail.AssetProductMapDTOs.find(x => x.ProductSKUMapID == productSKUID);
                        if (mappedProductData == null) {
                            $().showMessage($scope, $timeout, true, "The selected asset is not mapped to the selected product. Click the 'Fill Asset' button to load assets mapped to the selected product.", 2000);

                            detailModel.Asset = {
                                "Key": null,
                                "Value": null
                            };
                            hideOverlay();
                            return false;
                        }
                    }

                    detailModel.IsRequiredSerialNumber = assetDetail.IsRequiredSerialNumber;
                    detailModel.AssetDescription = assetDetail.Description;
                    detailModel.AssetPrefix = assetDetail.AssetPrefix;
                    detailModel.AssetLastSequenceNumber = assetDetail.LastSequenceNumber;
                    detailModel.AssetCategoryID = assetDetail.AssetCategoryID;
                    detailModel.AssetCategoryName = assetDetail.AssetCategoryName;
                    detailModel.AssetCategoryDepreciationRate = assetDetail.AssetCategoryDepreciationRate;

                    if (detailModel.Quantity > 0) {
                        $scope.QuantityChanges($scope.ModelStructure.DetailViewModel, detailModel);
                    }
                }                

                hideOverlay();
            }, function () {
                hideOverlay();
            })
        }
    };

    $scope.BringAssetSerialPopup = function (detailModel, masterModel) {

        if (detailModel.Quantity == 0) {
            return false;
        }

        if (detailModel.TransactionSerialMaps == null) {
            detailModel.TransactionSerialMaps = [];
        }

        if (detailModel.TransactionSerialMaps.length < detailModel.Quantity) {
            for (i = detailModel.TransactionSerialMaps.length; i < detailModel.Quantity; i++) {
                var serialDetail = angular.copy($scope.CRUDModel.DetailViewModel.TransactionSerialMaps)[0];
                serialDetail.AssetID = detail.Assset != null ? detail.Assset.Key : null;
                detailModel.TransactionSerialMaps.push(serialDetail);
            }
        }
        //else if (detailModel.TransactionSerialMaps.length > detailModel.Quantity) {
        //    for (i = detailModel.TransactionSerialMaps.length; i > detailModel.Quantity; i--) {
        //        //detailModel.TransactionSerialMaps.pop();
        //        detailModel.TransactionSerialMaps.splice(0, 1);
        //    }
        //}
        else if (detailModel.TransactionSerialMaps.length > detailModel.Quantity) {
            for (let i = detailModel.TransactionSerialMaps.length - 1;
                i >= 0 && detailModel.TransactionSerialMaps.length > detailModel.Quantity;
                i--) {
                if (!detailModel.TransactionSerialMaps[i].AssetSequenceCode) {
                    detailModel.TransactionSerialMaps.splice(i, 1);
                }
            }
        }

        $rootScope.AssetDetailViewModel = detailModel;
    }

    $scope.QuantityChanges = function (modelStructure, detailModel) {

        var gridModel = angular.copy(modelStructure[0].TransactionSerialMaps[0]);
        gridModel.DepreciationRate = detailModel.AssetCategoryDepreciationRate;

        var quantity = detailModel.Quantity != null ? detailModel.Quantity : 1;
        quantity = quantity <= 0 ? 1 : quantity;

        //viewModel.AssetSerialEntry.SerialEntryGrids = [angular.copy(viewModel.AssetSerialEntry.SerialEntryGrids[0])];

        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;
        if (!assetID) {
            $().showMessage($scope, $timeout, true, "Please select an asset first!", 1000);
            detailModel.Quantity = null;

            return false;
        }

        // Check if quantity is a valid positive integer using a regex
        if (!/^\d+$/.test(quantity)) {
            $().showMessage($scope, $timeout, true, "Please enter a valid positive number with no special characters or letters.", 1000);
            detailModel.Quantity = null;

            return false;
        }
        if (detailModel.Quantity <= 0) {
            $().showMessage($scope, $timeout, true, "Minimum 1 quantity is required!", 1000);
            detailModel.Quantity = null;

            return false;
        }

        detailModel.TransactionSerialMaps = [];
        for (i = 1; i <= quantity; i++) {
            index = i;
            gridModel.SLNo = i;
            gridModel.IsRowDisabled = false;
            gridModel.AssetSequenceCode = detailModel.AssetPrefix != null ? detailModel.AssetPrefix + "-" +
                ((detailModel.AssetLastSequenceNumber != null ? detailModel.AssetLastSequenceNumber : 0) + i) : "No-" + i;
            gridModel.AssetID = assetID;

            detailModel.TransactionSerialMaps.push(JSON.parse(JSON.stringify(gridModel)));

            //detailModel.TransactionSerialMaps.splice(index + 1, 0, angular.copy(gridModel));
        }
    };

}]);