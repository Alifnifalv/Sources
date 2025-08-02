using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherTransaction
    {
        [Key]
        public int TransID { get; set; }
        public string VoucherNo { get; set; }
        public long RefOrderID { get; set; }
        public decimal Amount { get; set; }
    }
}
