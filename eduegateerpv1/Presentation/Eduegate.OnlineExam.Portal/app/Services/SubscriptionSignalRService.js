angular
    .module('Eduegate.OnlineExam.Portal')
    .service('subscriptionService', subscription);

product.$inject = ['$http', '$subscription'];

function subscription($http, $subscription) {
    ////$.connection.hub.start();
    ////var subscriptions = {};
    //var instanceId = utility.generateUUID();

    //$.connection.notificationHub.client.sendMessage = function (details) {
    //    details = JSON.parse(details);
    //    switch (details.SubScriptionType) {
    //        case 1: // "NewActivity":
    //            $.each(subscriptions["newactivity"], function (index, data) {
    //                data.callback(details.Data);
    //            });
    //            break;
    //    }
    //}
    
    //var subscribe = function (setting, callback) {
    //    if (!subscriptions[setting.subscribeTo]) {
    //        subscriptions[setting.subscribeTo] = [];
    //    }

    //    subscriptions[setting.subscribeTo].push({
    //        'setting': setting, 'callback': callback
    //    });
    //};

    //return {
    //    subscribe: subscribe
    //};
}