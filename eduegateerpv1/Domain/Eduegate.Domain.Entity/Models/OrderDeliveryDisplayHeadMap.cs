using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("OrderDeliveryDisplayHeadMap", Schema = "distribution")]
    public partial class OrderDeliveryDisplayHeadMap
    {
        [Key]
        public long HeadID { get; set; }
        public byte CultureID { get; set; }
        public string DeliveryDisplayText { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
