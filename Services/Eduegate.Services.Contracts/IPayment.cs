using Eduegate.Services.Contracts;

namespace Eduegate.Services.Payment
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPayment" in both code and config file together.
    public interface IPayment
    {
        bool CreatePaymentRequest(PaymentDTO paymentTransaction);

        bool UpdatePaymentResponse(PaymentDTO paymentTransaction);

        string GetReturnUrl(string trackId, string gatewayType);

        PaymentDTO GetPaymentDetails(long orderID, string track);

        PaypalPaymentDTO GetPayPalPaymentDetail(long trackID, long trackKey, long customerID);

        long GetFortCustomerID(string trackKey, string email);

        bool UpdatePayPalIPNStatus(PaypalPaymentDTO paymentDto);

        bool UpdatePDTData(PaypalPaymentDTO paymentDto);
    }
}