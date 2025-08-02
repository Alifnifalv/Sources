using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductListBySKU
    {
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? ProductPrice { get; set; }
        public int? Sequence { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        [StringLength(50)]
        public string Barcode { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
        public string SKU { get; set; }
        [StringLength(500)]
        public string ImageFile { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SellingQuantityLimit { get; set; }
    }
}
