using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class InventoryDetailsView
    {
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long? ProductSKUMapIID { get; set; }
        [StringLength(1000)]
        public string SKU { get; set; }
        public long? ProductIID { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(150)]
        public string ProductSKUCode { get; set; }
        [StringLength(50)]
        public string BarCode { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Quantity { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CostPrice { get; set; }
        public string BrandName { get; set; }
        public string BrandID { get; set; }
        public long BranchID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public string PartNo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastCost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Price { get; set; }
    }
}
