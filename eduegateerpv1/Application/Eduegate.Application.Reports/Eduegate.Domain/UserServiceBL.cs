using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Enums;
using System.Linq;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework.Security;
using Eduegate.Globalization;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Security;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Domain
{
    public class UserServiceBL
    {
        UserServiceRepository userServiceRepository = new UserServiceRepository();
        AccountRepository accountRepository = new AccountRepository();
        ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
        //TransactionRepository transactionRepo = new TransactionRepository();
        ////OrderBL orderBL = new OrderBL();
        //AccountBL accountBL = new AccountBL(null);

        private CallContext _callContext;

        public UserServiceBL(CallContext callContext)
        {
            _callContext = callContext;
        }

        public string UpdateProfileDetails(UserDTO userDTO)
        {
            string message = string.Empty;

            if (userDTO != null)
            {
                var loginDetail = accountRepository.GetUserByLoginEmail(userDTO.LoginEmailID);

                Login login = new Login()
                {
                    LoginIID = userDTO.UserID,
                    LoginUserID = userDTO.LoginUserID,
                    LoginEmailID = userDTO.LoginEmailID,
                    Password = userDTO.Password,
                    PasswordSalt = userDTO.PasswordSalt,
                    StatusID = loginDetail.StatusID,
                    CreatedDate = loginDetail.CreatedDate,
                    UpdatedDate = DateTime.Now,
                    LoginRoleMaps = new List<LoginRoleMap>(),
                    //ClaimSetLoginMaps = Mappers.LoginMapper.Mapper(_callContext).ToClaimSetLoginMapDTO(userDTO),
                };

                foreach (var rol in userDTO.Roles)
                {
                    login.LoginRoleMaps.Add(new LoginRoleMap() { RoleID = rol.RoleID, LoginID = login.LoginIID });
                }

                if (userDTO.Customer.IsNotNull())
                {
                    login.Customers = new List<Customer>()
                    {
                        MapCustomerFromDTOTOEntity(userDTO),
                    };
                }

                if (userDTO.Contacts.IsNotNull() && userDTO.Contacts.Count > 0)
                {
                    login.Contacts = new List<Contact>();

                    foreach (ContactDTO contact in userDTO.Contacts)
                    {
                        login.Contacts.Add(MapContactFromDTOToEntity(contact));
                    }
                }

                message = userServiceRepository.UpdateProfileDetails(login);
            }

            return message;
        }

        public bool UpdatePersonalProfileDetails(UserDTO userDTO)
        {
            string message = string.Empty;
            bool result = false;

            if (userDTO != null)
            {
                var loginDetail = !string.IsNullOrEmpty(userDTO.LoginID) ? accountRepository.GetUserDetailsByID(long.Parse(userDTO.LoginID)) : accountRepository.GetUserByLoginEmail(userDTO.LoginEmailID);

                Login login = new Login()
                {
                    LoginIID = userDTO.UserID,
                    LoginUserID = userDTO.LoginUserID,
                    LoginEmailID = userDTO.LoginEmailID,
                    Password = userDTO.Password,
                    PasswordSalt = userDTO.PasswordSalt,
                    StatusID = loginDetail.StatusID,
                    CreatedDate = loginDetail.CreatedDate,
                    UpdatedDate = DateTime.Now,
                    LoginRoleMaps = new List<LoginRoleMap>(),
                };

                if (userDTO.Roles != null)
                {
                    foreach (var rol in userDTO.Roles)
                    {
                        login.LoginRoleMaps.Add(new LoginRoleMap() { RoleID = rol.RoleID, LoginID = login.LoginIID });
                    }
                }


                if (userDTO.Customer.IsNotNull())
                {
                    login.Customers = new List<Customer>()
                    {
                        MapCustomerFromDTOTOEntity(userDTO),
                    };

                    var customer = login.Customers.First();

                    if (userDTO.Branch != null && userDTO.Branch.BranchIID != 0)
                    {
                        customer.DefaultBranchID = userDTO.Branch.BranchIID;
                    }

                    customer.CustomerAddress = userDTO.CustomerAddress;
                    customer.AddressLongitude = userDTO.AddressLongitude;
                    customer.AddressLatitude = userDTO.AddressLatitude;
                    customer.GenderID = userDTO.Customer.GenderID;
                }

                if (userDTO.Contacts.IsNotNull() && userDTO.Contacts.Count > 0)
                {
                    login.Contacts = new List<Contact>();

                    foreach (ContactDTO contact in userDTO.Contacts)
                    {
                        login.Contacts.Add(MapContactFromDTOToEntity(contact));
                    }
                }

                result = userServiceRepository.UpdatePersonalProfileDetails(login);
            }

            return result;
        }

        private Customer MapCustomerFromDTOTOEntity(UserDTO userDTO)
        {
            var customer = new Customer();
            customer.CustomerAddress = userDTO.CustomerAddress;
            customer.AddressLongitude = userDTO.AddressLongitude;
            customer.AddressLatitude = userDTO.AddressLatitude;
            customer.GenderID = userDTO.Customer.GenderID;
            customer.DefaultBranchID = userDTO.Branch != null ? userDTO.Branch.BranchIID : (long?)null;
            customer.CustomerIID = userDTO.Customer.CustomerIID;
            customer.LoginID = userDTO.Customer.LoginID;
            customer.FirstName = userDTO.Customer.FirstName;
            customer.MiddleName = userDTO.Customer.MiddleName;
            customer.LastName = userDTO.Customer.LastName;
            customer.TitleID = userDTO.Customer.TitleID;
            customer.IsDifferentBillingAddress = userDTO.Customer.IsDifferentBillingAddress;
            customer.IsSubscribeForNewsLetter = userDTO.Customer.IsSubscribeOurNewsLetter;
            customer.IsTermsAndConditions = userDTO.Customer.IsTermsAndConditions;
            customer.PassportNumber = userDTO.Customer.PassportNumber;
            customer.PassportIssueCountryID = userDTO.Customer.PassportIssueCountryID;
            customer.CivilIDNumber = userDTO.Customer.CivilIDNumber;
            customer.Telephone = userDTO.Customer.TelephoneNumber;
            return customer;
        }

        private Contact MapContactFromDTOToEntity(ContactDTO contactDTO)
        {
            Contact contact = new Contact();
            contact.ContactIID = contactDTO.ContactID;
            contact.LoginID = contactDTO.LoginID;
            contact.FirstName = contactDTO.FirstName;
            contact.MiddleName = contactDTO.MiddleName;
            contact.LastName = contactDTO.LastName;
            contact.TitleID = contactDTO.TitleID;
            contact.AddressLine1 = contactDTO.AddressLine1;
            contact.AddressLine2 = contactDTO.AddressLine2;
            contact.AddressName = contactDTO.AddressName;
            contact.Block = contactDTO.Block;
            contact.BuildingNo = contactDTO.BuildingNo;
            contact.City = contactDTO.City;
            contact.CivilIDNumber = contactDTO.CivilIDNumber;
            contact.CountryID = contactDTO.CountryID;
            contact.Description = contactDTO.Description;
            contact.Flat = contactDTO.Flat;
            contact.Floor = contactDTO.Floor;
            contact.PassportIssueCountryID = contactDTO.PassportIssueCountryID;
            contact.PassportNumber = contactDTO.PassportNumber;
            contact.PostalCode = contactDTO.PostalCode;
            contact.State = contactDTO.State;
            contact.Street = contactDTO.Street;
            contact.AlternateEmailID1 = contactDTO.AlternateEmailID1;
            contact.AlternateEmailID2 = contactDTO.AlternateEmailID2;
            contact.IsBillingAddress = contactDTO.IsBillingAddress;
            contact.IsShippingAddress = contactDTO.IsShippingAddress;
            contact.TelephoneCode = contactDTO.TelephoneCode;
            contact.PhoneNo1 = contactDTO.TelephoneNumber.IsNotNullOrEmpty() ? contactDTO.TelephoneNumber.ToString() : "";
            return contact;
        }

        public string UpdateAddress(ContactDTO contact)
        {
            Contact contactEntity = MapContactFromDTOToEntity(contact);
            return userServiceRepository.UpdateAddress(contactEntity);
        }

        public bool DeleteAddressBook(decimal addressIID)
        {
            bool result = userServiceRepository.DeleteAddressBook(addressIID);
            return result;
        }

        public List<OrderHistoryDTO> GetBriefOrderHistory(string documentTypeID, long orderID = default(long))
        {
            List<OrderHistoryDTO> HistoryDetails = new List<OrderHistoryDTO>();
            OrderHistoryDTO orderHistory = null;
            var userHistoryList = new List<HistoryHeader>();
            decimal ConversionRate = 1;
            ConversionRate = UtilityRepository.GetCurrencyPrice(this._callContext);
            userHistoryList = userServiceRepository.GetBriefOrderHistoryDetails(documentTypeID, Convert.ToString(this._callContext.UserId), orderID,
                _callContext.IsNotNull() && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int), _callContext != null ? _callContext.LanguageCode : "en");
            if (userHistoryList.IsNotNull() && userHistoryList.Count > 0)
            {
                foreach (HistoryHeader historyHeader in userHistoryList)
                {
                    orderHistory = new OrderHistoryDTO();
                    orderHistory.OrderDetails = new List<OrderDetailDTO>();
                    orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                    orderHistory.TransactionNo = historyHeader.TransactionNo;
                    orderHistory.DocumentStatusID = historyHeader.DocumentStatusID;
                    orderHistory.DeliveryText = new ShoppingCartBL(_callContext).DeliveryTypeText(historyHeader.DeliveryTypeID, historyHeader.DeliveryDays);
                    if (historyHeader.OrderDetails.IsNotNull() && historyHeader.OrderDetails.Count > 0)
                    {
                        foreach (HistoryDetail historyDetail in historyHeader.OrderDetails)
                        {
                            orderHistory.OrderDetails.Add(new OrderDetailDTO()
                            {
                                ProductSKUMapID = historyDetail.ProductSKUMapID,
                                Quantity = historyDetail.Quantity,
                                UnitPrice = historyDetail.UnitPrice * ConversionRate,
                                Amount = historyDetail.Amount * ConversionRate, // price*quantity saved in Amount fiels in detail

                            });
                        }
                    }
                    HistoryDetails.Add(orderHistory);
                }
            }
            return HistoryDetails;
        }

        /* new method */
        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(string documentTypeID, int pageNumber, int pageSize, string customerID = null, CallContext context = null, long orderID = default(long), bool withCurrencyConversion = true, bool showSerialKey = false)
        {
            List<OrderHistoryDTO> HistoryDetails = new List<OrderHistoryDTO>();
            OrderHistoryDTO orderHistory = null;
            var userHistoryList = new List<HistoryHeader>();
            decimal ConversionRate = 1;
            var userDetail = new UserDTO();
            var lang = "";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            if (context.IsNull())
            {
                context = new CallContext();
                context.CurrencyCode = ConfigurationExtensions.GetAppConfigValue("CurrencyCode");
            }

            // get the converted price 
            ConversionRate = UtilityRepository.GetCurrencyPrice(context);

            if (!withCurrencyConversion)
                ConversionRate = 1;

            if (orderID == default(long) && customerID.IsNullOrEmpty())
                userDetail = null;
            var currency = "";
            if (_callContext == null)
            {
                KeyValueDTO dtoKeyValueOwners = new ReferenceDataBL(null).GetDefaultCurrency();
                if (dtoKeyValueOwners != null)
                {
                    currency = dtoKeyValueOwners.Value;
                }
                lang = ConfigurationExtensions.GetAppConfigValue("DefaultLanguageCode");
            }
            else
            {
                currency = _callContext.CurrencyCode;
                lang = _callContext.LanguageCode;
            }
            // Get order detail
            lang = _callContext != null ? _callContext.LanguageCode : "en";
            userHistoryList = userServiceRepository.GetOrderHistoryDetails(documentTypeID, Convert.ToString(customerID), pageNumber, pageSize, orderID,
                _callContext.IsNotNull() && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int), lang);
            int cultureID = 1;
            cultureID = !string.IsNullOrEmpty(lang) && lang == "ar" ? 2 : 1;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                if (userHistoryList.IsNotNull() && userHistoryList.Count > 0)
                {
                    foreach (HistoryHeader historyHeader in userHistoryList)
                    {
                        orderHistory = new OrderHistoryDTO();
                        orderHistory.OrderDetails = new List<OrderDetailDTO>();
                        orderHistory.ReplacementActions = new List<KeyValueDTO>();
                        decimal subTotalAmount = 0;
                        orderHistory.TransactionDate = historyHeader.TransactionDate;
                        orderHistory.TransactionDateString = historyHeader.TransactionDate.HasValue ? historyHeader.TransactionDate.Value.ToString(dateFormat) : null;
                        orderHistory.TransactionNo = historyHeader.TransactionNo;
                        orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                        orderHistory.Description = historyHeader.Description;
                        orderHistory.CustomerID = historyHeader.CustomerID;
                        orderHistory.SupplierID = historyHeader.SupplierID;
                        orderHistory.DocumentTypeID = historyHeader.DocumentTypeID;
                        orderHistory.PaymentMethod = historyHeader.PaymentMethod;
                        orderHistory.VoucherAmount = historyHeader.VoucherAmount != null ? Convert.ToDecimal(historyHeader.VoucherAmount * ConversionRate) : 0;
                        var deliveryTextResult = new TransactionBL(_callContext).GetOrderDeliveryTextByHeadID(orderHistory.TransactionOrderIID).ToList().Where(x => x.CultureID == cultureID).FirstOrDefault();
                        orderHistory.DeliveryDisplayText = deliveryTextResult.IsNotNull() ? deliveryTextResult.DeliveryDisplayText : string.Empty;
                        orderHistory.DocumentStatusID = historyHeader.DocumentStatusID;
                        var DocumentStat = dbContext.DocumentStatuses.FirstOrDefault(a => a.DocumentStatusID == historyHeader.DocumentStatusID);
                        orderHistory.DocumentStatus = DocumentStat.StatusName;


                        orderHistory.StudentID = historyHeader.StudentID;
                        orderHistory.SchoolID = historyHeader.SchoolID;
                        orderHistory.AcademicYearID = historyHeader.AcademicYearID;

                        var StudentDetails = new AccountRepository().GetStudentDetails(Convert.ToInt64(historyHeader.StudentID));
                        var classDet = dbContext.Classes.FirstOrDefault(a => a.ClassID == StudentDetails.ClassID);
                        var secDet = dbContext.Sections.FirstOrDefault(a => a.SectionID == StudentDetails.SectionID);

                        orderHistory.StudentName = StudentDetails.FirstName + StudentDetails.MiddleName + " " + StudentDetails.LastName;
                        orderHistory.ClassName = classDet.ClassDescription;
                        orderHistory.SectionName = secDet.SectionName;


                        if (_callContext.IsNotNull())
                            orderHistory.ReplacementActions = GetReplacementActions(historyHeader.TransactionOrderIID);
                        else
                            orderHistory.ReplacementActions = new List<KeyValueDTO>();
                        if (historyHeader.VoucherNo != null)
                        {
                            var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                            try
                            {
                                historyHeader.VoucherNo = StringCipher.Decrypt(historyHeader.VoucherNo, hash);

                                // added this block here to hide voucher number in payment method, TODO: create a separate method for it
                                var visibleSerialKey = string.Empty;
                                if (!string.IsNullOrEmpty(historyHeader.VoucherNo))
                                {
                                    if (showSerialKey)
                                    {
                                        visibleSerialKey = historyHeader.VoucherNo;
                                    }
                                    else
                                    {
                                        var length = historyHeader.VoucherNo.Length;

                                        if (historyHeader.VoucherNo.Length <= 4)
                                        {
                                            visibleSerialKey = new String('x', length);
                                        }
                                        else
                                        {
                                            visibleSerialKey = new String('x', length - 4) + historyHeader.VoucherNo.Substring(length - 4);
                                        }
                                    }
                                }
                                historyHeader.VoucherNo = visibleSerialKey;

                            }
                            catch (Exception) { }
                        }
                        orderHistory.VoucherNo = historyHeader.VoucherNo != null ? historyHeader.VoucherNo : "";
                        orderHistory.DiscountAmount = historyHeader.DiscountAmount != null ? Convert.ToDecimal(historyHeader.DiscountAmount * ConversionRate) : 0;
                        orderHistory.DiscountPercentage = historyHeader.DiscountPercentage != null ? historyHeader.DiscountPercentage : 0;
                        orderHistory.TransactionStatus = (Services.Contracts.Enums.TransactionStatus)historyHeader.TransactionStatus;
                        try
                        {
                            orderHistory.ActualOrderStatus = (Services.Contracts.Enums.ActualOrderStatus)historyHeader.ActualOrderStatus;
                        }
                        catch (Exception ex) { orderHistory.ActualOrderStatus = ActualOrderStatus.Picking; }
                        orderHistory.DeliveryAddress = OrderContactMapMapper.Mapper(context).ToDTO(historyHeader.DeliveryAddress);
                        orderHistory.BillingAddress = OrderContactMapMapper.Mapper(context).ToDTO(historyHeader.BillingAddress);
                        orderHistory.DeliveryAddress.CountryName = historyHeader.DeliveryCountryName;
                        orderHistory.DeliveryAddress.AreaName = historyHeader.DeliveryAreaName;
                        orderHistory.DeliveryAddress.CityName = historyHeader.DeliveryCityName;

                        orderHistory.BillingAddress.CountryName = historyHeader.BillingCountryName;
                        orderHistory.BillingAddress.AreaName = historyHeader.BillingAreaName;
                        orderHistory.BillingAddress.CityName = historyHeader.BillingCityName;
                        orderHistory.LoyaltyPoints = historyHeader.LoyaltyPoints != null ? historyHeader.LoyaltyPoints : 0;
                        orderHistory.Currency = currency;
                        orderHistory.DeliveryTypeID = historyHeader.DeliveryTypeID;
                        orderHistory.DeliveryText = new ShoppingCartBL(_callContext).DeliveryTypeText(historyHeader.DeliveryTypeID, historyHeader.DeliveryDays);

                        try
                        {
                            orderHistory.StatusTransaction = orderHistory.StatusTransaction = _callContext.IsNotNull() ? ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>.GetDisplayValue((Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus), historyHeader.ActualOrderStatus.ToString())), this._callContext.LanguageCode) : EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>.GetDisplayValue((Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus), historyHeader.ActualOrderStatus.ToString()));
                        }
                        catch (Exception ex) { orderHistory.StatusTransaction = orderHistory.StatusTransaction = _callContext.IsNotNull() ? ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>.GetDisplayValue(ActualOrderStatus.Picking), this._callContext.LanguageCode) : ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>.GetDisplayValue(ActualOrderStatus.Picking)); }


                        // get delivery Charge 
                        //TransactionHeadShoppingCartMap transactionHeadShoppingCartMap = userServiceRepository.GetTransactionHeadShoppingCartMap(historyHeader.TransactionOrderIID);

                        //if (transactionHeadShoppingCartMap.IsNotNull() && transactionHeadShoppingCartMap.ShoppingCartID.IsNotNull())
                        //{
                        //    orderHistory.DeliveryCharge = transactionHeadShoppingCartMap.DeliveryCharge.IsNull() ? 0 : transactionHeadShoppingCartMap.DeliveryCharge * ConversionRate;
                        //}
                        //else
                        //{
                        //    orderHistory.DeliveryCharge = 0;
                        //}
                        orderHistory.DeliveryCharge = historyHeader.DeliveryCharge;

                        if (historyHeader.OrderDetails.IsNotNull() && historyHeader.OrderDetails.Count > 0)
                        {
                            foreach (HistoryDetail historyDetail in historyHeader.OrderDetails)
                            {
                                var visibleSerialKey = string.Empty;
                                if (!string.IsNullOrEmpty(historyDetail.SerialNumber))
                                {
                                    if (showSerialKey)
                                    {
                                        visibleSerialKey = historyDetail.SerialNumber;
                                    }
                                    else
                                    {
                                        var length = historyDetail.SerialNumber.Length;

                                        if (historyDetail.SerialNumber.Length <= 4)
                                        {
                                            visibleSerialKey = new String('x', length);
                                        }
                                        else
                                        {
                                            visibleSerialKey = new String('x', length - 4) + historyDetail.SerialNumber.Substring(length - 4);
                                        }
                                    }
                                }
                                orderHistory.OrderDetails.Add(new OrderDetailDTO()
                                {
                                    TransactionIID = historyDetail.TransactionIID,
                                    ProductID = historyDetail.ProductID,
                                    ProductName = historyDetail.ProductName,
                                    SerialNumber = visibleSerialKey,
                                    PartNumber = historyDetail.PartNumber,
                                    ProductImageUrl = historyDetail.ProductImageUrl,
                                    ProductSKUMapID = historyDetail.ProductSKUMapID,
                                    Quantity = historyDetail.Quantity,
                                    UnitID = historyDetail.UnitID,
                                    DiscountPercentage = historyDetail.DiscountPercentage,
                                    UnitPrice = historyDetail.UnitPrice * ConversionRate,
                                    Amount = historyDetail.Amount * ConversionRate, // price*quantity saved in Amount fiels in detail
                                    ExchangeRate = historyDetail.ExchangeRate,
                                    Properties = shoppingCartRepository.GetPropertiesBySKU(Convert.ToInt64(historyDetail.ProductSKUMapID)),
                                    Categories = shoppingCartRepository.GetCategoriesBySKU(Convert.ToInt64(historyDetail.ProductSKUMapID)),
                                    //http://13.81.15.107:9095/WBImages/Products/103511/LargeImage/1.jpg
                                    ProductUrl = string.Format("{0}/{1}", Convert.ToString(historyDetail.ProductID), Convert.ToString(historyDetail.ProductImageUrl)),
                                    DetailIID = historyDetail.DetailIID,
                                    CancelQuantity = historyDetail.CancelQuantity,
                                    ActualQuantity = historyDetail.ActualQuantity,

                                });



                                subTotalAmount = subTotalAmount + Convert.ToDecimal(historyDetail.Amount * ConversionRate);
                            }
                        }

                        orderHistory.SubTotal = subTotalAmount;
                        var total = orderHistory.SubTotal + (decimal)orderHistory.DeliveryCharge - orderHistory.VoucherAmount;

                        if (Convert.ToDecimal(orderHistory.DiscountAmount) > 0)
                        {
                            total = total - Convert.ToDecimal(orderHistory.DiscountAmount);
                            orderHistory.Discount = Convert.ToDecimal(orderHistory.DiscountAmount);
                        }
                        else if (Convert.ToDecimal(orderHistory.DiscountPercentage) > 0)
                        {
                            orderHistory.Discount = (total * Convert.ToDecimal(orderHistory.DiscountPercentage) / 100);
                            total = total - ((total * Convert.ToDecimal(orderHistory.DiscountPercentage) / 100));
                        }

                        orderHistory.Total = total;
                        if (historyHeader.ParentTransactionOrderIID.IsNotNull() && historyHeader.ParentTransactionOrderIID > 0)
                        {
                            var parentTransaction = new TransactionBL(_callContext).GetTransactionDetail(Convert.ToInt64(historyHeader.ParentTransactionOrderIID));
                            orderHistory.ParentTransactionNo = parentTransaction.TransactionNo;
                            orderHistory.ParentTransactionOrderIID = parentTransaction.HeadIID;
                        }


                        if (orderID != default(long) && orderHistory.CustomerID.IsNotNull())
                        {
                            // Get user details for this order(having user info + billing + shipping info)
                            userDetail = new AccountBL(null).GetUserDetailsByCustomerID(Convert.ToInt64(orderHistory.CustomerID), false);

                            if ((int)Framework.Enums.DocumentReferenceTypes.SalesInvoice == new MetadataBL(_callContext).GetDocumentReferenceTypesByDocumentType(Convert.ToInt32(historyHeader.DocumentTypeID)).ReferenceTypeID)
                            {
                                userDetail.Contacts = new OrderBL(_callContext).GetOrderContacts(Convert.ToInt64(orderHistory.ParentTransactionOrderIID));
                            }
                            else
                            {
                                userDetail.Contacts = new OrderBL(_callContext).GetOrderContacts(orderID);
                            }
                        }

                        orderHistory.UserDetail = userDetail;
                        orderHistory.CompanyID = historyHeader.CompanyID;

                        HistoryDetails.Add(orderHistory);
                    }
                }

            return HistoryDetails;

        }
        /* new method */


        public List<OrderHistoryDTO> GetOrderHistoryItemDetails(string documentTypeID, int pageNumber, int pageSize, string customerID = null, CallContext context = null, long orderID = default(long), bool withCurrencyConversion = true)
        {

            List<OrderHistoryDTO> HistoryDetails = new List<OrderHistoryDTO>();
            OrderHistoryDTO orderHistory = null;
            var userHistoryList = new List<HistoryHeader>();
            decimal ConversionRate = 1;
            var userDetail = new UserDTO();

            if (context.IsNull())
            {
                context = new CallContext();
                context.CurrencyCode = ConfigurationExtensions.GetAppConfigValue("CurrencyCode");
            }

            // get the converted price 
            ConversionRate = UtilityRepository.GetCurrencyPrice(context);
            if (!withCurrencyConversion)
                ConversionRate = 1;

            if (orderID == default(long) && customerID.IsNullOrEmpty())
                userDetail = null;

            // Get order detail
            userHistoryList = userServiceRepository.GetOrderHistoryItemDetails(documentTypeID, Convert.ToString(customerID), pageNumber, pageSize, orderID, this._callContext.CompanyID.Value);


            if (userHistoryList.IsNotNull() && userHistoryList.Count > 0)
            {
                foreach (HistoryHeader historyHeader in userHistoryList)
                {
                    orderHistory = new OrderHistoryDTO();
                    orderHistory.OrderDetails = new List<OrderDetailDTO>();
                    decimal subTotalAmount = 0;
                    orderHistory.TransactionDate = historyHeader.TransactionDate;
                    orderHistory.TransactionNo = historyHeader.TransactionNo;
                    orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                    orderHistory.DeliveryText = new ShoppingCartBL(null).DeliveryTypeText(historyHeader.DeliveryTypeID, 1);
                    if (historyHeader.OrderDetails.IsNotNull() && historyHeader.OrderDetails.Count > 0)
                    {
                        foreach (HistoryDetail historyDetail in historyHeader.OrderDetails)
                        {
                            orderHistory.OrderDetails.Add(new OrderDetailDTO()
                            {
                                TransactionIID = historyDetail.TransactionIID,
                                ProductID = historyDetail.ProductID,
                                ProductName = historyDetail.ProductName,
                                ProductImageUrl = historyDetail.ProductImageUrl,
                                ProductSKUMapID = historyDetail.ProductSKUMapID,
                                Quantity = historyDetail.Quantity,
                                UnitID = historyDetail.UnitID,
                                DiscountPercentage = historyDetail.DiscountPercentage,
                                UnitPrice = historyDetail.UnitPrice * ConversionRate,
                                Amount = historyDetail.Amount * ConversionRate, // price*quantity saved in Amount fiels in detail
                                ExchangeRate = historyDetail.ExchangeRate,
                                Properties = shoppingCartRepository.GetPropertiesBySKU(Convert.ToInt64(historyDetail.ProductSKUMapID)),
                                Categories = shoppingCartRepository.GetCategoriesBySKU(Convert.ToInt64(historyDetail.ProductSKUMapID)),
                                ProductUrl = string.Format("productID={0}&productName={1}&skuID={2}", Convert.ToString(historyDetail.ProductID), Convert.ToString(historyDetail.ProductName), Convert.ToString(historyDetail.ProductSKUMapID)),
                                SerialNumber = historyDetail.SerialNumber,
                                ActualQuantity = historyDetail.ActualQuantity
                            });

                            subTotalAmount = subTotalAmount + Convert.ToDecimal(historyDetail.Amount * ConversionRate);
                        }
                    }
                    orderHistory.Currency = _callContext.CurrencyCode;
                    orderHistory.SubTotal = subTotalAmount;
                    orderHistory.UserDetail = userDetail;
                    HistoryDetails.Add(orderHistory);
                }
            }

            return HistoryDetails;

        }

        public int GetOrderHistoryCount(string documentTypeID, long customerID)
        {
            return new UserServiceRepository().GetOrderHistoryCount(documentTypeID, customerID);
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetailsWithPagination(string documentTypeID, string customerIID, int pageNumber, int pageSize, CallContext context = null, bool withCurrencyConversion = true)
        {
            var historyHeaderList = new UserServiceRepository().GetOrderHistoryDetailsWithPagination(documentTypeID, customerIID, pageNumber, pageSize);
            var orderHistoryListDTO = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();
            decimal ConversionRate = 1;
            var userDetail = new UserDTO();

            if (context.IsNull())
            {
                context = new CallContext();
                context.CurrencyCode = ConfigurationExtensions.GetAppConfigValue("CurrencyCode");
            }

            // get the converted price 
            ConversionRate = UtilityRepository.GetCurrencyPrice(context);

            if (!withCurrencyConversion)
                ConversionRate = 1;
            OrderHistoryDTO orderHistory = null;
            foreach (var historyHeader in historyHeaderList)
            {
                orderHistory = new OrderHistoryDTO();
                orderHistory.OrderDetails = new List<OrderDetailDTO>();
                decimal subTotalAmount = 0;
                orderHistory.TransactionDate = historyHeader.TransactionDate;
                orderHistory.Currency = _callContext.CurrencyCode != null ? _callContext.CurrencyCode : "";
                orderHistory.TransactionNo = historyHeader.TransactionNo;
                orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                orderHistory.Description = historyHeader.Description;
                orderHistory.CustomerID = historyHeader.CustomerID;
                orderHistory.SupplierID = historyHeader.SupplierID;
                orderHistory.DocumentTypeID = historyHeader.DocumentTypeID;
                orderHistory.DeliveryTypeID = historyHeader.DeliveryTypeID;
                orderHistory.PaymentMethod = historyHeader.PaymentMethod;
                orderHistory.LoyaltyPoints = historyHeader.LoyaltyPoints;
                orderHistory.VoucherAmount = historyHeader.VoucherAmount != null ? Convert.ToDecimal(historyHeader.VoucherAmount * ConversionRate) : 0;
                orderHistory.DiscountAmount = historyHeader.DiscountAmount != null ? Convert.ToDecimal(historyHeader.DiscountAmount * ConversionRate) : 0;
                orderHistory.DiscountPercentage = historyHeader.DiscountPercentage != null ? historyHeader.DiscountPercentage : 0;
                orderHistory.TransactionStatus = (Services.Contracts.Enums.TransactionStatus)historyHeader.TransactionStatus;
                try
                {
                    orderHistory.ActualOrderStatus = (Services.Contracts.Enums.ActualOrderStatus)historyHeader.ActualOrderStatus;
                }
                catch (Exception ex) { orderHistory.ActualOrderStatus = ActualOrderStatus.Picking; }
                var productList = GetOrderHistoryItemDetails(documentTypeID, 1, 10, orderID: historyHeader.TransactionOrderIID, withCurrencyConversion: false);
                foreach (var product in productList)
                {
                    subTotalAmount = subTotalAmount + product.SubTotal;
                }
                try
                {
                    //orderHistory.StatusTransaction = Enum.GetName(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus), historyHeader.ActualOrderStatus).ToString();
                    //var obj = (Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus), historyHeader.ActualOrderStatus.ToString());
                    orderHistory.StatusTransaction = ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>.GetDisplayValue((Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus), historyHeader.ActualOrderStatus.ToString())), this._callContext.LanguageCode);
                }
                catch (Exception ex) { orderHistory.StatusTransaction = ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>.GetDisplayValue(ActualOrderStatus.Picking), this._callContext.LanguageCode); }
                //TransactionHeadShoppingCartMap transactionHeadShoppingCartMap = userServiceRepository.GetTransactionHeadShoppingCartMap(historyHeader.TransactionOrderIID);

                //if (transactionHeadShoppingCartMap.IsNotNull() && transactionHeadShoppingCartMap.ShoppingCartID.IsNotNull())
                //{
                //    orderHistory.DeliveryCharge = transactionHeadShoppingCartMap.DeliveryCharge.IsNull() ? 0 : transactionHeadShoppingCartMap.DeliveryCharge * ConversionRate;
                //}
                //else
                //{
                //    orderHistory.DeliveryCharge = 0;
                //}
                orderHistory.DeliveryCharge = historyHeader.DeliveryCharge;

                orderHistory.SubTotal = subTotalAmount;
                var total = orderHistory.SubTotal + (decimal)orderHistory.DeliveryCharge - orderHistory.VoucherAmount;

                if (Convert.ToDecimal(orderHistory.DiscountAmount) > 0)
                {
                    total = total - Convert.ToDecimal(orderHistory.DiscountAmount);
                    orderHistory.Discount = Convert.ToDecimal(orderHistory.DiscountAmount);
                }
                else if (Convert.ToDecimal(orderHistory.DiscountPercentage) > 0)
                {
                    orderHistory.Discount = (total * Convert.ToDecimal(orderHistory.DiscountPercentage) / 100);
                    total = total - ((total * Convert.ToDecimal(orderHistory.DiscountPercentage) / 100));
                }

                orderHistory.Total = total;
                if (historyHeader.ParentTransactionOrderIID.IsNotNull() && historyHeader.ParentTransactionOrderIID > 0)
                {
                    var parentTransaction = new TransactionBL(_callContext).GetTransactionDetail(Convert.ToInt64(historyHeader.ParentTransactionOrderIID));
                    orderHistory.ParentTransactionNo = parentTransaction.TransactionNo;
                    orderHistory.ParentTransactionOrderIID = parentTransaction.HeadIID;
                }

                orderHistory.ResendMail = historyHeader.DeliveryTypeID == (int)DeliveryTypes.Email ?
                        orderHistory.ActualOrderStatus == Eduegate.Services.Contracts.Enums.ActualOrderStatus.Delivered ? true : false :
                        orderHistory.ActualOrderStatus != Eduegate.Services.Contracts.Enums.ActualOrderStatus.Failed ? true : false;

                orderHistoryListDTO.Add(orderHistory);

            }
            return orderHistoryListDTO;
        }


        public List<WishListDTO> GetWishListDetails(CallContext callContext)
        {
            List<SearchResult> wishList = userServiceRepository.GetWishListDetails(callContext.EmailID);
            List<WishListDTO> wishListDTO = new List<WishListDTO>();
            decimal ConvertedPrice = UtilityRepository.GetCurrencyPrice(callContext);

            if (wishList.Count > 0)
            {
                foreach (SearchResult product in wishList)
                {
                    wishListDTO.Add(new WishListDTO()
                    {
                        ProductIID = product.ProductIID,
                        ProductName = product.ProductName,
                        ProductImageUrl = product.ImageFile,
                        ProductPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(product.Amount) * ConvertedPrice, 3),
                        ProductDiscountedPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(product.DiscountedPrice) * ConvertedPrice, 3),
                        DesignerName = product.BrandName,
                        SKUID = product.SKUID,
                        SellingQuantityLimit = product.SellingQuantityLimit,
                        Quantity = product.Quantity,
                        Currency = _callContext.CurrencyCode
                    });
                }
            }
            return wishListDTO;
        }

        public bool AddWishListItem(long skuID, CallContext callContext)
        {
            return userServiceRepository.AddWishListItem(callContext.EmailID, skuID);
        }

        public bool RemoveWishListItem(long skuID, CallContext callContext)
        {
            return userServiceRepository.RemoveWishListItem(callContext.EmailID, skuID);
        }

        public bool RemoveSaveForLater(long skuID, CallContext callContext)
        {
            return userServiceRepository.RemoveSaveForLater(callContext.EmailID, skuID);
        }

        public bool AddSaveForLater(long skuID, CallContext callContext)
        {
            return userServiceRepository.AddSaveForLater(callContext.EmailID, skuID);
        }

        public List<WishListDTO> GetSaveForLater(CallContext callContext)
        {
            string strSKUID = "";
            List<SearchResult> SaveForLaterList = userServiceRepository.GetSaveForLater(callContext.EmailID);
            if (SaveForLaterList != null && SaveForLaterList.Count > 0)
            {
                foreach (SearchResult productSKUID in SaveForLaterList)
                {
                    strSKUID += productSKUID.SKUID + ",";
                }
                if (strSKUID.Length > 0)
                {
                    strSKUID = strSKUID.Remove(strSKUID.Length - 1, 1);

                }
            }
            List<WishListDTO> SaveForLaterListDTO = new List<WishListDTO>();
            //decimal ConvertedPrice = UtilityRepository.GetCurrencyPrice(callContext);
            decimal ConvertedPrice = (decimal)UtilityRepository.GetExchangeRate((int)_callContext.CompanyID, _callContext.CurrencyCode);
            //decimal ConvertedPrice = 1;

            if (SaveForLaterList.Count > 0)
            {
                var productInventoryList = new ProductDetailBL(_callContext).GetProductInventoryOnline(strSKUID, 0);
                foreach (SearchResult product in SaveForLaterList)
                {
                    var productInventory = (from productInventoryDetails in productInventoryList
                                            where productInventoryDetails.ProductSKUMapID == product.SKUID
                                            select productInventoryDetails).FirstOrDefault();

                    SaveForLaterListDTO.Add(new WishListDTO()
                    {
                        ProductIID = product.ProductIID,
                        ProductName = product.ProductName,
                        ProductImageUrl = product.ImageFile,
                        ProductPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(productInventory.ProductPricePrice * ConvertedPrice, 3),
                        ProductDiscountedPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(productInventory.ProductDiscountPrice * ConvertedPrice, 3),
                        DesignerName = product.BrandName,
                        SKUID = product.SKUID,
                        SellingQuantityLimit = product.SellingQuantityLimit,
                        Quantity = product.Quantity,
                        Currency = _callContext.CurrencyCode
                    });
                }
            }
            return SaveForLaterListDTO;
        }

        public long GetSaveForLaterCount(CallContext callContext)
        {
            return userServiceRepository.GetSaveForLaterCount(callContext.EmailID);
        }

        public bool UpdateNewsLetter(CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            bool result = false;

            if (customerDTO.IsNotNull())
            {
                customer.CustomerIID = customerDTO.CustomerIID;
                customer.LoginID = customerDTO.LoginID;
                customer.IsSubscribeForNewsLetter = customerDTO.IsSubscribeOurNewsLetter;

                result = userServiceRepository.UpdateNewsLetter(customer);
            }

            return result;
        }

        //public bool UpdateWishList(decimal productIID, ContextDTO contextualInformation)
        //{
        //    bool isWishListUpdated = userServiceRepository.UpdateWishList(productIID, contextualInformation);
        //    return isWishListUpdated;
        //}

        public UserInfoDTO GetUserInfo(decimal loggedInUserIID)
        {
            var userInfo = userServiceRepository.GetUserInfo(loggedInUserIID);
            return userInfo;
        }

        public Contact GetContactDetail(long contactID)
        {
            return userServiceRepository.GetContactDetail(contactID);
        }

        public List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID)
        {
            var productSKDDetailListDTO = new List<ProductSKUDetailDTO>();
            var productBLInit = new ProductBL(this._callContext);
            var productID = productIDs.Split(',');
            for (var i = 0; i <= productID.Length - 1; i++)
            {
                var productskudetailbl = productBLInit.GetProductDetailsEmarsys(long.Parse(productID[i]), cultureID);
                if (productskudetailbl.IsNotNull())
                {
                    if (productskudetailbl.SKUID != 0)
                    {
                        //productskudetailbl.ProductListingImage = productskudetailbl.ProductListingImage;
                        productSKDDetailListDTO.Add(productskudetailbl);
                    }
                }

            }
            return productSKDDetailListDTO;
        }


        private ContactDTO MapContactFromEntityToDTO(Contact contact)
        {
            var dto = new ContactDTO();

            if (contact.IsNull())
            {
                dto.ContactID = contact.ContactIID;
                dto.LoginID = contact.LoginID;
                dto.FirstName = contact.FirstName;
                dto.MiddleName = contact.MiddleName;
                dto.LastName = contact.LastName;
                dto.TitleID = contact.TitleID.HasValue ? (short?)short.Parse(contact.TitleID.ToString()) : null;
                dto.AddressLine1 = contact.AddressLine1;
                dto.AddressLine2 = contact.AddressLine2;
                dto.AddressName = contact.AddressName;
                dto.Block = contact.Block;
                dto.BuildingNo = contact.BuildingNo;
                dto.City = contact.City;
                dto.CivilIDNumber = contact.CivilIDNumber;
                dto.CountryID = contact.CountryID;
                dto.Description = contact.Description;
                dto.Flat = contact.Flat;
                dto.Floor = contact.Floor;
                dto.PassportIssueCountryID = contact.PassportIssueCountryID;
                dto.PassportNumber = contact.PassportNumber;
                dto.PostalCode = contact.PostalCode;
                dto.State = contact.State;
                dto.Street = contact.Street;
                dto.AlternateEmailID1 = contact.AlternateEmailID1;
                dto.AlternateEmailID2 = contact.AlternateEmailID2;
                dto.IsBillingAddress = contact.IsBillingAddress;
                dto.IsShippingAddress = contact.IsShippingAddress;
                dto.TelephoneCode = contact.TelephoneCode;
                dto.TelephoneNumber = contact.PhoneNo1;
            }
            else
            {
                dto = null;
            }
            return dto;
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            return new SecurityRepository().HasClaimAccess(claimID, userID);
        }

        public bool HasClaimAccessByResourceID(string resource, long userID)
        {
            var claim = Framework.CacheManager.MemCacheManager<bool?>.Get("CLAIMACESS_" + resource + "_" + userID.ToString());
            if (claim.HasValue)
            {
                return claim.Value;
            }
            else
            {
                var claimAccess = new SecurityRepository().HasClaimAccessByResourceID(resource, userID);
                Framework.CacheManager.MemCacheManager<bool?>.Add(claimAccess, "CLAIMACESS_" + resource + "_" + userID.ToString());
                return claimAccess;
            }
        }

        public List<ClaimDTO> GetClaimsByTypeAndLoginID(Eduegate.Services.Contracts.Enums.ClaimType type, long userID)
        {
            return Mappers.Security.ClaimMapper.Mapper(_callContext).ToDTO(new SecurityRepository().GetUserClaimsByType(userID, (int)type));
        }

        public List<ClaimDTO> GetDashBoardByUserID(Eduegate.Services.Contracts.Enums.ClaimType type, long userID)
        {
            return Mappers.Security.ClaimMapper.Mapper(_callContext).ToDTO(new SecurityRepository().GetDashBoardByUserID(userID, (int)type));
        }

        public int? GetJobStatusByHeadID(long headID)
        {
            return new TransactionRepository().GetJobStatusByHeadID(headID);
        }

        public List<KeyValueDTO> GetReplacementActions(long headID)
        {
            var jobstatusID = GetJobStatusByHeadID(headID);
            var jobStatuses = new List<KeyValueDTO>();
            if (jobstatusID != default(int?) && jobstatusID.HasValue)
            {
                if (this._callContext.IsNotNull())
                {
                    this._callContext.LoginID = this._callContext.LoginID.HasValue ? this._callContext.LoginID.Value : 0;
                }
                else
                {
                    this._callContext = new CallContext();
                    this._callContext.LoginID = 0;
                }
                jobStatuses = new ReferenceDataBL(this._callContext).GetLookUpData(LookUpTypes.ReplacementActions);
                jobStatuses.RemoveAll(a => a.Key == Convert.ToString((int)Framework.Helper.Enums.ReplacementActions.NoAction));
                var allowedCancelStatus = new SettingBL().GetSettingDetail(Constants.OrderCancelStatusSettings.ALLOWCANCELLATIONJOBSTATUS, this._callContext.CompanyID.Value);
                var allowedReturnStatus = new SettingBL().GetSettingDetail(Constants.OrderCancelStatusSettings.ALLOWRETURNJOBSTATUS, this._callContext.CompanyID.Value);
                var allowedCancelArray = allowedCancelStatus.SettingValue.Split(',');
                var allowedReturnArray = allowedReturnStatus.SettingValue.Split(',');
                if (allowedCancelArray.Contains(jobstatusID.ToString()))
                {
                    jobStatuses.RemoveAll(a => a.Key == Convert.ToString((int)Framework.Helper.Enums.ReplacementActions.Return));
                    jobStatuses.RemoveAll(a => a.Key == Convert.ToString((int)Framework.Helper.Enums.ReplacementActions.Replace));
                    jobStatuses.RemoveAll(a => a.Key == Convert.ToString((int)Framework.Helper.Enums.ReplacementActions.Exchange));
                }
                else if (allowedReturnArray.Contains(jobstatusID.ToString()))
                {
                    jobStatuses.RemoveAll(a => a.Key == Convert.ToString((int)Framework.Helper.Enums.ReplacementActions.Cancellation));
                }
                else
                {
                    jobStatuses = new List<KeyValueDTO>();
                }

            }
            jobStatuses = GetJobStatusesGlobalization(jobStatuses);
            return jobStatuses;

        }

        public List<KeyValueDTO> GetJobStatusesGlobalization(List<KeyValueDTO> jobStatuses)
        {

            foreach (var jobStatus in jobStatuses)
            {
                Eduegate.Services.Contracts.Enums.ReplacementActions replacementEnum;
                Enum.TryParse<Eduegate.Services.Contracts.Enums.ReplacementActions>(jobStatus.Key, out replacementEnum);
                jobStatus.Value = ResourceHelper.GetValue(EnumExtensions<Eduegate.Framework.Helper.Enums.ReplacementActions>.GetDisplayValue((Eduegate.Framework.Helper.Enums.ReplacementActions)Enum.Parse(typeof(Eduegate.Framework.Helper.Enums.ReplacementActions), jobStatus.Key)), this._callContext.LanguageCode);
            }

            return jobStatuses;
        }

        public long? RegisterUserDevice(string token, string userType)
        {
            var repository = new UserServiceRepository();
            var existingDevice = repository.GetRegisteredUserDevice(_callContext.LoginID.Value, token);

            var settingBL = new SettingBL(_callContext);

            string excludedUser = settingBL.GetSettingValue<string>("USEREXCLUDED");
            long? excludedUserID = long.Parse(excludedUser);

            bool isActive;

            if (userType == "Staff")
            {
                var registeredDevices = repository.GetRegisteredUserDeviceListByLoginID(_callContext.LoginID.Value);

                if (registeredDevices.Count == 0)
                {
                    isActive = true;
                }
                else if (_callContext.LoginID.Value == excludedUserID)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                }
            }
            else
            {
                isActive = true;
            }

            if (existingDevice == null)
            {
                var newDeviceMap = repository.RegisterUserDevice(new UserDeviceMap()
                {
                    LoginID = _callContext.LoginID.Value,
                    DeviceToken = token,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = _callContext != null && _callContext.LoginID.HasValue ?
                            int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null,
                    IsActive = isActive,
                });

                return newDeviceMap.UserDeviceMapIID;
            }
            else
            {
                return existingDevice.UserDeviceMapIID;
            }
        }

        public OperationResultDTO CheckUserDeviceAvailability(string token)
        {
            OperationResultDTO message = new OperationResultDTO()
            {
                operationResult = OperationResult.Success,
                Message = ""
            };

            var settingBL = new SettingBL(_callContext);

            string excludedUser = settingBL.GetSettingValue<string>("USEREXCLUDED");
            long? excludedUserID = long.Parse(excludedUser);

            if (_callContext.LoginID.Value != excludedUserID)
            {
                var repository = new UserServiceRepository();

                var existingActiveDevice = repository.GetRegisteredActiveUserDeviceByLoginID(_callContext.LoginID.Value);

                if (existingActiveDevice != null)
                {
                    if (existingActiveDevice.DeviceToken != token)
                    {
                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "You have logged into a new device, please contact admin and activate your device"
                        };
                    }
                    else
                    {
                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = ""
                        };
                    }
                }
                else
                {
                    var lastRegisteredDevice = repository.GetLastRegisteredUserDeviceByLoginID(_callContext.LoginID.Value);

                    if (lastRegisteredDevice != null)
                    {
                        if (lastRegisteredDevice.IsActive == true)
                        {
                            if (lastRegisteredDevice.DeviceToken == token)
                            {
                                message = new OperationResultDTO()
                                {
                                    operationResult = OperationResult.Success,
                                    Message = ""
                                };
                            }
                            else
                            {
                                message = new OperationResultDTO()
                                {
                                    operationResult = OperationResult.Error,
                                    Message = "You have logged into a new device, please contact admin and activate your device"
                                };
                            }
                        }
                        else
                        {
                            message = new OperationResultDTO()
                            {
                                operationResult = OperationResult.Error,
                                Message = "Your device not activated, please contact admin and activate your device"
                            };
                        }
                    }
                }

            }

            return message;
        }

    }
}