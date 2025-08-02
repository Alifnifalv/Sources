using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherStatus
    {
        public VoucherStatus()
        {
            this.Vouchers = new List<Voucher>();
        }

        [Key]
        public byte VoucherStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
