using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services
{
    public class Order : BaseService, IOrder
    {
        private OrderBL orderBL;
        public Order()
        {
           orderBL = new OrderBL(CallContext);
        }

        public string ConfirmOnlineOrder(string paymentMethod, long trackID)
        {
            try
            {
                var transId = orderBL.ConfirmOnlineOrder(paymentMethod, trackID, CallContext);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(transId));
                return transId;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string ConfirmOnlineOrderCartID(string paymentMethod, long trackID, long ShoppingCartIID,long CustomerID = 0,decimal deliveryCharge = -1)
        {
            try
            {
                var transId = orderBL.ConfirmOnlineOrder(paymentMethod, trackID, CallContext, ShoppingCartIID,null,"",CustomerID,deliveryCharge:deliveryCharge);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(transId));
                return transId;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ContactDTO> GetOrderContacts(long orderID)
        {
            try
            {
                var contacts = orderBL.GetOrderContacts(orderID);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(contacts));
                return contacts;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<OrderTrackingDTO> GetOrderTrack(long orderID)
        {
            try
            {
                var result = orderBL.GetOrderTrack(orderID, base.CallContext);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public DeliveryChargeDTO GetDeliveryCharges(long contactID)
        {
            return orderBL.GetDeliveryCharges(contactID);
        }
       
        public bool UpdateOrderTrackingStatus(OrderTrackingDTO track)
        {
            try
            {
                var result = orderBL.UpdateOrderTrackingStatus(track, base.CallContext);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public TransactionHeadDTO SaveWebsiteOrder(CheckoutPaymentDTO dto)
        {
            try
            {
                return null;
                //var result = new WebsiteOrderBL(CallContext).SaveWebsiteOrder(dto);
                //Eduegate.Logger.LogHelper<TransactionHeadDTO>.Info("Service Result : " + Convert.ToString(result));
                //return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<TransactionHeadDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long GetOrderIDFromOrderNo(string transactionNo)
        {
            try
            {
                var result = orderBL.GetOrderIDFromOrderNo(transactionNo);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID)
        {
            try
            {
                var result = orderBL.GetDeliveryType(headID);
                Eduegate.Logger.LogHelper<Eduegate.Framework.Helper.Enums.DeliveryTypes>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Eduegate.Framework.Helper.Enums.DeliveryTypes>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SetTemporaryBranchTransfer(long ShoppingCartID, bool isReverse)
        {
            try
            {
                var result = orderBL.SetTemporaryBranchTransfer(ShoppingCartID, isReverse);
                Eduegate.Logger.LogHelper<Order>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Order>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool isOrderOfUser(long headID, long customerID)
        {
            try
            {
                var result = orderBL.isOrderOfUser(headID,customerID);
                Eduegate.Logger.LogHelper<bool>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<bool>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public int GetActualOrderStatus(long headID)
        {
            try
            {
                var result = orderBL.GetActualOrderStatus(headID);
                Eduegate.Logger.LogHelper<int>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<int>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public KeyValueDTO SaveCancelReplaceReturnRequest(long headID, List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO> orderDetails)
        {
            try
            {
                return orderBL.SaveCancelReplaceReturnRequest(headID, orderDetails);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<bool>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
