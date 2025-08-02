using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StockReportView
    {
        [StringLength(1000)]
        public string ProductName { get; set; }
        [StringLength(150)]
        public string ProductSKUCode { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
        [StringLength(50)]
        public string BarCode { get; set; }
        [Required]
        [StringLength(150)]
        public string ProductCode { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
    }
}
