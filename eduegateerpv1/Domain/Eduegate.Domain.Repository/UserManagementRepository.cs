using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class UserManagementRepository
    {

        public Login GetUserManagement(decimal userIID)
        {
            var login = new Login();

            try
            {
                using(dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var loginEntity = dbContext.Logins.Where(x => x.LoginIID == userIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (loginEntity.IsNotNull())
                    {
                        login = new Login()
                        {
                            LoginIID = loginEntity.LoginIID,
                            LoginEmailID = loginEntity.LoginEmailID,
                            Password = loginEntity.Password,
                            PasswordSalt = loginEntity.PasswordSalt,
                            StatusID = loginEntity.StatusID,               
                        };

                        if (loginEntity.LoginRoleMaps.IsNotNull() && loginEntity.LoginRoleMaps.Count > 0)
                        {
                            login.LoginRoleMaps = new List<LoginRoleMap>();
                            var loginRoleMap = loginEntity.LoginRoleMaps.Where(x => x.RoleID == 5).FirstOrDefault();
                            
                            if(loginRoleMap.IsNotNull())
                            {
                                login.LoginRoleMaps.Add(new LoginRoleMap()
                                {
                                    LoginID = loginRoleMap.LoginID,
                                    RoleID = loginRoleMap.RoleID,
                                });
                            }   
                        }

                        if (loginEntity.Customers.IsNotNull() && loginEntity.Customers.Count > 0)
                        {
                            login.Customers = new List<Customer>();

                            foreach(Customer customer in loginEntity.Customers)
                            {
                                login.Customers.Add(new Customer()
                                {
                                    LoginID = customer.LoginID,
                                    TitleID = customer.TitleID,
                                    FirstName = customer.FirstName,
                                    MiddleName = customer.MiddleName,
                                    LastName = customer.LastName,
                                });
                            }
                        }

                        if (loginEntity.Contacts.IsNotNull() && loginEntity.Contacts.Count > 0)
                        {
                            login.Contacts = new List<Contact>();

                            foreach (Contact contact in loginEntity.Contacts)
                            {
                                login.Contacts.Add(new Contact()
                                {
                                    LoginID = contact.LoginID,
                                    TitleID = contact.TitleID,
                                    FirstName = contact.FirstName,
                                    MiddleName = contact.MiddleName,
                                    LastName = contact.LastName,
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the user management by user iid", TrackingCode.ERP);
            }

            return login;
        }

        public bool SaveUserManagement(Login login)
        {
            bool result = false;

            try
            {
                if (login.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (login.LoginIID <= 0)
                        {
                            dbContext.Logins.Add(login);
                        }
                        else
                        {
                            Login loginEntity = dbContext.Logins.Where(x => x.LoginIID == login.LoginIID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if(loginEntity.IsNotNull())
                            {
                                loginEntity.LoginIID = login.LoginIID;
                                loginEntity.LoginEmailID = login.LoginEmailID;
                                loginEntity.Password = login.Password;
                                loginEntity.PasswordSalt = login.PasswordSalt;
                                loginEntity.StatusID = login.StatusID;

                                if (login.LoginRoleMaps.IsNotNull() && login.LoginRoleMaps.Count > 0)
                                {
                                    foreach (LoginRoleMap roleMap in login.LoginRoleMaps)
                                    {
                                        LoginRoleMap lrmEntity = dbContext.LoginRoleMaps.Where(x => x.LoginID == login.LoginIID).FirstOrDefault();

                                        lrmEntity.LoginID = roleMap.LoginID;
                                        lrmEntity.RoleID = roleMap.RoleID;
                                    }
                                }

                                if (login.Customers.IsNotNull() && login.Customers.Count > 0)
                                {
                                    foreach (Customer customer in login.Customers)
                                    {
                                        Customer customerEntity = dbContext.Customers.Where(x => x.LoginID == login.LoginIID).FirstOrDefault();

                                        customerEntity.LoginID = customer.LoginID;
                                        customerEntity.TitleID = customer.TitleID;
                                        customerEntity.FirstName = customer.FirstName;
                                        customerEntity.LastName = customer.LastName;
                                    }
                                }

                                if (login.Contacts.IsNotNull() && login.Contacts.Count > 0)
                                {
                                    foreach (Contact contact in login.Contacts)
                                    {
                                        Contact contactEntity = dbContext.Contacts.Where(x => x.LoginID == login.LoginIID).FirstOrDefault();

                                        contactEntity.LoginID = contact.LoginID;
                                        contactEntity.TitleID = contact.TitleID;
                                        contactEntity.FirstName = contact.FirstName;
                                        contactEntity.LastName = contact.LastName;
                                    }
                                }
                            }
                        }

                        dbContext.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserManagementRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return result;
        }
    }
}
