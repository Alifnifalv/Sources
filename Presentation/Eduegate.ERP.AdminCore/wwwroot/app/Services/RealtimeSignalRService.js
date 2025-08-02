angular
    .module('Eduegate.ERP.Admin')
    .service('realtimeService', realtimeSubscription);

realtimeSubscription.$inject = ['$http', '$subscription'];

function realtimeSubscription($http, $subscription) {
    if (!signalR || !window.RealtimeHubHost) return;
    let connection;
    var subscriptions = {}
    var instanceId = utility.generateUUID()

    try {
        connection = new signalR.HubConnectionBuilder()
            .withUrl(window.RealtimeHubHost + "?name=" + window.UserName + "&loginid=" + window.LoginID)
            .configureLogging(signalR.LogLevel.Information)
            .build();
    } catch(ex) {
        console.log('Realtime Hub error occured');
    }

    connection.on('updatemessage', (details) => {
        details = JSON.parse(details);
        switch (details.SubscriptionType) {
            case 1: // "NewActivity":
                $.each(subscriptions.newactivity, function (index, data) {
                    data.callback([details.Data])
                });
                break;
            case 11: // "UserActive":
                $.each(subscriptions.realtimedata, function (index, data) {
                    data.callback([details.Data])
                });
                break;
        }
    });

    connection.onclose(() => {
        console.log("Connection closed. Attempting to reconnect on realtime hub...");
        startConnection();
    });

    function startConnection() {
        connection.start({ withCredentials: false })
            .then(() => console.log('Realtime Hub connected!'))
            .catch((error) => {
                console.error(`Failed to start connection : Realtime hub: ${error}`);
                setTimeout(() => startConnection(), 5000); // Retry after 5 seconds
            });
    }

    startConnection();

    var subscribe = function (setting, callback) {
        if (!subscriptions[setting.subscribeTo]) {
            subscriptions[setting.subscribeTo] = []
        }

        subscriptions[setting.subscribeTo].push({
            setting: setting, callback: callback
        })
    }

    return {
        subscribe: subscribe
    }
};
