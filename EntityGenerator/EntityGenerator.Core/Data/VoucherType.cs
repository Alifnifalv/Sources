using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VoucherTypes", Schema = "inventory")]
    public partial class VoucherType
    {
        public VoucherType()
        {
            Vouchers = new HashSet<Voucher>();
        }

        [Key]
        public byte VoucherTypeID { get; set; }
        [StringLength(50)]
        public string VoucherTypeName { get; set; }
        public bool? IsMinimumAmtRequired { get; set; }

        [InverseProperty("VoucherType")]
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
