using Hangfire;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
//using Eduegate.Domain.Settings;
//using Eduegate.Framework;
//using Eduegate.Platforms.SubscriptionManager;
using Eduegate.Services.Contracts.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkienSuite.Hub
{
    public class MintoringHub : Microsoft.AspNetCore.SignalR.Hub
    {
        //static List<ChatUserDTO> users = new List<ChatUserDTO>();
        private static int _userCount = 0;
        protected IBackgroundJobClient _backgroundJobs;

        public MintoringHub(IBackgroundJobClient backgroundJobs)
        {
            this._backgroundJobs = backgroundJobs;
        }

        //public async Task MessageTo(string message)
        //{
        //    var data = JsonConvert.DeserializeObject<SubscriptionDetail>(message);
        //    data.SubscriptionTypeName = data.SubscriptionType.ToString();
        //    await SendMessage(data);
        //}

        //public async Task SendMessage(SubscriptionDetail message)
        //{
        //    message.SubscriptionTypeName = message.SubscriptionType.ToString();
        //    var existUser = users.FirstOrDefault(x => x.EmailID == message.InitiaterEmailID);
        //    var receiverUser = users.FirstOrDefault(x => x.EmailID == message.ReceiverEmailID);
        //    var hostUrl = new SettingBL(null).GetSettingValue<string>("ImageHostUrl");

        //    switch (message.SubscriptionType)
        //    {
        //        case SubscriptionTypes.MessageReadStatus:
        //            var messageIDs = JsonConvert.DeserializeObject<List<long>>(message.Data.ToString());
        //            messageIDs.ForEach(x =>
        //            {
        //            });
        //            break;
        //        case SubscriptionTypes.UserStatus:
        //            dynamic messageData = JsonConvert.DeserializeObject<dynamic>(message.Data.ToString());
        //            var userID = messageData.UserID.ToString();
        //            var exists = users.FirstOrDefault(x => x.EmailID == userID);
        //            bool status = true;

        //            if (exists == null)
        //            {
        //                status = false;
        //            }

        //            message.Data = JsonConvert.SerializeObject(new
        //            {
        //                UserID = userID,
        //                UserName = messageData.UserName,
        //                Status = status,
        //                UserProfile = exists != null && !string.IsNullOrEmpty(exists?.UserProfile) ? string.Format("{0}/{1}", hostUrl, exists?.UserProfile) : null
        //            });

        //            await this.Clients.All.SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //            break;
        //        default:
        //            if (!string.IsNullOrEmpty(message.ReceiverEmailID))
        //            {
        //                dynamic parsedMessage = JsonConvert.DeserializeObject<dynamic>(message.Data.ToString());
        //                var toUser = users.FirstOrDefault(x => x.EmailID == message.ReceiverEmailID);

        //                if (toUser == null)
        //                {

        //                }

        //                if (existUser == null)
        //                {

        //                }

        //                message.ToUserProfile = toUser?.UserProfile != null ? string.Format("{0}/{1}", hostUrl, toUser?.UserProfile) : null;
        //                message.FromUserProfile = existUser?.UserProfile != null ? string.Format("{0}/{1}", hostUrl, existUser?.UserProfile) : string.Empty;


        //                if (toUser != null)
        //                {
        //                    await this.Clients.Client(toUser.ConnectionID)
        //                        .SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //                }
        //            }
        //            else
        //            {
        //                await this.Clients.All.SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //            }
        //            break;
        //    }
        //}

        //public override Task OnConnectedAsync()
        //{
        //    long? loginID = null;
        //    var httpContext = Context.GetHttpContext();
        //    var userName = !string.IsNullOrEmpty(Context.User.Identity.Name) ? Context.User.Identity.Name :
        //        httpContext.Request.Query["name"].ToString();
        //    var toUser = httpContext.Request.Query["connect"].ToString();

        //    var userData = Context.User.Claims
        //          .FirstOrDefault(x => x.Type == ClaimTypes.UserData);

        //    if (userData == null) return base.OnConnectedAsync();

        //    var fullName = Context.User.Claims
        //          .FirstOrDefault(x => x.Type == "FullName");

        //    var supplier = Context.User.Claims
        //          .FirstOrDefault(x => x.Type == "Supplier");

        //    var profileUrl = Context.User.Claims
        //          .FirstOrDefault(x => x.Type == "ProfileUrl");

        //    var callContext = userData.Value;
        //    CallContext context = null;

        //    if (!string.IsNullOrEmpty(callContext))
        //    {
        //        context = JsonConvert.DeserializeObject<CallContext>(callContext);

        //        if (context != null)
        //        {
        //            loginID = context.LoginID;
        //        }
        //    }

        //    var roleName = httpContext.Request.Query.ContainsKey("role") ?
        //        httpContext.Request.Query["role"].ToString() : null;
        //    _userCount++;

        //    return base.OnConnectedAsync();
        //}

   

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    var user = users.FirstOrDefault(x => x.ConnectionID == Context.ConnectionId);
        //    //SendStatusToAll(user, false).GetAwaiter().GetResult();
        //    users.Remove(user);
        //    _userCount--;
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
