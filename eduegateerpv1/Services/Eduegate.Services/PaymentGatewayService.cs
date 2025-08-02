using Eduegate.Domain.Mappers.Payment;
using Eduegate.Domain.Payment;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Payments;

namespace Eduegate.Services
{
    public class PaymentGatewayService : BaseService, IPaymentGatewayService
    {
        public string GenerateCardSessionID(byte? paymentModeID = null)
        {
            return new PaymentGatewayBL(this.CallContext).GenerateCardSessionID(paymentModeID);
        }

        public string GenerateCardSessionIDByTransactionNo(string transactionNo, byte? paymentModeID = null)
        {
            return new PaymentGatewayBL(this.CallContext).GenerateCardSessionIDByTransactionNo(transactionNo, paymentModeID);
        }

        public string PaymentValidation()
        {
            return new PaymentGatewayBL(this.CallContext).PaymentValidation();
        }
        public string PaymentQPayValidation()
        {
            return new PaymentGatewayBL(this.CallContext).PaymentQPayValidation();
        }

        public void LogPaymentLog(PaymentLogDTO paymentLog)
        {
            PaymentLogMapper.Mapper(CallContext).LogPaymentLog(paymentLog);
        }

        public string ValidatePaymentByTransaction(string transID, byte? paymentModeID = null, decimal? totalAmountCollected = null)
        {
            return new PaymentGatewayBL(this.CallContext).ValidatePaymentByTransaction(transID, paymentModeID, totalAmountCollected);
        }

    }
}