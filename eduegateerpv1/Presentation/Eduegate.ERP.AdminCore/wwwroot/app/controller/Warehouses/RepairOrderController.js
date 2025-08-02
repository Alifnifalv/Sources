app.controller("RepairOrderController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("RepairOrderController");

        $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService });

        $scope.GetVehcileDetails = function (event, element, model) {
            $('.preload-overlay').attr('style', 'display:block');

            $http({ method: 'Get', url: 'RepairOrder/GetVehcileDetails?chasisNo=' + model.ChasisNo + '&registationNo=' + model.RegitrationNo })
            .then(function (result) {
                model.VehicleDescription = result.DESCRIPTION;
                model.StockNoModel = result.STOCKNO;
                model.RegitrationNo = result.KTNO;
                model.RegitrationDate = result.REGISTRATION;
                model.BillVehicleType = result.VEH_TYPE;
                model.WarrantyKMs = result.WARRANTYKMv;
                model.LastServiceKMs = result.LASTKM;
                $('.preload-overlay').hide();
            });
        }
    }]);