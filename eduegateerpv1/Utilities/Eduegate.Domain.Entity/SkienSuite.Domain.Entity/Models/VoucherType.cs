using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherType
    {
        public VoucherType()
        {
            this.Vouchers = new List<Voucher>();
        }

        public byte VoucherTypeID { get; set; }
        public string VoucherTypeName { get; set; }
        public Nullable<bool> IsMinimumAmtRequired { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
