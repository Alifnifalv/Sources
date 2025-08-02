app.controller("EmployeeSettlementController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    console.log("EmployeeSettlementController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });



    $scope.EmployeeChanges = function ($event, $element, gridModel) {
        showOverlay();
        var model = gridModel;
        var url = "Payroll/Employee/GetEmployeeSettlementDetails?Employee=" + model.Employee.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.SalaryComponents = result.data;
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
