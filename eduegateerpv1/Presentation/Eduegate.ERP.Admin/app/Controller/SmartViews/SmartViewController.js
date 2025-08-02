app.controller('SmartViewController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$rootScope", function ($scope, $http, $compile, $window, $location, $timeout, $root) {
    console.log('SmartViewController controller loaded.');

    $scope.Init = function (model) {
    }

    $scope.LoadReport = function (event, reportName, reportFullName) {
        $("html, body").animate({ scrollTop: 0 }, "fast");
        var reportHeader = $(event.currentTarget).find('a').attr('title');
        if ($scope.ShowWindow(reportName, reportHeader, reportName))
            return;

        var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

        var reportUrl = "Home/ViewReports?reportName=" + reportFullName;
        $('#' + windowName).html('<iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
        var $iFrame = $('iframe[reportname=' + reportName + ']');
        $("#Load", $('#' + windowName)).hide();
        $iFrame.ready(function () {
            $("#Load", $('#' + windowName)).hide();
        });
    }

    $scope.FireEvent = function ($event, parameters, menuParameters) {
        if (parameters == null) return;

        var params = parameters.split(',');

        switch (params[0]) {
            case 'Create':
                $scope.Create(params[1].trim(), $event, params[3].trim(), params[4].trim(), menuParameters);
                break;
            case 'List':
                var runtimeParam;

                if (params[5]) {
                    runtimeParam = params[5];
                }

                $scope.List(params[1].trim(), $event, params[3].trim(), runtimeParam);
                break;
            case 'Report':
            case 'LoadReport':
                $scope.LoadReport($event, params[2].trim(), params[3].trim());
                break;
            case 'JobOperations':
                $scope.JobOperations(params[1].trim());
                break;
            case 'Load':
                $scope.LoadScreen($event, params[1].trim(), params[3].trim(), params[4].trim());
                break;
            case 'LoadHome':
                break;
        }
    }

    $scope.Create = function (view, event, title, windowName, menuParameters) {
        //var activeitem = $('ul.bodyrightmain-tab').find('li.active').position().left;
        //$('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
        if (event != undefined) {
            event.preventDefault();
            if (event.stopPropagation) event.stopPropagation();
        }

        $("html, body").animate({ scrollTop: 0 }, "fast");

        if (windowName == "SKU") {
            windowName = "Product";
        }

        if (title == undefined)
            title = 'Create ' + view;

        if ($scope.ShowWindow('Create' + windowName, title, 'Create' + windowName))
            return;

        $scope.AddWindow('Create' + windowName, title, 'Create' + windowName);

        var createUrl;

        if (menuParameters != null) {
            if (view.includes("?"))
                createUrl = view + '&parameters=' + menuParameters;
            else
                createUrl = view + '?parameters=' + menuParameters;
        }
        else
            createUrl = view;

        $.ajax({
            url: createUrl,
            type: 'GET',
            success: function (result) {
                $('#Create' + windowName, "#LayoutContentSection").replaceWith($compile(result)($scope)).updateValidation();
                $scope.ShowWindow('Create' + windowName, title, 'Create' + windowName);
                var topmenucontainer = $("body").find(".topmenuwrap-inner").outerWidth();
                var itemwidth = 0;
                $('ul.bodyrightmain-tab').children().each(function () {
                    itemwidth += $(this).outerWidth();
                });
                if (itemwidth > topmenucontainer) {
                    $('.btncontrols-wrap').show();
                }
                else {
                    $('.btncontrols-wrap').hide();
                }
                //var activeitem = $('ul.bodyrightmain-tab').find('li.active').position().left;
                //$('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText);
            }
        });
    }

    $scope.List = function (view, event, title, menuParameters) {
        //var activeitem = $('ul.bodyrightmain-tab').find('li.active').position().left;
        //$('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });

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

        if (menuParameters != null)
            createUrl = "Frameworks/Search/List?view=" + windowName + '&parameters=' + menuParameters;
        else
            createUrl = "Frameworks/Search/List?view=" + windowName;

        $.ajax({
            url: createUrl,
            type: 'GET',
            success: function (result) {
                $('#' + windowName + "Lists", "#LayoutContentSection").replaceWith($compile(result)($scope));
                $scope.ShowWindow(windowName + "Lists", view + " Lists", windowName + "lists");
                var topmenucontainer = $("body").find(".topmenuwrap-inner").outerWidth();
                var itemwidth = 0;
                //$('ul.bodyrightmain-tab').children().each(function () {
                //    itemwidth += $(this).outerWidth();
                //    // change to .outerWidth(true) if you want to calculate margin too.
                //});
                if (itemwidth > topmenucontainer) {
                    $('.btncontrols-wrap').show();
                }
                else {
                    $('.btncontrols-wrap').hide();
                }
                //var activeitem = $('ul.bodyrightmain-tab').find('li.active').position().left;
                //$('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
            }
        })
    }

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
                $scope.ShowWindow(name, title, name)
            });
    };
}]);