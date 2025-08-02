using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class InventoryBranchMarketPlaceView
    {
        public long ProductSKUMapIID { get; set; }
        public long BranchID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CostPrice { get; set; }
    }
}
