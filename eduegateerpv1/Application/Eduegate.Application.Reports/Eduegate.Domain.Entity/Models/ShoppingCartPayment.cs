using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartPayment
    {
        public System.DateTime CreatedOn { get; set; }
        public string CustomerSessionID { get; set; }
        public short Status { get; set; }
        public long CustomerID { get; set; }
        public decimal TotalAmt { get; set; }
        public string IP { get; set; }
        public string Country { get; set; }
    }
}
