using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderDeliveryDisplayHeadMap
    {
        public long HeadID { get; set; }
        public byte CultureID { get; set; }
        public string DeliveryDisplayText { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
