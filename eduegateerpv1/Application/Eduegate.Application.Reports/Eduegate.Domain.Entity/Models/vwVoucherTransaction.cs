using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwVoucherTransaction
    {
        public string VoucherNo { get; set; }
        public long RefOrderID { get; set; }
        public decimal Amount { get; set; }
        public string VoucherType { get; set; }
        public Nullable<byte> VoucherDiscount { get; set; }
    }
}
