app.controller("BookIssueReturnController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("BookIssueReturnController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.GetBookDetails = function (model) {
        showOverlay();

        var url = "Schools/School/GetIssuedBookDetails?CallAccNo=" + model.CallAccNo + "&bookMapID=" + model.Book?.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.BookIssueDetails = result.data.BookIssueDetails;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        var url = "Schools/School/GetBookQuantityDetails?CallAccNo=" + model.CallAccNo + "&bookMapID=" + model.Book?.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.AvailableBookQty = result.data.AvailableBookQty;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        var url = "Schools/School/GetBookDetailsByCallNo?CallAccNo=" + model.CallAccNo;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.LibraryBooks = result.data;
            }, function () {
                hideOverlay();
            });
    };

    //$scope.BookValueChange = function ($event, $element, model) {
    //    showOverlay();
    //    var url = "Schools/School/GetBookDetailsChange?BookID=" + model.Book.Key;
    //    $http({ method: 'Get', url: url })
    //        .then(function (result) {
    //            model.Acc_No = result.data.Acc_No;
    //            model.Call_No = result.data.Call_No;
    //            hideOverlay();
    //        }, function () {
    //            hideOverlay();
    //        });
    //};


    $scope.BookChanges = function ($event, $element, model) {
        showOverlay();

        if (model.CallAccNo != undefined && model.CallAccNo != "" && model.CallAccNo != null) {
            hideOverlay();
            return false
        }

        var url = "Schools/School/GetIssuedBookDetails?CallAccNo=" + model.CallAccNo + "&bookMapID=" + model.Book?.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.BookIssueDetails = result.data.BookIssueDetails;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        var url = "Schools/School/GetBookQuantityDetails?CallAccNo=" + model.CallAccNo + "&bookMapID=" + model.Book?.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                model.AvailableBookQty = result.data.AvailableBookQty;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };


    //Function for LibraryTransaction View from view screen
    $scope.ViewLibraryTransaction = function (libraryTransactionIID) {
        var windowName = 'LibraryTransaction';
        var viewName = 'Library Transaction';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + libraryTransactionIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
            });
    };

}]);