using Eduegate.Services.Contracts.Payments;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPaymentGatewayService" in both code and config file together.
    public interface IPaymentGatewayService
    {
        string GenerateCardSessionID(byte? paymentModeID = null);

        string GenerateCardSessionIDByTransactionNo(string transactionNo, byte? paymentModeID = null);

        string PaymentValidation();
        string PaymentQPayValidation();

        void LogPaymentLog(PaymentLogDTO paymentLog);

        string ValidatePaymentByTransaction(string transID, byte? paymentModeID = null, decimal? totalAmountCollected = null);
    }
}