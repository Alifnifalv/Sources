namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentDetailsLogKnet")]
    public partial class PaymentDetailsLogKnet
    {
        [Key]
        public int LogID { get; set; }

        [StringLength(50)]
        public string CustomerSessionID { get; set; }

        public long? CustomerID { get; set; }

        public long? TrackID { get; set; }

        public long? TrackKey { get; set; }

        public long? PaymentID { get; set; }

        public long? TransID { get; set; }

        [StringLength(100)]
        public string TransResult { get; set; }

        [StringLength(50)]
        public string TransPostDate { get; set; }

        [StringLength(50)]
        public string TransAuth { get; set; }

        [StringLength(50)]
        public string TransRef { get; set; }
    }
}
