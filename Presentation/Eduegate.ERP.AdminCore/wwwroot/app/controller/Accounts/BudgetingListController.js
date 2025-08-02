app.controller("BudgetingListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("BudgetingListController Loaded");
    $controller('ViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });
  
    $scope.Budgets = [];
    $scope.AccountGroups = [];
    $scope.BudgetTypes = [];
    $scope.BudgetSuggestions = [];
    $scope.CostCenters = [];

   
    $scope.BudgetEntries = [];

    function showOverlay() {
        $('.preload-overlay', $('#Budgeting')).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $('#Budgeting')).hide();
    }
    $scope.EditForcasting = function (budgetID) {

        if (budgetID) {
            showOverlay();
            var url = utility.myHost + "Accounts/Forecasting/Forecasting?budgetID=" + budgetID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    hideOverlay();
                    $scope.AddWindow('Forecasting', 'Forecasting', 'Forecasting');

                    $('#' + 'Forecasting', '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow('Forecasting', 'Forecasting', 'Forecasting');

                }, function () {
                    hideOverlay();
                });
        }
        else {
            hideOverlay();
        }
    };
    $scope.EditBudgeting = function (budgetID) {

        if (budgetID) {
            showOverlay();
            var url = utility.myHost + "Accounts/Budgeting/Budgeting?budgetID=" + budgetID;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    hideOverlay();
                    $scope.AddWindow('Budgeting', 'Budgeting', 'Budgeting');

                    $('#' + 'Budgeting', '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow('Budgeting', 'Budgeting', 'Budgeting');
                   
                }, function () {
                    hideOverlay();
                });
        }
        else {
            hideOverlay();
        }
    };
    }]);