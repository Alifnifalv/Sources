using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherCustomerTran
    {
        public int TransID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public string CustomerSessionID { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
    }
}
