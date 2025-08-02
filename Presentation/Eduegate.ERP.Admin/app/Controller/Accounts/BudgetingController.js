app.controller("BudgetingController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("BudgetingController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.BudgetingData = [];

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.FillBudgetingData = function () {

        var acountGroup = $scope.CRUDModel.ViewModel.AccountGroup;

        if (acountGroup == undefined || acountGroup.length < 0) {
            $().showMessage($scope, $timeout, true, "Please select Account Group!");

            return false;
        }
        showOverlay();
        $scope.BudgetingData = [];
        $scope.AccountGroupKeys = [];
        $scope.CRUDModel.ViewModel.AccountGroup.forEach(x => {
            $scope.AccountGroupKeys.push(
                {
                    Key: x.Key,
                    Value: x.Value
                });
        });
        
        //var dayIDs = Object.keys($scope.CRUDModel.ViewModel.AccountGroup);
        //var DaysID = dayIDs.toString();

        $.ajax({

            url: "Accounts/Budgeting/GetBudgetingData",
            type: "POST",
            data: {

                "AccountGroup": $scope.AccountGroupKeys
            },
            success: function (result) {
                if (result != null) {

                    result.forEach(x => {
                        $scope.BudgetingData.push(
                            {
                                AccountGroupID: x.AccountGroupID,
                                AccountGroup: x.AccountGroup,
                                BudgetingAccountDetail: x.BudgetingAccountDetail != null ? x.BudgetingAccountDetail.map(y => y) : null

                            }
                        );

                    });
                    $scope.CRUDModel.ViewModel.BudgetingDetail = $scope.BudgetingData;
                }
            },
            complete: function (result) {

                hideOverlay();
            }
        });

    }



}]);