using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Login GetUser(long loginID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.Logins
                        .Include(a => a.Customers)
                        .Include(b => b.UserSettings)
                        .Include(b => b.ClaimSetLoginMaps)
                        .Include(b => b.UserSettings)
                        .Where(a => a.LoginIID == loginID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Login> GetUsers()
        {
            var logins = new List<Login>();
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    logins = dbContext.Logins
                        .Include(a => a.Customers)
                        .Include(b => b.UserSettings)
                        .Include(b => b.ClaimSetLoginMaps)
                        .Include(b => b.UserSettings)
                        .AsNoTracking()
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return logins;
        }

        public Login GetEmailDetails(string LoginEmailID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var loginDetail = dbContext.Logins.Where(a => a.LoginEmailID == LoginEmailID)
                    .AsNoTracking()
                    .OrderByDescending(o => o.LoginIID)
                    .FirstOrDefault();
                return loginDetail;
            }
        }

        public Login GetEmail(string UserID)
        {
            byte? activeStatus = Convert.ToByte(UserStatus.Active);
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var loginDetail = dbContext.Logins.Where(a => a.LoginUserID == UserID && a.StatusID == activeStatus)
                    .AsNoTracking()
                    .OrderByDescending(o => o.LoginIID)
                    .FirstOrDefault();

                return loginDetail;
            }
        }

        public Login GetEmployeeDetails(string LoginEmailID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                var loginDetail = dbContext.Logins.Where(a => a.LoginEmailID == LoginEmailID).OrderByDescending(l => l.LoginIID).AsNoTracking().LastOrDefault();

                var employeeDetail = loginDetail?.LoginIID != null
                    ? dbContext.Employees.Where(a => a.LoginID == loginDetail.LoginIID).OrderByDescending(a => a.EmployeeIID).AsNoTracking().LastOrDefault()
                    : null;
                if (employeeDetail != null)
                {
                    return loginDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        public Login RegisterUser(Login loginDetails)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Logins.Add(loginDetails);

                    if (loginDetails.LoginIID != 0)
                    {
                        dbContext.Entry(loginDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    foreach (var customer in loginDetails.Customers)
                    {
                        if (customer.CustomerIID != 0)
                        {
                            dbContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }


            return loginDetails;
        }

        public Login SaveLogin(Login login)
        {
            Login updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Logins.Add(login);

                    if (login.LoginIID == 0)
                        dbContext.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    //if it's null keep the old password
                    if (login.Password == null)
                    {
                        dbContext.Entry(login).Property(a => a.Password).IsModified = false;
                        dbContext.Entry(login).Property(a => a.PasswordSalt).IsModified = false;
                    }

                    //remove existing roles and add it modified versions
                    dbContext.LoginRoleMaps.RemoveRange(dbContext.LoginRoleMaps.Where(a => a.LoginID == login.LoginIID).AsNoTracking().ToList());

                    foreach (var customer in login.Customers.ToList())
                    {
                        if (customer.CustomerIID == 0)
                        {
                            dbContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(customer).State = EntityState.Detached;
                            var customerEntity = dbContext.Customers.Where(a => a.CustomerIID == customer.CustomerIID).AsNoTracking().FirstOrDefault();
                            dbContext.Entry(customerEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            customerEntity.LoginID = login.LoginIID;
                            customerEntity.UpdatedBy = customer.UpdatedBy;
                            customerEntity.UpdatedDate = DateTime.Now;

                            if (login.Password != null)
                            {
                                customerEntity.IsMigrated = false;
                            }
                        }
                    }

                    foreach (var supplier in login.Suppliers.ToList())
                    {
                        if (supplier.SupplierIID == 0)
                        {
                            dbContext.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(supplier).State = EntityState.Detached;
                            var supplierEntity = dbContext.Suppliers.Where(a => a.SupplierIID == supplier.SupplierIID).AsNoTracking().FirstOrDefault();
                            dbContext.Entry(supplierEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            supplierEntity.LoginID = login.LoginIID;
                            supplierEntity.UpdatedBy = supplier.UpdatedBy;
                            supplierEntity.UpdateDate = DateTime.Now;
                        }
                    }

                    if (login.Contacts != null)
                    {
                        foreach (var contact in login.Contacts)
                        {
                            if (contact.ContactIID == 0)
                                dbContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (login.ClaimSetLoginMaps != null)
                    {
                        dbContext.ClaimSetLoginMaps.RemoveRange(dbContext.ClaimSetLoginMaps.Where(a => a.LoginID == login.LoginIID).AsNoTracking().ToList());
                    }

                    if (login.ClaimLoginMaps != null)
                    {
                        dbContext.ClaimLoginMaps.RemoveRange(dbContext.ClaimLoginMaps.Where(a => a.LoginID == login.LoginIID).AsNoTracking().ToList());
                    }

                    if (login.UserSettings != null)
                    {
                        foreach (var setting in login.UserSettings)
                        {
                            if (dbContext.UserSettings.Any(a => a.CompanyID == setting.CompanyID && a.SettingCode == setting.SettingCode &&
                             a.LoginID == setting.LoginID))
                            {
                                dbContext.Entry(setting).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(setting).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }
                    }
                    dbContext.SaveChanges();
                    updatedEntity = GetUserDetailsByID(login.LoginIID);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public Common ConfirmEmail(string userId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Login login = (from u in dbContext.Logins
                               where u.LoginUserID == userId
                               select u).AsNoTracking().FirstOrDefault();


                if (login.IsNull())
                    return Common.NotExists;
                else
                {
                    switch ((UserStatus)login.StatusID)
                    {
                        case UserStatus.Active:
                            return Common.AlreadyActive;
                        case UserStatus.InActive:
                            return Common.Blocked;
                        case UserStatus.NeedEmailVerification:
                            CustomerSetting cutomerSettings = (from u in dbContext.CustomerSettings
                                                               join c in dbContext.Customers on u.CustomerID equals c.CustomerIID
                                                               where c.LoginID == login.LoginIID
                                                               select u).FirstOrDefault();

                            login.StatusID = (byte)UserStatus.Active;
                            login.UpdatedDate = DateTime.Now;
                            cutomerSettings.IsConfirmed = true;
                            dbContext.SaveChanges();
                            return Common.Activated;
                    }
                }
            }

            return Common.UnSuccessful;
        }

        public KeyValuePair<Common, string> ForgotPassword(string loginUserEmailId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Login login = (from u in dbContext.Logins
                               where u.LoginEmailID == loginUserEmailId
                               select u).AsNoTracking().FirstOrDefault();
                if (login.IsNull())
                    return new KeyValuePair<Common, string>(Common.NotExists, string.Empty);
                else
                    return new KeyValuePair<Common, string>(Common.Success, login.LoginUserID);
            }
        }

        public void ApplyUserRole(LoginRoleMap RoleMap)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.LoginRoleMaps.Add(RoleMap);
                dbContext.SaveChanges();
            }
        }

        public bool IsUserAvailable(string UserEmailId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool isSuccess = (from u in dbContext.Logins
                                  where u.LoginEmailID == UserEmailId
                                  select u).AsNoTracking().Any();
                return isSuccess;
            }
        }

        public Common UpdatePassword(Login loginDetails)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (loginDetails.UserName == null)
                {
                    var loginDet = dbContext.Logins.Where(x => x.LoginIID == loginDetails.LoginIID).AsNoTracking().FirstOrDefault();
                    loginDetails.LoginEmailID = loginDet.LoginEmailID;
                    loginDetails.UserName = loginDet.UserName;
                }

                var login = (from u in dbContext.Logins
                             where (u.LoginEmailID.ToLower() == loginDetails.LoginEmailID.ToLower())
                             select u).AsNoTracking().ToList();
                if (login.Count() < 0)
                    return Common.NotExists;
                else
                {
                    //switch (login.StatusID.IsNotNull() ? (UserStatus)login.StatusID : UserStatus.StatusUnidentified)
                    //{
                    //    case UserStatus.Active:

                    //        if (!ConfigurationExtensions.GetAppConfigValue<bool>("IsJustCMS"))
                    //        {
                    //            var customer = (from a in dbContext.Customers where a.LoginID == login.LoginIID select a).FirstOrDefault();
                    //            if (customer != null)
                    //                customer.IsMigrated = false;
                    //        }
                    loginDetails.PasswordSalt = PasswordHash.CreateHash(loginDetails.Password);
                    //encryt the value to save in the DB as Password
                    loginDetails.Password = StringCipher.Encrypt(loginDetails.Password, loginDetails.PasswordSalt);

                    if (login.Any())
                    {
                        login.All(lgn =>
                        {
                            lgn.Password = loginDetails.Password;
                            lgn.PasswordSalt = loginDetails.PasswordSalt;
                            lgn.UpdatedDate = DateTime.Now;
                            lgn.RequirePasswordReset = false;

                            dbContext.Entry(lgn).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            return true;
                        });
                    }
                    dbContext.SaveChanges();
                    return Common.Success;
                    //case UserStatus.InActive:
                    //    return Common.Blocked;
                    //case UserStatus.NeedEmailVerification:
                    //    return Common.NeedEmailVerification;
                    //case UserStatus.StatusUnidentified:
                    //    return Common.ContcatCustomerSupport;
                    //}
                }
            }

            return Common.UnSuccessful;
        }

        public Login GetUserByUserId(string userId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var user = (from u in dbContext.Logins
                            where u.LoginUserID == userId
                            select u).AsNoTracking().FirstOrDefault();
                return user;
            }
        }

        public Login GetLoginDetailByLoginID(long loginId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Logins.Where(x => x.LoginIID == loginId).AsNoTracking().FirstOrDefault();
            }
        }


        public Login GetUserByLoginEmail(string loginEmailId)
        {
            byte? activeStatus = Convert.ToByte(UserStatus.Active);
            //commented this block as it was halting lazy loading(population) of virtual entities
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (loginEmailId.IsNullOrEmpty())
                {
                    loginEmailId = "";
                }

                var user = dbContext.Logins.Where(u => u.LoginEmailID == loginEmailId && u.StatusID == activeStatus)
                    .Include(i => i.ClaimSetLoginMaps).ThenInclude(i => i.ClaimSet)
                    .Include(i => i.ClaimLoginMaps).ThenInclude(i => i.Claim)
                    .AsNoTracking()
                    .OrderByDescending(o => o.LoginIID)
                    .FirstOrDefault();

                return user;
            }
        }

        public Login GetUserDetailsByID(long loginIID)
        {
            //commented this block as it was halting lazy loading(population) of virtual entities
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var user = dbContext.Logins.Where(a => a.LoginIID == loginIID)
                    .Include(i => i.Employees).ThenInclude(i => i.Branch)
                    .Include(i => i.UserSettings)
                    .Include(i => i.ClaimSetLoginMaps).ThenInclude(i => i.ClaimSet)
                    .Include(i => i.ClaimLoginMaps).ThenInclude(i => i.Claim)
                    .AsNoTracking()
                    .FirstOrDefault();

                //if (user != null)
                //{
                //    dbContext.Entry(user).Collection(a => a.Employees).Load();
                //    dbContext.Entry(user).Collection(a => a.ClaimSetLoginMaps).Load();
                //    dbContext.Entry(user).Collection(a => a.UserSettings).Load();

                //    foreach (var map in user.ClaimSetLoginMaps)
                //    {
                //        dbContext.Entry(map).Reference(a => a.ClaimSet).Load();
                //    }

                //    foreach (var map in user.ClaimLoginMaps)
                //    {
                //        dbContext.Entry(map).Reference(a => a.Claim).Load();
                //    }
                //}

                return user;
            }
        }

        public bool Login(string loginEmailId, string password, byte AppId)
        {
            byte? activeStatus = Convert.ToByte(UserStatus.Active);
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (AppId == (int)ApplicationType.ParentPortal || AppId == (int)ApplicationType.ParentMobileApp)
                {
                    bool isSuccess = (from u in db.Logins
                                      join rm in db.LoginRoleMaps on u.LoginIID equals rm.LoginID
                                      where u.LoginEmailID == loginEmailId && u.Password == password && u.StatusID == activeStatus
                                      && (rm.RoleID == (int)ApplicationType.ParentPortal)
                                      select u).AsNoTracking().Any();
                    if (isSuccess == false)
                    {
                        var loginID = (from u in db.Logins
                                       where u.LoginEmailID == loginEmailId && u.Password == password && u.StatusID == activeStatus
                                       select u.LoginIID).FirstOrDefault();

                        var isGuestUser = (from rm in db.LoginRoleMaps
                                           where rm.LoginID == loginID
                                           select rm.RoleID).Any();
                        isSuccess = !isGuestUser;
                    }
                    //bool isSuccess = (from u in db.Logins
                    //                  join rm in db.LoginRoleMaps on u.LoginIID equals rm.LoginID
                    //                  where u.LoginEmailID == loginEmailId && u.Password == password && u.StatusID == activeStatus
                    //                  && (rm.RoleID == (int)ApplicationType.ParentPortal)
                    //                  select u).Any();
                    return isSuccess;
                }
                else if (AppId == (int)ApplicationType.ERP)
                {
                    bool isSuccess = (from u in db.Logins
                                      join rm in db.LoginRoleMaps on u.LoginIID equals rm.LoginID
                                      where u.LoginEmailID == loginEmailId && u.Password == password && u.StatusID == activeStatus
                                      && (rm.RoleID == (int)ApplicationType.ERP || rm.RoleID == 3)
                                      select u).AsNoTracking().Any();
                    return isSuccess;
                }
                else if (AppId == (int)ApplicationType.StudentMobileApp)
                {
                    bool isSuccess = (from u in db.Logins
                                      join rm in db.LoginRoleMaps on u.LoginIID equals rm.LoginID
                                      where u.LoginEmailID == loginEmailId && u.Password == password && u.StatusID == activeStatus
                                      && (rm.RoleID == (int)ApplicationType.StudentMobileApp)
                                      select u).AsNoTracking().Any();
                    return isSuccess;
                }
                else
                {
                    bool isSuccess = (from u in db.Logins
                                      where u.LoginEmailID == loginEmailId && u.Password == password && u.StatusID == activeStatus
                                      select u).AsNoTracking().Any();
                    return isSuccess;
                }
            }
        }

        public string GetSaltHashByUserName(string loginEmailId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var ret = (from u in dbContext.Logins
                           where u.LoginEmailID == loginEmailId
                           select u.PasswordSalt).AsNoTracking().FirstOrDefault();
                return ret;
            }
        }
        public bool isValideAccount(string loginEmailId)
        {
            var ret = false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var loginid = dbContext.Logins.Where(u => u.LoginEmailID == loginEmailId).AsNoTracking().Select(u => u.LoginIID).FirstOrDefault();

                var valideStudent = dbContext.Students.Where(s => s.LoginID == loginid).AsNoTracking().Select(s => s.IsActive).FirstOrDefault();
                if (valideStudent != null)
                    ret = true;
            }

            return ret;
        }
        public List<LoginRoleMap> GetRoleDetails(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var roles = dbContext.LoginRoleMaps.Where(x => x.LoginID == loginID)
                    .Include(i => i.Role)
                    .AsNoTracking()
                    .ToList();

                //foreach (var role in roles)
                //{
                //    dbContext.Entry(role).Reference(a => a.Role).Load();
                //}

                return roles;
            }
        }

        public Customer GetCustomerDetails(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Customers
                    .Include(x => x.CustomerCards)
                    .Where(x => x.LoginID == loginID).AsNoTracking().FirstOrDefault();

            }
        }

        public List<Contact> GetContacts(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Contacts.Where(x => x.LoginID == loginID).AsNoTracking().ToList();
            }
        }

        public Contact GetLastOrderContact(long loginID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from ordercontact in db.OrderContactMaps
                        join order in db.TransactionHeads on ordercontact.OrderID equals order.HeadIID
                        join customer in db.Customers on order.CustomerID equals customer.CustomerIID
                        join contact in db.Contacts on ordercontact.ContactID equals contact.ContactIID
                        where ordercontact.IsShippingAddress == true && contact.LoginID == loginID
                        orderby ordercontact.OrderContactMapIID descending
                        select contact).AsNoTracking().FirstOrDefault();
            }
        }

        public bool AddContact(Contact contact)
        {
            bool returnValue = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.Contacts.Add(contact);
                dbContext.SaveChanges();
                returnValue = true;
            }

            return returnValue;
        }

        public long AddContactContactID(Contact contact)
        {
            long returnValue = 0;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                contact.StatusID = (int)AddressStatus.Active;
                dbContext.Contacts.Add(contact);
                dbContext.SaveChanges();
                returnValue = contact.ContactIID;
            }

            return returnValue;
        }

        public bool UpdateContact(ContactDTO contact)
        {
            bool returnValue = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Contact contactEntity = dbContext.Contacts.Where(x => x.ContactIID == contact.ContactID).AsNoTracking().FirstOrDefault();
                if (contactEntity.IsNotNull())
                {
                    contactEntity = MapContactToDBEntity(contact, contactEntity);
                    dbContext.SaveChanges();
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public bool RemoveContact(long contactID)
        {
            bool returnValue = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Contact contact = dbContext.Contacts.Where(x => x.ContactIID == contactID).AsNoTracking().FirstOrDefault();
                if (contact.IsNotNull())
                {
                    //dbContext.Contacts.Remove(contact);
                    contact.StatusID = (int)AddressStatus.InActive;
                    dbContext.SaveChanges();
                    returnValue = true;
                }
            }
            return returnValue;
            //throw new NotImplementedException();
        }

        public List<Contact> GetContactSummaryList(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var summary = dbContext.Contacts.Where(c => c.LoginID == loginID)
                              .AsNoTracking()
                              .Select(c => new Contact()
                              {
                                  ContactIID = c.ContactIID,
                                  AddressName = c.AddressName,
                              }).ToList();
                return summary;
            }
        }

        public Contact GetContactDetail(long contactID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Contacts.Where(x => x.ContactIID == contactID).AsNoTracking().FirstOrDefault();
            }
        }

        public bool IsPasswordResetRequired(string emailID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool isPasswordResetRequired = false;
                var loginAccount = dbContext.Logins.Where(x => x.LoginEmailID.ToLower() == emailID.Trim().ToLower()).AsNoTracking().ToList();
                if (loginAccount.Any())
                {
                    loginAccount.All(a =>
                    {
                        if (a.RequirePasswordReset == true)
                            isPasswordResetRequired = true;
                        return true;
                    }
                    );
                }
                return isPasswordResetRequired;
            }
        }

        private static Contact MapContactToDBEntity(ContactDTO contact, Contact contactEntity)
        {
            if (contact.IsNotNull())
            {
                contactEntity.FirstName = contact.FirstName;
                contactEntity.MiddleName = contact.MiddleName;
                contactEntity.LastName = contact.LastName;
                contactEntity.TitleID = contact.TitleID;
                contactEntity.AddressLine1 = contact.AddressLine1;
                contactEntity.AddressLine2 = contact.AddressLine2;
                contactEntity.AddressName = contact.AddressName;
                contactEntity.Block = contact.Block;
                contactEntity.BuildingNo = contact.BuildingNo;
                contactEntity.City = contact.City;
                contactEntity.CivilIDNumber = contact.CivilIDNumber;
                contactEntity.CountryID = contact.CountryID;
                contactEntity.Description = contact.Description;
                contactEntity.Flat = contact.Flat;
                contactEntity.Floor = contact.Floor;
                contactEntity.PassportIssueCountryID = contact.PassportIssueCountryID;
                contactEntity.PassportNumber = contact.PassportNumber;
                contactEntity.PostalCode = contact.PostalCode;
                contactEntity.State = contact.State;
                contactEntity.Street = contact.Street;
                contactEntity.AlternateEmailID1 = contact.AlternateEmailID1;
                contactEntity.AlternateEmailID2 = contact.AlternateEmailID2;
                contactEntity.IsBillingAddress = contact.IsBillingAddress;
                contactEntity.IsShippingAddress = contact.IsShippingAddress;
                contactEntity.UpdatedDate = DateTime.Now;
                contactEntity.UpdatedBy = contact.UpdatedBy;
                contactEntity.MobileNo1 = contact.MobileNo1;
                contactEntity.MobileNo2 = contact.MobileNo2;
                contactEntity.Avenue = contact.Avenue;
                contactEntity.AreaID = contact.AreaID;
                contactEntity.CityID = contact.CityID;
                contactEntity.District = contact.District;
                contactEntity.LandMark = contact.LandMark;
            }
            return contactEntity;
        }

        public List<Role> GetRoles()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Roles.OrderBy(a => a.RoleName).AsNoTracking().ToList();
            }
        }

        public Login GetUserByCustomerID(long customerID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from c in dbContext.Customers
                        join l in dbContext.Logins on c.LoginID equals l.LoginIID
                        where c.CustomerIID == customerID
                        select l)
                        .Include(x => x.UserSettings)
                        .AsNoTracking()
                        .SingleOrDefault();
            }
        }

        /// <summary>
        /// this will return contacts detail based on customerID
        /// </summary>
        /// <param name="customerID">long</param>
        /// <returns>list of contact</returns>
        public List<Contact> GetContactsByCustomerID(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var contacts = (from cu in db.Customers
                                join co in db.Contacts on cu.LoginID equals co.LoginID
                                where cu.CustomerIID == customerID
                                select co).AsNoTracking().ToList();
                return contacts;
            }
        }

        public List<KnowHowOption> GetKnowHowOptions()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.KnowHowOptions.AsNoTracking().ToList();
            }
        }

        public bool isCustomerConfirmed(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CustomerSettings.Where(a => a.CustomerID == customerID && a.IsConfirmed == true).AsNoTracking().Any();
            }
        }

        public long GetCustomerIDbyLoginID(long loginID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Customers.Where(a => a.LoginID == loginID).AsNoTracking().Select(b => b.CustomerIID).FirstOrDefault();
            }
        }

        public long GetLoginIDbyCustomerID(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var loginID = db.Customers.Where(a => a.CustomerIID == customerID).AsNoTracking().Select(b => b.LoginID).FirstOrDefault();
                if (loginID == null) return 0;
                else return loginID.Value;
            }
        }

        public bool SaveWebsiteOrderAddress(long contactID, bool sameBillingAddress, long loginID, long headID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var contact = db.Contacts.Where(a => a.ContactIID == contactID).AsNoTracking().FirstOrDefault();
                if (contact != null)
                {
                    var orderContactMap = MapContacttoOrderEntity(contact);
                    orderContactMap.IsShippingAddress = true;
                    orderContactMap.OrderID = headID;
                    if (sameBillingAddress)
                    {
                        orderContactMap.IsBillingAddress = true;
                    }
                    db.OrderContactMaps.Add(orderContactMap);

                    if (!sameBillingAddress)
                    {
                        var customerID = GetCustomerIDbyLoginID(loginID);
                        var billingcontact = db.Contacts.Where(a => a.LoginID == loginID && a.IsBillingAddress == true).AsNoTracking().FirstOrDefault();
                        var ordercontactBillingMap = MapContacttoOrderEntity(billingcontact);
                        ordercontactBillingMap.IsBillingAddress = true;
                        ordercontactBillingMap.OrderID = headID;
                        db.OrderContactMaps.Add(ordercontactBillingMap);
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        private OrderContactMap MapContacttoOrderEntity(Contact contact)
        {
            return new OrderContactMap()
            {
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                TitleID = contact.TitleID.HasValue ? (short?)short.Parse(contact.TitleID.ToString()) : null,
                AddressLine1 = contact.AddressLine1,
                AddressLine2 = contact.AddressLine2,
                AddressName = contact.AddressName,
                Block = contact.Block,
                BuildingNo = contact.BuildingNo,
                City = contact.City,
                CivilIDNumber = contact.CivilIDNumber,
                CountryID = contact.CountryID,
                Description = contact.Description,
                Flat = contact.Flat,
                Floor = contact.Floor,
                PassportIssueCountryID = contact.PassportIssueCountryID,
                PassportNumber = contact.PassportNumber,
                PostalCode = contact.PostalCode,
                State = contact.State,
                Street = contact.Street,
                AlternateEmailID1 = contact.AlternateEmailID1,
                AlternateEmailID2 = contact.AlternateEmailID2,
                MobileNo1 = contact.MobileNo1,
                MobileNo2 = contact.MobileNo2,
                AreaID = contact.AreaID,
                Avenue = contact.Avenue

            };
        }
        private OrderContactMap MapCustomertoOrderEntity(Customer customer)
        {
            return new OrderContactMap()
            {
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                TitleID = customer.TitleID.HasValue ? (short?)short.Parse(customer.TitleID.ToString()) : null,
                AddressLine1 = "",
                AddressLine2 = "",
                AddressName = "",
                Block = "",
                BuildingNo = "",
                City = "",
                CivilIDNumber = customer.CivilIDNumber,
                CountryID = customer.CountryID,
                Description = "",
                Flat = "",
                Floor = "",
                PassportIssueCountryID = customer.PassportIssueCountryID,
                PassportNumber = customer.PassportNumber,
                PostalCode = "",
                State = "",
                Street = "",
                AlternateEmailID1 = "",
                AlternateEmailID2 = "",
                MobileNo1 = "",
                MobileNo2 = "",

            };
        }

        public bool isUserAvailableEmailPhone(string emailID, string mobileNo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool isSuccess = (from u in dbContext.Logins
                                  where u.LoginEmailID == emailID
                                  select u).AsNoTracking().Any();
                if (!isSuccess)
                {
                    isSuccess = (from p in dbContext.Contacts
                                 where p.IsBillingAddress == true && p.MobileNo1 == mobileNo
                                 select p).AsNoTracking().Any();
                }
                return isSuccess;
            }
        }

        public bool isDuplicateUserAvailableEmailPhone(string emailID, string mobileNo, long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool isSuccess = (from u in dbContext.Logins
                                  where u.LoginEmailID == emailID && u.LoginIID != loginID
                                  select u).AsNoTracking().Any();
                if (!isSuccess)
                {
                    isSuccess = (from p in dbContext.Contacts
                                 where p.IsBillingAddress == true && p.MobileNo1 == mobileNo && p.LoginID != loginID
                                 select p).AsNoTracking().Any();
                }
                return isSuccess;
            }
        }

        public CustomerSetting GetCustomerSettings(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CustomerSettings.Where(c => c.CustomerID == customerID).AsNoTracking().SingleOrDefault();
            }
        }

        public OrderContactMap GetOrderContactMapsByCustomerID(long customerID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var orderContactMap = (from contact in dbContext.Contacts
                                       join ocm in dbContext.OrderContactMaps on contact.ContactIID equals ocm.ContactID
                                       where contact.CustomerID == customerID
                                       select ocm).AsNoTracking().FirstOrDefault();

                return orderContactMap;
            }
        }

        public List<Contact> GetContactByCustomerID(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var contacts = (from cu in db.Customers
                                join co in db.Contacts on cu.LoginID equals co.LoginID
                                where cu.CustomerIID == customerID
                                select co).AsNoTracking().ToList();

                return contacts;
            }
        }

        public bool IsMigratedCustomer(string emailID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var isMigrated = (from a in db.Logins
                                  join b in db.Customers on a.LoginIID equals b.LoginID
                                  where a.LoginEmailID == emailID && b.IsMigrated == true
                                  select b.CustomerIID).Any();
                return isMigrated;
            }
        }

        public bool CheckCustomerEmailIDAvailability(long loginID, string loginEmailID)
        {
            bool isAvailibility = false;

            using (var db = new dbEduegateERPContext())
            {
                isAvailibility = db.Logins.Any(x => x.LoginEmailID == loginEmailID);

                // if it is exist and loginId > 0
                if (isAvailibility && loginID > 0)
                {
                    // get logins by loginId
                    var login = db.Logins.Where(x => x.LoginIID == loginID).AsNoTracking().FirstOrDefault();

                    if (login != null)
                    {
                        if (loginEmailID == login.LoginEmailID)
                        {
                            isAvailibility = false;
                        }
                    }
                }
            }
            return isAvailibility;
        }
        public bool CheckStudentEmailIDAvailability(long loginID, string EmailID)
        {
            bool isAvailibility = false;

            using (var db = new dbEduegateERPContext())
            {
                isAvailibility = db.Logins.Where(x => x.LoginEmailID == EmailID).AsNoTracking().Any();

                // if it is exist and loginId > 0
                if (isAvailibility && loginID > 0)
                {
                    // get logins by loginId
                    var login = db.Logins.Where(x => x.LoginIID == loginID).AsNoTracking().FirstOrDefault();

                    if (login != null)
                    {
                        if (EmailID == login.LoginEmailID)
                        {
                            isAvailibility = false;
                        }
                    }
                }
            }
            return isAvailibility;
        }

        public Account GetAccountDetails(long accountID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Accounts.Where(x => x.AccountID == accountID).AsNoTracking().FirstOrDefault();
            }
        }

        public short NotifyMe(Notify entity)
        {
            short result = 0;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (dbContext.Notifies.Where(x => x.EmailID == entity.EmailID && x.ProductSKUMapID == entity.ProductSKUMapID && x.SiteID == entity.SiteID).AsNoTracking().Any() == false)
                {
                    dbContext.Notifies.Add(entity);
                    dbContext.SaveChanges();
                    result = 1;
                }
                else
                {
                    result = 2;
                }
                return result;
            }
        }

        public Login GetUserByLoginUserID(string userID)
        {
            //commented this block as it was halting lazy loading(population) of virtual entities
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var user = dbContext.Logins.Where(u => u.LoginUserID == userID)
                    .Include(i => i.ClaimSetLoginMaps).ThenInclude(i => i.ClaimSet)
                    .Include(i => i.ClaimLoginMaps).ThenInclude(i => i.Claim)
                    .Include(i => i.Suppliers)
                    .AsNoTracking()
                    .FirstOrDefault();
                //if (user.IsNotNull())
                //{
                //    dbContext.Entry(user).Collection(a => a.ClaimSetLoginMaps).Load();
                //    dbContext.Entry(user).Collection(a => a.ClaimLoginMaps).Load();

                //    foreach (var map in user.ClaimSetLoginMaps)
                //    {
                //        dbContext.Entry(map).Reference(a => a.ClaimSet).Load();
                //    }

                //    foreach (var map in user.ClaimLoginMaps)
                //    {
                //        dbContext.Entry(map).Reference(a => a.Claim).Load();
                //    }
                //}
                return user;
            }
        }

        public Login GetUserByMobileNumber(string mobileNumber)
        {
            //commented this block as it was halting lazy loading(population) of virtual entities
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var user = dbContext.Customers
                    .Where(u => u.Telephone == mobileNumber)
                    .Include(i => i.Login).ThenInclude(i => i.ClaimSetLoginMaps).ThenInclude(i => i.ClaimSet)
                    .Include(i => i.Login).ThenInclude(i => i.ClaimLoginMaps).ThenInclude(i => i.Claim)
                    .AsNoTracking()
                    .Select(c => c.Login)
                    .FirstOrDefault();

                //if (user.IsNotNull())
                //{
                //    dbContext.Entry(user).Collection(a => a.ClaimSetLoginMaps).Load();
                //    dbContext.Entry(user).Collection(a => a.ClaimLoginMaps).Load();

                //    foreach (var map in user.ClaimSetLoginMaps)
                //    {
                //        dbContext.Entry(map).Reference(a => a.ClaimSet).Load();
                //    }

                //    foreach (var map in user.ClaimLoginMaps)
                //    {
                //        dbContext.Entry(map).Reference(a => a.Claim).Load();
                //    }
                //}
                return user;
            }
        }

        public long GetLoginIDforOnlineStore(string emailID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Customers
                    .AsNoTracking()
                    .Where(a => a.Login.LoginEmailID == emailID)
                    .Select(b => b.CustomerIID).FirstOrDefault();
            }
        }

        public bool IsRoleExists(long loginID, int roleID)
        {
            try
            {
                using (var db = new dbEduegateERPContext())
                {
                    return db.LoginRoleMaps.Where(x => x.LoginID == loginID && x.RoleID == roleID).AsNoTracking().Any();
                }
            }
            catch
            {
                return false;
            }
        }

        public Student GetStudentDetails(long studentID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();
            }
        }

        public List<UserSetting> GetLoginSettings(long loginID)
        {
            using (var _dbContext = new dbEduegateERPContext())
            {
                return _dbContext.UserSettings
                    .AsNoTracking()
                    .Where(x => x.LoginID == loginID)
                    .ToList();
            }
        }

    }
}