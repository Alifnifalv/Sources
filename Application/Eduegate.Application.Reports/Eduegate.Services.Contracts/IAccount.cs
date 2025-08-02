using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccount" in both code and config file together.
    [ServiceContract]
    public interface IAccount
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveLogin")]
        UserDTO SaveLogin(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RegisterUser")]
        UserDTO RegisterUser(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserData")]
        UserDTO GetUserData(UserDTO userDTO);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "IsUserAvailable")]
        bool IsUserAvailable(string userName);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Login?loginEmailId={loginEmailId}&password={password}&appId={appId}")]
        bool Login(string loginEmailId, string password, string appId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSaltHashByUserName/{emailId}")]
        string GetSaltHashByUserName(string emailId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ConfirmEmail/{userId}")]
        Common ConfirmEmail(string userId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ResetPassword")]
        Common ResetPassword(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserDetails/{emailId}")]
        UserDTO GetUserDetails(string emailId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserDetailsByID/{loginID}")]
        UserDTO GetUserDetailsByID(string loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ForgotPassword/{loginEmailId}")]
        KeyValuePair<Common, string> ForgotPassword(string loginEmailId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "IsPasswordResetRequired/{loginEmailId}")]
        bool IsPasswordResetRequired(string loginEmailId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBillingShippingContact?customerID={customerID}&addressType={addressType}")]
        List<ContactDTO> GetBillingShippingContact(long customerID, AddressType addressType);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetContactDetail?contactID={contactID}")]
        ContactDTO GetContactDetail(long contactID);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetKnowHowOptions")]
        List<KnowHowOptionDTO> GetKnowHowOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "isCustomerConfirmed?customerID={customerID}")]
        bool isCustomerConfirmed(long customerID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShippingAddressContacts?loginID={loginID}&siteID={siteID}")]
        List<ContactDTO> GetShippingAddressContacts(long loginID, int siteID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddContact")]
        bool AddContact(CheckoutDTO checkoutDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveContact?contactID={contactID}")]
        bool RemoveContact(long contactID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateContact")]
        bool UpdateContact(ContactDTO contactDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBillingAddressContacts?loginID={loginID}")]
        ContactDTO GetBillingAddressContacts(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "isUserAvailableEmailPhone?emailID={emailID}&mobileNo={mobileNo}")]
        bool isUserAvailableEmailPhone(string emailID, string mobileNo);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCustomerDetails?customerID={customerID}&securityInfo={securityInfo}")]
        UserDTO GetCustomerDetails(long customerID, bool securityInfo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShippingAddressDetail?customerID={customerID}")]
        List<OrderContactMapDTO> GetShippingAddressDetail(long customerID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLoginIDbyCustomerID?customerID={customerID}")]
        long GetLoginIDbyCustomerID(long customerID);


        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserDetailsByCustomerID?customerID={customerID}")]
        UserDTO GetUserDetailsByCustomerID(long customerID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckCustomerEmailIDAvailability?loginID={loginID}&loginEmailID={loginEmailID}")]
        bool CheckCustomerEmailIDAvailability(long loginId, string loginEmailID);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckSudentEmailIDAvailability?loginID={loginID}&EmailID={EmailID}")]
        bool CheckStudentEmailIDAvailability(long loginId, string EmailID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveNotify")]
        NotifyMeDTO SaveNotify(NotifyMeDTO dto);

    }
}
