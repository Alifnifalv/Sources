using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentLogs", Schema = "payment")]
    public partial class PaymentLog
    {
        [Key]
        public long PaymentLogIID { get; set; }

        public long? TrackID { get; set; }

        public long? TrackKey { get; set; }

        public string RequestLog { get; set; }

        public string ResponseLog { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        public int? CompanyID { get; set; }

        public int? SiteID { get; set; }

        public string RequestUrl { get; set; }

        [StringLength(50)]
        public string RequestType { get; set; }

        public long? CartID { get; set; }

        public long? CustomerID { get; set; }

        public decimal? Amount { get; set; }

        public string TransNo { get; set; }

        public string ValidationResult { get; set; }

        public string CardType { get; set; }
       
        public int CardTypeID { get; set; }

        public string ResponseAcquirerID { get; set; }
        public string ResponseBankID { get; set; }

        public string ResponseCardExpiryDate { get; set; }
        public string ResponseCardHolderName { get; set; }

        public string ResponseConfirmationID { get; set; }
        public string ResponseStatus { get; set; }

        public string ResponseStatusMessage { get; set; }
        public string ResponseSecureHash { get; set; }

        public long? LoginID { get; set; }

        public virtual Login Login { get; set; }
    }
}