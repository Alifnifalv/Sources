using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartItemLocked
    {
        public int ShoppingCartItemID { get; set; }
        public string CustomerSessionID { get; set; }
        public Nullable<long> RefCustomerID { get; set; }
        public Nullable<int> RefProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
