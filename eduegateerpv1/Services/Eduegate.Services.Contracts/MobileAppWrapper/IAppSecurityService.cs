using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    public interface IAppSecurityService
    {
        OperationResultDTO Login(UserDTO user);

        OperationResultDTO ParentLogin(UserDTO user);

        OperationResultDTO StaffLogin(UserDTO user);

        UserDTO GetUserDetails();

        string GenerateApiKey(string uuid, string version);

        UserDTO LogOut();

        OperationResultDTO ResetPasswordOTPGenerate(string emailID);

        OperationResultDTO ResetPasswordVerifyOTP(string OTP, string email);

        OperationResultDTO SubmitPasswordChange(string email, string password);

        OperationResultDTO VisitorLogin(UserDTO user);

    }
}