using System;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.Payments;

namespace Eduegate.Services.Client
{
    public class PaymentGatewayServiceClient : BaseClient, IPaymentGatewayService
    {
        public PaymentGatewayServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public string GenerateCardSessionID(byte? paymentModeID = null)
        {
            throw new NotImplementedException();
        }

        public string GenerateCardSessionIDByTransactionNo(string transactionNo, byte? paymentModeID = null)
        {
            throw new NotImplementedException();
        }

        public string PaymentValidation()
        {
            throw new NotImplementedException();
        }
        public string PaymentqQPayValidation()
        {
            throw new NotImplementedException();
        }

        public void LogPaymentLog(PaymentLogDTO paymentLog)
        {
            throw new NotImplementedException();
        }

        public string ValidatePaymentByTransaction(string transID, byte? paymentModeID = null, decimal? totalAmountCollected = null)
        {
            throw new NotImplementedException();
        }

        public string PaymentQPayValidation()
        {
            throw new NotImplementedException();
        }
    }
}