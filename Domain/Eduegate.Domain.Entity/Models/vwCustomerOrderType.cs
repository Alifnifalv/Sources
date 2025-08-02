using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCustomerOrderType
    {
        [Key]
        public long OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<decimal> OrderAmount { get; set; }
    }
}
