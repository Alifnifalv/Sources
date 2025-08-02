using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Distributions;
using Eduegate.Framework.Contracts.Common;
using System.Data;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Globalization;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Domain
{
    public class OrderBL
    {
        private CallContext _context;

        public OrderBL(CallContext context)
        {
            _context = context;
        }

        #region Public Methods
        public string ConfirmOnlineOrder(string paymentMethod, long trackID, CallContext callContext, long shoppingCartIID = 0, UserRole? userRole = null, string deviceInfo = "", long CustomerID = 0, decimal deliveryCharge = -1)
        {
            var settingOnlineBranch = new SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINEBRANCHID, _context.CompanyID.HasValue ? (long)_context.CompanyID.Value : default(long));
            var settingSysBlock = new SettingBL().GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, _context.CompanyID.HasValue ? (long)_context.CompanyID.Value : default(long));

            // get marketplace branch Id
            var marketplaceSettingDto = new SettingBL().GetSettingDetail(Constants.TransactionSettings.MARKETPLACESALES, _context.CompanyID.HasValue ? (long)_context.CompanyID.Value : default(long));

            bool isSetDeliveryCharge = false;
            var cart = new ShoppingCartBL(_context).GetCart(callContext, withCurrencyConversion: false, isDeliveryCharge: false, isValidation: false,
                cartStatus: ShoppingCartStatus.PaymentInitiated, ShoppingCartIID: shoppingCartIID, customerIID: CustomerID);

            var transactionID = "";
            var cartProductsBranchID = cart.Products.Select(x => x.BranchID).Distinct();
            SettingDTO setting = new SettingDTO();
            var voucherMap = new ShoppingCartRepository().GetVoucherMap(cart.ShoppingCartID);
            decimal voucherAmount = 0;

            if (voucherMap.IsNotNull())
            {
                voucherAmount = (decimal)voucherMap.Amount;
            }

            foreach (var branchID in cartProductsBranchID)
            {
                var cartProductsBranchIDDeliveryTypeID = (from o in cart.Products
                                                          select new
                                                          {
                                                              DeliveryTypeID = o.DeliveryTypeID,
                                                              BranchID = o.BranchID,
                                                              DeliveryPriority = o.DeliveryPriority,
                                                              DeliveryDays = o.DeliveryDays.HasValue ? o.DeliveryDays.Value : 0,
                                                          }).Where(o => o.BranchID == branchID).Distinct().OrderBy(x => x.DeliveryPriority);
                var settingBranch = new SettingDTO();
                // check is it Market Place
                if (branchID > 0)
                {
                    var branch = new ReferenceDataRepository().GetBranch(branchID, false);
                    if (branch.IsNotNull() && branch.IsMarketPlace == true)
                    {
                        if (marketplaceSettingDto != null)
                        {
                            settingBranch = marketplaceSettingDto;
                        }
                        else
                        {
                            settingBranch = settingSysBlock;
                        }
                    }
                    else
                    {
                        settingBranch = settingSysBlock;
                    }
                }
                else
                {
                    settingBranch = settingSysBlock;
                }

                foreach (var deliveryType in cartProductsBranchIDDeliveryTypeID)
                {
                    int deliveryDays = 0;
                    isSetDeliveryCharge = false;
                    switch (deliveryType.DeliveryTypeID)
                    {
                        case (int)DeliveryTypes.nDaysDelivery:
                            deliveryDays = deliveryType.DeliveryDays;
                            OrderSettings(settingOnlineBranch, deliveryType.DeliveryTypeID, branchID, ref setting, ref cart, ref isSetDeliveryCharge);
                            transactionID = transactionID + Convert.ToString(OrderGeneration(paymentMethod, trackID, callContext, cart, branchID, deliveryType.DeliveryTypeID, int.Parse(setting.SettingValue), isSetDeliveryCharge, ref voucherAmount, Convert.ToInt64(settingBranch.SettingValue), deliveryDays, userRole, deviceInfo, CustomerID, deliveryCharge)) + ",";
                            break;
                        default:
                            deliveryDays = 0;
                            OrderSettings(settingOnlineBranch, deliveryType.DeliveryTypeID, branchID, ref setting, ref cart, ref isSetDeliveryCharge);
                            transactionID = transactionID + Convert.ToString(OrderGeneration(paymentMethod, trackID, callContext, cart, branchID, deliveryType.DeliveryTypeID, int.Parse(setting.SettingValue), isSetDeliveryCharge, ref voucherAmount, Convert.ToInt64(settingBranch.SettingValue), deliveryDays, userRole, deviceInfo, CustomerID, deliveryCharge)) + ",";
                            break;
                    }
                }
            }

            if (transactionID.Length > 0)
            {
                transactionID = transactionID.Remove(transactionID.Length - 1, 1);
            }

            return transactionID;
        }

        public void OrderSettings(SettingDTO settingOnlineBranch, int deliveryTypeID, long branchID, ref SettingDTO setting, ref CartDTO cart, ref bool isSetDeliveryCharge)
        {
            var mapper = Mappers.Common.SettingMapper.Mapper();

            if (deliveryTypeID == (int)DeliveryTypes.Email || deliveryTypeID == (int)DeliveryTypes.EmailInternationalDelivery)
            {
                if (branchID == Convert.ToInt64(settingOnlineBranch.SettingValue))
                {
                    setting = mapper.ToDTO(new SettingRepository().GetSettingDetail(Constants.TransactionSettings.ONLINESALESDIGITALDOCTTYPEID, _context.CompanyID.Value));
                }
                else
                {
                    setting = mapper.ToDTO(new SettingRepository().GetSettingDetail(Constants.TransactionSettings.ONLINESALESMARDIGITALDOCTTYPEID, _context.CompanyID.Value));
                }
            }
            else
            {
                if (branchID == Convert.ToInt64(settingOnlineBranch.SettingValue))
                {
                    setting = mapper.ToDTO(new SettingRepository().GetSettingDetail(Constants.TransactionSettings.ONLINESALESDOCTTYPEID, _context.CompanyID.Value));
                }
                else
                {
                    setting = mapper.ToDTO(new SettingRepository().GetSettingDetail(Constants.TransactionSettings.ONLINESALESMARDOCTTYPEID, _context.CompanyID.Value));
                }

                if (cart.IsDeliveryChargeAssigned == false && cart.IsOnlineBranchPhysicalCartItems == true)
                {
                    isSetDeliveryCharge = true;
                    cart.IsDeliveryChargeAssigned = true;
                }
            }
        }

        public long OrderGeneration(string paymentMethod, long trackID, CallContext callContext, CartDTO cart, long branchID, int deliveryTypeID, int documentTypeID, bool isSetDeliveryCharge, ref decimal voucherAmount, long sysBlockedBranchID = 0, int deliveryDays = 0, UserRole? userRole = null, string deviceInfo = "", long CustomerID = 0, decimal erpDeliveryCharge = -1)
        {
            long transactionHeadID = default(long);

            if (cart.IsNotNull())
            {
                var customerID = CustomerID > 0 ? CustomerID : 0;//Convert.ToInt64(new UtilityBL().GetCustomerID(callContext));
                decimal cartAmount = 0;
                var supplier = new SupplierRepository().GetSupplierByBranchnID(branchID);

                //Populate TransactionHead and TransactionDetail entries from ShoppingCart and ShoppingCartItems respectively
                //Create a TransactionHead and generate HeadID, thi OrderID 
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                var parameterTransactionList = new List<KeyValueParameterDTO>();

                parameterTransactionList.Add(new KeyValueParameterDTO
                {
                    ParameterName = "CARTID",
                    ParameterValue = Convert.ToString(cart.ShoppingCartID),
                });

                transaction.TransactionHead.TransactionNo = new MutualBL(_context).GetAndSaveNextTransactionNumber(documentTypeID, parameterTransactionList);
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(documentTypeID);
                transaction.TransactionHead.CustomerID = customerID;//Convert.ToInt64(new UtilityBL().GetCustomerID(callContext));
                transaction.TransactionHead.SupplierID = supplier != null ? supplier.SupplierIID : (long?)null;
                transaction.TransactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess;
                transaction.TransactionHead.DocumentStatusID = (short)DocumentStatuses.Draft;
                // Hardcoded a check for KD, we can avoid this after currency dropdown correction
                //transaction.TransactionHead.CurrencyID = Convert.ToInt32(new UtilityBL().GetCurrencyDetail(callContext.CurrencyCode.IsNotNullOrEmpty() ? (callContext.CurrencyCode == "KD" ? "KWD" : callContext.CurrencyCode) : callContext.CompanyID.HasValue && callContext.CompanyID > 0 ? new ReferenceDataRepository().GetCurrency(Convert.ToInt32(new SettingRepository().GetSettingDetail(Constants.TransactionSettings.DEFAUTCURRENCY, (int)callContext.CompanyID).SettingValue)).AnsiCode : "KWD").CurrencyID);
                transaction.TransactionHead.BranchID = sysBlockedBranchID;
                Eduegate.Framework.Payment.PaymentGatewayType paymentGatewayMethod;
                Enum.TryParse(paymentMethod, out paymentGatewayMethod);
                var entitlementID = GetEntitlementType(paymentGatewayMethod);

                transaction.TransactionHead.EntitlementID = (byte)entitlementID;
                //var companyDetails = new ReferenceDataBL(null).GetCompanyByCountryID(1);
                transaction.TransactionHead.CompanyID = callContext.CompanyID;
                transaction.TransactionHead.DeliveryTypeID = deliveryTypeID;
                if (transaction.TransactionHead.DocumentTypeID == (int)DocumentTypes.SO4DigitalOnline)
                {
                    transaction.TransactionHead.DeliveryMethodID = (short)DeliveryMethods.Virtual;
                }
                else
                    transaction.TransactionHead.DeliveryMethodID = (short)DeliveryMethods.Physical;
                transaction.TransactionHead.DeliveryDays = deliveryDays;
                
                var DeliveryCharge = 0M;

                if (isSetDeliveryCharge)
                {
                    if (erpDeliveryCharge != -1)
                    {
                        DeliveryCharge = erpDeliveryCharge;
                    }
                    else
                    {
                        DeliveryCharge = cart.DeliveryCharge;
                    }
                }
                else
                {
                    DeliveryCharge = 0;
                }

                transaction.TransactionHead.DeliveryCharge = DeliveryCharge;

                //TODO: setting BranchID, ToBranchID, DiscountPercentage, DiscountAmount, IsShipment, JobEntryHeadID, DueDate, DeliveryDate, in transaction table
                List<CartProductDTO> cartProducts = null;
                List<DeliveryTypeTimeSlotMapCultureDTO> deliveryTextCultureDTOList = null;
                switch (deliveryTypeID)
                {
                    case (int)DeliveryTypes.nDaysDelivery:
                        cartProducts = cart.Products.Where(x => x.BranchID == branchID && x.DeliveryTypeID == deliveryTypeID && x.DeliveryDays == deliveryDays).ToList();
                        break;
                    default:
                        cartProducts = cart.Products.Where(x => x.BranchID == branchID && x.DeliveryTypeID == deliveryTypeID).ToList();
                        break;
                }

                transaction.TransactionDetails = new List<TransactionDetailDTO>();

                foreach (var product in cartProducts)
                {
                    var cartQuantity = 0M;
                    var length = 0;

                    if ((deliveryTypeID == Convert.ToInt32(DeliveryTypes.Email)) || (deliveryTypeID == Convert.ToInt32(DeliveryTypes.EmailInternationalDelivery)))
                    {
                        cartQuantity = 1;
                        length = (int)product.Quantity;
                    }
                    else
                    {
                        cartQuantity = product.Quantity;
                        length = 1;
                    }

                    cartAmount = cartAmount + Convert.ToDecimal(product.Total);

                    for (int i = 0; i < length; i++)
                    {
                        transaction.TransactionDetails.Add(new TransactionDetailDTO()
                        {
                            Amount = Convert.ToDecimal(product.DiscountedPrice) * cartQuantity, //product.DiscountedPrice.IsNullOrEmpty() ? Convert.ToDecimal(product.ProductPrice) : Convert.ToDecimal(product.DiscountedPrice),
                            CreatedDate = DateTime.Now.ToString(),
                            ProductID = product.ProductID,
                            ProductSKUMapID = product.SKUID,
                            Quantity = cartQuantity,
                            UnitPrice = Convert.ToDecimal(product.DiscountedPrice),
                            UpdatedDate = DateTime.Now.ToString(),

                        });
                    }
                }

                var deliveryTypes = cartProducts.Count>0?cartProducts[0].DeliveryTypes.Where(x => x.DeliveryMethodID == deliveryTypeID).ToList():null;
                deliveryTextCultureDTOList = deliveryTypes.IsNotNull() && deliveryTypes.Count > 0 ? deliveryTypes[0].CutOffDisplayTextCulture : new List<DeliveryTypeTimeSlotMapCultureDTO>();

                cartAmount = cartAmount + (decimal)transaction.TransactionHead.DeliveryCharge;

                if (cartAmount <= voucherAmount)
                {
                    transaction.TransactionHead.EntitlementID = (byte)Eduegate.Framework.Payment.EntitlementType.Voucher;
                }

                if (userRole.IsNotNull())
                {
                    transaction.TransactionHead.TransactionRole = (int)userRole;
                }
                transaction.TransactionHead.DeliveryDate = deliveryTypes.IsNotNull() && deliveryTypes.Count > 0 ? deliveryTypes[0].DeliveryDateTime:DateTime.Now;
                transaction = new ProductDetailBL(_context).SaveTransactions(transaction);
                transactionHeadID = transaction.TransactionHead.HeadIID;

                if (transaction.TransactionHead.HeadIID != default(long))
                {
                    //Map Transaction Head ID with Shopping Cart ID
                    var tranCartMap = new TransactionHeadShoppingCartMap()
                    {
                        ShoppingCartID = cart.ShoppingCartID,
                        TransactionHeadID = transaction.TransactionHead.HeadIID,
                        //DeliveryCharge = DeliveryCharge,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    };

                    new OrderRepository().SaveTransactionHeadCartMap(tranCartMap);

                    // Get Billing/Shipping address for this cart
                    // putting shipping as billing if no billing address
                    if (cart.BillingAddressID.IsNull() || cart.BillingAddressID == 0)
                    {
                        if (cart.ShippingAddressID.HasValue)
                            cart.BillingAddressID = cart.ShippingAddressID;
                        else
                            throw new Exception("Shipping & billing address not provided, cannot generate order!!");
                    }

                    var billingAddress = new ContactDTO() { ContactID = cart.BillingAddressID.Value, IsBillingAddress = true, IsShippingAddress = false, };
                    var shippingAddress = new ContactDTO() { ContactID = cart.ShippingAddressID.Value, IsShippingAddress = true, IsBillingAddress = false, };
                    //var shippingAddress = userServiceBL.GetContactDetail(cart.ShippingAddressID);

                    var contactList = new List<ContactDTO>() { billingAddress, shippingAddress };

                    // Add selected billing/shipping addresses to OrderContactMap
                    //var contactResult = 
                    SaveOrderAddresses(transactionHeadID, contactList);

                    //Update cart status as confirmed
                    new ShoppingCartBL(_context).UpdateCartStatus(cart.ShoppingCartID, ShoppingCartStatus.CheckedOut, paymentMethod, ShoppingCartStatus.PaymentInitiated);

                    //update vouchermap and voucher if voucher used with this cart
                    //if (cart.IsVoucherApplied)
                    //{
                    //    new VoucherRepository().UpdateVoucher(cart.ShoppingCartID, Eduegate.Framework.Helper.Enums.Status.Used, Convert.ToInt64(callContext.UserId));
                    //}

                    var orderRepository = new OrderRepository();
                    decimal totalAmount = 0;

                    //update data in TransactionHeadEntitlementMap
                    if (cartAmount > voucherAmount)
                    {
                        cartAmount = cartAmount - voucherAmount;
                        totalAmount = cartAmount;
                        orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, entitlementID, cartAmount);
                        //save in table 1 entries -- 1)entitlement(cartamount) 
                        if (voucherAmount != 0)
                        {
                            //save in table 1 entries   2) voucher (voucher amt)
                            orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Voucher, voucherAmount);
                        }

                        voucherAmount = 0;
                    }

                    if (cartAmount == voucherAmount)
                    {
                        //save in table 1 entry for voucher 
                        totalAmount = 0;
                        orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Voucher, voucherAmount);
                        voucherAmount = 0;
                    }

                    if (cartAmount < voucherAmount)
                    {
                        voucherAmount = voucherAmount - cartAmount;
                        totalAmount = 0;
                        orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Voucher, cartAmount);
                        //save in table 1 entry for voucher from cartamount
                    }

                    var loyaltyPointsValue = new SettingBL().GetSettingDetail(Constants.TransactionSettings.LOYALTYPOINTSVALUE, (long)_context.CompanyID).SettingValue;
                    var categorizaionPointsValue = new SettingBL().GetSettingDetail(Constants.TransactionSettings.CATEGORIZATIONPOINTSVALUE, (long)_context.CompanyID).SettingValue;
                    var isPointsUpdated = orderRepository.UpdateCustomerLoyaltyPoints(totalAmount, decimal.Parse(loyaltyPointsValue), decimal.Parse(categorizaionPointsValue), transactionHeadID, customerID);

                    if ((userRole == UserRole.Mobile_App_En || userRole == UserRole.Mobile_App_Ar) && !string.IsNullOrEmpty(deviceInfo))
                    {
                        var deviceInfoSplit = deviceInfo.Split('|');
                        var devicePlatform = deviceInfoSplit.Length > 0 ? deviceInfoSplit[0] : string.Empty;
                        var deviceVersion = deviceInfoSplit.Length > 1 ? deviceInfoSplit[1] : string.Empty;
                        var orderRoleInserted = orderRepository.InsertOrderRoleTracking(transactionHeadID, devicePlatform, deviceVersion);
                    }

                    // update orderid
                    switch (paymentMethod)
                    {
                        case "KNET":
                            new OrderRepository().UpdateOrderIdPaymentDetailsKnet(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId));
                            break;
                        case "MIGS":
                            new OrderRepository().UpdateOrderIdPaymentMasterVisa(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId), cartAmount);
                            break;
                        case "PAYPAL":
                            new OrderRepository().UpdateOrderIdPaymentPaypal(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId));
                            break;
                        case "THEFORT":
                            new OrderRepository().UpdateOrderIdPaymentTheFort(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId));
                            break;
                        default:
                            break;
                    }

                   var transactionRepository = new TransactionRepository();
                   List<OrderDeliveryDisplayHeadMap> deliveryHeadMapList = new List<OrderDeliveryDisplayHeadMap>();
                   foreach (var item in deliveryTextCultureDTOList)
                   {
                        var deliveryTextList = new OrderDeliveryDisplayHeadMap()
                        {
                           HeadID = transaction.TransactionHead.HeadIID,
                           CultureID = item.CultureID,
                            DeliveryDisplayText = item.CutOffDisplayText
                       };
                       deliveryHeadMapList.Add(deliveryTextList);
                   }
                   transactionRepository.UpdateOrderDeliveryTextHeadMap(deliveryHeadMapList);
                   transactionRepository.UpdateTransactionHead(new TransactionHead()
                    {
                        HeadIID = tranCartMap.TransactionHeadID.Value,
                        TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.New,
                        DocumentStatusID = (byte)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved,
                    });
                }
            }

            return transactionHeadID;
        }


        public DateTime TransactionHeadDueDateCalculation(int deliveryTypeID, int deliveryDays = 0)
        {
            DateTime dueDateTime = DateTime.Now;
            switch (deliveryTypeID)
            {
                case (int)DeliveryTypes.Email:
                    dueDateTime = DateTime.Now;
                    break;
                case (int)DeliveryTypes.NextDay:// This has to come from settings which has to be implemented for Kuwait
                    dueDateTime = dueDateTime.AddDays(1);
                    break;
                case (int)DeliveryTypes.Express:
                    dueDateTime = dueDateTime.AddHours(4);
                    break;
                case (int)DeliveryTypes.SuperExpress:
                    dueDateTime = dueDateTime.AddHours(2);
                    break;
                case (int)DeliveryTypes.nDaysDelivery:
                    dueDateTime = dueDateTime.AddDays(deliveryDays);
                    break;
                case (int)DeliveryTypes.StorePickup:// This has to come from settings which has to be implemented for Kuwait
                    dueDateTime = dueDateTime.AddDays(deliveryDays);
                    break;
            }
            return dueDateTime;
        }

        public bool SaveOrderAddresses(long orderID, List<ContactDTO> contacts, CallContext callContext = null)
        {
            var exit = false;

            if (contacts.Count > 0)
            {
                var contactList = new List<Contact>();
                foreach (var contact in contacts)
                {
                    var contactEntity = new UserServiceBL(_context).GetContactDetail(contact.ContactID);

                    if (contactEntity != null)
                    {
                        if (Convert.ToBoolean(contact.IsBillingAddress))
                        {
                            contactEntity.IsBillingAddress = true;
                            contactEntity.IsShippingAddress = false;
                        }
                        else
                        {
                            contactEntity.IsBillingAddress = false;
                            contactEntity.IsShippingAddress = true;
                        }
                        contactEntity.ContactIID = contact.ContactID;

                        contactList.Add(contactEntity);
                    }
                }

                exit = SaveOrderAddress(orderID, contactList);
            }

            return exit;
        }

        public List<ContactDTO> GetOrderContacts(long orderID)
        {
            return OrderContactMapToContactDTO(new OrderRepository().GetOrderContacts(orderID));
        }

        public bool UpdateOrderTrackingStatus(OrderTrackingDTO track, CallContext callContext)
        {
            // fill callcontext detail
            track.CreatedBy = Convert.ToInt32(callContext.LoginID);
            track.CreatedDate = DateTime.Now;

            // Save
            var status = new OrderRepository().UpdateOrderTrackingStatus(OrderTrackingToEntity(track));

            if (status)
            {
                var repository = new DistributionRepository();

                //Send Pickup request
                var settingsParameters = repository.GetServiceProviderSettings(1);
                settingsParameters.Add(new ServiceProviderSetting() { SettingCode = "ReadyByTime", SettingValue = DateTime.Now.ToString("hh:mm") });
                settingsParameters.Add(new ServiceProviderSetting() { SettingCode = "MessageReference", SettingValue = new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F").ToString().Replace("-", "") });
                settingsParameters.Add(new ServiceProviderSetting() { SettingCode = "MessageTime", SettingValue = DateTime.Now.ToString() });
                //var dhlClient = new DHLClient(ServiceProviderSettingMapper.Mapper(callContext).ToKeyValueDTO(settingsParameters));
                //dhlClient.SendRequest(dhlClient.CreateNewPickup());
                ////Update logs
                //repository.SaveServiceProviderLogs(new ServiceProviderLog() { });
            }

            return status;
        }

        public List<OrderTrackingDTO> GetOrderTrack(long orderID, CallContext callContext)
        {
            var trackList = new List<OrderTrackingDTO>();

            var entityList = new OrderRepository().GetOrderTrack(orderID);

            if (entityList.Count > 0)
            {
                foreach (var track in entityList)
                {
                    trackList.Add(OrderTrackingToDTO(track));
                }
            }

            return trackList;
        }
        #endregion

        public DeliveryChargeDTO GetDeliveryCharges(long contactID)
        {
            DeliveryCharge deliveryCharge = new OrderRepository().GetDeliveryCharges(contactID);

            if (deliveryCharge.IsNull())
            {
                return null;
            }
            else
            {
                return DeliveryChargeMapper.ToDeliveryChargeDTOMap(deliveryCharge);
            }

        }

        public TransactionHeadDTO GetTransactionDetails(long headID)
        {
            return TransactionHeadMapper.Mapper(_context).ToDTO(new OrderRepository().GetTransactionDetails(headID));
        }

        public long GetOrderIDFromOrderNo(string transactionNo)
        {
            return new OrderRepository().GetOrderIDFromOrderNo(transactionNo);
        }

        public bool SetTemporaryBranchTransfer(long shoppingCartID, bool isReverse = false)
        {
            bool isSuccess = false;

            try
            {
                var shoppingCartRepository = new ShoppingCartRepository();
                var cartDetails = shoppingCartRepository.GetCartDetailbyIID(shoppingCartID);

                if (cartDetails == null) return false;

                if (!cartDetails.IsInventoryBlocked.HasValue)
                {
                    //Nothing blocked and asking to reverse, it's nonsense
                    if (isReverse) return false;
                }
                else
                {
                    //if the inventory already blocked and asking agin for blocking nothing to do it
                    if (cartDetails.IsInventoryBlocked.Value && !isReverse) return true;

                    //if the inventory is not blocked and asking to reverse, nothing to do it
                    if (!cartDetails.IsInventoryBlocked.Value && isReverse) return true;
                }

                var cartItemList = shoppingCartRepository.ShoppingCartItems(shoppingCartID, Convert.ToInt32(ShoppingCartStatus.PaymentInitiated), 1, null);

                if (cartItemList != null && cartItemList.Rows.Count > 0)
                {
                    var settingDTO = new SettingBL().GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, _context.CompanyID.Value);
                    long sysBlockedBranchID = 0;
                    int sysBlockedDocID = 0;

                    if (settingDTO != null)
                    {
                        sysBlockedBranchID = Convert.ToInt64(settingDTO.SettingValue);
                    }

                    var settingBranchTransferDOC = new SettingBL().GetSettingDetail(Constants.TransactionSettings.BRANCHTRANSFERSYSBLOCKEDDOC, _context.CompanyID.Value);
                    //var settingBranchBlocked = new SettingBL().GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, _context.CompanyID.Value);

                    if (settingBranchTransferDOC != null)
                    {
                        sysBlockedDocID = Convert.ToInt32(settingBranchTransferDOC.SettingValue);
                    }

                    var marketplaceSettingDto = new SettingBL().GetSettingDetail(Constants.TransactionSettings.MARKETPLACESYSTEMBLOCKEDORDER, _context.CompanyID.Value);
                    var marketplaceBranchTransferDOC = new SettingBL().GetSettingDetail(Constants.TransactionSettings.MARKETPLACEBRANCHTRANSFERSYSBLOCKEDDOC, _context.CompanyID.Value);

                    DataView dv = new DataView(cartItemList);
                    var branchGroupList = dv.ToTable(true, "BranchID");
                    var transactionMapper = Mappers.Transactions.TransactionMapper.Mapper(_context);

                    if (branchGroupList != null && branchGroupList.Rows.Count > 0)
                    {
                        foreach (DataRow drRow in branchGroupList.Rows)
                        {
                            DataView dt = new DataView(dv.ToTable(), "BranchID='" + drRow["BranchID"].ToString() + "'", "", DataViewRowState.CurrentRows);

                            // check is it MarketPlace
                            int branchId = string.IsNullOrWhiteSpace(Convert.ToString(drRow["BranchID"]))
                                ? 0
                                : Convert.ToInt32(drRow["BranchID"]);
                            if (branchId > 0)
                            {
                                var branch = new ReferenceDataRepository().GetBranch(branchId, false);
                                if (branch.IsNotNull() && branch.IsMarketPlace == true)
                                {
                                    if (marketplaceSettingDto != null)
                                    {
                                        sysBlockedBranchID = Convert.ToInt64(marketplaceSettingDto.SettingValue);
                                    }
                                    if (marketplaceBranchTransferDOC != null)
                                    {
                                        sysBlockedDocID = Convert.ToInt32(marketplaceBranchTransferDOC.SettingValue);
                                    }
                                }
                            }

                            if (isReverse)
                            {
                                ImmediateBranchTransfer(dt, sysBlockedBranchID, Convert.ToInt64(drRow["BranchID"]), sysBlockedDocID, shoppingCartID, isReverse);
                            }
                            else
                            {
                                ImmediateBranchTransfer(dt, Convert.ToInt64(drRow["BranchID"]), sysBlockedBranchID, sysBlockedDocID, shoppingCartID, isReverse);
                            }
                        }
                    }
                }

                isSuccess = true;
            }
            catch { }

            return isSuccess;
        }

        private void ImmediateBranchTransfer(DataView dt, long fromBranchID, long toBranchID, int documentTypeID, long shoppingCartID, bool isReverse)
        {
            var transactionMapper = Mappers.Transactions.TransactionMapper.Mapper(_context);
            var shoppingCartRepository = new ShoppingCartRepository();
            var entity = transactionMapper.ToEntity(transactionMapper.ToDTOFromCartItem(dt.ToTable(),
                                    fromBranchID, toBranchID, documentTypeID), Services.Contracts.Enums.TransactionStatus.Immediate);
            //entity.TransactionNo = new MutualBL(_context).GetAndSaveNextTransactionNumber(Convert.ToInt32(entity.DocumentTypeID));
            var branchTransfer = shoppingCartRepository.BlockInventoryForShoppingCartItems(entity, shoppingCartID, !isReverse);

            if (branchTransfer != null)
            {
                //process transfer immediatly when it's blocked
                var dtos = new List<ProductInventoryDTO>();

                foreach (var detail in branchTransfer.TransactionDetails)
                {
                    dtos.Add(new ProductInventoryDTO()
                    {
                        BranchID = branchTransfer.BranchID,
                        ToBranchID = branchTransfer.ToBranchID,
                        CompanyID = branchTransfer.CompanyID,
                        Quantity = detail.Quantity,
                        ProductSKUMapID = detail.ProductSKUMapID.Value,
                        CostPrice = detail.Amount,
                        ReferenceTypes = Framework.Enums.DocumentReferenceTypes.BranchTransfer,
                    });
                }

                var transactionBL = new TransactionBL(_context);
                transactionBL.ProcessProductInventory(dtos);
                transactionBL.UpdateTransactionHead(new TransactionHeadDTO()
                {
                    HeadIID = branchTransfer.HeadIID,
                    TransactionStatusID = (int)Services.Contracts.Enums.TransactionStatus.Complete,
                    Description = "blocked for the customer by sytem",
                    DocumentStatusID = (int)DocumentStatuses.Completed,
                });
            }
        }

        #region PrivateMethods
        private bool SaveOrderAddress(long orderID, List<Contact> contacts)
        {
            var orderContactMapList = new List<OrderContactMap>();
            foreach (var contact in contacts)
            {
                var orderContactMap = ContactToOrderContactMap(contact, Convert.ToBoolean(contact.IsBillingAddress), Convert.ToBoolean(contact.IsShippingAddress));
                orderContactMap.OrderID = orderID;
                orderContactMapList.Add(orderContactMap);
            }
            return new OrderRepository().SaveOrderAddress(orderContactMapList);
        }


        private OrderContactMap ContactToOrderContactMap(Contact contact, bool isBilling, bool isShipping)
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
                Avenue = contact.Avenue,
                District = contact.District,
                LandMark = contact.LandMark,
                PassportIssueCountryID = contact.PassportIssueCountryID,
                PassportNumber = contact.PassportNumber,
                PostalCode = contact.PostalCode,
                State = contact.State,
                Street = contact.Street,
                AlternateEmailID1 = contact.AlternateEmailID1,
                AlternateEmailID2 = contact.AlternateEmailID2,
                IsBillingAddress = isBilling,
                IsShippingAddress = isShipping,
                TelephoneCode = contact.TelephoneCode,
                PhoneNo1 = contact.PhoneNo1,
                PhoneNo2 = contact.PhoneNo2,
                MobileNo1 = contact.MobileNo1,
                MobileNo2 = contact.MobileNo2,
                WebsiteURL1 = contact.WebsiteURL1,
                WebsiteURL2 = contact.WebsiteURL2,
                AreaID = contact.AreaID,
                CityID = contact.CityID,
                ContactID = contact.ContactIID
                //CreatedBy = contact.CreatedBy,
                //CreatedDate = DateTime.Now,
                //UpdatedDate = DateTime.Now,
                //UpdatedBy = contact.UpdatedBy,
            };
        }

        private List<ContactDTO> OrderContactMapToContactDTO(List<OrderContactMap> contacts)
        {
            var contactDTOList = new List<ContactDTO>();
            foreach (var contact in contacts)
            {

                //var countryDetail = new CountryMasterBL().GetCountryDetail(Convert.ToInt64(contact.CountryID));
                var countryDetail = new ReferenceDataBL(_context).GetCountryDetail(Convert.ToInt64(contact.CountryID), false);

                contactDTOList.Add(new ContactDTO()
                {
                    FirstName = contact.FirstName,
                    MiddleName = contact.MiddleName,
                    LastName = contact.LastName,
                    TitleID = contact.TitleID.IsNotNull() ? contact.TitleID : null,
                    Title = contact.TitleID.IsNotNull() ? new AccountBL(null).GetPropertyName(Convert.ToInt64(contact.TitleID)) : "",
                    AddressLine1 = contact.AddressLine1,
                    AddressLine2 = contact.AddressLine2,
                    AddressName = contact.AddressName,
                    Block = contact.Block,
                    BuildingNo = contact.BuildingNo,
                    City = contact.City,
                    CivilIDNumber = contact.CivilIDNumber,
                    CountryID = contact.CountryID,
                    CountryName = countryDetail.IsNotNull() ? countryDetail.CountryName : "",
                    Description = contact.Description,
                    Flat = contact.Flat,
                    Floor = contact.Floor,
                    Avenue = contact.Avenue,
                    PassportIssueCountryID = contact.PassportIssueCountryID,
                    PassportNumber = contact.PassportNumber,
                    PostalCode = contact.PostalCode,
                    State = contact.State,
                    Street = contact.Street,
                    AlternateEmailID1 = contact.AlternateEmailID1,
                    AlternateEmailID2 = contact.AlternateEmailID2,
                    IsBillingAddress = Convert.ToBoolean(contact.IsBillingAddress),
                    IsShippingAddress = Convert.ToBoolean(contact.IsShippingAddress),
                    TelephoneCode = contact.TelephoneCode,
                    PhoneNo1 = contact.PhoneNo1,
                    PhoneNo2 = contact.PhoneNo2,
                    MobileNo1 = contact.MobileNo1,
                    MobileNo2 = contact.MobileNo2,
                    AreaID = contact.AreaID
                    //CreatedBy = contact.CreatedBy,
                    //CreatedDate = DateTime.Now,
                    //UpdatedDate = DateTime.Now,
                    //UpdatedBy = contact.UpdatedBy,
                });
            }
            return contactDTOList;
        }

        private OrderTracking OrderTrackingToEntity(OrderTrackingDTO dto)
        {
            return new OrderTracking
            {
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                Description = dto.Description,
                OrderID = dto.OrderID,
                StatusDate = dto.StatusDate,
                StatusID = (byte)dto.TransactionStatus,
            };
        }

        private OrderTrackingDTO OrderTrackingToDTO(OrderTracking entity)
        {
            return new OrderTrackingDTO
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                OrderID = Convert.ToInt64(entity.OrderID),
                StatusDate = entity.StatusDate,
                TransactionStatus = (Services.Contracts.Enums.TransactionStatus)entity.StatusID,
            };
        }


        public OrderContactMapDTO SaveOrderContactMap(OrderContactMapDTO dtoOrderContactMap)
        {
            // Convert from dto to entity
            var orderContactMap = OrderContactMapMapper.Mapper(_context).ToEntity(dtoOrderContactMap);
            // call repo to save entity
            orderContactMap = new OrderRepository().SaveOrderContactMap(orderContactMap);
            // convert from entity to dto
            dtoOrderContactMap = OrderContactMapMapper.Mapper(_context).ToDTO(orderContactMap);
            return dtoOrderContactMap;
        }

        private Eduegate.Framework.Payment.EntitlementType GetEntitlementType(Eduegate.Framework.Payment.PaymentGatewayType paymentGatewayMethod)
        {
            Eduegate.Framework.Payment.EntitlementType entitlementID;
            switch (paymentGatewayMethod)
            {
                case Framework.Payment.PaymentGatewayType.KNET:
                    entitlementID = Framework.Payment.EntitlementType.Knet;
                    break;
                case Framework.Payment.PaymentGatewayType.MIGS:
                case Framework.Payment.PaymentGatewayType.THEFORT:
                    entitlementID = Framework.Payment.EntitlementType.Visa_Mastercard;
                    break;
                case Framework.Payment.PaymentGatewayType.PAYPAL:
                    entitlementID = Framework.Payment.EntitlementType.Paypal;
                    break;
                case Framework.Payment.PaymentGatewayType.COD:
                default:
                    entitlementID = Framework.Payment.EntitlementType.COD;
                    break;
                case Framework.Payment.PaymentGatewayType.WALLET:
                    entitlementID = Framework.Payment.EntitlementType.Wallet;
                    break;
                case Framework.Payment.PaymentGatewayType.VOUCHER:
                    entitlementID = Framework.Payment.EntitlementType.Voucher;
                    break;
            }
            return entitlementID;
        }
        #endregion

        public Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID)
        {
            return new OrderRepository().GetDeliveryType(headID);
        }

        public bool isOrderOfUser(long headID, long customerID)
        {
            return new OrderRepository().isOrderOfUser(headID, customerID);
        }

        public int GetActualOrderStatus(long headID)
        {
            return new UserServiceRepository().GetTransactionStatus(headID);
        }
        public KeyValueDTO SaveCancelReplaceReturnRequest(long headID, List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO> orderDetails)
        {

            var keyvalueDTO = new KeyValueDTO();
            var vContinue = false;
            var currentJobStatus = new UserServiceBL(this._context).GetJobStatusByHeadID(headID);
            if (currentJobStatus != default(int?) && currentJobStatus.HasValue)
            {
                var allowedCancelStatus = new SettingBL().GetSettingDetail(Constants.OrderCancelStatusSettings.ALLOWCANCELLATIONJOBSTATUS, this._context.CompanyID.Value);
                var allowedReturnStatus = new SettingBL().GetSettingDetail(Constants.OrderCancelStatusSettings.ALLOWRETURNJOBSTATUS, this._context.CompanyID.Value);
                var allowedCancelArray = allowedCancelStatus.SettingValue.Split(',');
                var allowedReturnArray = allowedReturnStatus.SettingValue.Split(',');
                if (allowedReturnArray.Contains(currentJobStatus.ToString()))
                {
                    if (orderDetails.Any(a => a.Action == (int)Framework.Helper.Enums.ReplacementActions.Cancellation))
                    {
                        keyvalueDTO.Key = "false";
                        keyvalueDTO.Value = ResourceHelper.GetValue("CannotCancelDispatched", this._context.LanguageCode);
                    }
                    else
                        vContinue = true;
                }
                else
                    vContinue = true;
            }
            else
            {
                keyvalueDTO.Key = "false";
                keyvalueDTO.Value = ResourceHelper.GetValue("ContcatCustomerSupport", this._context.LanguageCode);
            }
            if (vContinue)
            {
                var ChangeOrderTransaction = new TransactionDTO();
                ChangeOrderTransaction.TransactionDetails = new List<TransactionDetailDTO>();
                ChangeOrderTransaction.ShipmentDetails = new ShipmentDetailDTO();
                ChangeOrderTransaction.OrderContactMap = new OrderContactMapDTO();
                ChangeOrderTransaction.OrderContactMaps = new List<OrderContactMapDTO>();
                ChangeOrderTransaction.TransactionHeads = new List<TransactionHeadDTO>();

                var transaction = new TransactionBL(_context).GetTransaction(headID);

                ChangeOrderTransaction.TransactionHead = transaction.TransactionHead;

                ChangeOrderTransaction.TransactionHead.HeadIID = 0;
                ChangeOrderTransaction.TransactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.New;
                ChangeOrderTransaction.TransactionHead.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft;

                var documentTypeID = new SettingBL().GetSettingDetail(Eduegate.Framework.Helper.Constants.OrderCancelStatusSettings.WEBSITEORDERCHANGEREQUEST, this._context.CompanyID.Value);
                ChangeOrderTransaction.TransactionHead.DocumentTypeID = int.Parse(documentTypeID.SettingValue);
                ChangeOrderTransaction.TransactionHead.TransactionNo = new MutualBL(this._context).GetNextTransactionNumber(int.Parse(documentTypeID.SettingValue));
                ChangeOrderTransaction.TransactionHead.TransactionDate = DateTime.Now;
                ChangeOrderTransaction.TransactionHead.JobEntryHeadID = default(long?);
                ChangeOrderTransaction.TransactionHead.JobStatusID = default(int?);
                //ChangeOrderTransaction.TransactionHead.BranchID = tra;
                ChangeOrderTransaction.TransactionDetails = new List<TransactionDetailDTO>();
                ChangeOrderTransaction.TransactionHead.ReferenceHeadID = headID;
                //ChangeOrderTransaction.TransactionHead.UpdatedBy = int.Parse(_context.UserId);
                //ChangeOrderTransaction.TransactionHead.UpdatedDate = DateTime.Now.ToString();
                ChangeOrderTransaction.TransactionHead.CreatedBy = int.Parse(_context.UserId);
                ChangeOrderTransaction.TransactionHead.CreatedDate = DateTime.Now.ToString();
                ChangeOrderTransaction.IgnoreEntitlementCheck = true;
                foreach (var item in orderDetails)
                {
                    if (item.Action.HasValue)
                    {
                        //item.Action = (int)Framework.Helper.Enums.ReplacementActions.NoAction;

                    var transDetails = transaction.TransactionDetails.Where(x => x.DetailIID == item.DetailIID).FirstOrDefault();
                    transDetails.Action = item.Action;
                    transDetails.Quantity = item.Quantity;
                    transDetails.HeadID = 0;
                    transDetails.ParentDetailID = transDetails.DetailIID;
                    transDetails.DetailIID = 0;
                    ChangeOrderTransaction.TransactionDetails.Add(transDetails);
                    }

                }

                ChangeOrderTransaction.OrderContactMap = transaction.OrderContactMap;
                ChangeOrderTransaction.OrderContactMaps = transaction.OrderContactMaps;
                ChangeOrderTransaction.ShipmentDetails = transaction.ShipmentDetails;

                var saved = new TransactionBL(_context).SaveTransactions(ChangeOrderTransaction);
                if (saved.TransactionHead.IsError == true)
                {
                    keyvalueDTO.Key = "false";
                    keyvalueDTO.Value = ResourceHelper.GetValue("ContcatCustomerSupport", this._context.LanguageCode);
                }
                else
                {
                    keyvalueDTO.Key = "true";
                    keyvalueDTO.Value = ResourceHelper.GetValue("RequestSuccess", this._context.LanguageCode);
                }

                
            }
           
            return keyvalueDTO;
        }
        private bool IsOrderDispateched(TransactionDTO transaction)
        {
            if (transaction.TransactionHead.JobStatusID == (int)Eduegate.Services.Contracts.Enums.JobStatuses.Assigned && transaction.TransactionHead.JobStatusID != (int)Eduegate.Services.Contracts.Enums.JobStatuses.Delivered)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsOrderDelivered(TransactionDTO transaction)
        {
            if (transaction.TransactionHead.JobStatusID == (int)Eduegate.Services.Contracts.Enums.JobStatuses.Delivered)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCancellationRequest(EditOrderDetailDTO edit)
        {
            if (edit.Action == Services.Contracts.Enums.ReplacementActions.Cancellation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsReturnOrReplaceRequest(EditOrderDetailDTO edit)
        {
            if (edit.Action == Services.Contracts.Enums.ReplacementActions.Return || edit.Action == Services.Contracts.Enums.ReplacementActions.Replace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
