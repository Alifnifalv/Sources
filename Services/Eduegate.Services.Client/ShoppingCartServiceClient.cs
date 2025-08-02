using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Extensions;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.ShoppingCart;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Service.Client
{
    public class ShoppingCartServiceClient : BaseClient, IShoppingCart
    {
        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _shoppingCartService = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.SHOPPING_CART_SERVICE_NAME);

        public ShoppingCartServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }
        public CartDTO GetCart(string TransactionHeadId, bool isDeliveryCharge, bool isValidation=true, ShoppingCartStatus status = ShoppingCartStatus.InProcess,long customerID=0) 
        {
            var result = ServiceHelper.HttpGetRequest(_shoppingCartService + "GetCart?TransactionHeadId=" + TransactionHeadId + "&isDeliveryCharge=" + isDeliveryCharge + "&isValidation=" + isValidation + "&status=" + status + "&customerID=" + customerID, _callContext);
            return JsonConvert.DeserializeObject<CartDTO>(result);
        }

        public bool UpdateCart(CartProductDTO product)
        {
            var result = ServiceHelper.HttpPostRequest(_shoppingCartService + "UpdateCart", product, _callContext);
            result = JsonConvert.DeserializeObject<string>(result);
            return Convert.ToBoolean(result);
        }

        public bool UpdateCartDelivery(CartProductDTO product)
        {
            var shoppingCartDetails = ServiceHelper.HttpPostRequest(_shoppingCartService + "UpdateCartDelivery", product, _callContext);
            shoppingCartDetails = JsonConvert.DeserializeObject<string>(shoppingCartDetails);
            return Convert.ToBoolean(shoppingCartDetails);
        }

        public bool UpdateShoppingCart(CartDTO cart)
        {
            var shoppingCartDetails = ServiceHelper.HttpPostRequest(_shoppingCartService + "UpdateShoppingCart", cart, _callContext);
            shoppingCartDetails = JsonConvert.DeserializeObject<string>(shoppingCartDetails);
            return Convert.ToBoolean(shoppingCartDetails);
        }

        public AddToCartStatusDTO AddToCart(CartProductDTO product)
        {
            var shoppingCartDetails = ServiceHelper.HttpPostRequest(_shoppingCartService + "AddToCart", product, _callContext);
            return JsonConvert.DeserializeObject<AddToCartStatusDTO>(shoppingCartDetails);
        }
         
        public CartDTO CreateCart(long customerID)
        {
            var createCartDetails = ServiceHelper.HttpPostRequest(_shoppingCartService + "CreateCart", customerID,  _callContext);
            return JsonConvert.DeserializeObject<CartDTO>(createCartDetails);
        } 

        public bool RemoveItem(long SKUID,long customerID = 0)
        {
            string isItemRemoved = string.Format("{0}/RemoveItem?SKUID={1}&customerID={2}", _shoppingCartService, SKUID, customerID);
            isItemRemoved = ServiceHelper.HttpGetRequest<string>(isItemRemoved, _callContext);
            return Convert.ToBoolean(isItemRemoved);
        }

        public bool MergeCart()
        {
            return false;
        }

        public int ProductCount()
        {
            var productCount = 0;
            var serviceResponse = ServiceHelper.HttpGetRequest(_shoppingCartService + "ProductCount", _callContext);
            return productCount = JsonConvert.DeserializeObject<int>(serviceResponse);
        }

        public CartDTO GetCartPrice(long customerID = 0)
        {
            var requestUri = string.Format("{0}/CartPrice?customerID={1}", _shoppingCartService, customerID);
            return ServiceHelper.HttpGetRequest<CartDTO>(requestUri, _callContext);
        }

        public decimal GetCartTotal(bool withCurrencyConversion, bool isDeliveryCharge, ShoppingCartStatus status)
        {
            var requestUri = string.Format("{0}/CartTotal?withCurrencyConversion={1}&isDeliveryCharge={2}&status={3}", _shoppingCartService, withCurrencyConversion, isDeliveryCharge, status);
            return ServiceHelper.HttpGetRequest<decimal>(requestUri, _callContext);
        }

        public VoucherDTO ApplyVoucher(string voucherNumber)
        {
            return null;
        }

        public Eduegate.Framework.Payment.PaymentGatewayType GetPaymentGatewayType(string selectedPaymentOption)
        {
            var url = string.Format("{0}/{1}?selectedPaymentOption={2}", _shoppingCartService, "GetPaymentGatewayType", selectedPaymentOption);
            return ServiceHelper.HttpGetRequest<Eduegate.Framework.Payment.PaymentGatewayType>(url, _callContext, _logger);
        }

        public bool RemoveVoucherMap(long shoppingCartID)
        {
            var url = string.Format("{0}/{1}", _shoppingCartService, "RemoveVoucherMap");
            var result = ServiceHelper.HttpPostRequest(url, shoppingCartID, _callContext);
            return Convert.ToBoolean(result);
        }

        public string DeliveryTypeText(int deliveryTypeID, int deliverydays)
        {
            var url = string.Format("{0}/{1}", _shoppingCartService, "DeliveryTypeText");
            return ServiceHelper.HttpGetRequest<string>(url, _callContext, _logger);
        }

        public long GetLoggedInUserCartID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            var url = string.Format("{0}/{1}?status={2}", _shoppingCartService, "GetLoggedInUserCartID", status);
            return ServiceHelper.HttpGetRequest<long>(url, _callContext);
        }

        public string GetCartDetailByHeadID(long orderID)
        {
            var url = string.Format("{0}/{1}?orderID={2}", _shoppingCartService, "GetCartDetailByHeadID", orderID);
            return ServiceHelper.HttpGetRequest(url, _callContext);
        }

        public long GetLoggedInUserCartIID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            var url = string.Format("{0}/{1}?status={2}", _shoppingCartService, "GetLoggedInUserCartIID", status);
            return ServiceHelper.HttpGetRequest<long>(url, _callContext);
        }

        public ShoppingCartDTO IsUserCartExist(long customerID)
        {
            var url = string.Format("{0}/{1}?customerID={2}", _shoppingCartService, "IsUserCartExist", customerID);
            return ServiceHelper.HttpGetRequest<ShoppingCartDTO>(url, _callContext);
        }

        public bool UpdateCartStatus(long cartID, ShoppingCartStatus cartStatuID, string paymentMethod, ShoppingCartStatus existingStatus = ShoppingCartStatus.None)
        {
            var url = string.Format("{0}/{1}?cartID={2}&cartStatuID={3}&paymentMethod={4}&existingStatus={5}", _shoppingCartService, "UpdateCartStatus", cartID, cartStatuID, paymentMethod, existingStatus);
            return ServiceHelper.HttpGetRequest<bool>(url, _callContext);
        }

        public bool UpdateCartCustomerID(long cartID, long customerID)
        {
            var url = string.Format("{0}/{1}?cartID={2}&customerID={3}", _shoppingCartService, "UpdateCartCustomerID", cartID, customerID);
            return ServiceHelper.HttpGetRequest<bool>(url, _callContext);
        }

        public bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID,Int16 PaymentGateWayID)
        {
            var url = string.Format("{0}/{1}?cartID={2}&cartStatuID={3}&PaymentGateWayID={4}", _shoppingCartService, "UpdateCartPaymentGateWay", cartID, cartStatuID, PaymentGateWayID);
            return ServiceHelper.HttpGetRequest<bool>(url, _callContext);
        }
        public CartPaymentDetailsDTO GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID)
        {
            var url = string.Format("{0}/{1}?ShoppingCartID={2}&PaymentGateWayID={3}", _shoppingCartService, "GetCartStatusWithPaymentGateWayTrackKey", ShoppingCartID, PaymentGateWayID);
            return ServiceHelper.HttpGetRequest<CartPaymentDetailsDTO>(url, _callContext);
        }

        public ProcessOrderDTO UpdateCartDescription(ProcessOrderDTO processOrderDTO)
        {
            var url = string.Format("{0}/{1}", _shoppingCartService, "UpdateCartDescription");
            return ServiceHelper.HttpPostGetRequest<ProcessOrderDTO>(string.Concat(_shoppingCartService, "UpdateCartDescription"), processOrderDTO, _callContext);
        }

        public CartDTO GetCartDetailbyIID(long cartIID)
        {
            var url = string.Format("{0}/{1}", _shoppingCartService, "GetCartDetailbyIID?cartIID={2}");
            return ServiceHelper.HttpGetRequest<CartDTO>(string.Concat(_shoppingCartService, "GetCartDetailbyIID", cartIID), _callContext);
        }
    }
}
