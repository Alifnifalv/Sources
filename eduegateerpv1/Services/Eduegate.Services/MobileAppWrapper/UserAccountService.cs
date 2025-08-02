using Eduegate.Domain;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.MobileAppWrapper;

namespace Eduegate.Services.MobileAppWrapper
{
    public class UserAccountService : BaseService, IUserAccountService
    {
        public UserAccountService(CallContext context)
        {
            CallContext = context;
        }

        public string IsValidShareHolder(string shareholderID)
        {
            //return new CustomerBL(CallContext).IsValidShareHolder(shareholderID);
            return null;
        }

        public void SendOTP(string mobileNumber)
        {
            var otpText = new NotificationBL(CallContext).SendOTP(mobileNumber);
            new CustomerBL(CallContext).SaveOTP(mobileNumber, otpText);
        }

        public void SendOTPByEmiratesID(string shareHolderID)
        {
            //var mobileNumber = new CustomerBL(CallContext).GetCustomerMobileNumberByShareHolderID(shareHolderID); 
            //new NotificationBL(CallContext).SendOTP(mobileNumber);
        }

        public bool ValidateOTP(string emirateID, string mobileNumber, string otpText)
        {
            return new CustomerBL(CallContext).IsValidateOTP(emirateID, mobileNumber, otpText);
        }

        public List<UserDTO> GetUsers()
        {
            return new AccountBL(CallContext).GetUsers();
        }

        public UserDTO GetUser(long loginId)
        {
            return new AccountBL(CallContext).GetUser(loginId);
        }

        public OperationResultDTO RegisterUserDevice(string token, string userType)
        {
            OperationResultDTO message = new OperationResultDTO()
            {
                operationResult = OperationResult.Success,
                Message = ""
            };

            var UserDeviceMapID = new UserServiceBL(CallContext).RegisterUserDevice(token, userType);

            //Check the user is active or not based on userType
            if (userType == "Staff")
            {
                if (UserDeviceMapID.HasValue)
                {
                    var data = new UserServiceBL(CallContext).CheckUserDeviceAvailability(token);

                    message = new OperationResultDTO()
                    {
                        operationResult = data.operationResult,
                        Message = data.Message
                    };
                }
            }
            return message;
        }

    }
}
