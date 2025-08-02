using System.Runtime.Serialization;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Payments
{
    public class PaymentDTO
    {
        public string Guid { get; set; }
        public string Amount { get; set; }
        public string CustomerID { get; set; }
        public string EmailId { get; set; }
        public string PaymentID { get; set; }
        public string TrackKey { get; set; }
        public string TrackID { get; set; }
        public string SessionID { get; set; }
        public string TransactionStatus { get; set; }
        public TransactionStatus Status { get; set; }
        public string InitiatedOn { get; set; }
        public string InitiatedFromIP { get; set; }
        public string InitiatedLocation { get; set; }
        public string PaymentGatewayUrl { get; set; }
        public string SuccessReturnUrl { get; set; }
        public string CancelReturnUrl { get; set; }
        public string FailureReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
        public string OrderID { get; set; }
        public string CartID { get; set; }
        public string AdditionalDetails { get; set; }
        public PaymentGatewayType PaymentGateway { get; set; }
        public string CustomAttributes { get; set; }
        public bool IsPaymentMocked { get; set; }
        public VoucherWalletTransactionDTO VoucherTransactionDetail { get; set; }
        public string AppKey { get; set; }

    }

    public partial class VoucherWalletTransactionDTO
    {
        public long TransID { get; set; }
        public string VoucherNo { get; set; }
        public long WalletTransactionID { get; set; }
        public decimal Amount { get; set; }
    }
}
