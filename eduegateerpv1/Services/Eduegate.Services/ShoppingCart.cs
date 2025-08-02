using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.ShoppingCart;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Services
{
    public class ShoppingCart : BaseService, IShoppingCart
    {
        ShoppingCartBL shoppingCartBL;

        public ShoppingCart()
        {
            shoppingCartBL = new ShoppingCartBL(CallContext);
        }

        #region Shoppingcart

        public CartDTO GetCart(string TransactionHeadId, bool isDeliveryCharge, bool isValidation = true, ShoppingCartStatus status = ShoppingCartStatus.InProcess,long  customerID=0)
        {
            try
            {
                var cart = shoppingCartBL.GetCart(CallContext, Convert.ToInt64(TransactionHeadId), isDeliveryCharge: isDeliveryCharge, isValidation: isValidation, cartStatus: status, customerIID: customerID);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(cart));
                return cart;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateCart(CartProductDTO product)
        {
            try
            {
                bool isShoppingCartUpdated = shoppingCartBL.UpdateCart(product, CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + isShoppingCartUpdated.ToString());
                return isShoppingCartUpdated;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateCartDelivery(CartProductDTO product)
        {
            try
            {
                bool isShoppingCartUpdated = shoppingCartBL.UpdateCartDelivery(product, CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + isShoppingCartUpdated.ToString());
                return isShoppingCartUpdated;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateShoppingCart(CartDTO cart)
        {
            try
            {
                bool isShoppingCartUpdated = shoppingCartBL.UpdateCart(cart, CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + isShoppingCartUpdated.ToString());
                return isShoppingCartUpdated;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public AddToCartStatusDTO AddToCart(CartProductDTO product)
        {
            try
            {
                var isShoppingCartUpdated = shoppingCartBL.AddToCart(product, CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + isShoppingCartUpdated.ToString());
                return isShoppingCartUpdated;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CartDTO CreateCart(long customerID)
        {  
            try 
            {
                var CartCreated = shoppingCartBL.CreateCart(customerID);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + CartCreated.ToString());
                return CartCreated;
            } 
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool RemoveItem(long SKUID,long customerID = 0)
        {
            try
            {
                bool isItemRemovedFromCart = shoppingCartBL.RemoveItem(SKUID, CallContext, customerID : customerID);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + isItemRemovedFromCart.ToString());
                return isItemRemovedFromCart;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool MergeCart()
        {
            var result = false;
            try
            {
                shoppingCartBL.MergeCart(CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + result.ToString());
                result = true;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                result = false;
            }
            return result;
        }

        public int ProductCount()
        {
            var result = 0;
            try
            {
                result = shoppingCartBL.ProductCount(CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + result.ToString());

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                result = 0;
            }

            return result;
        }

        public CartDTO GetCartPrice(long customerID=0)
        {
            return shoppingCartBL.GetCartPrice(CallContext, customerID);
        }

        public decimal GetCartTotal(bool withCurrencyConversion, bool isDeliveryCharge, ShoppingCartStatus cartStatus)
        {
            return shoppingCartBL.GetCartTotal(CallContext, withCurrencyConversion, isDeliveryCharge: isDeliveryCharge, cartStatus: cartStatus);
        }

        public Eduegate.Framework.Payment.PaymentGatewayType GetPaymentGatewayType(string selectedPaymentOption)
        {
            return shoppingCartBL.GetPaymentGatewayType(selectedPaymentOption);
        }

        public string DeliveryTypeText(int deliveryTypeID, int deliverydays)
        {
            return shoppingCartBL.DeliveryTypeText(deliveryTypeID, deliverydays);
        }

        #endregion


        #region Voucher
        public VoucherDTO ApplyVoucher(string voucherNumber)
        {
            try
            {
                var voucherResult = shoppingCartBL.ApplyVoucher(voucherNumber, CallContext);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(voucherResult));
                return voucherResult;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool RemoveVoucherMap(long shoppingCartID)
        {
            return shoppingCartBL.RemoveVoucherMap(shoppingCartID);
        }


        #endregion


        public long GetLoggedInUserCartID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            try
            {
                var cartID = shoppingCartBL.GetLoggedInUserCartID(status);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(cartID));
                return long.Parse(cartID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long GetLoggedInUserCartIID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            try
            {
                var cartID = shoppingCartBL.GetLoggedInUserCartIID(status);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(cartID));
                return cartID;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string GetCartDetailByHeadID(long orderID)
        {
            try
            {
                var cartID = shoppingCartBL.GetCartDetailByHeadID(orderID);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(cartID));
                return cartID;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ShoppingCartDTO IsUserCartExist(long customerID)
        {
            try
            {
                var dto = shoppingCartBL.IsUserCartExist(CallContext, customerID);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(customerID));
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateCartStatus(long cartID, ShoppingCartStatus cartStatuID, string paymentMethod, ShoppingCartStatus existingStatus = ShoppingCartStatus.None)
        {
            try
            {
                var updateCart = shoppingCartBL.UpdateCartStatus(cartID, cartStatuID, paymentMethod, existingStatus);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + Convert.ToString(cartID));
                return updateCart;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID, Int16 PaymentGateWayID)
        {
            try
            {
                bool isPaymentGateWayIDUpdated = shoppingCartBL.UpdateCartPaymentGateWay(cartID, cartStatuID, PaymentGateWayID);
                Eduegate.Logger.LogHelper<ShoppingCart>.Info("Service Result : " + isPaymentGateWayIDUpdated.ToString());
                return isPaymentGateWayIDUpdated;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public CartPaymentDetailsDTO GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID)
        {
            try
            {
                return shoppingCartBL.GetCartStatusWithPaymentGateWayTrackKey(ShoppingCartID, PaymentGateWayID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ProcessOrderDTO UpdateCartDescription(ProcessOrderDTO processOrderDTO)
        {
            try
            {
                return shoppingCartBL.UpdateCartDescription(processOrderDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateCartCustomerID(long cartID, long customerID)
        {
            try
            {
                return shoppingCartBL.UpdateCartCustomerID(cartID, customerID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CartDTO GetCartDetailbyIID(long cartIID)
        {
            try
            {
                return shoppingCartBL.GetCartDetailbyIID(cartIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CartDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
