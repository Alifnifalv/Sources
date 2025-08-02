app.controller("InventoryDetailSummaryViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("Inventory Details Summary View Detail");

    $controller('SummaryViewController', { $scope: $scope, $http: $http, $compile: $compile, $window: $window, $timeout: $timeout, $location: $location, $route: $route }); 

    //$scope.SaveProductInventory = function (inventories, windowContainer) {
    //    $('.preload-overlay', $(windowContainer)).show();
    //    var url = 'Inventories/InventoryDetails/UpdateInventory';

    //    $.ajax({
    //        type: "POST",
    //        url: url,
    //        data: JSON.stringify(inventories),
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            console.log(result);
    //            $('.preload-overlay', $(windowContainer)).hide();
    //        }
    //    });
    //}
}]);