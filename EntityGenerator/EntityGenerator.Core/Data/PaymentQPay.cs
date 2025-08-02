using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentQPay", Schema = "payment")]
    public partial class PaymentQPay
    {
        [Key]
        public long PaymentQPayIID { get; set; }
        public long? LoginID { get; set; }
        [StringLength(50)]
        public string SecureKey { get; set; }
        [StringLength(500)]
        public string SecureHash { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaymentAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionRequestDate { get; set; }
        [StringLength(50)]
        public string ActionID { get; set; }
        [StringLength(50)]
        public string BankID { get; set; }
        [StringLength(50)]
        public string NationalID { get; set; }
        [StringLength(50)]
        public string MerchantID { get; set; }
        [StringLength(20)]
        public string Lang { get; set; }
        [StringLength(50)]
        public string CurrencyCode { get; set; }
        [StringLength(500)]
        public string ExtraFields_f14 { get; set; }
        public int? Quantity { get; set; }
        [StringLength(50)]
        public string PaymentDescription { get; set; }
        [StringLength(150)]
        public string MerchantGatewayUrl { get; set; }
        [StringLength(50)]
        public string ResponseAcquirerID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ResponseAmount { get; set; }
        [StringLength(50)]
        public string ResponseBankID { get; set; }
        [StringLength(5)]
        public string ResponseCardExpiryDate { get; set; }
        [StringLength(50)]
        public string ResponseCardHolderName { get; set; }
        [StringLength(50)]
        public string ResponseCardNumber { get; set; }
        [StringLength(150)]
        public string ResponseConfirmationID { get; set; }
        [StringLength(50)]
        public string ResponseCurrencyCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResponseEZConnectResponseDate { get; set; }
        [StringLength(20)]
        public string ResponseLang { get; set; }
        [StringLength(50)]
        public string ResponseMerchantID { get; set; }
        [StringLength(50)]
        public string ResponseMerchantModuleSessionID { get; set; }
        [StringLength(50)]
        public string ResponsePUN { get; set; }
        [StringLength(50)]
        public string ResponseStatus { get; set; }
        [StringLength(500)]
        public string ResponseStatusMessage { get; set; }
        [StringLength(500)]
        public string ResponseSecureHash { get; set; }
        [StringLength(30)]
        public string LogType { get; set; }
    }
}
