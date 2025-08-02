using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCustomerOrderMiniMarket
    {
        public long OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public long RefCustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string EmailID { get; set; }
        public Nullable<decimal> MiniMarketTotal { get; set; }
    }
}
