app.controller("ExpenditureAllocationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("ExpenditureAllocationController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.updateDebitCredit = function (model) {
        if (model.Debit == undefined || model.Debit == null  ) {
            model.Debit = 0;
        }
        if (model.Credit == undefined || model.Credit == null) {
            model.Credit = 0;
        }
    };

    $scope.checkAmountEqual = function (gridModel,gridModelExpenditureAllocTransactionAlloc,) {
        if ($scope.CRUDModel.Model.DetailViewModel.length > 1) {
            let sum = $scope.CRUDModel.Model.DetailViewModel.slice(1) // Exclude the first row
                .reduce((total, row) => total + ((row.Debit || 0) - (row.Credit || 0)), 0);

            $scope.CRUDModel.Model.DetailViewModel[0].Debit = Math.abs(sum);
            $scope.CRUDModel.Model.DetailViewModel[0].Credit = 0;
            $scope.CRUDModel.Model.DetailViewModel[0].Amount = (-1 * parseFloat($scope.CRUDModel.Model.DetailViewModel[0].Credit)) + parseFloat($scope.CRUDModel.Model.DetailViewModel[0].Debit);
        }
    };

    $scope.checkSumAmount = function (parentrow, index) {


        if (parentrow.ExpenditureAllocTransactionAlloc.length > 0) {
            let sum = parentrow.ExpenditureAllocTransactionAlloc.slice(1) 
                .reduce((total, row) => total + ((row.DebitAmount || 0) - (row.CreditAmount || 0)), 0);

            if (sum > (parentrow.Debit || 0) - (parentrow.Credit || 0)) {

                $().showMessage($scope, $timeout, true, "The amount exceeded the total limit!");
                parentrow.ExpenditureAllocTransactionAlloc[index].Credit = 0;
                parentrow.ExpenditureAllocTransactionAlloc[index].Debit = 0;
                return false;
            }            
        }
     
    };

}]);