using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentDetailsTheFort", Schema = "payment")]
    public partial class PaymentDetailsTheFort
    {
        [Key]
        public long TrackID { get; set; }
        [Key]
        public long TrackKey { get; set; }
        public long CustomerID { get; set; }
        public long PaymentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InitOn { get; set; }
        [Required]
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
        [Unicode(false)]
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
        [Column(TypeName = "datetime")]
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
        [Unicode(false)]
        public string PTransCurrency { get; set; }
        [StringLength(50)]
        public string PTransPaymentOption { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string PTransEci { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string PTransAuthorizationCode { get; set; }
        [StringLength(35)]
        public string PTransOrderDesc { get; set; }
        [StringLength(150)]
        public string PTransResponseMessage { get; set; }
        public byte? PTransStatus { get; set; }
        public int? PTransResponseCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PTransCustomerIP { get; set; }
        [StringLength(255)]
        public string PTransCustomerEmail { get; set; }
        public short? PTransExpiryDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PTransCardNumber { get; set; }
        [StringLength(100)]
        public string PTransCustomerName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PTransActualAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PTransOn { get; set; }
        [Column(TypeName = "datetime")]
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
        [Unicode(false)]
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
        [Unicode(false)]
        public string CardHolderName { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("PaymentDetailsTheForts")]
        public virtual Customer Customer { get; set; }
    }
}
