app.controller("PushNotificationController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("Push Notification Controller Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.SendPushNotification = function ($event, $element, viewModel) {

        if (viewModel.Branch == null || viewModel.Branch == "") {
            $().showGlobalMessage($root, $timeout, true, "Select Branch!");
            return;
        }

        if (viewModel.NotificationUser == null || viewModel.NotificationUser == "") {
            $().showGlobalMessage($root, $timeout, true, "Select any user!");
            return;
        }

        if (viewModel.NotificationType == null || viewModel.NotificationType == "") {
            $().showGlobalMessage($root, $timeout, true, "Select any notification type!");
            return;
        }

        if ((viewModel.TextMessage == null || viewModel.TextMessage == "") && (viewModel.Message == null || viewModel.Message == "")) {
            $().showGlobalMessage($root, $timeout, true, "Message is required!");
            return;
        }

        if (viewModel.IsEmail == true && viewModel.Subject == null || viewModel.IsEmail == true && viewModel.Subject == "" || viewModel.IsEmail == true && viewModel.Subject == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Please fill Subject");
            return;
        }

        showOverlay();

        $.ajax({
            type: "POST",
            data: JSON.stringify(viewModel),
            url: utility.myHost + "Schools/School/SendPushNotification",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Response);
                    hideOverlay();
                }
                else {
                    $scope.$apply(function () {
                        $().showGlobalMessage($root, $timeout, false, result.Response);
                        hideOverlay();
                    });
                }
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, result.Response);
                hideOverlay();
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

    $scope.UserChanges = function ($event, $element, vModel) {
        if (vModel.NotificationUser == null || vModel.NotificationUser == "") return false;

        if (vModel.MessageSendType == null || vModel.MessageSendType == "" || vModel.MessageSendType == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Please select Message Send Type");
            vModel.NotificationUser = null;
            return;
        }
        showOverlay();
        var model = vModel;
        if (model.Branch == null || model.Branch == undefined || model.Branch == "") {
            model.Branch = 0;
        }
        model.MessageSendTo = null;

        var url = "Schools/School/GetToNotificationUsersByUser?userID=" + model.NotificationUser.Key + "&branchID=" + model.Branch + "&user=" + model.NotificationUser.Value;
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    if (model.MessageSendType == 2) {
                        $scope.LookUps.MessageSendTo = result.data;
                    }
                    else {
                        model.MessageSendTo = result.data;
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
    };

    
    $scope.NotificationTypeChanges = function ($event, $element, vModel) {
        if (vModel.NotificationType == null || vModel.NotificationType == "") return false;
        var model = vModel;
        //NotificationType == 1 -- email dropdown
        if (model.NotificationType.Value == "Email") {
            model.IsEmail = true;
        }
        else {
            model.IsEmail = false;
            model.Message = null;
            model.Subject = null;
        }
    };


    $scope.ChangeEmailTemplate = function ($event, $element, model) {
        var communicationModel = model;
        if (communicationModel.EmailTemplate == null || communicationModel.EmailTemplate == "") {
            return false;
        }

        var url = "Mutual/GetEmailTemplateByID?TemplateID=" + communicationModel.EmailTemplate.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                communicationModel.Message = result.data.EmailTemplate;

            }, function () {

            });
    };

    $scope.SendMailNotification = function ($event, $element, model) {
        var mailNotificationModel = model;
        if (!mailNotificationModel.MailToAddress) {
            $().showGlobalMessage($root, $timeout, true, "To mail address is required!");
            return;
        }

        if (!mailNotificationModel.MailSubject) {
            $().showGlobalMessage($root, $timeout, true, "Mail subject is required!");
            return;
        }

        if (!mailNotificationModel.MailMessage) {
            $().showGlobalMessage($root, $timeout, true, "Mail message is required!");
            return;
        }

        showOverlay();

        $.ajax({
            type: "POST",
            data: JSON.stringify(mailNotificationModel),
            url: utility.myHost + "Schools/School/SendMailNotification",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Response, 3000);
                    hideOverlay();
                }
                else {
                    $scope.$apply(function () {
                        $().showGlobalMessage($root, $timeout, false, result.Response, 3000);
                        hideOverlay();
                    });
                }
            },
            error: function (result) {
                $().showGlobalMessage($root, $timeout, true, result.Response);
                hideOverlay();
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

}]);