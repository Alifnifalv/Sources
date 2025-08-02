app.controller("SerialKeyTrackingController", ["$scope", "$compile", "$http", "$timeout", "productService", "purchaseorderService", "accountService", "$controller",
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller) {
        console.log("SerialKeyTrackingController");

        angular.extend(this, $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, productService: productService, purchaseorderService: purchaseorderService, accountService: accountService }));

        $scope.SearchTransactions = function (SerialNo, IsDigital) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
            $http({ method: 'GET', url: "Transaction/GetAllTransactionsBySerialKey?serialKey=" + SerialNo + "&IsDigital=" + IsDigital })
                .then(function (result) {
                    console.log("Trasaction list loaded successfully.");
                    $scope.CRUDModel.ViewModel.Transactions = result.data;
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                });
        };

        $scope.ResetSerialNo = function (SerialNo) {
            $scope.SerialNo = SerialNo;
            $scope.CRUDModel.ViewModel.SerialNo = null;
            $scope.CRUDModel.ViewModel.Transactions = null;
        };


        $scope.GetKeysEncryptDecrypt = function (SerialNos, IsEncrypted) {
            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
            $http({ method: 'GET', url: "Transaction/GetKeysEncryptDecrypt?serialKeys=" + SerialNos + "&IsEncrypted=" + IsEncrypted })
                .then(function (result) {
                    console.log("Trasaction list loaded successfully.");
                    $scope.CRUDModel.ViewModel.EncryptDecryptKeys.Values = result.data;
                    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
                });
        };
    }]);