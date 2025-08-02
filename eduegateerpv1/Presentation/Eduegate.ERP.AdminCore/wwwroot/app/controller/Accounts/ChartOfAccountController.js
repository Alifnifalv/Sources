app.controller("ChartOfAccountController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {
    console.log("ChartOfAccountController log Loaded");
    $scope.GetChartOfAccounts = {};
    $scope.CurrentStatus = {};
    $scope.MainAccountGroup = [];
    $scope.SubGroup = [];
    $scope.Ledger = [];
    $scope.LedgerCode = [];
    $scope.Sections = [];
    $scope.WeekDay = [];
    $scope.IsApprovedUsers = false;

    //Initializing the pos view model
    $scope.Init = function (model, windowname) {
        $scope.GetChartOfAccounts();

        $http({
            method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=MainGroup&defaultBlank=false'
        }).then(function (result) {
            $scope.MainAccountGroup = result.data;
        });

        //SubGroup
        //$http({
        //    method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=SubGroup&defaultBlank=false",
        //}).then(function (result) {
        //    $scope.SubGroup = result.data;
        //});

        ////LedgerAccount
        //$http({
        //    method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=LedgerAccount&defaultBlank=false",
        //}).then(function (result) {
        //    $scope.LedgerName = result.data;
        //});

        ////LedgerCode
        //$http({
        //    method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=LedgerAccount&defaultBlank=false",
        //}).then(function (result) {
        //    $scope.LedgerCodeData = result.data;
        //});
    };


    $scope.GetChartOfAccounts = function () {
        $.ajax({
            type: "GET",
            url: "Accounts/ChartOfAccount/GetChartOfAccount",
            success: function (result) {
                $scope.$apply(function () {
                    $scope.GetChartOfAccounts = result;
                });
            }
        });
    }

    $scope.ResetSaveFilter = function () {
        $('.preload-overlay').show();
        $('.preloader').show();

        $.ajax({
            type: "POST",
            url: 'Mutual/ResetFilter',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify($scope.Model),
            success: success,
        });
    };

    $scope.MainGroupChanges = function (mainGroup) {
        showOverlay();
        var url = "Schools/School/GetSubGroup?mainGroupID=" + mainGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.SubGroup = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SubGroupChanges = function (subGroup) {
        showOverlay();
        var url = "Schools/School/GetAccountGroup?subGroupID=" + subGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.Ledger = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.LedgerNameChanges = function (ledgerGroup) {
        showOverlay();
        var url = "Schools/School/GetAccountCodeByLedger?ledgerGroupID=" + ledgerGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LedgerCode = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);