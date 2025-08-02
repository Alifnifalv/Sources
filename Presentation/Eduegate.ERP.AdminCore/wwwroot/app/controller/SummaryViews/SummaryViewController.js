app.controller("SummaryViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Summary View Detail");
    this.windowContainer = null;
    var ViewName = null;
    var IID = null;
    var map = null;
    $scope.ViewModels = [];
    $scope.ProductPriceLists = [];
    $scope.ProductBranchLists = [];
    $scope.Branches = [];
    $scope.PriceListStatuses = [];
    var DefaultDynamicView = null;

    $scope.SummaryViewInit = function (window, viewName, iid, defaultDynamicView, modelQuantity, modelCustomerGroup) {
        windowContainer = '#' + window;
        ViewName = viewName;
        DefaultDynamicView = defaultDynamicView;
        $scope.SetDefaultView(iid);
        $scope.ModelQuantityStructure = angular.copy(modelQuantity);

        if (modelCustomerGroup != undefined)
            $scope.CustomerGroupModel = angular.copy(modelCustomerGroup);
    }

    $scope.SetDefaultView = function (iid) {
        $scope.ViewModels = [];
        $('.summary-toggle', $(windowContainer)).removeClass('show');
        $('.sum_togglecontent', $(windowContainer)).slideUp('fast');
        IID = iid;
        $scope.GetDynamicView(DefaultDynamicView, IID, true);
    }

    $scope.CloseSummaryPanel = function (event) {
        $(event.currentTarget, $(windowContainer)).closest('.pagecontent').removeClass('summaryview detail-panel minimize-fields priceview');
        $(".preload-overlay", $(windowContainer)).css("display", "none");
        $(windowContainer).find("#summarypanel").html('');
        $(".quickviewpanel").removeClass("slide");  
    }

    $scope.GetDynamicView = function (viewName, iid, isReload, event) {
        if (!isReload) {
            if ($scope.ViewModels[viewName] != undefined) {
                return;
            }
        }

        if (event == undefined)
            $('.preload-overlay', $(windowContainer)).show();
        else
            $(event.currentTarget).append('<span class="fa fa-circle-o-notch fa-pulse summary-loader fa-spin">&nbsp;</span>');

        $http({ method: 'Get', url: 'Mutual/ExecuteView?type=' + viewName + '&IID1=' + iid })
        .then(function (result) {

            if (result.data != '') {
                $scope.ViewModels[viewName] = JSON.parse(result.data.replace(/\\/g, "/"));
            }

            $('.preload-overlay', $(windowContainer)).hide();
            if (event != undefined)
                $('.summary-loader', event.currentTarget).remove();
        });
    }

    $scope.GetDynamicViewGrid = function (viewName, iid, isReload, event, callback) {
        if (!isReload) {
            if ($scope.ViewModels[viewName] != undefined) {
                return;
            }
        }

        if (event == undefined)
            $('.preload-overlay', $(windowContainer)).show();
        else
            $(event.currentTarget).append('<span class="fa fa-circle-o-notch fa-pulse summary-loader fa-spin">&nbsp;</span>');

        $http({ method: 'Get', url: 'Mutual/ExecuteViewGrid?type=' + viewName + '&IID1=' + iid })
        .then(function (result) {
            if (result.data.length > 0) {
                $scope.ViewModels[viewName] = JSON.parse(result.data.replace('\\', '\\\\'));
            }

            $('.preload-overlay', $(windowContainer)).hide();

            if (event != undefined) {
                $('.summary-loader', event.currentTarget).remove();
            }

            if (callback != undefined)
                callback($scope.ViewModels);
        });
    }


    $scope.AccordinClick = function (isReload, event) {
        if (event == undefined)
            $('.preload-overlay', $(windowContainer)).show();
        else
            $(event.currentTarget).append('<span class="fa fa-circle-o-notch fa-pulse summary-loader fa-spin">&nbsp;</span>');
    }

    $scope.Edit = function (view, ID) {
        if ($scope.ShowWindow("Edit" + view, "Edit " + view, "Edit" + view))
            return;

        $("#Overlay").fadeIn(100);

        var editUrl = view + "/Edit/" + ID.toString();
        $http({ method: 'Get', url: editUrl })
        .then(function (result) {
            $("#LayoutContentSection").append($compile(result.data)($scope));
            $scope.AddWindow("Edit" + view, "Edit " + view, "Edit" + view);
            $("#Overlay").fadeOut(100);
        });
    }

    $scope.Print = function (event, reportName, reportHeader, reportFullName, headIID) {

        if ($scope.ShowWindow(reportName, reportHeader, reportName) || headIID == null)
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);


        if (headIID != undefined && headIID > 0) {
            headID = headIID;
        }

        //var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
        var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&HeadID=" + headID; // $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
        $('#' + windowName).append('<script>function onLoadComplete() { }</script><center></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
        var $iFrame = $('iframe[reportname=' + reportName + ']');
        $iFrame.on('load', function () {
            $("#Load", $('#' + windowName)).hide();
        });     
    };

    $scope.PrintTransaction = function (event, reportName, reportHeader, reportFullName, headIID) {
        $http({ method: 'GET', url: "Home/ViewReports?returnFileBytes=true&HeadID=" + headIID + "&reportName=" + reportFullName  })
       .then(function (filename) {
           var w = window.open();
           w.document.write('<iframe onload="isLoaded()" id="pdf" name="pdf" src="'+ filename.data + '"></iframe><script>function isLoaded(){window.frames[\"pdf\"].print();}</script>');
       });

    };

    $scope.ShowDeliveryDetails = function (viewName, iid, googleMapDivID, isReload, event) {

        if (!isReload) {
            if ($scope.ViewModels[viewName] != undefined) {
                return;
            }
        }

        if (event == undefined)
            $('.preload-overlay', $(windowContainer)).show();
        else
            $(event.currentTarget).append('<span class="fa fa-circle-o-notch fa-pulse summary-loader fa-spin">&nbsp;</span>');


        $http({ method: 'Get', url: 'Mutual/ExecuteView?type=' + viewName + '&IID1=' + iid })
       .then(function (result) {
           if (result.data.length > 0) {
               $scope.ViewModels[viewName] = JSON.parse(result.data);
               if ($scope.ViewModels.DeliveryDetails.Latitude != '') {
                   utility.GetMapByCoordinates($scope.ViewModels.DeliveryDetails.Latitude, $scope.ViewModels.DeliveryDetails.Longitude, 8, googleMapDivID)
               }
               else
                   document.getElementById(googleMapDivID).innerHTML = "Not delivered";

               //utility.GetMapByCoordinates(29.3117, 47.4818, 8, googleMapDivID)
               //utility.GetMapByCoordinates(latitude,londitude,Zoomlevel, googleMapDivID)//Lat long should be fetched from service, currently hardcoded
           }
           $('.preload-overlay', $(windowContainer)).hide();
           $('.summary-loader', event.currentTarget).remove();
       });
    }

    $scope.SaveDeliveryCharge = function (productDeliveryTypeMaps, iid, isProduct) {

        $('.preload-overlay', $(windowContainer)).show();
        var url = 'Mutual/SaveDeliveryCharges?IID=' + iid + '&isProduct=' + isProduct;

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(productDeliveryTypeMaps),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result == true)
                    $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }
    $scope.SaveCustomerDeliveryCharge = function (customerGroupDeliveryTypeMaps, iid) {

        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            type: "POST",
            url: "Mutual/SaveCustomerDeliveryCharges?customerGroupID=" + iid,
            data: JSON.stringify(customerGroupDeliveryTypeMaps),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result == true)
                    $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }
    $scope.SaveZoneDeliveryCharge = function (DeliveryTypeAllowedZoneMaps, iid) {

        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            type: "POST",
            url: "Mutual/SaveZoneDeliveryCharges?ZoneID=" + iid,
            data: JSON.stringify(DeliveryTypeAllowedZoneMaps),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result == true)
                    $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }
    $scope.SaveAreaDeliveryCharge = function (DeliveryTypeAllowedAreaMaps, iid) {

        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            type: "POST",
            url: "Mutual/SaveAreaDeliveryCharges?AreaID=" + iid,
            data: JSON.stringify(DeliveryTypeAllowedAreaMaps),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result == true)
                    $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.ResetSmartViewGrid = function (viewName, iid) {

        $('.preload-overlay', $(windowContainer)).show();

        $http({ method: 'Get', url: 'Mutual/ExecuteViewGrid?type=' + viewName + '&IID1=' + iid })
        .then(function (result) {
            if (result.data.length > 0)
                $scope.ViewModels[viewName] = JSON.parse(result.data);

            $('.preload-overlay', $(windowContainer)).hide();
        });
    }

    // Product Price Setting Smart View Code Starts Here//

    $scope.ToggleGroup = function (event) {
        $(event.currentTarget).closest('.sum_togglecontent ').toggleClass('show');

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

   

 

    $scope.GetPriceSettingCustomerGroups = function (IsProduct, viewName, IID) {

        if ($scope.ViewModels[viewName] != undefined)
            return;

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";

        if (IsProduct == true)
            url = "ProductPriceSetting/GetPriceSettingCustomerGroups?IID=" + IID;
        else
            url = "ProductSKUPriceSetting/GetCustomerGroupPriceSettingsForSKU?IID=" + IID;

        $.ajax({
            type: "Get",
            url: url,
            success: function (result) {
                if (result.length > 0)
                    $scope.ViewModels[viewName] = result;
                else
                    $scope.ViewModels[viewName] = [];
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.SavePriceSettingCustomerGroups = function (isProduct, viewName, productIID) {

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";

        if (isProduct == true)
            url = "ProductPriceSetting/SavePriceSettingCustomerGroups?productIID=" + productIID;
        else
            url = "ProductSKUPriceSetting/SaveProductSKUPriceCustomerGroup?IID=" + IID;

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify($scope.ViewModels[viewName]),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result.IsError == false) {
                    if (result.UpdatedData.length >= 0) {
                        $scope.ViewModels[viewName] = result.UpdatedData;
                        $().showMessage($scope, $timeout, false, "Price Setting Customer Groups Created/Updated Successfully.");
                    }
                }
                else {
                    $().showMessage($scope, $timeout, true, "Price Setting Customer Groups Created/Updated Failed.");
                }

                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

  

   

    // Product Price Setting Smart View Code Ends Here //

    $scope.DownloadProcessedFeed = function (id) {
        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            type: "Get",
            url: "DataFeed/DownloadProcessedFeedFile?feedLogID=" + id,
            success: function (result) {
                if (result.IsSuccess == true)
                    window.location = result.DownloadPath;
                $('.preload-overlay', $(windowContainer)).hide();
            }

        });
    }
 

    //Product price settings end


    $scope.GetDeliveryTypeSettings = function (viewName, iid) {

        if ($scope.ViewModels[viewName] != undefined)
            return;

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";
        // pass branchId to service so we will get based on Branch
        switch (viewName) {
            case "ProductTypeDeliveryTypes":
                url = "Distributions/ProductDeliverySetting/GetProductDeliverySettings?IID=" + iid + "&isProduct=" + true + "&branchId=" + 1;
                break;
            case "ProductSKUTypeDeliveryTypes":
                url = "Distributions/ProductSKUDeliverySetting/GetProductSKUDeliverySettings?IID=" + iid + "&isProduct=" + false;
                break;
            case "ZoneDeliveryTypes":
                url = "Distributions/ZoneDeliverySetting/GetZoneDeliverySettings?zoneID=" + iid;
                break;
            case "AreaDeliveryTypes":
                url = "Distributions/AreaDeliverySetting/GetAreaDeliverySettings?areaID=" + iid;
                break;
            case "CustomerDeliveryTypes":
                url = "Distributions/CustomerGroupDeliverySetting/GetCustomerGroupDeliverySettings?customerGroupID=" + iid;
                break;
            default:
                break;
        }

        $http({ method: 'Get', url: url })
        .then(function (result) {
            if (result.data.length > 0)
                $scope.ViewModels[viewName] = result.data;

            $('.preload-overlay', $(windowContainer)).hide();
        });
    }

    $scope.SaveDeliveryTypeSettings = function (viewName, iid) {

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";

        switch (viewName) {
            case "ProductTypeDeliveryTypes":
                url = "Distributions/ProductDeliverySetting/SaveProductDeliverySettings?IID=" + iid + "&isProduct=" + true;
                break;
            case "ProductSKUTypeDeliveryTypes":
                url = "Distributions/ProductSKUDeliverySetting/SaveProductSKUDeliverySettings?IID=" + iid + "&isProduct=" + false;
                break;
            case "ZoneDeliveryTypes":
                url = "Distributions/ZoneDeliverySetting/SaveZoneDeliverySettings?IID=" + iid;
                break;
            case "AreaDeliveryTypes":
                url = "Distributions/AreaDeliverySetting/SaveAreaDeliverySettings?IID=" + iid;
                break;
            case "CustomerDeliveryTypes":
                url = "Distributions/CustomerGroupDeliverySetting/SaveCustomerGroupDeliverySettings?IID=" + iid;
                break;
            default:
                break;
        }

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify($scope.ViewModels[viewName]),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result.IsError == false) {
                    if (result.UpdatedData.length >= 0) {
                        $scope.ViewModels[viewName] = result.UpdatedData;
                        $().showMessage($scope, $timeout, false, "Delivery Settings Created/Updated Successfully.");
                        $scope.GetDeliveryTypeSettings(viewName, iid);
                    }
                }
                else {
                    $().showMessage($scope, $timeout, true, "Delivery Settings Created/Updated Failed.");
                }

                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.ResetDeliverySetting = function (viewName, ID, branchId) {

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";

        switch (viewName) {
            case "ProductTypeDeliveryTypes":
                url = "Distributions/ProductDeliverySetting/GetProductDeliverySettings?IID=" + ID + "&isProduct=" + true;
                break;
            case "ProductSKUTypeDeliveryTypes":
                url = "Distributions/ProductSKUDeliverySetting/GetProductSKUDeliverySettings?IID=" + ID + "&isProduct=" + false+ "&branchId=" +branchId;   // + "&branchId=" + branchId
                break;
            case "ZoneDeliveryTypes":
                url = "Distributions/ZoneDeliverySetting/GetZoneDeliverySettings?zoneID=" + ID;
                break;
            case "AreaDeliveryTypes":
                url = "Distributions/AreaDeliverySetting/GetAreaDeliverySettings?areaID=" + ID;
                break;
            case "CustomerDeliveryTypes":
                url = "Distributions/CustomerGroupDeliverySetting/GetCustomerGroupDeliverySettings?customerGroupID=" + ID;
                break;
            default:
                break;
        }

        $http({ method: 'Get', url: url })
        .then(function (result) {
            if (result.data.length > 0)
                $scope.ViewModels[viewName] = result.data;

            $('.preload-overlay', $(windowContainer)).hide();
        });
    }

    //SKU Tagging

    $scope.GetSKUTagLists = function () {

        $.ajax({
            url: "Product/GetSKUTags",
            type: 'GET',
            success: function (result) {

                $scope.TagModel.AvailableTags = [];

                if (result.IsError == false) {
                    if (result.SKUTags != null && result.SKUTags.length > 0) {
                        $.each(result.SKUTags, function (index, item) {
                            $scope.TagModel.AvailableTags.push(item);
                        });
                    }
                }
            }
        });
    }

    $scope.GetProductSKUTagMaps = function (productSKUMapIID) {

        $('.preload-overlay', $(windowContainer)).show();
        var uri = "ProductSKUTag/GetProductSKUTagMaps?productSKUMapIID=" + productSKUMapIID;
        $.ajax({
            type: "Get",
            url: uri,
            success: function (result) {
                if (result.length > 0)
                    $scope.$apply(function () {
                        $scope.TagModel.SelectedTags = result;
                    });
                else
                    $scope.TagModel.SelectedTags = [];
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.SaveProductSKUTags = function () {
        $('.preload-overlay', $(windowContainer)).show();
        // temp array
        var skuIds = new Array();
        $scope.TagModel.SelectedTags;
        skuIds.push($scope.ViewModels.ProductSKUDetails.ProductSKUMapIID)
        $scope.TagModel.ProductSKUIDs = skuIds; //$scope.SelectedIds;

        var url = 'ProductSKUTag/SaveProductSKUTags';
        var dataString = JSON.stringify($scope.TagModel);
        $.ajax({
            type: "POST",
            url: url,
            data: dataString,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result.IsError == false) {
                    $().showMessage($scope, $timeout, false, "Product SKU Tag Created/Updated Successfully.");
                }
                else {
                    $().showMessage($scope, $timeout, true, "Product SKU Tag Create/Update Failed.");
                }

                $('.preload-overlay', $(windowContainer)).hide();
            }
        });

    }

    $scope.UniqueTagV2 = function (selectedItem) {

        if (selectedItem.Key != undefined) { //Existing Tag
            var existModel = $.grep($scope.TagModel.SelectedTags, function (item) {
                return item.Value.toLowerCase().trim() == selectedItem.Value.toLowerCase().trim()
            });

            if (existModel.length > 1) {
                alert("Tag Name already exists...");
                $scope.TagModel.SelectedTags.pop(selectedItem);
            }

            return;
        }

    }

    $scope.tagTransformV2 = function (newTag) {
        return {
            Key: newTag.toUpperCase(), Value: newTag.toUpperCase()
        };
    }

    $scope.SaveProductInventory = function (inventories, windowContainer) {
        $('.preload-overlay', $(windowContainer)).show();
        var url = 'Inventories/InventoryDetails/UpdateInventory';

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(inventories),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.AddInventoryBranch = function () {

    }

    $scope.GetBranches = function () {
        $('.preload-overlay', $(windowContainer)).show();
        var url = 'MarketPlace/SupplierProduct/GetMarketPlaceBranches';
        $.ajax({
            type: "GET",
            url: url,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                //console.log(result.result);
                $timeout(function () {
                    $scope.Branches = result.result;
                });
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.SelectBranchInventory = function (viewName, iid, model, select2ctrl) {
        var branchExists = 0;
        if ($scope.ViewModels[viewName] != undefined && $scope.ViewModels[viewName] != null) {
            branchExists = $scope.ViewModels[viewName].filter(function (e) { return e.BranchID == select2ctrl.selected.Key; }).length;
        }
        else {
            $scope.ViewModels[viewName] = [];
        }

        if (branchExists > 0) {
            alert("Selected Branch Already Exists");
            return false;
        }

        model.BranchID = eval(select2ctrl.selected.Key);
        model.Batch = 0;
        model.Quantity = 0;
        model.ProductSKUMapID = iid;
        model.CostPrice = 0;
        model.Branch = select2ctrl.selected.Value;
        $scope.ViewModels[viewName].push(model);

        select2ctrl.selected.Value = "";
        $scope.GetBranches();


    }

    $scope.UpdateProcessingStatus = function (transaction, model, event) {

        var x;
        x = window.confirm("Want to update Processing Status?");
        if (x == false) {
            return;
        }

        angular.element(event.target).addClass('rotation');
        var headId;
        if (model == "salesinvoice") {
            headId = transaction.InvoiceID;
        }
        else if (model == "salesorder") {
            headId = transaction.HeadIID;
        }
        $http({
            method: 'Get', url: "Transaction/TriggerReprocess?headId=" + headId
        })
        .then(function (result) {
            if (result.data.toLowerCase() == "true") {
                if (model == "salesinvoice") {
                    transaction.ProcessingStatus = "New";
                }
                else if (model == "salesorder") {
                    transaction.TransactionStatus = "New";
                }

            }

            else {
                // show message of error
                $().showMessage($scope, $timeout, true, "Reprocessing Status Failed. ");
            }
        });
    }

    $scope.SelectBranchList = function (viewName, iid, model, select2ctrl) {

        if ($scope.ViewModels[viewName] == undefined)
            $scope.ViewModels[viewName] = [];

        model.BranchID = eval(select2ctrl.selected.Key);
        model.BranchName = select2ctrl.selected.Value;
        model.ProductSKUMapID = iid;

        /* restrict to add dublicate branch */
        
        if ($scope.ViewModels[viewName].length > 0) {
            var branchLisExists = $.grep($scope.ViewModels[viewName], function (result) { return result.BranchID == select2ctrl.selected.Key; }).length;

            if (branchLisExists > 0) {
                alert("Selected Branch Already Exists");
                return false;
            }
        }

        $scope.ViewModels[viewName].push(model);

        select2ctrl.selected.Value = "";
        $scope.GetProductBranchLists(); //Re binding the select2 control data to clear the selected value

        $scope.GetBranchDeliveryTypeSettings("ProductSKUTypeDeliveryTypes", model);
    }

    $scope.GetBranchDeliveryTypeSettings = function (viewName, model) {
        var iid = model.ProductSKUMapID;
        var branchId = model.BranchID;

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";
        // pass branchId to service so we will get based on Branch
        switch (viewName) {
            case "ProductSKUTypeDeliveryTypes":
                url = "Distributions/ProductSKUDeliverySetting/GetProductSKUDeliverySettings?IID=" + iid + "&isProduct=" + false + "&branchId=" + branchId;
                break;
            default:
                break;
        }

        $http({ method: 'Get', url: url })
        .then(function (result) {
            if (result.data.length > 0)

                // find based on branchId 
                for (var i = 0; i < $scope.ViewModels.ProductSKUBranchSetting.length; i++) {
                    if ($scope.ViewModels.ProductSKUBranchSetting[i].BranchID == branchId) {
                        // add the object with viewName
                        $scope.ViewModels.ProductSKUBranchSetting[i][viewName] = result;
                    }
                }

            $('.preload-overlay', $(windowContainer)).hide();
        });
    }

    $scope.SaveBranchDeliveryTypeSettings = function (viewName, model) {

        $('.preload-overlay', $(windowContainer)).show();
        var url = "";
        // pass branchId to service so we will get based on Branch
        switch (viewName) {
            case "ProductSKUTypeDeliveryTypes":
                url = "Distributions/ProductSKUDeliverySetting/SaveProductSKUBranchDeliverySettings";
                break;
            default:
                break;
        }
        console.log(model);
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(model),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                console.log(result);

                if (result.IsError == false) {
                    if (result.UpdatedData.length >= 0) {
                        $scope.ViewModels[viewName] = result.UpdatedData;
                        $().showMessage($scope, $timeout, false, " Branch Delivery Settings Created/Updated Successfully.");
                        $scope.GetBranchDeliveryTypeSettings(viewName, model);
                    }
                }
                else {
                    $().showMessage($scope, $timeout, true, "Branch Delivery Settings Created/Updated Failed.");
                }

                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

    $scope.GetBranchSettings = function (viewName, skuIID) {

        $('.preload-overlay', $(windowContainer)).show();

        $.ajax({
            type: "GET",
            contentType: "application/json;charset=utf-8",
            url: "Distributions/ProductSKUDeliverySetting/GetBranchDeliverySettings?skuIID=" + skuIID,
            success: function (result) {
                if (result.data == null)
                    $scope.ViewModels[viewName] = [];
                else
                    $scope.ViewModels[viewName] = result.data;
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

   
    $scope.ClosePopup = function (event) {
        $(event.currentTarget).hide();
        $('.popup.gridpopupfields', $(windowContainer)).fadeOut("fast");
        $('.popup.gridpopupfields', $(windowContainer)).removeAttr('data-list');
        $('#popupContainer', $(windowContainer)).html('');
        //$('.statusview').removeClass('active');
    }
   
}]);