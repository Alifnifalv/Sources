app.controller("ViewControllerDocManagement", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller)
{
    console.log("ViewControllerDocManagement");
    var IID = null;
    var windowContainer = null;
    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $scope.init = function (model, ViewIID, windowName) {
        windowContainer = '#' + windowName;
    }

    $scope.ReloadGrid = function () {
        $(".reload-the-grid-data", $(windowContainer)).trigger("click");
    }

    $scope.UploadDocument = function (event) {
        event.stopPropagation();

        var yposition = event.pageY - 134;
        $('.popup.gridpopupfields', $(windowContainer)).slideDown("fast");
        $('.popup.gridpopupfields', $(windowContainer)).css({ 'top': yposition });
        $('.transparent-overlay', $(windowContainer)).show();
        $('#popupContainer', $(windowContainer)).html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        $.ajax({
            url: 'Documents/DocManagement/Upload',
            type: 'GET',
            success: function (content) {
                $('#popupContainer', $(windowContainer)).html($compile(content)($scope));
                $('#popupContainer', $(windowContainer)).removeClass('loading');
                $('#popupContainer', $(windowContainer)).addClass('loaded');                
            }
        });
    }

    $scope.CloseUploadWindow = function (event) {
        $('.popup.gridpopupfields', $(windowContainer)).slideUp("fast");
    }
}]);