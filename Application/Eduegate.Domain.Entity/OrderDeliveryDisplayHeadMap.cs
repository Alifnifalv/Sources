namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.OrderDeliveryDisplayHeadMap")]
    public partial class OrderDeliveryDisplayHeadMap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long HeadID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        [StringLength(100)]
        public string DeliveryDisplayText { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }
    }
}
