using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("VoucherTypes", Schema = "inventory")]
    public partial class VoucherType
    {
        public VoucherType()
        {
            this.Vouchers = new List<Voucher>();
        }

        [Key]
        public byte VoucherTypeID { get; set; }
        public string VoucherTypeName { get; set; }
        public bool IsMinimumAmtRequired { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
