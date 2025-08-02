using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkienSuite.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        List<UserDTO> users = new List<UserDTO>();

        public NotificationHub()
        {
            //SubscriptionHandler.Subscriptions += SubscriptionHandler_Subscriptions;
        }

        //private void SubscriptionHandler_Subscriptions(object sender, SubscriptionDetail e)
        //{
        //    switch (e.SubscriptionType)
        //    {
        //        case SubscriptionTypes.PrintToPrinter:
        //        case SubscriptionTypes.NewActivity:
        //            SendMessage(e).GetAwaiter().GetResult();
        //            break;
        //    }
        //}

        //public async Task MessageTo(string message)
        //{
        //    var data = JsonConvert.DeserializeObject<SubscriptionDetail>(message);
        //    data.SubscriptionTypeName = data.SubscriptionType.ToString();
        //    await SendMessage(data);
        //}

        //public async Task SendMessage(SubscriptionDetail message)
        //{
        //    message.SubscriptionTypeName = message.SubscriptionType.ToString();
        //    await this.Clients.All.SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //}

        //public override Task OnConnectedAsync()
        //{
        //    var httpContext = Context.GetHttpContext();
        //    var userName = !string.IsNullOrEmpty(Context.User.Identity.Name) ? Context.User.Identity.Name :
        //        httpContext.Request.Query["username"].ToString();
        //    var roleName = httpContext.Request.Query.ContainsKey("role") ?
        //        httpContext.Request.Query["role"].ToString() : null;

        //    users.Add(new UserDTO() { LoginUserID = Context.ConnectionId, UserName = userName, RoleName = roleName });

        //    SendMessage(new SubscriptionDetail()
        //    {
        //        SubscriptionType = SubscriptionTypes.NewActivity,
        //        Data = new ActivityDTO()
        //        {
        //            Description = "New login",
        //            CreatedUserName = userName,
        //            CreatedDate = DateTime.Now,
        //        }
        //    }).GetAwaiter().GetResult();

        //    //update everyone one user is online
        //    return base.OnConnectedAsync();
        //}

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
