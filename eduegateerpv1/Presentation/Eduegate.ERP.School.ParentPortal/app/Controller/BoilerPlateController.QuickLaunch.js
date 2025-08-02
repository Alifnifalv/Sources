app.controller("BoilerPlateQuickLanuchController", ["$scope", "$compile", "$http", "$timeout","$controller", function ($scope, $compile, $http, $timeout, $controller) {
    console.log("BoilerPlateQuickLanuchController");

    angular.extend(this, $controller('BoilerPlateController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout }));

    $scope.Create = function (view, event, title, windowName) {
        if (event != undefined) {
            event.preventDefault();
            if (event.stopPropagation) event.stopPropagation();
        }

        $("html, body").animate({ scrollTop: 0 }, "fast");

        if (title == undefined)
            title = 'Create ' + view;

        if ($scope.ShowWindow('Create' + windowName, title, 'Create' + windowName))
            return;

        $scope.AddWindow('Create' + windowName, title, 'Create' + windowName);

        var createUrl = view;

        $.ajax({
            url: createUrl,
            type: 'GET',
            success: function (result) {
                $('#Create' + windowName, "#LayoutContentSection").replaceWith($compile(result)($scope)).updateValidation();
                $scope.ShowWindow('Create' + windowName, title, 'Create' + windowName);
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText);
            }
        })
    }

    $scope.List = function (view, event, title) {
        if (event != undefined) {
            event.preventDefault();
            if (event.stopPropagation) event.stopPropagation();
        }

        $("html, body").animate({ scrollTop: 0 }, "fast");
        var windowName = view.substring(view.indexOf('/') + 1);

        if (title == undefined)
            title = view + " Lists";

        if ($scope.ShowWindow(windowName + "Lists", view + " Lists", windowName + "lists"))
            return;

        $scope.AddWindow(windowName + "Lists", title, windowName + "Lists");

        var createUrl = "Frameworks/Search/List?view=" + windowName;
        $.ajax({
            url: createUrl,
            type: 'GET',
            success: function (result) {
                $('#' + windowName + "Lists", "#LayoutContentSection").replaceWith($compile(result)($scope));
                $scope.ShowWindow(windowName + "Lists", view + " Lists", windowName + "lists");
            }
        })
    }

}]);