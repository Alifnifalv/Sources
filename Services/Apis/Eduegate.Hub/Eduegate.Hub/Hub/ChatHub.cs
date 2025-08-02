using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Chat;
using Eduegate.Services.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eduegate.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        // Store user connections with a dictionary
        private static readonly Dictionary<string, string> UserConnections = new();

        public override Task OnConnectedAsync()
        {
            // Retrieve user ID from the query string
            var userId = Context.GetHttpContext().Request.Query["userId"];

            // Map userId to connection ID
            if (!string.IsNullOrEmpty(userId))
            {
                UserConnections[userId] = Context.ConnectionId;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the connection ID from the dictionary on disconnect
            var userId = UserConnections.FirstOrDefault(u => u.Value == Context.ConnectionId).Key;
            if (userId != null)
            {
                UserConnections.Remove(userId);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(long commentId, long fromLoginId, long toLoginId, int CreatedBy, string message, bool isRead , long? parentCommentId, string parentCommentText ,long? photoContentID,int referenceID)
        {
            // Convert both IDs to strings for the dictionary lookup
            var fromUserId = fromLoginId.ToString();
            var toUserId = toLoginId.ToString();

            // Send message to `fromLoginId` if connected
            if (UserConnections.TryGetValue(fromUserId, out var fromConnectionId))
            {
                await Clients.Client(fromConnectionId).SendAsync("ReceiveMessage", commentId, fromLoginId, toLoginId, CreatedBy, message, isRead, parentCommentId, parentCommentText , photoContentID , referenceID);
            }

            // Send message to `toLoginId` if connected
            if (UserConnections.TryGetValue(toUserId, out var toConnectionId))
            {
                await Clients.Client(toConnectionId).SendAsync("ReceiveMessage", commentId, fromLoginId, toLoginId, CreatedBy, message, isRead, parentCommentId, parentCommentText , photoContentID , referenceID);
            }
        }


        public async Task NotifyParentListUpdated(long teacherLoginId)
        {
            var toUserId = teacherLoginId.ToString();

            if (UserConnections.TryGetValue(toUserId, out var toConnectionId))
            {

                await Clients.User(toConnectionId).SendAsync("ParentListUpdated", teacherLoginId);
            }
        }

        public async Task NotifyTeacherListUpdated(long parentLoginId)
        {
            var toUserId = parentLoginId.ToString();

            if (UserConnections.TryGetValue(toUserId, out var toConnectionId))
            {

                await Clients.User(toConnectionId).SendAsync("TeacherListUpdated", parentLoginId);
            }
        }

        public async Task NotifyMessageRead(int commentId, long toLoginId)
        {
            var toUserId = toLoginId.ToString();

            if (UserConnections.TryGetValue(toUserId, out var toConnectionId))
            {
                Console.WriteLine($"Sending MessageRead to connection {toConnectionId}");
                await Clients.Client(toConnectionId).SendAsync("MessageRead", commentId, toLoginId, true);
            }
            else
            {
                Console.WriteLine($"No connection ID found for user ID {toUserId}");
            }
        }

        //    static List<ChatUserDTO> users = new List<ChatUserDTO>();
        //    private static int _userCount = 0;
        //    protected dbSkienShoppingCartContext _dbContext;
        //    protected IBackgroundJobClient _backgroundJobs;

        //    public ChatHub(dbSkienShoppingCartContext dbContext, IBackgroundJobClient backgroundJobs)
        //    {
        //        this._dbContext = dbContext;
        //        this._backgroundJobs = backgroundJobs;
        //    }

        //    public async Task MessageTo(string message)
        //    {
        //        var data = JsonConvert.DeserializeObject<SubscriptionDetail>(message);
        //        data.SubscriptionTypeName = data.SubscriptionType.ToString();
        //        await SendMessage(data);
        //    }

        //    public async Task SendMessage(SubscriptionDetail message)
        //    {
        //        message.SubscriptionTypeName = message.SubscriptionType.ToString();
        //        var existUser = users.FirstOrDefault(x => x.EmailID == message.InitiaterEmailID);
        //        var receiverUser = users.FirstOrDefault(x => x.EmailID == message.ReceiverEmailID);
        //        var hostUrl = new SettingBL(null).GetSettingValue<string>("ImageHostUrl");

        //        switch (message.SubscriptionType)
        //        {
        //            case SubscriptionTypes.MessageReadStatus:
        //                var messageIDs = JsonConvert.DeserializeObject<List<long>>(message.Data.ToString());
        //                messageIDs.ForEach(x =>
        //                {
        //                    new ChatBL(null, _dbContext).ChangeReadStatus(x, true).GetAwaiter().GetResult();
        //                });
        //                break;
        //            case SubscriptionTypes.UserStatus:
        //                dynamic messageData = JsonConvert.DeserializeObject<dynamic>(message.Data.ToString());
        //                var userID = messageData.UserID.ToString();
        //                var exists = users.FirstOrDefault(x => x.EmailID == userID);
        //                bool status = true;

        //                if (exists == null)
        //                {
        //                    status = false;
        //                    exists = new ChatBL(null, _dbContext).GetChatUser(userID).GetAwaiter().GetResult();
        //                }

        //                message.Data = JsonConvert.SerializeObject(new
        //                {
        //                    UserID = userID,
        //                    UserName = messageData.UserName,
        //                    Status = status,
        //                    UserProfile = exists != null && !string.IsNullOrEmpty(exists?.UserProfile) ? string.Format("{0}/{1}", hostUrl, exists?.UserProfile) : null
        //                });

        //                await this.Clients.All.SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //                break;
        //            default:
        //                if (!string.IsNullOrEmpty(message.ReceiverEmailID))
        //                {
        //                    dynamic parsedMessage = JsonConvert.DeserializeObject<dynamic>(message.Data.ToString());
        //                    var toUser = users.FirstOrDefault(x => x.EmailID == message.ReceiverEmailID);

        //                    if (toUser == null)
        //                    {
        //                        var userDto = new ChatUserDTO()
        //                        {
        //                            ConnectionID = null,
        //                            UserName = parsedMessage.ToUserName,
        //                            EmailID = message.ReceiverEmailID,
        //                            LoginID = message.ReceiverLoginID,
        //                            UserProfile = !string.IsNullOrEmpty(toUser?.UserProfile) ? string.Format("{0}/{1}", hostUrl, toUser?.UserProfile) : null,
        //                        };

        //                        toUser = new ChatBL(null, _dbContext).AddChatUser(userDto).GetAwaiter().GetResult();
        //                        userDto.UserProfile = toUser.UserProfile;
        //                    }

        //                    if (existUser == null)
        //                    {
        //                        var userDto = new ChatUserDTO()
        //                        {
        //                            ConnectionID = null,
        //                            UserName = parsedMessage.FromUserName,
        //                            EmailID = message.InitiaterEmailID,
        //                            LoginID = message.InitiaterLoginID,
        //                            UserProfile = !string.IsNullOrEmpty(toUser?.UserProfile) ? string.Format("{0}/{1}", hostUrl, toUser?.UserProfile) : null
        //                        };

        //                        existUser = new ChatBL(null, _dbContext).AddChatUser(userDto).GetAwaiter().GetResult();
        //                        userDto.UserProfile = existUser.UserProfile;
        //                    }

        //                    message.ToUserProfile = toUser?.UserProfile != null ? string.Format("{0}/{1}", hostUrl, toUser?.UserProfile) : null;
        //                    message.FromUserProfile = existUser?.UserProfile != null ? string.Format("{0}/{1}", hostUrl, existUser?.UserProfile) : string.Empty;

        //                    var savedMessages = await new ChatBL(null, _dbContext).SaveMessages(new List<Services.Contracts.Chat.ChatMessageDTO>()
        //                    {
        //                        new Services.Contracts.Chat.ChatMessageDTO()
        //                        {
        //                            FromUserID = existUser?.ChatUserID,
        //                            FromUserName = parsedMessage.FromUserName,
        //                            FromUserEmail = message.InitiaterEmailID,
        //                            FromUserProfile = existUser?.UserProfile != null ? string.Format("{0}/{1}", hostUrl, existUser?.UserProfile) : string.Empty,

        //                            ToUserID = toUser?.ChatUserID,
        //                            ToUserEmail = message.ReceiverEmailID,
        //                            ToUserName= parsedMessage.ToUserName,
        //                            ToUserProfile = toUser?.UserProfile != null ? string.Format("{0}/{1}", hostUrl, toUser?.UserProfile) : null,

        //                            Message = JsonConvert.SerializeObject(message),
        //                            CreatedBy = int.Parse(existUser?.ChatUserID.ToString()),
        //                            UpdatedBy = int.Parse(existUser?.ChatUserID.ToString()),
        //                            CreatedDate = DateTime.Now,
        //                            UpdatedDate = DateTime.Now,
        //                            SendDateTimeUTC = DateTime.UtcNow,
        //                        }
        //                    });

        //                    var savedMessage = savedMessages.FirstOrDefault();
        //                    dynamic savedParsedMessage = JsonConvert.DeserializeObject<dynamic>(savedMessage.Message);

        //                    message.Data = JsonConvert.SerializeObject(savedParsedMessage.Data);

        //                    if (toUser != null)
        //                    {
        //                        await this.Clients.Client(toUser.ConnectionID)
        //                            .SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //                    }
        //                }
        //                else
        //                {
        //                    await this.Clients.All.SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //                }
        //                break;
        //        }
        //    }

        //    public override Task OnConnectedAsync()
        //    {
        //        long? loginID = null;
        //        var httpContext = Context.GetHttpContext();
        //        var userName = !string.IsNullOrEmpty(Context.User.Identity.Name) ? Context.User.Identity.Name :
        //            httpContext.Request.Query["name"].ToString();
        //        var toUser = httpContext.Request.Query["connect"].ToString();

        //        var userData = Context.User.Claims
        //              .FirstOrDefault(x => x.Type == ClaimTypes.UserData);

        //        if (userData == null) return base.OnConnectedAsync();

        //        var fullName = Context.User.Claims
        //              .FirstOrDefault(x => x.Type == "FullName");

        //        var supplier = Context.User.Claims
        //              .FirstOrDefault(x => x.Type == "Supplier");

        //        var profileUrl = Context.User.Claims
        //              .FirstOrDefault(x => x.Type == "ProfileUrl");

        //        var callContext = userData.Value;
        //        CallContext context = null;

        //        if (!string.IsNullOrEmpty(callContext))
        //        {
        //            context = JsonConvert.DeserializeObject<CallContext>(callContext);

        //            if (context != null)
        //            {
        //                loginID = context.LoginID;
        //            }
        //        }

        //        var roleName = httpContext.Request.Query.ContainsKey("role") ?
        //            httpContext.Request.Query["role"].ToString() : null;

        //        var userDto = new ChatUserDTO()
        //        {
        //            ConnectionID = Context.ConnectionId,
        //            UserName = fullName != null && !string.IsNullOrEmpty(fullName.Value) ? fullName.Value : userName,
        //            RoleName = roleName,
        //            EmailID = userName,
        //            LoginID = loginID,
        //            UserProfile = !string.IsNullOrEmpty(profileUrl?.Value) ? (supplier != null && !string.IsNullOrEmpty(supplier.Value) ? "Suppliers/" : "UserProfile/") + profileUrl?.Value : null
        //        };

        //        if (string.IsNullOrEmpty(userDto.UserProfile))
        //        {
        //            var userDetail = new AccountBL(null, _dbContext, _backgroundJobs).GetUserDetails(userName).GetAwaiter().GetResult();
        //            userDto.UserProfile = userDetail?.ProfileFile;
        //        }

        //        var newUser = new ChatBL(null, _dbContext).AddChatUser(userDto).GetAwaiter().GetResult();

        //        if (newUser != null)
        //        {
        //            userDto.ChatUserID = newUser.ChatUserID;
        //            userDto.UserProfile = newUser.UserProfile;

        //            users.Add(userDto);

        //            SendMessage(new SubscriptionDetail()
        //            {
        //                SubscriptionType = SubscriptionTypes.NewActivity,
        //                Data = new ActivityDTO()
        //                {
        //                    Description = "New login",
        //                    CreatedUserName = userName,
        //                    CreatedDate = DateTime.Now,
        //                }
        //            }).GetAwaiter().GetResult();

        //            if (!string.IsNullOrEmpty(toUser))
        //            {
        //                var message = httpContext.Request.Query["message"].ToString();
        //                var user = users.Find(x => x.UserName == toUser);
        //                if (user != null)
        //                {
        //                    this.Clients.Client(user.ConnectionID)
        //                        .SendAsync("updatemessage", message).GetAwaiter().GetResult();
        //                }

        //                var messageDatas = GetResolvedMessages(userDto.ChatUserID, null).GetAwaiter().GetResult();

        //                //send chat history
        //                this.Clients.Client(user.ConnectionID)
        //                        .SendAsync("updatemessage", JsonConvert.SerializeObject(messageDatas)).GetAwaiter().GetResult();

        //                var unreadCount = new ChatBL(null, _dbContext).UnReadMessageCount(user.ChatUserID).GetAwaiter().GetResult();
        //                this.Clients.Client(user.ConnectionID)
        //                        .SendAsync("updatemessage", JsonConvert.SerializeObject(new
        //                        {
        //                            SubscriptionType = 7,
        //                            Data = new
        //                            { UnReadCount = unreadCount.Select(x => new { UserEmail = x.Key, MessageCount = x.Value }) }
        //                        })).GetAwaiter().GetResult();
        //                SendStatusAllAvailableUsersStatus(user).GetAwaiter().GetResult();
        //            }
        //            else
        //            {
        //                var messageDatas = GetResolvedMessages(userDto.ChatUserID, null).GetAwaiter().GetResult();

        //                //send chat history
        //                this.Clients.Client(userDto.ConnectionID)
        //                        .SendAsync("updatemessage", JsonConvert.SerializeObject(messageDatas)).GetAwaiter().GetResult();

        //                var unreadCount = new ChatBL(null, _dbContext).UnReadMessageCount(userDto.ChatUserID).GetAwaiter().GetResult();
        //                this.Clients.Client(userDto.ConnectionID)
        //                        .SendAsync("updatemessage", JsonConvert.SerializeObject(new
        //                        {
        //                            SubscriptionType = 7,
        //                            Data = new { UnReadCount = unreadCount.Select(x => new { UserEmail = x.Key, MessageCount = x.Value }) }
        //                        })).GetAwaiter().GetResult();
        //                SendStatusAllAvailableUsersStatus(userDto).GetAwaiter().GetResult();
        //            }
        //        }

        //        SendStatusToAll(newUser, true).GetAwaiter().GetResult();
        //        _userCount++;
        //        //update everyone one user is online
        //        return base.OnConnectedAsync();
        //    }

        //    private async Task<List<string>> GetResolvedMessages(long userID, long? toUserID)
        //    {
        //        var messageDatas = new List<string>();
        //        var previousMessages = await new ChatBL(null, _dbContext).GetMessages(userID, toUserID);

        //        previousMessages.ForEach(x =>
        //        {
        //            dynamic parsedMessage = JsonConvert.DeserializeObject<dynamic>(x.Message);
        //            parsedMessage.Data.ChatMessageIID = x.ChatMessageIID;
        //            parsedMessage.Data.IsRead = x.IsRead;
        //            parsedMessage.Data.IsSend = x.IsSend;
        //            parsedMessage.Data.FromUserProfile = x.FromLoginID.HasValue ? new SkienSuite.Domain.Commons.AccountBL(null).GetUserProfile(x.FromLoginID.Value) : null;
        //            parsedMessage.Data.ToUserProfile = x.ToLoginID.HasValue ? new SkienSuite.Domain.Commons.AccountBL(null).GetUserProfile(x.ToLoginID.Value) : null;
        //            messageDatas.Add(JsonConvert.SerializeObject(parsedMessage));
        //        });
        //        return messageDatas;
        //    }

        //    private async Task SendStatusAllAvailableUsersStatus(ChatUserDTO user)
        //    {
        //        foreach (var eachUser in users)
        //        {
        //            var message = new SubscriptionDetail()
        //            {
        //                SubscriptionType = SubscriptionTypes.UserStatus
        //            };

        //            message.Data = JsonConvert.SerializeObject(new
        //            {
        //                UserID = eachUser.EmailID,
        //                UserName = eachUser.UserName,
        //                Status = true,
        //                UserProfile = eachUser.UserProfile
        //            });

        //            await this.Clients.Client(user.ConnectionID).SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //        }
        //    }

        //    private async Task SendStatusToAll(ChatUserDTO user, bool status)
        //    {
        //        if (user == null) return;

        //        var message = new SubscriptionDetail()
        //        {
        //            SubscriptionType = SubscriptionTypes.UserStatus
        //        };

        //        message.Data = JsonConvert.SerializeObject(new
        //        {
        //            UserID = user.EmailID,
        //            UserName = user.UserName,
        //            Status = status,
        //            UserProfile = user.UserProfile
        //        });

        //        await this.Clients.All.SendAsync("updatemessage", JsonConvert.SerializeObject(message));
        //    }

        //    public override Task OnDisconnectedAsync(Exception exception)
        //    {
        //        var user = users.FirstOrDefault(x => x.ConnectionID == Context.ConnectionId);
        //        SendStatusToAll(user, false).GetAwaiter().GetResult();
        //        users.Remove(user);
        //        _userCount--;
        //        return base.OnDisconnectedAsync(exception);
        //    }
    }
}
