app.controller('LayoutHeaderController', ['$scope', '$http', "$rootScope", "subscriptionService", "$window", "$compile",
    function ($scope, $http, $root, $subscription, $window, $compile) {
    console.log('LayoutHeader controller loaded.');
    $root.IsGlobalError = false;
    $root.GlobalMessage = null;   
    $scope.HeaderInfoModel = {};
    $scope.HeaderInfoModel.ProfileFile = "Images/profilepic.png";
    $scope.SelectedTheme = 'gray';
    $scope.SelectedLayout = 'multi';
    $scope.mailData = [];
    $scope.LoginID = null;
    $scope.AcademicYear = [];
    $scope.School = [];
    $scope.SelectedAcademicYear = { "Key": null, "Value": null };
    $scope.SelectedSchool = {"Key": null, "Value": null};
    $scope.SelectedAcademicYearDetail = null;
    $scope.SelectedSchoolDetail = null;
    $scope.AcademicYearList = [];
    $scope.AlertCount = 0;
    $scope.SelectedLanguage = 'en';
        const themeToggleButton = document.getElementById('theme-toggle');
        const lightIcon = document.querySelector('.theme-icon-light');
        const darkIcon = document.querySelector('.theme-icon-dark');
        const htmlElement = document.documentElement;


    if ($subscription.subscribe) {
        $subscription.subscribe(
            { 'subscribeTo': 'alertcount', 'componentID': 'layout', 'container': '' },
            SetAlertCount
        );
    }

    function SetAlertCount(count) {
        $scope.$apply(function () {
            $scope.AlertCount = count;
        });
        };


        $scope.Init = function (theme, layout, cultureCode) {
            initializeTheme();
        if (theme) {
            $scope.SelectedTheme = theme;
        }

        if (theme) {
            $scope.SelectedLayout = layout;
        }

        if (cultureCode) {
            $scope.SelectedLanguage = cultureCode;
        }

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=ActiveAcademicYear&defaultBlank=false",
        }).then(function (result) {
            $scope.AcademicYear = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetLookUpData?lookType=School&defaultBlank=false",
        }).then(function (result) {
            $scope.School = result.data;
        });

        $.ajax({
            url: "Home/GetUserDetails",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.HeaderInfoModel = result.Response;
                        $scope.LoginID = $scope.HeaderInfoModel.LoginID;
                        $scope.SelectedSchool.Key = $scope.HeaderInfoModel.SchoolID.toString();
                        $scope.SelectedSchool.Value = $scope.HeaderInfoModel.School;
                        $scope.SelectedSchoolDetail = $scope.HeaderInfoModel.School;
                        $scope.SelectedAcademicYear.Key = $scope.HeaderInfoModel.AcademicYearID;
                        $scope.SelectedAcademicYear.Value = $scope.HeaderInfoModel.AcademicYear;
                        $scope.SelectedAcademicYearDetail = $scope.HeaderInfoModel.AcademicYear;
                        $scope.notificationAlertCount();
                    });
                }
            }
        });

        $scope.GetAcademicYearList();

    }

    $scope.GetAcademicYearList = function () {
        $.ajax({
            url: "Mutual/GetActiveAcademicYearListData",
            type: "GET",
            success: function (result) {
                $scope.AcademicYearList = result;
            }
        });
    }

    $scope.LanguageChanged = function (cultureCode) {
        $window.location = '\?language=' + cultureCode
    }

    $scope.ThemeChanged = function (theme) {
        var layoutQueryString;

        if ($scope.SelectedLayout == 'smart') {
            layoutQueryString = "layout=smart";            
        }
        else {
            layoutQueryString = "layout=multi";    
        }

        if (theme == 'gray') {
            $window.location = "\?theme=gray&" + layoutQueryString;
            $scope.SelectedTheme = 'gray';
        }
        else {
            $window.location = "\?theme=blue&" + layoutQueryString;
            $scope.SelectedTheme = 'blue';
        }
    }

    $scope.LayoutChanged = function (type) {
        var themeQueryString;

        if ($scope.SelectedTheme == 'gray') {
            themeQueryString = "theme=gray";
        }
        else {
            themeQueryString = "theme=blue";
        }

        if (type == 'smart') {
            $window.location = "\?layout=smart&" + themeQueryString;
            $scope.SelectedLayout = 'smart';
        }
        else {
            $window.location = "\?layout=multi&" + themeQueryString;
            $scope.SelectedLayout = 'multi';
        }
    }

    $scope.Signout = function () {
        $.ajax({
            url: "Account/Signout",
            type: "GET",
            success: function (result) {
                utility.redirect(utility.myHost + "Account/LogIn");
            }
        });
    }

    $scope.ChangeSchoolAcademicYear = function (event) {
        var targetElement = $(event.currentTarget);

        //$("[data-toggle='popover']").popover('destroy');
        
        targetElement.popover({
            container: 'body',
            placement: 'bottom',
            html: true,
            content: function () { return '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>' }
        });

        targetElement.popover('show');

        $.ajax({
            url: 'Schools/School/ChangeSchoolAcademicYear',
            type: 'GET',
            success: function (content) {
                $('#' + targetElement.attr('aria-describedby'))
                    .find('.popover-body').html($compile(content)($scope));
                window.dispatchEvent(new Event('resize'));
            }
        });
    }

        //To close Popup
        $scope.CloseSchoolPopup = function () {
            $(".popover").popover('dispose');
        }

    $scope.FillAcademicYearBySchool = function () {

        $scope.AcademicYear = [];
        var schoolID = $scope.SelectedSchool.Key || $scope.SelectedSchool;
        $scope.AcademicYearList.forEach(x => {
            if (x.SchoolID == schoolID) {
                $scope.AcademicYear.push({
                    "Key": x.AcademicYearID,
                    "Value": x.Description + " " + "(" + x.AcademicYearCode + ")",
                });
            };
        });

    }

        $scope.page = 1;
        $scope.pageSize = 20;
        $scope.loading = false;
        $scope.noMoreData = false;
        $scope.mailData = []; // Initialize only once

        $scope.loadMessages = function () {
            if ($scope.loading || $scope.noMoreData) return;

            $scope.loading = true;
            $scope.toGetMails();
        };

        $scope.toGetMails = function () {
            var url = 'Home/GetNotificationAlerts?page=' + $scope.page + '&pageSize=' + $scope.pageSize;

            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        if (data && data.Response && data.Response.length > 0) {
                            $scope.mailData = $scope.mailData.concat(data.Response); // ✅ Append instead of replacing
                            $scope.page++;

                            if (data.Response.length < $scope.pageSize) {
                                $scope.noMoreData = true;
                            }
                        } else {
                            $scope.noMoreData = true;
                        }

                        $scope.loading = false;
                        $scope.notificationAlertCount(); // Optional side-effect
                    });
                },
                error: function () {
                    $scope.$apply(function () {
                        $scope.loading = false;
                    });
                }
            });
        };

        angular.element(document).ready(function () {
            var container = document.getElementById('mailbox-container');

            if (container) {
                container.addEventListener('scroll', function () {
                    if (container.scrollTop + container.clientHeight >= container.scrollHeight - 50) {
                        $scope.$applyAsync(function () {
                            $scope.loadMessages(); // ✅ Correct function call
                        });
                    }
                });
            }

            // Optional: Initial load
            $scope.loadMessages();
        });

    //End To Get Mails

    //To NotificationCount
    $scope.notificationAlertCount = function () {
        $scope.AlertCount = 0;
        $.ajax({
            url: "Home/GetNotificationAlertsCount",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.AlertCount = data.Response;
                });
            }
        });
    }

    $scope.MarkAllAsRead = function () {
        $.ajax({
            type: "POST",
            url: utility.myHost + "Home/MarkNotificationAsRead?notificationAlertIID=" + 0,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $("#markAllButton").html("Saving...");
                if (result == "success") {
                    $scope.notificationAlertCount();
                    $scope.toGetMails();
                }
            },
            error: function () {
            },
            complete: function (result) {
                return null;
            }
        });
    }

    $scope.MarkAsReadSingle = function (requestedID) {
        $.ajax({
            type: "POST",
            url: utility.myHost + "Home/MarkNotificationAsRead?notificationAlertIID=" + requestedID ,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $("#markAllButton").html("Saving...");
                if (result == "success") {
                    $scope.notificationAlertCount();
                    $scope.toGetMails();
                }
            },
            error: function () {
            },
            complete: function (result) {
                return null;
            }
        });
    }


    $scope.LoadPopup = function (popupcontainer, event) {
        var dataAttr = $(event.currentTarget).attr('data-attr');
        var popupLeftPos = 0;
        var targetLeftPos = $(event.target).offset().left;
        var targetTopPos = $(event.target).offset().top - $(document).scrollTop();
        var pageWidth = $(document).outerWidth();
        var windowHeight = $(window).height();
        var eventWidth = $(event.target).outerWidth();
        var eventHeight = $(event.target).height();
        var popcontainerWidth = $(popupcontainer).outerWidth();
        var popcontainerHeight = $(popupcontainer).outerHeight();
        var popupTopPos = targetTopPos + eventHeight;
        var displayLeftArea = targetLeftPos + popcontainerWidth;
        var visiblePopupArea = popupTopPos + popcontainerHeight;
        $(popupcontainer).fadeIn("fast");
        if (displayLeftArea > pageWidth) {
            popupLeftPos = targetLeftPos - popcontainerWidth + eventWidth;
            $(popupcontainer).addClass('rightAligned');
        }
        else {
            popupLeftPos = targetLeftPos;
            $(popupcontainer).removeClass('rightAligned');
        }
        if (visiblePopupArea > windowHeight) {
            newTopPos = popupTopPos - popcontainerHeight - eventHeight;
            $(popupcontainer).addClass('setTooltipBottom');
        }
        else {
            newTopPos = popupTopPos;
            $(popupcontainer).removeClass('setTooltipBottom');
        }

        $(popupcontainer).css({ "left": popupLeftPos, "top": newTopPos });
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

    //To close Popup
    $scope.SaveSchool = function () {
        
            $.ajax({
                url: utility.myHost + "Account/ResetSchoolAcadamicYear",
                type: 'POST',
                data: { academicYearID: $scope.SelectedAcademicYear.Key, schoolID: $scope.SelectedSchool.Key },
                success: function (result) {
                    if (result.IsError === false) {
                        $scope.Message = result.Message;

                        if (result.MessageType != "") {
                            $scope.MessageType = result.MessageType;
                            $scope.ShowMessage = true;
                        }
                        $scope.$apply();
                        window.location.reload();
                        $("#ItemPopupSchool").hide();
                        $(".gridIItemPopupSchool").fadeOut("fast");
                        $(".gridItemOverlay").hide();
                        $(".ModelPopup-Container").hide();
                    };
                },
                error: function (err) {
                    $('.preload-overlay', $(windowContainer)).hide();
                    $scope.MessageType = "Error";
                    $scope.ShowMessage = true;
                }
            });
    }

    $scope.CloseSchoolMainPopup = function () {
        $(".gridIItemPopupSchool").fadeOut("fast");
        $(".gridItemOverlay").hide();
        $(".ModelPopup-Container").hide();
        }

        // Function to get the stored theme from localStorage
        function getStoredTheme() {
            return localStorage.getItem('theme') || 'light'; // Default to light theme
        }

        // Function to set the theme in localStorage
        function setStoredTheme(theme) {
            localStorage.setItem('theme', theme);
        }

        // Function to set the theme attribute and update icons
        function setTheme(theme) {
            htmlElement.setAttribute('data-bs-theme', theme);
            updateIcons(theme);
        }

        // Function to update icons based on the theme
        function updateIcons(theme) {
            if (theme === 'light') {
                lightIcon.classList.add('active');
                darkIcon.classList.remove('active');
            } else {
                lightIcon.classList.remove('active');
                darkIcon.classList.add('active');
            }
        }

        // Initialize the theme on page load
        function initializeTheme() {
            const storedTheme = getStoredTheme();
            setTheme(storedTheme);
        }


        (() => {
            'use strict';

            // Theme management functions
            const getStoredTheme = () => localStorage.getItem('theme');
            const setStoredTheme = (theme) => localStorage.setItem('theme', theme);

            const getPreferredTheme = () => {
                const storedTheme = getStoredTheme();
                if (storedTheme) return storedTheme;

                return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
            };

            const setTheme = (theme) => {
                const effectiveTheme = theme === 'auto'
                    ? window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
                    : theme;
                document.documentElement.setAttribute('data-bs-theme', effectiveTheme);
            };

            setTheme(getPreferredTheme());

            const showActiveTheme = (theme, focus = false) => {
                const themeSwitcher = document.querySelector('#bd-theme');
                if (!themeSwitcher) return;

                const themeSwitcherText = document.querySelector('#bd-theme-text');
                const activeThemeIcon = document.querySelector('.theme-icon-active use');
                const btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`);
                const svgOfActiveBtn = btnToActive.querySelector('svg use').getAttribute('href');

                document.querySelectorAll('[data-bs-theme-value]').forEach((element) => {
                    element.classList.remove('active');
                    element.setAttribute('aria-pressed', 'false');
                });

                btnToActive.classList.add('active');
                btnToActive.setAttribute('aria-pressed', 'true');
                activeThemeIcon.setAttribute('href', svgOfActiveBtn);

                const themeSwitcherLabel = `${themeSwitcherText.textContent} (${btnToActive.dataset.bsThemeValue})`;
                themeSwitcher.setAttribute('aria-label', themeSwitcherLabel);

                if (focus) themeSwitcher.focus();
            };

            window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
                const storedTheme = getStoredTheme();
                if (storedTheme !== 'light' && storedTheme !== 'dark') setTheme(getPreferredTheme());
            });

            window.addEventListener('DOMContentLoaded', () => {
                showActiveTheme(getPreferredTheme());

                document.querySelectorAll('[data-bs-theme-value]').forEach((toggle) => {
                    toggle.addEventListener('click', () => {
                        const theme = toggle.getAttribute('data-bs-theme-value');
                        setStoredTheme(theme);
                        applyViewTransition(() => {
                            setTheme(theme); // Set the theme before transitioning
                            showActiveTheme(theme, true);
                        });
                    });
                });
            });

            // Circular transition function
            let lastClick;
            addEventListener('click', (event) => (lastClick = event));

            function applyViewTransition(updateDOMCallback) {
                if (!document.startViewTransition) {
                    // Fallback: Apply theme change instantly
                    updateDOMCallback();
                    return;
                }

                // Get the last click position or use screen center as fallback
                const x = lastClick?.clientX ?? innerWidth / 2;
                const y = lastClick?.clientY ?? innerHeight / 2;

                // Calculate the radius for the circular transition
                const endRadius = Math.hypot(
                    Math.max(x, innerWidth - x),
                    Math.max(y, innerHeight - y)
                );

                // Create the view transition
                const transition = document.startViewTransition(() => {
                    updateDOMCallback();
                });

                // Animate with circular reveal
                transition.ready.then(() => {
                    document.documentElement.animate(
                        {
                            clipPath: [
                                `circle(0 at ${x}px ${y}px)`,
                                `circle(${endRadius}px at ${x}px ${y}px)`,
                            ],
                        },
                        {
                            duration: 500,
                            easing: 'ease-in-out',
                            pseudoElement: '::view-transition-new(root)',
                        }
                    );
                });
            }

            // Dark mode toggle with transition
     

            // Event listener for the theme toggle button
            // Dark mode toggle event listener
            themeToggleButton.addEventListener('click', () => {
                const currentTheme = htmlElement.getAttribute('data-bs-theme');
                const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
                setStoredTheme(newTheme);

                applyViewTransition(() => {
                    setTheme(newTheme);
                });
            });
            if (!document.startViewTransition) {
                window.toggleDarkMode = () => {
                    const currentTheme = htmlElement.getAttribute('data-bs-theme');
                    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
                    setStoredTheme(newTheme);
                    setTheme(newTheme);
                };
            }
            if (document.startViewTransition) {
                window.toggleDarkModeWithTransition = () => applyViewTransition(toggleDarkMode);
            } else {
                window.toggleDarkMode = toggleDarkMode;
            }
        })();








}]);