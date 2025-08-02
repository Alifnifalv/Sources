using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Repository
{
    public class UserServiceRepository
    {

        public string UpdateProfileDetails(Login login)
        {
            string message = string.Empty;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (login.IsNotNull())
                    {
                        Login loginEntity = dbContext.Logins.Where(x => x.LoginEmailID == login.LoginEmailID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (loginEntity.IsNotNull())
                        {
                            loginEntity.LoginUserID = login.LoginUserID;
                            loginEntity.LoginEmailID = login.LoginEmailID;
                            loginEntity.Password = login.Password;
                            loginEntity.PasswordSalt = login.PasswordSalt;
                            loginEntity.StatusID = login.StatusID;
                            loginEntity.CreatedDate = login.CreatedDate;
                            loginEntity.UpdatedDate = login.UpdatedDate;
                            loginEntity.LoginRoleMaps = login.LoginRoleMaps;

                            dbContext.SaveChanges();
                        }

                        if (login.Customers.IsNotNull())
                        {
                            foreach (Customer customer in login.Customers)
                            {
                                Customer customerEntity = dbContext.Customers.Where(x => x.CustomerIID == customer.CustomerIID).FirstOrDefault();

                                if (customerEntity.IsNotNull())
                                {
                                    customerEntity.CustomerIID = customer.CustomerIID;
                                    customerEntity.LoginID = customer.LoginID;
                                    customerEntity.FirstName = customer.FirstName;
                                    customerEntity.MiddleName = customer.MiddleName;
                                    customerEntity.LastName = customer.LastName;
                                    customerEntity.TitleID = customer.TitleID;
                                    customerEntity.IsDifferentBillingAddress = customer.IsDifferentBillingAddress;
                                    customerEntity.IsSubscribeForNewsLetter = customer.IsSubscribeForNewsLetter;
                                    customerEntity.IsTermsAndConditions = customer.IsTermsAndConditions;
                                    //customerEntity.CountryID = customer.CountryID;
                                    customerEntity.PassportNumber = customer.PassportNumber;
                                    customerEntity.PassportIssueCountryID = customer.PassportIssueCountryID;
                                    customerEntity.CivilIDNumber = customer.CivilIDNumber;
                                    //customerEntity.TelephoneCode = customer.TelephoneCode;
                                    //customerEntity.TelephoneNumber = customer.TelephoneNumber;

                                    dbContext.SaveChanges();
                                }
                            }
                        }

                        if (login.Contacts.IsNotNull() && login.Contacts.Count > 0)
                        {
                            foreach (Contact contact in login.Contacts)
                            {
                                Contact contactEntity = dbContext.Contacts.Where(x => x.ContactIID == contact.ContactIID).FirstOrDefault();

                                if (contactEntity.IsNotNull())
                                {
                                    contactEntity.ContactIID = contact.ContactIID;
                                    contactEntity.LoginID = contact.LoginID;
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
                                    contactEntity.TelephoneCode = contact.TelephoneCode;
                                    contactEntity.PhoneNo1 = contact.PhoneNo1;

                                    dbContext.SaveChanges();
                                }
                            }
                        }

                        message = "Profile details updated successfully";
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserServiceRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }

            return message;
        }


        public bool UpdatePersonalProfileDetails(Login login)
        {
            string message = string.Empty;
            bool result = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (login.IsNotNull())
                    {
                        Login loginEntity = login.LoginIID != 0 ? dbContext.Logins.Where(x => x.LoginIID == login.LoginIID)
                            .AsNoTracking()
                            .FirstOrDefault()  : dbContext.Logins.Where(x => x.LoginEmailID == login.LoginEmailID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        loginEntity.LoginEmailID = login.LoginEmailID;

                        if (login.Customers.IsNotNull())
                        {
                            foreach (Customer customer in login.Customers)
                            {
                                Customer customerEntity = dbContext.Customers.Where(x => x.CustomerIID == customer.CustomerIID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                if (customerEntity.IsNotNull())
                                {
                                    customerEntity.DefaultBranchID = customer.DefaultBranchID;
                                    customerEntity.FirstName = customer.FirstName;
                                    customerEntity.LastName = customer.LastName;
                                    customerEntity.UpdatedDate = DateTime.Now;
                                    customerEntity.CustomerAddress = customer.CustomerAddress;
                                    customerEntity.AddressLongitude = customer.AddressLongitude;
                                    customerEntity.AddressLatitude = customer.AddressLatitude;
                                    customerEntity.GenderID = customer.GenderID;
                                    //customerEntity.Telephone = customer.Telephone;
                                    customerEntity.IsSubscribeForNewsLetter = customer.IsSubscribeForNewsLetter;
                                    var billingAddressContact = dbContext.Contacts.Where(a => a.LoginID == login.LoginIID && a.IsBillingAddress == true)
                                        .AsNoTracking()
                                        .FirstOrDefault();
                                    //var updatedContact = customer.Contacts.FirstOrDefault();
                                    if (billingAddressContact.IsNotNull())
                                    {
                                        billingAddressContact.FirstName = customer.FirstName;
                                        billingAddressContact.LastName = customer.LastName;
                                        billingAddressContact.MobileNo1 = customer.Telephone;
                                    }
                                    dbContext.SaveChanges();
                                    result = true;
                                }
                            }
                        }
                        if (result)
                        {
                            message = "Personal Profile details updated successfully";
                        }
                        else
                        {
                            message = "Error! Please try later";
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserServiceRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }

            return result;
        }

        public string UpdateAddress(Contact contact)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (contact.IsNotNull())
                    {
                        if (contact.ContactIID > 0)
                        {
                            Contact contactEntity = dbContext.Contacts.Where(x => x.ContactIID == contact.ContactIID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            contactEntity.ContactIID = contact.ContactIID;
                            contactEntity.LoginID = contact.LoginID;
                            contactEntity.FirstName = contact.FirstName;
                            contactEntity.MiddleName = contact.MiddleName;
                            contactEntity.LastName = contact.LastName;
                            contactEntity.TitleID = contact.TitleID;
                            contactEntity.AddressLine1 = contact.AddressLine1;
                            contactEntity.AddressLine2 = contact.AddressLine2;
                            contactEntity.AddressName = contact.AddressName;
                            contactEntity.City = contact.City;
                            contactEntity.CountryID = contact.CountryID;
                            contactEntity.PostalCode = contact.PostalCode;
                            contactEntity.State = contact.State;
                            contactEntity.TelephoneCode = contact.TelephoneCode;
                            contactEntity.PhoneNo1 = contact.PhoneNo1;
                            contactEntity.IsBillingAddress = contact.IsBillingAddress;
                            contactEntity.IsShippingAddress = contact.IsShippingAddress;

                            dbContext.SaveChanges();
                        }
                        else
                        {
                            dbContext.Contacts.Add(contact);
                            dbContext.SaveChanges();
                        }

                        List<Contact> contacts = dbContext.Contacts.Where(x => x.LoginID == contact.LoginID)
                            .AsNoTracking()
                            .ToList();

                        if (contacts.IsNotNull() && contacts.Count > 0)
                        {
                            foreach (Contact contactEnt in contacts)
                            {
                                if (contactEnt.ContactIID != contact.ContactIID)
                                {
                                    if (contact.IsShippingAddress == true)
                                        contactEnt.IsShippingAddress = false;
                                    if (contact.IsBillingAddress == true)
                                        contactEnt.IsBillingAddress = false;

                                    dbContext.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to create new address :" + contact.ContactIID, 0);
            }

            return "Address created/updated successfully";
        }

        public bool DeleteAddressBook(decimal addressIID)
        {
            bool result = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    Contact contactEntity = dbContext.Contacts.Where(x => x.ContactIID == addressIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    dbContext.Contacts.Remove(contactEntity);
                    dbContext.SaveChanges();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to delete address. Address IId:" + addressIID, 0);
            }

            return result;
        }

        public List<HistoryHeader> GetBriefOrderHistoryDetails(string branchID, string customerIID, long orderID, int companyID, string lang = "en")
        {
            List<HistoryHeader> userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int branchIDTo = Convert.ToInt16(branchID);
            long customerID = Convert.ToInt64(customerIID);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();
                historyDetails = (from transactionHead in dbContext.TransactionHeads
                                  where
                                  //transactionHead.DocumentTypeID == documentID &&
                                  //transactionHead.CustomerID == customerID && 
                                  transactionHead.HeadIID == orderID
                                  select transactionHead)
                                  .AsNoTracking()
                                  .ToList();
                if (historyDetails.IsNotNull() && historyDetails.Count > 0)
                {
                    foreach (var history in historyDetails)
                    {
                        historyHeader = new HistoryHeader();
                        historyHeader.TransactionNo = history.TransactionNo;
                        historyHeader.TransactionOrderIID = history.HeadIID;
                        historyHeader.DeliveryTypeID = history.DeliveryTypeID.HasValue ? history.DeliveryTypeID.Value : 0;
                        historyHeader.DeliveryDays = history.DeliveryDays.HasValue ? history.DeliveryDays.Value : 0;
                        historyHeader.DocumentStatusID = history.DocumentStatusID;
                        var shoppingcartID = (from cartMaps in dbContext.TransactionHeadShoppingCartMaps where history.HeadIID == cartMaps.TransactionHeadID select cartMaps.ShoppingCartID).FirstOrDefault();
                        if (history.TransactionDetails.IsNotNull() && history.TransactionDetails.Count > 0)
                        {
                            historyHeader.OrderDetails = new List<HistoryDetail>();
                            foreach (TransactionDetail transactionDetail in history.TransactionDetails)
                            {
                                var productDetail = new HistoryDetail();
                                productDetail.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                                productDetail.Quantity = transactionDetail.Quantity;
                                productDetail.UnitPrice = transactionDetail.UnitPrice;
                                decimal UnitPriceAmount = Convert.ToDecimal(transactionDetail.Amount);
                                productDetail.Amount = UnitPriceAmount;

                                historyHeader.OrderDetails.Add(productDetail);
                            }
                        }
                        userHistoryList.Add(historyHeader);
                    }
                }
            }
            return userHistoryList;
        }


        public List<HistoryHeader> GetOrderHistoryDetails(string branchID, string customerIID, int pageNumber, int pageSize, long orderID, int companyID, string lang = "en")
        {
            List<HistoryHeader> userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int branchIDTo = Convert.ToInt16(branchID);
            long customerID = Convert.ToInt64(customerIID);
            var websiteChangeRequestDocumentType = new SettingRepository().GetSettingDetail(Constants.OrderCancelStatusSettings.WEBSITEORDERCHANGEREQUEST, companyID);
            var websitedocumentType = websiteChangeRequestDocumentType.IsNotNull() ? !string.IsNullOrEmpty(websiteChangeRequestDocumentType.SettingValue) ? int.Parse(websiteChangeRequestDocumentType.SettingValue) : 0 : 0;
            var jobNoActionStatus = (int)Framework.Helper.Enums.ReplacementActions.NoAction;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();

                if (orderID == default(long))
                {
                    historyDetails = (from transactionHead in dbContext.TransactionHeads
                                      join docType in dbContext.DocumentTypes on transactionHead.DocumentTypeID equals docType.DocumentTypeID
                                      join branch in dbContext.Branches on transactionHead.BranchID equals branch.BranchIID
                                      where (transactionHead.BranchID == branchIDTo || branch.IsMarketPlace == true)
                                      && transactionHead.CustomerID == customerID && transactionHead.CompanyID == companyID
                                      && (docType.ReferenceTypeID == 1 || docType.ReferenceTypeID == 3)

                                      select transactionHead).OrderByDescending(x => x.HeadIID).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                }
                else
                {
                    historyDetails = (from transactionHead in dbContext.TransactionHeads
                                      where
                                      //transactionHead.DocumentTypeID == documentID &&
                                      //transactionHead.CustomerID == customerID && 
                                      transactionHead.HeadIID == orderID
                                      select transactionHead)
                                      .Include(i => i.TransactionDetails)
                                      //.Include(i => i.TransactionDetails)
                                      .AsNoTracking().ToList();
                }


                if (historyDetails.IsNotNull() && historyDetails.Count > 0)
                {
                    foreach (var history in historyDetails)
                    {
                        historyHeader = new HistoryHeader();
                        var shoppingcartID = (from cartMaps in dbContext.TransactionHeadShoppingCartMaps where history.HeadIID == cartMaps.TransactionHeadID select cartMaps.ShoppingCartID).FirstOrDefault();
                        var voucherOrder = (from voucher in dbContext.ShoppingCartVoucherMaps
                                            where voucher.ShoppingCartID == shoppingcartID
                                            select voucher).AsNoTracking().FirstOrDefault();
                        historyHeader.TransactionOrderIID = history.HeadIID;
                        historyHeader.DocumentTypeID = history.DocumentTypeID;
                        historyHeader.CustomerID = history.CustomerID;
                        historyHeader.Description = history.Description;
                        historyHeader.SupplierID = history.SupplierID;
                        historyHeader.TransactionDate = history.TransactionDate;
                        historyHeader.TransactionNo = history.TransactionNo;
                        historyHeader.TransactionStatus = (int)history.TransactionStatusID;
                        historyHeader.ActualOrderStatus = GetTransactionStatus(history.HeadIID);
                        historyHeader.DeliveryTypeID = history.DeliveryTypeID.HasValue ? history.DeliveryTypeID.Value : 0;
                        historyHeader.DeliveryDays = history.DeliveryDays.HasValue ? history.DeliveryDays.Value : 0;
                        historyHeader.DocumentStatusID = history.DocumentStatusID.HasValue ? history.DocumentStatusID.Value : 0;
                        historyHeader.CompanyID = history.CompanyID.HasValue ? history.CompanyID.Value : (int?)null; // added to get companyid in orderhistory
                        historyHeader.PaymentMethod = shoppingcartID.IsNotNull() ? (from shoppingCart in dbContext.ShoppingCarts
                                                                                    where shoppingCart.ShoppingCartIID == shoppingcartID
                                                                                    select shoppingCart.PaymentMethod).AsNoTracking().FirstOrDefault().ToString() : null;

                        historyHeader.StudentID = history.StudentID;
                        historyHeader.SchoolID = history.SchoolID;
                        historyHeader.AcademicYearID = history.AcademicYearID;

                        try
                        {
                            var entitlementIDList = dbContext.TransactionHeadEntitlementMaps.Where(a => a.TransactionHeadID == history.HeadIID);
                            if (entitlementIDList.IsNotNull())
                            {
                                foreach (var entitlement in entitlementIDList)
                                {
                                    //System.Type type = typeof(Eduegate.Framework.Payment.EntitlementType);
                                    var paymentMethod = "";
                                    if (((Eduegate.Framework.Payment.EntitlementType)entitlement.EntitlementID == Framework.Payment.EntitlementType.Knet))
                                    {
                                        paymentMethod = "knet";
                                    }
                                    else if (((Eduegate.Framework.Payment.EntitlementType)entitlement.EntitlementID == Framework.Payment.EntitlementType.Visa_Mastercard))
                                    {
                                        paymentMethod = "cc";
                                    }
                                    else if (((Eduegate.Framework.Payment.EntitlementType)entitlement.EntitlementID == Framework.Payment.EntitlementType.Paypal))
                                    {
                                        paymentMethod = "paypal";
                                    }
                                    else if (((Eduegate.Framework.Payment.EntitlementType)entitlement.EntitlementID == Framework.Payment.EntitlementType.COD))
                                    {
                                        paymentMethod = "cod";
                                    }
                                    else if (((Eduegate.Framework.Payment.EntitlementType)entitlement.EntitlementID == Framework.Payment.EntitlementType.Voucher))
                                    {
                                        paymentMethod = "voucher";
                                    }
                                    else if (((Eduegate.Framework.Payment.EntitlementType)entitlement.EntitlementID == Framework.Payment.EntitlementType.Wallet))
                                    {
                                        paymentMethod = "wallet";
                                    }

                                    if (historyHeader.PaymentMethod.IsNullOrEmpty())
                                        historyHeader.PaymentMethod = paymentMethod;
                                    else
                                        historyHeader.PaymentMethod = historyHeader.PaymentMethod + "," + paymentMethod;
                                }
                            }
                        }
                        catch (Exception) { historyHeader.PaymentMethod = ""; }
                        historyHeader.DiscountAmount = history.DiscountAmount;
                        historyHeader.DiscountPercentage = history.DiscountPercentage;
                        historyHeader.ParentTransactionOrderIID = history.ReferenceHeadID != null ? history.ReferenceHeadID : null;

                        if (voucherOrder != null)
                        {

                            //decimal voucherAmount = Convert.ToDecimal(voucherOrder.Amount);
                            //historyHeader.VoucherAmount = voucherAmount;
                            //historyHeader.VoucherNo = voucherOrder.Voucher.VoucherNo;
                            var vouchers = dbContext.TransactionHeadEntitlementMaps.Where(a => a.EntitlementID == (short)Eduegate.Framework.Payment.EntitlementType.Voucher && a.TransactionHeadID == history.HeadIID).AsNoTracking().FirstOrDefault();
                            if (vouchers.IsNotNull())
                            {
                                dbContext.Entry(voucherOrder).Reference(a => a.Voucher).Load();
                                historyHeader.VoucherAmount = vouchers.Amount;
                                historyHeader.VoucherNo = voucherOrder.Voucher.VoucherNo;
                            }
                        }

                        historyHeader.DeliveryAddress = (from a in dbContext.TransactionHeads
                                                         join b in dbContext.OrderContactMaps on a.HeadIID equals b.OrderID
                                                         where a.HeadIID == history.HeadIID && b.IsShippingAddress == true
                                                         select b).AsNoTracking().FirstOrDefault();
                        if (historyHeader.DeliveryAddress.IsNotNull())
                        {
                            var countryName = (from a in dbContext.Countries where a.CountryID == historyHeader.DeliveryAddress.CountryID select a.CountryName).AsNoTracking().FirstOrDefault();
                            var areaName = (from a in dbContext.Areas where a.AreaID == historyHeader.DeliveryAddress.AreaID select a.AreaName).AsNoTracking().FirstOrDefault();
                            var cityName = (from a in dbContext.Cities where a.CityID == historyHeader.DeliveryAddress.CityID select a.CityName).AsNoTracking().FirstOrDefault();
                            historyHeader.DeliveryCountryName = countryName;
                            historyHeader.DeliveryAreaName = areaName;
                            historyHeader.DeliveryCityName = cityName;
                        }

                        historyHeader.BillingAddress = (from a in dbContext.TransactionHeads
                                                        join b in dbContext.OrderContactMaps on a.HeadIID equals b.OrderID
                                                        where a.HeadIID == history.HeadIID && b.IsBillingAddress == true
                                                        select b).AsNoTracking().FirstOrDefault();

                        if (historyHeader.BillingAddress.IsNotNull())
                        {
                            var countryName = (from a in dbContext.Countries where a.CountryID == historyHeader.BillingAddress.CountryID select a.CountryName).AsNoTracking().FirstOrDefault();
                            var areaName = (from a in dbContext.Areas where a.AreaID == historyHeader.BillingAddress.AreaID select a.AreaName).AsNoTracking().FirstOrDefault();
                            var cityName = (from a in dbContext.Cities where a.CityID == historyHeader.BillingAddress.CityID select a.CityName).AsNoTracking().FirstOrDefault();
                            historyHeader.BillingCountryName = countryName;
                            historyHeader.BillingAreaName = areaName;
                            historyHeader.BillingCityName = cityName;
                        }

                        historyHeader.LoyaltyPoints = (from a in dbContext.TransactionHeadPointsMaps
                                                       join b in dbContext.TransactionHeads on a.TransactionHeadID equals b.HeadIID
                                                       where b.HeadIID == history.HeadIID
                                                       select a.LoyaltyPoints).FirstOrDefault();

                        if (history.TransactionDetails.IsNotNull() && history.TransactionDetails.Count > 0)
                        {
                            historyHeader.OrderDetails = new List<HistoryDetail>();
                            long? previousSKU = 0;
                            long? currentSKU = 0;
                            int index = 0;
                            var referenceHead = dbContext.TransactionHeads.Where(a => a.ReferenceHeadID == history.HeadIID).FirstOrDefault();
                            var referenceDetails = new List<TransactionDetail>();
                            if (referenceHead.IsNotNull())
                            {
                                referenceDetails = dbContext.TransactionDetails.Where(a => a.HeadID == referenceHead.HeadIID).ToList();
                            }
                            foreach (TransactionDetail transactionDetail in history.TransactionDetails)
                            {
                                var serialKey = transactionDetail.SerialNumber;
                                previousSKU = currentSKU;
                                currentSKU = transactionDetail.ProductSKUMapID;

                                if (referenceDetails.IsNotNull())
                                {
                                    if (referenceDetails.Count > 0)
                                    {
                                        if (previousSKU != currentSKU)
                                        {
                                            index = 0;
                                            serialKey = referenceDetails.Where(a => a.ProductSKUMapID == transactionDetail.ProductSKUMapID).Select(a => a.SerialNumber).FirstOrDefault();
                                        }
                                        else
                                        {
                                            index++;
                                            serialKey = referenceDetails.Where(a => a.ProductSKUMapID == transactionDetail.ProductSKUMapID).Skip(index).Take(1).Select(a => a.SerialNumber).FirstOrDefault();

                                        }
                                    }

                                }
                                // Please do it on Business layer as per requirement
                                //if (!string.IsNullOrEmpty(serialKey))
                                //{
                                //    var length = serialKey.Length;
                                //    var newSerialKey = "";
                                //    if (serialKey.Length <= 4)
                                //    {
                                //        newSerialKey = new String('x', length);
                                //    }
                                //    else
                                //    {
                                //        newSerialKey = new String('x', length - 4) + serialKey.Substring(length - 4);
                                //    }
                                //    serialKey = newSerialKey;
                                //}
                                var productDetail = new HistoryDetail();

                                var cancelqty = (from detail in dbContext.TransactionDetails
                                                 join head in dbContext.TransactionHeads on detail.HeadID equals head.HeadIID
                                                 where head.DocumentTypeID == websitedocumentType
                                                 && head.ReferenceHeadID == history.HeadIID
                                                 && detail.Action != jobNoActionStatus
                                                 && detail.ProductSKUMapID == transactionDetail.ProductSKUMapID
                                                 select detail.Quantity).Sum() ?? 0;

                                productDetail.TransactionIID = transactionDetail.DetailIID;
                                productDetail.ProductID = transactionDetail.ProductID;
                                productDetail.SerialNumber = serialKey;
                                //dbContext.ProductSKUCultureDatas.Where(a=>a.ProductSKUMapID)
                                var skuDetails = dbContext.ProductSKUMaps.Where(a => a.ProductSKUMapIID == transactionDetail.ProductSKUMapID).FirstOrDefault();
                                productDetail.PartNumber = skuDetails.IsNotNull() ? skuDetails.PartNo : string.Empty;
                                if (lang == "ar")
                                {
                                    var prodtAr = dbContext.ProductSKUCultureDatas.Where(a => a.ProductSKUMapID == transactionDetail.ProductSKUMapID && a.CultureID == 2).AsNoTracking().Select(a => a.ProductSKUName).FirstOrDefault();
                                    productDetail.ProductName = prodtAr;
                                }
                                else
                                {
                                    productDetail.ProductName = dbContext.Products.Where(x => x.ProductIID == transactionDetail.ProductID).AsNoTracking().FirstOrDefault().ProductName;
                                }

                                productDetail.ProductImageUrl = (dbContext.ProductImageMaps.Where(p => p.ProductSKUMapID == transactionDetail.ProductSKUMapID && p.ProductImageTypeID == (long)Framework.Helper.Enums.ImageType.ThumbnailImage).AsNoTracking().FirstOrDefault() != null) ? (dbContext.ProductImageMaps.Where(p => p.ProductSKUMapID == transactionDetail.ProductSKUMapID && p.ProductImageTypeID == (long)Framework.Helper.Enums.ImageType.ThumbnailImage).AsNoTracking().FirstOrDefault().ImageFile) : null; ;
                                productDetail.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                                productDetail.Quantity = transactionDetail.Quantity;
                                productDetail.UnitID = transactionDetail.UnitID;
                                productDetail.DiscountPercentage = transactionDetail.DiscountPercentage;
                                productDetail.UnitPrice = transactionDetail.UnitPrice;
                                decimal UnitPriceAmount = Convert.ToDecimal(transactionDetail.Amount);
                                productDetail.Amount = UnitPriceAmount;
                                productDetail.ExchangeRate = transactionDetail.ExchangeRate;
                                productDetail.DetailIID = transactionDetail.DetailIID;
                                productDetail.CancelQuantity = cancelqty;
                                productDetail.ActualQuantity = transactionDetail.Quantity - cancelqty;
                                historyHeader.OrderDetails.Add(productDetail);
                            }
                        }
                        try
                        {
                            historyHeader.DeliveryCharge = (decimal)history.DeliveryCharge;
                        }
                        catch (Exception)
                        { historyHeader.DeliveryCharge = 0; }
                        userHistoryList.Add(historyHeader);
                    }
                }
            }

            return userHistoryList;
        }

        public List<HistoryHeader> GetOrderHistoryDetailsWithPagination(string branchID, string customerIID, int pageNumber, int pageSize)
        {
            List<HistoryHeader> userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int branchIDTo = Convert.ToInt16(branchID);
            long customerID = Convert.ToInt64(customerIID);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();
                historyDetails = (from transactionHead in dbContext.TransactionHeads
                                  join docType in dbContext.DocumentTypes on transactionHead.DocumentTypeID equals docType.DocumentTypeID
                                  join branch in dbContext.Branches on transactionHead.BranchID equals branch.BranchIID
                                  where (transactionHead.BranchID == branchIDTo || branch.IsMarketPlace == true)
                                   && transactionHead.CustomerID == customerID
                                   && (docType.ReferenceTypeID == 1 || docType.ReferenceTypeID == 3)
                                  select transactionHead).OrderByDescending(x => x.HeadIID).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                if (historyDetails.IsNotNull() && historyDetails.Count > 0)
                {
                    foreach (var history in historyDetails)
                    {
                        historyHeader = new HistoryHeader();
                        var shoppingcartID = (from cartMaps in dbContext.TransactionHeadShoppingCartMaps where history.HeadIID == cartMaps.TransactionHeadID select cartMaps.ShoppingCartID).FirstOrDefault();
                        var voucherOrder = (from voucher in dbContext.ShoppingCartVoucherMaps
                                            where voucher.ShoppingCartID == shoppingcartID
                                            select voucher)
                                            .Include(i => i.Voucher)
                                            .AsNoTracking().FirstOrDefault();

                        historyHeader.DeliveryTypeID = history.DeliveryTypeID.HasValue ? history.DeliveryTypeID.Value : 0;
                        historyHeader.TransactionOrderIID = history.HeadIID;
                        historyHeader.DocumentTypeID = history.DocumentTypeID;
                        historyHeader.CustomerID = history.CustomerID;
                        historyHeader.Description = history.Description;
                        historyHeader.SupplierID = history.SupplierID;
                        historyHeader.TransactionDate = history.TransactionDate;
                        historyHeader.TransactionNo = history.TransactionNo;
                        historyHeader.TransactionStatus = (int)history.TransactionStatusID;
                        historyHeader.ActualOrderStatus = GetTransactionStatus(history.HeadIID);
                        //historyHeader.PaymentMethod = shoppingcartID.IsNotNull() ? (from shoppingCart in dbContext.ShoppingCarts
                        //                                                            where shoppingCart.ShoppingCartIID == shoppingcartID
                        //                                                            select shoppingCart.PaymentMethod).FirstOrDefault().ToString() : null;
                        try
                        {
                            System.Type type = typeof(Eduegate.Framework.Payment.EntitlementType);
                            historyHeader.PaymentMethod = Enum.GetName(type, history.EntitlementID).ToString();
                        }
                        catch (Exception) { historyHeader.PaymentMethod = ""; }
                        historyHeader.DiscountAmount = history.DiscountAmount;
                        historyHeader.DiscountPercentage = history.DiscountPercentage;
                        historyHeader.ParentTransactionOrderIID = history.ReferenceHeadID != null ? history.ReferenceHeadID : null;

                        historyHeader.LoyaltyPoints = (from a in dbContext.TransactionHeadPointsMaps
                                                       join b in dbContext.TransactionHeads on a.TransactionHeadID equals b.HeadIID
                                                       where b.HeadIID == history.HeadIID
                                                       select a.LoyaltyPoints).FirstOrDefault();

                        if (voucherOrder != null)
                        {

                            //decimal voucherAmount = Convert.ToDecimal(voucherOrder.Amount);
                            //historyHeader.VoucherAmount = voucherAmount;
                            //historyHeader.VoucherNo = voucherOrder.Voucher.VoucherNo;
                            var vouchers = dbContext.TransactionHeadEntitlementMaps
                                .Where(a => a.EntitlementID == (short)Eduegate.Framework.Payment.EntitlementType.Voucher && a.TransactionHeadID == history.HeadIID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (vouchers.IsNotNull())
                            {
                                dbContext.Entry(voucherOrder).Reference(a => a.Voucher).Load();
                                historyHeader.VoucherAmount = vouchers.Amount;
                                historyHeader.VoucherNo = voucherOrder.Voucher.VoucherNo;
                            }
                        }
                        try
                        {
                            historyHeader.DeliveryCharge = (decimal)history.DeliveryCharge;
                        }
                        catch (Exception ex) { historyHeader.DeliveryCharge = 0; }
                        userHistoryList.Add(historyHeader);
                    }

                }
            }

            return userHistoryList;
        }

        public int GetTransactionStatus(long headID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var deliveryType = new OrderRepository().GetDeliveryType(headID);
                if (deliveryType == DeliveryTypes.Email)
                {
                    var referenceHead = dbContext.TransactionHeads.Where(a => a.ReferenceHeadID == headID)
                        .AsNoTracking()
                        .FirstOrDefault();
                    if (referenceHead.IsNotNull())
                    {
                        switch (referenceHead.TransactionStatusID)
                        {
                            case (int)Services.Contracts.Enums.TransactionStatus.Delivered:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Delivered;
                            case (int)Services.Contracts.Enums.TransactionStatus.Failed:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Failed;
                            case (int)Services.Contracts.Enums.TransactionStatus.Complete:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Delivered;
                            default:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Picking;//Other status apart from success & failed.
                        }
                    }
                    else
                        return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Picking;//Other status apart from success & failed.
                }
                else
                {
                    var head = dbContext.JobEntryHeads.Where(a => a.TransactionHeadID == headID)
                        .AsNoTracking()
                        .FirstOrDefault();
                    if (head.IsNotNull())
                    {
                        if (head.JobStatusID == (int)Eduegate.Services.Contracts.Enums.JobStatuses.FailedReceived || head.JobStatusID == (int)Eduegate.Services.Contracts.Enums.JobStatuses.FailedReceiving)
                            return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Failed;
                        else
                            return (int)head.JobStatusID;
                    }
                    else
                        return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Picking;//Other status apart from success & failed.
                }
            }
        }

        /// <summary>
        /// To Get the Order Details
        /// </summary>
        /// <param name="documentTypeID"></param>
        /// <param name="customerIID"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<HistoryHeader> GetOrderHistoryItemDetails(string documentTypeID, string customerIID, int pageNumber, int pageSize, long orderID, int companyID)
        {
            List<HistoryHeader> userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int documentID = Convert.ToInt16(documentTypeID);
            long customerID = Convert.ToInt64(customerIID);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();

                historyDetails = (from transactionHead in dbContext.TransactionHeads
                                  where
                                  //transactionHead.DocumentTypeID == documentID &&
                                  //transactionHead.CustomerID == customerID && 
                                  transactionHead.HeadIID == orderID
                                  select transactionHead)
                                  .AsNoTracking()
                                  .ToList();

                if (historyDetails.IsNotNull() && historyDetails.Count > 0)
                {
                    foreach (var history in historyDetails)
                    {
                        historyHeader = new HistoryHeader();

                        historyHeader.TransactionOrderIID = history.HeadIID;
                        historyHeader.DeliveryTypeID = history.DeliveryTypeID.HasValue ? history.DeliveryTypeID.Value : 0;
                        historyHeader.DeliveryDays = history.DeliveryDays.HasValue ? history.DeliveryDays.Value : 0;

                        if (history.TransactionDetails.IsNotNull() && history.TransactionDetails.Count > 0)
                        {
                            historyHeader.OrderDetails = new List<HistoryDetail>();

                            foreach (TransactionDetail transactionDetail in history.TransactionDetails)
                            {

                                var productDetail = new HistoryDetail();

                                productDetail.TransactionIID = transactionDetail.DetailIID;
                                productDetail.ProductID = transactionDetail.ProductID;

                                var skuDetails = dbContext.ProductSKUMaps.Where(a => a.ProductSKUMapIID == transactionDetail.ProductSKUMapID)
                                    .AsNoTracking()
                                    .FirstOrDefault();
                                var product = dbContext.Products.Where(x => x.ProductIID == transactionDetail.ProductID)
                                    .AsNoTracking()
                                    .FirstOrDefault();
                                productDetail.ProductName = skuDetails.IsNotNull() ? skuDetails.SKUName : product.IsNotNull() ? product.ProductName : null;

                                var productImageMaps = dbContext.ProductImageMaps.Where(p => p.ProductSKUMapID == transactionDetail.ProductSKUMapID)
                                    .AsNoTracking()
                                    .FirstOrDefault();
                                productDetail.ProductImageUrl = productImageMaps.IsNotNull() ? productImageMaps.ImageFile : null;

                                productDetail.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                                productDetail.Quantity = transactionDetail.Quantity;
                                productDetail.UnitID = transactionDetail.UnitID;
                                productDetail.DiscountPercentage = transactionDetail.DiscountPercentage;
                                productDetail.UnitPrice = transactionDetail.UnitPrice;
                                decimal UnitPriceAmount = Convert.ToDecimal(transactionDetail.Amount);
                                productDetail.Amount = UnitPriceAmount;
                                productDetail.ExchangeRate = transactionDetail.ExchangeRate;
                                productDetail.SerialNumber = transactionDetail.SerialNumber != null ? transactionDetail.SerialNumber : "";
                                productDetail.PartNumber = skuDetails.IsNotNull() ? skuDetails.PartNo : string.Empty;
                                historyHeader.OrderDetails.Add(productDetail);
                            }
                        }

                        userHistoryList.Add(historyHeader);
                    }
                }
            }

            return userHistoryList;
        }

        public int GetOrderHistoryCount(string documentTypeID, long customerID)
        {
            int documentID = Convert.ToInt16(documentTypeID);
            var count = 0;
            var branchIDTo = long.Parse(documentTypeID);
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                count = (from transactionHead in dbContext.TransactionHeads
                         join docType in dbContext.DocumentTypes on transactionHead.DocumentTypeID equals docType.DocumentTypeID
                         join branch in dbContext.Branches on transactionHead.BranchID equals branch.BranchIID
                         where (transactionHead.BranchID == branchIDTo || branch.IsMarketPlace == true)
                                   && transactionHead.CustomerID == customerID
                                   && (docType.ReferenceTypeID == 1 || docType.ReferenceTypeID == 3)
                         select transactionHead).Count();
            }
            return count;

        }

        public List<SearchResult> GetWishListDetails(string loginEmailID)
        {
            long? thumbnailImageID = Convert.ToInt32(Eduegate.Framework.Helper.Enums.ImageType.ThumbnailImage);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var products = (from login in dbContext.Logins
                                join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                                join wl in dbContext.WishLists on cust.CustomerIID equals wl.CustomerID
                                join psm in dbContext.ProductSKUMaps on wl.SKUID equals psm.ProductSKUMapIID
                                join p in dbContext.Products on psm.ProductID equals p.ProductIID
                                join pim in dbContext.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into one
                                from pim in one.DefaultIfEmpty() // if we don't have a record for this ProductId in ProductImageMaps table
                                join b in dbContext.Brands on p.BrandID equals b.BrandIID
                                join pplsm in dbContext.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into x
                                from xy in x.DefaultIfEmpty()
                                join ppl in dbContext.ProductPriceLists on xy.ProductPriceListID equals ppl.ProductPriceListIID into y
                                from ppl in y.DefaultIfEmpty()
                                join pis in dbContext.ProductInventories on psm.ProductSKUMapIID equals pis.ProductSKUMapID into z
                                from pis in z.DefaultIfEmpty()
                                where login.LoginEmailID == loginEmailID
                                && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == thumbnailImageID)
                                && (pim.Sequence == null || pim.Sequence == 1)
                                select new SearchResult
                                {
                                    ProductIID = p.ProductIID,
                                    SKUID = psm.ProductSKUMapIID,
                                    ProductName = p.ProductName,
                                    ImageFile = pim.ImageFile,
                                    BrandName = b.BrandName,
                                    Amount = psm.ProductPrice,
                                    SellingQuantityLimit = xy.SellingQuantityLimit,
                                    Quantity = pis.Quantity,
                                    DiscountedPrice = (ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? ppl.Price :
                                                       (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100 :
                                                       psm.ProductPrice,
                                });

                return products
                    .AsNoTracking()
                    .ToList();
            }
        }

        public bool AddWishListItem(string loginEmailID, long skuID)
        {
            long customerID = UtilityRepository.GetCustomerID(loginEmailID);

            if (customerID == 0)
                return false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (dbContext.WishLists.Where(x => x.CustomerID == customerID && x.SKUID == skuID).Any())
                    return false; //Based on true/false we are showing the message whether whishlist is added or not existing item stopping to show returning false

                dbContext.WishLists.Add(new WishList()
                {
                    CreatedDate = DateTime.Now,
                    CustomerID = customerID,
                    SKUID = skuID,
                });

                dbContext.SaveChanges();
            }

            return true;
        }

        public bool RemoveWishListItem(string loginEmailID, long skuID)
        {
            bool result = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var wishListItem = (from login in dbContext.Logins
                                        join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                                        join wl in dbContext.WishLists on cust.CustomerIID equals wl.CustomerID
                                        where login.LoginEmailID == loginEmailID && wl.SKUID == skuID
                                        select wl
                                        )
                                        .AsNoTracking()
                                        .FirstOrDefault();
                    if (wishListItem.IsNotNull())
                    {
                        dbContext.WishLists.Remove(wishListItem);
                        dbContext.SaveChanges();
                        result = true;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool AddSaveForLater(string loginEmailID, long skuID)
        {
            long customerID = UtilityRepository.GetCustomerID(loginEmailID);
            if (customerID == 0)
                return false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (dbContext.WishLists.Where(x => x.CustomerID == customerID && x.SKUID == skuID && x.IsWishList == true)
                    .AsNoTracking()
                    .Any())
                    return false; //Based on true/false we are showing the message whether whishlist is added or not existing item stopping to show returning false

                dbContext.WishLists.Add(new WishList()
                {
                    CreatedDate = DateTime.Now,
                    CustomerID = customerID,
                    SKUID = skuID,
                    IsWishList = true,
                });

                dbContext.SaveChanges();
            }

            return true;
        }

        public List<SearchResult> GetSaveForLater(string loginEmailID)
        {
            long? thumbnailImageID = Convert.ToInt32(Eduegate.Framework.Helper.Enums.ImageType.ThumbnailImage);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var products = (from login in dbContext.Logins
                                join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                                join wl in dbContext.WishLists on cust.CustomerIID equals wl.CustomerID
                                join psm in dbContext.ProductSKUMaps on wl.SKUID equals psm.ProductSKUMapIID
                                join p in dbContext.Products on psm.ProductID equals p.ProductIID
                                join pim in dbContext.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into one
                                from pim in one.DefaultIfEmpty() // if we don't have a record for this ProductId in ProductImageMaps table
                                join b in dbContext.Brands on p.BrandID equals b.BrandIID into aa
                                from b in aa.DefaultIfEmpty()
                                //join pplsm in dbContext.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into x
                                //from xy in x.DefaultIfEmpty()
                                where wl.IsWishList == true && login.LoginEmailID == loginEmailID
                                && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == thumbnailImageID)
                                && (pim.Sequence == null || pim.Sequence == 1)
                                select new SearchResult
                                {
                                    ProductIID = p.ProductIID,
                                    SKUID = psm.ProductSKUMapIID,
                                    ProductName = p.ProductName,
                                    ImageFile = pim.ImageFile,
                                    BrandName = b.BrandName,
                                    Amount = psm.ProductPrice,
                                    SellingQuantityLimit = 0,
                                    Quantity = 0,
                                    DiscountedPrice = 0,
                                });

                return products
                    .AsNoTracking()
                    .ToList();
            }
        }

        public long GetSaveForLaterCount(string loginEmailID)
        {
            long? thumbnailImageID = Convert.ToInt32(Eduegate.Framework.Helper.Enums.ImageType.ThumbnailImage);
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var products = (from login in dbContext.Logins
                                join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                                join wl in dbContext.WishLists on cust.CustomerIID equals wl.CustomerID
                                join psm in dbContext.ProductSKUMaps on wl.SKUID equals psm.ProductSKUMapIID
                                join p in dbContext.Products on psm.ProductID equals p.ProductIID
                                join pim in dbContext.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into one
                                from pim in one.DefaultIfEmpty() // if we don't have a record for this ProductId in ProductImageMaps table
                                join b in dbContext.Brands on p.BrandID equals b.BrandIID
                                join pplsm in dbContext.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into x
                                from xy in x.DefaultIfEmpty()
                                join ppl in dbContext.ProductPriceLists on xy.ProductPriceListID equals ppl.ProductPriceListIID into y
                                from ppl in y.DefaultIfEmpty()
                                join pis in dbContext.ProductInventories on psm.ProductSKUMapIID equals pis.ProductSKUMapID into z
                                from pis in z.DefaultIfEmpty()
                                where wl.IsWishList == true && login.LoginEmailID == loginEmailID
                                && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == thumbnailImageID)
                                && (pim.Sequence == null || pim.Sequence == 1)
                                select p.ProductIID).Count();

                return products;
            }
        }

        public bool RemoveSaveForLater(string loginEmailID, long skuID)
        {
            bool result = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var wishListItem = (from login in dbContext.Logins
                                        join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                                        join wl in dbContext.WishLists on cust.CustomerIID equals wl.CustomerID
                                        where wl.IsWishList == true && login.LoginEmailID == loginEmailID && wl.SKUID == skuID
                                        select wl
                                        )
                                        .AsNoTracking()
                                        .FirstOrDefault();
                    if (wishListItem.IsNotNull())
                    {
                        dbContext.WishLists.Remove(wishListItem);
                        dbContext.SaveChanges();
                        result = true;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool UpdateNewsLetter(Customer customer)
        {
            bool result = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (customer.IsNotNull())
                    {
                        Customer customerEntity = dbContext.Customers.Where(x => x.CustomerIID == customer.CustomerIID)
                            .AsNoTracking()
                            .FirstOrDefault();
                        customerEntity.IsSubscribeForNewsLetter = customer.IsSubscribeForNewsLetter;
                        dbContext.SaveChanges();

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update news letter. subscribe/unsubscribe:" + result, 0);
            }

            return result;
        }

        //public bool UpdateWishList(decimal productIID, ContextDTO contextualInformation)
        //{
        //    bool result = false;

        //    try
        //    {
        //        using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
        //        {
        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update wish list. subscribe/unsubscribe:" + result, 0);
        //    }

        //    return result;
        //}

        public UserInfoDTO GetUserInfo(decimal loggedInUserIID)
        {
            var userInfo = new UserInfoDTO();
            userInfo.FirstName = "Rahman";
            userInfo.LastName = "Khan";
            userInfo.IID = loggedInUserIID;
            userInfo.ProfileImageURL = "Images/profilepic.png";

            return userInfo;

        }

        public Contact GetContactDetail(long contactID)
        {
            using (dbEduegateERPContext dbcontext = new dbEduegateERPContext())
            {
                return dbcontext.Contacts.Where(x => x.ContactIID == contactID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }


        public TransactionHeadShoppingCartMap GetTransactionHeadShoppingCartMap(long transactionHeadID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                TransactionHeadShoppingCartMap obj = db.TransactionHeadShoppingCartMaps.Where(x => x.TransactionHeadID == transactionHeadID)
                    .AsNoTracking()
                    .ToList()
                    .FirstOrDefault();
                return obj;
            }
        }

        public UserDeviceMap GetRegisteredUserDevice(long loginID, string userDevice)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.UserDeviceMaps
                    .Where(x => x.LoginID == loginID && x.DeviceToken == userDevice)
                    .FirstOrDefault();
            }
        }

        public UserDeviceMap RegisterUserDevice(UserDeviceMap map)
        {
            using (var dbcontext = new dbEduegateERPContext())
            {
                dbcontext.UserDeviceMaps.Add(map);

                if (map.UserDeviceMapIID != 0)
                {
                    dbcontext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbcontext.SaveChanges();
                return map;
            }
        }

        public UserDeviceMap GetRegisteredActiveUserDeviceByLoginID(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.UserDeviceMaps
                    .Where(x => x.LoginID == loginID && x.IsActive == true)
                    .OrderByDescending(x => x.UserDeviceMapIID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public UserDeviceMap GetLastRegisteredUserDeviceByLoginID(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.UserDeviceMaps
                    .Where(x => x.LoginID == loginID)
                    .OrderByDescending(y => y.UserDeviceMapIID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<UserDeviceMap> GetRegisteredUserDeviceListByLoginID(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.UserDeviceMaps
                    .Where(x => x.LoginID == loginID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<long> GetAllUsersLoginIds()
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Logins.AsNoTracking().Select(x => x.LoginIID).ToList();
            }
        }

        public List<long> GetAllUsersLoginIdsByDefultBranch(long branchId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Logins
                    .Where(x => x.Customers.Any(y => y.DefaultBranchID == branchId))
                    .AsNoTracking()
                    .Select(x => x.LoginIID)
                    .ToList();
            }
        }

        public List<long> GetUsersLoginIdsByCustomerId(long customerId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Logins
                    .Where(x => x.Customers.Any(y => y.CustomerIID == customerId))
                    .AsNoTracking()
                    .Select(x => x.LoginIID)
                    .ToList();
            }
        }

        public List<long> GetAllParentLoginIds()
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Logins
                    .Where(x => x.Parents.Any(y => y.LoginID.HasValue))
                    .AsNoTracking()
                    .Select(x => x.LoginIID)
                    .ToList();
            }
        }

        public List<long> GetBranchWiseParentLoginIDs(long? branchID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Logins
                    .Where(x => x.Parents.Any(y => y.LoginID.HasValue && y.Students.Any(s => s.SchoolID == branchID)))
                    .AsNoTracking()
                    .Select(x => x.LoginIID)
                    .ToList();
            }
        }

    }
}
