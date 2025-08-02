app.controller("AssetDepreciationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.AssetCategoryChanges = function (model) {        

        if (model.AssetCategories.length > 1) {

            // Find the index of the element with Value === 'ALL'
            const allIndex = model.AssetCategories.findIndex(category => category.Value === 'ALL');

            if (allIndex !== -1) {
                if (allIndex === 0) {
                    // If 'ALL' is the first element, keep only 'ALL'
                    model.AssetCategories = model.AssetCategories.filter(category => category.Value === 'ALL');

                    $().showMessage($scope, $timeout, true, "If you select the 'ALL' category, then you will not be able to select any other categories.", 3000);
                } else {
                    // If 'ALL' is not the first element, remove 'ALL'
                    model.AssetCategories = model.AssetCategories.filter(category => category.Value !== 'ALL');

                    $().showMessage($scope, $timeout, true, "You cannot select 'ALL' if other categories are already selected.", 3000);
                }

                return; // Return after handling the condition
            }
        }

        model.AssetCategories.forEach(category => {
            var assetCategoryID = category != null ? category.Key != null ? category.Key : 0 : 0;
            if (assetCategoryID) {
                var url = utility.myHost + "Asset/GetAssetsByCategoryID?categoryID=" + assetCategoryID;

                showOverlay();

                $http({
                    method: 'GET',
                    url: url,
                }).then(function (result) {

                    if (result.data) {

                        if (model.AssetCategories.length == 1) {
                            $scope.LookUps.AllAssets = result.data;
                        }
                        else {
                            // Create a Set of existing keys in LookUps.AllAssets to check for duplicates
                            const existingKeys = new Set($scope.LookUps.AllAssets.map(asset => asset.Key));

                            // Filter out objects where Value is "ALL" or Key is 0, and avoid duplicates
                            let filteredData = result.data.filter(item => !existingKeys.has(item.Key)); // Ensure the key is not already in LookUps.AllAssets

                            $scope.LookUps.AllAssets.push(...filteredData);
                        }

                        if (model.Assets.length > 0 && $scope.LookUps.AllAssets.length > 0) {
                            // Get the set of valid keys from $scope.LookUps.AllAssets
                            const validKeys = new Set($scope.LookUps.AllAssets.map(asset => asset.Key));

                            // Filter model.Assets to keep only those with a Key present in validKeys
                            model.Assets = model.Assets.filter(asset => validKeys.has(asset.Key));
                        }
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
            }
        });
        
    };

    $scope.AssetChanges = function (model) {

        if (model.Assets.length > 1) {

            // Find the index of the element with Value === 'ALL'
            const allIndex = model.Assets.findIndex(asset => asset.Value === 'ALL');

            if (allIndex !== -1) {
                if (allIndex === 0) {
                    // If 'ALL' is the first element, keep only 'ALL'
                    model.Assets = model.Assets.filter(asset => asset.Value === 'ALL');

                    $().showMessage($scope, $timeout, true, "If you select the 'ALL' asset, then you will not be able to select any other Assets.", 3000);
                } else {
                    // If 'ALL' is not the first element, remove 'ALL'
                    model.Assets = model.Assets.filter(asset => asset.Value !== 'ALL');

                    $().showMessage($scope, $timeout, true, "You cannot select 'ALL' if other assets are already selected.", 3000);
                }

                return; // Return after handling the condition
            }
        }
    };

    $scope.DepreciationDetailButtonClick = function (model) {

        if (model.IsTransactionCompleted) {
            $().showMessage($scope, $timeout, true, "Depreciation cannot be recalculated if the transaction is already processed!", 2000);
            return false;
        }

        if (model.AssetCategories.length == 0) {
            $().showMessage($scope, $timeout, true, "Select at least one category to fill the depreciation grid, or select the 'ALL' option!", 2000);
            return false;
        }

        if (model.Assets.length > 0) {
            var month = model.DepreciationMonth;
            var year = model.DepreciationYear;
            var branchID = model.Branch != null ? model.Branch.Key : null;

            if (!branchID) {
                $().showMessage($scope, $timeout, true, "Select an branch!", 1000);
                return false;
            }
            if (!month) {
                $().showMessage($scope, $timeout, true, "Select an month!", 1000);
                return false;
            }
            if (!year) {
                $().showMessage($scope, $timeout, true, "Select an year!", 1000);
                return false;
            }

            showOverlay();

            $http({
                method: 'GET',
                url: utility.myHost + "Asset/CalculateAndFillDepreciationDetail",
                params: {
                    assetListString: JSON.stringify(model.Assets), // Send the list as a JSON string
                    categoryListString: JSON.stringify(model.AssetCategories),
                    month: month,
                    year: year,
                    branchID: branchID
                }
            }).then(function (result) {
                if (!result.data.IsError) {
                    $scope.CRUDModel.Model.DetailViewModel = result.data.Response;
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
        }
        else {
            $().showMessage($scope, $timeout, true, "Select at least one asset to fill the depreciation grid!", 2000);
            return false;
        }
    };

    $scope.AccountPostingButtonClick = function (model) {

        if (!model.IsTransactionCompleted) {
            $().showMessage($scope, $timeout, true, "You can post to the account only after the transaction entry is completed!", 2000);
            return false;
        }

        if (model.TransactionHeadIID > 0) {

            showOverlay();

            $http({
                method: 'GET',
                url: utility.myHost + "Asset/DepreciationAccountPosting",
                params: {
                    transactionHeadID: model.TransactionHeadIID, // Send the list as a JSON string
                    documentTypeID: model.DocumentType != null ? model.DocumentType.Key : null
                }
            }).then(function (result) {
                if (result.data) {
                    $().showMessage($scope, $timeout, false, "Account posting has been completed!");

                    hideOverlay();
                    return false;
                }
                else {
                    $().showMessage($scope, $timeout, true, "Account posting has failed!");

                    hideOverlay();
                    return false;
                }
            }, function () {
                hideOverlay();
            });
        }
        else {
            $().showMessage($scope, $timeout, true, "Account posting only possible if the entry is saved!", 2000);
            return false;
        }
    };

}]);