app.controller("ProductPriceController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Product price View Loaded");



    //Initializing the product price
    $scope.Init = function (model, modelQuantity) {
        /*Declaration*/
        $scope.ProductPriceModel = [];
        $scope.ProductPriceModel.SKUPrice = [];
        $scope.ProductPriceModel.CategoryPrice = [];
        $scope.ProductPriceModel.BrandPrice = [];
        $scope.ProductPriceModel.BranchMaps = [];
        $scope.AddProductPriceSKUList = [];
        $scope.ProductPriceListTypes = [];
        $scope.ProductPriceListLevels = [];
        $scope.CustomerGroups = [];

        //$scope.Servicecall();
        $scope.ModelStructure = angular.copy(model);
        $scope.ModelQuantityStructure = angular.copy(modelQuantity)
        $scope.ProductPriceModel = model;

        $scope.GetProductPriceListTypes();
        $scope.GetProductPriceListLevels();

        $scope.ProductPrice;
        $scope.GetProductPrice();

        $scope.IsPriceInfoSection = true;
        $scope.IsCategoryPriceSection = false;
        $scope.IsBrandPriceSection = false;
        $scope.IsEditProductPriceSection = false;
        $scope.IsBranchMapSection = false;
        $scope.submitted = false;
        $scope.IsProductSelect2Binded = false;

        $scope.ProductRowIndex = -1;
        $scope.CategoryRowIndex = -1;
        $scope.BrandRowIndex = -1;
        $scope.BranchRowIndex = -1;

        if ($scope.ProductPriceModel.ProductPriceListIID > 0) {
            GetProductPriceSKU($scope.ProductPriceModel.ProductPriceListIID)
        }

        //$http({ method: 'Get', url: "Mutual/GetLookUpData?lookType=CustomerGroup" })
        //  .then(function (groups) {
        //      $scope.CustomerGroups = groups;
        //  });

        //$http({ method: 'Get', url: "Category/GetCategories" })
        //.then(function (categories) {
        //    $scope.Categories = categories;
        //});

        //$http({ method: 'Get', url: "Mutual/GetLookUpData?lookType=Brands" })
        //.then(function (brands) {
        //    $scope.Brands = brands;
        //});

        $http({ method: 'Get', url: "Mutual/GetLookUpData?lookType=AllBranch" })
        .then(function (branch) {
            $scope.BranchMaps = branch.data;
        });
    }

    $scope.ToggleGroup = function (event) {
        $(event.currentTarget).closest('.summary-toggle ').toggleClass('show');

        if ($(event.currentTarget).closest('.summary-toggle').hasClass('show')) {
            $(event.currentTarget).closest('.summary-toggle').addClass('show');
            $(event.currentTarget).closest('.summary-toggle').find('.sum_togglecontent').slideDown('fast');
        }
        else {
            $(event.currentTarget).closest('.summary-toggle').removeClass('show');
            $(event.currentTarget).closest('.summary-toggle').find('.sum_togglecontent').slideUp('fast');
        }

        event.stopPropagation();
    }

    $scope.ToggleInnerGroup = function (event) {
        $(event.currentTarget).closest('.innertoggle ').toggleClass('show');
        if ($(event.currentTarget).closest('.innertoggle').hasClass('show')) {
            $(event.currentTarget).closest('.innertoggle').find('.sum_innercontent').slideDown('fast');
        }
        else {
            $(event.currentTarget).closest('.innertoggle').find('.sum_innercontent').slideUp('fast');
        }

        event.stopPropagation();
    }

    $scope.ToggleQuantity = function (event, index, row, model) {
        $(event.currentTarget).closest('tr').next('tr.quantity-toggle').find('.quantity-fieldswrap').slideToggle('fast');

        if (model.length == 0) {
            $scope.InsertGridRow(index, row, model);
        }

        event.stopPropagation();
    }

    $scope.InsertGridRow = function (index, row, model) {
        model.splice(index + 1, 0, angular.copy(row));
    }

    $scope.RemoveGridRow = function (index, row, model) {
        model.splice(index, 1);
    }

    $scope.LoadPriceInfoSection = function (ctrl, IsPriceInfo) {
        $scope.IsPriceInfoSection = IsPriceInfo;
        $scope.IsEditProductPriceSection = !IsPriceInfo;
        $scope.IsCategoryPriceSection = !IsPriceInfo;
        $scope.IsBrandPriceSection = !IsPriceInfo;
        $scope.IsBranchMapSection = !IsPriceInfo;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.LoadproductPriceSection = function (ctrl, IsProductPrice) {

        if (!$scope.IsProductSelect2Binded) {
            $timeout(function () {
                $($('#spanProductName').find('input')[0]).keyup(function () {
                    if (event.keyCode != 38 && event.keyCode != 40) {
                        $scope.GetProductPrice();
                    }
                });
            }, 100);

            $scope.IsProductSelect2Binded = true;
        }

        $scope.IsEditProductPriceSection = IsProductPrice;
        $scope.IsPriceInfoSection = !IsProductPrice;
        $scope.IsCategoryPriceSection = !IsProductPrice;
        $scope.IsBrandPriceSection = !IsProductPrice;
        $scope.IsBranchMapSection = !IsProductPrice;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.LoadCategoryPriceSection = function (ctrl) {
        $scope.IsPriceInfoSection = false;
        $scope.IsCategoryPriceSection = true;
        $scope.IsBrandPriceSection = false;
        $scope.IsEditProductPriceSection = false;
        $scope.IsBranchMapSection = false;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.LoadBrandPriceSection = function (ctrl) {
        $scope.IsPriceInfoSection = false;
        $scope.IsCategoryPriceSection = false;
        $scope.IsBrandPriceSection = true;
        $scope.IsEditProductPriceSection = false;
        $scope.IsBranchMapSection = false;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.LoadBranchMapSection = function (ctrl) {
        $scope.IsPriceInfoSection = false;
        $scope.IsCategoryPriceSection = false;
        $scope.IsBrandPriceSection = false;
        $scope.IsEditProductPriceSection = false;
        $scope.IsBranchMapSection = true;
        $scope.SettingLeftPanelActiveSection(ctrl);
    }

    $scope.SettingLeftPanelActiveSection = function (ctrl) {
        $(ctrl.currentTarget).parent().find("li").removeClass("active");
        $(ctrl.currentTarget).addClass("active");
    }

    //Load all the products
    $scope.GetProductPrice = function () {
        var searchText = ($('#spanProductName').find('input')[0]) != undefined ? ($('#spanProductName').find('input')[0]).value : '';

        $http({ method: 'Get', url: "Catalogs/Price/GetProductPriceList?searchText=" + searchText })
        .then(function (productsList) {

            console.log("Product price loaded successfully");
            $scope.ProductPriceList = [];
            if (productsList.data != undefined && productsList.data != null) {

                //Looping product item list and adding to the scope array variable
                $.each(productsList.data, function (index, item) {
                    $scope.ProductPriceList.push(item);
                });
            }
        });
    }

    //Click on add, adding the item to POS view page
    $scope.SelectItemFromSelect2Ctrl = function (model, select2Ctrl, type) {

        switch (type) {
            case 'sku':
                var isExist = $.grep($scope.ProductPriceModel.SKUPrice, function (result) { return result.ProductSKUID == model.ProductSKUMapIID; }).length;

                if (isExist > 0) {
                    alert("Selected brand already exists");
                    return;
                }

                var copyProductPrice = angular.copy($scope.ModelStructure.SKUPrice[0])
                copyProductPrice.ProductPriceListID = $scope.ProductPriceModel.ProductPriceListIID;
                copyProductPrice.ProductSKUID = model.ProductSKUMapIID;
                copyProductPrice.ProductPriceSKU = model.SKU.replace(model.ProductName, '');
                copyProductPrice.PartNumber = model.PartNo;
                copyProductPrice.Barcode = model.BarCode;
                copyProductPrice.ProductName = model.ProductName;
                copyProductPrice.Amount = model.ProductPrice;
                copyProductPrice.SortOrder = model.SortOrder;
                copyProductPrice.IsDirty = true;

                $scope.ProductPriceModel.SKUPrice.push(copyProductPrice);
                break;
            case 'brand':
                var isExist = $.grep($scope.ProductPriceModel.BrandPrice, function (result) { return result.BrandID == model.Key; }).length;

                if (isExist > 0) {
                    alert("Selected brand already exists");
                    return;
                }

                var copyProductPrice = angular.copy($scope.ModelStructure.BrandPrice[0])
                copyProductPrice.ProductPriceListID = $scope.ProductPriceModel.ProductPriceListIID;
                copyProductPrice.BrandID = model.Key;
                copyProductPrice.BrandName = model.Value;
                copyProductPrice.IsDirty = true;

                $scope.ProductPriceModel.BrandPrice.push(copyProductPrice);
                break;
            case 'branch':
                var isExist = $.grep($scope.ProductPriceModel.BranchMaps, function (result) {
                    return result.BranchID == model.Key;
                }).length;

                if (isExist > 0) {
                    alert("Selected branch already exists");
                    return;
                }

                var copyProductPrice = angular.copy($scope.ModelStructure.BranchMaps[0])
                copyProductPrice.ProductPriceListBranchMapIID = 0;
                copyProductPrice.ProductPriceListID = $scope.ProductPriceModel.ProductPriceListIID;
                copyProductPrice.BranchID = model.Key;
                copyProductPrice.BranchName = model.Value;
                //copyProductPrice.IsDirty = true;

                $scope.ProductPriceModel.BranchMaps.push(copyProductPrice);
                break;
            case 'category':
                var isExist = $.grep($scope.ProductPriceModel.CategoryPrice, function (result) { return result.CategoryID == model.Key; }).length;

                if (isExist > 0) {
                    alert("Selected brand already exists");
                    return;
                }

                var copyProductPrice = angular.copy($scope.ModelStructure.CategoryPrice[0])
                copyProductPrice.ProductPriceListID = $scope.ProductPriceModel.ProductPriceListIID;
                copyProductPrice.CategoryID = model.Key;
                copyProductPrice.CategoryName = model.Value;
                copyProductPrice.IsDirty = true;

                $scope.ProductPriceModel.CategoryPrice.push(copyProductPrice);
                break;
        }



        //select2Ctrl.selected.Name = "";
        //$scope.GetProductPrice();
    }

    $scope.Submit = function () {

        $scope.ProductPriceModel.StartDate = $("#PriceStartDate").val(); //.split("/").reverse().join("/");
        $scope.ProductPriceModel.EndDate = $("#PriceEndDate").val(); //.split("/").reverse().join("/");

        $scope.CreateProductPriceInfo();
    }

    $scope.CreateProductPriceInfo = function () {
        $scope.submitted = true;
        $scope.IsDateTime = true;

        var formValid = $scope.CreatePriceInfo.$valid;

        if ($scope.ProductPriceModel.EndDate == "") {
            $scope.ProductPriceModel.EndDate = null;
        }
        if ($scope.ProductPriceModel.StartDate == "") {
            $scope.ProductPriceModel.StartDate = null;
        }
        var startDT = Date.parse($scope.ProductPriceModel.StartDate)
        var endDT = Date.parse($scope.ProductPriceModel.EndDate)

        if (startDT > endDT) {
            formValid = false;
            $scope.IsDateTime = false;
        }

        var priceInfoUrl = "Catalogs/Price/CreatePriceInformationDetail";

        if (formValid == true) {
            //$("#LayoutContentSection").html(' <center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');
            $("#Overlay").css("display", "block");
            $http({ method: 'POST', url: priceInfoUrl, data: { 'productPriceInfo': $scope.ProductPriceModel } })              
            .then(function (result) {
                $scope.ProductPriceModel = result.data.PriceModel;
                $().showMessage($scope, $timeout, false, "Sucessfully saved.");
                $("#Overlay").css("display", "none");
            });
        }
        else {
            $scope.IsEditProductPriceSection = false;
            $scope.IsPriceInfoSection = true;
            $scope.IsCategoryPriceSection = false;
            $scope.IsBrandPriceSection = false;
            $scope.IsBranchMapSection = false;
            $('#myTab').find("li").removeClass("active");
            $('#liPriceInfo').addClass("active");
        }

    }

    //    $scope.UpdateBranchMaps = function () {
    //        var branchList = $scope.ProductPriceModel;
    //}
    //Updating pricelist data with sku maps
    $scope.UpdateSKUProductPrice = function () {

        $scope.PostSKUList = [];

        $.each($scope.AddProductPriceSKUList, function (index, item) {

            //if (item.IsDirty == true) {
            item.ProductPriceListID = $scope.ProductPriceModel.ProductPriceListIID;
            $scope.PostSKUList.push(item);
            //}
        });

        /* check if PostSKUList is empty then return */
        if ($scope.PostSKUList.length == 0 || $scope.PostSKUList == null) {
            var ProductPrice = {
                ProductPriceListID: $scope.ProductPriceModel.ProductPriceListIID,
            };
            $scope.PostSKUList.push(ProductPrice);
        }

        $http({ method: 'resultPOST', url: "Catalogs/Price/UpdateSKUProductPrice", data: { 'productPriceSKUModelList': $scope.PostSKUList } })
        .then(function () {

            if (result.IsError == false) {

                if (result.data.MapList.length > 0) {
                    $.each(result.data.MapList, function (mapIndex, mapItem) {

                        $.each($scope.AddProductPriceSKUList, function (index, item) {

                            if (item.ProductSKUID == mapItem.ProductSKUID) {
                                item.ProductPriceListItemMapIID = mapItem.ProductPriceListItemMapIID; //Updating the saved map iid
                                item.IsDirty = false;
                            }
                        });
                    });
                }
                $().showMessage($scope, $timeout, result.IsError, result.Message);
            }
            $("#Overlay").css("display", "none");
        });
    }

    $scope.RowSelect = function (event, index, tab) {
        //close all the editable row and enable to edit selected row
        $(event.currentTarget).parents('table.tablegrid').find('tr.toggleparentrow').removeClass("editable");
        $(event.currentTarget).closest('tr.toggleparentrow').addClass("editable");

        switch (tab) {
            case 'product':
                if ($scope.ProductRowIndex == index) {
                    $scope.ProductRowIndex = -1;
                    $(event.currentTarget).closest('tr.toggleparentrow').removeClass("editable");
                } else {
                    $scope.ProductRowIndex = index;
                }
                break;
            case 'category':
                if ($scope.CategoryRowIndex == index) {
                    $scope.CategoryRowIndex = -1;
                    $(event.currentTarget).closest('tr.toggleparentrow').removeClass("editable");
                } else {
                    $scope.CategoryRowIndex = index;
                }
                break;
            case 'brand':
                if ($scope.BrandRowIndex == index) {
                    $scope.BrandRowIndex = -1;
                    $(event.currentTarget).closest('tr.toggleparentrow').removeClass("editable");
                } else {
                    $scope.BrandRowIndex = index;
                }
                break;
        }

    }

    //Changing the text data making the row flag dirty as true to send data to db
    $scope.MakeDirtyTextChange = function (productSKUID) {

        $.each($scope.AddProductPriceSKUList, function (index, item) {

            if (item.ProductSKUID == productSKUID)
                item.IsDirty = true;
        });
    }


    $scope.RemoveSKUItem = function (itemID, skuID) {

        if (skuID) {
            /* then remove from js object only */
            for (var i = 0; i < $scope.AddProductPriceSKUList.length; i++) {
                var productPriceListId = $scope.AddProductPriceSKUList[i].ProductPriceListID;
                if (skuID == $scope.AddProductPriceSKUList[i].ProductSKUID) {
                    $scope.AddProductPriceSKUList.splice(i, 1);
                }

            }
        } else {
            ///* remove from js and DB */
            //$http({ method: 'Get', url: "Price/RemoveProductPriceListSKUMaps?id=" + itemID })
            //.then(function (result) {
            //    if (result == "True") {
            //        for (var i = 0; i < $scope.AddProductPriceSKUList.length; i++) {
            //            if (itemID == $scope.AddProductPriceSKUList[i].ProductPriceListItemMapIID) {
            //                $scope.AddProductPriceSKUList.splice(i, 1);
            //            }
            //        }
            //    }

            //});
        }
    }

    $scope.LoadProductPriceList = function () {
        if ($scope.ShowWindow("PriceLists", "Price Lists", "PriceLists"))
            return;

        $("#Overlay").fadeIn(100);

        $http({ method: 'Get', url: "Catalogs/Price/" })
        .then(function (result) {
            $("#LayoutContentSection").append($compile(result.data)($scope));
            $scope.AddWindow("PriceLists", "Price Lists", "PriceLists");
            $("#Overlay").fadeOut(100);
        });
    }

    function GetProductPriceSKU(id) {
        $http({ method: 'Get', url: "Catalogs/Price/GetProductPriceSKU?id=" + id })
        .then(function (result) {
            if (result.data.ProductPriceSKUViewModel.length > 0) {
                for (var i = 0; i < result.data.ProductPriceSKUViewModel.length; i++) {
                    //result.ProductPriceSKUViewModel[i].ProductPriceSKU = result.ProductPriceSKUViewModel[i].ProductPriceSKU.replace(result.ProductPriceSKUViewModel[i].ProductName, '');
                    $scope.AddProductPriceSKUList.push(result.data.ProductPriceSKUViewModel[i]);
                }
            }
        });
    }

    function validateStartEndDate() {
        $scope.ProductPriceModel.StartDate = $("#PriceStartDate").val(); //.split("/").reverse().join("/");
        $scope.ProductPriceModel.EndDate = $("#PriceEndDate").val(); //.split("/").reverse().join("/");

        var startDT = Date.parse($scope.ProductPriceModel.StartDate)
        var endDT = Date.parse($scope.ProductPriceModel.EndDate)

        if (startDT > endDT) {
            formValid = false;
            $scope.IsDateTime = false;
        }
        else {
            $scope.IsDateTime = true;
        }
    }


    $scope.GetProductPriceListTypes = function () {
        var uri = "Catalogs/Price/GetProductPriceListTypes";
        $http({ method: 'Get', url: uri })
        .then(function (result) {
            $scope.ProductPriceListTypes = result.data.ProductPriceListTypeViewModel;
        });
    }

    $scope.GetProductPriceListLevels = function () {
        var uri = "Catalogs/Price/GetProductPriceListLevels";
        $http({ method: 'Get', url: uri })
        .then(function (result) {
            $scope.ProductPriceListLevels = result.data.ProductPriceListLevelViewModel;
        });
    }


    $scope.RemoveProductPriceBranchMap = function (index) {
        $scope.ProductPriceModel.BranchMaps.splice(index, 1);
    }


}]);

