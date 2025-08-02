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
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class ShoppingCartServiceClient : BaseClient, IShoppingCart
    {
        ShoppingCart service = new ShoppingCart();

        public ShoppingCartServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }
        public CartDTO GetCart(string TransactionHeadId, bool isDeliveryCharge, bool isValidation=true, ShoppingCartStatus status = ShoppingCartStatus.InProcess,long customerID=0) 
        {
            return service.GetCart(TransactionHeadId, isDeliveryCharge, isValidation, status, customerID);           
        }

        public bool UpdateCart(CartProductDTO product)
        {
            return service.UpdateCart(product);
        }

        public bool UpdateCartDelivery(CartProductDTO product)
        {
            return service.UpdateCartDelivery(product);
        }

        public bool UpdateShoppingCart(CartDTO cart)
        {
            return service.UpdateShoppingCart(cart);
        }

        public AddToCartStatusDTO AddToCart(CartProductDTO product)
        {
            return service.AddToCart(product);
        }
         
        public CartDTO CreateCart(long customerID)
        {
            return service.CreateCart(customerID);
        } 

        public bool RemoveItem(long SKUID,long customerID = 0)
        {
            return service.RemoveItem(SKUID, customerID);
        }

        public bool MergeCart()
        {
            return false;
        }

        public int ProductCount()
        {
            return service.ProductCount();
        }

        public CartDTO GetCartPrice(long customerID = 0)
        {
            return service.GetCartPrice(customerID);
        }

        public decimal GetCartTotal(bool withCurrencyConversion, bool isDeliveryCharge, ShoppingCartStatus status)
        {
            return service.GetCartTotal(withCurrencyConversion, isDeliveryCharge, status);
        }

        public VoucherDTO ApplyVoucher(string voucherNumber)
        {
            return service.ApplyVoucher(voucherNumber);
        }

        public Eduegate.Framework.Payment.PaymentGatewayType GetPaymentGatewayType(string selectedPaymentOption)
        {
            return service.GetPaymentGatewayType(selectedPaymentOption);
        }

        public bool RemoveVoucherMap(long shoppingCartID)
        {
            return service.RemoveVoucherMap(shoppingCartID);
        }

        public string DeliveryTypeText(int deliveryTypeID, int deliverydays)
        {
            return service.DeliveryTypeText(deliveryTypeID, deliverydays);
        }

        public long GetLoggedInUserCartID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            return service.GetLoggedInUserCartID(status);
        }

        public string GetCartDetailByHeadID(long orderID)
        {
            return service.GetCartDetailByHeadID(orderID);
        }

        public long GetLoggedInUserCartIID(ShoppingCartStatus status = ShoppingCartStatus.InProcess)
        {
            return service.GetLoggedInUserCartIID(status);
        }

        public ShoppingCartDTO IsUserCartExist(long customerID)
        {
            return service.IsUserCartExist(customerID);
        }

        public bool UpdateCartStatus(long cartID, ShoppingCartStatus cartStatuID, string paymentMethod, ShoppingCartStatus existingStatus = ShoppingCartStatus.None)
        {
            return service.UpdateCartStatus(cartID, cartStatuID, paymentMethod, existingStatus);
        }

        public bool UpdateCartCustomerID(long cartID, long customerID)
        {
            return service.UpdateCartCustomerID(cartID, customerID);
        }

        public bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID,Int16 PaymentGateWayID)
        {
            return service.UpdateCartPaymentGateWay(cartID, cartStatuID, PaymentGateWayID);
        }
        public CartPaymentDetailsDTO GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID)
        {
            return service.GetCartStatusWithPaymentGateWayTrackKey(ShoppingCartID, PaymentGateWayID);
        }

        public ProcessOrderDTO UpdateCartDescription(ProcessOrderDTO processOrderDTO)
        {
            return service.UpdateCartDescription(processOrderDTO);
        }

        public CartDTO GetCartDetailbyIID(long cartIID)
        {
            return service.GetCartDetailbyIID(cartIID);
        }
    }
}
