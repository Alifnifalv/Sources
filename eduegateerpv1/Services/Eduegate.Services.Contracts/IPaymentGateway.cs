namespace Eduegate.Services.Contracts
{
    public interface IPaymentGateway
    {
        bool CreatePaymentRequest(PaymentDTO paymentTransaction);

        bool UpdatePaymentResponse(PaymentDTO paymentTransaction);

        string GetReturnUrl(string trackId, string gatewayType);
    }
}