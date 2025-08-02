using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Chat;

namespace Eduegate.Domain
{
    public class ChatBL
    {
        private ChatRepository chatRepository = new ChatRepository();
        private UserMasterRepository userRepository = new UserMasterRepository();
        private CallContext _context;

        public ChatBL(CallContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get chat history details
        /// </summary>
        /// <returns>List of chat  history</returns>
        public List<ChatHistoryDTO> GetChatHistoryDetails(string adminUserID)
        {
            ChatHistoryDTO chatHistory = null;
            List<ChatHistoryDTO> chatHistoryDetails = new List<ChatHistoryDTO>();

            List<ChatMessage> chatMessages = chatRepository.GetChatHistoryDetails(adminUserID);

            foreach (ChatMessage chatMsg in chatMessages)
            {
                chatHistory = new ChatHistoryDTO();

                chatHistory.CommunicationMessage = chatMsg.MessageBody;
                chatHistory.MessageIID = chatMsg.MessageIID;
                chatHistory.SenderUserID = chatMsg.SenderUserID;
                chatHistory.ReceiverUserID = chatMsg.ReceiverUserID;
                chatHistory.Date = chatMsg.Created;

                if (chatMsg.User != null)
                {
                    chatHistory.UserID = chatMsg.User.UserID;
                    chatHistory.UserName = chatMsg.User.UserName;

                    if (chatMsg.User.UserStatu != null)
                    {
                        chatHistory.UserStatus = chatMsg.User.UserStatu.Name;
                        chatHistory.UserStatusIID = chatMsg.User.UserStatu.StatusIID;
                    }
                }

                chatHistoryDetails.Add(chatHistory);
            }

            return chatHistoryDetails;
        }

        public UserDTO PasswordSignIn(string userID, string password)
        {
            var accountBL = new AccountBL(_context);

            if (!accountBL.Login(userID, password, 1))
            {
                  return null;
            }

            var userDetails = accountBL.GetUserDetails(userID);
            //var userRole = userRepository.UserRole(userDetails.UserID);
            return new UserDTO() { UserID = long.Parse(userDetails.LoginID), LoginID = userDetails.LoginID.ToString(), UserName = userDetails.UserName, RoleName = "Operator" };
        }
    }
}
