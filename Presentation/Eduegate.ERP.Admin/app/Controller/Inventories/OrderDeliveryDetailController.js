app.controller("OrderDeliveryDetailController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("Summary View Detail");
        angular.extend(this, $controller('SummaryViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.CountryLists =[];
        $scope.CityLists = [];
        $scope.skuStatuses = [];
        $scope.Init = function (ViewIID) {
            $scope.GetDeliveryTypes();
            $scope.GetCountries();
            $scope.GetDeliveryDetails(ViewIID);
        }

        $scope.GetDeliveryTypes = function () {
            var url = "SalesOrder/GetDeliveryOptions";
            $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.deliveryTypes = result;
            });
        }

        $scope.GetDeliveryDetails = function (ID) {
            $('.preload-overlay', $(windowContainer)).show();
            var editUrl = "SalesOrder/GetDeliveryDetails?Id=" + ID;
            $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                console.log(result.data);
                if (!result.IsError) {
                    $scope.OrderDeliveryDetailViewModel = result.data;
                    $scope.GetCityByCountryID(result.data.DeliveryDetails.Country.Key);
                    $scope.GetAreaByCityID(result.data.DeliveryDetails.City.Key);
                }
                else {
                    $().showMessage($scope, $timeout, true, result.UserMessage);
                }
                $('.preload-overlay', $(windowContainer)).hide();

            });
        }

        $scope.GetCountries = function () {
            var url = "Mutual/GetCountries";
            $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.Countries = result;
            });
        }


        $scope.GetCityByCountryID = function (country) {
            var url = "Mutual/GetCityByCountryID?countryID=" + country;
            $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.Cities = result;
            });
        }

        $scope.GetAreaByCityID = function (city) {
            var url = "Mutual/GetAreaByCityID?cityID=" + city;
            $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.Areas = result;
            });
        }

        $scope.SaveDeliveryDetails = function (model) {
            $('.preload-overlay', $(windowContainer)).show();
            var url = "SalesOrder/SaveDeliveryDetails";
            var OrderDeliveryDetail = model;
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(OrderDeliveryDetail),
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError)
                        $('.preload-overlay', $(windowContainer)).hide();
                    else
                        $().showMessage($scope, $timeout, true, result.UserMessage);
                }
            });
        }

    }]);