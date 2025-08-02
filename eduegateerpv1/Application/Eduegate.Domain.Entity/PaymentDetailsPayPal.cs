namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentDetailsPayPal")]
    public partial class PaymentDetailsPayPal
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TrackID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TrackKey { get; set; }

        public long RefCustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessEmail { get; set; }

        public long PaymentID { get; set; }

        public DateTime InitOn { get; set; }

        [Required]
        [StringLength(1)]
        public string InitStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string InitIP { get; set; }

        [Required]
        [StringLength(100)]
        public string InitLocation { get; set; }

        public decimal InitAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string InitCurrency { get; set; }

        public bool IpnVerified { get; set; }

        [StringLength(50)]
        public string TransID { get; set; }

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

        public DateTime? IpnHandlerUpdatedOn { get; set; }

        public decimal ExRateUSD { get; set; }

        public double InitAmountUSDActual { get; set; }

        public decimal InitAmountUSD { get; set; }

        public bool IpnVerificationRequired { get; set; }

        public decimal InitCartTotalUSD { get; set; }

        public decimal TransAmountActual { get; set; }

        public decimal TransAmountFee { get; set; }

        public double TransExchRateKWD { get; set; }

        public decimal TransAmountActualKWD { get; set; }

        public DateTime? TransOn2 { get; set; }

        public long? CartID { get; set; }

        public string PaypalDataTransferData { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
