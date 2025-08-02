using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentMasterVisa", Schema = "payment")]
    [Index("CardTypeID", Name = "IDX_PaymentMasterVisa_CardTypeID_CartID")]
    [Index("CartID", Name = "IDX_PaymentMasterVisa_CartID_")]
    [Index("CartID", "CardTypeID", Name = "IDX_PaymentMasterVisa_CartID__CardTypeID_CustomerID__SecureHash")]
    [Index("CartID", "CardTypeID", Name = "IDX_PaymentMasterVisa_CartID__CardTypeID_TrackKey__CustomerID__PaymentID__InitOn__InitStatus__InitI")]
    public partial class PaymentMasterVisa
    {
        [Key]
        public long TrackIID { get; set; }
        public long TrackKey { get; set; }
        public long? CustomerID { get; set; }
        public long? PaymentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InitOn { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string InitStatus { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InitIP { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string InitLocation { get; set; }
        public string VpcURL { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VpcVersion { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VpcCommand { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AccessCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MerchantID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VpcLocale { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaymentAmount { get; set; }
        public int? VirtualAmount { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PaymentCurrency { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResponseOn { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ResponseCode { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string CodeDescription { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Message { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReceiptNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AcquireResponseCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string BankAuthorizationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string BatchNo { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string CardType { get; set; }
        public int? ResponseAmount { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ResponseIP { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ResponseLocation { get; set; }
        public long? OrderID { get; set; }
        public long? CartID { get; set; }
        public string Response { get; set; }
        [StringLength(30)]
        public string LogType { get; set; }
        public int? CardTypeID { get; set; }
        [StringLength(500)]
        public string SecureHash { get; set; }
        [StringLength(50)]
        public string ResponseAcquirerID { get; set; }
        [StringLength(50)]
        public string ResponseBankID { get; set; }
        [StringLength(5)]
        public string ResponseCardExpiryDate { get; set; }
        [StringLength(50)]
        public string ResponseCardHolderName { get; set; }
        [StringLength(150)]
        public string ResponseConfirmationID { get; set; }
        [StringLength(50)]
        public string ResponseStatus { get; set; }
        [StringLength(500)]
        public string ResponseStatusMessage { get; set; }
        [StringLength(500)]
        public string ResponseSecureHash { get; set; }
        public long? LoginID { get; set; }
        public bool? SuccessStatus { get; set; }

        [ForeignKey("CardTypeID")]
        [InverseProperty("PaymentMasterVisas")]
        public virtual CardType CardTypeNavigation { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("PaymentMasterVisas")]
        public virtual Login Login { get; set; }
    }
}
