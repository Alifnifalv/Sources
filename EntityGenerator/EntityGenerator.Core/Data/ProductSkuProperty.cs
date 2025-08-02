using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductSkuProperty
    {
        public long ProductIID { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        public long ProductSKUMapIID { get; set; }
        public long PropertyIID { get; set; }
        [StringLength(50)]
        public string PropertyName { get; set; }
        public byte PropertyTypeID { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }
        public int? Sequence { get; set; }
        [StringLength(50)]
        public string Barcode { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
        public byte? StatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SellingQuantityLimit { get; set; }
        [StringLength(500)]
        public string ImageFile { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? ProductPrice { get; set; }
    }
}
