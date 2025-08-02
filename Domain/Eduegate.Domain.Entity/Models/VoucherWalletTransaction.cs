using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherWalletTransaction
    {
        [Key]
        public long TransID { get; set; }
        public string VoucherNo { get; set; }
        public long WalletTransactionID { get; set; }
        //public decimal Amount { get; set; }

    }
}