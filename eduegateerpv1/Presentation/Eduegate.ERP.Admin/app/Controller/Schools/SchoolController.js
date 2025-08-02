app.controller("SchoolController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];

    $scope.BankDropDownChanges = function (gridModel) {
        showOverlay();
        var model = gridModel;
        var bankID = model.Bank?.Key;

        var url = "Schools/School/GetBankDetailsByBankID?bankID=" + bankID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                var resultData = result.data;

                gridModel.PayerBankShortName = resultData.BankShortName;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.StudentBMI = function (model) {

        if (model.Weight == null || model.Height == null) {
            
            model.BMS = null;
            return null;
        }
        model.BMS = Math.round(((model.Weight) / (model.Height * model.Height)) * 10000);

    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);