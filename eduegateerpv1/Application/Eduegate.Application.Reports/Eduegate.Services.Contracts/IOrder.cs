using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrder" in both code and config file together.
    [ServiceContract]
    public interface IOrder
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConfirmOnlineOrder?paymentMethod={paymentMethod}&trackID={trackID}")]
        string ConfirmOnlineOrder(string paymentMethod, long trackID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConfirmOnlineOrderCartID?paymentMethod={paymentMethod}&trackID={trackID}&ShoppingCartIID={ShoppingCartIID}&CustomerID={CustomerID}&deliveryCharge={deliveryCharge}")]
        string ConfirmOnlineOrderCartID(string paymentMethod, long trackID, long ShoppingCartIID,long CustomerID = 0,decimal deliveryCharge = -1);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOrderContacts?orderID={orderID}")]
        List<ContactDTO> GetOrderContacts(long orderID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDeliveryCharges?contactID={contactID}")]
        DeliveryChargeDTO GetDeliveryCharges(long contactID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateOrderTrackingStatus")]
        bool UpdateOrderTrackingStatus(OrderTrackingDTO track);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOrderTrack?orderID={orderID}")]
        List<OrderTrackingDTO> GetOrderTrack(long orderID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveWebsiteOrder")]
        TransactionHeadDTO SaveWebsiteOrder(CheckoutPaymentDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOrderIDFromOrderNo?transactionNo={transactionNo}")]
        long GetOrderIDFromOrderNo(string transactionNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDeliveryType?headID={headID}")]
        Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SetTemporaryBranchTransfer?ShoppingCartID={ShoppingCartID}&isReverse={isReverse}")]
        bool SetTemporaryBranchTransfer(long ShoppingCartID, bool isReverse);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "isOrderOfUser?headID={headID}&customerID={customerID}")]
        bool isOrderOfUser(long headID, long customerID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetActualOrderStatus?headID={headID}")]
        int GetActualOrderStatus(long headID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCancelReplaceReturnRequest?headID={headID}")]
        KeyValueDTO SaveCancelReplaceReturnRequest(long headID, List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO> orderDetails);
    }
}
