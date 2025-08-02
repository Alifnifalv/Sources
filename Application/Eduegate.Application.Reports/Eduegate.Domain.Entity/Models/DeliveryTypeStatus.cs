using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypeStatus
    {
        public DeliveryTypeStatus()
        {
            this.DeliveryTypes1 = new List<DeliveryTypes1>();
        }

        public byte DeliveryTypeStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<DeliveryTypes1> DeliveryTypes1 { get; set; }
    }
}
