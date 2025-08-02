using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper.Enums;
using Newtonsoft.Json;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.ShoppingCart;
using System.Data;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Globalization;
using Eduegate.Framework.Security;
using Eduegate.Domain.Mappers.ShoppingCartMapper;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Payment;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Domain.Mappers.Payments;
using Eduegate.Domain.Security;

namespace Eduegate.Domain
{
    public class ShoppingCartBL
    {
        private static ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
        private CallContext _callContext;

        public ShoppingCartBL(CallContext callContext)
        {
            _callContext = callContext;
        }

        public string GetLoggedInUserCartID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            var cart = shoppingCartRepository.GetCartDetail(_callContext.UserId, (int)status,GetSiteID(_callContext));

            if (cart != null) return cart.CartID;
            else return string.Empty;
        }

        public long GetLoggedInUserCartIID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            var cart = shoppingCartRepository.GetCartDetail(_callContext.UserId, (int)status, GetSiteID(_callContext));

            if (cart != null) return cart.ShoppingCartIID;
            else return default(long);
        }

        #region ShoppingCart Methods
        public CartDTO GetCartDetailbyIID(long cartIID)
        {
            var cartDetail = new ShoppingCartRepository().GetCartDetailbyIID(cartIID);
            return CartMapper.Mapper(_callContext).ToDTO(cartDetail);
        }

        public CartDTO GetCart(CallContext contextualInformation, long TransactionHeadId = 0,
            bool withCurrencyConversion = true, bool isDeliveryCharge = false, bool isValidation = true, ShoppingCartStatus cartStatus = ShoppingCartStatus.InProcess, long ShoppingCartIID = 0, long? customerIID = null)
        {
            customerIID = customerIID = customerIID.HasValue && customerIID == 0 ? null : customerIID; 
            int cultureID = (int)1;
            if (!_callContext.IsNull() && _callContext.LanguageCode != null && _callContext.LanguageCode.Trim() != string.Empty)
            {
                //var languageDTO = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode);
                //cultureID = languageDTO.IsNotNull() ? languageDTO.CultureID : 1;
            }

            decimal ConversionRate = 1; // Conversion rate 1 if withCurrencyConversion = false
            if (withCurrencyConversion && _callContext.IsNotNull())
                ConversionRate = (decimal)UtilityRepository.GetExchangeRate(_callContext.CompanyID, _callContext.CurrencyCode);


            bool isCartItemDeleted = false;
            bool isCartItemOutOfStock = false;
            bool isCartItemQuantityAdjusted = false;
            bool isOnlineBranchPhysicalCartItems = false;

            //var settingDTO = new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINEBRANCHID, (long)_callContext.CompanyID);
            var customerID = customerIID.HasValue ? customerIID.ToString() : GetCustomerID(contextualInformation);


            var cartWithItems = new CartDTO();
            cartWithItems.IsProceedToPayment = true;
            // base price without conversion 
            decimal baseCartTotal = 0;

            var cart = new ShoppingCart();
            var items = new List<CartProductDTO>();

            if (TransactionHeadId.IsNotNull() && TransactionHeadId > 0)
            { 
                cart = shoppingCartRepository.GetCartDetail(customerID, TransactionHeadId, (int)ShoppingCartStatus.CheckedOut);
                items = shoppingCartRepository.GetCartItems(customerID, cart.ShoppingCartIID, ShoppingCartStatus.CheckedOut, cultureID, _callContext);
            }
            else 
            {
                cart = ShoppingCartIID == 0 ? shoppingCartRepository.GetCartDetail(customerID, (int)cartStatus,GetSiteID(_callContext)) : shoppingCartRepository.GetCartDetailbyIID(ShoppingCartIID);
                if (cart.IsNotNull()) { items = shoppingCartRepository.GetCartItems(customerID, cart.ShoppingCartIID, cartStatus, cultureID, _callContext); }
            }
           
            var skuIDStrg = "";

            foreach (var item in items)
            {
                skuIDStrg += item.SKUID.ToString() + ",";
            }

            if (skuIDStrg.Length > 0)
            {
                skuIDStrg = skuIDStrg.Remove(skuIDStrg.Length - 1, 1);
            }


            if (!customerIID.HasValue && !string.IsNullOrEmpty(contextualInformation.UserId) && Convert.ToInt64(contextualInformation.UserId) > 0)
            {
                customerIID = Convert.ToInt64(contextualInformation.UserId);
            }

            var temp = contextualInformation.CurrencyCode;
            contextualInformation.CurrencyCode = contextualInformation.CurrencyCode.IsNotNullOrEmpty() ? contextualInformation.CurrencyCode : new Domain.Setting.SettingBL().GetSettingValue<string>("CurrencyCode");
            var productInventoryList = new ProductDetailBL(_callContext).GetProductInventoryOnline(skuIDStrg, customerIID);
            contextualInformation.CurrencyCode = temp;

            if (customerIID.HasValue && customerIID > 0)
            {
                //comented as erp has to be updated
                foreach (var item in items)
                {
                    var productSkuActive = new ProductBL(null).ProductSkuIDActiveCheck(item.SKUID);
                    if (isValidation && (byte)ProductStatuses.Active != productSkuActive)
                    {
                        var isDeleted = RemoveItem(item.SKUID, contextualInformation, customerIID);
                        if (isCartItemDeleted == false && isDeleted == true)
                        {
                            isCartItemDeleted = true;
                            cartWithItems.IsCartItemDeleted = true;
                        }
                    }
                }
            }

            if (TransactionHeadId.IsNotNull() && TransactionHeadId > 0)
            {
                if (isCartItemDeleted) { items = shoppingCartRepository.GetCartItems(customerID, cart.ShoppingCartIID, ShoppingCartStatus.CheckedOut, cultureID, _callContext); }

                foreach (var item in items)
                {
                    var productInventory = (from productInventoryDetails in productInventoryList
                                            where productInventoryDetails.ProductSKUMapID == item.SKUID
                                            select productInventoryDetails).FirstOrDefault();

                    if (customerIID.HasValue && customerIID > 0)
                    {
                        if (isValidation && productInventory.Quantity == 0)
                        {
                            var isDeleted = RemoveItem(item.SKUID, contextualInformation, customerIID);
                            if (isDeleted && isCartItemOutOfStock == false)
                            {
                                item.Quantity = 0;
                                item.IsOutOfStock = true;
                                isCartItemOutOfStock = true;
                            }
                            else if (isDeleted && isCartItemOutOfStock == true)
                            {
                                item.Quantity = 0;
                                item.IsOutOfStock = true;
                            }
                        }

                        if (item.IsOutOfStock == false)
                        {
                            DataTable dt = shoppingCartRepository.CheckOutCartQuantityVerification(item.SKUID, customerIID.Value, (Int32)item.Quantity, (Int32)productInventory.Quantity);
                            if (isValidation && dt != null && dt.Rows.Count > 0 && item.Quantity != Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]))
                            {
                                bool isQtyUpdated = false;
                                if (Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]) == 0)
                                {
                                    isQtyUpdated = RemoveItem(item.SKUID, contextualInformation, customerIID);
                                }
                                else
                                {
                                    isQtyUpdated = shoppingCartRepository.UpdateItem(item.SKUID, Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]), customerIID.ToString(), item.BranchID, GetSiteID(_callContext));
                                }

                                if (isQtyUpdated)
                                {
                                    item.Quantity = Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]);
                                    item.IsCartQuantityAdjusted = true;
                                    if (isCartItemQuantityAdjusted == false)
                                    {
                                        isCartItemQuantityAdjusted = true;
                                    }
                                }
                            }
                        }
                    }

                    // convert 3 place of decimal
                    item.AvailableQuantity = isValidation == true ? productInventory.Quantity : item.Quantity;
                    item.PriceUnit = isValidation == true ? Utility.FormatDecimal(productInventory.ProductPricePrice * ConversionRate, 3) : Utility.FormatDecimal(Convert.ToDecimal(item.Price) * ConversionRate, 3);
                    item.Price = isValidation == true ? Convert.ToDecimal(item.PriceUnit) : Convert.ToDecimal(item.Price);
                    item.DiscountedPrice = isValidation == true ? Utility.FormatDecimal(productInventory.ProductDiscountPrice * ConversionRate, 3) : Utility.FormatDecimal(Convert.ToDecimal(item.DiscountedPrice) * ConversionRate, 3);
                    item.Total = isValidation == true ? Utility.FormatDecimal(Convert.ToDecimal(productInventory.ProductDiscountPrice) * item.Quantity * ConversionRate, 3) : Utility.FormatDecimal(Convert.ToDecimal(item.DiscountedPrice) * item.Quantity * ConversionRate, 3);
                    baseCartTotal += Convert.ToDecimal(item.Total);

                    if (item.DeliveryTypeID != Convert.ToInt32(DeliveryTypes.Email) && isOnlineBranchPhysicalCartItems == false)
                    {
                        isOnlineBranchPhysicalCartItems = true;
                    }
                }
            }
            else
            {
                //get cart in process
                if (cart.IsNotNull())
                {
                    if (isCartItemDeleted) { items = shoppingCartRepository.GetCartItems(customerID, cart.ShoppingCartIID, cartStatus, cultureID, _callContext); }

                    foreach (var item in items)
                    {
                        var productInventory = (from productInventoryDetails in productInventoryList
                                                where productInventoryDetails.ProductSKUMapID == item.SKUID
                                                select productInventoryDetails).FirstOrDefault();
                        if (customerIID.HasValue && customerIID > 0)
                        {
                            if (isValidation && productInventory.Quantity == 0)
                            {
                                var isDeleted = RemoveItem(item.SKUID, contextualInformation, customerIID);
                                if (isDeleted && isCartItemOutOfStock == false)
                                {
                                    item.Quantity = 0;
                                    item.IsOutOfStock = true;
                                    isCartItemOutOfStock = true;
                                }
                                else if (isDeleted && isCartItemOutOfStock == true)
                                {
                                    item.Quantity = 0;
                                    item.IsOutOfStock = true;
                                }

                            }
                            if (isValidation && item.IsOutOfStock == false)
                            {
                                DataTable dt = shoppingCartRepository.CheckOutCartQuantityVerification(item.SKUID, customerIID.Value, (Int32)item.Quantity, (Int32)productInventory.Quantity);
                                if (dt != null && dt.Rows.Count > 0 && item.Quantity != Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]))
                                {
                                    bool isQtyUpdated = false;
                                    var allowedquantity = Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]);

                                    if (allowedquantity == 0)
                                    {
                                        isQtyUpdated = RemoveItem(item.SKUID, contextualInformation, customerIID);
                                    }
                                    else
                                    {
                                        isQtyUpdated = shoppingCartRepository.UpdateItem(item.SKUID, Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]), customerIID.ToString(), item.BranchID, GetSiteID(_callContext));
                                    }
                                    if (isQtyUpdated)
                                    {
                                        item.Quantity = Convert.ToDecimal(dt.Rows[0]["AllowedQtyInCart"]);
                                        item.IsCartQuantityAdjusted = true;
                                        if (isCartItemQuantityAdjusted == false)
                                        {
                                            isCartItemQuantityAdjusted = true;
                                        }
                                    }
                                }
                            }
                        }

                        var deliveryDaysCount = cart.DeliveryDaysCount.HasValue ? Convert.ToDecimal(cart.DeliveryDaysCount) :1 ; 
                            item.AvailableQuantity = isValidation == true ? productInventory.Quantity : item.Quantity;
                            item.PriceUnit = isValidation == true ? Utility.FormatDecimal(productInventory.ProductPricePrice * ConversionRate, 3) : Utility.FormatDecimal(Convert.ToDecimal(item.Price) * ConversionRate, 3);
                            item.Price = isValidation == true ? Convert.ToDecimal(item.PriceUnit) : Convert.ToDecimal(item.Price);
                            item.DiscountedPrice = isValidation == true ? Utility.FormatDecimal(productInventory.ProductDiscountPrice * ConversionRate, 3) : Utility.FormatDecimal(Convert.ToDecimal(item.DiscountedPrice) * ConversionRate, 3);
                            item.Total = Utility.FormatDecimal(Convert.ToDecimal(productInventory.ProductDiscountPrice) * item.Quantity  * deliveryDaysCount * ConversionRate, 3);
                            item.IntlDeliveryEnabled = true;
                            baseCartTotal += Convert.ToDecimal(item.Total);

                            if (item.DeliveryTypeID != Convert.ToInt32(DeliveryTypes.Email) && isOnlineBranchPhysicalCartItems == false)
                            {
                                isOnlineBranchPhysicalCartItems = true;
                            }

                    }
                }

            }

            if (cart.IsNotNull())
            {
                if (isValidation)
                {
                    foreach (var itemPriceDetails in items)
                    {
                        var result = shoppingCartRepository.UpdateItemPrice(itemPriceDetails.SKUID, customerID, Convert.ToDecimal(itemPriceDetails.PriceUnit), Convert.ToDecimal(itemPriceDetails.DiscountedPrice),GetSiteID(_callContext));
                        bool isWeightUpdated = false;
                        // below call has to be done only for KSA
                        isWeightUpdated = shoppingCartRepository.ShoppingCartItemWeightUpdate(cart.ShoppingCartIID, itemPriceDetails.SKUID, Convert.ToInt32(_callContext.CompanyID));
                    }
                }
                cartWithItems.CustomerID = customerIID.HasValue?Convert.ToInt64(customerIID):default(long);
                cartWithItems.ShoppingCartID = cart.ShoppingCartIID;
                cartWithItems.PaymentMethod = cart.PaymentMethod;
                cartWithItems.StudentID = cart.StudentID;
                cartWithItems.SchoolID = cart.SchoolID;
                cartWithItems.AcademicYearID = cart.AcademicYearID;
                cartWithItems.BillingAddressID = Convert.ToInt64(cart.BillingAddressID);
                cartWithItems.ShippingAddressID = Convert.ToInt64(cart.ShippingAddressID);
                cartWithItems.IsIntlCart = cartWithItems.ShippingAddressID.HasValue && cartWithItems.ShippingAddressID.Value >0 ? shoppingCartRepository.IsInternationalCart(cartWithItems.ShippingAddressID.Value, GetSiteID(_callContext)) : false;
                var deliveryList = shoppingCartRepository.CartDeliveryList(cartWithItems.ShippingAddressID.Value, cart.ShoppingCartIID,!cartWithItems.IsIntlCart, GetSiteID(_callContext));

                if (deliveryList != null)
                {
                    foreach (var item in items)
                    {
                        var deliveryTypeList = new List<DeliveryDTO>();
                        var result = deliveryList.Select("ProductSKUMapID=" + item.SKUID + "");
                        if (result != null && result.Any())
                        {
                            var delSelected = result.CopyToDataTable().Select("DeliveryTypeID=" + item.DeliveryTypeID + "");
                            bool populateDeliveryType = false;
                            if (delSelected.Count() == 0)
                            {
                                populateDeliveryType = true;
                                item.DeliveryTypeID = 0;
                            }
                            foreach (var delType in result)
                            {
                                deliveryTypeList.Add(DeliveryDisplay(delType,cultureID));
                                if (Convert.ToString(item.DeliveryTypeID) == "0" || populateDeliveryType == true)
                                {
                                    item.DeliveryTypeID = Convert.ToInt32(delType["DeliveryTypeID"]);
                                    
                                    if (!string.IsNullOrEmpty(Convert.ToString(delType["DeliveryDays"])))
                                    {
                                        if (Convert.ToInt32(delType["DeliveryDays"]) > 0)
                                        {
                                            item.DeliveryDays = Convert.ToInt32(delType["DeliveryDays"]);
                                        }
                                    }
                                    if(customerIID.HasValue)
                                    { 
                                        item.CustomerID = customerIID;
                                    }
                                    UpdateCartDelivery(item, contextualInformation);
                                    populateDeliveryType = false;
                                }
                                
                                if (item.DeliveryTypeID == Convert.ToInt32(DeliveryTypes.Email) || item.DeliveryTypeID == Convert.ToInt32(DeliveryTypes.EmailInternationalDelivery))
                                {
                                    cartWithItems.CartDigitalAmount += item.Quantity * Convert.ToDecimal(item.DiscountedPrice);
                                }

                                if ((item.DeliveryTypeID == Convert.ToInt32(DeliveryTypes.Email)
                                    || item.DeliveryTypeID == Convert.ToInt32(DeliveryTypes.EmailInternationalDelivery)) && cartWithItems.IsEmailDeliveryInCart == false)
                                {
                                    cartWithItems.IsEmailDeliveryInCart = true;
                                }

                                if (item.DeliveryTypeID == Convert.ToInt32(DeliveryTypes.StorePickup) && cartWithItems.IsStorePickUpInCart == false)
                                {
                                    cartWithItems.IsStorePickUpInCart = true;
                                }
                            }
                            var displayDetails = (from del in deliveryTypeList
                                               select new
                                               {
                                                   DeliveryTypeID = del.DeliveryMethodID,
                                                   DisplayText =del.CutOffDisplayText,
                                                   DeliveryDateTime = del.DeliveryDateTime
                                               }).Where(o => o.DeliveryTypeID == item.DeliveryTypeID).FirstOrDefault();
                            item.DisplayText = !string.IsNullOrEmpty(displayDetails.DisplayText) ? displayDetails.DisplayText : string.Empty;
                            item.DeliveryDateTime = displayDetails.DeliveryDateTime.IsNotNull()?displayDetails.DeliveryDateTime:DateTime.Now;
                        }
                        else if (cartWithItems.IsIntlCart)
                        {
                            item.IntlDeliveryEnabled = false;
                            ProccedToPaymentCheck(ref cartWithItems);
                        }
                        else { ProccedToPaymentCheck(ref cartWithItems); }
                        item.DeliveryTypes = deliveryTypeList;
                    }
                }


                if (cart.IsNotNull())
                {
                    DataTable dt = shoppingCartRepository.CartDeliveryCost(cartWithItems.ShippingAddressID.Value, cart.ShoppingCartIID, Convert.ToInt32(contextualInformation.CompanyID), !cartWithItems.IsIntlCart);
                    cartWithItems.DeliveryCharge = dt != null ? Convert.ToDecimal(dt.Rows[0]["FinalDeliveryCost"]) : 0;
                    var convertedDeliveryCharge = Convert.ToDecimal(cartWithItems.DeliveryCharge) * ConversionRate;
                    var rounded = decimal.Parse(convertedDeliveryCharge.ToString());
                    cartWithItems.DeliveryCharge = rounded;
                }

                // recalculate the voucher based on amount (only if cart is InProcess) 
                if ((int)(ShoppingCartStatus.PaymentInitiated) == cart.CartStatusID)
                {
                    ReCalculateVoucher(cart, baseCartTotal + cartWithItems.DeliveryCharge);
                }

                // get voucherMap for this cart
                var voucherMap = shoppingCartRepository.GetVoucherMap(cart.ShoppingCartIID);
                cartWithItems.IsVoucherApplied = voucherMap.IsNotNull() ? true : false;
                cartWithItems.VoucherValue = voucherMap.IsNotNull() ? Utility.FormatDecimal(Convert.ToDecimal(voucherMap.Amount) * ConversionRate, 3) : "0";
                //get voucher map and deduct amount if voucher applied

                cartWithItems.Products = items;
                if (items.Count > 0)
                {
                    cartWithItems.SubTotal = Utility.FormatDecimal(baseCartTotal, 3);
                    //cartWithItems.SubTotal = Utility.FormatDecimal(subTotal, 3);
                    // calculate total
                    if (voucherMap.IsNotNull())
                    {
                        if (Convert.ToDecimal(cartWithItems.SubTotal) < (voucherMap.Amount * ConversionRate))
                        {
                            cartWithItems.VoucherValue = cartWithItems.SubTotal;
                        }
                    }
                    cartWithItems.Total = Utility.FormatDecimal(Convert.ToDecimal(cartWithItems.SubTotal) - Convert.ToDecimal(cartWithItems.VoucherValue) + Convert.ToDecimal(cartWithItems.DeliveryCharge), 3);
                }
                else
                {
                    cartWithItems.SubTotal = "0";
                    cartWithItems.Total = Utility.FormatDecimal(Convert.ToDecimal(cartWithItems.SubTotal) + Convert.ToDecimal(cartWithItems.DeliveryCharge), 3);
                }


            }
            else
            {
                cartWithItems.Products = null;
                cartWithItems.SubTotal = "0";
                cartWithItems.Total = Utility.FormatDecimal(Convert.ToDecimal(cartWithItems.SubTotal) + Convert.ToDecimal(cartWithItems.DeliveryCharge), 3);
            }
            // set false delivery chrge
            cartWithItems.IsDeliveryCharge = false;
            cartWithItems.IsCartItemDeleted = isCartItemDeleted;
            cartWithItems.IsCartItemOutOfStock = isCartItemOutOfStock;
            cartWithItems.IsCartItemQuantityAdjusted = isCartItemQuantityAdjusted;
            cartWithItems.IsOnlineBranchPhysicalCartItems = isOnlineBranchPhysicalCartItems;
            return cartWithItems;
        }

        public DeliveryDTO DeliveryDisplay(DataRow delType, int cultureID)
        {
            string strDeliveryText;
            int deliveryTypeID = Int32.Parse(delType["DeliveryTypeID"].ToString());
            switch (deliveryTypeID)
            {
                case (int)DeliveryTypes.nDaysDelivery:
                    var str = Convert.ToString(delType["DisplayRange"]) != "0" ? delType["DeliveryDays"].ToString() + "-" + (Convert.ToInt32(delType["DeliveryDays"]) + Convert.ToInt32(delType["DisplayRange"])).ToString() : delType["DeliveryDays"].ToString();
                    strDeliveryText = ResourceHelper.GetValue("DeliveryWithinDays1", _callContext.LanguageCode) + str + ResourceHelper.GetValue("DeliveryWithinDays2", _callContext.LanguageCode);
                    break;
                case (int)DeliveryTypes.InternationalDelivery:
                    strDeliveryText = !String.IsNullOrEmpty(delType["DeliveryDays"].ToString()) && delType["DeliveryDays"].ToString() != "0" ? string.Concat(ResourceHelper.GetValue("ShippingWithin", _callContext.LanguageCode), delType["DeliveryDays"].ToString(), ResourceHelper.GetValue("WorkingDays", _callContext.LanguageCode)) : ResourceHelper.GetValue("SameDayShipping", _callContext.LanguageCode);
                    break;
                case (int)DeliveryTypes.EmailInternationalDelivery:
                    strDeliveryText = ResourceHelper.GetValue("DeliveryByEmail", _callContext.LanguageCode);
                    break;
                default:
                    strDeliveryText = delType["DeliveryTypeName"].ToString();
                    break;
            }
            return GetDeliveryTimeSlot(deliveryTypeID, strDeliveryText, Int32.Parse(delType["DeliveryDays"].ToString()),cultureID);
        }

        public DeliveryDTO GetDeliveryTimeSlot(int deliveryTypeID, string deliveryString, int deliveryDays, int cultureID)
        {
            var deliveryTypeItem = new DeliveryDTO();
            deliveryTypeItem.DeliveryMethodID = deliveryTypeID;
            DateTime deliveryDate = DateTime.Now;
            var deliveryTimeSlot = shoppingCartRepository.GetDeliveryTimeSlot(deliveryTypeID);
            if (deliveryTimeSlot.IsNotNull())
            {
              if(deliveryTimeSlot.IsCutOff.HasValue?deliveryTimeSlot.IsCutOff.Value:false  )
              {
                  int addedDays = deliveryTypeID != (int)DeliveryTypes.nDaysDelivery ? deliveryTimeSlot.CutOffDays.HasValue ? deliveryTimeSlot.CutOffDays.Value : 0 : deliveryDays;
                  deliveryDate = new MutualBL(_callContext).GetHoliday(deliveryTypeID, 1, addedDays);
              }
              switch (deliveryTypeID)
              {
                  case (int)DeliveryTypes.NextDay:
                      DisplayText(deliveryTimeSlot, ref deliveryDate, 1, ref deliveryString, ref deliveryTypeItem, cultureID);
                      break;
                  case (int)DeliveryTypes.StorePickup:
                      DisplayText(deliveryTimeSlot, ref deliveryDate, 1, ref deliveryString, ref deliveryTypeItem, cultureID);
                      break;
                  case (int)DeliveryTypes.Express:
                      DisplayText(deliveryTimeSlot, ref deliveryDate, 2, ref deliveryString, ref deliveryTypeItem, cultureID);
                      break;
                  case (int)DeliveryTypes.SuperExpress:
                      DisplayText(deliveryTimeSlot, ref deliveryDate, 2, ref deliveryString, ref deliveryTypeItem, cultureID);
                      break;
                  case (int)DeliveryTypes.nDaysDelivery:
                      DisplayText(deliveryTimeSlot, ref deliveryDate, 0, ref deliveryString, ref deliveryTypeItem, cultureID);
                      break;
                  default:
                      break;
              }
              deliveryTypeItem.IsCutOff = deliveryTimeSlot.IsCutOff;
              deliveryTypeItem.CutOffDays = deliveryTimeSlot.CutOffDays;
              deliveryTypeItem.CutOffTime =Convert.ToString(deliveryTimeSlot.CutOffTime);
              deliveryTypeItem.CutOffHour = Convert.ToString(deliveryTimeSlot.CutOffHour);
            }
            deliveryTypeItem.DeliveryDateTime = deliveryDate;
            deliveryTypeItem.DeliveryType = deliveryString;
            deliveryTypeItem.DeliveryCharge = 0M;
            
            return deliveryTypeItem;
        }

        public void DisplayText(DeliveryTypeTimeSlotMap deliveryTimeSlot, ref DateTime deliveryDate, int formatType, ref string deliveryString, ref DeliveryDTO deliveryTypeItem, int cultureID)
        {
            var mapper = new DocumentTypeMapper();
            deliveryDate = deliveryTimeSlot.CutOffTime.HasValue ? deliveryDate.Add(deliveryTimeSlot.CutOffTime.Value) : !deliveryTimeSlot.CutOffTime.HasValue && deliveryTimeSlot.CutOffHour.HasValue ? deliveryDate.AddHours(Convert.ToDouble(deliveryTimeSlot.CutOffHour)):deliveryDate;
            switch (formatType)
            {
                case 1:
                    deliveryString += " (" + deliveryDate.ToString("dd-MMM-yyyy") + ")";
                    deliveryTypeItem.CutOffDisplayTextCulture = DeliveryTypeCheckMappers.ToDtoConfigCulureList(deliveryTimeSlot, deliveryDate);
                    deliveryTypeItem.CutOffDisplayText = deliveryTypeItem.CutOffDisplayTextCulture.Where(x=>x.CultureID==cultureID).Select(x=>x.CutOffDisplayText).FirstOrDefault();
                    break;
                case 2:
                    deliveryString += deliveryTimeSlot.CutOffDisplayText !=null?" (" + string.Format(deliveryTimeSlot.CutOffDisplayText, deliveryTimeSlot.CutOffHour.ToString()) + ")":string.Empty;
                    break;
                default:
                    break;
            }
        }

        

        void ProccedToPaymentCheck(ref CartDTO cartDTO)
        {
            if (cartDTO.IsProceedToPayment)
            {
                cartDTO.IsProceedToPayment = false;
            }
        }

        public bool IsInternationalCartByCountryID(int countryID, int siteID)
        {
            return shoppingCartRepository.IsInternationalCartByCountryID(countryID, siteID);
        }


        private void ReCalculateVoucher(ShoppingCart cart, decimal baseCartTotal)
        {
            // get voucher map 
            var voucherMap = shoppingCartRepository.GetVoucherMap(cart.ShoppingCartIID);
            if (voucherMap.IsNull())
                return;

            // get voucher
            var voucher = shoppingCartRepository.GetVoucher((long)voucherMap.VoucherID);
            if (voucher.IsNull())
                return;


            // Apply amount is current balance null else current balance
            if (voucher.CurrentBalance.IsNotNull())
                voucherMap.Amount = baseCartTotal > voucher.CurrentBalance ? (decimal)voucher.CurrentBalance : baseCartTotal;
            else
                voucherMap.Amount = baseCartTotal > voucher.Amount ? (decimal)voucher.Amount : baseCartTotal;

            // update voucher map
            shoppingCartRepository.UpdateVoucherMap(voucherMap);
        }


        public bool UpdateCart(CartProductDTO product, CallContext contextualInformation)
        {
            if (product.Quantity > 0)
            {
                return shoppingCartRepository.UpdateItem(product.SKUID, product.Quantity, product.CustomerID.HasValue && product.CustomerID >0 ? product.CustomerID.ToString() : GetCustomerID(contextualInformation), product.BranchID,GetSiteID(_callContext));
            }
            else
            {
                return RemoveItem(product.SKUID, contextualInformation, product.CustomerID);
            }
        }

        public bool UpdateCartDelivery(CartProductDTO product, CallContext contextualInformation)
        {
            if (product.DeliveryTypeID > 0)
            {
                if(product.DeliveryTypeID == (int)DeliveryTypes.nDaysDelivery)
                {
                    var deliveryType = shoppingCartRepository.GetDeliveryDays(product.SKUID, _callContext.CompanyID.Value, product.DeliveryTypeID);
                    product.DeliveryDays = deliveryType.DeliveryDays;
                }
                return shoppingCartRepository.UpdateItemDelivery(product.SKUID, product.DeliveryTypeID, 
                    product.CustomerID.HasValue && product.CustomerID > 0 ? product.CustomerID.ToString() : GetCustomerID(contextualInformation),
                    GetSiteID(_callContext), product.DeliveryDays, product.DeliveryRange, product.DeliveryTypeTimeSlotMapID);
            }
            else
            {
                return RemoveItem(product.SKUID, contextualInformation, product.CustomerID);
            }
        }

        public string DeliveryTypeText(int deliveryTypeID, int deliverydays)
        {
            string deliveryTypeText = "";
            switch (deliveryTypeID)
            {
                case (int)DeliveryTypes.nDaysDelivery:
                    deliveryTypeText = ResourceHelper.GetValue("DeliveryWithinDays1", _callContext != null ? _callContext.LanguageCode : "en") + deliverydays.ToString() + ResourceHelper.GetValue("DeliveryWithinDays2", _callContext != null ? _callContext.LanguageCode : "en");
                    break;
                case (int)DeliveryTypes.Email:
                    deliveryTypeText = ResourceHelper.GetValue("DeliveryByEmail", _callContext != null ? _callContext.LanguageCode : "en");
                    break;
                case (int)DeliveryTypes.Express:
                    deliveryTypeText = ResourceHelper.GetValue("DeliveryByExpress", _callContext != null ? _callContext.LanguageCode : "en");
                    break;
                case (int)DeliveryTypes.SuperExpress:
                    deliveryTypeText = ResourceHelper.GetValue("DeliveryBySuperExpress", _callContext != null ? _callContext.LanguageCode : "en");
                    break;
                case (int)DeliveryTypes.StorePickup:
                    deliveryTypeText = ResourceHelper.GetValue("DeliveryByStorePickUp", _callContext != null ? _callContext.LanguageCode : "en");
                    break;
                case (int)DeliveryTypes.NextDay:
                    deliveryTypeText = ResourceHelper.GetValue("DeliveryByNextDay", _callContext != null ? _callContext.LanguageCode : "en");
                    break;
                default:
                    break;
            }
            return deliveryTypeText;
        }


        public bool UpdateCart(CartDTO cartDTO, CallContext context)
        {
            var userCart = shoppingCartRepository.IsUserCartExist(cartDTO.CustomerID.HasValue ? cartDTO.CustomerID.Value.ToString() : GetCustomerID(context), (int)ShoppingCartStatus.InProcess, GetSiteID(_callContext));

            // Update entity using DTO
            //userCart.CreatedDate = DateTime.Now;
            userCart.BillingAddressID = (cartDTO.BillingAddressID.IsNotNull() ? cartDTO.BillingAddressID : userCart.BillingAddressID) == 0 ? cartDTO.ShippingAddressID : cartDTO.BillingAddressID;
            userCart.ShippingAddressID = cartDTO.ShippingAddressID.IsNotNull() ? cartDTO.ShippingAddressID : userCart.ShippingAddressID;
            // will add other fields later

            return shoppingCartRepository.UpdateCart(userCart);
        }


        public bool UpdateCartStatus(long cartID, ShoppingCartStatus cartStatuID, string paymentMethod = null, ShoppingCartStatus existingStatus = ShoppingCartStatus.None)
        {
            return shoppingCartRepository.UpdateCartStatus(cartID, cartStatuID,GetSiteID(_callContext), paymentMethod, existingStatus);
        }

        public bool UpdateCartCustomerID(long cartID, long customerID)
        {
            return shoppingCartRepository.UpdateCartCustomerID(cartID, customerID);
        }

        public bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID, Int16 PaymentGateWayID)
        {
            return shoppingCartRepository.UpdateCartPaymentGateWay(cartID, cartStatuID, PaymentGateWayID);
        }

        public ShoppingCartDTO IsUserCartExist(CallContext contextualInformation, long? customerID = null)
        {
            var entity = shoppingCartRepository.IsUserCartExist(customerID.HasValue ? customerID.Value.ToString() : GetCustomerID(contextualInformation), (int)ShoppingCartStatus.InProcess, GetSiteID(_callContext));

            if (entity.IsNotNull())
            {
                return new ShoppingCartDTO()
                {
                    ShoppingCartIID = entity.ShoppingCartIID,
                    CartID = entity.CartID
                };
            }
            else
                return null;
        }

        public ProcessOrderDTO UpdateCartDescription(ProcessOrderDTO processOrderDTO)
        {
            var mapper = new ProcessOrderMapper();
            var entity = shoppingCartRepository.UpdateCartDescription(mapper.ToEntity(processOrderDTO));

            if (entity.IsNotNull())
            {
                return mapper.ToDTO(entity);
            }
            else
                return null;
        }

        public AddToCartStatusDTO AddToCart(CartProductDTO product, CallContext contextualInformation)
        {
            bool isShoppingCartUpdated = false;
            string Message = "";

            var userCart = shoppingCartRepository.IsUserCartExist(product.CustomerID.HasValue && product.CustomerID > 0 ? product.CustomerID.ToString() : GetCustomerID(contextualInformation), (int)ShoppingCartStatus.InProcess, GetSiteID(_callContext));
            var settingDTO = new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINEBRANCHID,_callContext.CompanyID.HasValue?(long)_callContext.CompanyID.Value: default(long));
            var canteenCategoryID = new SettingRepository().GetSettingDetail("CANTEEN_CATEGORY_ID").SettingValue;
            string cartIID = string.Empty;

            if (userCart != null && userCart.CartID != null)
            {
                cartIID = userCart.CartID;
            }

            long LoginID = 0;
            long CustomerID = 0;

            if (contextualInformation != null && contextualInformation.LoginID != null)
            {
                LoginID = Convert.ToInt64(contextualInformation.LoginID);
                CustomerID = product.CustomerID.HasValue && product.CustomerID > 0 ? product.CustomerID.Value : Convert.ToInt64(contextualInformation.UserId);
            }

            var productInventory = new ProductDetailBL(contextualInformation).GetInventoryDetailsSKUID(product.SKUID, CustomerID);
            var addTocartStatus = shoppingCartRepository.AddToCartVerification(product.SKUID, CustomerID, (Int32)product.Quantity, cartIID, Convert.ToInt32(productInventory.Quantity));

            var addToCartStatusDTO = new AddToCartStatusDTO();

            if (productInventory != null)
            {
                addToCartStatusDTO.BranchID = productInventory.BranchID;
                addToCartStatusDTO.SKUID = productInventory.ProductSKUMapID;
                addToCartStatusDTO.CartMessage = AddToCartStatusMessage(2);
            }

            var cartItems = shoppingCartRepository.GetCartDetailsWithItems(CustomerID.ToString(), (int)ShoppingCartStatus.InProcess, null);

            var newAddedCartItem = shoppingCartRepository.GetAddedCartDetailsWithItems(product.SKUID);

            if (addTocartStatus != null)
            {
                foreach (var item in addTocartStatus.AsEnumerable())
                {
                    addToCartStatusDTO.Status = newAddedCartItem.CategoryIID == int.Parse(canteenCategoryID) ? true : Convert.ToBoolean(item["CartSuccess"]);
                    addToCartStatusDTO.CartMessage = newAddedCartItem.CategoryIID == int.Parse(canteenCategoryID) ? AddToCartStatusMessage(0) : AddToCartStatusMessage(Convert.ToInt16(item["CartErrorMessageID"]));
                    addToCartStatusDTO.BranchID = productInventory.BranchID;
                }
            }

            //var sameCategory = cartItems.Any(x => x.CategoryName.ToLower() == (newAddedCartItem.Any(y => y.CategoryName.ToLower())));

            product.BranchID = productInventory.BranchID;

            if (addToCartStatusDTO.Status)
            {
                if (userCart.IsNotNull())
                {
                    //check if items exists in shopping cart item
                    var item = shoppingCartRepository.IsItemExist(product.SKUID, userCart.CartID);

                    if (shoppingCartRepository.IsDigitalProduct(product.SKUID, userCart.CartID))
                    {
                        product.DeliveryTypeID = (int)DeliveryTypes.Email;
                    }

                    if (cartItems.CategoryIID == newAddedCartItem.CategoryIID)
                    {
                        if (item.IsNotNull())
                        {
                            var currentproductqty = shoppingCartRepository.GetShoppingCartItemQuantity(product.SKUID, userCart.CartID);
                            product.Quantity = product.Quantity + currentproductqty;
                            isShoppingCartUpdated = UpdateCart(product, contextualInformation);
                        }
                        else
                        {
                            isShoppingCartUpdated = AddItem(product, userCart);
                        }

                        //if the inventory is allocated for the cart and he wants to alter before confirming, data will be invalid and make the inventory available for the website.
                        if (isShoppingCartUpdated)
                        {
                            var cart = new ShoppingCartRepository().GetCartDetailbyIID(userCart.ShoppingCartIID);

                            if (cart.IsInventoryBlocked.HasValue && cart.IsInventoryBlocked.Value)
                            {
                                new OrderBL(_callContext).SetTemporaryBranchTransfer(cart.ShoppingCartIID, true);
                            }
                        }
                    }
                    else
                    {
                        isShoppingCartUpdated = false;
                        Message = "Please remove "+ cartItems.CategoryName + " items from the cart and continue" + Environment.NewLine + "shopping for other products";
                    }
                }
                else
                {
                    //create cart
                    userCart = Cart(product.CustomerID.HasValue && product.CustomerID > 0 ? product.CustomerID.Value.ToString() : GetCustomerID(contextualInformation));

                    if (shoppingCartRepository.IsDigitalProduct(product.SKUID, userCart.CartID))
                    {
                        product.DeliveryTypeID = (int)DeliveryTypes.Email;
                    }

                    //add item
                    isShoppingCartUpdated = AddItem(product, userCart);
                }

                if (isShoppingCartUpdated)
                {
                    var productPrice = new ProductDetailBL().GetPriceDetailsSKUID(product.BranchID, product.SKUID, CustomerID, cartIID);
                    if (productPrice != null)
                    {
                        addToCartStatusDTO.ProductDiscountPrice = productPrice.ProductDiscountPrice;
                    }
                }

                addToCartStatusDTO.Status = isShoppingCartUpdated;
                addToCartStatusDTO.CartMessage = Message;
            }

            return addToCartStatusDTO;
        }

        public ShoppingCart Cart(string CartID,bool isToInvalid=false, long? contactId = null)
        {  
            var userCart = new ShoppingCart();

            userCart.CartID = CartID.IsNotNull() ? CartID.ToString() : null;
            userCart.CreatedDate = DateTime.Now;
            //userCart.CreatedBy = _callContext.IsNotNull() ? _callContext.LoginID : null; // created by is required only for the carts created by ERP which is handled down for website join so with cartID
            userCart.CartStatusID = (int)ShoppingCartStatus.InProcess;
            userCart.CompanyID = _callContext.IsNotNull() ? _callContext.CompanyID : default(int?);
            userCart.SiteID = GetSiteID(_callContext);
            userCart.ShippingAddressID = contactId;

            if (isToInvalid)
            {
                shoppingCartRepository.SetCartStatusInvalid(CartID, (int)ShoppingCartStatus.InProcess, GetSiteID(_callContext));
                userCart.CreatedBy = _callContext.IsNull() ? default(int?) : _callContext.LoginID;
            }

            userCart = shoppingCartRepository.AddCart(userCart);
            return userCart;
        }

        public  int? GetSiteID(CallContext _callContext)
        {
            var siteID = _callContext.IsNull() || string.IsNullOrEmpty(_callContext.SiteID) ? (int?)null : 
                Convert.ToInt32(_callContext.SiteID);

            if(siteID.HasValue)
            {
                var site = shoppingCartRepository.GetSiteByCompany(_callContext.CompanyID);

                if(site != null)
                {
                    siteID = site.SiteID;
                }
                else
                {
                    siteID = (int?)null; //default
                }
            }

            return siteID;
        }


        public CartDTO CreateCart(long customerID)
        {
            var mapper = new CartMapper();

            // 
            var contacts = new AccountBL(_callContext).GetBillingShippingContact(customerID, AddressType.All);
            // get shipping contact
            var contact = contacts.Where(x => x.IsShippingAddress == true).FirstOrDefault();

            // this is we are doing becuase we need to return one address if we don't have shipping
            if (contacts.IsNull())
            {
                contact = contacts.FirstOrDefault();
            }

            var cart = Cart(customerID.ToString(), true, contact == null ? (long?)null : contact.ContactID);
            return mapper.ToDTO(cart);
        }

        string AddToCartStatusMessage(Int16 MessageID)
        {
            switch (MessageID)
            {
                case 1:
                    return "You have reached max purchase limit for today. Try again later";
                case 2:
                    return "Selected product quantity is no-more available";
                case 3:
                    return "Reached maximum allowed quantity in an order";
                case 4:
                    return "Please enter valid quantiy";
                default:
                    return "";
            }
        }

        public bool RemoveItem(long productSKU, CallContext contextualInformation, long? customerID = null)
        {
            return shoppingCartRepository.RemoveItem(productSKU, customerID.HasValue && customerID.Value > 0 ? customerID.Value.ToString() : GetCustomerID(contextualInformation), GetSiteID(_callContext));
        }

        public CartDTO GetCartPrice(CallContext callContext, long? customerID = null)
        {
            var cartDTO = GetCart(callContext, customerIID: customerID.IsNotNull() && customerID > 0 ? customerID :callContext.EmailID !=null?Convert.ToInt64(GetCustomerID(callContext)):(long?)null);
            return cartDTO;
        }

        public decimal GetCartTotal(CallContext callContext, bool withCurrencyConversion, bool isDeliveryCharge = false,
            ShoppingCartStatus cartStatus = ShoppingCartStatus.InProcess)
        {
            return decimal.Parse(GetCart(callContext, withCurrencyConversion: withCurrencyConversion, isDeliveryCharge: isDeliveryCharge, cartStatus: cartStatus).Total);
        }

        public int ProductCount(CallContext callContext)
        {
            return shoppingCartRepository.ProductCount(Convert.ToString(GetCustomerID(callContext)),GetSiteID(_callContext), ShoppingCartStatus.InProcess);
        }

        public bool MergeCart(CallContext callContext)
        {
            var result = false;

            if (!string.IsNullOrEmpty(callContext.EmailID) || !string.IsNullOrEmpty(callContext.GUID))
            {
                result = shoppingCartRepository.MergeCart(callContext.GUID, GetCustomerID(callContext),GetSiteID(callContext));
            }
            return result;
        }

        public string GetCartDetailByHeadID(long orderID)
        {
            var shoppingCartMapsdetail = new WarehouseRepository().GetCartDetailsByHeadID(orderID);

            if (shoppingCartMapsdetail.IsNotNull() && shoppingCartMapsdetail.Count > 0)
            {
                return Convert.ToString(shoppingCartMapsdetail.First().ShoppingCartID);
            }
            else
            {
                return string.Empty;
            }
        }


        #endregion

        #region Voucher Methods

        public VoucherDTO ApplyVoucher(string voucherNumber, CallContext callContext)
        {
            var defaultcurrency = ResourceHelper.GetValue(_callContext.CurrencyCode, _callContext != null ? _callContext.LanguageCode : "en");
            var voucherResult = new VoucherDTO();
            voucherResult.IsVoucherValid = false;
            voucherResult.VoucherMessage = ResourceHelper.GetValue("InvalidVoucher", _callContext != null ? _callContext.LanguageCode : "en");

            // get the converted price 
            decimal ConversionRate = (decimal)UtilityRepository.GetExchangeRate((int)_callContext.CompanyID, _callContext.CurrencyCode);
            //decimal ConversionRate = 1;

            var customerID = GetCustomerID(callContext);
            int? companyID = _callContext.IsNotNull() ? (int)_callContext.CompanyID : (int?)null;
            // check voucher validity
            var voucher = new Voucher();
            if (!string.IsNullOrEmpty(voucherNumber))
            {
                var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                try
                {
                    voucherNumber = StringCipher.Encrypt(voucherNumber, hash);
                }
                catch (Exception) { }
                voucher = shoppingCartRepository.GetVoucher(voucherNumber, companyID: companyID);
            }
            else
            {
                voucher = null;
            }


            // check based on voucher type
            if (voucher.IsNotNull() && !string.IsNullOrEmpty(voucher.VoucherNo))
            {
                //if (!(voucher.IsSharable == false && voucher.CustomerID == long.Parse(customerID)))
                //{
                //    voucher = null;
                //}
                long custID;
                long.TryParse(customerID, out custID);
                if (voucher.IsSharable.IsNull())
                {
                    voucher.IsSharable = false;
                }
                if (voucher.CustomerID.IsNull())
                {
                    voucher.CustomerID = 0;
                }
                if ((bool)voucher.IsSharable)
                {
                    if (voucher.CustomerID != 0)
                    {
                        voucher = null;
                    }
                }
                else
                {
                    if (custID == 0)
                    {
                        voucher = null;
                    }

                    if (voucher.CustomerID != custID)
                    {
                        voucher = null;
                    }
                }
                //switch ((VoucherTypes)voucher.VoucherTypeID)
                //{
                //    case VoucherTypes.Customer:
                //        long custID;
                //        long.TryParse(customerID, out custID);
                //        if (custID == 0)
                //        {
                //            voucher = null;
                //        }
                //        else if (voucher.CustomerID != custID)
                //            voucher = null;
                //        break;
                //}

            }


            if (voucher.IsNotNull() && !string.IsNullOrEmpty(voucher.VoucherNo))
            {
                // switch case

                if (voucher.ExpiryDate > DateTime.Now || voucher.ExpiryDate.IsNull())
                {

                    if (voucher.CurrentBalance == null)
                    {
                        voucher.CurrentBalance = voucher.Amount;
                    }
                    //voucher.CurrentBalance = voucher.CurrentBalance * ConversionRate;
                    // If voucher is valid and has balance apply it
                    if (Convert.ToDecimal(voucher.CurrentBalance) > 0)
                    {
                        var cartItems = GetCart(callContext, withCurrencyConversion: false, isValidation: false);
                        var cartTotal = cartItems.Products.AsEnumerable().Sum(x => Convert.ToDecimal(x.DiscountedPrice) * x.Quantity);
                        cartTotal = cartTotal + cartItems.DeliveryCharge;
                        var cartTotalConversion = cartTotal * ConversionRate;
                        //var cartTotal = decimal.Parse(GetCart(callContext, withCurrencyConversion: false).Total);
                        decimal minimumAmount = 0;

                        //decimal voucherMinumimAmount = 0;
                        //decimal.TryParse(voucher.MinimumAmount.ToString(), out voucherMinumimAmount);
                        if (voucher.MinimumAmount.IsNull())
                        {
                            decimal.TryParse(new Domain.Setting.SettingBL().GetSettingDetail("MINIMUMVOUCHERAMOUNTKUW").SettingValue, out minimumAmount);
                        }
                        else
                        {
                            decimal.TryParse(voucher.MinimumAmount.ToString(), out minimumAmount);
                        }
                        minimumAmount = minimumAmount * ConversionRate;
                        if (cartTotalConversion < minimumAmount)
                        {
                            voucherResult.Status = Eduegate.Infrastructure.Enums.VoucherStatus.Invalid;
                            voucherResult.VoucherMessage = ResourceHelper.GetValue("AtleastVoucherCartTotal", _callContext != null ? _callContext.LanguageCode : "en") + minimumAmount + " " + defaultcurrency + ResourceHelper.GetValue("VoucherOrMore", _callContext != null ? _callContext.LanguageCode : "en");
                        }
                        else
                        {
                            //voucherResult.IsVoucherValid = true;
                            voucherResult.CurrentBalance = Convert.ToDecimal(voucher.CurrentBalance) * ConversionRate;

                            //get cart
                            //var cart = shoppingCartRepository.GetCartDetail(customerID, (int)ShoppingCartStatus.InProcess);

                            //get cart items to calculate subtotal/total of cart (without conversion)
                            //var cartItems = shoppingCartRepository.GetCartItems(customerID, cart.ShoppingCartIID, ShoppingCartStatus.InProcess);
                            //var cartItems = GetCart(callContext, withCurrencyConversion: false).Products;

                            //get voucher map
                            var voucherMap = shoppingCartRepository.GetVoucherMap(cartItems.ShoppingCartID);

                            //if already voucher applied, remove it
                            if (voucherMap.IsNotNull())
                            {
                                shoppingCartRepository.RemoveVoucherMap(voucherMap.ShoppingCartVoucherMapIID);
                            }
                            if (voucherResult.CurrentBalance > cartTotalConversion)
                            {
                                voucherResult.VoucherMessage = ResourceHelper.GetValue("CreditVoucher", _callContext != null ? _callContext.LanguageCode : "en") + Convert.ToDouble(voucherResult.CurrentBalance.ToString()).ToString("#.000") + " " + defaultcurrency + " ." + ResourceHelper.GetValue("RemainingVoucher", _callContext != null ? _callContext.LanguageCode : "en") + " " + Convert.ToDouble((voucherResult.CurrentBalance - cartTotalConversion).ToString()).ToString("#.000") + " " + defaultcurrency + ResourceHelper.GetValue("RedeemOrderVoucher", _callContext != null ? _callContext.LanguageCode : "en");
                            }
                            else
                            {
                                voucherResult.VoucherMessage = "";
                            }

                            var newVoucherMap = new ShoppingCartVoucherMap();
                            newVoucherMap.ShoppingCartID = cartItems.ShoppingCartID;
                            newVoucherMap.Amount = cartTotal > voucher.CurrentBalance ? voucher.CurrentBalance : cartTotal;
                            //newVoucherMap.Amount = (cartItems.AsEnumerable().Sum(x => Convert.ToDecimal(x.DiscountedPrice) * x.Quantity) > voucher.CurrentBalance) ? voucher.CurrentBalance : cartItems.AsEnumerable().Sum(x => Convert.ToDecimal(x.DiscountedPrice) * x.Quantity);
                            newVoucherMap.VoucherID = voucher.VoucherIID;
                            newVoucherMap.CreatedDate = DateTime.Now;
                            newVoucherMap.StatusID = Convert.ToByte(Eduegate.Infrastructure.Enums.VoucherStatus.Valid);


                            //add map for this cart
                            newVoucherMap = shoppingCartRepository.AddVoucherMap(newVoucherMap);
                            if (newVoucherMap.IsNotNull())
                            {
                                voucherResult.Status = Eduegate.Infrastructure.Enums.VoucherStatus.Valid;
                                voucherResult.IsVoucherValid = true;
                                voucherResult.VoucherValue = Utility.FormatDecimal(Convert.ToDecimal(newVoucherMap.Amount * ConversionRate), 3); ;
                            }
                            else
                            {
                                voucherResult.Status = Eduegate.Infrastructure.Enums.VoucherStatus.Invalid;
                                voucherResult.VoucherMessage = ResourceHelper.GetValue("PltryLater", _callContext != null ? _callContext.LanguageCode : "en");
                            }
                        }

                        // manage amount 

                    }
                    else
                    {
                        voucherResult.Status = Eduegate.Infrastructure.Enums.VoucherStatus.Used;
                        var usedDate = new OrderRepository().GetLastUsedVoucherOrder(voucher.VoucherIID);
                        if (usedDate.IsNotNull())
                        {
                            voucherResult.VoucherMessage = ResourceHelper.GetValue("VoucherUsedOn", _callContext != null ? _callContext.LanguageCode : "en") + " " + Convert.ToDateTime(usedDate).ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            voucherResult.VoucherMessage = ResourceHelper.GetValue("VoucherUsed", _callContext != null ? _callContext.LanguageCode : "en");
                        }

                        //voucherResult.CurrentBalance = Convert.ToDecimal(voucher.CurrentBalance);
                    }
                }
                else
                {
                    voucherResult.Status = Eduegate.Infrastructure.Enums.VoucherStatus.Expired;
                    if (voucher.ExpiryDate.IsNotNull())
                    {
                        voucherResult.VoucherMessage = ResourceHelper.GetValue("VoucherExpiredOn", _callContext != null ? _callContext.LanguageCode : "en") + " " + Convert.ToDateTime(voucher.ExpiryDate).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        voucherResult.VoucherMessage = ResourceHelper.GetValue("VoucherExpired", _callContext != null ? _callContext.LanguageCode : "en");
                    }

                }
            }
            else
            {
                voucherResult.Status = Eduegate.Infrastructure.Enums.VoucherStatus.Invalid;
                //get cart
                var cart = shoppingCartRepository.GetCartDetail(customerID, (int)ShoppingCartStatus.InProcess, GetSiteID(_callContext));

                if (cart.IsNotNull())
                {
                    //get voucher map
                    var voucherMap = shoppingCartRepository.GetVoucherMap(cart.ShoppingCartIID);

                    //if already voucher applied, remove it
                    if (voucherMap.IsNotNull())
                    {
                        shoppingCartRepository.RemoveVoucherMap(voucherMap.ShoppingCartVoucherMapIID);
                    }
                    voucherResult.VoucherMessage = ResourceHelper.GetValue("InvalidVoucher", _callContext != null ? _callContext.LanguageCode : "en");
                }
            }
            return voucherResult;
        }

        public Eduegate.Framework.Payment.PaymentGatewayType GetPaymentGatewayType(string paymentMethod, ShoppingCartStatus cartStatus = ShoppingCartStatus.InProcess)
        {
            var cartItems = GetCart(this._callContext, withCurrencyConversion: false, isValidation: false, cartStatus: cartStatus);
            var cartTotalBeforeVoucher = cartItems.Products.AsEnumerable().Sum(x => Convert.ToDecimal(x.DiscountedPrice) * x.Quantity);
            cartTotalBeforeVoucher = cartTotalBeforeVoucher + cartItems.DeliveryCharge;
            var shoppingVoucherMap = shoppingCartRepository.GetVoucherMap(cartItems.ShoppingCartID);
            decimal voucherAmount = 0;
            //if (shoppingVoucherMap.IsNotNull())
            //{
            //    voucherAmount = (decimal)shoppingVoucherMap.Amount;
            //}
            //if (cartTotalBeforeVoucher == voucherAmount)
            //{
            //    return Framework.Payment.PaymentGatewayType.VOUCHER;
            //}
            //else
            //{
                return (Framework.Payment.PaymentGatewayType)Convert.ToInt32(paymentMethod);
            //}
            //return Eduegate.Framework.Payment.PaymentGatewayType.COD;


        }

        public bool RemoveVoucherMap(long shoppingCartID)
        {
            var voucherMap = shoppingCartRepository.GetVoucherMap(shoppingCartID);
            if (voucherMap.IsNotNull())
            {
                return shoppingCartRepository.RemoveVoucherMap(voucherMap.ShoppingCartVoucherMapIID);
            }
            return true;
        }

        #endregion

        #region Private Methods
        private string GetCustomerID(CallContext callContext)
        {
            if (string.IsNullOrEmpty(callContext.EmailID) && string.IsNullOrEmpty(callContext.MobileNumber))
            {
                return callContext.GUID;
            }
            else
            {
                var customer = Convert.ToString(UtilityRepository.GetCustomerID(callContext.EmailID, callContext.MobileNumber));
                callContext.UserId = customer;
                return customer;
            }
        }

        private bool AddItem(CartProductDTO product, ShoppingCart userCart)
        {
            var newItem = new ShoppingCartItem();
            newItem.ProductSKUMapID = product.SKUID;
            newItem.Quantity = product.Quantity;
            newItem.ShoppingCartID = userCart.ShoppingCartIID;
            newItem.BranchID = product.BranchID;
            newItem.DeliveryTypeID = product.DeliveryTypeID;
            return shoppingCartRepository.AddItem(newItem);
        }

        #endregion

        public CartPaymentDetailsDTO GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID)
        {
            var mapper = new CartPaymentDetailsMapper();
            CartPaymentGateWayDetails result = shoppingCartRepository.GetCartStatusWithPaymentGateWayTrackKey(ShoppingCartID, PaymentGateWayID);
            result.PaymentGateWay = ((Framework.Payment.PaymentGatewayType)PaymentGateWayID).ToString(); 
            return mapper.ToDTO(result);
        }

        public void UpdateCartIsInternational(long shoppingCartIID,bool status)
        {
            shoppingCartRepository.UpdateCartIsInternational(shoppingCartIID, status);
        }

        public List<PaymentMethodDTO> GetPaymentBySitePaymentGroup(int siteID, bool isCustomerVerified, bool isLocal, bool isDigitalCart)
        {
            var paymentMethodConvertion = "PaymentMethod_" + siteID.ToString() + "_" + isCustomerVerified.ToString()
                + "_" + isLocal.ToString() + "_" + isDigitalCart.ToString();
            var paymentMethods = Framework.CacheManager.MemCacheManager<List<PaymentMethodDTO>>
                .Get(paymentMethodConvertion);

            if (paymentMethods == null || paymentMethods.Count() == 0)
            {
                var paymentMethodEntity = new Eduegate.Domain.Repository.ReferenceDataRepository()
                    .GetPaymentBySitePaymentGroup(siteID, isCustomerVerified, isLocal, isDigitalCart, _callContext.UserId);
                paymentMethods = PaymentMethodMapper.Mapper(_callContext).ToDTO(paymentMethodEntity);

                Framework.CacheManager.MemCacheManager<List<PaymentMethodDTO>>
                .Add(paymentMethods, "PaymentMethod_" + siteID.ToString() + "_" + isCustomerVerified.ToString()
                + "_" + isLocal.ToString() + "_" + isDigitalCart.ToString());
            }

            return paymentMethods;
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentExceptions(List<long> skuIds, int areaID, int siteID, int deliveryTypeID = 0)//we have to move deliverytypeID to generic way just for kuwait release doing this
        {
            var paymentMethods = Framework.CacheManager.MemCacheManager<List<Eduegate.Domain.Entity.Models.PaymentMethod>>
                .Get("PaymentMethod_" + string.Join("_", skuIds) + "_" + areaID.ToString()
                + "_" + siteID.ToString() + "_" + deliveryTypeID.ToString());

            if (paymentMethods == null)
            {
                paymentMethods = new Eduegate.Domain.Repository.ReferenceDataRepository().GetPaymentExceptions(skuIds, areaID, siteID, deliveryTypeID);
                Framework.CacheManager.MemCacheManager<List<Eduegate.Domain.Entity.Models.PaymentMethod>>
               .Add(paymentMethods, "PaymentMethod_" + string.Join("_", skuIds) + "_" + areaID.ToString()
                + "_" + siteID.ToString() + "_" + deliveryTypeID.ToString());
            }

            return paymentMethods;
        }

        public List<PaymentMethodDTO> GetPaymentMethods(int siteID)
        {
            var shoppingCratBl = new ShoppingCartBL(_callContext);
            var carts = shoppingCratBl.GetCart(_callContext);
            var contact = carts.ShippingAddressID.HasValue ? new AccountBL(_callContext).GetContactDetail(carts.ShippingAddressID.Value) : null;

            if (contact.IsNull() && carts.Products.IsNotNull() && carts.Products.Any(a => (a.DeliveryTypeID != (int)DeliveryTypes.Email) || (a.DeliveryTypeID != (int)DeliveryTypes.EmailInternationalDelivery)))
            {
                var login = new AccountBL(_callContext).GetLoginDetailByLoginID(Convert.ToInt64(_callContext.LoginID));
                if (login != null)
                {
                    carts.IsIntlCart = !login.RegisteredCountryID.HasValue ? false
                        : !shoppingCratBl.IsInternationalCartByCountryID(login.RegisteredCountryID.Value, siteID);
                }
            }

            //shoppingCratBl.UpdateCartIsInternational(carts.ShoppingCartID, carts.IsIntlCart);
            var paymentMethods = GetPaymentBySitePaymentGroup(siteID,
                new CustomerBL(_callContext).CustomerVerificatonCheck(Convert.ToInt64(carts.CustomerID)),
                !carts.IsIntlCart, carts.IsEmailDeliveryInCart);
            var paymentExceptions = carts.Products.IsNotNull() ?
                    GetPaymentExceptions(carts.Products.Select(x => x.SKUID).ToList(),
                    carts.ShippingAddressID.HasValue ? contact.IsNotNull() && contact.AreaID.HasValue ?
                    contact.AreaID.Value : 0 : 0, siteID, carts.IsStorePickUpInCart ? (int)DeliveryTypes.StorePickup :
                    (int)DeliveryTypes.None) : new List<Eduegate.Domain.Entity.Models.PaymentMethod>();
            paymentMethods.RemoveAll(x => paymentExceptions.Any(y => y.PaymentMethodID == x.PaymentMethodID));

            if (carts.Products.IsNotNull())
            {
                if (new SecurityBL(_callContext).HasClaimAccess(1004, (long)_callContext.LoginID))
                {
                    if (!paymentMethods.Exists(x => x.PaymentMethodID == (int)Eduegate.Framework.Enums.PaymentMethod.COD))
                    {
                        paymentMethods.Add(PaymentMethodMapper.Mapper(_callContext).ToDTO(new Eduegate.Domain.Repository.ReferenceDataRepository()
                            .GetPaymentCOD((short)Eduegate.Framework.Enums.PaymentMethod.COD)));
                    }
                }
            }
            else
            {
                return null;
            }


            return paymentMethods;
        }
     
    }
}
