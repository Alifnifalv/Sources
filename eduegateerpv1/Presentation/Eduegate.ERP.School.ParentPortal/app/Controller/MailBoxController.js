//var app = angular.module('rzSliderDemo', ['rzSlider', 'ui.bootstrap'])
app.controller("MailBoxController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("MailBoxController Loaded");

    $scope.mailData = [];


    $scope.onLoad = function () {
        $scope.toGetMails("Inbox");
        $scope.NotificationAlertCount();
    }

    $scope.loadMessages = function () {
        $scope.toGetMails();
        $scope.NotificationAlertCount();
        scope.toGetAllMails();
        $scope.toGetSendMails();
    }

    //Get NotificationCount
    $scope.NotificationAlertCount = function () {
        $scope.AlertCount = 0;
        $.ajax({
            url: utility.myHost + "/Home/GetNotificationAlertsCount",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.AlertCount = data;
                });
            }
        });
    }
    

    //To Get Inbox/un read Mails
    $scope.toGetMails = function () {
        $scope.mailData = [];
        $scope.IsInbox = true;
        $scope.IsAllMail = false;
        $scope.IsSend = false;
        $scope.tab = "Inbox";
        $.ajax({
            url: utility.myHost + "/Home/GetNotificationAlerts",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.unreadMailData = data.Response;
                    //$scope.NotificationAlertCount();
                });
            }
        });
    }
    //End

    //To Get all Mails
    $scope.toGetAllMails = function () {
        $scope.mailData = [];
        $scope.IsAllMail = true;
        $scope.IsInbox = false;
        $scope.IsSend = false;
        $scope.tab = "All Mail";
        $.ajax({
            url: utility.myHost + "/Home/GetAllNotificationAlerts",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.allMails = data.Response;
                    //$scope.NotificationAlertCount();
                });
            }
        });
    }
    //End

    //To Get send Mails from Parent
    $scope.toGetSendMails = function () {
        $scope.mailData = [];
        $scope.IsSend = true;
        $scope.IsInbox = false;
        $scope.IsAllMail = false;
        $scope.tab = "Send";
        $.ajax({
            url: utility.myHost + "/Home/GetSendMailFromParent",
            type: "GET",
            success: function (data) {
                $scope.$apply(function () {
                    $scope.sendMails = data.Response;
                    //$scope.NotificationAlertCount();
                });
            }
        });
    }
    //End


    //To Get send Mails
    //$scope.toGetSendMails = function (Folder) {
    //    $scope.mailData = [];
    //    $scope.IsSend = true;
    //    $scope.IsInbox = false;
    //    $scope.IsAllMail = false;
    //    $.ajax({
    //        type: "GET",
    //        data: { Folder: Folder },
    //        url: utility.myHost + "/Home/GetAllMailsByLoginIDandFolderName",
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            if (result.Data.isError == false) {
    //                $scope.$apply(function () {
    //                    $scope.mailData = result.Data.mailList;
    //                });
    //            }
    //        },
    //    });
    //    //End
    //}

    $scope.MarkAsReadSingle = function (requestedID) {
        $.ajax({
            type: "POST",
            url: utility.myHost + "/Home/MarkNotificationAsRead?notificationAlertIID=" + requestedID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
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
            $.getJSON(utility.myHost + "/Home/ComposeMail", $scope.Email, function (result) {
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
}]);