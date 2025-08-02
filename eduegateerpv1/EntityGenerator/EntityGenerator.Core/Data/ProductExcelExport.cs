using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductExcelExport", Schema = "schools")]
    public partial class ProductExcelExport
    {
        [Key]
        public long ProductDataIID { get; set; }
        [StringLength(100)]
        public string Category { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SellingPrice { get; set; }
        [StringLength(100)]
        public string Campus { get; set; }
    }
}
