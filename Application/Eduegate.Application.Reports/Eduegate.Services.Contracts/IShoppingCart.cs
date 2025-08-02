using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Framework;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.ShoppingCart;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShoppingCart" in both code and config file together.
    [ServiceContract]
    public interface IShoppingCart
    {
        #region Shoppingcart
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCart?TransactionHeadId={TransactionHeadId}&isDeliveryCharge={isDeliveryCharge}&isValidation={isValidation}&status={status}&customerID={customerID}")]
        CartDTO GetCart(string TransactionHeadId, bool isDeliveryCharge, bool isValidation = true, ShoppingCartStatus status = ShoppingCartStatus.InProcess, long customerID = 0);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CartPrice?customerID={customerID}")]
        CartDTO GetCartPrice(long customerID=0);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCartDetailbyIID?cartIID={cartIID}")]
        CartDTO GetCartDetailbyIID(long cartIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CartTotal?withCurrencyConversion={withCurrencyConversion}&isDeliveryCharge={isDeliveryCharge}&status={status}")]
        decimal GetCartTotal(bool withCurrencyConversion, bool isDeliveryCharge, ShoppingCartStatus status);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateCart")]
        bool UpdateCart(Eduegate.Services.Contracts.Catalog.CartProductDTO product);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateShoppingCart")]
        bool UpdateShoppingCart(CartDTO cart);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddToCart")]
        AddToCartStatusDTO AddToCart(Eduegate.Services.Contracts.Catalog.CartProductDTO product);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateCart")]
        CartDTO CreateCart(long customerID);
          
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RemoveItem?SKUID={SKUID}&customerID={customerID}")]
        bool RemoveItem(long SKUID, long customerID); 

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ProductCount")]
        int ProductCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MergeCart")]
        bool MergeCart();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateCartDelivery")]
        bool UpdateCartDelivery(Eduegate.Services.Contracts.Catalog.CartProductDTO product);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPaymentGatewayType?selectedPaymentOption={selectedPaymentOption}")]
        Eduegate.Framework.Payment.PaymentGatewayType GetPaymentGatewayType(string selectedPaymentOption);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeliveryTypeText?deliveryTypeID={deliveryTypeID}&deliverydays={deliverydays}")]
        string DeliveryTypeText(int deliveryTypeID, int deliverydays);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLoggedInUserCartID?status={status}")]
        long GetLoggedInUserCartID(ShoppingCartStatus status = ShoppingCartStatus.InProcess);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLoggedInUserCartIID?status={status}")]
        long GetLoggedInUserCartIID(ShoppingCartStatus status = ShoppingCartStatus.InProcess);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "IsUserCartExist?customerID={customerID}")]
        ShoppingCartDTO IsUserCartExist(long customerID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateCartStatus?cartID={cartID}&cartStatuID={cartStatuID}&paymentMethod={paymentMethod}&existingStatus={existingStatus}")]
        bool UpdateCartStatus(long cartID, ShoppingCartStatus cartStatuID, string paymentMethod, ShoppingCartStatus existingStatus);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateCartCustomerID?cartID={cartID}&customerID={customerID}")]
        bool UpdateCartCustomerID(long cartID, long customerID);
        #endregion

        #region Voucher
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ApplyVoucher")]
        VoucherDTO ApplyVoucher(string voucherNumber);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RemoveVoucherMap")]
        bool RemoveVoucherMap(long shoppingCartID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCartDetailByHeadID?orderID={orderID}")]
        string GetCartDetailByHeadID(long orderID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateCartPaymentGateWay?cartID={cartID}&cartStatuID={cartStatuID}&PaymentGateWayID={PaymentGateWayID}")]
        bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID, Int16 PaymentGateWayID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCartStatusWithPaymentGateWayTrackKey?ShoppingCartID={ShoppingCartID}&PaymentGateWayID={PaymentGateWayID}")]
        CartPaymentDetailsDTO GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateCartDescription")]
        ProcessOrderDTO UpdateCartDescription(ProcessOrderDTO processOrderDTO);

        #endregion
    }
}
