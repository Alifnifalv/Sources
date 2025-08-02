app.controller("StockInController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {

    console.log("Stock-in view loaded successfully");

    //Initializing the pos view model
    $scope.Init = function (model) {

        $scope.StockModel = model;
        $scope.Searchtext = "";
        $scope.GetProducts($scope.Searchtext);
        $scope.GetSuppliers($scope.Searchtext);
        $scope.DefaultQuantityText = 1;

        $timeout(function () {
            $("#StockInProduct").find("input").first().bind("keyup", function (event) {
                //we should avoid DB call while pressing Up and Drown arrows
                if(event.keyCode != 38 && event.keyCode != 40) {
                    $scope.GetProducts($(this).val());
                }
            });

            $("#StockInSupplier").find("input").first().bind("keyup", function () {
                if(event.keyCode != 38 && event.keyCode != 40) {
                    $scope.GetSuppliers($(this).val());
                }
            });

            $scope.CaluculateTotalPrice();

        }, 100);
    }

    //Load all the products
    $scope.GetProducts = function (searchText) {

        $.ajax({
            url: "Catalogs/ProductSKU/ProductSKUSearch?searchText=" + searchText,
            type: 'GET',
            success: function (productsList) {

                $scope.Products = [];

                if (productsList != undefined && productsList != null) {
                    //Looping produst item list and adding to the scope array variable
                    $.each(productsList, function (index, item) {
                        $scope.Products.push(item);
                    });

                    $scope.$apply();
                }
            }
        })
    }

    //Load all the suppliers
    $scope.GetSuppliers = function (searchText) {

        $.ajax({
            url: "Inventory/GetSuppliers?searchText=" + searchText,
            type: 'GET',
            success: function (supplierList) {

                if (supplierList != undefined && supplierList != null) {

                    $scope.Suppliers = [];

                    if (supplierList != undefined && supplierList != null) {
                    //Looping customer item list and adding to the scope array variable
                    $.each(supplierList, function (index, item) {
                        $scope.Suppliers.push(item);
                    });
                    $scope.$apply();
                }
            }
            }
        })
    }

    $scope.OnChangeProductSelect2 = function (product, select2Ctrl) {

        $scope.ProductSelect2Control = select2Ctrl;
        $scope.ProductSelect2Control.selected.Name = product.ProductSKU;

        if (event.keyCode == 9)
            $scope.AddProcuctItem(product);
    }

    $scope.OnChangeSupplierSelect2 = function (supplier, select2Ctrl) {

        $scope.SupplierSelect2Ctrl = select2Ctrl;
        $scope.SupplierSelect2Ctrl.selected.Name = supplier.SupplierName;

        $scope.StockModel.SupplierID = supplier.SupplierIID;
    }

    //Click on add adding the item to POS view page
    $scope.AddProcuctItem = function (product) {

        if (product != null) {

            //Product select2 control
            if ($scope.ProductSelect2Control.selected.Name == "" || $scope.ProductSelect2Control.selected.Name == null)
                return;

            var productExists = [];

            if ($scope.StockModel.TransactionDetails.length > 0)
                var productExists = $.grep($scope.StockModel.TransactionDetails, function (result) { return result.ProductSKUMapID == product.ProductSKUMapIID; }).length;

            if (productExists > 0) {
                var gridItem = $.grep($scope.StockModel.TransactionDetails, function (result) { return result.ProductSKUMapID == product.ProductSKUMapIID; })[0];
                $scope.IncreaseQuantity(gridItem.QuantityText, gridItem.ProductSKUMapID, gridItem.Quantity, gridItem.UnitPrice);
            }
            else {
                var productItem = {
                        DetailIID: 0, HeadID: 0, ProductID: product.ProductIID, ProductName: product.ProductSKU, ImageFile: product.ImageFile,
                    ProductSKUMapID: product.ProductSKUMapIID, ProductSKU: product.ProductSKU,
                        Amount: product.Price, QuantityText: $scope.DefaultQuantityText, Quantity: product.Quantity,
                        UnitPrice: product.UnitPrice, DiscountPercentage: product.DiscountPercentage
                    }; //Adding as a json object to the pos view model

                $scope.StockModel.TransactionDetails.push(productItem);
                $scope.CaluculateTotalPrice();
            }

            $scope.ProductSelect2Control.selected.Name = "";
            $scope.GetProducts($scope.Searchtext);
        }
    }

    // Calculating the total price as price * quantity
    $scope.CaluculateTotalPrice = function () {

        $scope.StockModel.SubTotal = 0;

        if ($scope.StockModel.TransactionDetails != undefined && $scope.StockModel.TransactionDetails.length > 0) {
            $.each($scope.StockModel.TransactionDetails, function (index, item) {

                $scope.StockModel.SubTotal += item.Amount; //Calculating total price for all the grid items
            });

            $scope.StockModel.TotalPrice = $scope.StockModel.SubTotal;

            if ($scope.StockModel.TotalDiscount > 0)
                $scope.TotalDiscountByAmount();
            if ($scope.StockModel.TotalDiscountPercentage > 0)
                $scope.TotalDiscountByPercentage();
        }
        else {
            $scope.StockModel.TotalDiscount = 0;
            $scope.StockModel.TotalDiscountPercentage = 0;
            $scope.StockModel.TotalProductDiscount = 0;
            $scope.StockModel.TotalPrice = 0;
        }
    }

    $scope.ClosePointOfSaleWindow = function () {
        $("#PointOfSaleView").css("display", "none");
    }

    $scope.IncreaseQuantity = function (quantity, productSKUMapID, quantityCount, productPrice) {

        var selectedItem = $scope.GetItemFromTheGridItems(productSKUMapID); // Getting the selected item from the grid

        if (selectedItem != undefined && selectedItem != null) {
            selectedItem.QuantityText = quantity + $scope.DefaultQuantityText; // Increasing the quantity
            selectedItem.Amount = selectedItem.QuantityText * productPrice * (100 - selectedItem.DiscountPercentage) / 100; // Calculating price with quantity
            $scope.CaluculateTotalPrice(); // Calculating the total price
        }
    }

    $scope.DecreseQuantity = function (quantity, productSKUMapID, quantityCount, productPrice) {

        if (quantity <= 1)
            return; // Stopping the decrasing quantity for negative values and 0

        var selectedItem = $scope.GetItemFromTheGridItems(productSKUMapID); // Getting the selected item from the grid

        if (selectedItem != undefined && selectedItem != null) {
            selectedItem.QuantityText = quantity - $scope.DefaultQuantityText; // Decreasing the quantity
            selectedItem.Amount = selectedItem.QuantityText * productPrice * (100 - selectedItem.DiscountPercentage) / 100; // Calculating price with quantity
            $scope.CaluculateTotalPrice(); // Calculating the total price
        }
    }

    $scope.GetItemFromTheGridItems = function (productSKUMapID) {

        return $.grep($scope.StockModel.TransactionDetails, function (result) { return result.ProductSKUMapID == productSKUMapID; })[0]; // Getting item from the grid
    }

    $scope.RemoveItemFromStockGrid = function (index, stockItem) {

        $scope.StockModel.TransactionDetails.splice(index, 1); //Removing item from the grid
        $scope.CaluculateTotalPrice(); // Calculating the total price
    }

    $scope.RowSelected = function (event) {

        //close all the editable row and enable to edit selected row
        $(".currentsale").find("tr").removeClass("editable");
        $(event.currentTarget).addClass("editable");
    }

    $scope.TouchView = function () {
        $('.table-wrap').toggleClass('active');
    }

    $scope.SaveTransactions = function () {

        $scope.submitted = true;
        var formValid = $scope.StockIn.$valid;

        if ($scope.StockModel.SupplierID <= 0)
            return;

        if ($scope.StockModel.TransactionDetails.length <= 0) {
            alert("Please add product(s)");
            return;
        }

        if (formValid == true) {
            $scope.StockModel.TransactionDate = $("#StockInDatePicker").val();

            $.ajax({
                url: "Inventory/SaveTransactions",
                type: 'POST',
                data: $scope.StockModel,
                success: function (result) {

                    if (result.IsError == false) {
                        $scope.LoadPurchaseGrid();
                    }
                }
            });
        }
    }

    //Click on cancel redirecting to respective list view
    $scope.CancelStockIn = function () {
        $scope.LoadPurchaseGrid();
    }

    $scope.CreateSupplier = function () {

        $("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        $.ajax({
            //url: "Supplier/SupplierCreation",
            url: "Supplier/Create",
            type: 'GET',
            success: function (result) {
                $("#LayoutContentSection").html($compile(result)($scope)).updateValidation();
            }
        })
    }

    $scope.LoadPurchaseGrid = function () {
        $("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:40px;color:white;"></span></center>');

        $.ajax({
            url: "Purchase/List",
            type: 'GET',
            success: function (result) {
                $("#LayoutContentSection").html($compile(result)($scope));
            }
        })
    }

    $scope.ProductDiscount = function () {

        $scope.StockModel.SubTotal = 0;

        $.each($scope.StockModel.TransactionDetails, function (index, item) {

             if(item.DiscountPercentage == undefined){
                   return;  
             }
            //var discountAmount = item.UnitPrice * item.DiscountPercentage / 100;
            item.Amount = item.QuantityText * item.UnitPrice * (100 - item.DiscountPercentage) / 100;
            $scope.StockModel.SubTotal += item.Amount; //Calculating total price for all the grid items
        });

        $scope.StockModel.TotalPrice = $scope.StockModel.SubTotal;

        //Resetting the total discount values
        $scope.StockModel.TotalDiscount = 0;
        $scope.StockModel.TotalDiscountPercentage = 0;
        $scope.StockModel.TotalProductDiscount = 0;
    }

    $scope.TotalDiscountByAmount = function (event) {

        if($scope.StockModel.TotalDiscount == undefined) {
            return;
        }
        if ($scope.StockModel.TransactionDetails.length > 0) {

            if ($scope.StockModel.TotalDiscount > $scope.StockModel.SubTotal)
                $scope.StockModel.TotalDiscount = 0;

            $scope.StockModel.TotalProductDiscount = $scope.StockModel.TotalDiscount * 1; //change to numeric value multiplying into 1
            $scope.StockModel.TotalPrice = $scope.StockModel.SubTotal - $scope.StockModel.TotalDiscount;
        }
        else
        {
            $scope.CaluculateTotalPrice();
        }
    }

    $scope.TotalDiscountByPercentage = function (event) {

         if ($scope.StockModel.TotalDiscountPercentage == undefined)
                return

        if ($scope.StockModel.TransactionDetails.length > 0) {
            $scope.StockModel.TotalDiscountPercentage = $scope.StockModel.TotalDiscountPercentage *1; //change to numeric value multiplying into 1
            $scope.StockModel.TotalProductDiscount = $scope.StockModel.SubTotal * $scope.StockModel.TotalDiscountPercentage / 100;
            $scope.StockModel.TotalPrice = $scope.StockModel.SubTotal - $scope.StockModel.TotalProductDiscount;
        }
        else
        {
            $scope.CaluculateTotalPrice();
        }
    }

    $scope.FillDiffDatasOnly = function ($event, $element, viewModel) {
        if (viewModel.FillDiffDatasOnly == true) {
            console.log(viewModel);
        }
    }

}]);