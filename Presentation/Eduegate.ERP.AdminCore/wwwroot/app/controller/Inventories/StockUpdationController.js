app.controller("StockUpdationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.FillDiffDatasOnly = function ($event, $element) {

        if ($event.FillDiffDatasOnly == true) {
            var fullList = $event.StockUpdationDetail;

            var filteredData = fullList.filter(x => x.DifferenceQuantity != 0);
            $event.StockUpdationDetail = filteredData;
        }

        else {
            $('#Overlay').show();
            var url = "Inventories/StockVerification/FillPhysicalStockDataINStockUpdate?ID=" + $event.StockVerificationHeadID;
            $http({
                method: 'Get', url: url
            })
                .then(function (result) {
                    $event.StockUpdationDetail = result.data.StockUpdationDetail;
                    $('#Overlay').hide();
                }, function () {
                    $('#Overlay').hide();
                });
        }
    }


    $scope.FillProductList = function ($event, $element, crudModel) {
        if (crudModel.Branch == null || crudModel.Branch.Key == 0 || crudModel.Branch.Key == "" || crudModel.Branch.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select Branch !");
            return false;
        }
        $('#Overlay').show();
        var model = crudModel;

        var url = utility.myHost + "Inventories/StockVerification/FillProductItemsForPhyscicalStockVerification?ID=" + model.Branch.Key + "&dateString=" + model.TransactionDate;
        $http({
            method: 'Get', url: url
        })
            .then(function (result) {
                $scope.CRUDModel.Model.DetailViewModel = result.data;
                $('#Overlay').hide();
            });
    };

}]);