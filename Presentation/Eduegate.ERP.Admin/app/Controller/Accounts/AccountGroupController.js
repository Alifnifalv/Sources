app.controller("AccountGroupController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.ParentGroupChanges = function ($event, $element, groupModel) {
        showOverlay();
        var model = groupModel;
        var url = "Schools/School/GetGroupCodeByParentGroup?ParentGroupID=" + model.ParentGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.GroupCode = result.data.GroupCode;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.AccountGroupChanges = function ($event, $element, groupModel) {
        showOverlay();
        var model = groupModel;
        var url = "Schools/School/GetAccountCodeByGroup?GroupID=" + model.Group.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.AccountCode = result.data.AccountCode;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.MainGroupChanges = function ($event, $element, groupModel) {
        showOverlay();
        var model = groupModel;
        model.SubGroup = null;
        var url = "Schools/School/GetSubGroup?mainGroupID=" + model.MainGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.SubGroup = result.data;
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.SubGroupChanges = function ($event, $element, groupModel) {
        showOverlay();
        var model = groupModel;
        model.Group = null;
        var url = "Schools/School/GetAccountGroup?subGroupID=" + model.SubGroup.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.AccountGroup = result.data;
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