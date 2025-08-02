using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Eduegate.Platforms.SubscriptionManager;

namespace Eduegate.Services.Notification.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
            SubscriptionHandler.Subscriptions += SubscriptionHandler_Subscriptions;
        }

        private void SubscriptionHandler_Subscriptions(object sender, SubscriptionDetail e)
        {
            switch(e.SubScriptionType)
            {
                case SubscriptionTypes.NewActivity:
                    SendMessage(e);
                    break;
            }
        }

        public void SendMessage(SubscriptionDetail message)
        {
            Clients.All.sendMessage(JsonConvert.SerializeObject(message));
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}