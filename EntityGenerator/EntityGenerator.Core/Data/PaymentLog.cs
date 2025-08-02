using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentLogs", Schema = "payment")]
    [Index("CartID", Name = "IDX_PaymentLogs_CartID_ResponseLog")]
    [Index("CartID", Name = "IDX_PaymentLogs_CartID_ResponseLog__CreatedDate__RequestType__Amount__ValidationResult")]
    [Index("CustomerID", Name = "IDX_PaymentLogs_CustomerID_")]
    public partial class PaymentLog
    {
        [Key]
        public long PaymentLogIID { get; set; }
        public long? TrackID { get; set; }
        public long? TrackKey { get; set; }
        public string RequestLog { get; set; }
        public string ResponseLog { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }
        public string RequestUrl { get; set; }
        [StringLength(50)]
        public string RequestType { get; set; }
        public long? CartID { get; set; }
        public long? CustomerID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string TransNo { get; set; }
        public string ValidationResult { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string CardType { get; set; }
        public int? CardTypeID { get; set; }
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

        [ForeignKey("LoginID")]
        [InverseProperty("PaymentLogs")]
        public virtual Login Login { get; set; }
    }
}
