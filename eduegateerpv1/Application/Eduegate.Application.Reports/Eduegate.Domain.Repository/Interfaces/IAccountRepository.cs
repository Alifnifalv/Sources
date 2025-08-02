using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Domain.Repository
{
    public interface IAccountRepository
    {
        Login GetUser(long loginID);
        List<Login> GetUsers();

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Login GetEmailDetails(string EmailIID);

        Login GetEmail(string UserID);

        /// <summary>
        /// Get list of roles associated to user
        /// </summary>
        /// <param name="loginEmailID"></param>
        /// <returns></returns>
        Login RegisterUser(Login loginDetails);
        /// <summary>
        /// save login information, add, update should be supported
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns></returns>
        Login SaveLogin(Login login);
        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Common ConfirmEmail(string UserId);
        /// <summary>
        /// Pass email id to get user id, which should be communicated to user by email
        /// </summary>
        /// <param name="loginEmailID"></param>
        /// <returns></returns>
        KeyValuePair<Common, string> ForgotPassword(string loginUserEmailId);
        /// <summary>
        /// Assign a role to the user
        /// </summary>
        /// <param name="RoleMap"></param>
        void ApplyUserRole(LoginRoleMap roleMap);
        /// <summary>
        /// Check if the user exists or not
        /// </summary>
        /// <param name="loginEmailID"></param>
        /// <returns></returns>
        bool IsUserAvailable(string loginEmailID);
        /// <summary>
        /// Update user password 
        /// </summary>
        /// <param name="LoginDetails"></param>
        /// <returns></returns>
        Common UpdatePassword(Login loginDetails);
        /// <summary>
        /// Get user details by passing user guid
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        Login GetEmployeeDetails(string loginEmailID);

        Login GetUserByUserId(string userId);
        /// <summary>
        /// Get user details by passing user registered email id
        /// </summary>
        /// <param name="loginEmailID"></param>
        /// <returns></returns>
        Login GetUserByLoginEmail(string loginEmailID);
        Login GetUserByLoginUserID(string userID);
        Login GetUserByMobileNumber(string mobileNumber);
        /// <summary>
        /// Get User logins by IID
        /// </summary>
        /// <param name="loginIID"></param>
        /// <returns></returns>
        Login GetUserDetailsByID(long loginIID);
        /// <summary>
        /// Validate user login
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        bool Login(string UserName, string password, byte AppId);
        /// <summary>
        /// Get users password salt key for the given email id
        /// </summary>
        /// <param name="loginEmailID"></param>
        /// <returns></returns>
        string GetSaltHashByUserName(string loginEmailID);
        /// <summary>
        /// Get customer details by login id
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        Customer GetCustomerDetails(long loginID);
        /// <summary>
        /// Get list of roles associated to user
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        List<LoginRoleMap> GetRoleDetails(long loginID);
        /// <summary>
        /// Get list of contacts associated to user
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        List<Contact> GetContacts(long loginID);

        Contact GetLastOrderContact(long loginID);
        /// <summary>
        /// Add new contact associated to user
        /// </summary>
        /// <param name="Contact"></param>
        /// <returns></returns>
        bool AddContact(Contact contact);
        /// <summary>
        /// Update contact associated to user
        /// </summary>
        /// <param name="Contact"></param>
        /// <returns></returns>
        //bool UpdateContact(ContactDTO contact);
        /// <summary>
        /// Remove contact by ID
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        bool RemoveContact(long contactID);
        /// <summary>
        /// Get all contacts for a given login id
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        List<Contact> GetContactSummaryList(long loginID);
        /// <summary>
        /// Get details of the contact
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        Contact GetContactDetail(long contactID);
        /// <summary>
        /// Post migration, password for the users should be reset.
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns></returns>
        bool IsPasswordResetRequired(string emailID);
        /// <summary>
        /// Get login details for given customer id
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
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
    }
}