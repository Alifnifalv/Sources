app.controller("SummaryViewProductSKUController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("ProductSKU Summary View Detail");
    $scope.skuStatuses = [];
    $scope.SKUViewModel = null;
    $scope.ProductLists = [];
    $scope.employees = [];
    var Toproduct = null;
    $scope.Init = function (window, model, ViewIID) {
        $scope.SKUViewModel = model;
        windowContainer = $('#' + window);
        //$scope.GetSKUStatuses();
    }
    $scope.GetSKUDetails = function (ViewIID) {
        $scope.GetSKUStatuses();
        $scope.GetProductSKUDetails(ViewIID);
    }

    //$scope.CloseSummaryPanel = function (event) {
    //    $(event.currentTarget, windowContainer).closest('.pagecontent').removeClass('summaryview');
    //    $(windowContainer).closest("#summarypanel").html('');
    //}
    $scope.Init = function (skuID, productModel) {
        $scope.LoadProductsList();
        $scope.LoadEmployeeList();
        $scope.GetEmployeeDetails(skuID);
        if (productModel != null || productModel != undefined) {
            $scope.ProductModel = angular.copy(productModel);
        }
        }

        $scope.LoadProductsList = function () {
            var searchText = ($('#spanProductList').find('input')[0]) != undefined ? ($('#spanProductList').find('input')[0]).value : '';

                $($('#spanProductList').find('input')[0]).keyup(function () {
                    if (event.keyCode != 38 && event.keyCode != 40) {
                        $scope.GetProductsList(event.currentTarget.value);
                    }
                });
        }
    
        $scope.GetProductsList = function (searchText) {
            $('.preload-overlay', $(windowContainer)).show();
            $http({ method: 'Get', url: "SKU/ProductSearch?searchText=" + searchText })
            .then(function (result) {
                $scope.ProductLists = result.Data;
                $('.preload-overlay', $(windowContainer)).hide();
            });
        }
        $scope.SelectProductList = function (model) {
            Toproduct = model.ProductID;
        }

        $scope.SaveSKUProductMap = function (skuID) {

            $('.preload-overlay', $(windowContainer)).show();
            var url = "SKU/SaveSKUProductMap?productID=" + Toproduct + '&skuID=' + skuID;
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(product),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError)
                        $('.preload-overlay', $(windowContainer)).hide();
                    else
                        $().showMessage($scope, $timeout, true, result.UserMessage);
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

    $scope.GetSKUStatuses = function () {
        var url = "Product/GetProductStatus"; 
        $http({ method: 'Get', url: url })
        .then(function (result) {
            $scope.skuStatuses = result.data;
        });
    }

    $scope.GetProductSKUDetails = function (skuID) {
        $('.preload-overlay', $(windowContainer)).show();
        var editUrl = "SKU/GetProductSKUDetails?IID=" + skuID.toString();
        $http({ method: 'Get', url: editUrl })
        .then(function (result) {
            console.log(result.data);
            if (!result.IsError) {
                $scope.SKUViewModel = result.data;
                $('.preload-overlay', $(windowContainer)).hide();
            } else {
                $().showMessage($scope, $timeout, true, result.UserMessage);
            }
            
        });
    }

    $scope.SaveProductSKUDetails = function (model) {
        $('.preload-overlay', $(windowContainer)).show();
        var url = "SKU/SaveProductSKUDetails";
        var sku = {sku: model};
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(sku),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError)
                    $('.preload-overlay', $(windowContainer)).hide();
                else
                    $().showMessage($scope, $timeout, true, result.UserMessage);
            }
        });
    }

    $scope.LoadEmployeeList = function () {
        var searchText = ($('#spanEmployeeList').find('input')[0]) != undefined ? ($('#spanEmployeeList').find('input')[0]).value : '';

        $($('#spanEmployeeList').find('input')[0]).keyup(function () {
            if (event.keyCode != 38 && event.keyCode != 40) {
                $scope.GetEmployeeList(event.currentTarget.value);
            }
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

    $scope.SaveSKUManager = function (skuID,ownerID) {

        $('.preload-overlay', $(windowContainer)).show();

        var url = "SKU/SaveSKUManagers?skuID=" + skuID + "&ownerID=" + ownerID;
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify($scope.ProductModel),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError)
                    $('.preload-overlay', $(windowContainer)).hide();
                else
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                $('.preload-overlay', $(windowContainer)).hide();
            }
        });
    }

        $scope.GetEmployeeDetails = function (skuID) {
        var editUrl = "SKU/GetEmployeeDetails?ID=" + skuID.toString();
        $http({ method: 'Get', url: editUrl })
        .then(function (result) {
            console.log(result);
            if (!result.IsError) { 
                $scope.ProductModel.KeyValueOwners = result.SKUOwner;
            } else {
                $().showMessage($scope, $timeout, true, result.UserMessage);
            }

        });
    }

}]);