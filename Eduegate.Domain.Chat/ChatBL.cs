//namespace Eduegate.Domain.Chat
//{
//    public class ChatBL
//    {
//        private ChatRepository repository;
//        private SkienSuite.Framework.CallContext _callContext { get; set; }
//        protected dbSkienShoppingCartContext _dbContext;

//        public ChatBL(Eduegate.Framework.CallContext context, dbSkienShoppingCartContext dbContext)
//        {
//            _callContext = context;
//            _dbContext = dbContext;
//            repository = new ChatRepository();
//        }

//        public async Task<IDictionary<string, int>> UnReadMessageCount(long userID)
//        {
//            try
//            {
//                return await repository.UnReadMessageCount(userID);
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<BoilerplateBL>.Fatal(ex.Message, ex);
//                throw;
//            }
//        }

//        public async Task ChangeReadStatus(long messageID, bool status)
//        {
//            try
//            {
//                await repository.ChangeReadStatus(messageID, status);
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<BoilerplateBL>.Fatal(ex.Message, ex);
//                throw;
//            }
//        }

//        public async Task ChangeSendStatus(long messageID, bool status)
//        {
//            try
//            {
//                await repository.ChangeSendStatus(messageID, status);
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<BoilerplateBL>.Fatal(ex.Message, ex);
//                throw;
//            }
//        }

//        public async Task<List<ChatMessageDTO>> GetMessages()
//        {
//            try
//            {
//                var messages = await repository.GetMessages();
//                return ToMessageDTO(messages);
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<BoilerplateBL>.Fatal(ex.Message, ex);
//                throw;
//            }
//        }

//        public List<ChatMessageDTO> ToMessageDTO(List<ChatMessage> messages)
//        {
//            var dtos = new List<ChatMessageDTO>();

//            foreach (var message in messages)
//            {
//                dtos.Add(ToMessageDTO(message));
//            }

//            return dtos;
//        }

//        public ChatMessageDTO ToMessageDTO(ChatMessage message)
//        {
//            return new ChatMessageDTO()
//            {
//                ChatMessageIID = message.ChatMessageIID,
//                FromUserID = message.FromUserID,
//                FromUserEmail = message.FromUser?.EmailID,
//                FromUserName = message.FromUser?.UserName,
//                FromLoginID = message.FromUser?.LoginID,
//                Message = message.ChatMessage1,
//                ToUserID = message.ToUserID,
//                ToUserEmail = message.ToUser?.EmailID,
//                ToUserName = message.ToUser?.UserName,
//                ToLoginID = message.ToUser?.LoginID,
//                CreatedBy = message.CreatedBy,
//                CreatedDate = message.CreatedDate,
//                SendDateTimeUTC = message.SendDateTimeUTC,
//                ReadDateTimeUTC = message.ReadDateTimeUTC,
//                ReceiveDateTimeUTC = message.ReceiveDateTimeUTC,
//                UpdatedBy = message.UpdatedBy,
//                UpdatedDate = message.UpdatedDate,
//                ParentMessageID = message.ParentMessageID,
//                IsRead = message.IsRead.HasValue ? message.IsRead.Value : false,
//                IsSend = message.IsSend.HasValue ? message.IsSend.Value : false,
//                AttachmentID = message.AttachmentID
//            };
//        }

//        public async Task<List<ChatMessageDTO>> GetMessages(long userID, long? toUserID)
//        {
//            try
//            {
//                var dtos = new List<ChatMessageDTO>();
//                var messages = toUserID.HasValue ?
//                    await repository.GetMessages(userID, toUserID.Value) : await repository.GetMessages(userID);

//                foreach (var message in messages)
//                {
//                    dtos.Add(new ChatMessageDTO()
//                    {
//                        ChatMessageIID = message.ChatMessageIID,
//                        FromUserID = message.FromUserID,
//                        FromLoginID = message.FromUser?.LoginID,
//                        Message = message.ChatMessage1,
//                        ToUserID = message.ToUserID,
//                        ToLoginID = message.ToUser?.LoginID,
//                        CreatedDate = message.CreatedDate,
//                        UpdatedDate = message.UpdatedDate,
//                        CreatedBy = message.CreatedBy,
//                        UpdatedBy = message.UpdatedBy,
//                        ReadDateTimeUTC = message.ReadDateTimeUTC,
//                        SendDateTimeUTC = message.SendDateTimeUTC,
//                        ReceiveDateTimeUTC = message.ReceiveDateTimeUTC,
//                        IsRead = message.IsRead.HasValue ? message.IsRead.Value : false,
//                        IsSend = message.IsSend.HasValue ? message.IsSend.Value : false,
//                        AttachmentID = message.AttachmentID
//                    });
//                }

//                return dtos;
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<BoilerplateBL>.Fatal(ex.Message, ex);
//                return null;
//            }
//        }

//        public async Task<List<ChatMessageDTO>> SaveMessages(List<ChatMessageDTO> messages)
//        {
//            try
//            {
//                var entities = new List<ChatMessage>();

