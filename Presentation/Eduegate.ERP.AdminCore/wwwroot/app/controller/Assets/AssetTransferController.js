app.controller("AssetTransferController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $rootScope) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.AssetChanges = function (detailModel, masterModel) {

        var branchID = masterModel.Branch != null && masterModel.Branch.Key != null ? masterModel.Branch.Key : 0;

        if (branchID == 0) {
            $().showMessage($scope, $timeout, true, "Please select a 'From Branch' first!", 2000);
            detailModel.Asset = null;
            return false;
        }

        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;

        if (assetID) {
            detailModel.Quantity = null;

            //Bring asset details using asset
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

            //Bring asset inventory details by asset
            $http({
                method: 'Get',
                url: utility.myHost + "Assets/AssetInventory/GetAssetInventoryDetail?assetID=" + assetID + "&branchID=" + branchID
            }).then(function (result) {

                var inventoryDetail = result.data;

                detailModel.AvailableQuantity = inventoryDetail.AvailableQuantity;

                hideOverlay();
            }, function () {
                hideOverlay();
            })
        }
    };

    $scope.BringAssetSerialPopup = function (detail) {

        if (detail.Quantity == 0) {
            return false;
        }

        if (detail.TransactionSerialMaps == null) {
            detail.TransactionSerialMaps = [];
        }

        if (detail.TransactionSerialMaps.length < detail.Quantity) {
            for (i = detail.TransactionSerialMaps.length; i < detail.Quantity; i++) {
                var serialDetail = angular.copy($scope.CRUDModel.DetailViewModel.TransactionSerialMaps)[0];
                serialDetail.AssetID = detail.Assset != null ? detail.Assset.Key : null;
                detail.TransactionSerialMaps.push(serialDetail);
            }
        }
        else if (detail.TransactionSerialMaps.length > detail.Quantity) {
            for (i = detail.TransactionSerialMaps.length; i > detail.Quantity; i--) {
                detail.TransactionSerialMaps.pop();
            }
        }

        $rootScope.AssetDetailViewModel = detail;

        $scope.FillAssetSequences(detail);
    }

    $scope.FillAssetSequences = function (model) {

        var assetID = model.Asset != null ? model.Asset.Key : null;
        $scope.LookUps.AssetSerialMapCodes = [];
        if (assetID) {
            var url = utility.myHost + "Asset/GetAssetSerialMapsByAssetID?assetID=" + assetID;
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
                            model.TransactionSerialMaps.forEach(map => {
                                $timeout(function () {
                                    $scope.$apply(function () {

                                        map.AssetSerialMap = $scope.LookUps.AssetSerialMapCodes[0];

                                        $scope.AssetSerialMapChanges(map);
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

    $scope.QuantityChanges = function (viewModel) {

        var assetID = viewModel.Asset != null ? viewModel.Asset.Key : null;
        if (!assetID) {
            $().showMessage($scope, $timeout, true, "Please select an asset first!", 1500);
            viewModel.Quantity = null;
        }
        if (viewModel.Quantity > viewModel.AvailableQuantity) {
            $().showMessage($scope, $timeout, true, "The quantity has been exceeded!", 1500);
            viewModel.Quantity = null;
        }
        
    };

    $scope.AssetSerialMapChanges = function (model) {

        if (model.AssetTransactionSerialMapIID == 0) {
            var serialMapID = model.AssetSerialMap.Key;

            if (serialMapID) {
                var mapDetail = $scope.AssetSerialMaps.find(x => x.AssetSerialMapIID == serialMapID);

                if (mapDetail) {
                    model.AssetSequenceCode = mapDetail.AssetSequenceCode;
                    model.SerialNumber = mapDetail.SerialNumber;
                    model.AssetTag = mapDetail.AssetTag;
                    model.FirstUseDateString = mapDetail.FirstUseDateString;
                    model.LastDepDateString = mapDetail.LastToDateString;
                    model.AccumulatedDepreciationAmount = mapDetail.AccumulatedDepreciationAmount;
                }
            }
        }
    };

    $scope.CheckLastDayOfMonth = function (model, fieldName) {
        if (!model || !model[fieldName]) return; // Ensure model & field exist

        var stringDate = model[fieldName]; // Get the date value

        if (stringDate) {

            $http.get(utility.myHost + "Home/CheckNotPastDate?stringDate=" + encodeURIComponent(stringDate))
                .then(function (result) {
                    if (result.data.IsError) {
                        $().showMessage($scope, $timeout, true, result.data.Response);

                        // Clear the input field dynamically using model and fieldName
                        model[fieldName] = null;
                    }
                })
                .catch(function (error) {
                    console.error("Error checking past date:", error);
                })
                .finally(function () {
                    hideOverlay();
                });

            $http.get(utility.myHost + "Home/CheckLastDayOfMonth?stringDate=" + encodeURIComponent(stringDate))
                .then(function (result) {
                    if (result.data.IsError) {
                        $().showMessage($scope, $timeout, true, result.data.Response);

                        // Clear the input field dynamically using model and fieldName
                        model[fieldName] = null;
                    }
                })
                .catch(function (error) {
                    console.error("Error checking last day of month:", error);
                })
                .finally(function () {
                    hideOverlay();
                });
        }
    };

}]);