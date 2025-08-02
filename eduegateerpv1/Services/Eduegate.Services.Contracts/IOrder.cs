using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrder" in both code and config file together.
    public interface IOrder
    {
        string ConfirmOnlineOrder(string paymentMethod, long trackID);

        string ConfirmOnlineOrderCartID(string paymentMethod, long trackID, long ShoppingCartIID, long CustomerID = 0, decimal deliveryCharge = -1);


        List<ContactDTO> GetOrderContacts(long orderID);

        DeliveryChargeDTO GetDeliveryCharges(long contactID);

        bool UpdateOrderTrackingStatus(OrderTrackingDTO track);

        List<OrderTrackingDTO> GetOrderTrack(long orderID);

        TransactionHeadDTO SaveWebsiteOrder(CheckoutPaymentDTO dto);

        long GetOrderIDFromOrderNo(string transactionNo);

        Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID);

        bool SetTemporaryBranchTransfer(long ShoppingCartID, bool isReverse);

        bool isOrderOfUser(long headID, long customerID);

        int GetActualOrderStatus(long headID);

        KeyValueDTO SaveCancelReplaceReturnRequest(long headID, List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO> orderDetails);
    }
}