using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VoucherStatuses", Schema = "inventory")]
    public partial class VoucherStatus
    {
        public VoucherStatus()
        {
            Vouchers = new HashSet<Voucher>();
        }

        [Key]
        public byte VoucherStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
