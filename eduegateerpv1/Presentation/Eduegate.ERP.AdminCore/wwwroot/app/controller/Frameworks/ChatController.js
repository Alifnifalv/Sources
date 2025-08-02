app.controller("ChatController", ["$scope", "$compile", function ($scope, $compile) {
        var conId = null;
        $scope.Recent = "Recent chat";
        $scope.Message = "";
        $scope.Users = [];
        $scope.FilteredUsers =[];
        $scope.RecentUsers = [];

        $scope.Messages = [];
        $scope.LoggedInUser = null;
        $scope.events = {};
        $scope.ChatWindowCount = 0;
        var IsShowPopup = false;
        var Notifier = new BrowserNotifier();

        $(window).on('focus', function () { IsShowPopup = false; });
        $(window).on('blur', function () { IsShowPopup = true; }); 

        // Declare a proxy to reference the hub.
        var chatHub = $.connection.notificationHub;
        registerClientMethods(chatHub);

        $('#search-input').keyup(function (event) {
            var searchText = $('#search-input').val().toUpperCase();
            var users = JSLINQ($scope.Users).Where(function (item) { if (searchText == '') return true; else return item.UserName.toUpperCase().indexOf(searchText) != -1 });
            $scope.$apply(function () {
                $scope.FilteredUsers =[];
                angular.forEach(users.items, function (row, index) {
                    $scope.FilteredUsers.splice(0, 0, {
                        ConnectionID: row.ConnectionID, UserID: row.UserID, UserName: row.UserName, Hours: null, UreadMessages: 0, CountryCode: row.CountryCode
                            });
                });
            });
        });

        $scope.HasUnreadMessage = function (event, data) {
            if (data.UreadMessages != 0)
                return true;
            else
                return false;
        }

        // Start Hub
        //$.connection.hub.start().done(function () {
        //    chatHub.server.connect($scope.User.UserID, $scope.User.UserName, $scope.User.UserRole, $scope.CountryCode, $scope.cultureCode);
        //});      

        function OpenChatSettingsWindow() {
            var $div = $('<div id="chatSettings"></div>') // removed bottom:0 from style
			.load($scope.HostUrl + '/Chat/ChatSettings', function () {
			    $window = $('#chatSettings');
			    $("#chatSettings").html($compile($div)($scope));
			});

            $('#ChatMessageContainer .chatsettingswindow').append($div);

        }

        function InitializeClientEvents(connectionID, userName, message, callback, responseID) {
            $window = $('#' + connectionID);
            $window.find('#userName').text(userName);
            $textBox = $window.find("#txtPrivateMessage");
            $textBox.focus();

            $textBox.keyup(function(event) {
                chatHub.server.updateStatus($scope.LoggedInUser.ConnectionID, connectionID, "TYPING");
            });

            $window.find('#sendButton').click(function (event) {
                $textBox = $(this).siblings('textarea');
                var msg = $textBox.val().trim();
                if (msg.length > 0) {
                    chatHub.server.sendMessageTo(connectionID, msg);
                }

                $textBox.val('');
                $textBox.focus();
            });

            $window.find('#signout').click(function (event) {
                chatHub.server.chatExit(connectionID);
            });

            $window.find('#close').click(function () {
                $('#divContainer').remove();
            });

            $window.find("#txtPrivateMessage").keyup(function (e) {
                if (e.ctrlKey && e.which == 13) {
                    $(this).val($(this).val() + "\n");
                    return true;
                }
                else
                if (e.which == 13) {
                    $(this).siblings('input[type=image]').click();
                    return false;
                }
            });

            if (callback != undefined) {
                callback(connectionID, userName, message, false, responseID);
            }
        }

        function OpenChatWindow(connectionID, userName, message, callback, responseID, isVisible) {
      
            //var getChatWindowsHtml = loadChatWindow(connectionID);
            //show the message screen on dailogue
            var $div = $('<div id=' + connectionID + '></div>') // removed bottom:0 from style

            if(isVisible != undefined && isVisible == true) {
                $div.hide();
                $div.removeClass('active');
            }
            else
            {
                $div.addClass('active');
            }

            $div.load($scope.HostUrl + '/Chat/ChatWindow', function () {
			    $window = $('#' + connectionID);
			    $window.find('#userName').text(userName);

			    InitializeClientEvents(connectionID, userName, message, callback, responseID);
			    $window.show();
			});

            $('#ChatMessageContainer .chatswindow').append($div);
        }

        $scope.OnConnected = function OnConnected(id, userName, allUsers, messages, countryCode, cultureCode) {
            var usertype2;
            if (countryCode == undefined || countryCode == null || countryCode == 'null') {
                userType2 = "CMS";
            }
            else {
                userType2 = countryCode;
            }

            angular.forEach(allUsers, function (row, index) {
                var userType1;
                if (row.CountryCode == undefined || row.CountryCode == null || row.CountryCode == 'null') {
                    userType1 = "CMS";
                }
                else
                {
                    userType1 = row.CountryCode;
                }

                if (id != row.ConnectionID) {
                    $scope.Users.splice(0, 0, {
                        ConnectionID: row.ConnectionID, UserID: row.UserID, UserName: row.UserName, Hours: null, UreadMessages: 0, CountryCode: userType2, CultureCode: row.CultureCode
                    });
                    $scope.FilteredUsers.splice(0, 0, {
                        ConnectionID: row.ConnectionID, UserID: row.UserID, UserName: row.UserName, Hours: null, UreadMessages: 0, CountryCode: userType2, CultureCode: row.CultureCode
                    });
                }
                else {
                    $scope.LoggedInUser = { ConnectionID: id, UserID: row.UserID, UserName: userName, Hours: null, UreadMessages: 0, CountryCode: userType2, CultureCode: cultureCode };
                }
            });

            angular.forEach(messages, function (row, index) {
                $scope.Messages.push({ UserID: row.UserID, Message: row.Message, UserName: row.UserName, Startdate: null, UreadMessages: 0 });
            });
        }

        $scope.OnNewUserConnected = function OnNewUserConnected(id, userID, userName, countryCode, cultureCode) {

            if (countryCode == undefined || countryCode == 'null')
            {
                countryCode = "CMS";
            }

            $scope.Users.splice(0, 0, { ConnectionID: id, UserID: userID, UserName: userName, Hours: 0, UreadMessages: 0, CountryCode: countryCode, CultureCode: cultureCode });
            $scope.FilteredUsers.splice(0, 0, { ConnectionID: id, UserID: userID, UserName: userName, Hours: 0, UreadMessages: 0, CountryCode: countryCode, CultureCode: cultureCode });
        }

        $scope.OnUserDisconnected = function OnUserDisconnected(id, userName) {
             $scope.Users.forEach(function(element, index, array) {
                  if(element.ConnectionID === id) {
                      $scope.Users.splice(index, 1);
                }
              });

             $scope.FilteredUsers.forEach(function(element, index, array) {
                 if (element.ConnectionID === id) {
                     $scope.RecentUsers.splice(0, 0, { ConnectionID: element.ConnectionID, UserID: element.UserID, UserName: element.UserName, Hours: 0, UreadMessages: 0, CountryCode: element.CountryCode, CultureCode: element.CultureCode });
                     $scope.FilteredUsers.splice(index, 1);
                }
            });

            //hide the window
            //$window = $('#' + id);
            //$window.remove();
        }

        $scope.SendMessageTo = function SendMessageTo(fromConnectionID, toConnectionID, message) {
            // always the dailogue always open
            if ($scope.LoggedInUser.ConnectionID == fromConnectionID) { // if the message from sender itself
                AddMessage(toConnectionID, $scope.LoggedInUser.UserName, message, true);
            }
            else {
                // window is already opened if not we should open a new window
                $window = $('#' + fromConnectionID);
                var user = JSLINQ($scope.Users).Where(function (item) { return item.ConnectionID == fromConnectionID; }).items;

                var isVisible = false;
                if ($('.chat-space').length != 0 && !$window.hasClass('active')) {
                    isVisible = true;
                    var filteredUser = JSLINQ($scope.FilteredUsers).Where(function (item) { return item.ConnectionID == user[0].ConnectionID; }).items;
                    filteredUser[0].UreadMessages = filteredUser[0].UreadMessages + 1;
                }

                if ($window.length == 0) {
                    OpenChatWindow(user[0].ConnectionID, user[0].UserName, message, AddMessage, null, isVisible);
                } else {
                    if (isVisible) {
                        $window.show();
                        $window.find('.chat-space').show(); 
                        $window.addClass('active');
                    }

                    AddMessage(user[0].ConnectionID, user[0].UserName, message, false);
                }

                if (toConnectionID != fromConnectionID && IsShowPopup) {
                    Notifier.notifyMe($scope.HostUrl, message, user[0].UserName);
                }
            }

           

            if (parentContainer != null) {
                parentContainer.postMessage("ShowChatWindow", "*");
            }
        }

        $scope.UpdateStatus = function UpdateStatus(fromConnectionID, toConnectionID, message) {
            $window = $('#' +fromConnectionID);
            var $element = $window.find('#statusMessage');
            if ($element.attr('style') == undefined || $element.css('display') == 'none') {
                $element.text(message).fadeIn();
                $element.fadeOut(1000);
            }
        }

        $scope.SendResponseMessageTo = function SendResponseMessageTo(fromConnectionID, toConnectionID, message, responseID) {
            $('.chat-space').each(function (index, element) {
                $(element).hide();
            });

            var isVisible = false;
            if ($('.chat-space').length != 0) {
                isVisible = true;
                var filteredUser = JSLINQ($scope.FilteredUsers).Where(function (item) { return item.ConnectionID == fromConnectionID; }).items;
                filteredUser[0].UreadMessages = filteredUser[0].UreadMessages + 1;
            }

            // always the dailogue always open
            // window is already opened if not we should open a new window
            $('#hdCheckWindow').val('1');
            $window = $('#' + fromConnectionID);
            var user = JSLINQ($scope.Users).Where(function (item) { return item.ConnectionID == fromConnectionID; }).items;

            if ($window.length == 0) {
                OpenChatWindow(user[0].ConnectionID, user[0].UserName, message, AddYesNoMessage, responseID, isVisible);
            } else {
                if (isVisible) {
                    $window.show();
                    $window.find('.chat-space').show();
                    $window.addClass('active');
                }
                AddYesNoMessage(user[0].ConnectionID, user[0].UserName, message, true, responseID);
            }
            
            if (toConnectionID != fromConnectionID) {
                Notifier.notifyMe($scope.HostUrl, message, user[0].UserName, "Chat request");
            }

            if (parentContainer != null) {
                parentContainer.postMessage("ShowChatWindow", "*");
            }
        }

        $scope.UserAssingedTo = function UserAssingedTo(fromConnectionID, toConnectionID, message) {
            var user = JSLINQ($scope.Users).Where(function (item) { return item.ConnectionID == fromConnectionID; }).items;
            AddMessage($scope.LoggedInUser.ConnectionID, user[0].UserName, message, true);
            InitializeClientEvents(fromConnectionID, user[0].UserName, message);
        }

        $scope.BroadcastMessage = function BroadcastMessage(fromConnectionID, toConnectionID, message) {
            var user = JSLINQ($scope.Users).items;
            for (var i = 0; i < user.items.length; i++) {
                if ($window.length == 0) {
                    OpenChatWindow(user.items[i].ConnectionID, user[i].UserName, message);
                } else {
                    AddMessage(user.items[i].ConnectionID, user.items[i].UserName, message, false);
                }
            }
        }
        function registerClientMethods(chatHub) {
            // Calls when user successfully logged in
            chatHub.client.onConnected = function (id, userName, allUsers, messages, countryCode, cultureCode) {
                $scope.$apply(function () { $scope.OnConnected(id, userName, allUsers, messages, countryCode, cultureCode); });
            }

            // On New User Connected
            chatHub.client.onNewUserConnected = function (id, userID, userName, countryCode, cultureCode) {
                $scope.$apply(function () { $scope.OnNewUserConnected(id, userID, userName, countryCode, cultureCode); });
            }

            // On User Disconnected
            chatHub.client.onUserDisconnected = function (id, userName) {
                $scope.$apply(function () { $scope.OnUserDisconnected(id, userName); });
            }

            chatHub.client.sendMessageTo = function (fromConnectionID, toConnectionID, message) {                
                $scope.$apply(function () { $scope.SendMessageTo(fromConnectionID, toConnectionID, message); });
            }

            chatHub.client.updateStatus = function (fromConnectionID, toConnectionID, message) {
                $scope.$apply(function () { $scope.UpdateStatus(fromConnectionID, toConnectionID, message); });
            }

            chatHub.client.sendResponseMessageTo = function (fromConnectionID, toConnectionID, message, responseID) {
                $scope.$apply(function () { $scope.SendResponseMessageTo(fromConnectionID, toConnectionID, message, responseID); });
            }

            chatHub.client.userAssingedTo = function (fromConnectionID, toConnectionID, message) {
                $scope.$apply(function () { $scope.UserAssingedTo(fromConnectionID, toConnectionID, message); });
            }

            chatHub.client.broadcastMessage = function (fromConnectionID, toConnectionID, message) {
                $scope.$apply(function () { $scope.BroadcastMessage(fromConnectionID, toConnectionID, message); });
            }          
        }

        function AddMessage(connectionID, userName, message, isSender, responseID) {
            $window = $('#' + connectionID);
            var messageTemplate = null;

            if (isSender) {
                messageTemplate = $('#templates #senderMessage').html();
            }
            else {
                messageTemplate = $('#templates #recieveMessage').html();
            }

            message = message.replace(/(?:\r\n|\r|\n)/g, '<br />');
            messageTemplate = messageTemplate.replace(/%%MESSAGE%%/g, message)
                                              .replace(/%%CURRENTTIME%%/g, getCurrentTime())
                                              .replace(/%%RESPONSEID%%/g, responseID);
            $window.find('#divMessage').append(messageTemplate);

            // set scrollbar
            var height = $window.find('#divMessage')[0].scrollHeight;
            $window.find('#divMessage').scrollTop(height);
        }

        function AddYesNoMessage(connectionID, userName, message, isSender, responseID) {
            $window = $('#' + connectionID);
            var messageTemplate = $('#templates #responseYesNo').html();
            messageTemplate = messageTemplate.replace(/%%MESSAGE%%/g, message)
                                             .replace(/%%CURRENTTIME%%/g, getCurrentTime())
                                             .replace(/%%RESPONSEID%%/g, responseID);

            $window.find('#divMessage').append(messageTemplate);
            //initialize events
            $window.find('#yes' + responseID).click(function clickYes(event) {
                $(event.target).attr('disabled', 'disabled');
                $(event.target).next('#no' + responseID).attr('disabled', 'disabled');
                $(event.target).off('click');
                $(event.target).next('#no' + responseID).off('click');
                chatHub.server.sendResponse(responseID, connectionID);
            });

            $window.find('#no' + responseID).click(function clickNo(event) {
                $(event.target).attr('disabled', 'disabled');;
                $(event.target).previous('#no' + responseID).attr('disabled', 'disabled');;
                $(event.target).off('click');
                $(event.target).previous('#no' + responseID).off('click');
            });

            // set scrollbar
            var height = $window.find('#divMessage')[0].scrollHeight;
            $window.find('#divMessage').scrollTop(height);
        }

        function getCurrentTime() {
            var currentTime;
            var currentDate = new Date();
            var hour = currentDate.getHours();
            var meridiem = hour >= 12 ? "PM" : "AM";
            currentTime = ((hour + 11) % 12 + 1) + ":" + currentDate.getMinutes() + meridiem;
            return currentTime;
        }

        $scope.events.close = function close(event) {
            if (parentContainer != null) {
                parentContainer.postMessage("HideWindow", "*");
            }
        }

        $scope.events.OpenHistoryChatWindow = function OpenHistoryChatWindow(event) {

            var historyWindowUrl = $scope.HostUrl + "Chat/ChatHistoryWindow?adminUserID=" + $scope.User.UserID;

            $.ajax({
                url: historyWindowUrl,
                type: 'GET',
                success: function () {
                }
            })
        }

        $scope.events.CloseChatWindow = function CloseChatWindow(event, isTitleRequired)
        {
            $('#divContainer').remove();

            if (isTitleRequired == 'True') {
                $(location).attr('href', $scope.HostUrl + "Chat/Login");
            }
        }

        $scope.events.Settings = function Settings(event, data) {
            $('.chat-space').each(function (index, element) {
                $(element).hide();
            });
            $window = $('#chatSettings');

            if ($window.length == 0) {
                OpenChatSettingsWindow();
            }
            else {
                $window.find('.chat-space').show();
            }
        }

        $scope.events.UserSelection = function UserSelection(event, data) {
            $('.chat-space').each(function (index, element) {
                $(element).hide();
            });

            $window = $('#' + data.ConnectionID);
            var filteredUser = JSLINQ($scope.FilteredUsers).Where(function (item) { return item.ConnectionID == data.ConnectionID; }).items;
            filteredUser[0].UreadMessages = 0;

            if ($window.length == 0) {
                OpenChatWindow(data.ConnectionID, data.UserName);
            }
            else {
                $window.show();
                $window.find('.chat-space').show();
                $textBox = $window.find("#txtPrivateMessage");
                $textBox.focus();
            }

            $('.contacts-list .contact-img-name').each(function (index, element) {
                $(element).removeClass("selected");
                $(element).closest("contact-img-name").removeClass("selected");
            });

            $(event.target).closest(".contact-img-name").addClass("selected");
        }

        $scope.events.RecentUserSelection = function RecentUserSelection(event, data) {
            $('.chat-space').each(function (index, element) {
                $(element).hide();
            });
            $window = $('#' + data.ConnectionID);
            var recentUser = JSLINQ($scope.RecentUsers).Where(function (item) { return item.ConnectionID == data.ConnectionID; }).items;
            recentUser[0].UreadMessages = 0;

            if ($window.length == 0) {
                OpenChatWindow(data.ConnectionID, data.UserName);
            }
            else {
                $window.show();
                $window.find('.chat-space').show();
                $textBox = $window.find("#txtPrivateMessage");
                $textBox.focus();
            }

            $('.contacts-list .contact-img-name').each(function (index, element) {
                $(element).removeClass("selected");
                $(element).closest("contact-img-name").removeClass("selected");
            });

            $(event.target).closest(".contact-img-name").addClass("selected");
        }
}]);