app.controller("QuotationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.RFQChanges = function ($event, model) {
        model.Quotations = null;
        model.ItemList = null;
        model.ComparedList = null;
        model.ListDescription = null;

        var data = { rfqHeadIID: model.RFQ.Key };
        $.ajax({
            type: "GET",
            url: "/Inventories/PurchaseQuotation/FillQuotationsByRFQ",
            contentType: "application/json",
            data: data,
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        $scope.CRUDModel.ViewModel.Quotations = result;

                        if (result.length == 0) {
                            $scope.CRUDModel.ViewModel.ListDescription = "There are no quotations for the " + model.RFQ.Value;
                        }
                        else {
                            $scope.CRUDModel.ViewModel.ListDescription = "There are " + result.length + " quotations submitted for " + model.RFQ.Value;
                        }
                    }
                });
            }
        });
    };
    
    $scope.QuotationChanges = function ($event, model) {
        model.ItemList = null;
        model.ComparedList = null;
        model.ListDescription = null;
        model.IsListCompared = null;
    };

    $scope.FillItemList = function ($event, crudModel) {

        if (crudModel.RFQ == null || crudModel.RFQ == undefined) {
            $().showMessage($scope, $timeout, true, "Please select RFQ First!");
            return false;
        }

        if (crudModel.Quotations == null || crudModel.Quotations == undefined) {
            $().showMessage($scope, $timeout, true, "Please select quotations !");
            return false;
        }

        $('#Overlay').show();

        $scope.CRUDModel.ViewModel.IsListCompared = null;
        $scope.CRUDModel.ViewModel.ComparedList = null;

        var QuotationIDs = crudModel.Quotations.map(function (item) {
            return { Key: item.Key, Value: item.Value };
        });

        var loadUrl = "/Inventories/PurchaseQuotation/FillQuotationItemList";

        $.ajax({
            type: "POST",
            url: loadUrl,
            data: JSON.stringify(QuotationIDs),
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        $scope.CRUDModel.ViewModel.ItemList = result;
                    }
                });
            }
        }); 

        //For SupplierRemarkList
        $.ajax({
            type: "POST",
            url: "/Inventories/PurchaseQuotation/GetSupplierRemarkList",
            data: JSON.stringify(QuotationIDs),
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        $scope.CRUDModel.ViewModel.SupplierRemarks = result;
                    }
                });
            }
        });

        $('#Overlay').hide();
    };

    $scope.ComapareList = function ($event, crudModel) {
        if (crudModel.ItemList == null || crudModel.ItemList == undefined || crudModel.ItemList[0].DetailIID == 0) {
            $().showMessage($scope, $timeout, true, "There is no item list to compare !");
            return false;
        }
        $('#Overlay').show();

        const filteredList = crudModel.ItemList.filter(item => item.Quantity > 0);
        // Group the items by ProductSKUMapID
        const groupedItems = {};
        filteredList.forEach(item => {
            if (!groupedItems[item.SKUID.Key]) {
                groupedItems[item.SKUID.Key] = [item];
            } else {
                groupedItems[item.SKUID.Key].push(item);
            }
        });

        // Find the item with the least price in each group
        const result = Object.values(groupedItems).map(group => {
            return group.reduce((acc, cur) => acc.UnitPrice < cur.UnitPrice ? acc : cur);
        });

        $scope.CRUDModel.ViewModel.ComparedList = result;
        $scope.CRUDModel.ViewModel.IsListCompared = "YES";

        $('#Overlay').hide();
    };


    //BidOpening Screen/GeneratePO
    $scope.BidRFQChanges = function ($event, model) {
        $scope.LookUps.Bids = null;
        model.FinalList = null;
        model.ListDescription = null;
        model.Bids = null;
        $scope.CRUDModel.ViewModel.ComparedList = null;

        var data = { rfqHeadIID: model.ClosedRFQ.Key };
        $.ajax({
            type: "GET",
            url: "/Inventories/PurchaseQuotation/FillBidLookUpByRFQ",
            contentType: "application/json",
            data: data,
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        $scope.LookUps.Bids = result;

                        if (result.length == 0) {
                            $scope.CRUDModel.ViewModel.ListDescription = "There are no bids against " + model.ClosedRFQ.Value;
                        }
                        else {
                            $scope.CRUDModel.ViewModel.ListDescription = "There are " + result.length + " bids approved for " + model.ClosedRFQ.Value;
                        }
                    }
                });
            }
        });
    };

    $scope.BidChanges = function ($event, crudModel) {

        if (crudModel.ClosedRFQ == null || crudModel.ClosedRFQ == undefined) {
            $().showMessage($scope, $timeout, true, "Please select RFQ First!");
            return false;
        }

        if (crudModel.Bids == null || crudModel.Bids == undefined) {
            $().showMessage($scope, $timeout, true, "Please select bid !");
            return false;
        }

        $('#Overlay').show();

        $scope.CRUDModel.ViewModel.ComparedList = null;

        var data = { bidApprovalIID: crudModel.Bids.Key };

        var loadUrl = "/Inventories/PurchaseQuotation/FillBidItemList";

        $.ajax({
            type: "GET",
            url: loadUrl,
            data: data,
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        $scope.CRUDModel.ViewModel.ComparedList = result;
                    }
                });
            }
        });

        $('#Overlay').hide();
    };


    //Dropdown changes function for Tender authendication grid
    $scope.EmployeeDropdownChanges = function ($event, $index, model) {

        $.ajax({
            type: "GET",
            url: "/Inventories/PurchaseQuotation/GetEmployeeDetailsByEmployeeIID?employeeIID=" + model[$index].Employee.Key,
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        model[$index].UserName = result.EmployeeName;
                        model[$index].UserID = result.EmployeeCode;
                        model[$index].EmailID = result.WorkEmail;
                    }
                });
            }
        });
    };

    $scope.ExistingUserSelects = function ($event, $index, model) {

        model[$index].Employee = null;
        model[$index].UserName = null;
        model[$index].UserID = null;
        model[$index].EmailID = null;

        $.ajax({
            type: "GET",
            url: "/Inventories/PurchaseQuotation/GetBidUserDetailsByAuthenticationID?authendicationID=" + model[$index].BidUsers.Key,
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        model[$index].AuthenticationID = result.AuthenticationID;
                        model[$index].Employee = result.Employee;
                        model[$index].UserName = result.UserName;
                        model[$index].UserID = result.UserID;
                        model[$index].EmailID = result.EmailID;
                    }
                });
            }
        });
    };

}]);