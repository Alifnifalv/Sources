using Eduegate.Services.Contracts.Commons;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserAccountService" in both code and config file together.
    public interface IUserAccountService
    {
        void SendOTPByEmiratesID(string emirateID);

        void SendOTP(string mobileNumber);

        bool ValidateOTP(string emirateID, string mobileNumber, string otpText);

        string IsValidShareHolder(string shareholderID);

        List<UserDTO> GetUsers();

        UserDTO GetUser(long loginId);

        OperationResultDTO RegisterUserDevice(string token, string userType);
    }
}