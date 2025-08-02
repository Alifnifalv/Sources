using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductItem
    {
        public System.DateTime OrderDate { get; set; }
        public int RefOrderProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public long OrderID { get; set; }
    }
}
