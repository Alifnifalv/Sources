using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherStatus
    {
        public VoucherStatus()
        {
            this.Vouchers = new List<Voucher>();
        }

        public byte VoucherStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
