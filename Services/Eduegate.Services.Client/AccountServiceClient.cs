using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain;

namespace Eduegate.Service.Client
{
    public class AccountServiceClient : BaseClient, IAccount
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string accountService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);

        public AccountServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            var bannerUri = string.Format("{0}/{1}", accountService, "GetKnowHowOptions");
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.KnowHowOptionDTO>>(bannerUri, _callContext, _logger);
        }

        public List<LoginDTO> LoginlistSagas()
        {
            var bannerUri = string.Format("{0}/{1}", accountService, "LoginlistSagas");
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.LoginDTO>>(bannerUri, _callContext, _logger);
        }



        public UserDTO SaveLogin(UserDTO user)
        {
            var url = string.Format("{0}/{1}", accountService, "SaveLogin");
            return ServiceHelper.HttpPostGetRequest<UserDTO>(url, user, _callContext, _logger);
        }

        public UserDTO RegisterUser(UserDTO user)
        {
            var url = string.Format("{0}/{1}", accountService, "RegisterUser");
            return ServiceHelper.HttpPostGetRequest<UserDTO>(url, user, _callContext, _logger);
        }

        public UserDTO GetUserData(UserDTO userDTO)
        {
            var url = string.Format("{0}/{1}", accountService, "GetUserData");
            return ServiceHelper.HttpPostGetRequest<UserDTO>(url, userDTO, _callContext, _logger);
        }
        public bool IsUserAvailable(string userName)
        {
            return true;
        }

        public bool Login(string loginEmailId, string password, string appId)
        {
            //string uri = accountService + "Login/" + loginEmailId + "/" + password + "/" + appId;
            ////uri = HttpUtility.UrlEncode(uri);
            //string result = ServiceHelper.HttpGetRequest(uri);
            //bool isvalid = Convert.ToBoolean(result);
            //return isvalid;

            var bannerUri = string.Format("{0}/{1}?loginEmailId={2}&password={3}&appId={4}", accountService, "Login", loginEmailId, password, appId);
            return ServiceHelper.HttpGetRequest<bool>(bannerUri, _callContext, _logger);
        }

        public string GetSaltHashByUserName(string emailId)
        {
            return "";
        }


        public Common ConfirmEmail(string userId)
        {
            string uri = string.Concat(accountService, "ConfirmEmail/", userId);
            string serviceResult = ServiceHelper.HttpGetRequest(uri);
            Common result = JsonConvert.DeserializeObject<Common>(serviceResult);
            return result;
        }

        public Common ResetPassword(UserDTO user)
        {
            string serviceResult = ServiceHelper.HttpPostRequest(string.Concat(accountService, "ResetPassword"), user);
            return JsonConvert.DeserializeObject<Common>(serviceResult);
        }

        public UserDTO GetUserDetails(string emailId)
        {
            string uri = string.Format("{0}/{1}/{2}", accountService, "GetUserDetails", emailId);
            return ServiceHelper.HttpGetRequest<UserDTO>(uri, _callContext);
        }

        public UserDTO GetUserDetailsByID(string loginID)
        {
            string uri = string.Format("{0}/{1}/{2}", accountService, "GetUserDetailsByID", loginID);
            return ServiceHelper.HttpGetRequest<UserDTO>(uri, _callContext);
        }

        public KeyValuePair<Common, string> ForgotPassword(string loginEmailId)
        {
            string uri = string.Format("{0}/{1}/{2}", accountService, "ForgotPassword", loginEmailId);
            string serviceResult = ServiceHelper.HttpGetRequest(uri);
            KeyValuePair<Common, string> result = JsonConvert.DeserializeObject<KeyValuePair<Common, string>>(serviceResult);
            return result;
        }

        public bool IsPasswordResetRequired(string loginEmailId)
        {
            return true;
        }

        public List<ContactDTO> GetBillingShippingContact(long customerID, AddressType addressType)
        {
            var Uri = string.Format("{0}/{1}?customerID={2}&addressType={3}", accountService, "GetBillingShippingContact", customerID, addressType);
            return ServiceHelper.HttpGetRequest<List<ContactDTO>>(Uri, _callContext, _logger);
        }


        public ContactDTO GetContactDetail(long contactID)
        {
            var Uri = string.Format("{0}/{1}?contactID={2}", accountService, "GetContactDetail", contactID);
            return ServiceHelper.HttpGetRequest<ContactDTO>(Uri, _callContext, _logger);
        }

        public bool isCustomerConfirmed(long customerID)
        {
            var bannerUri = string.Format("{0}/{1}?customerID={2}", accountService, "isCustomerConfirmed", customerID);
            return ServiceHelper.HttpGetRequest<bool>(bannerUri, _callContext, _logger);
        }

        public List<ContactDTO> GetShippingAddressContacts(long loginID, int siteID = 0)
        {
            var bannerUri = string.Format("{0}/{1}?loginID={2}&siteID={3}", accountService, "GetShippingAddressContacts", loginID, siteID);
            return ServiceHelper.HttpGetRequest<List<ContactDTO>>(bannerUri, _callContext, _logger);
        }

        public bool AddContact(CheckoutDTO checkoutDTO)
        {
            var uri = string.Format("{0}/AddContact", accountService);
            var result = ServiceHelper.HttpPostRequest(uri, checkoutDTO, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public bool RemoveContact(long contactID)
        {
            var bannerUri = string.Format("{0}/{1}?contactID={2}", accountService, "RemoveContact", contactID);
            return ServiceHelper.HttpGetRequest<bool>(bannerUri, _callContext, _logger);
        }

        public bool UpdateContact(ContactDTO contactDTO)
        {
            var uri = string.Format("{0}/UpdateContact", accountService);
            var result = ServiceHelper.HttpPostRequest(uri, contactDTO, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public ContactDTO GetBillingAddressContacts(long loginID)
        {
            var bannerUri = string.Format("{0}/{1}?loginID={2}", accountService, "GetBillingAddressContacts", loginID);
            return ServiceHelper.HttpGetRequest<ContactDTO>(bannerUri, _callContext, _logger);
        }

        public bool isUserAvailableEmailPhone(string emailID, string mobileNo)
        {
            var bannerUri = string.Format("{0}/{1}?emailID={2}&mobileNo={3}", accountService, "isUserAvailableEmailPhone", emailID, mobileNo);
            return ServiceHelper.HttpGetRequest<bool>(bannerUri, _callContext, _logger);
        }

        public UserDTO GetCustomerDetails(long customerID, bool securityInfo)
        {
            var uri = string.Format("{0}/{1}?customerID={2}&securityInfo={3}", accountService, "GetCustomerDetails", customerID, securityInfo);
            return ServiceHelper.HttpGetRequest<UserDTO>(uri, _callContext, _logger);
        }

        public List<OrderContactMapDTO> GetShippingAddressDetail(long customerID)
        {
            string uri = string.Format("{0}/GetShippingAddressDetail?customerID={1}", accountService, customerID);
            List<OrderContactMapDTO> ocmDTO = ServiceHelper.HttpGetRequest<List<OrderContactMapDTO>>(uri, _callContext, _logger);
            return ocmDTO;
        }

        public long GetLoginIDbyCustomerID(long customerID)
        {
            var bannerUri = string.Format("{0}/{1}?customerID={2}", accountService, "GetLoginIDbyCustomerID", customerID);
            return ServiceHelper.HttpGetRequest<long>(bannerUri, _callContext, _logger);
        }


        public UserDTO GetUserDetailsByCustomerID(long customerID)
        {
            var uri = string.Format("{0}/{1}?customerID={2}", accountService, "GetUserDetailsByCustomerID", customerID);
            return ServiceHelper.HttpGetRequest<UserDTO>(uri, _callContext, _logger);
        }

        public bool CheckCustomerEmailIDAvailability(long loginID, string loginEmailID)
        {
            var result = ServiceHelper.HttpGetRequest(accountService + "CheckCustomerEmailIDAvailability?loginID=" + loginID + "&loginEmailID=" + loginEmailID, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
        public bool CheckStudentEmailIDAvailability(long loginID, string EmailID)
        {
            var result = ServiceHelper.HttpGetRequest(accountService + "CheckStudentEmailIDAvailability?loginID=" + loginID + "&EmailID=" + EmailID, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public List<String> GetLoginReq()
        {
            var bannerUri = string.Format("{0}/{1}", accountService, "GetLoginReq");
            return ServiceHelper.HttpGetRequest<List<String>>(bannerUri, _callContext, _logger);
        }

        public NotifyMeDTO SaveNotify(NotifyMeDTO dto)
        {
            var result = ServiceHelper.HttpPostRequest(accountService + "SaveNotify", dto, _callContext);
            return JsonConvert.DeserializeObject<NotifyMeDTO>(result);
        }
    }
}
