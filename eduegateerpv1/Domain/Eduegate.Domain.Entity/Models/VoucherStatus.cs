using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("VoucherStatuses", Schema = "inventory")]
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
