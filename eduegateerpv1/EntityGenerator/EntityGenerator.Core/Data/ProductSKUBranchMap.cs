using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKUBranchMaps", Schema = "catalog")]
    public partial class ProductSKUBranchMap
    {
        [Key]
        public long ProductSKUBranchMapIID { get; set; }
        public long? ProductSKUID { get; set; }
        public long? BranchID { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("ProductSKUBranchMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("ProductSKUID")]
        [InverseProperty("ProductSKUBranchMaps")]
        public virtual ProductSKUMap ProductSKU { get; set; }
    }
}
