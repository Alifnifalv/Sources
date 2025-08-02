app.controller("SummaryViewShoppingCartController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("SummaryViewProductSKUPriceSettingController");

        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.SummaryViewInitChild = function (window, viewName, iid, defaultDynamicView, modelQuantity) {
            this.SummaryViewInit(window, viewName, iid, defaultDynamicView, modelQuantity);
        }

        $scope.SetUpdateCartDefault = function (viewModel) {
            $scope.$apply(function () {
                viewModel["UpdateCart"] = { "CartStatus": viewModel.ShoppingCart.CartStatus };
            });
        }

        $scope.UpdateCart = function (cart, event) {
            $(event.currentTarget, $(windowContainer)).disabled = true;
            $('.preload-overlay', $(windowContainer)).show();

            var url = "ShoppingCart/UpdateCartCustomerAndStatus?cartID=" + cart.CartID.toString()
                + "&customerID=" + cart.CustomerID.toString() 
                + "&cartStatus=" + cart.CartStatus 
                + "&paymentMethod=" + cart.PaymentMethod;
            $http({ method: 'Get', url: url })
            .then(function (result) {
                cart.Message = "Succesfully updated the status.";
                $('.preload-overlay', $(windowContainer)).hide();
                $scope.CloseSummaryPanel(event);
            })
            .error(function (result) {
                cart.Message = "Cart update failed.";
                $('.preload-overlay', $(windowContainer)).hide();
                $(event.currentTarget, $(windowContainer)).disabled = false;
            });
        }

        $scope.ProcessOrder = function (event) {
            $('.preload-overlay', $(windowContainer)).show();
            $http({ method: 'Post', url: 'ShoppingCart/ProcessOrder', data: JSON.stringify($scope.ViewModels.ShoppingCart) })
           .then(function (result) {
               $timeout(function () {
                   $scope.ViewModels.ShoppingCart = result;
               });
               $('.preload-overlay', $(windowContainer)).hide();
               $scope.CloseSummaryPanel(event);
           })
             .error(function (result) {
                 cart.Message = "Process order failed.";
                 $('.preload-overlay', $(windowContainer)).hide();
                 $(event.currentTarget, $(windowContainer)).disabled = false;
             });
        }
    }]);