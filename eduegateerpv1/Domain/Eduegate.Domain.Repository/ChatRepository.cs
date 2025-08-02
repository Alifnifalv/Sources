using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Services.Contracts.Chat;

namespace Eduegate.Domain.Repository
{
    public class ChatRepository
    {
        /// <summary>
        /// if user not exist then add 
        /// </summary>
        /// <param name="UserId">string</param>
        /// <param name="UserName">string</param>
        /// <returns>true/false</returns>
        public bool RegisterChatUser(string userId, string userName)
        {
            using (dbBlinkChatEntities db = new dbBlinkChatEntities())
            {
                bool has = db.Users.Any(x => x.UserID == userId);
                if (!has)
                {
                    User _User = new User
                    {
                        UserID = userId,
                        UserName = userName,
                        UserStatusID = 1,
                        Created = DateTime.Now
                    };

                    // Add the new object to the Users collection.
                    db.Users.Add(_User);

                    // Save the change to the database. 
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return true;
            }
        }

        public void LogChatMessages(string sender, string reciever, string message)
        {
            using (dbBlinkChatEntities db = new dbBlinkChatEntities())
            {
                long senderUserId = db.Users.Single(x => x.UserID == sender).UserIID;
                long recieverUserId = db.Users.FirstOrDefault(x => x.UserID == reciever).UserIID;
                ChatMessage _ChatMessage = new ChatMessage
                {
                    MessageBody = message,
                    SenderUserID = senderUserId,
                    ReceiverUserID = recieverUserId,
                    Created = DateTime.Now
                };

                // Add the new object to the Users collection.
                db.ChatMessages.Add(_ChatMessage);

                // Save the change to the database. 
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        public List<ChatMessage> GetChatHistoryDetails(string adminUserID)
        {
            List<ChatMessage> chatMessagesList = new List<ChatMessage>();
            ChatMessage chatMessage = null;

            using (dbBlinkChatEntities dbContext = new dbBlinkChatEntities())
            {
                //Bringing the user details to get the chatmessage
                var userDetail = (from user in dbContext.Users
                            where user.UserID == adminUserID
                            select new
                            {
                                user,
                            }).FirstOrDefault();

                //Bring the chatmessage based on the user iid
                var ChatMessages = (from chatMsg in dbContext.ChatMessages
                        where (chatMsg.SenderUserID == userDetail.user.UserIID || chatMsg.ReceiverUserID == userDetail.user.UserIID )
                        orderby chatMsg.Created ascending
                        select new
                        {
                            chatMsg.MessageBody,
                            chatMsg.MessageIID,
                            chatMsg.SenderUserID,
                            chatMsg.ReceiverUserID,
                            chatMsg.Created,
                            chatMsg.User.UserName,
                            chatMsg.User.UserID,
                            chatMsg.User.UserStatu.StatusIID,
                            chatMsg.User.UserStatu.Name
                        }).ToList();

                //Looping and adding the item to the chat message list
                foreach (var chat in ChatMessages)
                {
                    chatMessage = new ChatMessage();
                    chatMessage.User = new User();
                    chatMessage.User.UserStatu = new UserStatu();

                    chatMessage.MessageIID = chat.MessageIID;
                    chatMessage.MessageBody = chat.MessageBody;
                    chatMessage.SenderUserID = chat.SenderUserID;
                    chatMessage.ReceiverUserID = chat.ReceiverUserID;
                    chatMessage.Created = chat.Created;
                    chatMessage.User.UserID = chat.UserID;
                    chatMessage.User.UserName = chat.UserName;
                    chatMessage.User.UserStatu.StatusIID = chat.StatusIID;
                    chatMessage.User.UserStatu.Name = chat.Name;

                    chatMessagesList.Add(chatMessage);
                }
            }

            return chatMessagesList;
        }
    }
}
