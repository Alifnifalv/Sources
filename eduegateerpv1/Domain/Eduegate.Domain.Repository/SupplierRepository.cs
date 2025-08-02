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
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity;
using Eduegate.Services.Contracts.Vendor;
using Eduegate.Domain.Setting;
using Eduegate.Framework.Security;
using Eduegate.Framework;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts;

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
                    return dbContext.Suppliers.Where(x => x.LoginID == loginID)
                        .AsNoTracking()
                        .FirstOrDefault();
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
                    return dbContext.Suppliers.Where(x => x.BranchID == branchID)
                        .AsNoTracking()
                        .FirstOrDefault();
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

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Suppliers.Where(x => x.SupplierIID == supplierID)
                    .Include(x => x.SupplierStatus)
                    .Include(x => x.Login)
                    .Include(x => x.Employee)
                    .Include(x => x.ReturnMethod)
                    .Include(x => x.ReceivingMethod)
                    .Include(x => x.BusinessType)
                    .Include(x => x.TaxJurisdictionCountry)
                    .Include(x => x.SupplierContentIDs)
                    .Include(x => x.SupplierAccountMaps).ThenInclude(x => x.Account)
                    .AsNoTracking()
                    .FirstOrDefault();

                supplier = entity;
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
                        suppliers = dbContext.Suppliers
                            .Where(supplier =>
                                string.Concat(supplier.FirstName, supplier.MiddleName, supplier.LastName).Contains(searchText) &&
                                (supplier.StatusID == null || supplier.StatusID == 1) &&
                                supplier.CompanyID == companyID)
                            .OrderBy(a => a.FirstName)
                            .ThenBy(a => a.LastName)
                            .Take(dataSize)
                            .AsNoTracking()
                            .ToList();
                    }
                    else
                    {
                        if (dataSize > 0) //Getting only 50 data from table
                        {
                            suppliers = (from supplier in dbContext.Suppliers
                                         select supplier).Where(a => (a.StatusID == null || a.StatusID == 1) && a.CompanyID == companyID).OrderBy(a => a.SupplierIID).Take(dataSize)
                                         .AsNoTracking()
                                         .ToList();
                        }
                        else //Getting all the data from table 
                        {
                            suppliers = (from supplier in dbContext.Suppliers
                                         select supplier).Where(a => (a.StatusID == null || a.StatusID == 1) && a.CompanyID == companyID).OrderBy(a => a.FirstName).ThenBy(a => a.LastName)
                                         .AsNoTracking()
                                         .ToList();
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

        #region SaveSupplier Old Code --- NotUsing --11/7/2024
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
                            Supplier supplierEntity = dbContext.Suppliers.Where(x => x.SupplierIID == supplier.SupplierIID && x.CompanyID == companyID)
                                .AsNoTracking()
                                .FirstOrDefault();

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
                                var contacts = dbContext.Contacts.Where(x => x.SupplierID == supplier.SupplierIID)
                                    .AsNoTracking()
                                    .ToList();
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
                                dbContext.Entry(supplier.Login).State = Microsoft.EntityFrameworkCore.EntityState.Added;

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
                                dbContext.Entry(loginRoleMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                                dbContext.Entry(supplier.Login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
        #endregion

        public List<Supplier> GetSupplierBySupplierIdAndCR(string searchText)
        {
            int pageSize = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>("MaxFetchCount").ToString());
            List<Supplier> suppliers = new List<Supplier>();
            dbEduegateERPContext db = new dbEduegateERPContext();
            if (string.IsNullOrEmpty(searchText))
            {
                suppliers = db.Suppliers.Take(pageSize)
                    .AsNoTracking()
                    .ToList();
            }
            else
            {
                suppliers = db.Suppliers.Where(x => x.SupplierIID == (string.IsNullOrEmpty(searchText) ? 0 : Convert.ToInt64(searchText))
                || x.VendorCR == searchText).Take(pageSize)
                .AsNoTracking()
                .ToList();
            }
            return suppliers;
        }

        public List<SupplierStatus> GetSupplierStatuses()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.SupplierStatuses
                    .AsNoTracking()
                    .ToList();
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
                      .Where(pplbm => pplbm.BranchID == branchID && pplbm.ProductPriceList.ProductPriceListTypeID == 2)
                      .AsNoTracking()
                      .FirstOrDefault(); // ProductPriceListTypeID = 2 for base pricelist 

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
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                                select s)
                                .AsNoTracking()
                                .FirstOrDefault();

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
                        .Include(i => i.Account).ThenInclude(i => i.Group)
                        .Include(i => i.Account).ThenInclude(i => i.ParentAccount)
                        .Include(i => i.Account).ThenInclude(i => i.AccountBehavoir)
                        .Where(a=>a.SupplierID == SupplierId)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public List<ReceivingMethod> GetReceivingMethodDetails()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ReceivingMethods.OrderBy(a => a.ReceivingMethodName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<ReturnMethod> GetReturnMethodDetails()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ReturnMethods.OrderBy(a => a.ReturnMethodName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public ReceivingMethod GetSupplierDeliveryMethod(long supplierID)
        { 
            using (dbEduegateERPContext db = new dbEduegateERPContext())  
            {
                var supplier = (from s in db.Suppliers join rm in
                                db.ReceivingMethods on s.ReceivingMethodID equals rm.ReceivingMethodID
                                where s.SupplierIID == supplierID
                                select rm)
                                .AsNoTracking()
                                .FirstOrDefault();
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
                                select rm)
                                .AsNoTracking()
                                .FirstOrDefault();
                return supplierReturn; 
            }
        }


        //Vendor Portal New Vendor Registration
        #region Registration  ---  Start
        public VendorRegisterDTO NewVendorRegistration(VendorRegisterDTO registrationDTO)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    //Duplicate Validation with emailId
                    var supliers = db.Suppliers.Where(x => x.SupplierEmail == registrationDTO.Email)
                        .AsNoTracking()
                        .ToList();

                    if(supliers.Count > 0)
                    {
                        registrationDTO.IsError = true;
                        registrationDTO.ReturnMessage = "Vendor is already registered! please check";
                    }
                    else
                    {
                        var roleID = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>("Vendor_Portal_RoleID").ToString());
                        var statusID = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>("SUPPLIER_REGISTERED_STATUSID").ToString());
                        
                        MutualRepository mutualRepository = new MutualRepository();
                        Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

                        sequence = mutualRepository.GetNextSequence("SupplierCode", null);

                        string supplierCode = sequence.LastSequence.ToString().PadLeft((int)sequence.ZeroPadding, '0');

                        //Create Login
                        var login = new Login()
                        {
                            LoginUserID = registrationDTO.VendorCr,
                            LoginEmailID = registrationDTO.Email,
                            UserName = sequence.Prefix + supplierCode,
                            StatusID = 1,
                            CreatedDate = DateTime.Now,
                        };

                        login.PasswordSalt = PasswordHash.CreateHash(registrationDTO.Password);
                        //encryt the value to save in the DB as Password
                        login.Password = StringCipher.Encrypt(registrationDTO.Password, login.PasswordSalt);

                        db.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        db.SaveChanges();

                        login.LoginRoleMaps.Add(new LoginRoleMap()
                        {
                            LoginID = login.LoginIID,
                            RoleID = roleID,
                            CreatedBy = (int?)login.LoginIID,
                            CreatedDate = DateTime.Now,
                        });

                        var supplier = new Supplier()
                        {
                            SupplierCode = sequence.Prefix + supplierCode,
                            FirstName = registrationDTO.FirstName,
                            //MiddleName = registrationDTO.MiddleName,
                            //LastName = registrationDTO.LastName,
                            VendorCR = registrationDTO.VendorCr,
                            SupplierEmail = registrationDTO.Email,
                            Telephone = registrationDTO.TelephoneNo,
                            LoginID = login.LoginIID,
                            StatusID = (byte?)statusID,
                        };

                        db.Suppliers.Add(supplier);

                        db.SaveChanges();

                        registrationDTO.SupplierIID = supplier.SupplierIID;
                        registrationDTO.SupplierCode = supplier.SupplierCode;
                        registrationDTO.LoginID = supplier.LoginID;

                        registrationDTO.IsError = false;
                        registrationDTO.ReturnMessage = "Vendor Registered Successfully";
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(ex.Message, ex);
                    registrationDTO.ReturnMessage = "Something went wrong! please contact admin";
                    registrationDTO.IsError = true;
                }

            }
            return registrationDTO;
        }
        #endregion Registration --- End

        #region GetQuotationList --- Start
        public List<PurchaseQuotationListDTO> GetQuotationListByLoginID(long loginID)
        {
            var listDto = new List<PurchaseQuotationListDTO>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var rfqHead = db.TransactionHeads.Where(x => x.RFQSupplierRequestMapHeads.Any(y => y.Supplier.LoginID == loginID))
                    .Include(x => x.RFQSupplierRequestMapHeads).ThenInclude(y => y.Supplier)
                    .Include(x => x.TransactionDetails).ThenInclude(u => u.ProductSKUMap)
                    .Include(x => x.TransactionDetails).ThenInclude(i => i.Unit)
                    .Include(x => x.School).OrderByDescending(x => x.HeadIID)
                    .ToList();
                
                foreach (var rfq in rfqHead)
                {
                    var detailListDto = new List<PurchaseQuotationListDetailDTO>();

                    if (rfq.TransactionDetails.Count > 0)
                    {
                        foreach (var detail in rfq.TransactionDetails)
                        {
                            detailListDto.Add(new PurchaseQuotationListDetailDTO()
                            {
                                DetailIID = detail.DetailIID,
                                ProductID = detail.ProductID,
                                ProductSKUMapID = detail.ProductSKUMapID,
                                Product = detail.ProductSKUMap.SKUName,
                                ProductCode = detail.ProductSKUMap.ProductSKUCode,
                                Quantity = detail.Quantity,
                                Unit = detail.Unit.UnitName,
                                Price = detail.UnitPrice,
                                Amount = detail.Amount,
                            });
                        }
                    }

                    if (rfqHead.Count > 0)
                    {
                        listDto.Add(new PurchaseQuotationListDTO()
                        {
                            Date = rfq.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                            QuotationNo = rfq.TransactionNo,
                            FromSchool = rfq.School.SchoolName,
                            Validity = rfq.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                            Status = "Edit",
                            DetailsDTO = detailListDto,
                            TotalAmount = detailListDto.Sum(x => x.Amount),
                            TotalPrice = detailListDto.Sum(x => x.Price),
                            TotalQuantity = detailListDto.Sum(x => x.Quantity)
                        });
                    }
                }

            }

            return listDto;
        }
        #endregion List --- End

        #region GetQuotationItemList by HeadIID --- Start
        public TransactionDTO GetRFQItemListByIID(long iid,long? supplierId)
        {
            bool IsEditable = true;

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var docTypeID = new SettingBL().GetSettingValue<string>("QUOTATION_DOC_TYP_ID"); //Quotation Document TypeID

            var rfqDTO = new TransactionDTO();
            var rfqHeadDTO = new TransactionHeadDTO();
            var rfqDetailDTO = new List<TransactionDetailDTO>();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var rfqHead = db.TransactionHeads.Where(x => (x.ReferenceHeadID == iid && supplierId == x.SupplierID) || x.HeadIID == iid)
                                .Include(x => x.RFQSupplierRequestMapHeads).ThenInclude(y => y.Supplier)
                                .Include(x => x.TransactionDetails).ThenInclude(u => u.ProductSKUMap)
                                .Include(x => x.TransactionDetails).ThenInclude(i => i.Unit)
                                .Include(x => x.School).ToList().LastOrDefault();

                //Validation for new edit cases
                //Case 1 : New Entry :: If RFQ doesn't have any referenceHeadID then its new Quotation
                //Case 2 : Edit case :: Have an referenceHeadID but it is not created by the user > then it will be new Quotation
                //Case 3 : Edit case :: Have an referenceHeadID and its created by the user > then edit and save screen
                
                if(int.Parse(docTypeID) == rfqHead.DocumentTypeID)
                {
                    if (rfqHead != null && rfqHead.ReferenceHeadID == null) // Case 1
                    {
                        IsEditable = false;
                    }
                    else if (rfqHead != null && rfqHead.ReferenceHeadID.HasValue)
                    {
                        if (rfqHead.SupplierID == supplierId) // Case 3
                        {
                            IsEditable = true;
                        }
                        else // Case 2
                        {
                            IsEditable = false;
                        }
                    }
                }
                else
                {
                    IsEditable = false;
                }


                foreach (var detail in rfqHead.TransactionDetails)
                {
                    rfqDetailDTO.Add(new TransactionDetailDTO()
                    {
                        DetailIID = IsEditable ?  detail.DetailIID : 0,
                        ProductID = detail.ProductID,
                        ProductSKUMapID = detail.ProductSKUMapID,
                        ProductName = detail.ProductSKUMap?.SKUName,
                        ProductCode = detail.ProductSKUMap?.ProductSKUCode,
                        Quantity = detail.Quantity,
                        UnitID = detail.UnitID,
                        Unit = detail.Unit?.UnitName,
                        UnitPrice = detail.UnitPrice,
                        Amount = detail.Amount,
                        UnitGroupID = detail.Unit?.UnitGroupID,
                        Fraction = detail.Fraction,
                        ForeignRate = detail.ForeignRate,
                    });
                }
                rfqHeadDTO = new TransactionHeadDTO()
                {
                    HeadIID = IsEditable ? rfqHead.HeadIID : 0,
                    TransactionNo = rfqHead.TransactionNo,
                    DueDate = rfqHead.DueDate,
                    Validity = rfqHead.DueDate.HasValue ? rfqHead.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    SchoolID = rfqHead.SchoolID,
                    ReferenceHeadID = rfqHead.ReferenceHeadID.HasValue ? rfqHead.ReferenceHeadID : rfqHead.HeadIID,
                    DocumentStatusID = rfqHead.DocumentTypeID != int.Parse(docTypeID) ? 1 : rfqHead.DocumentStatusID,
                    IsQuotation = rfqHead.DocumentTypeID == int.Parse(docTypeID) ? true : false,
                    DocumentStatusName = rfqHead.DocumentStatusID == null ? db.TransactionStatuses.FirstOrDefault(t => t.TransactionStatusID == rfqHead.TransactionStatusID)?.Description : db.DocumentStatuses.FirstOrDefault(x => x.DocumentStatusID == rfqHead.DocumentStatusID)?.StatusName,
                    DocumentTypeID = rfqHead.DocumentTypeID,
                    DiscountAmount = rfqHead.DiscountAmount,
                    DiscountPercentage = rfqHead.DiscountPercentage,
                };

                rfqDTO.TransactionHead = rfqHeadDTO;
                rfqDTO.TransactionDetails = rfqDetailDTO;
            }
            return rfqDTO;
        }
        #endregion List --- End

        #region Tender Bid Opening --- Start

        public TenderAuthenticationDTO BidLoginValidate(TenderAuthenticationDTO bidLoginDTO)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var getAuth = db.TenderAuthentications
                                                .Where(auth => (auth.UserID == bidLoginDTO.UserID || auth.UserName == bidLoginDTO.UserID) &&
                                                    auth.EmailID == bidLoginDTO.EmailID).AsNoTracking().ToList().LastOrDefault();

                    if (getAuth != null)
                    {
                        if(getAuth.IsActive == false)
                        {
                            bidLoginDTO.IsError = true;
                            bidLoginDTO.ReturnMessage = "The user you entered is not active. Please contact the administrator.";
                        }
                        else
                        {
                            string encryptPassword = "";

                            encryptPassword = StringCipher.Encrypt(bidLoginDTO.Password, getAuth.PasswordSalt);

                            bidLoginDTO.Password = encryptPassword;

                            if (getAuth.Password == bidLoginDTO.Password)
                            {
                                bidLoginDTO.IsError = false;
                                bidLoginDTO.AuthenticationID = getAuth.AuthenticationID;
                                bidLoginDTO.LoginID = getAuth.LoginID.HasValue ? getAuth.LoginID : getAuth.AuthenticationID ;
                                bidLoginDTO.UserID = getAuth.UserID;
                                bidLoginDTO.EmailID = getAuth.EmailID;
                            }
                            else
                            {
                                bidLoginDTO.IsError = true;
                                bidLoginDTO.ReturnMessage = "Invalid user, email, or password. Please check.";
                            }
                        }
                        
                    }
                    else
                    {
                        bidLoginDTO.ReturnMessage = "Something went wrong!. Please contact the administrator";
                        bidLoginDTO.IsError = true;
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(ex.Message, ex);
                    bidLoginDTO.ReturnMessage = "Something went wrong! please contact admin";
                    bidLoginDTO.IsError = true;
                }

            }
            return bidLoginDTO;
        }


        public TenderAuthenticationDTO GetBidUserDetails(long id) 
        {
            var result = new TenderAuthenticationDTO();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var getData = db.TenderAuthentications
                                                .AsNoTracking()
                                                .FirstOrDefault(x => x.AuthenticationID == id);

                    if(getData != null)
                    {
                        result = new TenderAuthenticationDTO()
                        {
                            AuthenticationID = getData.AuthenticationID,
                            UserName = getData.UserName,
                            EmailID = getData.EmailID,
                            UserID = getData.UserID,
                            LoginID = getData.LoginID.HasValue ? getData.LoginID : getData.AuthenticationID,
                            IsApprover = db.TenderAuthenticationLogs.FirstOrDefault(x => x.TenderID == getData.TenderID && x.AuthenticationID == getData.AuthenticationID).IsApprover,
                        };
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(ex.Message, ex);
                    result.ReturnMessage = "Something went wrong! please contact admin";
                    result.IsError = true;
                }

            }
            return result;
        }
        
        public List<TenderAuthenticationDTO> GetBidUserListByTenderID(long tenderID) 
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var result = new List<TenderAuthenticationDTO>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var getData = db.TenderAuthenticationLogs
                                                .AsNoTracking()
                                               .Where(x => x.TenderID == tenderID && x.IsActive == true);

                    if (getData != null)
                    {
                        foreach (var auth in getData)
                        {
                            var userData = db.TenderAuthentications
                                .Include(x => x.Tender)
                                .Where(x => x.AuthenticationID == auth.AuthenticationID).ToList().LastOrDefault();

                            if(userData != null)
                            {
                                result.Add(new TenderAuthenticationDTO()
                                {
                                    UserName = userData.UserName,
                                    EmailID = userData.EmailID,
                                    UserID = userData.UserID,
                                    IsTenderApproved = auth.IsTenderApproved,
                                    IsTenderOpened = auth.IsTenderOpened,
                                    OpenedDateString = auth.OpenedDate.HasValue ? auth.OpenedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                    OpenedTime = auth.OpenedDate.HasValue ? auth.OpenedDate.Value.ToString("hh:mm tt") : null,
                                    NumOfAuthorities = userData.Tender.NumOfAuthorities,
                                    LoginID = userData.LoginID.HasValue ? userData.LoginID : userData.AuthenticationID,
                                    TenderAuthMapIID = auth.TenderAuthMapIID,
                                    IsApprover = auth.IsApprover
                                });
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(ex.Message, ex);
                }

            }
            return result;
        }


        public List<TenderMasterDTO> GetTenderList(long loginID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var result = new List<TenderMasterDTO>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var tenderIDs = db.TenderAuthenticationLogs
                                               .Where(x => x.AuthenticationID == loginID).Select(x => x.TenderID).ToList();

                    var entity = db.Tenders.Where(x => tenderIDs.Contains(x.TenderIID))
                        .AsNoTracking().ToList();

                    if (entity != null)
                    {
                        foreach (var tender in entity)
                        {
                            result.Add(new TenderMasterDTO()
                            {
                                TenderIID = tender.TenderIID,
                                NumOfAuthorities = tender.NumOfAuthorities,
                                StartDateString = tender.StartDate.HasValue ? tender.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                EndDateString = tender.EndDate.HasValue ? tender.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                OpeningDateString = tender.OpeningDate.HasValue ? tender.OpeningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                IsActive = tender.IsActive == true ? true : false,
                                IsOpened = tender.IsOpened,
                                Name = tender.Name,
                                Title = tender.Title,
                                Description = tender.Description,
                                LoginID = (long?)loginID,
                            });
                        }

                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(ex.Message, ex);
                }

            }
            return result;
        }

        public bool OpenAndUpdateTenderLog(long iid)
        {
            var result = false;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var entity = db.TenderAuthenticationLogs
                                                .AsNoTracking()
                                               .FirstOrDefault(x => x.TenderAuthMapIID == iid);

                    if (entity != null)
                    {
                        entity.IsTenderOpened = true;
                        entity.OpenedDate = DateTime.Now;
                        entity.Remarks = "Bid opended on : " + DateTime.Now.ToString(dateFormat);

                        db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();

                        if (entity.TenderID.HasValue)
                        {
                            var userList = GetBidUserListByTenderID(entity.TenderID.Value);

                            int NumOfAuth = (int)userList.FirstOrDefault().NumOfAuthorities;
                            int OpenedUsersCount = userList.Where(x => x.IsTenderOpened == true).Count();
                            
                            if (OpenedUsersCount >= NumOfAuth)
                            {
                                result = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<SupplierRepository>.Fatal(ex.Message, ex);
                }

            }
            return result;
        }

        #endregion --- End
    }
}
