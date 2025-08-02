app.controller("SummaryViewCustomerController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("Customer Summary View Detail");
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

    $scope.EditProduct = function (customerID) {
        if ($scope.ShowWindow("EditCustomer", "Edit Customer", "EditCustomer"))
            return;

        $("#Overlay").fadeIn(100);

        var editUrl = "Customer/Edit/" + customerID.toString();
        $http({ method: 'Get', url: editUrl })
        .then(function (result) {
            $("#LayoutContentSection").append($compile(result)($scope));
            $scope.AddWindow("EditCustomer", "Edit Customer", "EditCustomer");
            $("#Overlay").fadeOut(100);
        });
    }
}]);