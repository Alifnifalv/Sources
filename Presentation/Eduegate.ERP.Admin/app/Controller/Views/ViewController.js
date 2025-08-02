
app.controller("ViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("List Controller");
    $scope.SelectedIds = [];
    $scope.SelectedRow = null;
    $scope.SelectedRowIndex = null;
    $scope.SelectedIID = null;

    $scope.Message = null;
    $scope.IsError = false;
    $scope.WindowContainer = null;

    $scope.ChildInit = function (model, ViewIID, windowID) {
        $scope.WindowContainer = windowID;
    }

    $scope.BringQuickFilter = function () {
        $('.headerFilter', $('#' + $scope.WindowContainer)).slideToggle('fast');
    }

    $scope.ChangeJobProfileStatus = function (event) {
        $http({ method: 'Get', url: 'HR/JobProfile/MoveToArchive?id=' + IID })
            .then(function (result) {
                $().showGlobalMessage($scope, $timeout, false, "Applied job moved to archived.");
            });
    };


    $scope.AllocateInvoices = function (event, allocationType) {
        if ($scope.SelectedIID === null) {
            $().showGlobalMessage($scope, $timeout, true, "Please select a voucher for selection.");
            return;
        }

        var name = allocationType === "Receipt" ? "EditRVInvoiceAllocation" : "EditPVInvoiceAllocation";
        var title = allocationType + " Allocations";
        $scope.AddWindow(name, title, name);
        var url = 'Accounts/' + allocationType + 'Allocation/Allocate?invoiceIDs=' + $scope.SelectedIID;

        $http({ method: 'Get', url: url })
            .then(function (result) {
                $('#' + name, "#LayoutContentSection").replaceWith($compile(result.data)($scope));
                $scope.ShowWindow(name, title, name);
            });       
    };
}]);