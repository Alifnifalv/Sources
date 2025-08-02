using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherWalletTransaction
    {
        public long TransID { get; set; }
        public string VoucherNo { get; set; }
        public long WalletTransactionID { get; set; }
        //public decimal Amount { get; set; }

    }
}