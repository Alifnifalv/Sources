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

namespace Eduegate.Service.Client
{
    public class OrderServiceClient : BaseClient, IOrder
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string orderService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.ORDER_SERVICE_NAME);

        public OrderServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public string ConfirmOnlineOrder(string paymentMethod, long trackID)
        {
            var uri = string.Format("{0}/{1}?paymentMethod={2}&trackID={3}", orderService, "ConfirmOnlineOrder", paymentMethod, trackID);
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext, _logger);
        }

        public string ConfirmOnlineOrderCartID(string paymentMethod, long trackID, long ShoppingCartIID,long CustomerID =0,decimal deliveryCharge = -1)
        {
            var uri = string.Format("{0}/{1}?paymentMethod={2}&trackID={3}&ShoppingCartIID={4}&CustomerID={5}&deliveryCharge={6}", orderService, "ConfirmOnlineOrderCartID", paymentMethod, trackID, ShoppingCartIID,CustomerID,deliveryCharge);
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext, _logger);
        }

        public List<ContactDTO> GetOrderContacts(long orderID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderTrackingStatus(OrderTrackingDTO track)
        {
            var uri = string.Format("{0}/{1}", orderService, "UpdateOrderTrackingStatus");
            return bool.Parse(ServiceHelper.HttpPostRequest<OrderTrackingDTO>(uri, track, _callContext));
        }

        public List<OrderTrackingDTO> GetOrderTrack(long orderID)
        {
            var uri = string.Format("{0}/{1}?orderID={2}", orderService, "GetOrderTrack", orderID);
            return ServiceHelper.HttpGetRequest<List<OrderTrackingDTO>>(uri, _callContext, _logger);
        }

        public DeliveryChargeDTO GetDeliveryCharges(long contactID)
        {
            throw new NotImplementedException();
        }

        public TransactionHeadDTO SaveWebsiteOrder(CheckoutPaymentDTO dto)
        {
            var result = ServiceHelper.HttpPostRequest(orderService + "SaveWebsiteOrder", dto, _callContext);
            return JsonConvert.DeserializeObject<TransactionHeadDTO>(result);
        }

        public long GetOrderIDFromOrderNo(string transactionNo)
        {
            var uri = string.Format("{0}/{1}?transactionNo={2}", orderService, "GetOrderIDFromOrderNo", transactionNo);
            return ServiceHelper.HttpGetRequest<long>(uri, _callContext, _logger);
        }

        public Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID)
        {
            var uri = string.Format("{0}/{1}?headID={2}", orderService, "GetDeliveryType", headID);
            return ServiceHelper.HttpGetRequest<Eduegate.Framework.Helper.Enums.DeliveryTypes>(uri, _callContext, _logger);
        }

        public bool SetTemporaryBranchTransfer(long shoppingCartID, bool isReverse)
        {
            var uri = string.Format("{0}/{1}?ShoppingCartID={2}&isReverse={3}", orderService, "SetTemporaryBranchTransfer", shoppingCartID, isReverse);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public bool isOrderOfUser(long headID, long customerID)
        {
            var uri = string.Format("{0}/{1}?headID={2}&customerID={3}", orderService, "isOrderOfUser", headID, customerID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public int GetActualOrderStatus(long headID)
        {
            var uri = string.Format("{0}/{1}?headID={2}", orderService, "GetActualOrderStatus", headID);
            return ServiceHelper.HttpGetRequest<int>(uri, _callContext, _logger);
        }
        public KeyValueDTO SaveCancelReplaceReturnRequest(long headID, List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO> orderDetails)
        {
            var uri = string.Format("{0}/{1}?headID={2}", orderService, "SaveCancelReplaceReturnRequest", headID);
            //var result = ServiceHelper.HttpPostSerializedObject(uri, JsonConvert.SerializeObject(orderDetails), _callContext);
            var result = ServiceHelper.HttpPostRequest(uri, orderDetails, _callContext);
            return JsonConvert.DeserializeObject<KeyValueDTO>(result);
        }
    }
}
