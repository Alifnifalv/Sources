app.controller("WPSController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    console.log("WPSController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.Init = function (model, windowname, type) {
        $scope.type = type;
    };

    $scope.CurrentDate = null;

    $scope.SchoolChanges = function ($event, $element, model) {
        showOverlay();
        var schoolID = model.School?.Key;

        var url = "Schools/School/FillPayerBanksBySchoolID?schoolID=" + schoolID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.SchoolBankPayerDetails = result.data;

                $scope.CurrentDate = model.FileCreationDateString;
                $scope.LookUps.BankName = [];
                model.Bank = [];
                model.PayerBankDetailIID = null;
                model.BankID = null;
                model.PayerIBAN = null;
                model.PayerBankShortName = null;
                model.EmployerEID = null;
                model.PayerQID = null;
                model.PayerEID = null;

                if ($scope.SchoolBankPayerDetails.length > 0) {
                    $scope.SchoolBankPayerDetails.forEach(x => {
                        $scope.LookUps.BankName.push({
                            "Key": x.Bank.Key,
                            "Value": x.Bank.Value,
                        });
                    });

                    if ($scope.LookUps.BankName.length == 1) {
                        $scope.LookUps.BankName.forEach(x => {
                            model.Bank = { Key: x.Key, Value: x.Value }
                        });

                        $scope.FilterPayerDetails($scope.SchoolBankPayerDetails, model.Bank.Key);
                    }
                    else {
                        hideOverlay();
                        return false;
                    }
                } else {
                    hideOverlay();
                    return false;
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.BankChanges = function ($event, $element, model) {
        showOverlay();

        if (model.Bank.Key != null || model.Bank.Key != undefined) {
            if ($scope.SchoolBankPayerDetails.length > 0) {
                $scope.FilterPayerDetails($scope.SchoolBankPayerDetails, model.Bank.Key);
            }
        }
        hideOverlay();
    }

    $scope.FilterPayerDetails = function (datas, bankID) {

        var finalData = datas.filter(f => f.BankID == bankID);

        $scope.CRUDModel.ViewModel = finalData[0];
        $scope.CRUDModel.ViewModel.FileCreationDateString = $scope.CurrentDate;
    }

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);
