using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderItemReturn
    {
        public long RefOrderID { get; set; }
        public long RefOrderItemID { get; set; }
        public int Quantity { get; set; }
        public int QuantityApprove { get; set; }
    }
}
