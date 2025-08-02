using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DeliveryTypeStatuses", Schema = "inventory")]
    public partial class DeliveryTypeStatus
    {
        public DeliveryTypeStatus()
        {
            this.DeliveryTypes1 = new List<DeliveryTypes1>();
        }

        [Key]
        public byte DeliveryTypeStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<DeliveryTypes1> DeliveryTypes1 { get; set; }
    }
}
