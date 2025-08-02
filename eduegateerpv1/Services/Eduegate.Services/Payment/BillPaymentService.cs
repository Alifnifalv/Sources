using Eduegate.Domain;
using Eduegate.Domain.Payment;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Contracts.Payments;

namespace Eduegate.Services.Payment
{
    public class BillPaymentService : BaseService
    {
        public BillingResponseDTO GetBillingInformation(string agentId, string token, string timeStamp,
            string StudentRollNumber, string ChildQID)
        {
            return new BillPaymentBL(CallContext)
                .GetBillingInformation(agentId, token, timeStamp, StudentRollNumber, ChildQID);
        }

        public PaymentResponseDTO MakePayment(BankBillPaymentDTO payment)
        {
            return new BillPaymentBL(CallContext).MakePayment(payment);
        }
        public string PendingQPayPaymentProcess()
        {
            return new PaymentGatewayBL(CallContext).PendingQPayPaymentProcess();
        }
    }
}