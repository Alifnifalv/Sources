using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartTransLocked
    {
        [Key]
        public int ShoppingCartItemID { get; set; }
        public string CustomerSessionID { get; set; }
        public Nullable<long> RefCustomerID { get; set; }
        public Nullable<decimal> PreviousTotal { get; set; }
        public Nullable<decimal> CartTotal { get; set; }
        public Nullable<decimal> MaxLimit { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
