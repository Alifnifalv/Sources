using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductVoucherSetting
    {
        public int id { get; set; }
        public decimal VoucherAmount { get; set; }
        public int VoucherValidity { get; set; }
    }
}
