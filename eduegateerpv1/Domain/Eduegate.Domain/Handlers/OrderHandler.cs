using Eduegate.Domain.Helpers;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Domain.Mappers.Common;
using Eduegate.Domain.Handlers;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Handlers
{
    public class OrderHandler
    {
        CallContext _callContext;
        TransactionHandler _transactionHandler;
        ShoppingCartHelper _cartHelpers;
        SettingMapper _settingMapper;
        //PaymentHandler _paymentHandler;
        private static ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();

        public static OrderHandler Handler(CallContext _callContext, ShoppingCartHelper _cartHelpers)
        {
            var handler = new OrderHandler();
            handler._callContext = _callContext;
            handler._transactionHandler = TransactionHandler.Handler(_callContext);
            handler._cartHelpers = _cartHelpers;
            handler._settingMapper = SettingMapper.Mapper();
            //handler._paymentHandler = PaymentHandler.Handler(_callContext);
            return handler;
        }

        public List<TransactionDTO> OrderGenerationByBranches(CartDTO cart,
            string paymentMethod, long trackID, UserRole? userRole = null, string deviceInfo = "",
            long? customerID = (int?)null, decimal deliveryCharge = -1, CheckoutPaymentDTO paymentDTO = null)
        {
            var transactionDTO = new List<TransactionDTO>();
            var cartProductsBranchIDs = cart.Products.Select(x => x.BranchID).Distinct();

            bool isSetDeliveryCharge = false;
            // get marketplace branch Id
            var marketplaceSettingDto = _cartHelpers.GetSettingValue<long>(Constants.TransactionSettings.MARKETPLACESALES,
                _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(long), 0);
            var settingSysBlock = _cartHelpers.GetSettingValue<long>(Constants.TransactionSettings.SYSBLOCKEDBRANCH,
                _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(long), 0);
            var settingOnlineBranch = _cartHelpers.GetSettingValue<long>(Constants.TransactionSettings.ONLINEBRANCHID,
                _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(long), 0);
            int documentTypeId = 0;

            var voucherMap = new Eduegate.Domain.Repository.ShoppingCartRepository().GetVoucherMap(cart.ShoppingCartID);
            decimal voucherAmount = 0;

            if (voucherMap.IsNotNull())
            {
                voucherAmount = (decimal)voucherMap.Amount;
            }

            foreach (var branchID in cartProductsBranchIDs)
            {
                var cartProductsBranchIDDeliveryTypeID = (from o in cart.Products
                                                          select new
                                                          {
                                                              o.DeliveryTypeID,
                                                              o.BranchID,
                                                              o.DeliveryPriority,
                                                              DeliveryDays = o.DeliveryDays.HasValue ? o.DeliveryDays.Value : 0,
                                                          }).Where(o => o.BranchID == branchID)
                                                            .Distinct()
                                                            .OrderBy(x => x.DeliveryPriority);
                long? settingBranch = settingOnlineBranch;
                //// check is it Market Place
                //if (branchID > 0)
                //{
                //    var branch = new Eduegate.Domain.Repository.ReferenceDataRepository().GetBranch(branchID, false);

                //    if (branch.IsNotNull() && branch.IsMarketPlace == true)
                //    {
                //        if (marketplaceSettingDto != 0)
                //        {
                //            settingBranch = marketplaceSettingDto;
                //        }
                //        else
                //        {
                //            settingBranch = settingSysBlock;
                //        }
                //    }
                //    else
                //    {
                //        settingBranch = settingSysBlock;
                //    }
                //}
                //else
                //{
                //    settingBranch = settingSysBlock;
                //}

                foreach (var deliveryType in cartProductsBranchIDDeliveryTypeID)
                {
                    int deliveryDays = 0;
                    isSetDeliveryCharge = false;
                    switch (deliveryType.DeliveryTypeID)
                    {
                        case (int)DeliveryTypes.nDaysDelivery:
                            {
                                deliveryDays = deliveryType.DeliveryDays;
                                OrderSettings(settingOnlineBranch, deliveryType.DeliveryTypeID, branchID, ref documentTypeId, ref cart, ref isSetDeliveryCharge);
                                var transaction = OrderGeneration(paymentMethod, trackID, _callContext, cart,
                                        branchID, deliveryType.DeliveryTypeID,
                                        documentTypeId, isSetDeliveryCharge, ref voucherAmount, settingBranch,
                                        deliveryDays, userRole, deviceInfo, customerID, deliveryCharge, paymentDTO);
                                transactionDTO.Add(transaction);
                                break;
                            }
                        default:
                            {
                                deliveryDays = 0;
                                OrderSettings(settingOnlineBranch, deliveryType.DeliveryTypeID, branchID, ref documentTypeId, ref cart, ref isSetDeliveryCharge);
                                var transaction = OrderGeneration(paymentMethod, trackID, _callContext, cart,
                                        branchID, deliveryType.DeliveryTypeID,
                                        documentTypeId, isSetDeliveryCharge, ref voucherAmount,
                                        settingBranch, deliveryDays, userRole, deviceInfo, customerID, deliveryCharge, paymentDTO);
                                transactionDTO.Add(transaction);
                                break;
                            }
                    }
                }
            }

            return transactionDTO;
        }

        public TransactionDTO OrderGeneration(string paymentMethod, long trackID, CallContext callContext, CartDTO cart, long branchID,
           int deliveryTypeID, int documentTypeID, bool isSetDeliveryCharge, ref decimal voucherAmount, long? sysBlockedBranchID = 0,
           int deliveryDays = 0, UserRole? userRole = null, string deviceInfo = "",
           long? CustomerID = (int?)null, decimal erpDeliveryCharge = -1, CheckoutPaymentDTO paymentDTO = null)
        {
            long transactionHeadID = default(long);
            var transaction = new TransactionDTO();
            var settingBL = new Domain.Setting.SettingBL(callContext);

            if (cart.IsNotNull())
            {
                var customerID = CustomerID > 0 ? CustomerID : (int?)null;//Convert.ToInt64(new UtilityBL().GetCustomerID(callContext));
                decimal cartAmount = 0;
                var supplier = new Eduegate.Domain.Repository.ReferenceDataRepository().GetSupplierByBranchnID(branchID);

                //Populate TransactionHead and TransactionDetail entries from ShoppingCart and ShoppingCartItems respectively
                //Create a TransactionHead and generate HeadID, thi OrderID 
                transaction.TransactionHead = new TransactionHeadDTO();
                var parameterTransactionList = new List<KeyValueParameterDTO>();

                parameterTransactionList.Add(new KeyValueParameterDTO
                {
                    ParameterName = "CARTID",
                    ParameterValue = Convert.ToString(cart.ShoppingCartID),
                });

                transaction.TransactionHead.TransactionNo = _transactionHandler
                    .GetAndSaveNextTransactionNumber(documentTypeID, parameterTransactionList);
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(documentTypeID);
                transaction.TransactionHead.CustomerID = customerID;//Convert.ToInt64(new UtilityBL().GetCustomerID(callContext));
                transaction.TransactionHead.SupplierID = supplier != null ? supplier.SupplierIID : (long?)null;
                transaction.TransactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess;
                transaction.TransactionHead.DocumentStatusID = (short)DocumentStatuses.Draft;
                // Hardcoded a check for KD, we can avoid this after currency dropdown correction
                transaction.TransactionHead.CurrencyID = settingBL.GetSettingValue<int>("DEFAULTCURRENCYID");

                transaction.TransactionHead.BranchID = sysBlockedBranchID;
                transaction.TransactionHead.DiscountAmount = cart.Discount;
                Eduegate.Framework.Payment.PaymentGatewayType paymentGatewayMethod;
                Enum.TryParse(paymentMethod, out paymentGatewayMethod);
                var entitlementID = _transactionHandler.GetEntitlementType(paymentGatewayMethod);

                transaction.TransactionHead.EntitlementID = byte.Parse(entitlementID);
                //var companyDetails = new ReferenceDataBL(null).GetCompanyByCountryID(1);
                transaction.TransactionHead.CompanyID = callContext.CompanyID;
                transaction.TransactionHead.DeliveryTypeID = deliveryTypeID;
                transaction.TransactionHead.DeliveryTimeslotID = cart.DeliveryTimeslotID;
                transaction.TransactionHead.StudentID = cart.StudentID;
                transaction.TransactionHead.SchoolID = cart.SchoolID;
                transaction.TransactionHead.AcademicYearID = cart.AcademicYearID;
                transaction.TransactionHead.UpdatedBy = settingBL.GetSettingValue<int>("USERPORTALONLINE");
                transaction.TransactionHead.CreatedBy = settingBL.GetSettingValue<int>("USERPORTALONLINE");
                //if (paymentMethod == "COD")


                if (transaction.TransactionHead.DocumentTypeID == (int)DocumentTypes.SO4DigitalOnline)
                {
                    transaction.TransactionHead.DeliveryMethodID = (short)DeliveryMethods.Virtual;
                }
                else
                {
                    transaction.TransactionHead.DeliveryMethodID = (short)DeliveryMethods.Physical;
                }

                transaction.TransactionHead.DeliveryDays = deliveryDays;

                decimal? deliveryCharge;

                if (isSetDeliveryCharge)
                {
                    if (erpDeliveryCharge != -1)
                    {
                        deliveryCharge = erpDeliveryCharge;
                    }
                    else
                    {
                        deliveryCharge = cart.DeliveryCharge;
                    }
                }
                else
                {
                    deliveryCharge = 0;
                }

                transaction.TransactionHead.DeliveryCharge = deliveryCharge;

                //TODO: setting BranchID, ToBranchID, DiscountPercentage, DiscountAmount, IsShipment, JobEntryHeadID, DueDate, DeliveryDate, in transaction table
                List<CartProductDTO> cartProducts = null;
                List<DeliveryTypeTimeSlotMapCultureDTO> deliveryTextCultureDTOList = null;
                switch (deliveryTypeID)
                {
                    case (int)DeliveryTypes.nDaysDelivery:
                        cartProducts = cart.Products
                            .Where(x => x.BranchID == branchID && x.DeliveryTypeID == deliveryTypeID && x.DeliveryDays == deliveryDays)
                            .ToList();
                        break;
                    default:
                        cartProducts = cart.Products
                            .Where(x => x.BranchID == branchID && x.DeliveryTypeID == deliveryTypeID)
                            .ToList();
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

                    long skumapid = product.SKUID;
                    var sellingUnit = _transactionHandler.GetSellingUnit(skumapid);

                    for (int i = 0; i < length; i++)
                    {
                        transaction.TransactionDetails.Add(new TransactionDetailDTO()
                        {
                            Amount = Convert.ToDecimal(product.Price) * cartQuantity, //product.DiscountedPrice.IsNullOrEmpty() ? Convert.ToDecimal(product.ProductPrice) : Convert.ToDecimal(product.DiscountedPrice),
                            CreatedDate = DateTime.UtcNow.ToString(),
                            ProductID = product.ProductID,
                            ProductSKUMapID = product.SKUID,
                            Quantity = cartQuantity,
                            ActualUnitPrice = product.Price,
                            UnitPrice = Convert.ToDecimal(product.Price),
                            UpdatedDate = DateTime.Now.ToString(),
                            DiscountAmount = (Convert.ToDecimal(product.Price) * cartQuantity) - (product.ProductDiscountPrice * cartQuantity),
                            DiscountPercentage = product.DiscountPercentage,
                            CartItemID = product.CartItemID,
                            //TaxAmount1 = product.Tax,
                            UnitID = sellingUnit,
                            UpdatedBy = settingBL.GetSettingValue<int>("USERPORTALONLINE"),
                            CreatedBy = settingBL.GetSettingValue<int>("USERPORTALONLINE")
                        });

                        //cartAmount += Convert.ToDecimal(product.Price) * cartQuantity;
                    }
                }

                var deliveryTypes = cartProducts.Count > 0 && cartProducts[0].DeliveryTypes != null
                    ? cartProducts[0].DeliveryTypes.Where(x => x.DeliveryMethodID == deliveryTypeID).ToList() : null;
                deliveryTextCultureDTOList = deliveryTypes.IsNotNull() && deliveryTypes.Count > 0 ? deliveryTypes[0].CutOffDisplayTextCulture : new List<DeliveryTypeTimeSlotMapCultureDTO>();

                cartAmount = cartAmount + (!transaction.TransactionHead.DeliveryCharge.HasValue ? 0 :
                    transaction.TransactionHead.DeliveryCharge.Value);

                var entitlementmaps = new TransactionHeadEntitlementMapDTO();

                if (transaction.TransactionHead != null)
                {
                    transaction.TransactionHeadEntitlementMap = new TransactionHeadEntitlementMapDTO();

                    transaction.TransactionHeadEntitlementMap.EntitlementID = byte.Parse(entitlementID);
                    transaction.TransactionHeadEntitlementMap.Amount = cartAmount;
                    transaction.TransactionHeadEntitlementMap.PaymentTrackID = paymentDTO.PaymentTrackID;
                    transaction.TransactionHeadEntitlementMap.PaymentTransactionNumber = paymentDTO.PaymentTransactionNumber;
                }

                // UPDATING THE TRANSACTION AMOUNT BASED ON PAYMENT METHODS
                switch (paymentMethod)
                {
                    case "MIGS":
                        transaction.TransactionHead.PaidAmount = cartAmount;
                        break;
                    case "COD":
                        transaction.TransactionHead.PaidAmount = cartAmount;
                        break;
                    case "QPAY":
                        transaction.TransactionHead.PaidAmount = cartAmount;
                        break;
                }

                if (cartAmount <= voucherAmount && voucherAmount > 0)
                {
                    transaction.TransactionHead.EntitlementID = (byte)Eduegate.Framework.Payment.EntitlementType.Voucher;
                }

                if (userRole.IsNotNull())
                {
                    transaction.TransactionHead.TransactionRole = (int)userRole;
                }

                transaction.TransactionHead.DeliveryDate = deliveryTypes.IsNotNull() && deliveryTypes.Count > 0 ? deliveryTypes[0].DeliveryDateTime : DateTime.Now;
                transaction = TransactionHandler.Handler(_callContext).SaveTransactions(transaction);
                transactionHeadID = transaction.TransactionHead.HeadIID;

                if (transaction.TransactionHead.HeadIID != default(long))
                {
                    //Map Transaction Head ID with Shopping Cart ID
                    var tranCartMap = new TransactionHeadShoppingCartMap()
                    {
                        ShoppingCartID = cart.ShoppingCartID,
                        TransactionHeadID = transaction.TransactionHead.HeadIID,
                        //DeliveryCharge = DeliveryCharge,
                        CreatedDate = DateTime.UtcNow,
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

                    var contactList = new List<ContactDTO>() { billingAddress, shippingAddress };

                    // Add selected billing/shipping addresses to OrderContactMap
                    //var contactResult = 
                    SaveOrderAddresses(transactionHeadID, contactList);

                    //Update cart status as confirmed
                    new Eduegate.Domain.Repository.ShoppingCartRepository().UpdateCartStatus(cart.ShoppingCartID, ShoppingCartStatus.CheckedOut,
                         _cartHelpers.GetSiteID(_callContext), paymentMethod, ShoppingCartStatus.PaymentInitiated);

                    var orderRepository = new OrderRepository();

                    //var remainingAmount = _paymentHandler.HandleVoucherAmount(transactionHeadID, cartAmount, voucherAmount);
                    //remainingAmount = _paymentHandler.HandleRedeemLoyalty(transactionHeadID, remainingAmount, paymentDTO.LoyaltyAmount);

                    //if (remainingAmount > 0)
                    //{
                    //    orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, entitlementID, remainingAmount);
                    //}

                    //var loyaltyPointsValue = _cartHelpers.GetSettingValue<decimal?>(Constants.TransactionSettings.LOYALTYPOINTSVALUE, (long)_callContext.CompanyID, (decimal?)null);
                    //var categorizaionPointsValue = _cartHelpers.GetSettingValue<decimal?>(Constants.TransactionSettings.CATEGORIZATIONPOINTSVALUE, (long)_callContext.CompanyID, (decimal?)null);

                    //if (loyaltyPointsValue.HasValue || categorizaionPointsValue.HasValue)
                    //{
                    //    orderRepository.UpdateCustomerLoyaltyPoints(remainingAmount, loyaltyPointsValue.Value, categorizaionPointsValue.Value,
                    //        transactionHeadID, customerID);
                    //}

                    //if ((userRole == UserRole.Mobile_App_En || userRole == UserRole.Mobile_App_Ar) && !string.IsNullOrEmpty(deviceInfo))
                    //{
                    //    var deviceInfoSplit = deviceInfo.Split('|');
                    //    var devicePlatform = deviceInfoSplit.Length > 0 ? deviceInfoSplit[0] : string.Empty;
                    //    var deviceVersion = deviceInfoSplit.Length > 1 ? deviceInfoSplit[1] : string.Empty;
                    //    var orderRoleInserted = orderRepository.InsertOrderRoleTracking(transactionHeadID, devicePlatform, deviceVersion);
                    //}

                    // update orderid
                    switch (paymentMethod)
                    {
                        case "KNET":
                            orderRepository.UpdateOrderIdPaymentDetailsKnet(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId));
                            break;
                        case "MIGS":
                            orderRepository.UpdateOrderIdPaymentMasterVisa(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.LoginID), cartAmount);
                            transaction.TransactionHead.PaidAmount = cartAmount;
                            break;
                        case "PAYPAL":
                            orderRepository.UpdateOrderIdPaymentPaypal(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId));
                            break;
                        case "THEFORT":
                            orderRepository.UpdateOrderIdPaymentTheFort(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.UserId));
                            break;
                        case "QPAY":
                            orderRepository.UpdateOrderIdPaymentMasterVisa(trackID, (long)tranCartMap.TransactionHeadID, Convert.ToInt64(callContext.LoginID), cartAmount);
                            transaction.TransactionHead.PaidAmount = cartAmount;
                            break;
                        default:
                            break;
                    }

                    var transactionRepository = new TransactionRepository();
                    var deliveryHeadMapList = new List<OrderDeliveryDisplayHeadMap>();

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
                    //transactionRepository.UpdatePaidAmount(tranCartMap.TransactionHeadID.Value);
                }
            }

            return transaction;
        }

        public bool SaveOrderAddresses(long orderID, List<ContactDTO> contacts, CallContext callContext = null)
        {
            var exit = false;

            if (contacts.Count > 0)
            {
                var contactList = new List<Contact>();
                foreach (var contact in contacts)
                {
                    var accountRepo = new AccountRepository();
                    var contactEntity = accountRepo.GetContactDetail(contact.ContactID);

                    if (contactEntity != null)
                    {
                        if (contactEntity.AreaID == 0)
                        {
                            contactEntity.AreaID = (int?)null;
                        }

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

        private bool SaveOrderAddress(long orderID, List<Contact> contacts)
        {
            var orderContactMapList = new List<OrderContactMap>();
            foreach (var contact in contacts)
            {
                var orderContactMap = ContactToOrderContactMap(contact,
                    Convert.ToBoolean(contact.IsBillingAddress),
                    Convert.ToBoolean(contact.IsShippingAddress));
                orderContactMap.OrderID = orderID;
                orderContactMapList.Add(orderContactMap);
            }

            return new Eduegate.Domain.Repository.OrderRepository()
                .SaveOrderAddress(orderContactMapList);
        }

        private OrderContactMap ContactToOrderContactMap(Contact contact,
            bool isBilling, bool isShipping)
        {
            return new OrderContactMap()
            {

                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                TitleID = contact.TitleID.HasValue ?
                (short?)short.Parse(contact.TitleID.ToString())
                : null,
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
                ContactID = contact.ContactIID,
                Longitude = contact.Longitude,
                Latitude = contact.Latitude,
                LocationID = contact.LocationID,
            };
        }

        public void OrderSettings(long? settingOnlineBranch, int deliveryTypeID, long branchID,
            ref int documentTypeId, ref CartDTO cart, ref bool isSetDeliveryCharge)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var newAddedCartItem = shoppingCartRepository.GetCartDetailsWithItems(cart.CustomerID.ToString(), (int)ShoppingCartStatus.PaymentInitiated, null);

                var CanteenSOID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SALES_ORDER_EXTERNAL");
                var canteenCategoryID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CANTEEN_CATEGORY_ID");

                if (deliveryTypeID == (int)DeliveryTypes.Email || deliveryTypeID == (int)DeliveryTypes.EmailInternationalDelivery)
                {
                    if (branchID == settingOnlineBranch)
                    {
                        documentTypeId = _cartHelpers.GetSettingValue(Constants.TransactionSettings.ONLINESALESDIGITALDOCTTYPEID,
                            _callContext.CompanyID.Value, 0);
                    }
                    else
                    {
                        documentTypeId = _cartHelpers.GetSettingValue(Constants.TransactionSettings.ONLINESALESMARDIGITALDOCTTYPEID,
                            _callContext.CompanyID.Value, 0);
                    }
                }
                else
                {
                    if (branchID == settingOnlineBranch)
                    {
                        if (newAddedCartItem.CategoryIID == long.Parse(canteenCategoryID))
                        {
                            documentTypeId = int.Parse(CanteenSOID);
                        }
                        else 
                        {
                            documentTypeId = _cartHelpers.GetSettingValue(Constants.TransactionSettings.ONLINESALESDOCTTYPEID,
                                _callContext.CompanyID.Value, 0);
                        }
                    }
                    else
                    {
                        documentTypeId = _cartHelpers.GetSettingValue(Constants.TransactionSettings.ONLINESALESMARDOCTTYPEID,
                            _callContext.CompanyID.Value, 0);
                    }

                    if (cart.IsDeliveryChargeAssigned == false && cart.IsOnlineBranchPhysicalCartItems == true)
                    {
                        isSetDeliveryCharge = true;
                        cart.IsDeliveryChargeAssigned = true;
                    }
                }
            }
        }

    }
}
