//var app = angular.module('rzSliderDemo', ['rzSlider', 'ui.bootstrap'])
app.controller("MailBoxController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("MailBoxController Loaded");

    $scope.mailData = [];

    $scope.CurrentTab = null;

    $scope.onLoad = function () {
        $scope.toGetMails("Inbox");
        $scope.NotificationAlertCount();
    }

    $scope.loadMessages = function () {
        // Show the spinner
        document.getElementById("spinner-container").style.display = "flex";

        $scope.toGetMails($scope.CurrentTab);
        $scope.NotificationAlertCount();
        $scope.toGetAllMails($scope.CurrentTab);
        $scope.toGetSendMails($scope.CurrentTab);
        $scope.GotoCurrentTab($scope.CurrentTab);
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
    

    //To Get Inbox/un read Mails
    $scope.toGetMails = function (currentTab) {
        $scope.mailData = [];
        $scope.CurrentTab = currentTab;

        $.ajax({
            url: utility.myHost + "Home/GetNotificationAlerts",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.unreadMailData = data.Response;
                    $scope.GotoCurrentTab($scope.CurrentTab);
                    //$scope.NotificationAlertCount();
                });
            }
        });
    }
    //End

    //To Get all Mails
    $scope.toGetAllMails = function (currentTab) {
        $scope.mailData = [];
        $scope.CurrentTab = currentTab;

        $.ajax({
            url: utility.myHost + "Home/GetAllNotificationAlerts",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.allMails = data.Response;
                    $scope.GotoCurrentTab($scope.CurrentTab);
                    //$scope.NotificationAlertCount();
                });
            }
        });
    }
    //End

    //To Get send Mails from Parent
    $scope.toGetSendMails = function (currentTab) {
        $scope.mailData = [];
        $scope.CurrentTab = currentTab;

        $.ajax({
            url: utility.myHost + "Home/GetSendMailFromParent",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.sendMails = data.Response;
                    $scope.GotoCurrentTab($scope.CurrentTab);
                    //$scope.NotificationAlertCount();
                });
            }
        });
    }
    //End

    $scope.MarkAsRead = function (requestedID) {

        // Show the spinner
        document.getElementById("spinner-container").style.display = "flex";

        $.ajax({
            type: "POST",
            url: utility.myHost + "Home/MarkNotificationAsRead?notificationAlertIID=" + requestedID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result == "success") {
                    $scope.NotificationAlertCount();
                    $scope.toGetMails($scope.CurrentTab);
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
        if (fromMail == null || fromMail == "" || fromMail == "? undefined:undefined ?") {
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
    }
    //End Compose Mail

    //Close Compose Mail
    $scope.closeComposeMail = function () {
        $scope.Email = {};
        $("#mailComposeContainer").hide();
    }
    //End Close Compose Mail

    $scope.onLoad();


    $scope.GotoCurrentTab = function (currentTab) {

        $scope.IsInbox = false;
        $scope.IsAllMail = false;
        $scope.IsSend = false;
        $scope.tab = null;

        if (currentTab) {
            if (currentTab == "AllMails") {
                $scope.IsAllMail = true;
            }
            else if (currentTab == "Sent") {
                $scope.IsSend = true;
            }
            else {
                $scope.IsInbox = true;
            }
            $scope.tab = currentTab;
        }
        else {
            return false;
        }

        // Set a timeout to hide the spinner after 1 seconds 
        setTimeout(function () {
            document.getElementById("spinner-container").style.display = "none";
        }, 1000); 
    };

}]);