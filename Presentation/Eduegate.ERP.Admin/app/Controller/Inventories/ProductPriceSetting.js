app.controller("ProductPriceSetting", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("ProductPriceSetting");
        var vm = this;

        vm.ViewModels = [];
        vm.ProductPriceLists = [];
        vm.ProductBranchLists = [];
        vm.Branches = [];
        vm.PriceListStatuses = [];

        vm.Init = function (window) {
            windowContainer = '#' + window;        

            vm.LoadProductPriceLists();
            vm.GetProductPriceLists(true);
            // To get Branches
            vm.LoadProductBranchLists();
            vm.GetProductBranchLists();
            vm.GetBranches();
            vm.GetPriceSettingStatus();
        }

        vm.GetBranches = function () {
            $('.preload-overlay', $(windowContainer)).show();
            var url = 'MarketPlace/SupplierProduct/GetMarketPlaceBranches';
            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    //console.log(result.result);
                    $timeout(function () {
                        vm.Branches = result.result;
                    });
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }


        vm.ResetPriceSettingCustomerGroups = function (isProduct, viewName, IID) {
            vm.ViewModels[viewName] = undefined;
            vm.GetPriceSettingCustomerGroups(isProduct, viewName, IID);
        }

        vm.LoadProductBranchLists = function () {
            var searchText = ($('#spanBranchList').find('input')[0]) != undefined ? ($('#spanBranchList').find('input')[0]).value : '';

            $timeout(function () {
                $($('#spanBranchList').find('input')[0]).keyup(function () {
                    if (event.keyCode != 38 && event.keyCode != 40) {
                        vm.GetProductBranchLists();
                    }
                });
            }, 100);
        }

        vm.GetProductBranchLists = function () {

            $http({
                method: 'Get', url: 'ProductSKUPriceSetting/GetProductBranchLists'
            })
                .then(function (result) {
                    vm.ProductBranchLists = result.data;
                });
        }

        vm.GetProductPriceSettingForSKU = function (viewName, skuIID) {
            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "Get",
                url: "ProductSKUPriceSetting/GetProductPriceListForSKU?skuIID=" + skuIID,
                success: function (result) {
                    if (result.length > 0)
                        $scope.$apply(function () {
                            vm.ViewModels[viewName] = result;
                        });
                    else
                        vm.ViewModels[viewName] = [];

                    /* hide rows based on MarketPlace */
                    if (result.length > 0) {
                        vm.isMarketPlace = !result[0].IsMarketPlace;
                    }

                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        vm.SelectPriceList = function (viewName, iid, model, select2ctrl) {

            if (vm.ViewModels[viewName] == undefined)
                vm.ViewModels[viewName] = [];

            if (vm.ViewModels[viewName].length > 0) {
                var priceListExists = $.grep(vm.ViewModels[viewName], function (result) { return result.ProductPriceListID == select2ctrl.selected.Key; }).length;

                if (priceListExists > 0) {
                    alert("Selected Price List Already Exists");
                    return false;
                }
            }

            model.ProductPriceListID = eval(select2ctrl.selected.Key);
            model.PriceDescription = select2ctrl.selected.Value;
            model.ProductSKUID = iid;

            vm.ViewModels[viewName].push(model);

            select2ctrl.selected.Value = "";
            vm.GetProductPriceLists(); //Re binding the select2 control data to clear the selected value
        }

        vm.SaveSellingPriceSetting = function (viewName, skuIID) {
            if (vm.ViewModels[viewName]) {
                if (vm.ViewModels[viewName].length == 0) {
                    return;
                }
            }

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "POST",
                url: "ProductSKUPriceSetting/SaveProductSKUPrice?skuIID=" + skuIID,
                data: JSON.stringify(vm.ViewModels[viewName]),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    console.log(result);

                    if (result.IsError == false) {
                        if (result.ProductPriceSKUs.length >= 0) {
                            vm.ViewModels[viewName] = result.ProductPriceSKUs;
                            $().showMessage($scope, $timeout, false, "Price Settings Created/Updated Successfully.");
                            vm.GetProductPriceSettingForSKU(viewName, skuIID);
                        }
                    }
                    else {
                        $().showMessage($scope, $timeout, true, "Price Settings Created/Updated Failed.");
                    }

                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        vm.CancelClick = function (event, model) {
            model.splice(0, model.length);
        }

        vm.GetDataHistory = function (entityID, IID, fieldName, event) {

            event.stopPropagation();
            var yposition = event.pageY - 234;

            $('.popup.gridpopupfields', $(windowContainer)).slideDown("fast");
            $('.popup.gridpopupfields', $(windowContainer)).attr('data-list', fieldName);
            $('.popup.gridpopupfields', $(windowContainer)).css({ 'top': yposition });
            $('.transparent-overlay', $(windowContainer)).show();

            $('#popupContainer', $(windowContainer)).html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            var url;
            url = 'Logger/DataHistoryTemplate';

            $http.get(url).then(function (response) {
                var content = response.data
                $('#popupContainer', $(windowContainer)).html($compile(content)($scope));
                url = 'Logger/GetDataHistory?EntityID=' + entityID + '&IID=' + IID + '&FieldName=' + fieldName;
                $http.get(url).then(function (response) {
                    vm.ViewModels.DH = response.data;
                    $('#popupContainer', $(windowContainer)).removeClass('loading');
                    $('#popupContainer', $(windowContainer)).addClass('loaded');
                });
            });
        }

        vm.ToggleQuantity = function (event, index, row, model) {
            $(event.currentTarget).closest('tr').next('tr.quantity-toggle').find('.quantity-fieldswrap').slideToggle('fast');

            if (model.length == 0) {
                vm.InsertGridRow(event, index, row, model);
            }

            event.stopPropagation();
        }

        vm.RemoveGridRow = function (event, index, row, model) {

            if (model.length == 1)
                $(event.currentTarget).parents('tr.quantity-toggle').find('.quantity-fieldswrap').slideToggle('fast');

            model.splice(index, 1);
        }

        vm.InsertGridRow = function (event, index, row, model) {
            model.splice(index + 1, 0, angular.copy(row));
        }


        vm.RemoveCustomerGridRow = function (event, rowindex, row, viewName, model) {

            $.each(vm.ViewModels[viewName], function (index, item) {

                vm.ViewModels[viewName][index].CustomerGroupPrices.splice(rowindex, 1);
            });

        }

        vm.LoadProductPriceLists = function () {
            var searchText = ($('#spanPriceList').find('input')[0]) != undefined ? ($('#spanPriceList').find('input')[0]).value : '';

            $timeout(function () {
                $($('#spanPriceList').find('input')[0]).keyup(function () {
                    if (event.keyCode != 38 && event.keyCode != 40) {
                        vm.GetProductPriceLists();
                    }
                });
            }, 100);
        }

        vm.GetProductPriceLists = function (opentab) {

            $http({ method: 'Get', url: 'ProductSKUPriceSetting/GetProductPriceLists' })
                .then(function (result) {
                    vm.ProductPriceLists = result.data;

                    if (opentab) {
                        $timeout(function () {
                            $('.sum_toggleheader', $(windowContainer)).trigger('click');
                        });
                    }
                });
        }

        vm.SelectPriceListForCustomerGroup = function (isProduct, viewName, iid, model, select2Ctrl) {

            var priceListExists = $.grep(vm.ViewModels[viewName][0].CustomerGroupPrices, function (result) { return result.ProductPriceListID == select2Ctrl.selected.Key; }).length;

            if (priceListExists > 0) {
                alert("Selected Price List Already Exists");
                return false;
            }

            $.each(vm.ViewModels[viewName], function (index, item) {

                model.ProductPriceListID = eval(select2Ctrl.selected.Key);
                model.PriceListName = select2Ctrl.selected.Value;
                model.ProductSKUID = isProduct == false ? iid : null;
                model.ProductID = isProduct == true ? iid : null;
                model.CustomerGroupID = vm.ViewModels[viewName][index].CustomerGroupIID;

                vm.ViewModels[viewName][index].CustomerGroupPrices.push(angular.copy(model));
            });

            select2Ctrl.selected.Value = "";
            vm.GetProductPriceLists(); //Re binding the select2 control data to clear the selected value
        }

        vm.SelectCategoryPriceList = function (viewName, iid, model, select2ctrl) {

            if (vm.ViewModels[viewName] == undefined)
                vm.ViewModels[viewName] = [];

            if (vm.ViewModels[viewName].length > 0) {
                var priceListExists = $.grep(vm.ViewModels[viewName], function (result) { return result.ProductPriceListID == select2ctrl.selected.Key; }).length;

                if (priceListExists > 0) {
                    alert("Selected Price List Already Exists");
                    return false;
                }
            }

            model.ProductPriceListID = eval(select2ctrl.selected.Key);
            model.PriceDescription = select2ctrl.selected.Value;
            model.CategoryID = iid;

            vm.ViewModels[viewName].push(model);

            select2ctrl.selected.Value = "";
            vm.GetProductPriceLists(); //Re binding the select2 control data to clear the selected value
        }
        // GetCategoryPriceSettings
        vm.GetCategoryPriceSettings = function (viewName, categoryIID) {

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "Get",
                url: "CategoryPriceSetting/GetCategoryPriceSettings?categoryIID=" + categoryIID,
                success: function (result) {
                    if (result.length > 0)
                        vm.ViewModels[viewName] = result;
                    else
                        vm.ViewModels[viewName] = [];
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }
        //SaveCategoryPriceSetting

        vm.SaveCategoryPriceSetting = function (viewName, categoryIID) {

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "POST",
                url: "CategoryPriceSetting/SaveCategoryPriceSettings?categoryIID=" + categoryIID,
                data: JSON.stringify(vm.ViewModels[viewName]),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    console.log(result);

                    if (result.IsError == false) {
                        if (result.CategoryPrices.length >= 0) {
                            vm.ViewModels[viewName] = result.CategoryPrices;
                            $().showMessage($scope, $timeout, false, "Price Settings Created/Updated Successfully.");
                        }
                    }
                    else {
                        $().showMessage($scope, $timeout, true, "Price Settings Created/Updated Failed.");
                    }

                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        vm.SelectBrandPriceList = function (viewName, iid, model, select2ctrl) {

            if (vm.ViewModels[viewName] == undefined)
                vm.ViewModels[viewName] = [];

            if (vm.ViewModels[viewName].length > 0) {
                var priceListExists = $.grep(vm.ViewModels[viewName], function (result) { return result.ProductPriceListID == select2ctrl.selected.Key; }).length;

                if (priceListExists > 0) {
                    alert("Selected Price List Already Exists");
                    return false;
                }
            }

            model.ProductPriceListID = eval(select2ctrl.selected.Key);
            model.PriceDescription = select2ctrl.selected.Value;
            model.BrandID = iid;

            vm.ViewModels[viewName].push(model);

            select2ctrl.selected.Value = "";
            vm.GetProductPriceLists(); //Re binding the select2 control data to clear the selected value
        }
        // GetBrandPriceSettings
        vm.GetBrandpriceSettings = function (viewName, brandIID) {

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "Get",
                url: "BrandPriceSetting/GetBrandPriceSettings?brandIID=" + brandIID,
                success: function (result) {
                    if (result.length > 0)
                        vm.ViewModels[viewName] = result;
                    else
                        vm.ViewModels[viewName] = [];
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }
        //SaveBrandPriceSetting

        vm.SaveBrandPriceSetting = function (viewName, brandIID) {

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "POST",
                url: "BrandPriceSetting/SaveBrandPriceSettings?brandIID=" + brandIID,
                data: JSON.stringify(vm.ViewModels[viewName]),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    console.log(result);

                    if (result.IsError == false) {
                        if (result.BrandPrices.length >= 0) {
                            vm.ViewModels[viewName] = result.BrandPrices;
                            $().showMessage($scope, $timeout, false, "Price Settings Created/Updated Successfully.");
                        }
                    }
                    else {
                        $().showMessage($scope, $timeout, true, "Price Settings Created/Updated Failed.");
                    }

                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        //Product price Settings

        vm.GetProductPriceSettings = function (viewName, productIID) {

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "Get",
                url: "ProductPriceSetting/GetProductPriceSettings?productIID=" + productIID,
                success: function (result) {
                    if (result.length > 0)
                        vm.ViewModels[viewName] = result;
                    else
                        vm.ViewModels[viewName] = [];
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        vm.SelectProductPriceSettingPrice = function (viewName, iid, model, select2ctrl) {

            if (vm.ViewModels[viewName] == undefined)
                vm.ViewModels[viewName] = [];

            if (vm.ViewModels[viewName].length > 0) {
                var priceListExists = $.grep(vm.ViewModels[viewName], function (result) { return result.ProductPriceListID == select2ctrl.selected.Key; }).length;

                if (priceListExists > 0) {
                    alert("Selected Price List Already Exists");
                    return false;
                }
            }

            model.ProductPriceListID = eval(select2ctrl.selected.Key);
            model.PriceDescription = select2ctrl.selected.Value;
            model.ProductID = iid;

            vm.ViewModels[viewName].push(model);

            select2ctrl.selected.Value = "";
            vm.GetProductPriceLists(); //Re binding the select2 control data to clear the selected value
        }

        vm.SaveProductPriceSettings = function (viewName, productIID) {

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "POST",
                url: "ProductPriceSetting/SaveProductPriceSettings?productIID=" + productIID,
                data: JSON.stringify(vm.ViewModels[viewName]),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    console.log(result);

                    if (result.IsError == false) {
                        if (result.ProductPriceSettings.length >= 0) {
                            vm.ViewModels[viewName] = result.ProductPriceSettings;
                            $().showMessage($scope, $timeout, false, "Price Settings Created/Updated Successfully.");
                        }
                    }
                    else {
                        $().showMessage($scope, $timeout, true, "Price Settings Created/Updated Failed.");
                    }

                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        vm.GetPriceSettingStatus = function () {
            $http({ method: 'Get', url: 'ProductSKUPriceSetting/GetProductPriceListStatus' })
                .then(function (result) {
                    vm.PriceListStatuses = result.data;
                });
        }
    }]);