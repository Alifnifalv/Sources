app.controller("ProductBundleController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.OnProductBundleSelect = function ($event, $element, gridModel) {
        showOverlay();
        var productId = gridModel.FromProduct.Key;
        if (productId === null || productId === 0) {
            $().showMessage($scope, $timeout, true, "Please Select Product Name");
            hideOverlay();
            return false;
        }

        var url = "Home/GetProductDetails?productId=" + productId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $timeout(function () {
                    $scope.$apply(function () {
                        gridModel.ItemCostPrice = result.data.CostPrice;
                        gridModel.ItemSellingPrice = result.data.SellingPrice;
                        gridModel.FromProductSKUMapID = result.data.FromProductSKUMapID;
                        gridModel.AvailableQuantity = result.data.AvailableQuantity;
                    });
                });
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.OnBundleSelect = function ($event, $element, detail) {
        showOverlay();
        var productskuId = detail.SKUID?.Key;
        if (productskuId === null || productskuId === 0) {
            $().showMessage($scope, $timeout, true, "Please Select Product Name");
            hideOverlay();
            return false;
        }

        var url = "Home/GetProductBundleData?productskuId=" + productskuId;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $timeout(function () {
                    $scope.$apply(function () {
                        detail.UnitPrice = result.data.CostPrice;
                        detail.AvalaibleQuantity = result.data.AvailableQuantity;
                        detail.Unit = { Key: result.data.UnitID, Value: result.data.Unit };
                        
                    });
                });
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SellingUnitGroupChanges = function (viewModel) {

        if (!viewModel.SellingUnitGroup) {
            return false;
        }
        showOverlay();
        viewModel.SellingUnit = null;
        var groupID = viewModel.SellingUnitGroup;

        $.ajax({
            type: "GET",
            data: { groupID: groupID },
            url: utility.myHost + "Home/GetUnitByUnitGroup",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.LookUps.SellingUnits = result;
            },
            error: function () {
                hideOverlay();

            },
            complete: function () {
                hideOverlay();
            }
        });
    };

    $scope.PurchasingUnitGroupChange = function (viewModel) {

        if (!viewModel.PurchaseUnitGroup) {
            return false;
        }
        showOverlay();
        viewModel.PurchasingUnit = null;
        var groupID = viewModel.PurchaseUnitGroup;

        $.ajax({
            type: "GET",
            data: { groupID: groupID },
            url: utility.myHost + "Home/GetUnitByUnitGroup",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.LookUps.PurchasingUnits = result;
            },
            error: function () {
                hideOverlay();

            },
            complete: function () {
                hideOverlay();
            }
        });
    };

    $scope.ProductCodeChanges = function (viewModel) {

        viewModel.ProductCode = viewModel.ProductCode.toUpperCase();
        viewModel.BarCode = viewModel.ProductCode;
    };

}]);