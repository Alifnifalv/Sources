app.controller("SummaryViewSupplierSKUController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    	console.log("SummaryViewSupplierSKUController");
    	$scope.ProductSKUDetails = null;
        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));


        $scope.SummaryViewInitChild = function (window, viewName, iid, defaultDynamicView) {
            this.SummaryViewInit(window, viewName, iid, defaultDynamicView);
            //$scope.GetProductSKUDetails(iid);
        }

        // get sku details
        $scope.GetProductSKUDetails = function (skuID) {
            $scope.ProductSKUDetails = null;
            $('.preload-overlay', $(windowContainer)).show();
            var editUrl = "SupplierSKU/GetProductSKUDetails?IID=" + skuID.toString();
            $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                if (!result.IsError) {
                    $scope.ProductSKUDetails = result.data;
                    //console.log($scope.SKUViewModel);
                    
                } else {
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                }
                $('.preload-overlay', $(windowContainer)).hide();

            });
        };


        // save sku details
        $scope.UpdateSupplierSKUInventory = function (model) {
            $('.preload-overlay', $(windowContainer)).show();
            var url = "SupplierSKU/UpdateSupplierSKUInventory";
            var sku = { sku: model };
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(sku),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.IsError)
                        $().showMessage($scope, $timeout, true, result.UserMessage);

                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }
        
    }]);