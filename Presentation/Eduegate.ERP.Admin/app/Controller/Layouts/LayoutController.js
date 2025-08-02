app.controller('LayoutController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", "$timeout",
    function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
        console.log('LayoutController controller loaded.');

        $scope.WindowTabs = [];
        $scope.SelectedWindowIndex = 0;
        $scope.AlertCount = 0;
        $scope.MenuItems = [];
        var _menuItemsCache = [];
        $root.MenuSearchText = "";
        $scope.layout = null;
        $scope.WindowCount = 0;

        $subscription.subscribe(
            { 'subscribeTo': 'alertcount', 'componentID': 'layout', 'container': '' },
            SetAlertCount
        );

        function SetAlertCount(count) {
            $scope.$apply(function () {
                $scope.AlertCount = count;
            });
        };

        $scope.ShowClose = function (index) {
            if (index == 0) return false;
            return true;
        };

        $scope.Init = function (layout, dashboardTitle) {
            $scope.layout = layout;
            $scope.WindowTabs.push({ Index: 0, Name: 'Dashbaord', Title: dashboardTitle || 'Dashboard', Container: 'DashbaordWindow' });
            //load menu tree structure
            if ($scope.layout == 'smart') {
                loadMenuFlatStructure();
            }
            else {
                loadMenuTreeStructure();
            }
        };

        function loadMenuFlatStructure() {
            $.ajax({
                url: "Mutual/GetERPMenuFlat?menuLinkType=ERPNavigation",
                type: "GET",
                success: function (result) {
                    if (!result != null) {
                        angular.copy(result, _menuItemsCache);
                        var ordered = _.sortBy(result, 'MenuGroup');
                        var menuItems = _.groupBy(ordered, 'MenuGroup');
                        $scope.MenuItems = menuItems;
                    }
                }
            });
        }

        function loadMenuTreeStructure() {
            $.ajax({
                url: "Mutual/GetERPMenu?menuLinkType=ERPNavigation",
                type: "GET",
                success: function (result) {
                    if (!result != null) {
                        angular.copy(result.MenuItems, _menuItemsCache);
                        $scope.$apply(function () {
                            $scope.MenuItems = result.MenuItems;
                        });
                    }
                }
            });
        }

        var previousSearch = null;

        $scope.FilterMenu = function (event) {
            $timeout(function () {
                if (previousSearch != $root.MenuSearchText) {
                    var cloneMenu = JSON.parse(JSON.stringify(_menuItemsCache));
                    var filteredMenu = filterMenuData(cloneMenu);
                    $scope.MenuItems = []
                    $.each(filteredMenu, function (index, value) {
                        $scope.MenuItems.push(value);
                    });
                    previousSearch = $root.MenuSearchText;
                }
            });
        };

        function filterMenuData(data) {
            var r = data.filter(function (o) {
                if (o.SubItems) o.SubItems = filterMenuData(o.SubItems);
                return o.MenuName.toUpperCase().indexOf($root.MenuSearchText.toUpperCase()) >= 0 ||
                    o.Hierarchy.toUpperCase().indexOf($root.MenuSearchText.toUpperCase()) >= 0;
            });
            return r;
        }

        $scope.ShowWindow = function (name, title, container) {
            var item = $.grep($scope.WindowTabs, function (e) { return e.Name == name; });
            if (item.length === 0)
                return false;
            else {
                $scope.SelectedWindowIndex = item[0].Index;
                var itemContainer = $("#" + item[0].Container);
                itemContainer.attr('windowindex', $scope.SelectedWindowIndex);
                $(".windowcontainer").slideUp(100).removeClass('active');
                $("#" + item[0].Container).slideDown(100).addClass('active');
                return true;
            }
        };

        $scope.AddWindow = function (name, title, container) {
            $scope.WindowCount = $scope.WindowCount + 1;
            var item = $.grep($scope.WindowTabs, function (e) { return e.Name == name; });

            if (item.length == 0) {
                $scope.WindowTabs.push({ Index: $scope.WindowTabs.length, Name: name, Title: title, Container: container });
                $scope.SelectedWindowIndex = $scope.WindowTabs.length - 1;
            }
            else {
                $scope.SelectedWindowIndex = item[0].Index;
            }

            item = $.grep($scope.WindowTabs, function (e) { return e.Name === name; });
            $(".windowcontainer").hide().removeClass('active');
            container = $("#" + item[0].Container);
            container.attr('windowindex', $scope.SelectedWindowIndex);

            if (container.length == 0) {
                $("#LayoutContentSection").append("<div id='" + item[0].Container + "' class='windowcontainer active' style='display:block;color:white;'><center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>");
            }

            container.slideDown('slow');
            return item[0].Container;
        };

        $scope.SelectWindowTab = function (event, index) {
            if ($scope.SelectedWindowIndex == index) return;
            $scope.SelectedWindowIndex = index;
            $(".windowcontainer").hide().removeClass('active');
            $("#" + $scope.WindowTabs[index].Container).show().addClass('active');
        };

        $scope.CloseWindowTab = function (index) {

            if (index >= $scope.WindowTabs.length) {
                index = $scope.WindowTabs.length - 1;
            }

            var window = $('#' + $scope.WindowTabs[index].Container);
            window.hide();
            window.html('');
            window.remove();
            $scope.WindowTabs.splice(index, 1);

            if ($scope.SelectedWindowIndex >= index)
                $scope.SelectWindowTab(null, $scope.SelectedWindowIndex - 1);

            var topmenucontainer = $("body").find(".topmenuwrap-inner").outerWidth();
            var itemwidth = 0;
            $('ul.bodyrightmain-tab').children().each(function () {
                itemwidth += $(this).outerWidth();
                // change to .outerWidth(true) if you want to calculate margin too.
            });
            if (itemwidth < topmenucontainer) {
                $('.btncontrols-wrap').hide();
            }

            $scope.WindowCount = $scope.WindowCount - 1;
        };

        $scope.CloseSmartWindow = function (event) {
            $scope.CloseWindowTab($scope.WindowCount);
        };

        $scope.CloseWindow = function (event) {
            var window = $(event.currentTarget).closest('.windowcontainer');
            $scope.CloseWindowTab(window.attr('windowindex'));
            window.removeClass('active');
        };

        $scope.ChangePassword = function (event) {
            event.stopPropagation();
            var offs = $('.header').offset();
            var yposition = event.pageY - offs.top;
            $('.transparent-overlay').show();
            $('.popup.gridpopupfields').slideDown("fast");
            $('.popup.gridpopupfields').addClass('fixedpos');
            $('.popup.gridpopupfields').css({ 'top': yposition });
            $('.myprofile-dropdown').slideUp('fast');


            $('#globalPopupContainer').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                url: "Account/ResetPassword",
                type: 'GET',
                success: function (content) {
                    $('#globalPopupContainer').html($compile(content)($scope));
                    $('#globalPopupContainer').removeClass('loading');
                    $('#globalPopupContainer').addClass('loaded');
                }
            });
        };


        $scope.ClosePopup = function (event) {
            $(event.currentTarget).hide();
            $('.popup.gridpopupfields').fadeOut("fast");
            setTimeout(function (e) {
                $('.popup.gridpopupfields').css('top', '');
                $('.popup.gridpopupfields').removeClass('fixedpos');
            }, 200);
            $('.popup.gridpopupfields', $(windowContainer)).removeAttr('data-list');
            $('#popupContainer', $(windowContainer)).html('');
            //$('.statusview').removeClass('active');
        };

        $root.FireEvent = function ($event, parameters, menuParameters) {
            if (parameters == null) return;

            var params = parameters.split(',');

            switch (params[0]) {
                case 'Create':
                    $scope.Create(params[1].trim(), $event, params[3].trim(), params[4].trim(), menuParameters);
                    break;
                case 'SortableList':
                    {
                        var runtimeParam;

                        if (params[5]) {
                            runtimeParam = params[5];
                        }

                        $scope.List(params[1].trim(), $event, params[3].trim(), runtimeParam, true);
                    }
                    break;
                case 'List':
                    {
                        var runtimeParam2;

                        if (params[5]) {
                            runtimeParam2 = params[5];
                        }

                        $scope.List(params[1].trim(), $event, params[3].trim(), runtimeParam2);
                    }
                    break;
                case 'Report':
                case 'LoadReport':
                    $root.LoadReport($event, params[2].trim(), params[3].trim(), params[4] ? params[4].trim() : null);
                    break;
                case 'JobOperations':
                    $scope.JobOperations(params[1].trim());
                    break;
                case 'Load':
                    $root.LoadScreen($event, params[1].trim(), params[3].trim(), params[4].trim());
                    break;
                case 'LoadHome':
                    $scope.SelectWindowTab(null, 0);
                    break;
            }
        }

        $root.LoadReport = function (event, reportName, reportFullName, reportTitle) {
            $("html, body").animate({ scrollTop: 0 }, "fast");
            var reportHeader = reportTitle ? reportTitle : $(event.currentTarget).find('a').attr('title');
            if ($scope.ShowWindow(reportName, reportHeader, reportName))
                return;

            var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

            var reportUrl = "Home/ViewReports?reportName=" + reportFullName;
            var loadContent = "<center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>";
            $('#' + windowName).html(loadContent + '<iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
            var $iFrame = $('iframe[reportname=' + reportName + ']');
            $iFrame.ready(function () {
                setTimeout(function () {
                    $("#Load", $('#' + windowName)).hide();
                },1000);
            });
        };

        $root.LoadScreen = function (event, url, name, title) {
            if (event != undefined) {
                event.preventDefault();
                if (event.stopPropagation) event.stopPropagation();
            }

            if ($(event.currentTarget).hasClass('brcolor') == true) { return; }

            $scope.AddWindow(name, title, name);

            $http({ method: 'Get', url })
                .then((result) => {
                    $('#' + name, '#LayoutContentSection').replaceWith($compile(result.data)($scope));
                    $scope.ShowWindow(name, title, name);
                });
        };

        $root.Create = function (view, event, title, windowName, menuParameters) {
            var activeitem = $('ul.bodyrightmain-tab').find('li.active')?.position()?.left;
            $('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
            if (event !== undefined) {
                event.preventDefault();
                if (event.stopPropagation) event.stopPropagation();
            }

            $("html, body").animate({ scrollTop: 0 }, "fast");

            if (title === undefined)
                title = 'Create ' + view.substring(view.indexOf('/') + 1);

            if ($scope.ShowWindow('Create' + windowName, title, 'Create' + windowName))
                return;

            $scope.AddWindow('Create' + windowName, title, 'Create' + windowName);

            var createUrl;

            if (menuParameters !== null)
                if (view.includes("?"))
                    createUrl = view + '&parameters=' + menuParameters;
                else
                    createUrl = view + '?parameters=' + menuParameters;
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

                    var activeitem = $('ul.bodyrightmain-tab').find('li.active')?.position()?.left;
                    $('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
                },
                error: function (request, status, message, b) {
                    $().showGlobalMessage($root, $timeout, true, request.responseText);
                }
            });
        };

        $root.List = function (view, event, title, menuParameters, isSortable = false) {
            var activeitem = $('ul.bodyrightmain-tab').find('li.active');

            if (activeitem && activeitem.length > 0) {
                activeitem.position().left;
            }

            $('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });

            if (event !== undefined) {
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

            if (isSortable) {
                if (menuParameters != null)
                    createUrl = "Frameworks/Search/List?view=" + windowName + '&parameters=' + menuParameters + "&isSortableList=true";
                else
                    createUrl = "Frameworks/Search/List?view=" + windowName + "&isSortableList=true";
            }
            else {
                if (menuParameters != null)
                    createUrl = "Frameworks/Search/List?view=" + windowName + '&parameters=' + menuParameters;
                else
                    createUrl = "Frameworks/Search/List?view=" + windowName;
            }

            $.ajax({
                url: createUrl,
                type: 'GET',
                success: function (result) {
                    $('#' + windowName + "Lists", "#LayoutContentSection").replaceWith($compile(result)($scope));
                    $scope.ShowWindow(windowName + "Lists", view + " Lists", windowName + "lists");
                    var topmenucontainer = $("body").find(".topmenuwrap-inner").outerWidth();
                    var itemwidth = 0;
                    $('ul.bodyrightmain-tab').children().each(function () {
                        itemwidth += $(this).outerWidth();
                        // change to .outerWidth(true) if you want to calculate margin too.
                    });
                    if (itemwidth > topmenucontainer) {
                        $('.btncontrols-wrap').show();
                    }
                    else {
                        $('.btncontrols-wrap').hide();
                    }

                    var activeitem = $('ul.bodyrightmain-tab').find('li.active')?.position()?.left;
                    $('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
                }
            });
        };

        $root.LoadCommunication = function (row, event, ID, screenID, container) {
            event.preventDefault();
            $("[data-original-title]").popover('dispose');
            $(".popover", container).hide();

            $(event.currentTarget).popover({
                placement: 'left',
                html: true
            }).on('show.bs.popover', function () {
                $(".overlaydiv").show();
            }).on('hide.bs.popover', function () {
                $(".overlaydiv").hide();
            });

            $(event.currentTarget).popover('show');
            $('#' + $(event.currentTarget).attr('aria-describedby'))
                .find('.popover-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

            $.ajax({
                url: 'Mutual/Communication?referenceID=' + (ID ? ID : row.LeadIID).toString() + '&screenID=' + screenID,
                type: 'GET',
                success: function (content) {
                    $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-body').html($compile(content)($scope));
                    $timeout(function () {
                        window.dispatchEvent(new Event('resize'));
                    });
                }
            });

        };

        $scope.successCallBack = null;
        $scope.cancelCallBack = null;

        $root.showConfirmationPopup = function (options) {
            var parameterOptions = {
                popupName: options.name || 'popup', popupTitle: options.title || 'Confirm',
                popupMessage: options.message,
                cancelbtnLabel: options.firstButtonLabel || 'Ok',
                confirmbtnLabel: options.secondButtonLabel || 'Cancel',
                cancelEvent: options.cancelEvent, confirmEvent: options.confirmEvent
            };

            $scope.successCallBack = parameterOptions.confirmEvent;
            $scope.cancelCallBack = parameterOptions.cancelEvent;

            $("body").append('<div class="modalPopup" popuptype="' + parameterOptions.popupName + '">' +
                '<div class="modalPopup__overlay" ng-click="removePopup()"></div>' +
                '<div class="modalPopup__Inner displayFlex flexColumn">' +
                '<div class="modalPopup__content displayFlex flexColumn">' + parameterOptions.popupMessage + '</div>' +
                '<div class="modalPopup__btns d-flex flexColumn justify-content-end">' +
                '</div>' +
                '</div>' +
                '</div>');

            if (parameterOptions.popupTitle) {
                $(".modalPopup__Inner").prepend('<div class="modalPopup__title displayFlex flexColumn">' + parameterOptions.popupTitle + '</div>');
            }
            if (parameterOptions.cancelbtnLabel) {
                $(".modalPopup__btns").append('<button class="button-orange medium me-3 secondary" ng-click="$root.removePopup()">' + parameterOptions.cancelbtnLabel + '</button>');
            }

            if (parameterOptions.confirmbtnLabel) {
                $(".modalPopup__btns").append('<button class="button-orange medium marginLeft10 primary" ng-click="$root.confirmPopup()">' + parameterOptions.confirmbtnLabel + '</button>');
            }

            $compile($('.modalPopup[popuptype="' + parameterOptions.popupName + '"]').contents())($scope);
        }

        $root.confirmPopup = function () {
            $('.modalPopup').remove();

            if ($scope.successCallBack) {
                $scope.successCallBack();
            }
        }

        $root.removePopup = function () {
            $('.modalPopup').remove();

            if ($scope.cancelCallBack) {
                $scope.cancelCallBack();
            }
        }

        $root.ShowPopup = function (currentTarget, windowContainer) {
            var popdetect = $(currentTarget).attr('data-popup-type')
            $('.preload-overlay', $(windowContainer)).show()
            var xpos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerWidth() / 2
            var ypos = $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).outerHeight() / 2
            $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).css({ 'margin-top': '-' + ypos + 'px', 'margin-left': '-' + xpos + 'px' })
            $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).fadeIn(500)
            $(".popup[data-popup-type='" + popdetect + "']", $(windowContainer)).addClass('show')

            $('.popup', $(windowContainer)).on('click', 'span.close', function () {
                $(this).closest('.popup').fadeOut(500);
                $(this).closest('.popup').removeClass('show cropPopup');
                $('.preload-overlay', $(windowContainer)).fadeOut(500);
                $(".dynamicPopoverOverlay, .snapshotOverlay").hide();
                $("input.UploadFile").val('');
            })

            $('.popupbtn a', $(windowContainer)).on('click', function (e) {
                e.preventDefault()
            })

            $('.preload-overlay', $(windowContainer)).on('click', function () {
                $('.popup', $(windowContainer)).removeClass('show')
                $('.popup', $(windowContainer)).fadeOut(500)
                $('.preload-overlay', $(windowContainer)).fadeOut(500);
            })
        }

}]);