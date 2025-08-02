using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using Eduegate.Framework.Helper;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Repository
{
    public class CustomerRepository
    {
        public void SaveCustomerCard(CustomerCard card)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var customer = dbContext.Customers
                    .Include(x=> x.CustomerCards)
                    .Where(x => x.CustomerIID == card.CustomerID).AsNoTracking().FirstOrDefault();

                if(customer != null)
                {
                    if(customer.CustomerCards != null)
                    {
                        customer.CustomerCards = new List<CustomerCard>();
                        customer.CustomerCards.Add(card);
                       
                    }
                    else
                    {
                        var customerCard = customer.CustomerCards.First();
                        customerCard.CardNumber = card.CardNumber;
                        customerCard.CardTypeID = card.CardTypeID;
                    }

                    dbContext.SaveChanges();
                }
            }
        }

        public void UpdateUserDefaultBranch(long customerId, long branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var customer = dbContext.Customers.Where(x => x.CustomerIID == customerId).AsNoTracking().FirstOrDefault();
                customer.DefaultBranchID = branchID;
                dbContext.SaveChanges();
            }
        }

        public CustomerMaster GetCustomer(long customerId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CustomerMasters.Where(x => x.CustomerID == customerId).AsNoTracking().FirstOrDefault();
            }
        }

        public Customer GetCustomerByShareHolderID(string shareholderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Customers.Where(x => x.ShareHolderID == shareholderID).AsNoTracking().FirstOrDefault();
            }
        }

        public Customer GetCustomerByMobileNumber(string mobileNumber)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Customers
                    .Include(a=> a.Login)
                    .Where(x => x.Telephone == mobileNumber).AsNoTracking().FirstOrDefault();
            }
        }

        public Customer GetCustomerByLoginID(long LoginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Customers.Where(x => x.LoginID == LoginID).AsNoTracking().FirstOrDefault();
            }
        }

        public Customer GetWalletCustomerDetails(long customerId)
        {
            Customer customer = null;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //TODO: Check later
                //customer = dbContext.Customers.Where(x => x.CustomerIID == customerId).Include(x => x.CustomerGroup).Include(x => x.CustomerStatus).FirstOrDefault();
                customer = dbContext.Customers.Where(x => x.CustomerIID == customerId)
                    .Include(i => i.Login)
                    .AsNoTracking()
                    .FirstOrDefault();
                //dbContext.Entry(customer).Reference(a => a.Login).Load();
            }
            return customer;
        }

        public List<Customer> GetCustomers(string searchText, int dataSize)
        {
            // check if search text is integer or not 
            Int64 n;
            bool isNumeric = Int64.TryParse(searchText, out n);
            var customerList = new List<Customer>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (isNumeric)
                {
                    customerList = (from customer in dbContext.Customers
                                    join login in dbContext.Logins on customer.LoginID equals login.LoginIID
                                    join contact in dbContext.Contacts on login.LoginIID equals contact.LoginID
                                    join rolemap in dbContext.LoginRoleMaps on login.LoginIID equals rolemap.LoginID into loginRoleMap
                                    from rolemap in loginRoleMap.DefaultIfEmpty()

                                    join role in dbContext.Roles on rolemap.RoleID equals role.RoleID into roleRoleMap
                                    from role in roleRoleMap.DefaultIfEmpty()

                                    where (string.Concat(customer.FirstName, customer.MiddleName, customer.LastName).Contains(searchText)
                                    || contact.MobileNo1.Contains(searchText) || login.LoginEmailID.Contains(searchText))
                                    && (role.RoleID == 2)
                                    select customer).OrderBy(a =>  a.FirstName).ThenBy(a => a.MiddleName).ThenBy(a => a.LastName).Take(dataSize).AsNoTracking().ToList();
                }
                else if (!string.IsNullOrEmpty(searchText))
                {
                    // remove join from contact table becuase we need only numeric field from contact which we already checked in above clause
                    customerList = (from customer in dbContext.Customers
                                    join login in dbContext.Logins on customer.LoginID equals login.LoginIID
                                    join rolemap in dbContext.LoginRoleMaps on login.LoginIID equals rolemap.LoginID into loginRoleMap
                                    from rolemap in loginRoleMap.DefaultIfEmpty()

                                    join role in dbContext.Roles on rolemap.RoleID equals role.RoleID into roleRoleMap
                                    from role in roleRoleMap.DefaultIfEmpty()

                                        //join contact in dbContext.Contacts on login.LoginIID equals contact.LoginID
                                    where
                                    (string.Concat(customer.FirstName, customer.MiddleName, customer.LastName).Contains(searchText) || login.LoginEmailID.Contains(searchText))
                                    && (role.RoleID == 2)
                                    select customer).OrderBy(a => a.FirstName).ThenBy(a => a.MiddleName).ThenBy(a => a.LastName).Take(dataSize).AsNoTracking().ToList();
                }
                else
                {
                    if (dataSize > 0)  //Getting only 50 data from table
                    {
                        customerList = (from customer in dbContext.Customers
                                        select customer).OrderBy(a => a.FirstName).ThenBy(a => a.MiddleName).ThenBy(a => a.LastName).Take(dataSize).AsNoTracking().ToList();
                    }
                    else //Getting all the data from table
                    {
                        customerList = (from customer in dbContext.Customers
                                        select customer).OrderBy(a => a.FirstName).ThenBy(a => a.MiddleName).ThenBy(a => a.LastName).AsNoTracking().ToList();
                    }
                }
            }

            return customerList;
        }

        public Customer GetCustomerV2(long customerID)
        {
            var customer = new Customer();
            using (var dbContext = new dbEduegateERPContext())
            {
                customer = dbContext.Customers.Where(x => x.CustomerIID == customerID)
                    /*.Include(x => x.CustomerGroup)*/
                    //.Include(x => x.CustomerStatus)
                    .Include(x => x.Login)
                    .Include(x => x.CustomerSettings)
                    .Include(x => x.CustomerAccountMaps).ThenInclude(x => x.Account)
                    .Include(a=> a.CustomerSupplierMaps)
                    .AsNoTracking()
                    .FirstOrDefault();
                //dbContext.Entry(customer).Reference(a => a.Login).Load();
                //dbContext.Entry(customer).Collection(a => a.CustomerSettings).Load();

                //if (customer != null)
                //{
                //    if (customer.Login != null)
                //    {
                //        dbContext.Entry(customer.Login).Collection(a => a.Contacts).Load();
                //        customer.Contacts = dbContext.Contacts.Where(a => a.LoginID == customer.LoginID).ToList();
                //    }

                //    if (customer.CustomerAccountMaps != null)
                //    {
                //        foreach (var map in customer.CustomerAccountMaps)
                //        {
                //            dbContext.Entry(map).Reference(a => a.Account).Load();
                //        }
                //    }
                //}
            }              

            return customer;
        }

        public List<EntityPropertyMap> GetEntityPropertyMapsByType(long referenceID, short entityTypeID, short entityPropertyTypeID)
        {
            List<EntityPropertyMap> entityPropertyMaps = new List<EntityPropertyMap>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                entityPropertyMaps = (from epm in dbContext.EntityPropertyMaps
                                      where epm.ReferenceID == referenceID && epm.EntityTypeID == entityTypeID && epm.EntityPropertyTypeID == entityPropertyTypeID
                                      select epm).AsNoTracking().ToList();
            }

            return entityPropertyMaps;
        }

        public string GetEntityPropertyName(long? entityPropertyID)
        {
            string epName = string.Empty;

            if (entityPropertyID.HasValue)
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var ep = (from epm in dbContext.EntityProperties
                              where epm.EntityPropertyIID == entityPropertyID
                              select epm).AsNoTracking().FirstOrDefault();

                    if (ep.IsNotNull())
                    {
                        epName = ep.PropertyName;
                    }
                }
            }

            return epName;
        }


        public bool IsSubscribe(string Email)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();

            bool result = (from s in db.Subscriptions
                           where s.SubscriptionEmail == Email
                           select s).AsNoTracking().Any();
            return result;
        }

        public string AddSubscription(Subscription subscription)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();

            db.Subscriptions.Add(subscription);
            db.SaveChanges();
            return subscription.VarificationCode;
        }


        public string SubscribeConfirmation(string VarificationCode)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            Subscription subscription = new Subscription();

            var query = (from s in db.Subscriptions
                         where s.VarificationCode == VarificationCode
                         select s).AsNoTracking().FirstOrDefault();
            if (query.VarificationCode != VarificationCode)
            {
                return null;
            }
            query.StatusID = (byte)UserStatus.Active;
            db.SaveChanges();

            return query.SubscriptionEmail;
        }

        public Customer SaveCustomer(Customer customer)
        {
            Customer updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (customer.IsNotNull())
                    {
                        dbContext.Customers.Add(customer);

                        if (customer.CustomerIID == 0)
                            dbContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        else
                            dbContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        //Login
                        if (customer.Login.IsNotNull())
                        {
                            var contactIIDs = customer.Login.Contacts.Select(a => a.ContactIID).ToList();

                            //Delete contacts
                            var contacts = dbContext.Contacts.Where(x => x.LoginID == customer.Login.LoginIID && x.Login.Contacts.Any(a => !contactIIDs.Contains(a.ContactIID))).ToList();
                            if (contacts.IsNotNull())
                                dbContext.Contacts.RemoveRange(contacts);

                            if (customer.Login.Contacts.IsNotNull() && customer.Login.Contacts.Count > 0)
                            {
                                foreach (var contact in customer.Login.Contacts.ToList())
                                {
                                    if (contact.ContactIID == 0)
                                        dbContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    else
                                        dbContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            if (customer.Login.LoginIID == 0)
                            {
                                dbContext.Logins.Add(customer.Login);
                                dbContext.Entry(customer.Login).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                if (customer.Login.LoginRoleMaps == null || customer.Login.LoginRoleMaps.Count == 0)
                                {
                                    // Add rolemap for newly created customer
                                    var loginRoleMap = new LoginRoleMap
                                    {
                                        RoleID = 1,
                                        LoginID = customer.Login.LoginIID,
                                        CreatedBy = customer.CreatedBy,
                                        CreatedDate = customer.CreatedDate,
                                    };

                                    dbContext.Entry(loginRoleMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }
                            else
                            {
                                dbContext.Entry(customer.Login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            if (customer.Login.Password == null)
                            {
                                dbContext.Entry(customer.Login).Property(a => a.Password).IsModified = false;
                                dbContext.Entry(customer.Login).Property(a => a.PasswordSalt).IsModified = false;
                            }
                            else
                            {
                                customer.IsMigrated = false;
                            }
                        }


                        //Customer Settings
                        if (customer.CustomerSettings != null)
                        {
                            foreach (var setting in customer.CustomerSettings)
                            {
                                if (dbContext.CustomerSettings.Any(a => a.CustomerSettingIID == setting.CustomerSettingIID))
                                {
                                    dbContext.Entry(setting).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                else
                                {
                                    dbContext.CustomerSettings.Add(setting);
                                    dbContext.Entry(setting).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }
                        }

                        if(customer.CustomerAccountMaps != null)
                        {
                            foreach(var map in customer.CustomerAccountMaps)
                            {
                                if(map.CustomerAccountMapIID != 0)
                                {
                                    dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext.Entry(map.Account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }
                    }

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Customers.Where(x => x.CustomerIID == customer.CustomerIID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public bool SavePriceListEntitlementMaps(List<ProductPriceListCustomerMap> priceListEntitlementMaps)
        {
            var exit = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var map in priceListEntitlementMaps)
                {
                    dbContext.ProductPriceListCustomerMaps.Add(map);
                    if (map.ProductPriceListCustomerMapIID <= 0)
                        dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
                exit = true;
            }
            return exit;
        }

        public List<CustomerGroup> GetCustomerGroups()
        {
            var customerGroups = new List<CustomerGroup>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                customerGroups = dbContext.CustomerGroups.OrderBy(a => a.GroupName).AsNoTracking().ToList();
            }

            return customerGroups;
        }

        public CustomerGroup GetCustomerGroup(long customerGroupID)
        {
            var customerGroup = new CustomerGroup();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                customerGroup = dbContext.CustomerGroups.Where(x => x.CustomerGroupIID == customerGroupID).AsNoTracking().FirstOrDefault();
            }

            return customerGroup;
        }

        public CustomerGroup SaveCustomerGroup(CustomerGroup customerGroup)
        {
            CustomerGroup updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.CustomerGroups.Add(customerGroup);

                    if (customerGroup.CustomerGroupIID == 0)
                        dbContext.Entry(customerGroup).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(customerGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.CustomerGroups.Where(x => x.CustomerGroupIID == customerGroup.CustomerGroupIID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }


        public Customer IsCustomerExist(string email, string phone)
        {
            string message = string.Empty;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Customer customer = (from cu in dbContext.Customers
                                     join l in dbContext.Logins on cu.LoginID equals l.LoginIID
                                     join co in dbContext.Contacts on l.LoginIID equals co.LoginID
                                     where l.LoginEmailID == email || co.MobileNo1 == phone
                                     select cu)
                                     .Include(a=> a.CustomerSupplierMaps)
                                     .Include(a => a.CustomerSettings)
                                     .Include(i => i.Login).ThenInclude(i => i.Contacts)
                                     .AsNoTracking().ToList().FirstOrDefault();

                //if (customer == null)
                //{
                //    return null;
                //}

                //dbContext.Entry(customer).Reference(a => a.Login).Load();

                //if (customer.Login != null)
                //{
                //    dbContext.Entry(customer.Login).Collection(a => a.Contacts).Load();
                //}
                return customer;
            }
        }

        public List<Customer> GetCustomerByCustomerIdAndCR(string searchText)
        {
            var customers = new List<Customer>();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    customers = db.Customers.Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>(Constants.MAX_FETCH_COUNT)).ToList();
                }
                else
                {
                    customers = db.Customers.Where(x => (x.CustomerIID.ToString() + x.FirstName + x.MiddleName + x.LastName + x.Telephone).Contains(searchText))
                        .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>(Constants.MAX_FETCH_COUNT)).ToList();
                }
            }

            return customers;
        }

        public CustomerSupplierMap SaveCustomerSupplierMap(CustomerSupplierMap entity)
        {
            try //This is a temporary fix, we need to populate Timestamps for this entity, then this issue will be fixed
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    if (entity.SupplierID == null || entity.SupplierID == 0)
                        return new CustomerSupplierMap();

                    var existEntity = db.CustomerSupplierMaps.Where(x => x.SupplierID == entity.SupplierID && x.CustomerID == entity.CustomerID).AsNoTracking().FirstOrDefault();
                    if (existEntity != null)
                    {
                        existEntity.SupplierID = entity.SupplierID;
                        existEntity.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        db.CustomerSupplierMaps.Add(entity);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return entity;

        }

        public CustomerSupplierMap GetCustomerSupplierMapByCustomerID(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CustomerSupplierMaps.Where(x => x.CustomerID == customerID).AsNoTracking().FirstOrDefault();
            }
        }

        public CustomerProductReference SaveCustomerProductReferences(CustomerProductReference entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.CustomerProductReferences.Add(entity);
                if (entity.CustomerProductReferenceIID > 0)
                {
                    // Update
                    db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    // Insert
                    db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                db.SaveChanges();
                return entity;
            }
        }

        public bool RemoveCustomerProductReferences(CustomerProductReference entity)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.CustomerProductReferences.RemoveRange(db.CustomerProductReferences.Where(x => x.CustomerID == entity.CustomerID).AsNoTracking().ToList());
                db.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }


        public List<CustomerProductReference> GetCustomerProductReferencesByCustomerID(CustomerProductReference entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CustomerProductReferences.Where(x => x.CustomerID == entity.CustomerID).AsNoTracking().ToList();
            }
        }

        public Customer GetCustomerByContactID(long contactID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var customer = (from cu in db.Customers
                                join co in db.Contacts on cu.CustomerIID equals co.CustomerID
                                where co.ContactIID == contactID
                                select cu).Include(x => x.Contacts)
                                .AsNoTracking()
                                .FirstOrDefault();
                return customer;
            }
        }

        public Customer GetCustomerDetail(long customerID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Customers.Where(x => x.CustomerIID == customerID).AsNoTracking().FirstOrDefault();
            }
        }

        public short AddNewsletterSubsciption(string emailID, int cultureID, string ipAddress)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                if (!db.NewsletterSubscriptions.Any(a => a.EmailID == emailID))
                {
                    if (!db.Logins.Any(a => a.LoginEmailID == emailID && a.StatusID == (byte)UserStatus.Active))
                    {
                        var newsletterSubscription = new NewsletterSubscription();
                        newsletterSubscription.EmailID = emailID;
                        newsletterSubscription.CreatedDate = DateTime.Now;
                        newsletterSubscription.IPAddress = ipAddress;
                        newsletterSubscription.CultureID = (byte)cultureID;
                        newsletterSubscription.StatusID = (int)Framework.Helper.Enums.NewsletterSubscriptionStatus.Subscribed;
                        db.NewsletterSubscriptions.Add(newsletterSubscription);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 2;
                }
            }
        }


        public Customer GetCustomerByTransactionHeadId(long headId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Customers
                    .Join(db.TransactionHeads, c => c.CustomerIID, t => t.CustomerID, (c, t) => new { c, t })
                    .Where(x => x.t.HeadIID == headId)
                    .AsNoTracking()
                    .Select(x => x.c)
                    .FirstOrDefault();
            }
        }

        public List<CustomerAccountMap> SaveCustomerAccountMaps(List<CustomerAccountMap> entityList)
        {
            try
            {
                long customerId = 0;
                var entity = entityList.FirstOrDefault();
                if (entity != null)
                {
                    customerId = (long)entity.CustomerID;
                }
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var existingDBList = dbContext.CustomerAccountMaps.Where(a=> a.CustomerID == customerId);
                    foreach (var customerAccountMap in entityList)
                    {
                        if (existingDBList != null)
                        {
                            var customerAccountMapInDB = existingDBList.Where(a => a.CustomerAccountMapIID == customerAccountMap.CustomerAccountMapIID)
                                .AsNoTracking()
                                .FirstOrDefault();
                            if (customerAccountMapInDB == null)
                            {
                                if(customerAccountMap.Account != null)
                                    customerAccountMap.Account.AccountBehavoirID = customerAccountMap.Account.AccountBehavoirID == 0 ? null : customerAccountMap.Account.AccountBehavoirID;
                                dbContext.CustomerAccountMaps.Add(customerAccountMap);
                            }
                            else
                            {
                                if (customerAccountMap.AccountID != null)
                                {
                                    customerAccountMapInDB.AccountID = customerAccountMap.AccountID; // Base account
                                }
                                else
                                {
                                    customerAccountMapInDB.Account = customerAccountMap.Account;
                                }
                            }
                        }
                        else
                        {
                            dbContext.CustomerAccountMaps.Add(customerAccountMap);
                        }
                    }

                    dbContext.SaveChanges();
                }
                return this.GetCustomerAccountMaps(customerId);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }
            //return entityList;
        }
        public List<CustomerAccountMap> GetCustomerAccountMaps(long customerId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CustomerAccountMaps
                        .Include(a => a.EntityTypeEntitlement)
                        .Include(b => b.Account)
                        .Include(i => i.Account).ThenInclude(i => i.Group)
                        .Include(i => i.Account).ThenInclude(i => i.ParentAccount)
                        .Include(i => i.Account).ThenInclude(i => i.AccountBehavoir)
                        .Where(a => a.CustomerID == customerId)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public bool CheckCustomerEmailIDAvailability(long contactId, string mobileNumber)
        {
            bool isAvailibility = false;

            using (var db = new dbEduegateERPContext())
            {
                isAvailibility = db.Contacts.Where(x => x.MobileNo1 == mobileNumber).AsNoTracking().Any();

                // if it is exist and loginId > 0
                if (isAvailibility && contactId > 0)
                {
                    // get logins by loginId
                    var contact = db.Contacts.Where(x => x.ContactIID == contactId).AsNoTracking().FirstOrDefault();

                    if (contact != null)
                    {
                        if (mobileNumber == contact.MobileNo1)
                        {
                            isAvailibility = false;
                        }
                    }
                }
            }
            return isAvailibility;
        }

        public Customer GetCustomerDetailsLoyaltyPoints(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Customers.Include(a => a.CustomerSettings).Where(a => a.CustomerIID == customerID).AsNoTracking().FirstOrDefault();
            }
        }

        public List<TransactionHead> GetTransactionHeadLoyaltyPoints(long customerID, long companyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var transactionList = (from t in db.TransactionHeads
                                       join p in db.TransactionHeadPointsMaps on t.HeadIID equals p.TransactionHeadID
                                       where t.CustomerID == customerID && t.CompanyID == companyID
                                       select t
                                      ).OrderByDescending(a => a.TransactionDate).Include(a => a.TransactionHeadPointsMaps).AsNoTracking().ToList();

                return transactionList;
            }
        }

        public Customer GetCustomerDetailsCategorizationPoints(long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Customers.Include(a => a.CustomerSettings).Where(a => a.CustomerIID == customerID).AsNoTracking().FirstOrDefault();
            }
        }

        public CustomerGroup GetCustomerGroup(decimal categorizationPoints)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CustomerGroups.Where(a => a.PointLimit <= categorizationPoints).OrderByDescending(a => a.PointLimit).AsNoTracking().FirstOrDefault();
            }
        }

        public void SaveCustomerOTP(long loginID, string otpText)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var login = db.Logins.Where(a => a.LoginIID == loginID).AsNoTracking().FirstOrDefault();
                login.LastOTP = otpText;
                db.SaveChanges();
            }
        }

        public long? GetCustomerIDByLoginID(long LoginID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var customer = dbContext.Customers.Where(x => x.LoginID == LoginID).AsNoTracking().FirstOrDefault();
                return customer == null ? (long?)null : customer.CustomerIID;
            }
        }

        public CustomerStatus GetCustomerStatusByID(byte customerStatusID)
        {
            var customerStatus = new CustomerStatus();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                customerStatus = dbContext.CustomerStatuses.Where(x => x.CustomerStatusID == customerStatusID).AsNoTracking().FirstOrDefault();
            }

            return customerStatus;
        }

    }
}
