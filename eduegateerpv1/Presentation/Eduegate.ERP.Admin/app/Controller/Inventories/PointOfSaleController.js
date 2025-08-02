app.controller("PointOfSaleController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", '$rootScope', function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
    console.log("Point Of Sale View Loaded");

    var windowContainer = null;

    //Initializing the pos view model
    $scope.Init = function (window, model, width, emptyModel) {
        windowContainer = '#' + window;
        printerWidth = width;

        if (!model.DocumentTypeID) {
            $().showMessage($scope, $timeout, true, 'POS document is not configured properly or not exists.');
        }

        $scope.ModelStructure = angular.copy(emptyModel);
        $scope.PosModel = model;
        $scope.Searchtext = "";
        $scope.GetProductList($scope.Searchtext);
        $scope.GetCustomers($scope.Searchtext);
        $scope.GetTaxTemplates();
        $scope.GetShortCutKeys();
        $scope.DefaultQuantityText = 1;
        $scope.GrandTotal = 0;
        $scope.SubTotal = 0;
        $scope.TotalTaxes = 0;
        $scope.PosModel.DeliveryCharge = 0;
        $scope.ProductPrice = [];
        $scope.TaxTemplates = [];
        $scope.ShortCutKeys = [];
        $scope.PosModel.Payment.TotalAmountReceived = 0;

        if ($scope.PosModel.TransactionPaymentMethods == null)
            $scope.PosModel.TransactionPaymentMethods = [];

        $timeout(function () {
            $("#POSProduct", $(windowContainer)).find("input").first().bind("keyup", function () {
                if (event.keyCode !== 38 && event.keyCode != 40) {
                    $scope.GetProductList($(this).val());
                }
            });

            $("#POSCustomer", $(windowContainer)).find("input").first().bind("keyup", function () {
                if (event.keyCode !== 38 && event.keyCode != 40) {
                    $scope.GetCustomers($(this).val());
                }
            });

            $scope.GetCategoryLists('');
        }, 100);
    };

    $scope.CallFunction = function (name) {
        if (angular.isFunction($scope[name]))
            $scope[name]();
    };

    $scope.GetShortCutKeys = function () {
        var url = "Mutual/GetScreenMetadata?screen=PointOfSale";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.ShortCutKeys = result.data;
                $.each(result.data, function (index, data) {
                    shortcut.add(data.KeyCode, function () {
                        $scope.CallFunction(data.Action);
                    });
                });
            });
    };

    $scope.GetTaxTemplates = function (posProduct) {
        var url = "Mutual/GetLookUpData?lookType=TaxTemplates&defaultBlank=false";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.TaxTemplates = result.data;
            });
    };


    $scope.GetTaxTemplateItem = function (product) {
        if (product.TaxTemplate == "" || product.TaxTemplate == null) {
            product.TaxPercentage = 0;
            product.HasTaxInclusive = false;
            return;
        }

        var url = "Mutual/GetTaxTemplateItem?taxTemplateID=" + product.TaxTemplate;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                product.TaxPercentage = result.data.Percentage;
                product.HasTaxInclusive = result.data.HasTaxInclusive;
            });
    };

    $scope.ReloadProduct = function (cat) {
        $.ajax({
            url: "Catalogs/ProductSKU/ProductSKUByCategoryID?categoryID=" + cat.CategoryID,
            type: 'GET',
            success: function (productsList) {
                $scope.$apply(function () {
                    $scope.Products = [];
                    if (productsList != undefined && productsList != null) {

                        //Looping produst item list and adding to the scope array variable
                        $.each(productsList.Data, function (index, item) {

                            if (item.Quantity != null && item.SellingQuantityLimit != null) {
                                if (item.SellingQuantityLimit > item.Quantity)
                                    item.Quantity = item.SellingQuantityLimit;
                            }

                            $scope.Products.push(item);
                        });
                    }
                });
            }
        });
    };

    $scope.GetCategoryLists = function (searchText) {
        $.ajax({
            url: "Catalogs/Product/GetCategoryList?searchText=" + searchText,
            type: 'GET',
            success: function (categoryLists) {
                $scope.$apply(function () {
                    $scope.Categories = [];
                    if (categoryLists != undefined && categoryLists != null) {

                        //Looping produst item list and adding to the scope array variable
                        $.each(categoryLists, function (index, item) {
                            $scope.Categories.push(item);
                        });
                    }
                    $('.preload-overlay', $(windowContainer)).fadeOut(500);
                });
            }
        });
    };

    var getProductAjaxHandler = null;

    $scope.InitializeProductSelect2cntrl = function (cntrl) {
        $scope.ProductSelect2Control = cntrl;
    };

    //Load all the products
    $scope.GetProductList = function (searchText, isCallFromBarCode) {
        searchText = searchText.trim();

        if (getProductAjaxHandler != null) {
            getProductAjaxHandler.abort();
        }

        getProductAjaxHandler = $.ajax({
            url: "Catalogs/ProductSKU/ProductSKUSearch?searchText=" + searchText,
            type: 'GET',
            success: function (productsList) {
                productsList = productsList.Data;
                // after get the record from service, filter the record by SKUCode and BarCode
                if (isCallFromBarCode) {
                    productsList = productsList.filter(function (item, index) {
                        if (item.BarCode && item.ProductSKU) {
                            return item.BarCode.indexOf(searchText) || item.ProductSKU.indexOf(searchText);
                        }
                    });
                }

                $scope.Products = [];
                if (productsList != undefined && productsList != null) {

                    //Looping produst item list and adding to the scope array variable
                    $.each(productsList, function (index, item) {

                        if (item.Quantity != null && item.SellingQuantityLimit != null) {
                            if (item.SellingQuantityLimit > item.Quantity)
                                item.Quantity = item.SellingQuantityLimit;
                        }

                        $scope.Products.push(item);
                    });

                    if (isCallFromBarCode && productsList.length > 0 && searchText.length > 0) {
                        for (var i = 0; i < productsList.length; i++) {
                            $scope.ProductSelect2Control.selected = productsList[i];
                            $scope.ProductSelect2Control.selected.Name = productsList[i].ProductSKU;
                            $scope.AddProcuctItem(productsList[i]);
                            $scope.ProductSelect2Control.selected = null;
                            $("#ProductSearchByBarCode", $(windowContainer)).val('');
                        }

                        //if ($scope.PosModel.TransactionDetails.length == 0) {
                        //    alert('Selected product is not avaible now.');
                        //}
                    }
                    else if (isCallFromBarCode && searchText.length > 0) {
                        //alert("Selected product is not avaible now.");
                    }
                    $scope.$apply();
                }

            }
        });


    };

    $scope.RefreshProductSelect2 = function (event) {
        if (event) {
            $scope.GetProductList(event.currentTarget.value, true);
        }
    };

    $scope.BarCodeKeyPress = function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            $scope.GetProductList(event.currentTarget.value, true);
        }
    };

    //Load all the customers
    $scope.GetCustomers = function (searchText) {

        $.ajax({
            url: "Customer/GetCustomerByCustomerIdAndCR?searchText=" + searchText,
            type: 'GET',
            success: function (customerList) {
                $scope.Customers = [];
                if (customerList != undefined && customerList != null) {

                    //Looping customer item list and adding to the scope array variable
                    $.each(customerList.Data, function (index, item) {
                        $scope.Customers.push(item);
                    });

                    $scope.$apply();
                }
            }
        });
    };

    $scope.OnChangeProductSelect2 = function (product, select2Ctrl) {
        $scope.ProductSelect2Control.selected.Name = product.ProductSKU;
        $scope.AddProcuctItem(product);
    };

    $scope.OnChangeCustomerSelect2 = function (customer, customerSelect2Ctrl) {

        $scope.PosModel.CustomerID = customer.Key;
        customerSelect2Ctrl.selected.Name = customer.Value;
    };

    //Click on add adding the item to POS view page
    $scope.AddProcuctItem = function (product) {

        if (product !== null) {

            if ($scope.PosModel.TransactionDetails.length > 0)
                var productExists = $.grep($scope.PosModel.TransactionDetails, function (result) { return result.ProductSKUMapID == product.ProductSKUMapIID; }).length;

            if (productExists > 0) {
                var gridItem = $.grep($scope.PosModel.TransactionDetails, function (result) { return result.ProductSKUMapID == product.ProductSKUMapIID; })[0];
                $scope.IncreaseQuantity(gridItem);
            }
            else {
                var productItem = {
                    DetailIID: 0, HeadID: 0, ProductID: product.ProductIID, ProductName: product.ProductSKU, ImageFile: product.ImageFile,
                    ProductSKUMapID: product.ProductSKUMapIID, ProductSKU: product.ProductSKU,
                    Amount: product.Price == 0 ? $scope.DefaultQuantityText * product.UnitPrice * (100 - product.DiscountPricePercentage) / 100 : $scope.DefaultQuantityText * product.Price * (100 - product.DiscountPricePercentage) / 100,
                    QuantityText: $scope.DefaultQuantityText, Quantity: product.Quantity,
                    UnitPrice: product.UnitPrice, DiscountPercentage: product.DiscountPricePercentage,
                    BarCode: product.BarCode,
                    ProductCost: product.ProductCost,
                    TaxTemplateID: product.TaxTemplateID,
                    TaxTemplate: product.TaxTemplateID.toString(),
                    TaxAmount: product.TaxAmount,
                    TaxPercentage: product.TaxPercentage,
                    HasTaxInclusive: product.HasTaxInclusive
                }; //Adding as a json object to the pos view model

                $scope.PosModel.TransactionDetails.push(productItem);
                //$scope.CaluculateTotalPrice();
            }

            //$scope.ProductSelect2Control.selected.Name = "";
            //$scope.GetProductList($scope.Searchtext);
        }
    };

    // Calculating the total price as price * quantity
    $scope.CaluculateTotalPrice = function () {

        $scope.PosModel.SubTotal = 0;
        if ($scope.PosModel.DeliveryCharge == null || $scope.PosModel.DeliveryCharge == "") {
            $scope.PosModel.DeliveryCharge = 0;
        }

        if ($scope.PosModel.TransactionDetails.length > 0) {

            $.each($scope.PosModel.TransactionDetails, function (index, item) {
                item.QuantityText = item.QuantityText.length == 0 ? 0 : parseInt(item.QuantityText);
                // check Quantity should not be more than available quantity
                //if (item.QuantityText > item.Quantity) {
                //    item.QuantityText = item.Quantity;
                //    alert('Quantity should not be more than available quantity.');
                //    return;
                //}
                item.Amount = parseInt(item.QuantityText) * item.UnitPrice
                $scope.PosModel.SubTotal += item.Amount; //Calculating total price for all the grid items
            });

            // Set value on each field
            $scope.PosModel.TotalPrice = $scope.PosModel.SubTotal + parseFloat($scope.PosModel.DeliveryCharge);
            $scope.PosModel.Payment.Amount = $scope.PosModel.SubTotal + parseFloat($scope.PosModel.DeliveryCharge);

            /* totalWithoutDeliveryCharge will use by getTotalByDeliveryCharge method */
            $scope.totalWithoutDeliveryCharge = parseFloat($scope.PosModel.SubTotal);

            if ($scope.PosModel.TotalDiscount > 0)
                $scope.TotalDiscountByAmount();
            if ($scope.PosModel.TotalDiscountPercentage > 0)
                $scope.TotalDiscountByPercentage();
        }
        else {
            $scope.PosModel.TotalDiscount = 0;
            $scope.PosModel.TotalDiscountPercentage = 0;
            $scope.PosModel.TotalProductDiscount = 0;
            $scope.PosModel.TotalPrice = parseFloat($scope.PosModel.SubTotal) + parseFloat($scope.PosModel.DeliveryCharge);
            $scope.totalWithoutDeliveryCharge = parseFloat($scope.PosModel.SubTotal);
        }
    };

    $scope.ClosePointOfSaleWindow = function () {
        $("#PointOfSaleView", $(windowContainer)).css("display", "none");
    };

    $scope.IncreaseQuantity = function (row) {
        row.QuantityText = parseFloat(row.QuantityText);
        //if (quantity >= quantityCount)
        //    return; // Stopping when quantity is more than the quantity count

        ///*incremental quantity shuld be always less than actual quantity */
        //if (avilableQty <= quantity)
        //    return;

        //var selectedItem = $scope.GetItemFromTheGridItems(productSKUMapID); // Getting the selected item from the grid
        //selectedItem.QuantityText = quantity + $scope.DefaultQuantityText; // Increasing the quantity
        ////selectedItem.Amount = selectedItem.QuantityText * productPrice * (100 - selectedItem.DiscountPercentage) / 100;; // Calculating price with quantity
        row.QuantityText = row.QuantityText + 1;
        //$scope.CaluculateTotalPrice(); // Calculating the total price
    };

    $scope.DecreseQuantity = function (row) {
        row.QuantityText = parseFloat(row.QuantityText);

        if (row.QuantityText <= 1)
            return; // Stopping the decrasing quantity below one.

        //var selectedItem = $scope.GetItemFromTheGridItems(productSKUMapID); // Getting the selected item from the grid
        //selectedItem.QuantityText = quantity - $scope.DefaultQuantityText; // Decreasing the quantity
        ////selectedItem.Amount = selectedItem.QuantityText * productPrice * (100 - selectedItem.DiscountPercentage) / 100;; // Calculating price with quantity
        row.QuantityText = row.QuantityText - 1;
        //$scope.CaluculateTotalPrice(); // Calculating the total price
    };

    $scope.GetItemFromTheGridItems = function (productSKUMapID) {

        return $.grep($scope.PosModel.TransactionDetails, function (result) { return result.ProductSKUMapID == productSKUMapID; })[0]; // Getting item from the grid
    };

    $scope.RemoveItemFromPOSGrid = function (index, pOSSalesReturn) {
        $scope.PosModel.TransactionDetails.splice(index, 1); //Removing item from the grid
        //$scope.CaluculateTotalPrice(); // Calculating the total price
        $scope.MakeObjectPOSSalesReturnTransactionDetail(pOSSalesReturn);
    };

    $scope.RowSelected = function (event) {

        //close all the editable row and enable to edit selected row
        $(".currentsale", $(windowContainer)).find("tr").removeClass("editable");
        $(event.currentTarget).addClass("editable");

        //close all the tooltip popup and enable the selected popuypo
        $('.product_details', $(windowContainer)).fadeOut();
        $(event.currentTarget).find(".product_details").fadeIn(200);
    };

    $scope.TouchView = function () {
        $('.table-wrap', $(windowContainer)).toggleClass('active');
    };

    //Paying point of sale items

    $scope.GlobalPopupWindow = function (currentTarget, callback) {
        var popdetect = $(currentTarget).attr('data-popup-type');
        //$('.preload-overlay', $(windowContainer)).show();
        var xpos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerWidth() / 2;
        var ypos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerHeight() / 2;
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).fadeIn(500, function () { if (callback) { callback() } });
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).addClass('show');

        $('.popup', $(windowContainer)).on('click', 'span.close', function () {
            $(this).closest('.popup').fadeOut(500);
            $(this).closest('.popup').removeClass('show');
            $('.preload-overlay', $(windowContainer)).fadeOut(500);
        });

        $('.popupbtn a', $(windowContainer)).on('click', function (e) {
            e.preventDefault();
        });

        $('.preload-overlay', $(windowContainer)).on('click', function () {
            $('.popup', $(windowContainer)).removeClass('show');
            $('.popup', $(windowContainer)).fadeOut(500);
            $('.preload-overlay', $(windowContainer)).fadeOut(500);
        });
    };

    $scope.PickProductPrice = function (event, productSKU) {
        $scope.GlobalPopupWindow(event.currentTarget);
        $scope.GetProductPriceSettingForSKU(productSKU);
    };

    $scope.LoadPaymentScreen = function () {
        $scope.GlobalPopupWindow("#paymentpopup", function () {
            $timeout(function () {
                $('#amountReceived').focus();
            }); });
        $scope.PosModel.Payment.Amount = $scope.PosModel.TotalPrice;
    };

    $scope.VerifyTransactionAndLoadPaymentWithPrint = function () {
        $scope.VerifyTransactionAndLoadPayment(true);
    };

    $scope.VerifyTransactionAndLoadPayment = function (printFlag) {
        $scope.message = null;
        $scope.submitted = true;

        if (!$scope.POSForm.$valid)
            return;

        if ($scope.PosModel.TransactionDetails.length <= 0 &&
            $scope.PosModel.POSSalesReturnTransactionDetails.length <= 0) {
            $().showMessage($scope, $timeout, true, 'Please add Product(s)');
            return;
        }

        $scope.PosModel.TransactionDate = $("#POSDatePicker", $(windowContainer)).val();
        $scope.IsPrint = printFlag;

        //Load payment screen
        $scope.LoadPaymentScreen();
        $timeout(function () {
            $('#amountReceived').focus();
        });
    };

    //Click on cancel redirecting to respective list view
    $scope.CancelPOS = function () {

        if ($scope.ShowWindow('SalesLists', 'Sales', 'SalesLists'))
            return;

        $(".preload-overlay", $(windowContainer)).fadeIn(100);

        $.ajax({
            url: "Sales/List",
            type: 'GET',
            success: function (result) {
                $("#LayoutContentSection", $(windowContainer)).append($compile(result)($scope));
                $scope.AddWindow('SalesLists', 'Sales', 'SalesLists');
                $(".preload-overlay", $(windowContainer)).fadeOut(100);
            }
        });
    };

    $scope.CreateCustomer = function () {

        if ($scope.ShowWindow('CreateCustomer', 'Create Customer', 'CreateCustomer'))
            return;

        $(".preload-overlay", $(windowContainer)).fadeIn(100);

        $.ajax({
            url: "Customer/Create",
            type: 'GET',
            success: function (result) {
                $("#LayoutContentSection", $(windowContainer)).append($compile(result)($scope)).updateValidation();
                $scope.AddWindow('CreateCustomer', 'Create Customer', 'CreateCustomer');
                $(".preload-overlay", $(windowContainer)).fadeOut(100);
            }
        });
    };

    $scope.DetailedCreateCustomer = function (event) {
        this.$parent.Create('Inventories/Customer', event, 'Create Customer', 'CreateCustomer');
    };

    $scope.LoadSalesGrid = function () {
        //$("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-spinner fa-pulse waypoint" style="font-size:40px;"></span></center>');
        if ($scope.ShowWindow('SalesLists', 'Sales', 'SalesLists'))
            return;

        $(".preload-overlay").fadeIn(100);

        $.ajax({
            url: "Sales/List",
            type: 'GET',
            success: function (result) {
                $("#LayoutContentSection").append($compile(result)($scope));
                $scope.AddWindow('SalesLists', 'Sales', 'SalesLists');
                $(".preload-overlay").fadeOut(100);
            }
        });
    };

    $scope.ProductDiscount = function () {

        $scope.PosModel.SubTotal = 0;

        $.each($scope.PosModel.TransactionDetails, function (index, item) {

            //var discountAmount = item.UnitPrice * item.DiscountPercentage / 100;
            if (item.DiscountPercentage == undefined) {
                return;
            }
            //item.DiscountPercentage = item.DiscountPercentage.trim();
            item.DiscountPercentage = item.DiscountPercentage.toString().trim();
            item.Amount = item.QuantityText * item.UnitPrice * (100 - item.DiscountPercentage) / 100;
            $scope.PosModel.SubTotal += item.Amount; //Calculating total price for all the grid items
        });

        $scope.PosModel.TotalPrice = $scope.PosModel.SubTotal + parseFloat($scope.PosModel.DeliveryCharge)
        $scope.totalWithoutDeliveryCharge = parseFloat($scope.PosModel.SubTotal);

        //Resetting the total discount values
        $scope.PosModel.TotalDiscount = 0;
        $scope.PosModel.TotalDiscountPercentage = 0;
        $scope.PosModel.TotalProductDiscount = 0;
    }

    $scope.TotalDiscountByAmount = function (event) {

        if ($scope.PosModel.TotalDiscount == undefined) {
            return;
        }

        if ($scope.PosModel.TransactionDetails.length > 0) {

            if ($scope.PosModel.TotalDiscount > $scope.PosModel.SubTotal)
                $scope.PosModel.TotalDiscount = 0;

            $scope.PosModel.TotalProductDiscount = $scope.PosModel.TotalDiscount * 1;
            $scope.PosModel.TotalPrice = $scope.PosModel.SubTotal - $scope.PosModel.TotalDiscount + parseFloat($scope.PosModel.DeliveryCharge);
            /* totalWithoutDeliveryCharge will use by getTotalByDeliveryCharge method */
            $scope.totalWithoutDeliveryCharge = parseFloat($scope.PosModel.SubTotal) - parseFloat($scope.PosModel.TotalDiscount);
        }

        else {
            //$scope.CaluculateTotalPrice();
        }
    }

    $scope.TotalDiscountByPercentage = function (event) {

        if ($scope.PosModel.TotalDiscountPercentage == undefined)
            return

        if ($scope.PosModel.TransactionDetails.length > 0) {
            $scope.PosModel.TotalDiscountPercentage = $scope.PosModel.TotalDiscountPercentage * 1; //change to numeric value multiplying into 1
            $scope.PosModel.TotalProductDiscount = $scope.PosModel.SubTotal * $scope.PosModel.TotalDiscountPercentage / 100;
            $scope.PosModel.TotalPrice = $scope.PosModel.SubTotal - $scope.PosModel.TotalProductDiscount + parseFloat($scope.PosModel.DeliveryCharge);
            /* totalWithoutDeliveryCharge will use by getTotalByDeliveryCharge method */
            $scope.totalWithoutDeliveryCharge = parseFloat($scope.PosModel.SubTotal) - parseFloat($scope.PosModel.TotalProductDiscount);
        }
        else {
            //$scope.CaluculateTotalPrice();
        }
    }


    /* add customer */
    $scope.Customer = {};
    $scope.Customer.Login = {};
    $scope.Customer.Contacts = [];
    var objContact = {}
    objContact.MobileNo1 = null;
    $scope.Customer.Contacts.push(objContact);

    $scope.AddCustomer = function () {
        $http({
            url: "Customer/Save",
            method: "POST",
            data: $scope.Customer
        }).then(function (data, status, headers, config) {
            data = data.data;

            if (data.IsError && data.IsError == true) {
                $().showMessage($scope, $timeout, true, data.UserMessage);
                return;
            }

            $scope.message = "Customer created successfully.";
            /* once customer is created it should be Customer list and value should be in select 2 customer control*/
            $scope.Customers.push(data);
            $scope.PosModel.CustomerID = data.CustomerIID;
            $scope.PosModel.Customer.CustomerIID = data.CustomerIID;
            $scope.PosModel.Customer.CustomerName = data.CustomerName;
            //customerSelect2Ctrl.selected.Name = customer.CustomerName;

            $().showMessage($scope, $timeout, false, $scope.message);
            $('.popup span.close', $(windowContainer)).trigger('click');
            emptyQuickCustomer();

        }, function (data, status, headers, config) {
            $().showMessage($scope, $timeout, true, "Error occured, please check with adminstrator");
        });
    };

    $scope.IsCustomerEmailPhoneExist = function () {
        $scope.submitted = true;
        var formValid = $scope.quickCustomer.$valid;
        if (!formValid) {
            return;
        }

        var uri = "Customer/IsCustomerExist";
        $http({
            url: uri,
            method: "GET",
            params: { email: $scope.Customer.Login.LoginEmailID, phone: $scope.Customer.Contacts[0].PhoneNo1 }
        }).then(function (data, status, headers, config) {
            data = data.data;

            if (data == "") {
                $scope.Customer.IsPassword = true;
                $scope.AddCustomer();
            }
            else {
                /* assign value to input field */
                $scope.Customer.FirstName = data.FirstName;
                $scope.Customer.LastName = data.LastName;
                $scope.Customer.Login.LoginEmailID = data.Login.LoginEmailID;
                $scope.Customer.Contacts[0].PhoneNo1 = data.Contacts[0].PhoneNo1;

                /* once customer is created it should be Customer list and value should be in select 2 customer control*/
                $scope.Customers.push(data);
                $scope.PosModel.CustomerID = data.CustomerIID;
                $scope.PosModel.Customer.CustomerIID = data.CustomerIID;
                $scope.PosModel.Customer.CustomerName = data.CustomerName;

                $scope.message = "User email OR phone already exist.";
                $().showMessage($scope, $timeout, true, $scope.message);
                /* bind customer drop down */
                // $scope.Customers.push(data);               
            }
            //$scope.ClosePaymentScreen();

        }, function (data, status, headers, config) {
            $().showMessage($scope, $timeout, true, "Error occured, please check with adminstrator");
        });
    };

    $scope.ShowProductCreation = function (evt) {
        ShowPopup('#productCreation');
        $.ajax({
            url: 'Frameworks/CRUD/Create?screen=QuickCatalog',
            type: 'GET',
            success: function (result) {
                $('#dynamicPopoverContainer').html($compile(result)($scope)).updateValidation();
                $('#dynamicPopoverContainer .windowcontainer').slideDown(100);
                $("#dynamicPopoverContainer .button-orange").each(function (index, element) {
                    if (!$(element).hasClass('savefeature')) {
                        $(element).hide();
                    }
                });
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText);
            }
        });
    };

    $scope.ShowCustomerCreation = function (evt) {
        ShowPopup('#customerCreation');
        $.ajax({
            url: 'Frameworks/CRUD/Create?screen=QuickCustomer',
            type: 'GET',
            success: function (result) {
                $('#dynamicPopoverContainer').html($compile(result)($scope)).updateValidation();
                $('#dynamicPopoverContainer .windowcontainer').slideDown(100);
                $("#dynamicPopoverContainer .button-orange").each(function (index, element) {
                    if (!$(element).hasClass('savefeature')) {
                        $(element).hide();
                    }
                });
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText);
            }
        });
    };

    function ShowPopup(currentTarget) {
        var popdetect = $(currentTarget).attr('data-popup-type');
        $('.preload-overlay', $(windowContainer)).show();
        var xpos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerWidth() / 2;
        var ypos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerHeight() / 2;
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).fadeIn(500);
        $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).addClass('show');

        $('.popup', $(windowContainer)).on('click', 'span.close', function () {
            $(this).closest('.popup').fadeOut(500);
            $(this).closest('.popup').removeClass('show');
            $('.preload-overlay', $(windowContainer)).fadeOut(500);
        });

        $('.popupbtn a', $(windowContainer)).on('click', function (e) {
            e.preventDefault();
        });

        $('.preload-overlay', $(windowContainer)).on('click', function () {
            $('.popup', $(windowContainer)).removeClass('show');
            $('.popup', $(windowContainer)).fadeOut(500);
            $('.preload-overlay', $(windowContainer)).fadeOut(500);
        });
    }

    /* empty all input vlaue of Quick Customer */
    function emptyQuickCustomer() {
        $scope.Customer.FirstName = null;
        $scope.Customer.Login.LoginEmailID = null;
        $scope.Customer.Contacts[0].MobileNo1 = null;
        $scope.Customer.CustomerCR = null;
        $scope.message = null;
        $scope.submitted = false;
    }

    $scope.Print = function (TransactionID, isPrintOnly) {
        // $("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-spinner fa-pulse waypoint" style="font-size:40px;"></span></center>');
        isPrintOnly = isPrintOnly || false;

        $http({ method: 'GET', url: 'Inventories/PointOfSale/PrintPOSInvoice/' + TransactionID })
            .then(function (result) {
                jsPrint(result.data);
                if (!isPrintOnly) {
                    //$scope.LoadSalesGrid();
                }
            });
    };

    $scope.GetBalanceAmount = function () {
        if ($scope.PosModel.Payment.AmountReceived > $scope.PosModel.Payment.Amount) {
            $scope.PosModel.Payment.BalanceAmount = $scope.PosModel.Payment.AmountReceived - $scope.PosModel.Payment.Amount;
        }
        else {
            $scope.PosModel.Payment.BalanceAmount = 0.00;
        }
    };

    $scope.ClearPay = function () {
        $scope.PosModel.Payment.TotalAmountReceived = 0;
        $scope.PosModel.TransactionPaymentMethods = [];
        $timeout(function () {
            $('#amountReceived').focus();
        });
    };

    //Submit on cash/card
    $scope.SubmitPay = function (ctrl) {
        $scope.message = null;
        /* get the payment method name using html attribute */
        $scope.paymentMethod = $("#" + ctrl, $(windowContainer)).next().attr("payment-method").replace('Customer', '');
        $scope.paymentMethodId = $("#" + ctrl, $(windowContainer)).next().attr("payment-methodId");

        var amt = parseFloat($scope.PosModel.Payment.AmountReceived);

        if (!$scope.PosModel.Payment.TotalAmountReceived) {
            $scope.PosModel.Payment.TotalAmountReceived = 0;
        }

        $scope.PosModel.Payment.TotalAmountReceived = $scope.PosModel.Payment.TotalAmountReceived + amt;

        var today = utility.formatDate(new Date());
        var amountBalance = $scope.PosModel.Payment.Amount - $scope.PosModel.Payment.AmountPaid;

        if (amt >= amountBalance) {
            amt = amountBalance;
        }

        if (amt != 0) {
            /* add in $scope.PosModel.TransactionPaymentMethods */
            var tempObj = {
                PaymentMethodID: $scope.paymentMethodId,
                PaymentMethodName: $scope.paymentMethod,
                Amount: amt,
                AmountDisplay: amt,
                PaymentDate: today
            };
            $scope.PosModel.TransactionPaymentMethods.push(tempObj);
        }

        /* get some of TransactionPaymentMethods */
        $timeout(function () {
            $('#amountReceived').focus();
        });
    };

    $scope.getExistingPaymentMethod = function (paymentBy) {
        for (var i = 0; i < $scope.PosModel.TransactionPaymentMethods.length; i++) {
            if ($scope.PosModel.TransactionPaymentMethods[i].PaymentMethodID == paymentBy)
                return $scope.PosModel.TransactionPaymentMethods[i];
        }
        return null;
    };

    $scope.calculatePaymentTotals = function () {
        /* calculate balance amount */
        var balance = 0;
        if ($scope.PosModel.PaymentMethod == 'Cash') {
            balance = $scope.PosModel.TotalPrice - ($scope.PosModel.Payment.AmountPaid + parseFloat($scope.PosModel.Payment.AmountReceived));
        }

        if (balance < 0) {
            balance = -(balance);
        } else balance = 0;

        var total = 0;
        for (var i = 0; i < $scope.PosModel.TransactionPaymentMethods.length; i++) {
            var paymentItem = $scope.PosModel.TransactionPaymentMethods[i];
            total += parseFloat(paymentItem.Amount);
        }

        $scope.PosModel.Payment.AmountPaid = total;
        if ($scope.PosModel.PaymentMethod == 'Cash') {
            $scope.PosModel.Payment.BalanceAmount = balance;
        }
        /*calculate amount which we  have to get from customer*/
        $scope.PosModel.Payment.RemainingAmount = $scope.PosModel.TotalPrice - $scope.PosModel.Payment.AmountPaid;
    };
  
    $scope.SubmitPayRight = function (ctrl) {
        $scope.cashReturn = 0;
        $scope.message = null;
        $scope.paymentMethod = $("#" + ctrl, $(windowContainer)).next().attr("payment-method");

        if ($scope.PosModel.Payment.AmountReceived > 0) {

            /* check if data exist for paymentMethod */
            var paymentObject = $scope.getExistingPaymentMethod($scope.paymentMethod);

            /*FIRST*/
            if (paymentObject != null) {
                /* delete from paymentObject */
                utility.removeItemFromList($scope.PosModel.TransactionPaymentMethods, paymentObject);

                if ($scope.PosModel.TransactionPaymentMethods == null) {
                    $scope.balanceToBeRecieved = $scope.totalAmount;
                }
                else {
                    $scope.balanceToBeRecieved = $scope.totalAmount - $scope.SumOfPayment();
                }
            }

            /*SECOND*/
            var amt = 0;
            //var utc = new Date().toJSON().slice(0, 10);
            //var currentDateTimetoUTC = new Date().toUTCString();
            var today = new Date();
            if ($scope.PosModel.Payment.AmountReceived > $scope.balanceToBeRecieved) {
                amt = $scope.balanceToBeRecieved;
            }
            else {
                amt = $scope.PosModel.Payment.AmountReceived;
            }

            if (amt > 0) {
                /* add in $scope.PosModel.TransactionPaymentMethods */
                var tempObj = {
                    PaymentMethodID: $scope.paymentMethod,
                    PaymentMethodName: $scope.paymentMethod,
                    Amount: utility.isInt(parseFloat(amt)) === true ? parseFloat(amt) : parseFloat(amt).toFixed(3),
                    PaymentDate: today.getTime(),
                };
                $scope.PosModel.TransactionPaymentMethods.push(tempObj);
            }

            /*THIRD*/
            $scope.balanceToBeRecieved = $scope.totalAmount - $scope.SumOfPayment();

            /*FOUR*/
            if ($scope.paymentMethod == 'Cash') {
                $scope.cashReturn = $scope.PosModel.Payment.AmountReceived - amt;
            }

        }
        else {
            /* check if data exist for paymentMethod */
            var paymentObject = $scope.getExistingPaymentMethod($scope.paymentMethod);

            /*FIRST*/
            if (paymentObject != null) {
                /* delete from paymentObject */
                utility.removeItemFromList($scope.PosModel.TransactionPaymentMethods, paymentObject);

                if ($scope.PosModel.TransactionPaymentMethods == null) {
                    $scope.balanceToBeRecieved = $scope.totalAmount;
                }
                else {
                    $scope.balanceToBeRecieved = $scope.totalAmount - $scope.SumOfPayment();
                }
            }
        }
        $scope.PosModel.Payment.RemainingAmount = $scope.balanceToBeRecieved;
        $scope.PosModel.Payment.Amount = $scope.totalAmount;
        $scope.PosModel.Payment.BalanceAmount = $scope.cashReturn;
        $scope.PosModel.Payment.AmountPaid = $scope.SumOfPayment();
        $scope.PosModel.Payment.AmountReceived = 0;
    }

    $scope.SumOfPayment = function () {
        var total = 0;
        for (var i = 0; i < $scope.PosModel.TransactionPaymentMethods.length; i++) {
            var paymentItem = $scope.PosModel.TransactionPaymentMethods[i];
            total += parseFloat(paymentItem.Amount);
        }
        return total;
    }

    $scope.RemoveItemFromPaymentGridRight = function (index) {
        /* reset the paymentMethod and Balance */
        $scope.PosModel.PaymentMethod = null;
        $scope.PosModel.Payment.BalanceAmount = 0;

        $scope.PosModel.TransactionPaymentMethods.splice(index, 1); //Removing item from the grid
        if ($scope.PosModel.TransactionPaymentMethods == null) {
            $scope.balanceToBeRecieved = $scope.totalAmount;
        }
        else {
            $scope.balanceToBeRecieved = $scope.totalAmount - $scope.SumOfPayment();
        }
        $scope.PosModel.Payment.RemainingAmount = $scope.balanceToBeRecieved;
        $scope.PosModel.Payment.AmountPaid = $scope.SumOfPayment();
    }

    $scope.RemoveItemFromPaymentGrid = function (index) {
        /*reset the paymentMethod and Balance */
        $scope.PosModel.TransactionPaymentMethods.splice(index, 1); //Removing item from the grid
        /* get some of TransactionPaymentMethods */
    }

    $scope.tagTransformV2 = function (newTag) {
        return { Key: '', Value: newTag };
    }

    $scope.CustomerFreeSelect = function (select, item) {

    },

    $scope.SaveTransactionAjaxCall = function () {
        $.ajax({
            url: "Inventories/SalesInvoice/SaveTransactions",
            type: 'POST',
            data: $scope.PosModel,
            success: function (result) {
                if (result.IsError == false) {
                    $scope.$apply(function () {
                        $scope.PosModel = result.Transaction;
                    });

                    if ($scope.IsPrint === true) { //Pay and print
                        $scope.Print(result.Transaction.HeadIID);
                    }

                    $().showMessage($scope, $timeout, false, "Saved sucessfully.");
                } else {
                    var message;
                    if (result.UserMessage) {
                        message = result.UserMessage;
                    }
                    else {
                        if (result.Message) {
                            message = result.Message;
                        }
                    }

                    $().showMessage($scope, $timeout, true, message);
                }

                $('.preload-overlay', $(windowContainer)).fadeOut(500);
                $scope.NextNewEntry();
            }
        });
    };

    $scope.NextNewEntry = function () {
        $scope.PosModel = angular.copy($scope.ModelStructure);
        $scope.IsPrint = false;
        $('.controls input', $($scope.CrudWindowContainer)).filter(":visible:first").focus();
    };

    $scope.CloseWindow = function () {
        $scope.CloseWindowTab($($scope.CrudWindowContainer).closest('.windowcontainer').attr('windowindex'));
    };

    $scope.UpdateStatus = function (transactionStatusID) {
        $scope.IsPrint = false;
        $scope.PosModel.DocumentStatusID = transactionStatusID;
        //$scope.PosModel.DocumentStatus = transactionStatusID;

        $scope.SaveTransactionAjaxCall();
    };

    $scope.ConfirmPayment = function () {
        if ($scope.PosModel.Payment.AmountPaid.toFixed(3) === $scope.PosModel.Payment.Amount.toFixed(3)) {
            $('.preload-overlay', $(windowContainer)).show();
            // Reset Transaction according to inventory controller
            $scope.PosModel.TransactionStatusID = null;
            $scope.PosModel.TransactionStatus = null;

            $scope.SaveTransactionAjaxCall();
            $scope.message = null;
            $scope.ClosePaymentScreen();
        }
        else {
            $scope.message = "Total & Paid Amount should be equal.";
        }
    };

    /* it will enable or disable Parked button from UI*/
    $scope.IsParkedDisabled = function () {
        var headID = $scope.PosModel.HeadIID;
        var statusID = $scope.PosModel.TransactionStatusID;
        /*should setisfy this condition */
        if ((headID === 0 && (statusID === 1 || statusID === null)) || statusID === 18) {
            return false;
        }
        else {
            return true;
        }
    };

    $scope.ClosePaymentScreen = function () {
        $('.popup', $(windowContainer)).removeClass('show').fadeOut(500);        
    }

    $scope.MakeObjectPOSSalesReturnTransactionDetail = function (pOSSalesReturn) {
        if ($scope.PosModel.HeadIID > 0) {
            $scope.PosModel.POSSalesReturnTransactionDetails.push(pOSSalesReturn);
        }
    };

    /* this method will calculate the Total amount using delivery charge */
    $scope.getTotalByDeliveryCharge = function () {
        /* handle delivery charge */
        if ($scope.PosModel.DeliveryCharge == null || $scope.PosModel.DeliveryCharge == "") {
            $scope.PosModel.DeliveryCharge = 0;
        }
        $scope.PosModel.DeliveryCharge = parseFloat($scope.PosModel.DeliveryCharge);
        $scope.PosModel.TotalPrice = parseFloat($scope.totalWithoutDeliveryCharge) + parseFloat($scope.PosModel.DeliveryCharge);
        $scope.PosModel.Payment.Amount = parseFloat($scope.totalWithoutDeliveryCharge) + parseFloat($scope.PosModel.DeliveryCharge);
    };

    $scope.EditCategory = function (event, category) {
        event.preventDefault();
        viewName = "Category";
        windowName = "Category";

        if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        var editUrl = 'Frameworks/CRUD/Create?screen=' + viewName + "&ID=" + category.CategoryID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
            });
    };

    $scope.EditProduct = function (event, product) {
        event.preventDefault();
        //viewName = "Product";
        //windowName = "Product";

        //if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName))
        //    return;

        //$scope.AddWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        //var productID = null;

        //var editUrl = 'Frameworks/CRUD/Create?screen=QuickCatalog&ID=' + product.ProductIID;

        ShowPopup(event.currentTarget);
        $.ajax({
            url: 'Frameworks/CRUD/Create?screen=QuickCatalog&ID=' + product.ProductIID,
            type: 'GET',
            success: function (result) {
                $('#dynamicPopoverContainer').html($compile(result)($scope)).updateValidation();
                $('#dynamicPopoverContainer .windowcontainer').slideDown(100);
                $("#dynamicPopoverContainer .button-orange").each(function (index, element) {
                    if (!$(element).hasClass('savefeature')) {
                        $(element).hide();
                    }
                });
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText);
            }
        });

        //$http({ method: 'Get', url: editUrl })
        //    .then(function (result) {
        //        $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
        //        $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        //    });
    };

    $scope.SetOrUnSetFavouriteProduct = function (event, product) {
        event.preventDefault();
        event.stopPropagation();
        //$scope.GlobalPopupWindow(event);
    };

    $scope.SetOrUnSetFavouriteCategory = function (event, category) {
        event.preventDefault();
        event.stopPropagation();
        //$scope.GlobalPopupWindow(event);
    };

    $scope.EditProductPrice = function (event, product) {
        event.preventDefault();
        event.stopPropagation();
        $scope.GlobalPopupWindow(event.currentTarget);
        $('#divProductPrice ', $(windowContainer)).html('');
        $http({ method: 'Get', url: 'Catalogs/Price/SKUPriceSettings?SKUID=' + product.ProductSKUMapIID })
            .then(function (result) {
                $('#divProductPrice ', $(windowContainer)).append($compile(result.data)($scope)).updateValidation();
            });
    };

    $scope.CloseWindow = function () {
        $scope.CloseWindowTab($(windowContainer).closest('.windowcontainer').attr('windowindex'));
    };

    $scope.GetProductPriceSettingForSKU = function (product) {
        $('.preload-overlay', $(windowContainer)).fadeIn(500);

        $.ajax({
            type: "Get",
            url: "ProductSKUPriceSetting/GetProductPriceListForSKU?skuIID=" + product.ProductSKUMapID,
            success: function (result) {
                $scope.$apply(function () {
                    $scope.ProductPrice = result;
                    $('.preload-overlay', $(windowContainer)).fadeOut(500);
                });
            }
        });
    };

    $scope.SetProductPrice = function ($event, price) {
        var gridItem = $.grep($scope.PosModel.TransactionDetails, function (result) { return result.ProductSKUMapID == price.ProductSKUID; })[0];
        gridItem.UnitPrice = price.Discount && price.Discount != null ? price.Discount : price.Price;
        var popupcontainer = $($event.currentTarget).closest('.popup');
        $(popupcontainer).closest('.popup').fadeOut(500);
        $(popupcontainer).closest('.popup').removeClass('show');
    };
}]);