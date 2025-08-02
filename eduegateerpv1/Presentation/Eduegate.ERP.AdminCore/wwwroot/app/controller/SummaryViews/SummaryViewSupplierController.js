app.controller("SummaryViewSupplierController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Supplier Summary View Detail");
    $scope.Brands = null;
    var windowContainer = null;

    $scope.Init = function (window, model, ViewIID) {
        $scope.ProductViewModel = model;
        $scope.InitializeWayPoint();
        windowContainer = $('#' + window);
    }

    $scope.CloseSummaryPanel = function (event) {
        $(event.currentTarget, windowContainer).closest('.pagecontent').removeClass('summaryview');
        $(".preload-overlay", $(windowContainer)).css("display", "none");
        $(windowContainer).closest("#summarypanel").html('');
    }

    $scope.GetBrands = function (brnadID) {

        var brandListURL = "Brand/Get/" + brnadID;
        $http({ method: 'Get', url: brandListURL })
        .then(function (result) {
            $scope.Brands = result;
        });
    }

    $scope.EditProduct = function (productID) {
        if ($scope.ShowWindow("EditSupplier", "Edit Supplier", "EditSupplier"))
            return;

        $("#Overlay").fadeIn(100);

        var editUrl = "Supplier/Edit/" + productID.toString();
        $http({ method: 'Get', url: editUrl })
        .then(function (result) {
            $("#LayoutContentSection").append($compile(result)($scope));
            $scope.AddWindow("EditSupplier", "Edit Supplier", "EditSupplier");
            $("#Overlay").fadeOut(100);
        });
    }
}]);