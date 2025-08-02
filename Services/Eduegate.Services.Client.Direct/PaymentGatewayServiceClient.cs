using System;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Payments;

namespace Eduegate.Services.Client.Direct
{
    public class PaymentGatewayServiceClient : IPaymentGatewayService
    {
        PaymentGatewayService service = new PaymentGatewayService();

        public PaymentGatewayServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public string GenerateCardSessionID(byte? paymentModeID = null)
        {
            return service.GenerateCardSessionID(paymentModeID);
        }

        public string GenerateCardSessionIDByTransactionNo(string transactionNo, byte? paymentModeID = null)
        {
            return service.GenerateCardSessionIDByTransactionNo(transactionNo, paymentModeID);
        }

        public string PaymentValidation()
        {
            return service.PaymentValidation();
        }

        public string PaymentQPayValidation()
        {
            return service.PaymentQPayValidation();
        }

        public void LogPaymentLog(PaymentLogDTO paymentLog)
        {
            service.LogPaymentLog(paymentLog);
        }

        public string ValidatePaymentByTransaction(string transID, byte? paymentModeID = null, decimal? totalAmountCollected = null)
        {
            return service.ValidatePaymentByTransaction(transID, paymentModeID, totalAmountCollected);
        }

    }
}