using System;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.ShoppingCart;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShoppingCart" in both code and config file together.
    public interface IShoppingCart
    {
        #region Shoppingcart
        CartDTO GetCart(string TransactionHeadId, bool isDeliveryCharge, bool isValidation = true, ShoppingCartStatus status = ShoppingCartStatus.InProcess, long customerID = 0);

        CartDTO GetCartPrice(long customerID = 0);

        CartDTO GetCartDetailbyIID(long cartIID);

        decimal GetCartTotal(bool withCurrencyConversion, bool isDeliveryCharge, ShoppingCartStatus status);

        bool UpdateCart(Eduegate.Services.Contracts.Catalog.CartProductDTO product);

        bool UpdateShoppingCart(CartDTO cart);

        AddToCartStatusDTO AddToCart(Eduegate.Services.Contracts.Catalog.CartProductDTO product);

        CartDTO CreateCart(long customerID);
          
        bool RemoveItem(long SKUID, long customerID); 

        int ProductCount();

        bool MergeCart();

        bool UpdateCartDelivery(Eduegate.Services.Contracts.Catalog.CartProductDTO product);

        Eduegate.Framework.Payment.PaymentGatewayType GetPaymentGatewayType(string selectedPaymentOption);

        string DeliveryTypeText(int deliveryTypeID, int deliverydays);

        long GetLoggedInUserCartID(ShoppingCartStatus status = ShoppingCartStatus.InProcess);

        long GetLoggedInUserCartIID(ShoppingCartStatus status = ShoppingCartStatus.InProcess);

        ShoppingCartDTO IsUserCartExist(long customerID);

        bool UpdateCartStatus(long cartID, ShoppingCartStatus cartStatuID, string paymentMethod, ShoppingCartStatus existingStatus);

        bool UpdateCartCustomerID(long cartID, long customerID);
        #endregion

        #region Voucher
        VoucherDTO ApplyVoucher(string voucherNumber);

        bool RemoveVoucherMap(long shoppingCartID);

        string GetCartDetailByHeadID(long orderID);

        bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID, Int16 PaymentGateWayID);

        CartPaymentDetailsDTO GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID);

        ProcessOrderDTO UpdateCartDescription(ProcessOrderDTO processOrderDTO);

        #endregion
    }
}