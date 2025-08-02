angular
    .module('Eduegate.ERP.Admin')
    .service('subscriptionService', subscription);

product.$inject = ['$http', '$subscription'];

function subscription($http, $subscription) {
    try {
        if (!signalR || !window.NotificationHost) return;    
   
        var signalRConnection;
        var subscriptions = {}
        var instanceId = utility.generateUUID()

        try {
            signalRConnection = new signalR.HubConnectionBuilder()
                .withUrl(window.NotificationHost + "?name=" + window.UserName + "&loginid=" + window.LoginID)
                .configureLogging(signalR.LogLevel.Information)
                .build();
        } catch (ex) {
            console.log('Hub error occured');
        }

        signalRConnection.on('updatemessage', (details) => {
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

        signalRConnection.onclose(() => {
            console.log("Connection closed. Attempting to reconnect on notification hub...");
            startConnection();
        });
    } catch (ex) {
        console.log('Hub error occured');
        return;
    }

    function startConnection() {
        if (signalRConnection) {
            signalRConnection.start({ withCredentials: false })
                .then(() => console.log('Hub connected on notification hub!'))
                .catch((error) => {
                    console.error(`Failed to start connection on notification hub: ${error}`);
                    setTimeout(() => startConnection(), 5000); // Retry after 5 seconds
                });
        }
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
