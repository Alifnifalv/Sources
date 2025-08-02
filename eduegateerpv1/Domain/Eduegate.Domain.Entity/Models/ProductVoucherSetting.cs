using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductVoucherSetting
    {
        [Key]
        public int id { get; set; }
        public decimal VoucherAmount { get; set; }
        public int VoucherValidity { get; set; }
    }
}
