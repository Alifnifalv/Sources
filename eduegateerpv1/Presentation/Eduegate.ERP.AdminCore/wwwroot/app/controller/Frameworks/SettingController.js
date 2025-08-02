app.controller('SettingController', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {
    console.log('SettingController controller loaded.');
    $scope.SettingModel = [];
    var windowContainer = null;

    $scope.Init = function (model, window) {
        windowContainer = '#' + window;
        $scope.SettingModel = model;

        $.ajax({
            url: "/Settings/Setting/GetSettings",
            type: "GET",
            success: function (result) {
                $scope.$apply(function () { $scope.SettingModel = result; });
                $('.preload-overlay', $(windowContainer)).fadeOut(500);
            }
        });
    };

    $scope.TitleClick = function (event) {
        $(event.target).toggleClass('active');
        if ($(event.target).hasClass('active')) {
            $(event.target).next('.sublist-grid').slideDown("fast");
        }
        else {
            $(event.target).next('.sublist-grid').slideUp("fast");
        }
    };

    $scope.SaveChanges = function (setting) {

        if (!setting.IsDirty) return;

        $('.preload-overlay', $(windowContainer)).fadeIn(500);

        $.ajax({
            url: "/Settings/Setting/SaveSettings",
            type: "POST",
            data: setting,
            success: function (result) {
                setting.IsDirty = false;
                $().showMessage($scope, $timeout, false, "Updated the settings sucessfully.");
                $('.preload-overlay', $(windowContainer)).fadeOut(500);
            }
        });
    };
}]);