app.controller('LayoutHeaderController', ['$scope', '$http', "$rootScope", "$window", "$timeout", function ($scope, $http, $rootScope, $window, $timeout) {
    console.log('LayoutHeader controller loaded.');
    $rootScope.IsGlobalError = false;
    $rootScope.GlobalMessage = null;
    $scope.HeaderInfoModel = {};
    $scope.HeaderInfoModel.ProfileFile = utility.myHost + "Images/profilepic.png";
    $scope.SelectedTheme = 'gray';
    $scope.SelectedLayout = 'multi';
    $rootScope.LoginID = null;
    $rootScope.UserName = null;
    $scope.Email = {};
    $scope.FromEmail = null;
    $scope.AlertCount = 0;
    $scope.ToEmailList = [];
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
                $scope.FillEmailID();
                $scope.NotificationAlertCount();
                $scope.toGetMails();
            }
        });
    };

    //call ToasterPlugin
    function callToasterPlugin(status, title) {
        new Notify({
            status: status,
            title: title,
            effect: 'fade',
            speed: 300,
            customClass: null,
            customIcon: null,
            showIcon: true,
            showCloseButton: true,
            autoclose: true,
            autotimeout: 3000,
            gap: 20,
            distance: 20,
            type: 1,
            position: 'right top'
        })
    }
    //end call ToasterPlugin
    //$scope.NewApplicationClick = function () {
    //    window.location = "NewApplicationFromSibling?loginID=" + $rootScope.LoginID;
    //};
    $scope.loadMessages = function () {
        $scope.toGetMails();
    }

    //Get NotificationCount
    $scope.NotificationAlertCount = function () {
        $scope.AlertCount = 0;
        $.ajax({
            url: utility.myHost + "Home/GetNotificationAlertsCount",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.AlertCount = data;
                });
            }
        });
    }

    //To Get Mails
    $scope.toGetMails = function () {
        $scope.mailData = [];
        $.ajax({
            url: utility.myHost + "Home/GetNotificationAlerts",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.mailData = data.Response;
                    $scope.NotificationAlertCount();
                });
            }
        });
    }
    //End To Get Mails


    $scope.MarkAsRead = function (requestedID) {
        $.ajax({
            type: "POST",
            url: utility.myHost + "Home/MarkNotificationAsRead?notificationAlertIID=" + requestedID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $("#markAllButton").html("Saving...");
                if (result == "success") {
                    $scope.NotificationAlertCount();
                    $scope.toGetMails();
                }
            },
            error: function () {
            },
            complete: function (result) {
                $("#markAllButton").html("Mark all read");
                return null;
            }
        });
    }

    //Compose Mail
    $scope.ComposeMail = function () {

        console.log($scope.Email);
        var fromMail = $scope.FromEmail;
        $scope.Email.frommail = $scope.FromEmail;
        var toMail = $("#tomail").val();
        var Subject = $("#subjectmail").val();
        var Body = $("#bodymail").val();
        if (fromMail == null || fromMail == "" || fromMail =="? undefined:undefined ?") {
            $("#frommail").focus();
            callToasterPlugin('error', 'Please select from mail id');
            return;
        }
        else if (toMail == null || toMail == "" || toMail == "? undefined:undefined ?") {
            $("#tomail").focus();
            callToasterPlugin('error', 'Please select to mail id');
            return;
        }
        else if (Subject == null || Subject == "") {
            $("#subjectmail").focus(); callToasterPlugin('error', 'Please enter subject');
            return;
        }
        else if (Body == null || Body == "") {
            $("#bodymail").focus();
            callToasterPlugin('error', 'Please enter body content');
            return;
        }
        else {
            $.getJSON(utility.myHost + "Home/ComposeMail", $scope.Email, function (result) {
                $("#mailComposeContainer").hide();
            })
        }
        var frm = document.getElementsByName('contact-form')[0];
        frm.submit(); // Submit the form
        frm.reset();  // Reset all form data
        return false; // Prevent page refresh

    }

    //End Compose Mail

    //Close Compose Mail
    $scope.closeComposeMail = function () {
        $scope.Email = {};
        $("#mailComposeContainer").hide();
    }
    //End Close Compose Mail

    $scope.MyWardClick = function () {
        window.location.replace(utility.myHost + "Home/MyWards");
    };

    $scope.NewApplicationClick = function () {
        window.location.replace(utility.myHost + "Home/NewApplicationFromSibling?loginID=" + $rootScope.LoginID);
    };

    $scope.NewApplicationGuestClick = function () {
        window.location.replace(utility.myHost + "Home/NewApplication?loginID=" + $rootScope.LoginID);
    };

    $scope.ApplicationListClick = function () {
        window.location.replace(utility.myHost + "Home/ApplicationListFromSibling");
    };

    $scope.CircularListClick = function () {
        window.location.replace(utility.myHost + "Home/CircularList");
    };

    $scope.CounselorTabClick = function () {
        window.location.replace(utility.myHost + "Home/CounselorHubList");
    };

    $scope.GalleryTabClick = function () {
        window.location.replace(utility.myHost + "Home/GalleryView");
    };

    $scope.TransportApplicationListClick = function () {
        window.location.replace(utility.myHost + "Home/StudentTransportRequestList");
    };

    $scope.StudentPickupRequestListClick = function () {
        window.location.replace(utility.myHost + "Home/StudentPickupRequestList");
    };

    $scope.ApplicationListClickFromPopUP = function () {
        window.location.replace(utility.myHost + "Home/ApplicationListFromSibling");
    };

    $scope.ApplicationListClickFromPopUPGuest = function () {
        window.location.replace(utility.myHost + "Home/AnonymousDashbaord");
    };

    $scope.MailBoxClick = function () {
        window.location.replace(utility.myHost + "Home/Mailbox");
    };

    $scope.FeePaymentTabClick = function () {
        window.location.replace(utility.myHost + "Fee/Index");
    };

    $scope.ReportCardClick = function () {
        window.location.replace(utility.myHost + "Home/ReportCard");
    };

    $scope.PaymentGatewayClick = function () {
        var url = utility.myHost + "PaymentGateway/InitiateQPAYPayment";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result && result.data && result.data.Data) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            
                        });
                    }
                    )
                }
            }, function () {

            });
    };

    $scope.CoachingProgramClick = function () {
        window.location.replace(utility.myHost + "FormBuilder/CoachingFormHistory");
    };

    $scope.TicketTabClick = function () {
        window.location.replace(utility.myHost + "Ticket/Index");
    };

    $scope.AboutClick = function () {
        window.location.replace(utility.myHost + "Home/About");
    };

    $scope.ContactClick = function () {
        window.location.replace(utility.myHost + "Home/Contact");
    };

    $scope.MeetingTabClick = function () {
        $http({
            method: 'Get', url: utility.myHost + "Setting/GetSettingValueByKey?settingKey=" + "CLIENT_Meeting_PORTAL",
        }).then(function (result) {

            window.open(result.data + "Home/Meeting?loginID=" + $rootScope.LoginID, '_blank');

        });
    };

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

    $scope.FillEmailID = function () {
        $scope.FromEmail = $scope.HeaderInfoModel.LoginEmailID;
        var url = utility.myHost + "Home/GetTeacherEmailByParentLoginID";
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result && result.data && result.data.Data) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.ToEmailList = result.data.Data.mailList;
                        });  }
                        )
                }
            }, function () {

            });
    };

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

}]);