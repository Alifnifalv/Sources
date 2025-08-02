using System;
using System.Collections.Generic;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Checkout;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class OrderServiceClient : BaseClient, IOrder
    {
        Order service = new Order();

        public OrderServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
            service.CallContext = callContext;
        }

        public string ConfirmOnlineOrder(string paymentMethod, long trackID)
        {
            return service.ConfirmOnlineOrder(paymentMethod, trackID);
        }

        public string ConfirmOnlineOrderCartID(string paymentMethod, long trackID, long ShoppingCartIID,long CustomerID =0,decimal deliveryCharge = -1)
        {
            return service.ConfirmOnlineOrder(paymentMethod, trackID);
        }

        public List<ContactDTO> GetOrderContacts(long orderID)
        {
            return service.GetOrderContacts(orderID);
        }

        public bool UpdateOrderTrackingStatus(OrderTrackingDTO track)
        {
            return service.UpdateOrderTrackingStatus(track);
        }

        public List<OrderTrackingDTO> GetOrderTrack(long orderID)
        {
            return service.GetOrderTrack(orderID);
        }

        public DeliveryChargeDTO GetDeliveryCharges(long contactID)
        {
            return service.GetDeliveryCharges(contactID);
        }

        public TransactionHeadDTO SaveWebsiteOrder(CheckoutPaymentDTO dto)
        {
            return service.SaveWebsiteOrder(dto);
        }

        public long GetOrderIDFromOrderNo(string transactionNo)
        {
            return service.GetOrderIDFromOrderNo(transactionNo);
        }

        public Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID)
        {
            return service.GetDeliveryType(headID);
        }

        public bool SetTemporaryBranchTransfer(long shoppingCartID, bool isReverse)
        {
            return service.SetTemporaryBranchTransfer(shoppingCartID, isReverse);
        }

        public bool isOrderOfUser(long headID, long customerID)
        {
            return service.isOrderOfUser(headID, customerID);
        }

        public int GetActualOrderStatus(long headID)
        {
            return service.GetActualOrderStatus(headID);
        }

        public KeyValueDTO SaveCancelReplaceReturnRequest(long headID, List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO> orderDetails)
        {
            return service.SaveCancelReplaceReturnRequest(headID, orderDetails);
        }
    }
}
