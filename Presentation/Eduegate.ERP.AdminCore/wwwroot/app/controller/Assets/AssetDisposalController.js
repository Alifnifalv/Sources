app.controller("AssetDisposalController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $rootScope) {

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
            $().showMessage($scope, $timeout, true, "Please select a 'Branch' first!", 2000);
            detailModel.Asset = null;
            return false;
        }

        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;

        if (assetID) {
            showOverlay();
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
            //$http({
            //    method: 'Get',
            //    url: utility.myHost + "Assets/AssetInventory/GetAssetInventoryDetail?assetID=" + assetID + "&branchID=" + branchID
            //}).then(function (result) {

            //    var inventoryDetail = result.data;

            //    detailModel.AvailableQuantity = inventoryDetail.AvailableQuantity;

            //    hideOverlay();
            //}, function () {
            //    hideOverlay();
            //})

            detailModel.AssetSerialMap = {
                "Key": null,
                "Value": null
            }
            detailModel.AssetSequenceCode = null;
            detailModel.LastDepDateString = null;
            detailModel.NetAmount = null;
            detailModel.AccumulatedDepreciationAmount = null;
            detailModel.LastNetValue = null;
            detailModel.DisposibleAmount = null;
            detailModel.DifferenceAmount = null;

            $scope.FillAssetSequences(detailModel, masterModel);
        }
    };

    $scope.FillAssetSequences = function (detailModel, masterModel) {

        var branchID = masterModel.Branch != null && masterModel.Branch.Key != null ? masterModel.Branch.Key : 0;
        var assetID = detailModel.Asset != null ? detailModel.Asset.Key : null;
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
                            $timeout(function () {
                                $scope.$apply(function () {

                                    detailModel.AssetSerialMap = $scope.LookUps.AssetSerialMapCodes[0];

                                    $scope.AssetSerialMapChanges(detailModel, masterModel);
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
                detailModel.LastDepDateString = mapDetail.LastToDateString;
                detailModel.CostAmount = mapDetail.CostPrice;
                detailModel.AccumulatedDepreciationAmount = mapDetail.AccumulatedDepreciationAmount;
                detailModel.LastNetValue = mapDetail.LastNetValue != null ? mapDetail.LastNetValue : (mapDetail.NetAmount - mapDetail.AccumulatedDepreciationAmount);
            }
        }
    };

    $scope.DisposedAmountChanges = function (detailModel) {
        let inputAmount = detailModel.DisposibleAmount;

        // Allow only if input is not null or undefined
        if (inputAmount !== undefined && inputAmount !== null && inputAmount !== '') {
            // Check for valid number using regex (matches integers and decimals)
            const numberRegex = /^(?:\d+|\d*\.\d+|\d+\.)$/;

            if (!numberRegex.test(inputAmount)) {
                $().showMessage($scope, $timeout, true, "Please enter a valid number greater than or equal to zero!");
                detailModel.DisposibleAmount = null;
                detailModel.DifferenceAmount = 0;
                return false;
            }

            //let numericValue = parseFloat(input);

            // Final check for negativity
            if (inputAmount < 0) {
                $().showMessage($scope, $timeout, true, "Enter a value greater than or equal to zero!");
                detailModel.DisposibleAmount = null;
                detailModel.DifferenceAmount = 0;
                return false;
            }

            detailModel.DisposibleAmount = inputAmount;
            detailModel.DifferenceAmount = parseFloat(detailModel.LastNetValue) - parseFloat(inputAmount);
        } else {
            detailModel.DifferenceAmount = 0;
        }
    };

    $scope.BringAssetSerialPopup = function (detail) {

        if (detail.Quantity == 0 || (detail.hasOwnProperty("IsSerailNumberAutoGenerated") == true && detail.IsSerailNumberAutoGenerated == true)) {
            return;
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
    };

}]);