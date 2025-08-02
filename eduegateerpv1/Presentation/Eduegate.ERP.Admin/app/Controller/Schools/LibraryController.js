app.controller("LibraryController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.GetBooKCategoryName = function ($event, $element, dueModel) {
        showOverlay();
        dueModel.BookCategoryName = "";
        var model = dueModel;
        var url = "Schools/School/GetBooKCategoryName?bookCategoryCodeId=" + model.LibraryBookCategoryCode.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                
                $scope.CRUDModel.ViewModel.BookCategoryName = result.data.BookCategoryName;
                $scope.CRUDModel.ViewModel.BookCode = result.data.BookCode;
                $scope.CRUDModel.ViewModel.BookCodeSequenceNo = result.data.BookCodeSequenceNo;
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