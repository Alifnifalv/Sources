app.controller("SummaryViewControllerSupplierPurchaseOrderActionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("SummaryViewController.SupplierPurchaseOrderAction loaded");
        
        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.documentStatuses = null;
        $scope.OrderDetail = null;
        $scope.HeadIID = null;
        //$scope.GetOrderDocumentStatuses();

        $scope.SummaryViewInit = function (iid) {
            $scope.HeadIID = iid;
        }

        // get sku details
        $scope.GetOrderDetails = function () {
            //console.log(ViewModels.Transaction.HeadIID);
            $scope.GetOrderDocumentStatuses();
            $scope.OrderDetail = null;
            $('.preload-overlay', $(windowContainer)).show();
            var editUrl = "SuppTransaction/GetMarketPlaceOrderStatus?IID=" + $scope.HeadIID.toString();
            $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                if (!result.IsError) {
                    $scope.OrderDetail = result.data;
                } else {
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                }
                $('.preload-overlay', $(windowContainer)).hide();

            });
        };

        $scope.GetOrderDocumentStatuses = function () {
            //$('.preload-overlay', $(windowContainer)).show();
            var url = "Mutual/GetLookUpData?lookType=MarketPlaceOrderStatuses&defaultBlank=false";
            $http({ method: 'Get', url: url })
            .then(function (result) {
                if (!result.IsError) {
                    $scope.documentStatuses = result;

                } else {
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                }
                //$('.preload-overlay', $(windowContainer)).hide();

            });
        };


        // save sku details
        $scope.UpdateMarketPlaceOrderStatus = function (model) {
            $('.preload-overlay', $(windowContainer)).show();
            var url = "SuppTransaction/UpdateMarketPlaceOrderStatus";
            var sku = { orderDetail: model };
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(sku),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.IsError)
                        $().showMessage($scope, $timeout, true, result.UserMessage);
                    else
                        $scope.OrderDetail = result.data;
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

    }]);