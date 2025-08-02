using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using System.Data.Entity;

namespace Eduegate.Domain.Repository
{
    public class SupplierRepository
    {

        public Supplier GetSupplierByLoginID(long loginID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.Suppliers.Where(x => x.LoginID == loginID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public Supplier GetSupplierByBranchnID(long branchID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.Suppliers.Where(x => x.BranchID == branchID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public Supplier GetSupplier(long supplierID)
        {
            var supplier = new Supplier();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var entity = dbContext.Suppliers.Where(x => x.SupplierIID == supplierID)
                        .Include(x => x.SupplierStatus).Include(x => x.Login).Include(x => x.Employee)
                        .Include(x => x.ReturnMethod).Include(x => x.ReceivingMethod)
                        .Include(x => x.SupplierAccountMaps)
                        .FirstOrDefault();

                    if (entity.IsNotNull())
                    {
                        supplier = new Supplier()
                        {
                            TitleID = entity.TitleID,
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            UpdatedBy = entity.UpdatedBy,
                            UpdateDate = entity.UpdateDate,
                            SupplierIID = entity.SupplierIID,
                            FirstName = entity.FirstName,
                            MiddleName = entity.MiddleName,
                            LastName = entity.LastName,
                            StatusID = entity.StatusID,
                            VendorCR = entity.VendorCR,
                            CRExpiry = entity.CRExpiry,
                            VendorNickName = entity.VendorNickName,
                            CompanyLocation = entity.CompanyLocation,
                            EmployeeID = entity.EmployeeID,
                            SupplierCode = entity.SupplierCode,
                            SupplierAddress = entity.SupplierAddress,
                            LoginID = entity.LoginID,
                            TimeStamps = entity.TimeStamps,
                            Login = entity.Login,
                            IsMarketPlace = entity.IsMarketPlace,
                            BranchID = entity.BranchID,
                            ReceivingMethodID = entity.ReceivingMethodID,
                            ReturnMethodID = entity.ReturnMethodID,
                            Profit = entity.Profit,
                            SupplierEmail = entity.SupplierEmail,
                            Telephone = entity.Telephone
                        };

                        if (entity.SupplierStatus.IsNotNull())
                        {
                            supplier.SupplierStatus = new SupplierStatus()
                            {
                                SupplierStatusID = entity.SupplierStatus.SupplierStatusID,
                                StatusName = entity.SupplierStatus.StatusName,
                            };
                        }
                        if (entity.Employee.IsNotNull())
                        {
                            supplier.Employee = new Employee()
                            {
                                EmployeeIID = entity.Employee.EmployeeIID,
                                EmployeeName = entity.Employee.EmployeeName,
                            };
                        }
                        if(entity.ReturnMethodID.IsNotNull())
                        {
                            supplier.ReturnMethod = new ReturnMethod()
                            {
                                ReturnMethodName = entity.ReturnMethod.ReturnMethodName,
                            };
                        }
                        if(entity.ReceivingMethodID.IsNotNull())
                        {
                            supplier.ReceivingMethod = new ReceivingMethod()
                            {
                                ReceivingMethodName = entity.ReceivingMethod.ReceivingMethodName,
                            };
                        }
                        if (entity.Contacts.IsNotNull() && entity.Contacts.Count > 0)
                        {
                            supplier.Contacts = new List<Contact>();

                            foreach (Contact contact in entity.Contacts)
                            {
                                supplier.Contacts.Add(new Contact()
                                {
                                    ContactIID = contact.ContactIID,
                                    SupplierID = contact.SupplierID,
                                    BuildingNo = contact.BuildingNo,
                                    Floor = contact.Floor,
                                    Block = contact.Block,
                                    Flat = contact.Flat,
                                    AddressName = contact.AddressName,
                                    AddressLine1 = contact.AddressLine1,
                                    AddressLine2 = contact.AddressLine2,
                                    CountryID = contact.CountryID,
                                    CityID = contact.CityID,
                                    State = contact.State,
                                    City = contact.City,
                                    District = contact.District,
                                    LandMark = contact.LandMark,
                                    Avenue = contact.Avenue,
                                    Street = contact.Street,
                                    AreaID = contact.AreaID,
                                    Description = contact.Description,
                                    PostalCode = contact.PostalCode,
                                    TelephoneCode = contact.TelephoneCode,
                                    PhoneNo1 = contact.PhoneNo1,
                                    MobileNo1 = contact.MobileNo1,
                                    MobileNo2 = contact.MobileNo1,
                                    AlternateEmailID1 = contact.AlternateEmailID1,
                                    AlternateEmailID2 = contact.AlternateEmailID2,
                                    WebsiteURL1 = contact.WebsiteURL1,
                                    WebsiteURL2 = contact.WebsiteURL2,
                                    //TimeStamps = contact.TimeStamps,
                                    CreatedBy = contact.CreatedBy,
                                    CreatedDate = contact.CreatedDate,
                                    UpdatedBy = contact.UpdatedBy,
                                    UpdatedDate = contact.UpdatedDate,
                                    IsBillingAddress = contact.IsBillingAddress,
                                    IsShippingAddress = contact.IsShippingAddress,
                                    FirstName = contact.FirstName,
                                    MiddleName = contact.MiddleName,
                                    LastName = contact.LastName,
                                    PassportNumber = contact.PassportNumber,
                                    CivilIDNumber = contact.CivilIDNumber,
                                    StatusID = contact.StatusID,
                                });
                            }
                        }

                        if (entity.SupplierAccountMaps.IsNotNull() && entity.SupplierAccountMaps.Count > 0)
                        {
                            supplier.SupplierAccountMaps = new List<SupplierAccountMap>();

                            foreach (var account in entity.SupplierAccountMaps)
                            {
                                dbContext.Entry(account).Reference(a => a.Account).Load();
                                supplier.SupplierAccountMaps.Add(account);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return supplier;
        }

        public List<Supplier> GetSuppliers(string searchText, int dataSize, int? companyID)
        {
            List<Supplier> suppliers = new List<Supplier>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        suppliers = (from supplier in dbContext.Suppliers
                                     where string.Concat(supplier.FirstName + supplier.MiddleName + supplier.LastName).Contains(searchText)
                                     select supplier).Where(a => (a.StatusID == null || a.StatusID == 1) && a.CompanyID == companyID).OrderBy(a => new { a.FirstName, a.LastName }).Take(dataSize).ToList();
                    }
                    else
                    {
                        if (dataSize > 0) //Getting only 50 data from table
                        {
                            suppliers = (from supplier in dbContext.Suppliers
                                         select supplier).Where(a => (a.StatusID == null || a.StatusID == 1) && a.CompanyID == companyID).OrderBy(a => new {a.SupplierIID }).Take(dataSize).ToList();
                        }
                        else //Getting all the data from table 
                        {
                            suppliers = (from supplier in dbContext.Suppliers
                                         select supplier).Where(a => (a.StatusID == null || a.StatusID == 1) && a.CompanyID == companyID).OrderBy(a => new { a.FirstName, a.LastName }).ToList();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return suppliers;
        }

        public Supplier SaveSupplier(Supplier supplier, int? companyID)
        {
            bool result = false;
            try
            {
                if (supplier.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (supplier.SupplierIID <= 0)
                        {
                            dbContext.Suppliers.Add(supplier);
                        }
                        else
                        {
                            Supplier supplierEntity = dbContext.Suppliers.Where(x => x.SupplierIID == supplier.SupplierIID && x.CompanyID == companyID).FirstOrDefault();

                            if (supplierEntity.IsNotNull())
                            {
                                supplierEntity.SupplierIID = supplier.SupplierIID;
                                supplierEntity.LoginID = supplier.LoginID;
                                supplierEntity.TitleID = supplier.TitleID;
                                supplierEntity.FirstName = supplier.FirstName;
                                supplierEntity.MiddleName = supplier.MiddleName;
                                supplierEntity.LastName = supplier.LastName;
                                supplierEntity.StatusID = supplier.StatusID;
                                supplierEntity.SupplierCode = supplier.SupplierCode;
                                supplierEntity.SupplierAddress = supplier.SupplierAddress;

                                supplierEntity.Telephone = supplier.Telephone;
                                supplierEntity.SupplierEmail = supplier.SupplierEmail;
                                supplierEntity.VendorCR = supplier.VendorCR;
                                supplierEntity.CRExpiry = supplier.CRExpiry;
                                supplierEntity.VendorNickName = supplier.VendorNickName;
                                supplierEntity.CompanyLocation = supplier.CompanyLocation;
                                supplierEntity.IsMarketPlace = supplier.IsMarketPlace;
                                supplierEntity.BranchID = supplier.BranchID;
                                supplierEntity.ReturnMethodID = supplier.ReturnMethodID == 0 ? (int?)null : supplier.ReturnMethodID;
                                supplierEntity.ReceivingMethodID = supplier.ReceivingMethodID == 0 ? (int?)null : supplier.ReceivingMethodID;
                                supplierEntity.CompanyID = supplier.CompanyID;
                                supplierEntity.Profit = supplier.Profit;
                                supplierEntity.UpdatedBy = supplier.UpdatedBy;
                                supplierEntity.UpdateDate = supplier.UpdateDate;

                                // Delete contacts for incoming supplier
                                var contacts = dbContext.Contacts.Where(x => x.SupplierID == supplier.SupplierIID).ToList();
                                if (contacts.IsNotNull())
                                    dbContext.Contacts.RemoveRange(contacts);

                                if (supplier.Contacts.IsNotNull() && supplier.Contacts.Count > 0)
                                {
                                    foreach (var contact in supplier.Contacts)
                                    {
                                        // Add contacts for incoming supplier
                                        contact.SupplierID = supplier.SupplierIID;
                                        supplierEntity.Contacts.Add(contact);
                                    }
                                }
                            }
                        }

                        if (supplier.Login != null)
                        {
                            if (supplier.Login.LoginIID == 0)
                            {
                                dbContext.Entry(supplier.Login).State = System.Data.Entity.EntityState.Added;

                                // Add rolemap for newly created customer
                                var loginRoleMap = new LoginRoleMap
                                {
                                    RoleID = 2,
                                    LoginID = supplier.Login.LoginIID,
                                    CreatedBy = supplier.CreatedBy,
                                    CreatedDate = supplier.CreatedDate,
                                    UpdatedBy = supplier.UpdatedBy,
                                    UpdatedDate = supplier.UpdateDate,
                                };
                                dbContext.Entry(loginRoleMap).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                                dbContext.Entry(supplier.Login).State = System.Data.Entity.EntityState.Modified;

                            if (supplier.Login.Password == null)
                            {
                                dbContext.Entry(supplier.Login).Property(a => a.Password).IsModified = false;
                                dbContext.Entry(supplier.Login).Property(a => a.PasswordSalt).IsModified = false;
                            }
                        }

                        //if (supplier.SupplierAccountMaps != null)
                        //{
                        //    foreach (var map in supplier.SupplierAccountMaps)
                        //    {
                        //        if (map.SupplierAccountMapIID != 0)
                        //        {
                        //            dbContext.Entry(map).State = EntityState.Unchanged;
                        //            dbContext.Entry(map.Account).State = EntityState.Unchanged;
                        //        }
                        //    }
                        //}

                        dbContext.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            if (result)
                return supplier;
            else
                return null;
        }

        public List<Supplier> GetSupplierBySupplierIdAndCR(string searchText)
        {
            int pageSize = Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("MaxFetchCount").ToString());
            List<Supplier> suppliers = new List<Supplier>();
            dbEduegateERPContext db = new dbEduegateERPContext();
            if (string.IsNullOrEmpty(searchText))
            {
                suppliers = db.Suppliers.Take(pageSize).ToList();
            }
            else
            {
                suppliers = db.Suppliers.Where(x => x.SupplierIID == (string.IsNullOrEmpty(searchText) ? 0 : Convert.ToInt64(searchText))
                || x.VendorCR == searchText).Take(pageSize).ToList();
            }
            return suppliers;
        }

        public List<SupplierStatus> GetSupplierStatuses()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.SupplierStatuses.ToList();
            }
        }

        public ProductPriceListSKUMap GetSKUPriceDetailByBranch(long branchID, long skuMapID)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    var priceDetail = db.ProductPriceListBranchMaps
                      .Include(p => p.ProductPriceList)
                      .Include(p => p.ProductPriceList.ProductPriceListSKUMaps)
                      .Where(pplbm => pplbm.BranchID == branchID && pplbm.ProductPriceList.ProductPriceListTypeID == 2).FirstOrDefault(); // ProductPriceListTypeID = 2 for base pricelist 

                    if (priceDetail.IsNotNull())
                        return priceDetail.ProductPriceList.ProductPriceListSKUMaps.Where(p => p.ProductSKUID == skuMapID).FirstOrDefault();
                    else
                        return null;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public Supplier UpdateSupplierBranch(Supplier supplier)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.Suppliers.Attach(supplier);
                var entry = dbContext.Entry(supplier);
                entry.State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            return supplier;
        }

        public Supplier GetSuplierByProductPriceListId(long productPriceListId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var supplier = (from pplbm in db.ProductPriceListBranchMaps
                                join
                                s in db.Suppliers on pplbm.BranchID equals s.BranchID
                                where pplbm.ProductPriceListID == productPriceListId
                                select s).FirstOrDefault();

                return supplier;
            }
        }

        public List<SupplierAccountMap> SaveSupplierAccountMaps(List<SupplierAccountMap> entityList)
        {
            try
            {
                long supplierId = 0;
                var entity = entityList.FirstOrDefault();
                if (entity != null)
                {
                    supplierId = (long)entity.SupplierID;
                }
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    List<SupplierAccountMap> existingDBList = dbContext.SupplierAccountMaps.ToList();
                    foreach (SupplierAccountMap supplierAccountMap in entityList)
                    {

                        if (existingDBList != null)
                        {
                            SupplierAccountMap supplierAccountMapInDB = existingDBList.Where(a => a.SupplierAccountMapIID == supplierAccountMap.SupplierAccountMapIID).FirstOrDefault();
                            if (supplierAccountMapInDB == null)
                            {
                                dbContext.SupplierAccountMaps.Add(supplierAccountMap);
                            }
                            else
                            {
                                //supplierAccountMapInDB.AccountID = supplierAccountMap.AccountID;
                                if (supplierAccountMap.AccountID != null)
                                {
                                    supplierAccountMapInDB.AccountID = supplierAccountMap.AccountID; // Base account
                                }
                                else
                                {
                                    supplierAccountMapInDB.Account = supplierAccountMap.Account;
                                }
                            }
                        }
                        else
                        {
                            dbContext.SupplierAccountMaps.Add(supplierAccountMap);
                        }

                    }
                    dbContext.SaveChanges();
                }
                return this.GetSupplierAccountMaps(supplierId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return entityList;
        }
        public List<SupplierAccountMap> GetSupplierAccountMaps(long SupplierId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.SupplierAccountMaps
                        .Include(a => a.EntityTypeEntitlement)
                        .Include(b => b.Account)
                        .Include("Account.Group")
                        .Include("Account.Account1")
                        .Include("Account.AccountBehavoir")
                        .Where(a=>a.SupplierID == SupplierId).ToList();
            }
        }

        public List<ReceivingMethod> GetReceivingMethodDetails()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ReceivingMethods.OrderBy(a => a.ReceivingMethodName).ToList();
            }
        }

        public List<ReturnMethod> GetReturnMethodDetails()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ReturnMethods.OrderBy(a => a.ReturnMethodName).ToList();
            }
        }

        public ReceivingMethod GetSupplierDeliveryMethod(long supplierID)
        { 
            using (dbEduegateERPContext db = new dbEduegateERPContext())  
            {
                var supplier = (from s in db.Suppliers join rm in
                                db.ReceivingMethods on s.ReceivingMethodID equals rm.ReceivingMethodID
                                where s.SupplierIID == supplierID
                                select rm).FirstOrDefault();
                return supplier;
            }
        }

        public ReturnMethod GetSupplierReturnMethod(long supplierID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            { 
                var supplierReturn = (from s in db.Suppliers
                                join rm in
                                db.ReturnMethods on s.ReturnMethodID equals rm.ReturnMethodID
                                where s.SupplierIID == supplierID
                                select rm).FirstOrDefault();
                return supplierReturn; 
            }
        }
    }
}
