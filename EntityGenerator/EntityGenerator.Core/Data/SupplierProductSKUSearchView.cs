using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierProductSKUSearchView
    {
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        [StringLength(1000)]
        public string SKU { get; set; }
        [StringLength(50)]
        public string Barcode { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductPrice { get; set; }
        public long? BranchID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
    }
}
