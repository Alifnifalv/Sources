using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Domain.Repository
{
    public interface IAccountRepository
    {
        Login GetUser(long loginID);
        List<Login> GetUsers();

        Login GetEmailDetails(string EmailIID);

        Login GetEmail(string UserID);

        Login RegisterUser(Login loginDetails);

        Login SaveLogin(Login login);

        Common ConfirmEmail(string UserId);

        KeyValuePair<Common, string> ForgotPassword(string loginUserEmailId);

        void ApplyUserRole(LoginRoleMap roleMap);

        bool IsUserAvailable(string loginEmailID);

        Common UpdatePassword(Login loginDetails);

        Login GetEmployeeDetails(string loginEmailID);

        Login GetUserByUserId(string userId);

        Login GetUserByLoginEmail(string loginEmailID);

        Login GetUserByLoginUserID(string userID);

        Login GetUserByMobileNumber(string mobileNumber);

        Login GetUserDetailsByID(long loginIID);

        bool Login(
            string UserName, string password, byte AppId);

        string GetSaltHashByUserName(string loginEmailID);

        Customer GetCustomerDetails(long loginID);

        List<LoginRoleMap> GetRoleDetails(long loginID);

        List<Contact> GetContacts(long loginID);

        Contact GetLastOrderContact(long loginID);

        bool AddContact(Contact contact);

        //bool UpdateContact(ContactDTO contact);

        bool RemoveContact(long contactID);

        List<Contact> GetContactSummaryList(long loginID);

        Contact GetContactDetail(long contactID);

        bool IsPasswordResetRequired(string emailID);

        Login GetUserByCustomerID(long customerID);

        bool isCustomerConfirmed(long customerID);

        long GetCustomerIDbyLoginID(long loginID);

        bool isUserAvailableEmailPhone(string emailID, string mobileNo);

        long AddContactContactID(Contact contact);

        bool isDuplicateUserAvailableEmailPhone(string emailID, string mobileNo, long loginID);

        CustomerSetting GetCustomerSettings(long customerID);

        bool IsMigratedCustomer(string emailID);

        long GetLoginIDbyCustomerID(long customerID);

        Login GetLoginDetailByLoginID(long loginId);

        short NotifyMe(Notify entity);

        bool IsRoleExists(long loginId, int roleID);

        List<UserSetting> GetLoginSettings(long loginID);
    }
}