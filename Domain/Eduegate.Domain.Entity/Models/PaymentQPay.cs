using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentQPay", Schema = "payment")]
    public partial class PaymentQPay
    {
        [Key]
        public long PaymentQPayIID { get; set; }
        public long? LoginID { get; set; }
        public string SecureKey { get; set; }
        public string SecureHash { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime? TransactionRequestDate { get; set; }
        public string ActionID { get; set; }
        public string BankID { get; set; }
        public string NationalID { get; set; }
        public string MerchantID { get; set; }
        public string Lang { get; set; }
        public string CurrencyCode { get; set; }
        public string ExtraFields_f14 { get; set; }
        public int? Quantity { get; set; }
        public string PaymentDescription { get; set; }
        public string MerchantGatewayUrl { get; set; }
        public string ResponseAcquirerID { get; set; }
        public decimal? ResponseAmount { get; set; }
        public string ResponseBankID { get; set; }
        public string ResponseCardExpiryDate { get; set; }
        public string ResponseCardHolderName { get; set; }
        public string ResponseCardNumber { get; set; }
        public string ResponseConfirmationID { get; set; }
        public string ResponseCurrencyCode { get; set; }
        public DateTime? ResponseEZConnectResponseDate { get; set; }
        public string ResponseLang { get; set; }
        public string ResponseMerchantID { get; set; }
        public string ResponseMerchantModuleSessionID { get; set; }
        public string ResponsePUN { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseStatusMessage { get; set; }
        public string ResponseSecureHash { get; set; }
        public string LogType { get; set; }
    }
}