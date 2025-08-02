
app.controller("LoanApprovalListController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    console.log("LoanApprovalListController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.ApproveLoanRequest = function (loanRequestID, loanHeadID) {


        var windowName = 'LoanApproval';
        var viewName = 'Loan/Advance Approval';
       
        if (loanRequestID) {
            showOverlay();         

            if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
                return;

            $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
            editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + loanRequestID;

            $http({ method: 'Get', url: editUrl })
                .then(function (result) {
                    $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                    $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
                });

        }
        else {
            hideOverlay();
        }
    };
}]);
