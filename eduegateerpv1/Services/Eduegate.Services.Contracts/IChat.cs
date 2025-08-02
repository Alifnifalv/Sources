using System.Collections.Generic;
using Eduegate.Services.Contracts.Chat;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChat" in both code and config file together.
    public interface IChat
    {
        List<ChatHistoryDTO> GetChatHistoryDetails(string userID);

        UserDTO PasswordSignIn(string userID, string password);
    }
}