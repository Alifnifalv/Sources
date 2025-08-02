using Eduegate.Services.Contracts.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserAccountService" in both code and config file together.
    [ServiceContract]
    public interface IUserAccountService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SendOTPByEmiratesID?emirateID={emirateID}")]
        void SendOTPByEmiratesID(string emirateID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SendOTP?mobileNumber={mobileNumber}")]
        void SendOTP(string mobileNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidateOTP?emirateID={emirateID}&mobileNumber={mobileNumber}&otpText={otpText}")]
        bool ValidateOTP(string emirateID, string mobileNumber, string otpText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "IsValidShareHolder?shareholderID={shareholderID}")]
        string IsValidShareHolder(string shareholderID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUsers")]
        List<UserDTO> GetUsers();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUser?loginId={loginId}")]
        UserDTO GetUser(long loginId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RegisterUserDevice?token={token}&userType={userType}")]
        OperationResultDTO RegisterUserDevice(string token, string userType);
    }
}
