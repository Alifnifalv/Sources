app.controller("SubscriptionController", ["$scope", "$http", "$compile","chat", "toaster", function ($scope, $http, $compile, $chat, $toaster) {
    console.log("Summary View Detail");

    $scope.messages = [];
    $http.get("GetLoginUserID").then(function (response) {
        $scope.LogonUserID = response.data;
    });

    $scope.sendMessage = function () {
        chat.server.sendMessage($scope.LogonUserID.data + ' Just modified records, please refresh page to get latest!');
        $scope.newMessage = "";
    };

    chat.client.newMessage = function onNewMessage(message) {
        toaster.pop('warning', "Please read message!", message);

        $scope.messages.push({ message: message });

        $scope.$apply();

        console.log(message);
    };

}]);