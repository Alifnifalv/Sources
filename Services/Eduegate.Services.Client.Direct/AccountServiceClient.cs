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
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class AccountServiceClient : IAccount
    {
        Account service = new Account();

        public AccountServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            return service.GetKnowHowOptions();
        }

        public List<LoginDTO> LoginlistSagas()
        {
            //return service.LoginlistSagas();
            return null;
        }

        public UserDTO SaveLogin(UserDTO user)
        {
            return service.SaveLogin(user);
        }

        public UserDTO RegisterUser(UserDTO user)
        {
            return service.RegisterUser(user);
        }
        public UserDTO GetUserData(UserDTO userDTO)
        {
            return service.GetUserData(userDTO);
        }

        public bool IsUserAvailable(string userName)
        {
            return service.IsUserAvailable(userName);
        }

        public bool Login(string loginEmailId, string password, string appId)
        {
            return service.Login(loginEmailId, password, appId);
        }

        public string GetSaltHashByUserName(string emailId)
        {
            return service.GetSaltHashByUserName(emailId);
        }


        public Common ConfirmEmail(string userId)
        {
            return service.ConfirmEmail(userId);
        }

        public Common ResetPassword(UserDTO user)
        {
            return service.ResetPassword(user);
        }

        public UserDTO GetUserDetails(string emailId)
        {
            return service.GetUserDetails(emailId);
        }

        public UserDTO GetUserDetailsByID(string loginID)
        {
            return service.GetUserDetailsByID(loginID);
        }

        public KeyValuePair<Common, string> ForgotPassword(string loginEmailId)
        {
            return service.ForgotPassword(loginEmailId);
        }

        public bool IsPasswordResetRequired(string loginEmailId)
        {
            return service.IsPasswordResetRequired(loginEmailId);
        }

        public List<ContactDTO> GetBillingShippingContact(long customerID, AddressType addressType)
        {
            return service.GetBillingShippingContact(customerID, addressType);
        }


        public ContactDTO GetContactDetail(long contactID)
        {
            return service.GetContactDetail(contactID);
        }

        public bool isCustomerConfirmed(long customerID)
        {
            return service.isCustomerConfirmed(customerID);
        }

        public List<ContactDTO> GetShippingAddressContacts(long loginID, int siteID = 0)
        {
            return service.GetShippingAddressContacts(loginID, siteID);
        }

        public bool AddContact(CheckoutDTO checkoutDTO)
        {
            return service.AddContact(checkoutDTO);
        }

        public bool RemoveContact(long contactID)
        {
            return service.RemoveContact(contactID);
        }

        public bool UpdateContact(ContactDTO contactDTO)
        {
            return service.UpdateContact(contactDTO);
        }

        public ContactDTO GetBillingAddressContacts(long loginID)
        {
            return service.GetBillingAddressContacts(loginID);
        }

        public bool isUserAvailableEmailPhone(string emailID, string mobileNo)
        {
            return service.isUserAvailableEmailPhone(emailID, mobileNo);
        }

        public UserDTO GetCustomerDetails(long customerID, bool securityInfo)
        {
            return service.GetCustomerDetails(customerID, securityInfo);
        }

        public List<OrderContactMapDTO> GetShippingAddressDetail(long customerID)
        {
            return service.GetShippingAddressDetail(customerID);
        }

        public long GetLoginIDbyCustomerID(long customerID)
        {
            return service.GetLoginIDbyCustomerID(customerID);
        }


        public UserDTO GetUserDetailsByCustomerID(long customerID)
        {
            return service.GetUserDetailsByCustomerID(customerID);
        }

        public bool CheckCustomerEmailIDAvailability(long loginID, string loginEmailID)
        {
            return service.CheckCustomerEmailIDAvailability(loginID, loginEmailID);
        }
        public bool CheckStudentEmailIDAvailability(long loginID, string EmailID)
        {
            return service.CheckStudentEmailIDAvailability(loginID, EmailID);
        }

        public List<String> GetLoginReq()
        {
            //return service.GetLoginReq();
            return null;
        }

        public NotifyMeDTO SaveNotify(NotifyMeDTO dto)
        {
            return service.SaveNotify(dto);
        }
    }
}
