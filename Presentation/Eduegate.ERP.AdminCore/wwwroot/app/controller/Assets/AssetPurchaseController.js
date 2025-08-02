app.controller("AssetPurchaseController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $rootScope) {

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

        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;
        if (assetID) {
            showOverlay();

            $http({
                method: 'Get',
                url: utility.myHost + "Asset/GetAssetDetailsByID?assetID=" + assetID
            }).then(function (result) {

                var assetDetails = result.data;
                detailModel.IsRequiredSerialNumber = assetDetails != null ? assetDetails.IsRequiredSerialNumber : false;
                detailModel.AssetDescription = assetDetails != null ? assetDetails.Description : null;

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
        else if (detailModel.TransactionSerialMaps.length > detailModel.Quantity) {
            for (i = detailModel.TransactionSerialMaps.length; i > detailModel.Quantity; i--) {
                detailModel.TransactionSerialMaps.pop();
            }
        }

        $rootScope.AssetDetailViewModel = detailModel;

        $scope.FillAssetSequences(detailModel, masterModel);
    }

    $scope.FillAssetSequences = function (detailModel, masterModel) {

        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;
        var branchID = masterModel.Branch != null && masterModel.Branch.Key != null ? masterModel.Branch.Key : 0;

        $scope.LookUps.AssetSerialMapCodes = [];
        if (assetID) {
            var url = utility.myHost + "Asset/GetAssetSerialMapsByAssetAndBranchID?assetID=" + assetID + "&branchID=" + branchID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.AssetSerialMaps = result.data;

                    if ($scope.AssetSerialMaps.length > 0) {
                        $scope.AssetSerialMaps.forEach(x => {
                            var sequenceMap = {
                                "Key": x.AssetSerialMapIID,
                                "Value": x.AssetSequenceCode
                            }
                            $scope.LookUps.AssetSerialMapCodes.push(sequenceMap);
                        });

                        if ($scope.LookUps.AssetSerialMapCodes.length == 1) {
                            detailModel.TransactionSerialMaps.forEach(map => {
                                $timeout(function () {
                                    $scope.$apply(function () {

                                        map.AssetSerialMap = $scope.LookUps.AssetSerialMapCodes[0];

                                        $scope.AssetSerialMapChanges(map, masterModel);
                                    });
                                });
                            });
                        }
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                })
        }
    };

    $scope.AssetSerialMapChanges = function (detailModel, masterModel) {
        if (gridModel.AssetTransactionSerialMapIID == 0) {
            var serialMapID = detailModel.AssetSerialMap.Key;
            var selectedBranchID = masterModel.Branch.Key;

            if (serialMapID) {
                var mapDetail = $scope.AssetSerialMaps.find(x => x.AssetSerialMapIID == serialMapID);

                if (mapDetail) {
                    if (mapDetail.InventoryBranchID != selectedBranchID) {
                        $().showMessage($scope, $timeout, true, "The selected item is not available in the selected branch!", 2000);
                        detailModel.AssetSerialMap = {
                            "Key": null,
                            "Value": null,
                        };

                        return false;
                    }

                    detailModel.AssetSequenceCode = mapDetail.AssetSequenceCode;
                    detailModel.SerialNumber = mapDetail.SerialNumber;
                    detailModel.AssetTag = mapDetail.AssetTag;
                    detailModel.FirstUseDateString = mapDetail.FirstUseDateString;
                    detailModel.LastDepDateString = mapDetail.LastToDateString;
                    detailModel.NetAmount = mapDetail.NetAmount;
                    detailModel.LastNetValue = mapDetail.LastNetValue != null ? mapDetail.LastNetValue : (mapDetail.NetAmount - mapDetail.AccumulatedDepreciationAmount);
                    gridModel.AccumulatedDepreciationAmount = mapDetail.AccumulatedDepreciationAmount;
                }
            }
        }
    };

}]);