app.controller('LayoutHeaderController', ['$scope', '$http', "$rootScope", "$window", "$timeout", function ($scope, $http, $rootScope, $window, $timeout) {
    console.log('LayoutHeader controller loaded.');

    $rootScope.IsGlobalError = false;
    $rootScope.GlobalMessage = null;
    $scope.SelectedTheme = 'gray';
    $scope.SelectedLayout = 'multi';
    $rootScope.LoginID = null;
    $rootScope.UserName = null;
    $scope.AlertCount = 0;

    $scope.HeaderInfoModel = {};
    $scope.HeaderInfoModel.ProfileFile = utility.myHost + "Images/profilepic.png";

    $scope.AvailableSlotCount = 0;
    $scope.FilledSlotCount = 0;
    $scope.NotAvailableSlotCount = 0;
    $scope.SlotData = [10, 7, 3];

    $scope.ScheduledMeetingCount = 0;
    $scope.CompletedMeetingCount = 0;
    $scope.CancelledMeetingCount = 0;

    $scope.Init = function (theme, layout) {
        if (theme) {
            $scope.SelectedTheme = theme;
        }

        if (theme) {
            $scope.SelectedLayout = layout;
        }
        $.ajax({
            url: utility.myHost + "Home/GetUserDetails",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.HeaderInfoModel = result.Response;
                        $rootScope.LoginID = result.Response.LoginID;
                        $rootScope.UserName = result.Response.UserName;
                    });
                }

                $scope.GetActiveSignUpGroups();
            }
        });
    };

    //Tab Clicks Start
    $scope.EventsTabClick = function () {
        window.location.href = utility.myHost + "SignUp/Event";
    };

    $scope.ConferenceTabClick = function () {
        window.location.href = utility.myHost + "Home/Conference";
    };

    $scope.MeetingRequestTabClick = function () {
        window.location.href = utility.myHost + "SignUp/MeetingRequestList";
    };
    //Tab click End

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
    };

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
    };

    $scope.changepassword = function () {
        window.location.href = utility.myHost + "Account/ResetPassword";
    };

    $scope.Signout = function () {
        $.ajax({
            url: utility.myHost + "Account/Signout",
            type: "GET",
            success: function (result) {
                setTimeout(preventBack, 0);
                Logout();
                utility.redirect(utility.myHost + "Account/LogIn");
            }
        });
    };

    //Clear History
    function preventBack() {
        window.history.forward(-1);
    }
    
    function Logout() {
        try {
            Android.Logout();
        } catch (e) {
        }
    }
    //End Clear History

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    $scope.GetActiveSignUpGroups = function () {
        $.ajax({
            url: utility.myHost + "SignUp/GetActiveSignupGroups",
            type: "GET",
            success: function (result) {
                if (!result.IsError && result.Response != null) {
                    $scope.$apply(function () {
                        $scope.SignUpGroupDetails = result.Response;
                    });
                }
            }
        });
    };

    $scope.GroupViewClick = function (groupDetails) {
        window.location.replace(utility.myHost + "SignUp/EventDetails?groupID=" + groupDetails.SignupGroupID);
    }

}]);