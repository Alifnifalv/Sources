using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderLog
    {
        public long LogID { get; set; }
        public long RefLogOrderID { get; set; }
        public short OrderStatus { get; set; }
        public long UserID { get; set; }
        public System.DateTime LogDateTime { get; set; }
        public virtual OrderMaster OrderMaster { get; set; }
    }
}
