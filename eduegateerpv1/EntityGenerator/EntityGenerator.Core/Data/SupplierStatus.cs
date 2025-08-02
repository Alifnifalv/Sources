using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SupplierStatuses", Schema = "mutual")]
    public partial class SupplierStatus
    {
        public SupplierStatus()
        {
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public byte SupplierStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
