using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentDetailsPayPal", Schema = "payment")]
    public partial class PaymentDetailsPayPal
    {
        [Key]
        public long TrackID { get; set; }
        [Key]
        public long TrackKey { get; set; }
        public long RefCustomerID { get; set; }
        [Required]
        [StringLength(50)]
        public string BusinessEmail { get; set; }
        public long PaymentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InitOn { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string InitStatus { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string InitIP { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string InitLocation { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal InitAmount { get; set; }
        [Required]
        [StringLength(3)]
        public string InitCurrency { get; set; }
        public bool IpnVerified { get; set; }
        [StringLength(50)]
        public string TransID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TransAmount { get; set; }
        [StringLength(3)]
        public string TransCurrency { get; set; }
        [StringLength(20)]
        public string TransStatus { get; set; }
        [StringLength(20)]
        public string TransPayerID { get; set; }
        [StringLength(30)]
        public string TransDateTime { get; set; }
        [StringLength(20)]
        public string TransPayerStatus { get; set; }
        [StringLength(30)]
        public string TransPayerEmail { get; set; }
        [StringLength(10)]
        public string TransPaymentType { get; set; }
        [StringLength(30)]
        public string TransMessage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransOn { get; set; }
        [StringLength(50)]
        public string TransReason { get; set; }
        [StringLength(5)]
        public string TransNoOfCart { get; set; }
        [StringLength(20)]
        public string TransAddressStatus { get; set; }
        [StringLength(10)]
        public string TransAddressCountryCode { get; set; }
        [StringLength(10)]
        public string TransAddressZip { get; set; }
        [StringLength(30)]
        public string TransAddressName { get; set; }
        [StringLength(30)]
        public string TransAddressStreet { get; set; }
        [StringLength(20)]
        public string TransAddressCountry { get; set; }
        [StringLength(20)]
        public string TransAddressCity { get; set; }
        [StringLength(20)]
        public string TransAddressState { get; set; }
        [StringLength(20)]
        public string TransResidenceCountry { get; set; }
        public long? OrderID { get; set; }
        public bool? IpnHandlerVerified { get; set; }
        [StringLength(50)]
        public string IpnHandlerTransID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IpnHandlerUpdatedOn { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ExRateUSD { get; set; }
        public double InitAmountUSDActual { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InitAmountUSD { get; set; }
        public bool IpnVerificationRequired { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InitCartTotalUSD { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransAmountActual { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransAmountFee { get; set; }
        public double TransExchRateKWD { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal TransAmountActualKWD { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransOn2 { get; set; }
        public long? CartID { get; set; }
        [Unicode(false)]
        public string PaypalDataTransferData { get; set; }

        [ForeignKey("RefCustomerID")]
        [InverseProperty("PaymentDetailsPayPals")]
        public virtual Customer RefCustomer { get; set; }
    }
}
