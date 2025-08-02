using Eduegate.Services.Contracts.Commons;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    [ServiceContract]
    public interface IAppSecurityService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Login")]
        OperationResultDTO Login(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ParentLogin")]
        OperationResultDTO ParentLogin(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "StaffLogin")]
        OperationResultDTO StaffLogin(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserDetails")]
        UserDTO GetUserDetails();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateApiKey?uuid={uuid}&version={version}")]
        string GenerateApiKey(string uuid, string version);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "LogOut")]
        UserDTO LogOut();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ResetPasswordOTPGenerate?emailID={emailID}")]
        OperationResultDTO ResetPasswordOTPGenerate(string emailID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ResetPasswordVerifyOTP?OTP={OTP}&email={email}")]
        OperationResultDTO ResetPasswordVerifyOTP(string OTP, string email);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubmitPasswordChange?email={email}&password={password}")]
        OperationResultDTO SubmitPasswordChange(string email, string password);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VisitorLogin")]
        OperationResultDTO VisitorLogin(UserDTO user);

    }
}