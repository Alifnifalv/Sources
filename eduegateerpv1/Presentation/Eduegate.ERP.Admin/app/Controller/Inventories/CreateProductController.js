app.controller("CreateProductController", ["$scope", "$http", "$compile", "$window", "$timeout", function ($scope, $http, $compile, $window, $timeout) {
    console.log("CreateProductController is loaded");
    $scope.isQuickCreateTabSelected = false;
    $scope.isSKUTabSelected = false;
    $scope.isProductFeatureTabSelected = false;
    $scope.isProductTypeLoaded = false;
    $scope.isSearchCategoryTabSelected = false;
    $scope.isSKUPropertiesReadToLoad = false;
    $scope.isQuickCreateTabLoaded = false;
    $scope.isQuickCreateTabLoaded = false;
    $scope.isSKUTabLoaded = false;
    $scope.isProductFeatureTabLoaded = false;
    $scope.isProductTypeLoaded = false;
    $scope.isSearchCategoryTabLoaded = false;
    $scope.isUploadImageTabLoaded = false;
    $scope.isProductMapTabSelected = false;
    $scope.isProductMapTabLoaded = false;
    $scope.isProductConfigTabLoaded = false;
    $scope.isSeoMetadataSelected = false;
    $scope.isPriceSettingSelected = false;
    $scope.isSeoMetadataTabLoaded = false;
    $scope.isPriceSettingTabLoaded = false;
    $scope.isUploadVideoTabLoaded = false;
    $scope.isUploadVideoTabSelected = false;
    $scope.employees = [];
    $scope.isBundleTabSelected = false;
    $scope.isBundleTabLoaded = false;
    $scope.availableBrands = [];
    $scope.productFamilies = [];
    $scope.productStatus = [];
    $scope.propertyTypes = [];
    $scope.propertyTypeProperties = [];
    $scope.skuCartesianFormOfProperties = [];
    $scope.availableCategories = [];
    $scope.skuKeyValuePairForEdit = [];
    $scope.uploadedImagesInfo = [];
    $scope.SKUMapImagesToSelect = [];
    $scope.selectedSKUMapImages = [];
    $scope.isDefaultSKUGenerated = false;
    $scope.submitted = false;
    $scope.IsProductImageRequired = false;
    $scope.IsSKUImageRequired = false;
    $scope.AvailableTags = [];
    $scope.uploadVideosInfo = [];
    $scope.SKUMapVideosToSelect = [];
    $scope.selectedSKUMapVideos = [];
    $scope.NonVariantProperties = [];
    $scope.productTypes = [];
    $scope.SKUTags = [];
    $scope.TaxTemplates = [];
    var windowContainer = null;

    $scope.Init = function (model, window) {
        windowContainer = '#' + window;
        $scope.ProductModel = model;
        console.log($scope.ProductModel);
        $scope.ProductMap = model.ProductMaps;
        $scope.BundleMaps = model.BundleMaps;

        if ($scope.ProductModel.QuickCreate.ProductFamilyIID == 15) {
            $scope.ShowBundle = true;
        }
        if ($scope.ProductModel.QuickCreate.CultureInfo == null || $scope.ProductModel.QuickCreate.CultureInfo.length < 0) {
            $scope.GetCultureList();
        }
        $scope.GetBrandList();
        $scope.GetEmployeeList();
        $scope.GetProductFamilies();
        $scope.GetProductStatus();
        $scope.GetProductTypes();
        $scope.GetTaxTemplates();
        $scope.LoadQuickCreate();
        $scope.GetCategoryList();
        $scope.GetProductTagList();
        $scope.GetSKUTagLists();
        $scope.DeleteUploadedImages(null);
        $scope.DeleteUploadedVideos(null);
        $scope.defaultEmptySKUMap;        

        if ($scope.ProductModel.QuickCreate.ProductIID <= 0) {
            $scope.isDefaultSKUGenerated = true;
            $scope.GenerateDefaultSKU();
            console.log($scope.ProductModel);
        }

        if ($scope.ProductModel.QuickCreate.ProductIID > 0) {
            $scope.GetPropertiesByProductFamilyID($scope.ProductModel.QuickCreate.ProductFamilyIID, true);
        }
        if ($scope.ProductModel.SKUMappings.length == 1 && ($scope.ProductModel.SKUMappings[0].SKU == null || $scope.ProductModel.SKUMappings[0].SKU == '')) {
            $scope.ProductModel.SKUMappings[0].SKU = $scope.ProductModel.QuickCreate.ProductName;
            return;
        }

        if ($scope.ShowBundle) {
            return;
        }

        if ($scope.ProductModel.QuickCreate.ProductIID > 0) {
            if ($scope.ProductModel.SKUMappings != null && $scope.ProductModel.SKUMappings) {
                $.each($scope.ProductModel.QuickCreate.Properties, function (propertyIndex, propertyItem) {
                    $.each(propertyItem.SelectedProperties, function (selctedindex, selectedItem) {
                        $scope.skuKeyValuePairForEdit.push({ key: selectedItem.PropertyIID, value: selectedItem.PropertyName });
                    });
                });
                $.each($scope.ProductModel.SKUMappings, function (index, item) {

                    var skuPropertyName = "";
                    var skuarr = item.SKU.split('»');

                    for (var i = 0; i <= skuarr.length; i++) {

                        if (skuarr[i] != "" && $scope.skuKeyValuePairForEdit.length > 0) {
                            var skuValue = $.grep($scope.skuKeyValuePairForEdit, function (item) { return item.key == skuarr[i] });

                            if (skuValue.length > 0) {

                                var val = $.grep($scope.skuKeyValuePairForEdit, function (item) { return item.key == skuarr[i] })[0].value;

                                if (val == "") { break; }

                                if (skuPropertyName == "") {
                                    skuPropertyName = val;
                                }
                                else {
                                    skuPropertyName += "»" + val;
                                }
                            }
                        }
                    }

                    item.SKU = $scope.ProductModel.QuickCreate.ProductName + "»" + skuPropertyName;
                })
            }
        }
    }

    $scope.MultilanguagePopup = function (event, action) {
        event.preventDefault();
        var popup = $(event.target.parentNode).find('.languageoption');
        //$('.languageoption').fadeOut();
        
        if (action == 'show')
            $(popup).fadeIn(300, function () {
                $(popup).find('input,textarea').filter(':visible:first').focus();
            });
        //else
        //    $(popup).fadeOut();
    }

    $scope.MultilanguagePopupTest = function (event, action) {
        event.preventDefault();
        var popup = $(event.target.parentNode).find('.languageoption');
        //$('.languageoption').fadeOut();
        if (action == 'show')
            $(popup).fadeIn(300, function () {
                $(popup).find('input,textarea').filter(':visible:first').focus();
            });
        //else
        //    $(popup).fadeOut();
    }

    $scope.AttachDateRangePicker = function (ctrl, callback) {
        $('input[name="daterange"]').daterangepicker(
          {
              locale: {
                  format: 'DD/MMM/YYYY',
              },
          },
         function (start, end, label) {
             if (callback != undefined) {
                 callback();
             }
         })
    };

    $scope.setBrandModel = function (brand) {
        if (brand != null && brand != undefined)
            $scope.ProductModel.QuickCreate.BrandIID = brand.Key;
    }

    $scope.LoadQuickCreate = function () {

        if ($scope.isQuickCreateTabLoaded) {
            return;
        }

        //$('.overlaydiv').show();

        $.ajax({
            url: "Catalogs/Product/_QuickCreate",
            type: "GET",
            success: function (result) {
                $("#QuickCreate", $(windowContainer)).html($compile(result)($scope));
                if ($scope.ProductModel.QuickCreate.Properties != null && $scope.ProductModel.QuickCreate.Properties.length > 0 || $scope.ProductModel.QuickCreate.DefaultProperties != null && $scope.ProductModel.QuickCreate.DefaultProperties.length > 0) {
                    $scope.LoadSKUProperties(false, null)
                }
                $scope.isQuickCreateTabSelected = true;
                $scope.isQuickCreateTabLoaded = true;
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.LoadSKUMapping = function () {

        if ($scope.isDefaultSKUGenerated) {
            $scope.ProductModel.SKUMappings[0].SKU = $scope.ProductModel.QuickCreate.ProductName;
        }

        if ($scope.isSKUTabSelected || $scope.isSKUTabLoaded) {
            return;
        }
        //$('.overlaydiv').show();

        $.ajax({
            url: "Catalogs/Product/_CreateSKUMapping",
            type: "GET",
            success: function (result) {
                $("#SKUMapping", $(windowContainer)).html($compile(result)($scope));
                $scope.isSKUTabLoaded = true;
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.LoadProductFeature = function () {

        if ($scope.isProductFeatureTabSelected) {
            return;
        }

        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/_CreateProductFeature",
            type: "GET",
            success: function (result) {
                $("#ProductFeature", $(windowContainer)).html($compile(result)($scope));
                $scope.isProductFeatureTabLoaded = true;
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.LoadProductType = function () {

        if ($scope.isProductTypeLoaded) {
            return;
        }

        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/_CreateProductType",
            type: "GET",
            success: function (result) {
                $("#ProductType", $(windowContainer)).html($compile(result)($scope));
                $scope.isProductTypeLoaded = true;
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.LoadSearchCategory = function () {

        if ($scope.isSearchCategoryTabSelected || $scope.isSearchCategoryTabLoaded) {
            return;
        }

        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/_SearchCategory",
            type: "GET",
            success: function (result) {
                $("#SearchCategory", $(windowContainer)).html($compile(result)($scope));
                $scope.isSearchCategoryTabLoaded = true;
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.tagTransform = function (newTag) {
        return { TagIID: 0, TagName: newTag.toUpperCase() };
    }

    $scope.UniqueTag = function (selectedItem) {

        if (selectedItem.TagIID > 0) { //Existing Tag
            var existModel = $.grep($scope.ProductModel.SelectedTags, function (item) { return item.TagName.toLowerCase().trim() == selectedItem.TagName.toLowerCase().trim() });

            if (existModel.length > 1) {
                alert("Tag Name already exists...");
                $scope.ProductModel.SelectedTags.pop(selectedItem);
            }

            return;
        }
        else { // New Tag
            var existsDB = $.grep($scope.AvailableTags, function (item) { return item.TagName.toLowerCase().trim() == selectedItem.TagName.toLowerCase().trim() });

            if (existsDB.length > 0) {
                alert("Tag Name already exists...");
                $scope.ProductModel.SelectedTags.pop(selectedItem);
                return;
            }
        }
    }

    $scope.LoadUploadProductImages = function () {
        if ($scope.isUploadImageTabSelected == true || $scope.isUploadImageTabLoaded == true) {
            return
        }

        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/_UploadProductImages",
            type: "GET",
            success: function (result) {
                $("#UploadProductImage", $(windowContainer)).html($compile(result)($scope));
                $scope.isUploadImageTabLoaded = true;
                /*Need to implement dropzone plugin later*/
                $scope.InitializeDropZonePlugin();
                $('.overlaydiv').hide();
            }
        });
    };

    $scope.LoadProductView = function () {
        //$('.overlaydiv').show();
        var productViewUrl = "Catalogs/Product/ProductView";
        $http({ method: 'Get', url: productViewUrl })
        .then(function (result) {
            console.log("product view loaded successfully");
            //$location.path('/ProductView');
            $("#LayoutContentSection").append($compile(result.data)($scope));
            $scope.AddWindow('ProductLists', 'Product Lists', 'productLists');
            $('.overlaydiv').hide();
        });
    }

    $scope.Create = function (view) {

        var windowName = view.substring(view.indexOf('/') + 1);
        if ($scope.ShowWindow('Create' + windowName, 'Create ' + view, 'Create' + windowName))
            return;

        $("#Overlay").fadeIn(100);
        //var createUrl = view + "/Create";
        var createUrl = "Brand/Create";
        $.ajax({
            url: createUrl,
            type: 'GET',
            success: function (result) {
                $("#LayoutContentSection").append($compile(result)($scope)).updateValidation();
                $scope.AddWindow('Create' + windowName, 'Create ' + view, 'Create' + windowName);
                $("#Overlay").fadeOut(100);
            }
        })
    }

    /* load empty Product Relationship view */
    $scope.LoadProductMaps = function () {
        if ($scope.isProductMapTabSelected || $scope.isProductMapTabLoaded) {
            return;
        }
        $scope.ProductMap.FromProduct.ProductName = $scope.ProductModel.QuickCreate.ProductName;
        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/ProductToProductMaps",
            type: "GET",
            success: function (result) {
                $("#ProductMaps", $(windowContainer)).html($compile(result)($scope));
                $scope.isProductMapTabLoaded = true;
                //$timeout(function() {
                //    $($('#relation_1').find('input')[0]).keyup(function () { $scope.GetProductListBySearch(); });
                //},100);
                $scope.$apply();
                //$scope.GetProductToProductMapDetail();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.LoadSeoMetadata = function () {
        if ($scope.isSeoMetadataSelected || $scope.isSeoMetadataTabLoaded) {
            return;
        }
        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/SeoMetadata",
            type: "GET",
            success: function (result) {
                $("#SeoMetadata", $(windowContainer)).html($compile(result)($scope));
                $scope.isSeoMetadataTabLoaded = true;
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.LoadPriceSettings = function () {
        if ($scope.isPriceSettingSelected || $scope.isPriceSettingTabLoaded) {
            return;
        }

        var skuAsString = '';

        $.each($scope.ProductModel.SKUMappings, function (index, item) {
            skuAsString = skuAsString + item.ProductSKUMapID + ',';
        });

        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/PriceSettings?skuIds=" + skuAsString,
            type: "GET",
            success: function (result) {
                $('.overlaydiv').hide();
                $("#pricesettings", $(windowContainer)).html($compile(result)($scope));
                $scope.isPriceSettingTabLoaded = true;
                $scope.$apply();               
            }
        });
    }

    $scope.LoadBundleMaps = function () {

        if ($scope.isBundleTabSelected == true || $scope.isBundleTabLoaded == true) {
            return
        }

        $.ajax({
            url: "Catalogs/Product/ProductBundleMaps",
            type: "GET",
            success: function (result) {
                $("#BundleMaps", $(windowContainer)).html($compile(result)($scope));
                $scope.isBundleTabLoaded = true;
                $scope.BundleProductList = [];
                $scope.BundleProductSKUList = [];
                $scope.GetNonBundleProductList();
                $scope.GetNonBundleProductSKUList();
                $timeout(function () {
                    $($('#ProductBundleMaps').find('input')[0]).keyup(function () { $scope.GetNonBundleProductList() });
                    $($('#SKUBundleMaps').find('input')[0]).keyup(function () { $scope.GetNonBundleProductSKUList() });
                }, 100);
                $scope.$apply();
            }
        });
    }

    $scope.GetProductToProductMapDetail = function () {
        $("#Load", "#Product").show();
        //$('.overlaydiv').show();

        $.ajax({
            url: "Product/GetProductToProductMap?productID=" + $scope.ProductModel.QuickCreate.ProductIID,
            type: "GET",
            success: function (result) {
                /* manage if product is not created */
                if (result.ProductMap.FromProduct.ProductID <= 0) {
                    result.ProductMap.FromProduct.ProductName = $scope.ProductModel.QuickCreate.ProductName;
                }
                $scope.ProductMap = result.ProductMap;
                $scope.$apply();

                $("#Load", "#Product").hide();
                $('.overlaydiv').hide();
            },
            error: function (error) {
                $("#Load", "#Product").hide();
                $('.overlaydiv').hide();
            }
        });


    }

    /* refresh the Product list on search from DB */
    $scope.RefreshProductList = function (searchText) {
        /* reset the due to dublicacy */
        $scope.ProductMap.ProductList = [];
        $.ajax({
            url: "Product/GetProductListBySearch?searchText=" + searchText,
            type: "GET",
            success: function (result) {
                if (!result.IsError) {
                    for (var i = 0; i < result.ProductList.length; i++) {
                        /* same product should not add */
                        if ($scope.ProductModel.QuickCreate.ProductIID != result.ProductList[i].ProductID) {
                            $scope.ProductMap.ProductList.push(result.ProductList[i]);
                        }
                        $scope.$apply();
                    }
                }
            }
        });
    }



    $scope.GetProductListBySearch = function () {
        var searchText = ($('#relation_1').find('input')[0]) != undefined ? ($('#relation_1').find('input')[0]).value : '';
        $.ajax({
            url: "Product/GetProductListBySearchText?searchText=" + searchText,
            type: "GET",
            success: function (result) {
                if (!result.IsError) {
                    if (!$scope.isProductMapTabLoaded) {
                        if ($scope.ProductModel.QuickCreate.ProductIID <= 0) {
                            /* For Create */
                            $scope.ProductMap = {
                                FromProduct: {
                                    ProductID: $scope.ProductModel.QuickCreate.ProductIID,
                                    ProductName: $scope.ProductModel.QuickCreate.ProductName
                                },
                                IsFromProductDisabled: true, ToProduct: [], ProductList: result.Products,
                                SalesRelationshipTypes: result.Relations
                            };
                        }
                        else {
                            /* For Update */
                            $scope.ProductMap.ProductList = result.Products;
                            $scope.ProductMap.SalesRelationshipTypes = result.Relations;
                        }
                    }
                    else {
                        $scope.ProductMap.FromProduct = {
                            ProductID: $scope.ProductModel.QuickCreate.ProductIID,
                            ProductName: $scope.ProductModel.QuickCreate.ProductName
                        }
                        $scope.ProductMap.ProductList = result.Products;
                        $scope.ProductMap.SalesRelationshipTypes = result.Relations;
                        $scope.$apply();
                    }
                }
            }
        })
    }

    $scope.GetCultureList = function () {

        var cultureListURL = "Product/GetCultureList";
        $http({ method: 'Get', url: cultureListURL })
        .then(function (result) {
            $scope.ProductModel.QuickCreate.CultureInfo = [];
            $scope.ProductModel.QuickCreate.CultureInfo = result.data;
        });
    }

    $scope.GetEmployeeList = function (searchText) {

        if (searchText == null || searchText == undefined)
            searchText = "";

        var url = "Payroll/Employee/SearchEmployee?searchText=" + searchText;
        $http({ method: 'Get', url: url })
        .then(function (result) {
            $scope.employees = result.data;
        });
    }

    $scope.GetBrandList = function (searchText) {
        if (searchText == null || searchText == undefined)
            searchText = "";

        var brandListURL = "Catalogs/Product/GetBrandList?searchText=" + searchText;
        $http({ method: 'Get', url: brandListURL })
        .then(function (result) {
            $scope.availableBrands = result.data;
        });

    }

    $scope.GetProductFamilies = function (searchText) {
        if (searchText == null || searchText == undefined)
            searchText = "";

        var productFamiliesURL = "Catalogs/Product/GetProductFamilies?searchText=" + searchText;
        $http({ method: 'Get', url: productFamiliesURL })
        .then(function (result) {
            $scope.productFamilies = result.data;
        });
    }

    $scope.GetProductStatus = function () {

        var productStausURL = "Catalogs/Product/GetProductStatus";
        $http({ method: 'Get', url: productStausURL })
        .then(function (result) {
            $scope.productStatus = result.data;
        });
    }

    $scope.GetProductTypes = function () {

        var productStausURL = "Catalogs/Product/GetProductTypes";
        $http({ method: 'Get', url: productStausURL })
        .then(function (result) {
            $scope.productTypes = result.data;
        });
    }

    $scope.GetTaxTemplates = function () {
        var url = "Mutual/GetLookUpData?lookType=TaxTemplates";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.TaxTemplates = result.data;
            });
    }

    $scope.UpdateDefaultLangValueInPopup = function () {
        $.each($scope.ProductModel.QuickCreate.CultureInfo, function (index, item) {
            if (item.CultureName == $scope.ProductModel.DefaultLanguage) {
                item.CultureValue = $scope.ProductModel.QuickCreate.ProductName;
            }
        });
    }

    $scope.UpdateDefaultLangValToProductName = function (currentElementID) {
        if (currentElementID == $scope.ProductModel.DefaultLanguage) {
            $scope.ProductModel.QuickCreate.ProductName = $("#" + currentElementID).val();
        }
    }

    $scope.LoadProductBundleSKUs = function () {

        $scope.ProductModel.SKUMappings = [];
        var SKUName = "";
        if ($scope.BundleMaps.ProductMaps != null && $scope.BundleMaps.ProductMaps.length > 0) {

            $.each($scope.BundleMaps.ProductMaps, function (index, item) {
                if (SKUName != "") {
                    SKUName = SKUName + "»" + item.ProductName;
                }
                else {
                    SKUName = item.ProductName;
                }
            });
        }
        var bundleSKULine = { SKU: SKUName, SkuName: SKUName, ProductSKUCode: 1, Sequence: 1, ProductID: $scope.ProductModel.QuickCreate.ProductIID, ProductPrice: null, PartNo: null, BarCode: null, ProductSKUMapID: -1, ImageMaps: [] };
        $scope.ProductModel.SKUMappings.push(bundleSKULine);
    }

    $scope.UpdateProductBundleSKU = function () {

        $scope.isDefaultSKUGenerated = false;
        if ($scope.BundleMaps.ProductMaps != null && $scope.BundleMaps.ProductMaps.length > 0) {
            $scope.LoadProductBundleSKUs();
        }

        var SKUName = "";
        if ($scope.BundleMaps.SKUMaps != null && $scope.BundleMaps.SKUMaps.length > 0) {

            $scope.skuCartesianFormOfProperties = [];
            var skuArraysToGenerateLines = [];

            //getting unique productIds
            var productIDs = [];
            $.each($scope.BundleMaps.SKUMaps, function (index, item) { productIDs.push(item.ProductID) });
            productIDs = $.unique(productIDs);

            //forming array for each product with the respective mapped skus
            $.each(productIDs, function (index, productID) {
                var SKUsFromOneProduct = []
                $.each($scope.BundleMaps.SKUMaps, function (index, item) {
                    if (item.ProductID == productID) {
                        SKUsFromOneProduct.push(item.ProductSKUName);
                    }
                });
                if (SKUsFromOneProduct.length > 0) {
                    skuArraysToGenerateLines.push(SKUsFromOneProduct);
                }
            });

            //Forming cartesian for sku lines
            if (skuArraysToGenerateLines != null && skuArraysToGenerateLines.length > 0) {
                $scope.skuCartesianFormOfProperties = [];
                var CartesianItems = CrossProduct(skuArraysToGenerateLines);
                while (CartesianItems.next()) CartesianItems.do($scope.InsertSKUCombination);
            }

            //Generating Sku lines
            $scope.ProductModel.SKUMappings = [];
            $.each($scope.skuCartesianFormOfProperties, function (index, item) {
                var skuline = { SKU: item, SkuName: item, ProductSKUCode: 1, Sequence: index + 1, ProductID: $scope.ProductModel.QuickCreate.ProductIID, ProductPrice: null, PartNo: null, BarCode: null, ProductSKUMapID: -(index + 1), ImageMaps: [] }
                $scope.ProductModel.SKUMappings.push(skuline);
            });
        }

        if (($scope.BundleMaps.ProductMaps == null || $scope.BundleMaps.ProductMaps.length <= 0) && ($scope.BundleMaps.SKUMaps == null || $scope.BundleMaps.SKUMaps.length <= 0)) {
            $scope.GenerateDefaultSKU();
        }
    }

    $scope.LoadSKUProperties = function (clearExistingProperties, family) {
        if (family != null && family != undefined)
            $scope.ProductModel.QuickCreate.ProductFamilyIID = family.Key;

        if ($scope.ProductModel.QuickCreate.ProductFamilyIID == 15) {
            $scope.ShowBundle = true;
            $scope.LoadProductBundleSKUs();
            return;
        }
        else {
            $scope.ShowBundle = false;
        }

        if (clearExistingProperties) {
            $scope.ProductModel.QuickCreate.Properties = [];
            $scope.GetPropductPropertyTypes();
            $scope.GetPropertiesByProductFamilyID();
            //$scope.ProductModel.SKUMappings = [];
            $scope.GenerateDefaultSKU();

        }
        else {
            $.each($scope.ProductModel.QuickCreate.Properties, function (propertyIndex, propertyItem) {
                $scope.LoadAvailablePropertiesAndMapPropertyName(propertyItem);
            });
        }

        //$('.overlaydiv').show();

        $.ajax({
            url: "Catalogs/Product/_SKUProperties?productFamilyIID=" + $scope.ProductModel.QuickCreate.ProductFamilyIID,
            type: "GET",
            success: function (result) {
                $("#PropertyType", $(windowContainer)).html($compile(result)($scope));
                $scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.GetPropductPropertyTypes = function () {
        $.ajax({
            url: "Catalogs/Product/GetProductPropertyTypes?productFamilyIID=" + $scope.ProductModel.QuickCreate.ProductFamilyIID,
            type: "GET",
            success: function (result) {
                $.each(result, function (index, item) {
                    $scope.ProductModel.QuickCreate.Properties.push({ PropertyType: item, AvailableProperties: [], SelectedValue: null });
                });
                $scope.$apply();
                $scope.GetPropertiesByPropertyTypeID(result);
            }
        });
    }

    $scope.GetPropertiesByPropertyTypeID = function (propertyTypes, callback) {
        if (propertyTypes == null || propertyTypes.length <= 0) {
            return;
        }
        var lastPropertyTypeID = propertyTypes[propertyTypes.length - 1].PropertyTypeID
        $.each(propertyTypes, function (index, item) {
            $.ajax({
                url: "Catalogs/Product/GetPropertiesByPropertyTypeID?propertyTypeIID=" + item.PropertyTypeID,
                type: "GET",
                success: function (result) {
                    var currentItem = $.grep($scope.ProductModel.QuickCreate.Properties, function (prop) { return prop.PropertyType.PropertyTypeID == item.PropertyTypeID });
                    $scope.$apply(function () {
                        currentItem[0].AvailableProperties = result;
                    });
                }
            });
        });
    }

    $scope.GetPropertiesByProductFamilyID = function (isOnlyForVariants) {
        $.ajax({
            url: "Catalogs/Product/GetProperitesByProductFamilyID?productFamilyID=" + $scope.ProductModel.QuickCreate.ProductFamilyIID,
            type: "GET",
            success: function (result) {
                if (!isOnlyForVariants) {
                    $scope.ProductModel.QuickCreate.DefaultProperties = result;
                    $scope.ProductModel.SKUMappings[0].Properties = angular.copy(result);
                }
                $scope.NonVariantProperties = result;
            }
        });
    }

    $scope.LoadAvailablePropertiesAndMapPropertyName = function (propertyItem) {
        $.ajax({
            url: "Catalogs/Product/GetPropertiesByPropertyTypeID?propertyTypeIID=" + propertyItem.PropertyType.PropertyTypeID,
            type: "GET",
            success: function (result) {
                propertyItem.AvailableProperties = result;
                $.each(propertyItem.SelectedProperties, function (selectedIndex, selectedItem) {
                    selectedItem.PropertyName = $.grep(propertyItem.AvailableProperties, function (item) { return item.PropertyIID == selectedItem.PropertyIID })[0].PropertyName;
                });
            }
        });
    }

    function CrossProduct(sets) {

        var n = sets.length, carets = [], args = [];

        function init() {
            for (var i = 0; i < n; i++) {
                carets[i] = 0;
                args[i] = sets[i][0];
            }
        }

        function next() {
            if (!args.length) {
                init();
                return true;
            }
            var i = n - 1;
            carets[i]++;
            if (carets[i] < sets[i].length) {
                args[i] = sets[i][carets[i]];
                return true;
            }
            while (carets[i] >= sets[i].length) {
                if (i == 0) {
                    return false;
                }
                carets[i] = 0;
                args[i] = sets[i][0];
                carets[--i]++;
            }
            args[i] = sets[i][carets[i]];
            return true;
        }

        return {
            next: next,
            do: function (callback) {
                return callback(args);
            }
        }
    }

    $scope.OnPropertyChange = function () {
        $scope.existingSKUMaps = $scope.ProductModel.SKUMappings;
        $scope.GenerateSKULine();

        if ($scope.existingSKUMaps != null && $scope.existingSKUMaps.length > 0) {

            $.each($scope.ProductModel.SKUMappings, function (index, item) {
                var isSKUExist = [];
                $.each($scope.existingSKUMaps, function (skuIndex, SKU) {
                    if (SKU.SKU == item.SKU) {
                        isSKUExist = SKU;
                        return;
                    }
                });
                if (isSKUExist != null && isSKUExist.SKU != undefined) {
                    item.BarCode = isSKUExist.BarCode;
                    item.ImageMaps = isSKUExist.ImageMaps;
                    item.PartNumber = isSKUExist.PartNumber;
                    item.ProductID = isSKUExist.ProductID;
                    item.ProductPrice = isSKUExist.ProductPrice;
                    item.Properties = isSKUExist.Properties;
                    item.ProductInventoryConfigViewModels = isSKUExist.ProductInventoryConfigViewModels
                    item.ProductSKUMapID = isSKUExist.ProductSKUMapID > 0 ? isSKUExist.ProductSKUMapID : item.ProductSKUMapID;
                    item.StatusID = isSKUExist.StatusID;

                }
            })
        }
    }

    $scope.InsertSKUCombination = function (args) {

        var SKUCombination = "";
        for (var i = 0; i < args.length; i++) {

            if (i == 0) {
                SKUCombination = args[i];
            }
            else {
                SKUCombination += "»" + args[i];
            }
        }
        $scope.skuCartesianFormOfProperties.push(SKUCombination);

    }

    $scope.GetCategoryList = function (searchText) {
        if (searchText == null || searchText == undefined)
            searchText = "";

        var categoryListURL = "Catalogs/Product/GetCategoryList?searchText=" + searchText;
        $http({
            method: 'Get', url: categoryListURL
        })
        .then(function (result) {
            $scope.availableCategories = result.data;
        });
    }

    $scope.GenerateSKULine = function () {

       // $scope.ProductModel.SKUMappings = [];
        $scope.isDefaultSKUGenerated = false;
        $scope.skuCartesianFormOfProperties = [];
        var skuArraysToGenerateLines = [];

        //Fetching the selected properties as an array to form cartesian of all the selected properties
        $.each($scope.ProductModel.QuickCreate.Properties, function (propertyIndex, propertyItem) {
            var skuSelectedProperties = []
            if (propertyItem.SelectedProperties != null && propertyItem.SelectedProperties.length > 0) {
                $.each(propertyItem.SelectedProperties, function (selectedIndex, selectedItem) {
                    skuSelectedProperties.push(selectedItem.PropertyName);
                });
                skuArraysToGenerateLines.push(skuSelectedProperties);
            }
        });

        //Forming cartesian of of all the selected properties 
        if (skuArraysToGenerateLines != null && skuArraysToGenerateLines.length > 0) {
            $scope.skuCartesianFormOfProperties = [];
            var CartesianItems = CrossProduct(skuArraysToGenerateLines);
            while (CartesianItems.next()) CartesianItems.do($scope.InsertSKUCombination);
        }

        //Generating Sku lines
        multipleSKusAssing();

        if ($scope.ProductModel.SKUMappings.length <= 0) {
            $scope.isDefaultSKUGenerated = true;
            $scope.GenerateDefaultSKU();
        }
    }

    function multipleSKusAssing()
    {
        $scope.skuNameList = [];
        $.each($scope.skuCartesianFormOfProperties, function (index, item) {
            $scope.ProductModel.SKUMappings[0].SkuName.Text = $scope.ProductModel.QuickCreate.ProductName + "»" + item;
            $scope.ProductModel.SKUMappings[0].SkuName.CultureDatas[0].CultureValue = $scope.ProductModel.QuickCreate.ProductName + "»" + item;
            $scope.ProductModel.SKUMappings[0].ProductInventoryConfigViewModels = $scope.ProductModel.SKUMappings[0].ProductInventoryConfigViewModels;
            $scope.skuNameList.push(angular.copy($scope.ProductModel.SKUMappings[0]));
        });
        $scope.ProductModel.SKUMappings = [];
        $.each($scope.skuCartesianFormOfProperties, function (index, item) {
            //var config = angular.copy($scope.ProductModel.InventoryConfig);
            var prop = angular.copy($scope.NonVariantProperties);
            
            //$scope.ProductModel.SKUMappings.splice($scope.ProductModel.SKUMappings[index], 1);
           // console.log(skuName);
            var skuline = { SKU: $scope.ProductModel.QuickCreate.ProductName + "»" +item, SkuName: $scope.skuNameList[index].SkuName, ProductSKUCode: 1, Sequence: index +1, ProductID: $scope.ProductModel.QuickCreate.ProductIID, ProductPrice: null, PartNo: null, BarCode: null, ProductSKUMapID: - (index +1), ImageMaps: [], ProductInventoryConfigViewModels: $scope.skuNameList[index].ProductInventoryConfigViewModels, Properties: prop }
            $scope.ProductModel.SKUMappings.push(skuline);
        });
    }

    $scope.InitializeDropZonePlugin = function () {
        Dropzone.autoDiscover = false;
        $("#mydropzone").dropzone({
            previewsContainer: "#previews",
            addRemoveLinks: true,
            uploadMultiple: true,
            multiplesuccess: function (file, response) {
                console.log("uploaded successfully" + response);
            }
        });
    };

    $scope.TriggerUploadFile = function ($event) {        
        $timeout(function () {
            angular.element('#UploadFile').trigger('click');
            //$("#UploadFile").trigger('click');
        });
    }

    $scope.TriggerVideoUploadFile = function () {
        //$timeout(function () {
        angular.element('#UploadVideoFile').trigger('click');
        //}, 1);

    }


    $scope.UploadImageFiles = function (uploadfiles) {

        var existingFilameIndex = 0;
        if ($scope.ProductModel.QuickCreate.ProductIID > 0 && $scope.ProductModel.UploadedFiles != null && $scope.ProductModel.UploadedFiles.length > 0) {
            var filenames = [];
            var combinedImageList = [];

            if ($scope.uploadedImagesInfo != undefined && $scope.uploadedImagesInfo.length > 0) {
                combinedImageList = $scope.ProductModel.UploadedFiles.concat($scope.uploadedImagesInfo);
            } else {
                combinedImageList = $scope.ProductModel.UploadedFiles;
            }

            $.grep(combinedImageList, function (e) { filenames.push(parseInt(e.FileName.split('.')[0])) });
            existingFilameIndex = Math.max.apply(Math, filenames) != undefined ? Math.max.apply(Math, filenames) : 0
        }
        var url = 'Catalogs/Product/UploadProductImages?existingFileNameIndex=' + existingFilameIndex;
        var xhr = new XMLHttpRequest();
        var fd = new FormData();
        for (i = 0; i < uploadfiles.files.length; i++) {
            fd.append(uploadfiles.files[i].name, uploadfiles.files[i]);
        }

        xhr.open("POST", url, true);
        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response);
                if (result.Success == true && result.FileInfo.length > 0) {
                    $.each(result.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + "?" + item.FileName.split('.')[0] + Math.random();
                        $scope.uploadedImagesInfo.push(item);
                    });
                    $scope.IsProductImageRequired = false;
                    $scope.$apply();
                }
            }
        }
        xhr.send(fd);
    }

    $scope.DeleteUploadedImages = function (imageInfo) {
        var fileName = imageInfo != null && imageInfo != undefined ? imageInfo.FileName : ""
        $.ajax({
            url: "Catalogs/Product/DeleteUploadProductImages?fileName=" + fileName + '&productID=' + $scope.ProductModel.QuickCreate.ProductIID,
            type: "POST",
            success: function (result) {
                if (result.Success) {
                    if (fileName != null) {
                        $scope.$apply(function () {
                            $scope.uploadedImagesInfo = $.grep($scope.uploadedImagesInfo, function (e) {
                                return e.FileName != imageInfo.FileName;
                            });

                            $scope.ProductModel.UploadedFiles = $.grep($scope.ProductModel.UploadedFiles, function (e) {
                                return e.FileName != imageInfo.FileName;
                            });
                        });

                        if ($scope.uploadedImagesInfo.length <= 0 && $scope.ProductModel.UploadedFiles <=0) {
                            $scope.IsProductImageRequired = true;
                        }

                        
                    }
                }
            }
        })
    }

    $scope.OpenImageMapPopup = function (currentSKURow, event) {

        var popdetect = $(event.currentTarget).attr('data-popup-type');
        //$('.overlaydiv').show();
        var xpos = $(".popup[data-popup-type='" + popdetect + "']").outerWidth() / 2;
        var ypos = $(".popup[data-popup-type='" + popdetect + "']").outerHeight() / 2;
        $(".popup[data-popup-type='" + popdetect + "']").css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
        $(".popup[data-popup-type='" + popdetect + "']").fadeIn(500);
        $(".popup[data-popup-type='" + popdetect + "']").addClass('show');
        var listheight = $('.search-content').height();
        if (listheight > 231) {
            $('.searchlist.tableheader').addClass('fixedposition');
        }
        $(".set-sku-image").attr("data-skumapID", currentSKURow.ProductSKUMapID);

        if (currentSKURow.ImageMaps != null && currentSKURow.ImageMaps.length > 0) {
            $.each(currentSKURow.ImageMaps, function (index, item) { $scope.selectedSKUMapImages.push(item) });
        }

        if ($scope.ProductModel.UploadedFiles != null && $scope.ProductModel.UploadedFiles.length > 0) {
            $.each($scope.ProductModel.UploadedFiles, function (index, item) { $scope.SKUMapImagesToSelect.push(item) });
        }
        if ($scope.uploadedImagesInfo != null && $scope.uploadedImagesInfo.length > 0) {
            $.each($scope.uploadedImagesInfo, function (index, item) { $scope.SKUMapImagesToSelect.push(item) });
        }
    }

    $scope.MapImagesToskuLine = function (event) {

        var skuID = $(event.currentTarget).attr('data-skumapID');
        var sequence = 1;
        $.each($scope.ProductModel.SKUMappings, function (skuindex, skuItem) {
            if (skuItem.ProductSKUMapID == skuID) {
                if (skuItem.ImageMaps == null) {
                    skuItem.ImageMaps = [];
                }
                else {
                    existingSequenceList = []
                    if (skuItem.ImageMaps.length > 0) {
                        $.each(skuItem.ImageMaps, function (index, item) { existingSequenceList.push(parseInt(item.Sequence)); });
                        sequence = existingSequenceList != null && existingSequenceList != undefined ? Math.max.apply(Math, existingSequenceList) + 1 : sequence;
                    }
                }

                $.each($scope.SKUMapImagesToSelect, function (index, item) {
                    if (item.isSelected) {

                        var isImageAlreadyMapped = $.grep(skuItem.ImageMaps, function (e) { return e.ImageName == item.FileName; });

                        if (isImageAlreadyMapped.length <= 0) {

                            $scope.selectedSKUMapImages.push({ ImageName: item.FileName, ImagePath: item.FilePath, Sequence: sequence });
                            skuItem.ImageMaps.push({ ImageName: item.FileName, ImagePath: item.FilePath, Sequence: sequence });
                            sequence++
                        }
                        item.isSelected = false;
                    }
                })
                return;
            }
        });
    }

    $scope.RemoveImageMapsToSKuLines = function (imageInfo) {
        var skuID = $("#add", "#ImageMapPopup").attr('data-skumapID');
        $scope.selectedSKUMapImages = $.grep($scope.selectedSKUMapImages, function (e) { return e.ImageName != imageInfo.ImageName; });
        $.each($scope.ProductModel.SKUMappings, function (skuindex, skuItem) {
            if (skuItem.ProductSKUMapID == skuID) {
                skuItem.ImageMaps = $.grep(skuItem.ImageMaps, function (e) { return e.ImageName != imageInfo.ImageName; });
                return;
            }
        });
    }

    $scope.CloseImageMapPopup = function (event) {
        $(event.currentTarget).closest('.popup').fadeOut(500);
        $(event.currentTarget).closest('.popup').removeClass('show');
        $('.overlaydiv').hide();
        $scope.selectedSKUMapImages = [];
        $scope.SKUMapImagesToSelect = [];
    }


    $scope.ToggleImageSelected = function (currentItem) {
        currentItem.isSelected = currentItem.isSelected ? false : true;
    }

    $scope.GenerateDefaultSKU = function () {

        if ($scope.ProductModel.SKUMappings.length == 0) {
            $scope.ProductModel.SKUMappings.push($scope.defaultEmptySKUMap);
        }
        $scope.isDefaultSKUGenerated = true;
        //var config = angular.copy($scope.ProductModel.InventoryConfig);
        var config = angular.copy($scope.ProductModel.QuickCreate.ProductInventoryConfigViewModels);
        var skuProductInventoryConfig = angular.copy($scope.ProductModel.SKUMappings[0].ProductInventoryConfigViewModels);
        $scope.ProductModel.SKUMappings[0].SkuName.Text = $scope.ProductModel.QuickCreate.ProductName;
        $scope.ProductModel.SKUMappings[0].SkuName.CultureDatas[0].CultureValue = $scope.ProductModel.QuickCreate.ProductName;
        //$scope.ProductModel.SKUMappings[0].SkuName.ProductInventoryConfigViewModels = skuProductInventoryConfig;
        var skuName = angular.copy($scope.ProductModel.SKUMappings[0].SkuName);
        $scope.ProductModel.SKUMappings = [];
        var prop = angular.copy($scope.NonVariantProperties);
        var defaultSKULine = { SKU : $scope.ProductModel.QuickCreate.ProductName, SkuName: skuName, ProductSKUCode: 1, Sequence: 1, ProductID: $scope.ProductModel.QuickCreate.ProductIID, ProductPrice: null, PartNo: null, BarCode: null, ProductSKUMapID: -1, ImageMaps: [], isDefaultSKU: $scope.isDefaultSKUGenerated, ProductInventoryConfigViewModels: skuProductInventoryConfig, Properties: prop };
        $scope.defaultEmptySKUMap = defaultSKULine;
        $scope.ProductModel.SKUMappings.push(defaultSKULine);
    }

    $scope.GetNonBundleProductList = function () {

        var searchText = $($('#ProductBundleMaps').find('input')[0]).val();
        searchText = searchText != undefined ? searchText : "";

        $.ajax({
            url: "Catalogs/Product/GetProductListBySearch?searchText=" + searchText + "&excludeProductFamilyID=" + parseInt($scope.ProductModel.QuickCreate.ProductFamilyIID),
            type: "GET",
            success: function (result) {
                if (!result.IsError) {

                    $scope.BundleProductList = [];
                    if (result.ProductList != null && result.ProductList.length > 0) {
                        if ($scope.BundleMaps.ProductMaps == null) { $scope.BundleMaps.ProductMaps = []; }

                        $.each($scope.BundleMaps.ProductMaps, function (index, item) {
                            result.ProductList = $.grep(result.ProductList, function (e) { return e.ProductID != item.ProductID });
                        });


                        $.each(result.ProductList, function (index, item) {
                            var bundleViewModel = { BundleID: -1, ProductID: item.ProductID, ProductName: item.ProductName };
                            $scope.BundleProductList.push(bundleViewModel);
                        });
                    }

                    $scope.$apply();
                }
            }
        });
    }

    $scope.GetNonBundleProductSKUList = function () {

        var searchText = $($('#SKUBundleMaps').find('input')[0]).val();
        searchText = searchText != undefined ? searchText : "";

        $.ajax({
            url: "Inventory/GetProducts?searchText=" + searchText,
            type: "GET",
            success: function (result) {
                $scope.BundleProductSKUList = [];
                if (!result.IsError) {

                    if (result != null && result.length > 0) {

                        if ($scope.BundleMaps.SKUMaps == null) { $scope.BundleMaps.SKUMaps = [] }

                        //removing already selected items from dropdown list 
                        $.each($scope.BundleMaps.SKUMaps, function (index, item) {
                            result = $.grep(result, function (e) { return e.ProductSKUMapIID != item.ProductSKUMapID });
                        });

                        $.each(result, function (index, item) {
                            var bundleViewModel = { BundleID: -1, ProductID: item.ProductIID, ProductName: item.ProductName, ProductSKUMapID: item.ProductSKUMapIID, ProductSKUName: item.ProductSKU };
                            $scope.BundleProductSKUList.push(bundleViewModel);
                        });
                    }
                }
            }
        });
    }

    $scope.SaveProduct = function () {

        $scope.submitted = true;

        //  validating Quick create Tab.
        if (!$scope.QuickCreateForm.$valid) {
            $scope.isQuickCreateTabSelected = true;
            $scope.isUploadImageTabSelected = false;
            $scope.isSKUTabSelected = false;
            $scope.isSearchCategoryTabSelected = false;
            $scope.isProductMapTabSelected = false;
            $scope.isBundleTabSelected = false;
            $scope.isSeoMetadataSelected = false;
            $scope.isPriceSettingSelected = false;
            return;
        }

        // here we are checking product IsOnline or not if it is then we have to check all the tab validation else not
        // 5 (Draft) status is only using to create MP
        if ($scope.ProductModel.QuickCreate.IsOnline === true && ($scope.ProductModel.QuickCreate.StatusIID == "2" || $scope.ProductModel.QuickCreate.StatusIID == "5")) {
            //validating BundleProducts
            if ($scope.ProductModel.QuickCreate.ProductFamilyIID == 15) {

                if (($scope.BundleMaps.ProductMaps == undefined || $scope.BundleMaps.ProductMaps.length <= 0) && ($scope.BundleMaps.SKUMaps == undefined || $scope.BundleMaps.SKUMaps.length <= 0)) {
                    $scope.LoadBundleMaps();
                    $scope.isBundleTabSelected = true;
                    $scope.isQuickCreateTabSelected = false;
                    $scope.isUploadImageTabSelected = false;
                    $scope.isSKUTabSelected = false;
                    $scope.isSearchCategoryTabSelected = false;
                    $scope.isProductMapTabSelected = false;
                    $scope.isSeoMetadataSelected = false;
                    $scope.isPriceSettingSelected = false;
                    return;
                }
            }

            //validating uploaded images
            if ($scope.ProductModel.QuickCreate.ProductIID <= 0) {

                if ($scope.uploadedImagesInfo.length <= 0) {
                    $scope.LoadUploadProductImages();
                    $scope.IsProductImageRequired = true;
                    $scope.isQuickCreateTabSelected = false;
                    $scope.isUploadImageTabSelected = true;
                    $scope.isSKUTabSelected = false;
                    $scope.isSearchCategoryTabSelected = false;
                    $scope.isProductMapTabSelected = false;
                    $scope.isBundleTabSelected = false;
                    $scope.isSeoMetadataSelected = false;
                    $scope.isPriceSettingSelected = false;
                    return;
                }
            }

            //validating sku lines
            if (($scope.SKUMappingForm == undefined && $scope.ProductModel.QuickCreate.ProductIID <= 0) || ($scope.SKUMappingForm != undefined && !$scope.SKUMappingForm.$valid)) {
                $scope.LoadSKUMapping();
                $scope.isQuickCreateTabSelected = false;
                $scope.isUploadImageTabSelected = false;
                $scope.isSKUTabSelected = true;
                $scope.isSearchCategoryTabSelected = false;
                $scope.isProductMapTabSelected = false;
                $scope.isProductConfigTabSelected = false;
                $scope.isBundleTabSelected = false;
                $scope.isSeoMetadataSelected = false;
                $scope.isPriceSettingSelected = false;
                return;
            }


            //validating sku images
            $.each($scope.ProductModel.SKUMappings, function (index, item) {
                if (item.ImageMaps.length <= 0) {
                    $scope.IsSKUImageRequired = true;
                    return;
                }
            })

            if ($scope.IsSKUImageRequired) {
                $scope.LoadSKUMapping();
                $scope.isQuickCreateTabSelected = false;
                $scope.isUploadImageTabSelected = false;
                $scope.isSKUTabSelected = true;
                $scope.isSearchCategoryTabSelected = false;
                $scope.isProductMapTabSelected = false;
                $scope.IsSKUImageRequired = false;
                $scope.isProductConfigTabSelected = false;
                $scope.isBundleTabSelected = false;
                $scope.isSeoMetadataSelected = false;
                $scope.isPriceSettingSelected = false;
                return;
            }

            if ($scope.ProductModel.SelectedCategory.length <= 0) {
                $scope.LoadSearchCategory();
                $scope.isQuickCreateTabSelected = false;
                $scope.isUploadImageTabSelected = false;
                $scope.isSKUTabSelected = false;
                $scope.isSearchCategoryTabSelected = true;
                $scope.isProductMapTabSelected = false;
                $scope.isProductConfigTabSelected = false;
                $scope.isBundleTabSelected = false;
                $scope.isSeoMetadataSelected = false;
                $scope.isPriceSettingSelected = false;
                return;
            }

            if ($scope.isDefaultSKUGenerated) {
                $scope.ProductModel.SKUMappings[0].SKU = $scope.ProductModel.QuickCreate.ProductName;
            }
        }

        $("#Load", "#Product").show();
        //$('.overlaydiv').show();

        var newTags = $.grep($scope.ProductModel.SelectedTags, function (t) { return t.TagIID == 0 });

        if (newTags.length > 0) { //Creating new properties which is the tag user is added dynamically

            var productTags = [];

            $.each(newTags, function (index, item) {

                var tag = { TagIID: item.TagIID, TagName: item.TagName };
                productTags.push(tag);
            });

            $.ajax({
                url: "Catalogs/Product/CreateProperties",
                type: "POST",
                data: JSON.stringify(productTags),
                contentType: "application/json",
                success: function (result) {
                    if (result.IsError == false) {

                        if (result.ProductTags.length > 0) {
                            $.each($scope.ProductModel.SelectedTags, function (index, item) {

                                var match = $.grep(result.ProductTags, function (t) { return t.TagName == item.TagName })[0];

                                if (match != null && match != undefined)
                                    item.TagIID = match.TagIID;

                            });

                            $scope.GetProductTagList(); //Getting all the tags from db
                            $scope.SaveProductAjaxCall();
                        }
                        else {
                            $("#Load", "#Product").hide();
                            $('.overlaydiv').hide();
                        }
                    }
                    else {
                        $("#Load", "#Product").hide();
                        $('.overlaydiv').hide();
                    }
                },
                complete: function (result) {
                    $("#Load", "#Product").hide();
                    $('.overlaydiv').hide();
                }
            });
        }
        else {
            $scope.SaveProductAjaxCall();
        }
    }

    $scope.SaveProductAjaxCall = function () {
        $.ajax({
            url: "Catalogs/Product/UpdateProduct",
            type: "POST",
            dataType: "json",
            data: $scope.ProductModel,
            success: function (result) {
                var msg = "Sucessfully saved.";

                if (result > 0) {
                    if ($scope.ProductMap != undefined) {
                        $scope.ProductModel.QuickCreate.ProductIID = result;
                        $scope.ProductMap.FromProduct.ProductID = result;
                        $scope.SaveProductMaps(msg);

                    }
                    else {
                        $().showMessage($scope, $timeout, false, msg);
                    }
                }
                else {
                    $().showMessage($scope, $timeout, true, result);
                }

                $("#Load", "#Product").hide();
                $('.overlaydiv').hide();
            },
            error : function () {
                $().showMessage($scope, $timeout, true, "Error occured, please close and try again or check with your administator.");
            },
            complete: function (result) {
                $("#Load", "#Product").hide();
                $('.overlaydiv').hide();
            }
        });
    }


    $scope.SaveProductMaps = function (msg) {
        $("#Load", "#Product").show();
        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/SaveProductMaps",
            type: "POST",
            dataType: "json",
            data: $scope.ProductMap,
            success: function (result) {
                if (result) {
                    $().showMessage($scope, $timeout, false, msg);
                }
                $("#Load", "#Product").hide();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.ShowAlert = function (msg) {
        var r = confirm(msg);
        if (r == true) {
            $scope.LoadProductView();
        }
    }

    $scope.GetProductTagList = function () {

        $.ajax({
            url: "Catalogs/Product/GetProductTags",
            type: 'GET',
            success: function (result) {

                $scope.AvailableTags = [];

                if (result.IsError == false) {
                    if (result.ProductTags != null && result.ProductTags.length > 0) {
                        $.each(result.ProductTags, function (index, item) {
                            $scope.AvailableTags.push(item);
                        });
                    }
                }
            }
        });
    }

    $scope.GetSKUTagLists = function () {

        $.ajax({
            url: "Catalogs/Product/GetSKUTags",
            type: 'GET',
            success: function (result) {

                $scope.SKUTags = [];

                if (result.IsError == false) {
                    if (result.SKUTags != null && result.SKUTags.length > 0) {
                        $.each(result.SKUTags, function (index, item) {
                            $scope.SKUTags.push(item);
                        });
                    }
                }
            }
        });
    }

    /* load empty Product Config view */
    $scope.LoadProductConfig = function (isRoot) {
        if ($scope.isProductConfigTabSelected || $scope.isProductConfigTabLoaded) {
            return;
        }

        var parameter;

        if (isRoot != undefined && isRoot == true) {
            parameter = 'true';
        }
        else {
            parameter = 'false';
        }

        //$('.overlaydiv').show();
        $.ajax({
            url: "Catalogs/Product/ProductConfig?isRoot=" + parameter,
            type: "GET",
            success: function (result) {
                $("#ProductConfigSKU", $(windowContainer)).html($compile(result)($scope));
                $scope.isProductConfigTabLoaded = true;
                //$timeout(function() {
                //    $($('#relation_1').find('input')[0]).keyup(function () { $scope.GetProductListBySearch(); });
                //},100);
                $scope.$apply();
                //$scope.GetProductToProductMapDetail();
                $('.overlaydiv').hide();
            }
        });
    }


    /* product sku config */

    /* by default it would be false */
    $scope.SKUIndex = -1;

    $scope.ShowCustomSKUField = function (index) {
        //$scope.myClass = { show: false, showfields: false };

        //$scope[index] ? $scope[index] = false : $scope[index] = true;
        //  ng-style="$index == SKUIndex ? { display:'table-row' } : { display:'none' }"
        // ng-show="$index == SKUIndex"
        // ng-class="{'showfields show' : $index == SKUIndex}"
        if ($scope.SKUIndex == index) {
            $scope.SKUIndex = -1;
        }
        else {
            $scope.SKUIndex = index; 
        }
        //$scope.SKUIndex = index;
        //$scope.myClass.show = !$scope.myClass.show;
        //$scope.myClass.showfields = !$scope.myClass.showfields;
    }

    //********   Video Upload code starts here *******//

    $scope.LoadUploadProductVideos = function () {

        $scope.isUploadVideoTabSelected = true;
        $scope.isQuickCreateTabSelected = false;
        $scope.isUploadImageTabSelected = false;
        $scope.isSKUTabSelected = false;
        $scope.isSearchCategoryTabSelected = false;
        $scope.isProductMapTabSelected = false;
        $scope.isProductConfigTabSelected = false;
        $scope.isSeoMetadataSelected = false;
        $scope.isPriceSettingSelected = false;
        $scope.isBundleTabSelected = false;

        if ($scope.isUploadVideoTabLoaded == true)
            return;

        //$('.overlaydiv').show();

        $.ajax({
            url: "Catalogs/Product/_UploadProductVideos",
            type: "GET",
            success: function (result) {
                $("#UploadProductVideo", $(windowContainer)).html($compile(result)($scope));
                $scope.isUploadVideoTabLoaded = true;
                //$scope.$apply();
                $('.overlaydiv').hide();
            }
        });
    }

    $scope.UploadVideoFiles = function (uploadVideoFiles) {

        var existingFilameIndex = 0;

        if ($scope.ProductModel.QuickCreate.ProductIID > 0 && $scope.ProductModel.UploadedVideoFiles != null && $scope.ProductModel.UploadedVideoFiles.length > 0) {
            var filenames = [];
            $.grep($scope.ProductModel.UploadedFiles, function (e) { filenames.push(parseInt(e.FileName.split('.')[0])) });
            existingFilameIndex = Math.max.apply(Math, filenames) != undefined ? Math.max.apply(Math, filenames) : 0
        }

        var url = 'Catalogs/Product/UploadProductVideos?existingFileNameIndex=' + existingFilameIndex;
        var xhr = new XMLHttpRequest();
        var fd = new FormData();

        for (i = 0; i < uploadVideoFiles.files.length; i++) {
            fd.append(uploadVideoFiles.files[i].name, uploadVideoFiles.files[i]);
        }

        xhr.open("POST", url, true);

        xhr.onreadystatechange = function (url) {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var result = JSON.parse(xhr.response);
                if (result.Success == true && result.FileInfo.length > 0) {
                    $.each(result.FileInfo, function (index, item) {
                        item.FilePath = item.FilePath + "?" + item.FileName.split('.')[0] + Math.random();
                        $scope.uploadVideosInfo.push(item);
                    });
                    $scope.$apply();
                }
            }
        }

        xhr.send(fd);
    }

    $scope.DeleteUploadedVideos = function (imageInfo) {

        var fileName = imageInfo != null && imageInfo != undefined ? imageInfo.FileName : "";

        $.ajax({
            url: "Catalogs/Product/DeleteUploadProductVideos?fileName=" + fileName,
            type: "POST",
            success: function (result) {
                if (result.Success) {
                    if (fileName != null) {
                        $scope.uploadVideosInfo = $.grep($scope.uploadVideosInfo, function (e) {
                            return e.FileName != imageInfo.FileName;
                        });

                        $scope.$apply();
                    }
                }
            }
        })
    }

    $scope.OpenVideoMapPopup = function (currentSKURow, event) {

        var popdetect = $(event.currentTarget).attr('data-popup-type');
        //$('.overlaydiv').show();
        var xpos = $(".popup[data-popup-type='" + popdetect + "']").outerWidth() / 2;
        var ypos = $(".popup[data-popup-type='" + popdetect + "']").outerHeight() / 2;
        $(".popup[data-popup-type='" + popdetect + "']").css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
        $(".popup[data-popup-type='" + popdetect + "']").fadeIn(500);
        $(".popup[data-popup-type='" + popdetect + "']").addClass('show');
        var listheight = $('.search-content').height();
        if (listheight > 231) {
            $('.searchlist.tableheader').addClass('fixedposition');
        }
        $($(".set-sku-video"), $("#VideoMapPopup")).attr("data-skumapID", currentSKURow.ProductSKUMapID);

        if (currentSKURow.VideoMaps != null && currentSKURow.VideoMaps.length > 0) {
            $.each(currentSKURow.VideoMaps, function (index, item) { $scope.selectedSKUMapVideos.push(item) });
        }

        if ($scope.ProductModel.UploadVideoFiles != null && $scope.ProductModel.UploadVideoFiles.length > 0) {
            $.each($scope.ProductModel.UploadVideoFiles, function (index, item) { $scope.SKUMapVideosToSelect.push(item) });
        }
        if ($scope.uploadVideosInfo != null && $scope.uploadVideosInfo.length > 0) {
            $.each($scope.uploadVideosInfo, function (index, item) { $scope.SKUMapVideosToSelect.push(item) });
        }
    }

    $scope.MapVideosToskuLine = function (event) {

        var skuID = $(event.currentTarget).attr('data-skumapID');
        var sequence = 1;
        $.each($scope.ProductModel.SKUMappings, function (skuindex, skuItem) {
            if (skuItem.ProductSKUMapID == skuID) {
                if (skuItem.VideoMaps == null) {
                    skuItem.VideoMaps = [];
                }
                else {
                    existingSequenceList = []
                    if (skuItem.VideoMaps.length > 0) {
                        $.each(skuItem.VideoMaps, function (index, item) { existingSequenceList.push(parseInt(item.Sequence)); });
                        sequence = existingSequenceList != null && existingSequenceList != undefined ? Math.max.apply(Math, existingSequenceList) + 1 : sequence;
                    }
                }

                $.each($scope.SKUMapVideosToSelect, function (index, item) {
                    if (item.isSelected) {

                        var isImageAlreadyMapped = $.grep(skuItem.VideoMaps, function (e) { return e.ImageName == item.FileName; });

                        if (isImageAlreadyMapped.length <= 0) {

                            $scope.selectedSKUMapVideos.push({ VideoName: item.FileName, VideoPath: item.FilePath, Sequence: sequence });
                            skuItem.VideoMaps.push({ VideoName: item.FileName, VideoPath: item.FilePath, Sequence: sequence });
                            sequence++
                        }
                        item.isSelected = false;
                    }
                })
                return;
            }
        });
    }

    $scope.RemoveVideoMapsToSKuLines = function (imageInfo) {
        var skuID = $("#add", "#VideoMapPopup").attr('data-skumapID');
        $scope.selectedSKUMapVideos = $.grep($scope.selectedSKUMapVideos, function (e) { return e.VideoName != imageInfo.VideoName; });
        $.each($scope.ProductModel.SKUMappings, function (skuindex, skuItem) {
            if (skuItem.ProductSKUMapID == skuID) {
                skuItem.VideoMaps = $.grep(skuItem.VideoMaps, function (e) { return e.VideoName != imageInfo.VideoName; });
                return;
            }
        });
    }

    $scope.CloseVideoMapPopup = function (event) {
        $(event.currentTarget).closest('.popup').fadeOut(500);
        $(event.currentTarget).closest('.popup').removeClass('show');
        $('.overlaydiv').hide();
        $scope.selectedSKUMapVideos = [];
        $scope.SKUMapVideosToSelect = [];
    }


    $scope.ToggleVideoSelected = function (currentItem) {
        currentItem.isSelected = currentItem.isSelected ? false : true;
    }

    //*********** Video upload code ends here ********/


    //Entering barcode in sku section checking exists or not
    $scope.BarcodeExist = function (sku) {

        if (sku.BarCode == null || sku.BarCode == "") {
            sku.BarCodeExists = false;
            return;
        }

        $.ajax({
            url: "Inventory/GetProducts?searchText=" + sku.BarCode,
            type: 'GET',
            success: function (productsList) {

                if (productsList != undefined && productsList != null) {
                    var barCodeExists = $.grep(productsList, function (e) { return e.BarCode == sku.BarCode });

                    if(barCodeExists.length > 0) {
                        if (barCodeExists.length == 1 && barCodeExists[0].ProductSKUMapIID == sku.ProductSKUMapID) {
                            sku.barCodeExists = false;
                        }
                        else {
                            sku.barCodeExists = true;
                        }
                    }
                    else {
                        sku.BarCodeExists = false;
                    }

                    $scope.$apply();
                    return false;
                }
            }
        })
    }

    //Entering part number in sku section checking exists or not
    $scope.PartNumberExists = function (sku) {

        if (sku.PartNumber == null || sku.PartNumber == "") {
            sku.PartNoExists = false;
            return;
        }

        $.ajax({
            url: "Inventory/GetProducts?searchText=" + sku.PartNumber,
            type: 'GET',
            success: function (productsList) {
                if (productsList != undefined && productsList != null) {
                    var partNoExists = $.grep(productsList, function (e) { return e.PartNo == sku.PartNumber });

                    if (partNoExists.length > 0)
                    {
                        if (partNoExists.length == 1 && partNoExists[0].ProductSKUMapIID == sku.ProductSKUMapID) {
                            sku.PartNoExists = false;
                        }
                        else {
                            sku.PartNoExists = true;
                        }
                    }
                    else
                        sku.PartNoExists = false;

                    $scope.$apply();
                    return false;
                }
            }
        })
    }

    $scope.RichEditorClick1 = function (event, viewModel, fieldName, index, value) {
        event.preventDefault();
        //$scope.texteditor = $(event.currentTarget.closest('.controls')).find('textarea')[0];
            var model = { HtmlText: value, CallBack: "ApplyRichEditor" }
        $http({ method: 'POST', url: 'Mutual/ShowCKEditor', contentType: "application/json;charset=utf-8", data: model })
                .then(function (content) {
                    //$('.overlaydiv').show();
                    $('#dailogue').html($compile(content.data)($scope));
                    var xpos = $("#dailogue").outerWidth() / 2;
                    var ypos = $("#dailogue").outerHeight() / 2;
                    $("#dailogue").css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
                    $("#dailogue.popup").addClass('show');
                    $("#dailogue").fadeIn();
                    $("#dailogue").find('#apply').click(function () {
                        var col = GetFieldInstanceFromString(viewModel, fieldName, $("#dailogue").find('#richtext').val(), index);
                        $("#dailogue .cancel").click();
                    })
                });
    }

    $scope.RichEditorClick = function (event, viewModel, productInventoryConfigID, fieldName,index,value) {
        event.preventDefault();
        //$scope.texteditor = $(event.currentTarget.closest('.controls')).find('textarea')[0];
       $scope.texteditor = value;
        var model = { HtmlText: value, CallBack: "ApplyRichEditor"}
        $http({ method: 'POST', url: 'Mutual/ShowCKEditor', contentType: "application/json;charset=utf-8", data: model })
                .then(function (content) {
                    //$('.overlaydiv').show();
                    $('#dailogue').html($compile(content.data)($scope));
                    var xpos = $("#dailogue").outerWidth() / 2;
                    var ypos = $("#dailogue").outerHeight() / 2;
                    $("#dailogue").css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' });
                    $("#dailogue.popup").addClass('show');
                    $("#dailogue").fadeIn();
                    $("#dailogue").find('#apply').click(function () {
                        var col = GetFieldInstanceFromString(viewModel, productInventoryConfigID, fieldName, $("#dailogue").find('#richtext').val(), index);
                        $("#dailogue .cancel").click();
                    })
                });
    }

    function GetFieldInstanceFromString(fieldField, productInventoryConfigID, field, value, rowIndex) {
        if (productInventoryConfigID == null || productInventoryConfigID == undefined) {
            var obj = $scope;
            var colName = null;
            $.each(fieldField.split("."), function (index, val) {
                colName = val;
    
                if (obj[val] != null && val != field) {
                    obj = obj[val];
                }
            });
            obj[colName] = value;
            return obj[colName];
        }
        else {
                  if (field == "Description") {
                         fieldField.Description.CultureDatas[rowIndex].CultureValue = value;
                         }
             if (field == "Details") {
                         fieldField.Details.CultureDatas[rowIndex].CultureValue = value;
                         }
        }
    }

    $scope.CloseCKEditor = function () {
        $("#dailogue").fadeOut();
        $('#dailogue').html('');
        $('.overlaydiv').hide();
        $("#dailogue.popup").removeClass('show');
    }

    function GetFieldInstanceFromString1(fieldField, field, value) {
        var obj = $scope;
        var colName = null;
        $.each(fieldField.split("."), function (index, val) {
            colName = val;

            if (obj[val] != null && val != field) {
                obj = obj[val];
            }

        });

        obj[colName] = value;
        return obj[colName];
    }
    /** Changes for CKEditor **/

    $scope.UniqueTagV2 = function (selectedItem,tags) {

        if (selectedItem.Key != undefined) { //Existing Tag
            var existModel = $.grep(tags, function (item) { return item.Value.toLowerCase().trim() == selectedItem.Value.toLowerCase().trim() });

            if (existModel.length > 1) {
                alert("Tag Name already exists...");
                tags.pop(selectedItem);
            }

            return;
        }
        else { // New Tag
            var existsDB = $.grep(tags, function (item) { return item.Value.toLowerCase().trim() == selectedItem.Value.toLowerCase().trim() });

            if (existsDB.length > 0) {
                alert("Tag Name already exists...");
                tags.pop(selectedItem);
                return;
            }
        }
    }

    $scope.tagTransformV2 = function (newTag) {
        return { Key: newTag.toUpperCase(), Value: newTag.toUpperCase() };
    }

}]);