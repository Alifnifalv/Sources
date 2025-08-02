app.controller("UserDeviceMapController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
/*    console.log("UserDeviceMapController Loaded");*/

    //angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.FillUserDeviceMap = function (UserDeviceMapIID) {
        var windowName = 'UserDeviceMap';
        var viewName = 'User Device Map';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + UserDeviceMapIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
            });

    };

}]);