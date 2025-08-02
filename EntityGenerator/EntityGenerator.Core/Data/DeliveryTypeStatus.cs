using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeStatuses", Schema = "inventory")]
    public partial class DeliveryTypeStatus
    {
        public DeliveryTypeStatus()
        {
            DeliveryType1 = new HashSet<DeliveryType1>();
        }

        [Key]
        public byte DeliveryTypeStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<DeliveryType1> DeliveryType1 { get; set; }
    }
}
