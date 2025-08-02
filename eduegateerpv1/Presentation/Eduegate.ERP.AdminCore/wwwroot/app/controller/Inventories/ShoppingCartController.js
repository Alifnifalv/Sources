app.controller("ShoppingCartController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("Shopping Cart Controller");

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService });


        $scope.OnCartSKUChange = function ($select, $index, gridModel) {
            var customer = 0;
            if ($scope.CRUDModel.Model.MasterViewModel.Customer != null && $scope.CRUDModel.Model.MasterViewModel.Customer != undefined) {
                customer = $scope.CRUDModel.Model.MasterViewModel.Customer.Key;
            }
            $.ajax({
                url: "ShoppingCart/GetInventoryDetailsSKUID?skuID=" + $select.selected.Key + "&customerID=" + customer,
                type: 'GET',
                success: function (productItem) {

                    $timeout(function () {
                        $scope.CRUDModel.Model.DetailViewModel[$index].AvailableQuantity = productItem.ViewModel.Quantity;
                        $scope.CRUDModel.Model.DetailViewModel[$index].UnitPrice = productItem.ViewModel.ProductDiscountPrice;
                    });
                    console.log(productItem);
                }
            });
            if (($index+1) == $scope.CRUDModel.Model.DetailViewModel.length) {
                var vm = angular.copy($scope.CRUDModel.DetailViewModel);
                //$timeout(function () {
                $scope.CRUDModel.Model.DetailViewModel.push(vm);
                //});
            }
            //alert("called");
        }

        $scope.CreateCart = function (customerId, contactId)
        {
            if (customerId == undefined) customerId = 0;

            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');

            $.ajax({
                url: "ShoppingCart/CreateCart?customerID=" + customerId,
                success: function (result) {
                    if (result.IsError) {
                        $().showMessage($scope, $timeout, true, result.Message);
                        return;
                    }

                    $scope.$apply(function () {
                        $scope.CRUDModel.Model.MasterViewModel.ShoppingCartID = result.ShoppingCartID;
                    });
                },
                complete: function () {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                }
            });
        }

        $scope.OnSKUChange = function ($select, $index, gridModel, customer) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
            var cartProductViewModel = {};
            cartProductViewModel.SKU = gridModel.SKUID.Key;
            cartProductViewModel.Quantity = 1;
            cartProductViewModel.CustomerID = customer;

            $.ajax({
                url: "ShoppingCart/AddToCart",
                type: "POST",
                data: cartProductViewModel,
                success: function (productItem) {
                    if (productItem.IsError) {
                        $().showMessage($scope, $timeout, true, productItem.Message);
                        return;
                    }

                    $timeout(function () {
                        if (productItem.data == null)
                            return;
                        $scope.CRUDModel.Model.DetailViewModel[$index].Description = productItem.data.ProductDetails[$index].ProductName;
                        //$scope.CRUDModel.Model.DetailViewModel[$index].DeliveryCharge = productItem.data.DeliveryCharge;
                        $scope.CRUDModel.Model.DetailViewModel[$index].AvailableQuantity = productItem.data.ProductDetails[$index].ProductAvailableQuantity;
                        $scope.CRUDModel.Model.DetailViewModel[$index].Quantity = productItem.data.ProductDetails[$index].ProductCartQuantity;
                        $scope.CRUDModel.Model.DetailViewModel[$index].UnitPrice = productItem.data.ProductDetails[$index].ProductDiscountPrice;
                        $scope.CRUDModel.Model.DetailViewModel[$index].Amount = (productItem.data.ProductDetails[$index].ProductCartQuantity) * (productItem.data.ProductDetails[$index].ProductDiscountPrice);

                        if ($scope.CRUDModel.Model.DetailViewModel[$index].LookUps == undefined) {
                            $scope.CRUDModel.Model.DetailViewModel[$index].LookUps = [];
                        }

                        $scope.CRUDModel.Model.DetailViewModel[$index].LookUps['DeliveryOptions'] = productItem.data.ProductDetails[$index].DeliveryOptions;
                        $scope.CRUDModel.Model.MasterViewModel.DeliveryCharge = productItem.data.DeliveryCharge;
                        $scope.CRUDModel.Model.MasterViewModel.Amount = productItem.data.Total;
                        $scope.CRUDModel.Model.MasterViewModel.SubTotal = productItem.data.SubTotal;
                    });
                    console.log(productItem);
                },
                complete: function () {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                }
            });
            if (($index+1) == $scope.CRUDModel.Model.DetailViewModel.length) {
                var vm = angular.copy($scope.CRUDModel.DetailViewModel);
                $scope.CRUDModel.Model.DetailViewModel.push(vm);
            }
        }


        $scope.ResetCart = function (cartId, statusID) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');

            $.ajax({
                url: "ShoppingCart/UpdateCartStatus?cartID=" + cartId + "&statusID=" + statusID + "&paymentMethod=" + null,
                success: function (result) {
                    if (result.IsError) {
                        $().showMessage($scope, $timeout, true, productItem.Message);
                        return;
                    }
                    $timeout(function () {
                        $scope.CRUDModel = angular.copy($scope.ModelStructure);
                    });
                },
                complete: function () {
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                }
            });
        }

        function OnCustomerChange (model) {

            if (model.ShoppingCartID != 0) {
                model.IsShoppingCart = true;
            }
            else {
                model.IsShoppingCart = false;
            }
        }

        $scope.EditCustomer = function (view, ID) {
            if ($scope.ShowWindow("Edit" + view, "Edit " + view, "Edit" + view))
                return;

            $("#Overlay").fadeIn(100);

            var editUrl = view + "/Edit/" + ID.toString();
            $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $("#LayoutContentSection").append($compile(result)($scope));
                $scope.AddWindow("Edit" + view, "Edit " + view, "Edit" + view);
                $("#Overlay").fadeOut(100);
            });
        }

        $scope.SaveOrderAddress = function (paramBillingAddress, paramShippingAddress, callback, customerID) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).show();
            // Save billing/shipping addresses for current cart
            var url = 'ShoppingCart/SaveOrderAddresses?billingAddressID=' + paramBillingAddress + '&shippingAddressID=' + paramShippingAddress + '' + '&customerID=' + customerID;

            $http({ method: 'GET', url: url })
            .then(function (result) {
                $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
            });
        }

        $scope.UpdateItem = function (index, productDetail, quantity, isEdit, customerID) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).show();
            var newQuantity = parseInt(productDetail.Quantity);

            var serviceUrl = 'ShoppingCart/UpdateCart/';
            var data = {
                'SKUID': productDetail.SKUID.Key, 'quantity': newQuantity, 'customerID': customerID
            };
            //call service to update
            $.ajax({
                url: serviceUrl,
                type: "POST",
                data: data,
                success: function (result) {
                    if (result.IsError != undefined && !result.IsError) {
                        $timeout(
                                         function () {
                                             $scope.CRUDModel.Model.DetailViewModel[index].Amount = result.data.Products[index].Total;
                                             $scope.CRUDModel.Model.MasterViewModel.DeliveryCharge = result.data.DeliveryCharge;
                                             $scope.CRUDModel.Model.MasterViewModel.Amount = result.data.Total;
                                             $scope.CRUDModel.Model.MasterViewModel.SubTotal = result.data.SubTotal;
                                             try {
                                                 for (var i = 0; i < result.data.Products.length; i++) {
                                                     if (result.data.Products[i].SKUID == productDetail.SKUID.Key) {
                                                         productDetail.Quantity = result.data.Products[i].Quantity;
                                                         break;
                                                     }
                                                 }
                                             }
                                             catch (e) { }
                                             if (parseInt(productDetail.Quantity) <= 0) {
                                                 for (var i = 0; i < $scope.CRUDModel.Model.DetailViewModel.length; i++) {
                                                     if ($scope.CRUDModel.Model.DetailViewModel[index].SKUID.Key == productDetail.SKUID.Key) {
                                                         $scope.CRUDModel.Model.DetailViewModel.splice(index, 1);
                                                         break;
                                                     }
                                                 }
                                             }
                                             if (productDetail.Quantity != newQuantity) {
                                                 $().showMessage($scope, $timeout, true, "We're sorry ! We are able to accommodate only" + productDetail.Quantity)
                                             }
                                             $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                                         }
                             );
                    }
                }
           });
        };



        $scope.RemoveItem = function (index,currentRow,customerID) {
            RemoveCart(index, currentRow, customerID);
        }

        function RemoveCart(index,currentRow,customerID) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).show();
            var url = "ShoppingCart/RemoveItem?SKUID=" + currentRow.SKUID.Key + "&customerID=" + customerID;
            $scope.currentRow = currentRow;
            $.ajax({
                url: url,
                type: 'GET',
                success: function (result) {
                    if (result.IsError != undefined && !result.IsError) {
                        /* find index then remove */
                        for (var i = 0; i < $scope.CRUDModel.Model.DetailViewModel.length; i++) {
                            if ($scope.CRUDModel.Model.DetailViewModel[index].SKUID.Key == currentRow.SKUID.Key) {
                                $timeout(
                                        function () {
                                            $scope.CRUDModel.Model.MasterViewModel.DeliveryCharge = result.data.DeliveryCharge;
                                            $scope.CRUDModel.Model.MasterViewModel.Amount = result.data.Total;
                                            $scope.CRUDModel.Model.MasterViewModel.SubTotal = result.data.SubTotal;
                                            $scope.CRUDModel.Model.DetailViewModel.splice(index, 1);
                                        }
                                );
                                $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                                break;
                            }
                        }
                    }
                }
            });
        }

        $scope.DeliverySelectionChanged = function (productID, deliveryTypeID,customerID) {
            UpdateItemDelivery(productID, deliveryTypeID,customerID);
        }
        function UpdateItemDelivery(productID, deliveryTypeID,customerID) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).show();

            var data = { 'SKUID': productID, 'deliveryTypeID': deliveryTypeID, 'customerID': customerID };
            url = "ShoppingCart/UpdateCartDelivery?SKUID=" + productID + "&deliveryTypeID=" + deliveryTypeID + "&customerID=" + customerID;
            //call service to update
            $.ajax({
                url: url,
                type: "POST",
                data: data,
                success: function (result) {
                    if (result.IsError != undefined && !result.IsError) {
                        $timeout(
                        function () {
                            $scope.CRUDModel.Model.MasterViewModel.DeliveryCharge = result.data.DeliveryCharge;
                            $scope.CRUDModel.Model.MasterViewModel.Amount = result.data.Total;
                            $scope.CRUDModel.Model.MasterViewModel.SubTotal = result.data.SubTotal;
                            $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                        }

                    );

                    }
                }
            });
        };

    }]);