namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentDetailsTheFort")]
    public partial class PaymentDetailsTheFort
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TrackID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TrackKey { get; set; }

        public long CustomerID { get; set; }

        public long PaymentID { get; set; }

        public DateTime InitOn { get; set; }

        [Required]
        public string InitStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string InitIP { get; set; }

        [Required]
        [StringLength(100)]
        public string InitLocation { get; set; }

        public decimal InitAmount { get; set; }

        [StringLength(100)]
        public string PShaRequestPhrase { get; set; }

        [StringLength(30)]
        public string PAccessCode { get; set; }

        [StringLength(30)]
        public string PMerchantIdentifier { get; set; }

        [StringLength(10)]
        public string PCommand { get; set; }

        [StringLength(3)]
        public string PCurrency { get; set; }

        [StringLength(255)]
        public string PCustomerEmail { get; set; }

        [StringLength(2)]
        public string PLang { get; set; }

        [StringLength(100)]
        public string PMerchantReference { get; set; }

        [StringLength(1000)]
        public string PSignatureText { get; set; }

        [StringLength(250)]
        public string PSignature { get; set; }

        public int? PAmount { get; set; }

        public short RefCountryID { get; set; }

        [StringLength(250)]
        public string PTransSignature { get; set; }

        [StringLength(50)]
        public string TransID { get; set; }

        public DateTime? POn { get; set; }

        [StringLength(10)]
        public string PTransCommand { get; set; }

        [StringLength(100)]
        public string PTransMerchantReference { get; set; }

        public int? PTransAmount { get; set; }

        [StringLength(30)]
        public string PTransAccessCode { get; set; }

        [StringLength(30)]
        public string PTransMerchantIdentifier { get; set; }

        [StringLength(3)]
        public string PTransCurrency { get; set; }

        [StringLength(50)]
        public string PTransPaymentOption { get; set; }

        [StringLength(20)]
        public string PTransEci { get; set; }

        [StringLength(10)]
        public string PTransAuthorizationCode { get; set; }

        [StringLength(35)]
        public string PTransOrderDesc { get; set; }

        [StringLength(150)]
        public string PTransResponseMessage { get; set; }

        public byte? PTransStatus { get; set; }

        public int? PTransResponseCode { get; set; }

        [StringLength(50)]
        public string PTransCustomerIP { get; set; }

        [StringLength(255)]
        public string PTransCustomerEmail { get; set; }

        public short? PTransExpiryDate { get; set; }

        [StringLength(50)]
        public string PTransCardNumber { get; set; }

        [StringLength(100)]
        public string PTransCustomerName { get; set; }

        public decimal? PTransActualAmount { get; set; }

        public DateTime? PTransOn { get; set; }

        public DateTime? TransOn { get; set; }

        public long? OrderID { get; set; }

        [StringLength(15)]
        public string TServiceCommand { get; set; }

        [StringLength(30)]
        public string TAccessCode { get; set; }

        [StringLength(1000)]
        public string TSignatureText { get; set; }

        [StringLength(250)]
        public string TSignature { get; set; }

        [StringLength(100)]
        public string TMerchantReference { get; set; }

        [StringLength(2)]
        public string TLanguage { get; set; }

        [StringLength(100)]
        public string TShaRequestPhrase { get; set; }

        public int? TAmount { get; set; }

        [StringLength(30)]
        public string TMerchantIdentifier { get; set; }

        [StringLength(100)]
        public string AdditionalDetails { get; set; }

        public long? CartID { get; set; }

        [StringLength(50)]
        public string CardHolderName { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
