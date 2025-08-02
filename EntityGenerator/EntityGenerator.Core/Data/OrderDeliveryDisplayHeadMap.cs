using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OrderDeliveryDisplayHeadMap", Schema = "distribution")]
    public partial class OrderDeliveryDisplayHeadMap
    {
        [Key]
        public long HeadID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(100)]
        public string DeliveryDisplayText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("OrderDeliveryDisplayHeadMaps")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("OrderDeliveryDisplayHeadMaps")]
        public virtual TransactionHead Head { get; set; }
    }
}
