using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCustomerOrderType
    {
        public long OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<decimal> OrderAmount { get; set; }
    }
}