//                foreach (var message in messages)
//                {
//                    entities.Add(new ChatMessage()
//                    {
//                        ChatMessageIID = message.ChatMessageIID,
//                        FromUserID = message.FromUserID,
//                        ChatMessage1 = message.Message,
//                        ToUserID = message.ToUserID,
//                        CreatedBy = message.CreatedBy,
//                        CreatedDate = message.CreatedDate,
//                        SendDateTimeUTC = message.SendDateTimeUTC,
//                        ReadDateTimeUTC = message.ReadDateTimeUTC,
//                        ReceiveDateTimeUTC = message.ReceiveDateTimeUTC,
//                        UpdatedBy = message.UpdatedBy,
//                        UpdatedDate = message.UpdatedDate,
//                        ParentMessageID = message.ParentMessageID,
//                    });
//                }

//                var savedEntities = await repository.SaveMessages(entities);
//                return ToMessageDTO(await repository.GetMessages(savedEntities.Select(x => x.ChatMessageIID).ToList()));
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<ChatBL>.Fatal(ex.Message.ToString(), ex);
//            }

//            return null;
//        }

//        public async Task AddChatUsers(List<ChatUserDTO> users)
//        {
//            try
//            {
//                var entities = new List<ChatUser>();
//                foreach (var user in users)
//                {
//                    entities.Add(new ChatUser()
//                    {
//                        ChatUserID = user.ChatUserID,
//                        EmailID = user.EmailID,
//                        LoginID = user.LoginID,
//                        UserName = user.UserName,
//                        ExternalReference1 = user.ExternalReference1,
//                        ExternalReference2 = user.ExternalReference2,
//                    });
//                }

//                await repository.SaveUsers(entities);
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<ChatBL>.Fatal(ex.Message.ToString(), ex);
//            }
//        }

//        public async Task<ChatUserDTO> GetChatUser(string emailID)
//        {
//            try
//            {
//                var userEntity = await repository.GetUser(emailID);

//                if (userEntity == null)
//                {
//                    var userDetail = await new ShoppingCart.AccountBL(_callContext, _dbContext, null).GetUserDetails(emailID);
//                    userDetail.ProfileFile = new AccountBL(_callContext).GetUserProfileWithoutHost(userDetail.UserID);

//                    if (string.IsNullOrEmpty(System.IO.Path.GetFileName(userDetail.ProfileFile)))
//                    {
//                        userDetail.ProfileFile = null;
//                    }

//                    await repository.SaveUser(new ChatUser()
//                    {
//                        EmailID = userDetail.LoginEmailID,
//                        LoginID = userDetail.UserID,
//                        UserName = userDetail.UserName,
//                        ExternalReference1 = null,
//                        ExternalReference2 = null,
//                        UserProfile = userDetail.ProfileFile
//                    });

//                    userEntity = await repository.GetUser(emailID);
//                }

//                return new ChatUserDTO()
//                {
//                    ChatUserID = userEntity.ChatUserID,
//                    EmailID = userEntity.EmailID,
//                    ExternalReference1 = userEntity.ExternalReference1,
//                    ExternalReference2 = userEntity.ExternalReference2,
//                    LoginID = userEntity.LoginID,
//                    UserName = userEntity.UserName,
//                    UserProfile = userEntity.UserProfile,
//                };
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<BoilerplateBL>.Fatal(ex.Message, ex);
//                throw;
//            }
//        }

//        public async Task<ChatUserDTO> AddChatUser(ChatUserDTO user)
//        {
//            try
//            {
//                if (!user.LoginID.HasValue)
//                {
//                    var userDetail = await new ShoppingCart.AccountBL(_callContext, _dbContext, null).GetUserDetails(user.EmailID);
//                    user.LoginID = userDetail.UserID;
//                    user.UserName = userDetail.UserName;
//                }

//                if (string.IsNullOrEmpty(user.UserProfile))
//                {
//                    user.UserProfile = new AccountBL(_callContext).GetUserProfileWithoutHost(user.LoginID.Value);

//                    if (string.IsNullOrEmpty(System.IO.Path.GetFileName(user.UserProfile)))
//                    {
//                        user.UserProfile = null;
//                    }
//                }

//                var userEntity = await repository.SaveUser(new ChatUser()
//                {
//                    ChatUserID = user.ChatUserID,
//                    EmailID = user.EmailID,
//                    LoginID = user.LoginID,
//                    UserName = user.UserName,
//                    ExternalReference1 = user.ExternalReference1,
//                    ExternalReference2 = user.ExternalReference2,
//                    UserProfile = user.UserProfile
//                });

//                return new ChatUserDTO()
//                {
//                    ChatUserID = userEntity.ChatUserID,
//                    EmailID = userEntity.EmailID,
//                    ExternalReference1 = userEntity.ExternalReference1,
//                    ExternalReference2 = userEntity.ExternalReference2,
//                    LoginID = userEntity.LoginID,
//                    UserName = userEntity.UserName,
//                    UserProfile = userEntity.UserProfile
//                };
//            }
//            catch (Exception ex)
//            {
//                SkienSuite.Logger.LogHelper<ChatBL>.Fatal(ex.Message.ToString(), ex);
//                return null;
//            }
//        }
//    }
//}
