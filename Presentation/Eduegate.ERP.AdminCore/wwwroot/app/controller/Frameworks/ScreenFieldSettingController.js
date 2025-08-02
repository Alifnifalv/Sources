app.controller('ScreenFieldSettingController', ['$scope', '$http', '$timeout', '$compile', function ($scope, $http, $timeout, $compile) {
    console.log('ScreenFieldSettingController controller loaded.');
    $scope.ViewModels = [];
    $scope.Properties = [
        {
            'IsDirty': false, 'ValueType': 'String', 'Description': 'Format', SettingValue : ''
        }, {
            'IsDirty': false, 'ValueType': 'Bool', 'Description': 'Upper Case', SettingValue: ''
        },
        {
            'IsDirty': false, 'ValueType': 'String', 'Description': 'Prefix', SettingValue: ''
        }

    ];
    var windowContainer = null;
    $scope.SelectedView = null;

    $scope.Init = function (model, window) {
        windowContainer = '#' + window;
        $scope.ViewModel = model;

        $.ajax({
            url: "Settings/Setting/GetViews",
            type: "GET",
            success: function (result) {
                $scope.$apply(function () { $scope.ViewModels = result; });
                $('.preload-overlay', $(windowContainer)).fadeOut(500);
            }
        });
    };

    $scope.LoadScreen = function() {
        $.ajax({
            url: 'Frameworks/CRUD/CreateMaster?screen=' + $scope.SelectedView,
            type: 'GET',
            success: function (result) {
                result = result.replace('ng-controller', 'ng-controller-invalid');
                $('#ScreenLayout').html(result);    
                $('#ScreenLayout .windowcontainer').slideDown('slow');
                $('#ScreenLayout .windowcontainer .preloader').hide();
                $('#ScreenLayout .windowcontainer .preload-overlay').hide();
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText);
            }
        });
    };

    $scope.SaveChanges = function (setting) {

        if (!setting.IsDirty) return;

        $('.preload-overlay', $(windowContainer)).fadeIn(500);

        $.ajax({
            url: "Settings/Setting/SaveSettings",
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