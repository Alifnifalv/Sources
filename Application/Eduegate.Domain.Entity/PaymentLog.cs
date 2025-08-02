namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentLogs")]
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

        [StringLength(25)]
        public string TransNo { get; set; }

        public string ValidationResult { get; set; }
    }
}
