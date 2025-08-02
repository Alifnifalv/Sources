app.controller("PurchaseRequestController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.GetProductItemsByRequestIds = function ($event, $element, crudModel) {
        var RequestIDs = crudModel.PurchaseRequests.map(function (item) {
            return { Key: item.Key, Value: item.Value };
        });

        var loadUrl = "/Inventories/PurchaseQuotation/GetProductsByPurchaseRequestID";

        $.ajax({
            type: "POST",
            url: loadUrl,
            data: JSON.stringify(RequestIDs),
            contentType: "application/json",
            success: function (result) {
                $scope.$apply(function () {
                    if (result == undefined) {
                        return false;
                    } else {
                        $scope.CRUDModel.Model.DetailViewModel = result;
                    }
                });
            }
        });
    };

}]);