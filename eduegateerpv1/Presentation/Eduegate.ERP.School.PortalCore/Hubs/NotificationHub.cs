using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Eduegate.Platforms.SubscriptionManager;
using Eduegate.Services.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Eduegate.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        List<UserDTO> users = new List<UserDTO>();

        public NotificationHub()
        {
            SubscriptionHandler.Subscriptions += SubscriptionHandler_Subscriptions;
        }

        private void SubscriptionHandler_Subscriptions(object sender, SubscriptionDetail e)
        {
            switch (e.SubScriptionType)
            {
                case SubscriptionTypes.NewActivity:
                    SendMessage(e);
                    break;
            }
        }

        public void SendMessage(SubscriptionDetail message)
        {
            Clients.All.SendAsync(JsonConvert.SerializeObject(message));
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

    }
}