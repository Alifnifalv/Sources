using Eduegate.Domain.Helpers;
using Eduegate.Domain.Handlers;
using Eduegate.Framework;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.Eduegates;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Eduegate.Domain.MobileAppWrapper;
using Eduegate.Domain.Repository;
using System.Linq;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Globalization;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Managers
{
    public class OrderManager
    {
        CallContext _callContext;
        ShoppingCartHelper _cartHelpers;
        OrderHandler _orderHandler;
        Schedulers.Scheduler _scheduler;
        TransactionBL transactionBL;

        public static OrderManager Manager(CallContext callContext)
        {
            var manager = new OrderManager();
            manager._callContext = callContext;
            manager._cartHelpers = ShoppingCartHelper.Helper(callContext);
            manager._orderHandler = OrderHandler.Handler(callContext, manager._cartHelpers);
            manager._scheduler = new Schedulers.Scheduler();
            return manager;
        }

        public string GenerateOrder(CartDTO cart,
            string paymentMethod, long trackID, UserRole? userRole = null, string deviceInfo = "",
            long? customerID = (int?)null, decimal deliveryCharge = -1, List<long> headIDs = null,
            CheckoutPaymentDTO paymentDTO = null)
        {
            var processedDocumentTypeIds = new List<int>();
            var transactions = _orderHandler.OrderGenerationByBranches(cart, paymentMethod, trackID,
                userRole, deviceInfo, customerID, deliveryCharge, paymentDTO);

            string generatedOrderNumber = string.Empty;
            transactions.ForEach(transaction =>
            {
                transaction.TransactionHead.TransactionDetails = transaction.TransactionDetails;
                _scheduler.Process(transaction.TransactionHead);
                generatedOrderNumber = generatedOrderNumber + transaction.TransactionHead.HeadIID.ToString() + ",";
                headIDs.Add(transaction.TransactionHead.HeadIID);
            });

            
            if (generatedOrderNumber.Length > 0)
            {
                generatedOrderNumber = generatedOrderNumber.Remove(generatedOrderNumber.Length - 1, 1);
            }

            return generatedOrderNumber;
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetPaymentPendingOrders()
        {
            try
            {
                var shoppingCartRepository = new ShoppingCartRepository();
                var dtos = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();
                var customerId = _cartHelpers.GetCustomerID(_callContext);
                var failedPaymenetPeriod = new SettingBL(_callContext).GetSettingValue<int>("FAILEDPAYMENTPERIODINDAYS", 4);
                var carts = shoppingCartRepository.GetPaymentPendingOrders(customerId, failedPaymenetPeriod);

                foreach (var cart in carts)
                {
                    var branch = new ReferenceDataBL(_callContext).GetBranch(cart.BranchID.Value);
                    var contact = cart.ShippingAddressID.HasValue ? new AppDataBL(_callContext).GetAddressByContactID(cart.ShippingAddressID.Value) : null;
                    var orderDetail = new Services.Contracts.OrderHistory.OrderHistoryDTO()
                    {
                        CartID = cart.ShoppingCartIID,
                        CustomerID = cart.CustomerID,
                        CartPaymentMethod = cart.PaymentMethod,
                        CustomerName = cart.CustomerID.ToString(),
                        TransactionDate = cart.UpdatedDate,
                        PaymentMethod = cart.PaymentMethod,
                        Total = Convert.ToDecimal(cart.ShoppingCartItems.Sum(x => x.Quantity * x.ProductDiscountPrice)),
                        DeliveryCharge = cart.DeliveryCharge,
                        BranchID = cart.BranchID,
                        BranchCode = branch.BranchCode,
                        BranchName = branch.BranchName,
                        DeliveryAddress = contact != null ? new Services.Contracts.OrderContactMapDTO()
                        {
                            ContactID = cart.ShippingAddressID,
                            AddressLine1 = contact.AddressLine1,
                            AddressLine2 = contact.AddressLine2,
                            AddressName = contact.AddressName,
                            AreaName = contact.AreaName,
                            Block = contact.Block,
                            BuildingNo = contact.BuildingNo,
                            FirstName = contact.FirstName,
                            Flat = contact.Flat,
                            Floor = contact.Floor
                        } : null
                    };

                    dtos.Add(orderDetail);
                }

                return dtos;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ShoppingCartBL>.Fatal(ex.Message, ex);
                throw;
            }
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO>
            GetOrderHistoryDetailsWithPagination(string documentTypeID, string customerIID, int pageNumber, int pageSize,
            CallContext context = null, bool withCurrencyConversion = true)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            var historyHeaderList = new TransactionRepository()
                .GetOrderHistoryDetailsWithPagination(documentTypeID, customerIID, pageNumber, pageSize);
            var orderHistoryListDTO = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();
            decimal ConversionRate = 1;
            var userDetail = new UserDTO();

            if (context.IsNull())
            {
                context = new CallContext();
                context.CurrencyCode = ConfigurationExtensions.GetAppConfigValue("CurrencyCode");
            }

            // get the converted price 
            ConversionRate = new ReferenceDataRepository().GetCurrencyPrice(context);

            if (!withCurrencyConversion)
                ConversionRate = 1;


            Services.Contracts.OrderHistory.OrderHistoryDTO orderHistory = null;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())

            foreach (var historyHeader in historyHeaderList)
            {
                orderHistory = new Services.Contracts.OrderHistory.OrderHistoryDTO();
                orderHistory.OrderDetails = new List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO>();
                decimal subTotalAmount = 0;
                orderHistory.TransactionDateString = historyHeader.TransactionDate.HasValue ? historyHeader.TransactionDate.Value.ToString(dateFormat) : null;
                orderHistory.Currency = _callContext.CurrencyCode != null ? _callContext.CurrencyCode : "QAR";
                orderHistory.TransactionNo = historyHeader.TransactionNo;
                orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                orderHistory.Description = historyHeader.Description;
                orderHistory.CustomerID = historyHeader.CustomerID;
                orderHistory.SupplierID = historyHeader.SupplierID;
                orderHistory.DocumentTypeID = historyHeader.DocumentTypeID;
                orderHistory.DocumentStatusID = historyHeader.DocumentStatusID;
                orderHistory.DocumentStatus = Convert.ToString(historyHeader.DocumentStatusID);
                orderHistory.DeliveryTypeID = historyHeader.DeliveryTypeID;
                orderHistory.PaymentMethod = historyHeader.PaymentMethod; 
                orderHistory.CartPaymentMethod = historyHeader.CartPaymentMethod;
                orderHistory.LoyaltyPoints = historyHeader.LoyaltyPoints;
                orderHistory.BranchName = historyHeader.BranchCode;
                orderHistory.BranchName = historyHeader.BranchName;

                orderHistory.StudentID = historyHeader.StudentID;
                orderHistory.SchoolID = historyHeader.SchoolID;
                orderHistory.AcademicYearID = historyHeader.AcademicYearID;
                var StudentDetails = new AccountRepository().GetStudentDetails(Convert.ToInt64(historyHeader.StudentID));
                    if (StudentDetails != null) {
                        var classDet = historyHeader.StudentID != null ? dbContext.Classes.FirstOrDefault(a => a.ClassID == StudentDetails.ClassID) : null;
                        var secDet = historyHeader.StudentID != null ? dbContext.Sections.FirstOrDefault(a => a.SectionID == StudentDetails.SectionID) : null;
                        orderHistory.StudentName = StudentDetails.FirstName + StudentDetails.MiddleName + " " + StudentDetails.LastName;
                        orderHistory.ClassName = classDet.ClassDescription;
                        orderHistory.SectionName = secDet.SectionName;
                    }
                    var deliveryType =  dbContext.DeliveryTypes1.FirstOrDefault(a => a.DeliveryTypeID == historyHeader.DeliveryTypeID);
                orderHistory.DeliveryTypeName = deliveryType != null ? deliveryType.DeliveryTypeName : null;

                orderHistory.VoucherAmount = historyHeader.VoucherAmount != null ?
                    Convert.ToDecimal(historyHeader.VoucherAmount * ConversionRate) : 0;
                orderHistory.DiscountAmount = historyHeader.DiscountAmount != null ?
                    Convert.ToDecimal(historyHeader.DiscountAmount * ConversionRate) : 0;
                orderHistory.DiscountPercentage = historyHeader.DiscountPercentage != null ?
                    historyHeader.DiscountPercentage : 0;
                orderHistory.TransactionStatus =
                    (Services.Contracts.Enums.TransactionStatus)historyHeader.TransactionStatus;

                try
                {
                    orderHistory.ActualOrderStatus = (Services.Contracts.Enums.ActualOrderStatus)historyHeader
                        .ActualOrderStatus;
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<UserServiceBL>.Fatal(ex.Message, ex);
                    orderHistory.ActualOrderStatus = Services.Contracts.Enums.ActualOrderStatus.Picking;
                }

                orderHistory.JobStatusID = (int)orderHistory.ActualOrderStatus;
                var productList = new TransactionBL(_callContext).GetOrderHistoryItemDetails(documentTypeID, 1, 10, orderID: historyHeader.TransactionOrderIID, withCurrencyConversion: false);

                foreach (var product in productList)
                {
                    subTotalAmount = subTotalAmount + product.SubTotal;
                }

                try
                {
                    orderHistory.StatusTransaction = ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>
                        .GetDisplayValue((Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus),
                        historyHeader.ActualOrderStatus.ToString())), this._callContext.LanguageCode);
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<UserServiceBL>.Fatal(ex.Message, ex);
                    orderHistory.StatusTransaction =
                       ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>
                        .GetDisplayValue(Services.Contracts.Enums.ActualOrderStatus.Picking), this._callContext.LanguageCode);
                }

                orderHistory.DeliveryCharge = historyHeader.DeliveryCharge;

                orderHistory.SubTotal = subTotalAmount + (historyHeader.DiscountAmount.HasValue ? historyHeader.DiscountAmount.Value : 0);
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
                    var parentTransaction = new TransactionBL(_callContext)
                        .GetTransactionDetail(Convert.ToInt64(historyHeader.ParentTransactionOrderIID));
                    orderHistory.ParentTransactionNo = parentTransaction.TransactionNo;
                    orderHistory.ParentTransactionOrderIID = parentTransaction.HeadIID;
                }

                orderHistory.ResendMail = historyHeader.DeliveryTypeID == (int)DeliveryTypes.Email ?
                        orderHistory.ActualOrderStatus == Services.Contracts.Enums.ActualOrderStatus.Delivered ? true : false :
                        orderHistory.ActualOrderStatus != Services.Contracts.Enums.ActualOrderStatus.Failed ? true : false;

                orderHistory.JobStatusID = (int)orderHistory.ActualOrderStatus;
                orderHistory.DeliveryAddress = historyHeader.DeliveryAddress != null ? new OrderContactMapDTO()
                {
                    AddressLine1 = historyHeader.DeliveryAddress.AddressLine1,
                    AddressLine2 = historyHeader.DeliveryAddress.AddressLine2,
                    FirstName = historyHeader.DeliveryAddress.FirstName,
                    MobileNo1 = historyHeader.DeliveryAddress.MobileNo1,
                    ContactID = historyHeader.DeliveryAddress.ContactID,
                    AreaName = historyHeader.DeliveryAddress.AreaID.HasValue ? historyHeader.DeliveryAddress.Area?.AreaName : null,
                    BuildingNo = historyHeader.DeliveryAddress.BuildingNo,
                    LocationName = historyHeader.DeliveryAddress.LocationID.HasValue
                            ? new ReferenceDataRepository().GetAreaLocationName(historyHeader.DeliveryAddress.LocationID.Value) : null,
                } : null;

                orderHistoryListDTO.Add(orderHistory);
            }
            return orderHistoryListDTO;
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetScheduledOrderHistoryDetails(string documentTypeID, string customerIID, int pageNumber, int pageSize,CallContext context = null, bool withCurrencyConversion = true)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            var historyHeaderList = new TransactionRepository()
                .GetScheduledOrderHistoryDetailsWithPagination(documentTypeID, customerIID, pageNumber, pageSize);
            var orderHistoryListDTO = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();


            Services.Contracts.OrderHistory.OrderHistoryDTO orderHistory = null;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())

                foreach (var historyHeader in historyHeaderList)
                {
                    orderHistory = new Services.Contracts.OrderHistory.OrderHistoryDTO();
                    orderHistory.OrderDetails = new List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO>();
                    decimal subTotalAmount = 0;
                    orderHistory.TransactionDateString = historyHeader.TransactionDate.HasValue ? historyHeader.TransactionDate.Value.ToString(dateFormat) : null;
                    orderHistory.Currency = _callContext.CurrencyCode != null ? _callContext.CurrencyCode : "QAR";
                    orderHistory.TransactionNo = historyHeader.TransactionNo;
                    orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                    orderHistory.Description = historyHeader.Description;
                    orderHistory.CustomerID = historyHeader.CustomerID;
                    orderHistory.SupplierID = historyHeader.SupplierID;
                    orderHistory.DocumentTypeID = historyHeader.DocumentTypeID;
                    orderHistory.DocumentStatusID = historyHeader.DocumentStatusID;
                    orderHistory.DocumentStatus = Convert.ToString(historyHeader.DocumentStatusID);
                    orderHistory.DeliveryTypeID = historyHeader.DeliveryTypeID;
                    orderHistory.PaymentMethod = historyHeader.PaymentMethod;
                    orderHistory.CartPaymentMethod = historyHeader.CartPaymentMethod;
                    orderHistory.LoyaltyPoints = historyHeader.LoyaltyPoints;
                    orderHistory.BranchName = historyHeader.BranchCode;
                    orderHistory.BranchName = historyHeader.BranchName;

                    orderHistory.StudentID = historyHeader.StudentID;
                    orderHistory.SchoolID = historyHeader.SchoolID;
                    orderHistory.AcademicYearID = historyHeader.AcademicYearID;

                    orderHistory.StartDate = historyHeader.StartDate;
                    orderHistory.EndDate = historyHeader.EndDate;
                    orderHistory.SubscriptionType = historyHeader.SubscriptionType;
                    orderHistory.DeliveryTimeSlots = historyHeader.DeliveryTimeSlots;

                    orderHistory.SubciptionDeliveryDays = new List<Framework.Contracts.Common.KeyValueDTO>();
                    foreach(var day in historyHeader.SubciptionDeliveryDays)
                    {
                        var days = dbContext.Days.FirstOrDefault(a => a.DayID == day.WeekDayID);

                        orderHistory.SubciptionDeliveryDays.Add(new Framework.Contracts.Common.KeyValueDTO() 
                        { 
                            Key = day.WeekDayID.ToString(),
                            Value = days.DayName,
                        });
                    }
                    //orderHistory.SubciptionDeliveryDays = historyHeader.SubciptionDeliveryDays;


                    var StudentDetails = new AccountRepository().GetStudentDetails(Convert.ToInt64(historyHeader.StudentID));
                    if (StudentDetails != null)
                    {
                        var classDet = historyHeader.StudentID != null ? dbContext.Classes.FirstOrDefault(a => a.ClassID == StudentDetails.ClassID) : null;
                        var secDet = historyHeader.StudentID != null ? dbContext.Sections.FirstOrDefault(a => a.SectionID == StudentDetails.SectionID) : null;
                        orderHistory.StudentName = StudentDetails.FirstName + " " +StudentDetails.MiddleName + " " + StudentDetails.LastName;
                        orderHistory.ClassName = classDet.ClassDescription;
                        orderHistory.SectionName = secDet.SectionName;
                    }
                    var deliveryType = dbContext.DeliveryTypes1.FirstOrDefault(a => a.DeliveryTypeID == historyHeader.DeliveryTypeID);
                    orderHistory.DeliveryTypeName = deliveryType != null ? deliveryType.DeliveryTypeName : null;

                    orderHistory.DiscountPercentage = historyHeader.DiscountPercentage != null ?
                        historyHeader.DiscountPercentage : 0;
                    orderHistory.TransactionStatus =
                        (Services.Contracts.Enums.TransactionStatus)historyHeader.TransactionStatus;

                    try
                    {
                        orderHistory.ActualOrderStatus = (Services.Contracts.Enums.ActualOrderStatus)historyHeader
                            .ActualOrderStatus;
                    }
                    catch (Exception ex)
                    {
                        Eduegate.Logger.LogHelper<UserServiceBL>.Fatal(ex.Message, ex);
                        orderHistory.ActualOrderStatus = Services.Contracts.Enums.ActualOrderStatus.Picking;
                    }

                    orderHistory.JobStatusID = (int)orderHistory.ActualOrderStatus;
                    var productList = new TransactionBL(_callContext).GetOrderHistoryItemDetails(documentTypeID, 1, 10, orderID: historyHeader.TransactionOrderIID, withCurrencyConversion: false);

                    foreach (var product in productList)
                    {
                        subTotalAmount = subTotalAmount + product.SubTotal;
                    }

                    try
                    {
                        orderHistory.StatusTransaction = ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>
                            .GetDisplayValue((Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus),
                            historyHeader.ActualOrderStatus.ToString())), this._callContext.LanguageCode);
                    }
                    catch (Exception ex)
                    {
                        Eduegate.Logger.LogHelper<UserServiceBL>.Fatal(ex.Message, ex);
                        orderHistory.StatusTransaction =
                           ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>
                            .GetDisplayValue(Services.Contracts.Enums.ActualOrderStatus.Picking), this._callContext.LanguageCode);
                    }

                    orderHistory.DeliveryCharge = historyHeader.DeliveryCharge;

                    orderHistory.SubTotal = subTotalAmount + (historyHeader.DiscountAmount.HasValue ? historyHeader.DiscountAmount.Value : 0);
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
                        var parentTransaction = new TransactionBL(_callContext)
                            .GetTransactionDetail(Convert.ToInt64(historyHeader.ParentTransactionOrderIID));
                        orderHistory.ParentTransactionNo = parentTransaction.TransactionNo;
                        orderHistory.ParentTransactionOrderIID = parentTransaction.HeadIID;
                    }

                    orderHistory.ResendMail = historyHeader.DeliveryTypeID == (int)DeliveryTypes.Email ?
                            orderHistory.ActualOrderStatus == Services.Contracts.Enums.ActualOrderStatus.Delivered ? true : false :
                            orderHistory.ActualOrderStatus != Services.Contracts.Enums.ActualOrderStatus.Failed ? true : false;

                    orderHistory.JobStatusID = (int)orderHistory.ActualOrderStatus;
                    orderHistory.DeliveryAddress = historyHeader.DeliveryAddress != null ? new OrderContactMapDTO()
                    {
                        AddressLine1 = historyHeader.DeliveryAddress.AddressLine1,
                        AddressLine2 = historyHeader.DeliveryAddress.AddressLine2,
                        FirstName = historyHeader.DeliveryAddress.FirstName,
                        MobileNo1 = historyHeader.DeliveryAddress.MobileNo1,
                        ContactID = historyHeader.DeliveryAddress.ContactID,
                        AreaName = historyHeader.DeliveryAddress.AreaID.HasValue ? historyHeader.DeliveryAddress.Area?.AreaName : null,
                        BuildingNo = historyHeader.DeliveryAddress.BuildingNo,
                        LocationName = historyHeader.DeliveryAddress.LocationID.HasValue
                                ? new ReferenceDataRepository().GetAreaLocationName(historyHeader.DeliveryAddress.LocationID.Value) : null,
                    } : null;

                    orderHistoryListDTO.Add(orderHistory);
                }
            return orderHistoryListDTO;
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetCanteenNormalOrderHistoryDetails(string documentTypeID, string customerIID, int pageNumber, int pageSize, CallContext context = null, bool withCurrencyConversion = true)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            var historyHeaderList = new TransactionRepository()
                .GetCanteenNormalOrderHistoryDetailsWithPagination(documentTypeID, customerIID, pageNumber, pageSize);
            var orderHistoryListDTO = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();


            Services.Contracts.OrderHistory.OrderHistoryDTO orderHistory = null;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())

                foreach (var historyHeader in historyHeaderList)
                {
                    orderHistory = new Services.Contracts.OrderHistory.OrderHistoryDTO();
                    orderHistory.OrderDetails = new List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO>();
                    decimal subTotalAmount = 0;
                    orderHistory.TransactionDateString = historyHeader.TransactionDate.HasValue ? historyHeader.TransactionDate.Value.ToString(dateFormat) : null;
                    orderHistory.Currency = _callContext.CurrencyCode != null ? _callContext.CurrencyCode : "QAR";
                    orderHistory.TransactionNo = historyHeader.TransactionNo;
                    orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                    orderHistory.Description = historyHeader.Description;
                    orderHistory.CustomerID = historyHeader.CustomerID;
                    orderHistory.SupplierID = historyHeader.SupplierID;
                    orderHistory.DocumentTypeID = historyHeader.DocumentTypeID;
                    orderHistory.DocumentStatusID = historyHeader.DocumentStatusID;
                    orderHistory.DocumentStatus = Convert.ToString(historyHeader.DocumentStatusID);
                    orderHistory.DeliveryTypeID = historyHeader.DeliveryTypeID;
                    orderHistory.PaymentMethod = historyHeader.PaymentMethod;
                    orderHistory.CartPaymentMethod = historyHeader.CartPaymentMethod;
                    orderHistory.LoyaltyPoints = historyHeader.LoyaltyPoints;
                    orderHistory.BranchName = historyHeader.BranchCode;
                    orderHistory.BranchName = historyHeader.BranchName;

                    orderHistory.StudentID = historyHeader.StudentID;
                    orderHistory.SchoolID = historyHeader.SchoolID;
                    orderHistory.AcademicYearID = historyHeader.AcademicYearID;

                    orderHistory.StartDate = historyHeader.StartDate;
                    orderHistory.EndDate = historyHeader.EndDate;
                    orderHistory.SubscriptionType = historyHeader.SubscriptionType;
                    orderHistory.DeliveryTimeSlots = historyHeader.DeliveryTimeSlots;

                    orderHistory.SubciptionDeliveryDays = new List<Framework.Contracts.Common.KeyValueDTO>();
                    foreach (var day in historyHeader.SubciptionDeliveryDays)
                    {
                        var days = dbContext.Days.FirstOrDefault(a => a.DayID == day.WeekDayID);

                        orderHistory.SubciptionDeliveryDays.Add(new Framework.Contracts.Common.KeyValueDTO()
                        {
                            Key = day.WeekDayID.ToString(),
                            Value = days.DayName,
                        });
                    }
                    //orderHistory.SubciptionDeliveryDays = historyHeader.SubciptionDeliveryDays;


                    var StudentDetails = new AccountRepository().GetStudentDetails(Convert.ToInt64(historyHeader.StudentID));
                    if (StudentDetails != null)
                    {
                        var classDet = historyHeader.StudentID != null ? dbContext.Classes.FirstOrDefault(a => a.ClassID == StudentDetails.ClassID) : null;
                        var secDet = historyHeader.StudentID != null ? dbContext.Sections.FirstOrDefault(a => a.SectionID == StudentDetails.SectionID) : null;
                        orderHistory.StudentName = StudentDetails.FirstName + " " + StudentDetails.MiddleName + " " + StudentDetails.LastName;
                        orderHistory.ClassName = classDet.ClassDescription;
                        orderHistory.SectionName = secDet.SectionName;
                    }
                    var deliveryType = dbContext.DeliveryTypes1.FirstOrDefault(a => a.DeliveryTypeID == historyHeader.DeliveryTypeID);
                    orderHistory.DeliveryTypeName = deliveryType != null ? deliveryType.DeliveryTypeName : null;

                    orderHistory.DiscountPercentage = historyHeader.DiscountPercentage != null ?
                        historyHeader.DiscountPercentage : 0;
                    orderHistory.TransactionStatus =
                        (Services.Contracts.Enums.TransactionStatus)historyHeader.TransactionStatus;

                    try
                    {
                        orderHistory.ActualOrderStatus = (Services.Contracts.Enums.ActualOrderStatus)historyHeader
                            .ActualOrderStatus;
                    }
                    catch (Exception ex)
                    {
                        Eduegate.Logger.LogHelper<UserServiceBL>.Fatal(ex.Message, ex);
                        orderHistory.ActualOrderStatus = Services.Contracts.Enums.ActualOrderStatus.Picking;
                    }

                    orderHistory.JobStatusID = (int)orderHistory.ActualOrderStatus;
                    var productList = new TransactionBL(_callContext).GetOrderHistoryItemDetails(documentTypeID, 1, 10, orderID: historyHeader.TransactionOrderIID, withCurrencyConversion: false);

                    foreach (var product in productList)
                    {
                        subTotalAmount = subTotalAmount + product.SubTotal;
                    }

                    try
                    {
                        orderHistory.StatusTransaction = ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>
                            .GetDisplayValue((Eduegate.Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ActualOrderStatus),
                            historyHeader.ActualOrderStatus.ToString())), this._callContext.LanguageCode);
                    }
                    catch (Exception ex)
                    {
                        Eduegate.Logger.LogHelper<UserServiceBL>.Fatal(ex.Message, ex);
                        orderHistory.StatusTransaction =
                           ResourceHelper.GetValue(EnumExtensions<Eduegate.Services.Contracts.Enums.ActualOrderStatus>
                            .GetDisplayValue(Services.Contracts.Enums.ActualOrderStatus.Picking), this._callContext.LanguageCode);
                    }

                    orderHistory.DeliveryCharge = historyHeader.DeliveryCharge;

                    orderHistory.SubTotal = subTotalAmount + (historyHeader.DiscountAmount.HasValue ? historyHeader.DiscountAmount.Value : 0);
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
                        var parentTransaction = new TransactionBL(_callContext)
                            .GetTransactionDetail(Convert.ToInt64(historyHeader.ParentTransactionOrderIID));
                        orderHistory.ParentTransactionNo = parentTransaction.TransactionNo;
                        orderHistory.ParentTransactionOrderIID = parentTransaction.HeadIID;
                    }

                    orderHistory.ResendMail = historyHeader.DeliveryTypeID == (int)DeliveryTypes.Email ?
                            orderHistory.ActualOrderStatus == Services.Contracts.Enums.ActualOrderStatus.Delivered ? true : false :
                            orderHistory.ActualOrderStatus != Services.Contracts.Enums.ActualOrderStatus.Failed ? true : false;

                    orderHistory.JobStatusID = (int)orderHistory.ActualOrderStatus;
                    orderHistory.DeliveryAddress = historyHeader.DeliveryAddress != null ? new OrderContactMapDTO()
                    {
                        AddressLine1 = historyHeader.DeliveryAddress.AddressLine1,
                        AddressLine2 = historyHeader.DeliveryAddress.AddressLine2,
                        FirstName = historyHeader.DeliveryAddress.FirstName,
                        MobileNo1 = historyHeader.DeliveryAddress.MobileNo1,
                        ContactID = historyHeader.DeliveryAddress.ContactID,
                        AreaName = historyHeader.DeliveryAddress.AreaID.HasValue ? historyHeader.DeliveryAddress.Area?.AreaName : null,
                        BuildingNo = historyHeader.DeliveryAddress.BuildingNo,
                        LocationName = historyHeader.DeliveryAddress.LocationID.HasValue
                                ? new ReferenceDataRepository().GetAreaLocationName(historyHeader.DeliveryAddress.LocationID.Value) : null,
                    } : null;

                    orderHistoryListDTO.Add(orderHistory);
                }
            return orderHistoryListDTO;
        }
    }
}
