using System.Collections.Generic;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccount" in both code and config file together.
    public interface IAccount
    {
        UserDTO SaveLogin(UserDTO user);

        UserDTO RegisterUser(UserDTO user);

        UserDTO GetUserData(UserDTO userDTO);

        bool IsUserAvailable(string userName);

        bool Login(string loginEmailId, string password, string appId);

        string GetSaltHashByUserName(string emailId);

        Common ConfirmEmail(string userId);

        Common ResetPassword(UserDTO user);

        UserDTO GetUserDetails(string emailId);

        UserDTO GetUserDetailsByID(string loginID);

        KeyValuePair<Common, string> ForgotPassword(string loginEmailId);

        bool IsPasswordResetRequired(string loginEmailId);

        List<ContactDTO> GetBillingShippingContact(long customerID, AddressType addressType);

        ContactDTO GetContactDetail(long contactID);

        List<KnowHowOptionDTO> GetKnowHowOptions();

        bool isCustomerConfirmed(long customerID);

        List<ContactDTO> GetShippingAddressContacts(long loginID, int siteID);

        bool AddContact(CheckoutDTO checkoutDTO);

        bool RemoveContact(long contactID);

        bool UpdateContact(ContactDTO contactDTO);

        ContactDTO GetBillingAddressContacts(long loginID);

        bool isUserAvailableEmailPhone(string emailID, string mobileNo);

        UserDTO GetCustomerDetails(long customerID, bool securityInfo);

        List<OrderContactMapDTO> GetShippingAddressDetail(long customerID);

        long GetLoginIDbyCustomerID(long customerID);

        UserDTO GetUserDetailsByCustomerID(long customerID);

        bool CheckCustomerEmailIDAvailability(long loginId, string loginEmailID);

        bool CheckStudentEmailIDAvailability(long loginId, string EmailID);

        NotifyMeDTO SaveNotify(NotifyMeDTO dto);

    }
}