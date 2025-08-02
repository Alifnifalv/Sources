app.controller('LeftMenuController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$rootScope", function ($scope, $http, $compile, $window, $location, $timeout, $root) {
    console.log('LeftMenu controller loaded.');

    $scope.FireEvent = function ($event, parameters, menuParameters, menu) {
        $event.stopPropagation();
        $event.preventDefault();
        var $menuEvent = $event;
        if (!parameters) {
            $timeout(function () {
                $scope.$apply(function () {
                    $scope.FilterMenu($menuEvent);
                    $timeout(function () {
                        ExpandMenu($('#menuLink_' + menu.MenuID));
                    });
                });
            });
        }
        else {
            $root.FireEvent($menuEvent, parameters, menuParameters);
        }
    }

    function ExpandMenu($target) {
        $target.toggleClass('open');
        if ($target.hasClass('open')) {
            $($target.closest('li').find('ul')[0]).slideDown('fast');
        }
        else {
            $target.removeClass('open');
            $($target.closest('li').find('ul')[0]).slideUp('fast');
        }
    }

    $scope.LoadReport = function (event, reportName, reportFullName) {
        $("html, body").animate({ scrollTop: 0 }, "fast");
        var reportHeader = $(event.currentTarget).find('a').attr('title');
        if ($scope.ShowWindow(reportName, reportHeader, reportName))
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var reportUrl = "http://localhost/Eduegate.Application.Reports?reportName=" + reportFullName;
        $('#' + windowName).html('<iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
        var $iFrame = $('iframe[reportname=' + reportName + ']');
        $("#Load", $('#' + windowName)).hide();
        $iFrame.ready(function () {
            $("#Load", $('#' + windowName)).hide();
        });
    };

    $scope.Create = function (view, event, title, windowName, menuParameters) {
        $root.Create(view, event, title, windowName, menuParameters);
    };

    $scope.List = function (view, event, title, menuParameters, isSortable = false) {
        $root.List(view, event, title, menuParameters, isSortable);
    };

    $scope.JobOperations = function (operationType) {
        if ($scope.ShowWindow("JobOperations" + operationType, "Job- " + operationType, "JobOperations" + operationType))
            return;

        $scope.AddWindow("JobOperations" + operationType, "Job- " + operationType, "JobOperations" + operationType);

        $http({ method: 'Get', url: "Warehouses/JobOperation/Operations?type=" + operationType })
            .then(function (result) {
                $('#JobOperations' + operationType, "#LayoutContentSection").replaceWith($compile(result.data)($scope));
                $scope.ShowWindow("JobOperations" + operationType, "Job- " + operationType, "JobOperations" + operationType);
            });
    };

    $scope.LoadScreen = function (event, url, name, title) {
        if (event != undefined) {
            event.preventDefault();
            if (event.stopPropagation) event.stopPropagation();
        }

        if ($(event.currentTarget).hasClass("brcolor") == true)
            return;

        $scope.AddWindow(name, title, name);

        $http({ method: 'Get', url: url })
            .then(function (result) {
                $('#' + name, "#LayoutContentSection").replaceWith($compile(result.data)($scope));
                $scope.ShowWindow(name, title, name);
            });
    };
}]);