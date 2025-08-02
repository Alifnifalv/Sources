using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Eduegate.Logger;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Checkout;

namespace Eduegate.Services
{
    public class Account : BaseService, IAccount
    {
        public UserDTO RegisterUser(UserDTO user)
        {
            try
            {
                //string result = null;
                //LogHelper<UserDTO>.Info("Calling AddUser from service to business for user " + user.LoginEmailID);//"User "+user.LoginEmail+"has been succesfully registered"
                return new AccountBL(CallContext).RegisterUser(user);
                //if (user.IsNotNull())
                //    result = JsonConvert.SerializeObject(user);
                //return result;
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                throw exception;
            }
        }

        public UserDTO GetUserData(UserDTO userDTO)
        {
            try
            {
                //string result = null;
                //LogHelper<UserDTO>.Info("Calling AddUser from service to business for user " + user.LoginEmailID);//"User "+user.LoginEmail+"has been succesfully registered"
                return new AccountBL(CallContext).GetUserData(userDTO);
                //if (user.IsNotNull())
                //    result = JsonConvert.SerializeObject(user);
                //return result;
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                throw exception;
            }
        }

        public bool IsUserAvailable(string userName)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    return new AccountBL(CallContext).IsUserAvailable(userName);
                }
                return false;
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public bool Login(string loginEmailId, string password, string appId)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginEmailId))
                {
                    return new AccountBL(CallContext).Login(loginEmailId, password, Convert.ToByte(appId));
                }
                return false;
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public string GetSaltHashByUserName(string userName)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    return new AccountBL(CallContext).GetSaltHashByUserName(userName);
                }
                return null;
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                return null;
            }
        }

        public Common ConfirmEmail(string userId)
        {
            return new AccountBL(CallContext).ConfirmEmail(userId);
        }

        public KeyValuePair<Common, string> ForgotPassword(string loginEmailId)
        {
            return new AccountBL(CallContext).ForgotPassword(loginEmailId);
        }

        public Common ResetPassword(UserDTO user)
        {
            return new AccountBL(CallContext).ResetPassword(user);
        }

        public UserDTO GetUserDetails(string loginEmailID)
        {
            try
            {
                return new AccountBL(CallContext).GetUserDetails(loginEmailID);
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                return null;
            }

        }

        public bool IsPasswordResetRequired(string loginEmailID)
        {
            return new AccountBL(CallContext).IsPasswordResetRequired(loginEmailID);
        }

        public UserDTO GetUserDetailsByID(string loginID)
        {
            try
            {
                return new AccountBL(CallContext).GetUserDetailsByID(long.Parse(loginID));
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                return null;
            }

        }

        public UserDTO SaveLogin(UserDTO user)
        {
            return new AccountBL(CallContext).SaveLogin(user);
        }

        public List<ContactDTO> GetBillingShippingContact(long customerID, AddressType addressType)
        {
            return new AccountBL(CallContext).GetBillingShippingContact(customerID, addressType);
        }

        public ContactDTO GetContactDetail(long contactID)
        {
            return new AccountBL(CallContext).GetContactDetail(contactID);
        }

        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            return new AccountBL(CallContext).GetKnowHowOptions();
        }

        public bool isCustomerConfirmed(long customerID)
        {
            try
            {
                return new AccountBL(CallContext).isCustomerConfirmed(customerID);
            }
            catch (Exception exception)
            {
                LogHelper<bool>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public List<ContactDTO> GetShippingAddressContacts(long loginID, int siteID = 0)
        {
            try
            {
                return new AccountBL(CallContext).GetShippingAddressContacts(loginID, siteID);
            }
            catch (Exception exception)
            {
                LogHelper<List<ContactDTO>>.Fatal(exception.Message, exception);
                return new List<ContactDTO>();
            }
        }

        public ContactDTO GetBillingAddressContacts(long loginID)
        {
            try
            {
                return new AccountBL(CallContext).GetBillingAddressContacts(loginID);
            }
            catch (Exception exception)
            {
                LogHelper<List<ContactDTO>>.Fatal(exception.Message, exception);
                return new ContactDTO();
            }
        }

        public bool AddContact(CheckoutDTO checkoutDTO)
        {
            try
            {
                return new AccountBL(CallContext).AddContact(checkoutDTO.contactDTO, checkoutDTO.callContext);
            }
            catch (Exception exception)
            {
                LogHelper<bool>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public bool RemoveContact(long contactID)
        {
            try
            {
                return new AccountBL(CallContext).RemoveContact(contactID);
            }
            catch (Exception exception)
            {
                LogHelper<bool>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public bool UpdateContact(ContactDTO contactDTO)
        {
            try
            {
                return new AccountBL(CallContext).UpdateContact(contactDTO);
            }
            catch (Exception exception)
            {
                LogHelper<bool>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public bool isUserAvailableEmailPhone(string emailID, string mobileNo)
        {
            try
            {
                return new AccountBL(CallContext).isUserAvailableEmailPhone(emailID, mobileNo);
            }
            catch (Exception exception)
            {
                LogHelper<UserDTO>.Fatal(exception.Message, exception);
                return false;
            }
        }

        public UserDTO GetCustomerDetails(long customerID, bool securityInfo)
        {
            try
            {
                return new AccountBL(CallContext).GetUserDetailsByCustomerID(customerID, securityInfo);
            }
            catch (Exception exception)
            {
                LogHelper<Account>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<OrderContactMapDTO> GetShippingAddressDetail(long customerID)
        {
            try
            {
                List<OrderContactMapDTO> ocmDTO = new AccountBL(CallContext).GetShippingAddressDetail(customerID);
                return ocmDTO;
            }
            catch (Exception exception)
            {
                LogHelper<Account>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long GetLoginIDbyCustomerID(long customerID)
        {
            try
            {
                return new AccountBL(CallContext).GetLoginIDbyCustomerID(customerID);
            }
            catch (Exception exception)
            {
                LogHelper<Account>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public UserDTO GetUserDetailsByCustomerID(long customerID)
        {
            try
            {
                return new AccountBL(CallContext).GetUserDetailsByCustomerID(customerID, false);
            }
            catch (Exception exception)
            {
                LogHelper<Account>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool CheckCustomerEmailIDAvailability(long loginID, string loginEmailID)
        {
            return new AccountBL(this.CallContext).CheckCustomerEmailIDAvailability(loginID, loginEmailID);
        }
        public bool CheckStudentEmailIDAvailability(long loginID, string EmailID)
        {
            return new AccountBL(this.CallContext).CheckStudentEmailIDAvailability(loginID, EmailID);
        }

        public NotifyMeDTO SaveNotify(NotifyMeDTO dto)
        {
            return new AccountBL(this.CallContext).NotifyMe(dto);
        }
    }
}