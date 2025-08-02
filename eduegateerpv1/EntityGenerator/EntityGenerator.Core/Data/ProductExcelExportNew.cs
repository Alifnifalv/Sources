using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductExcelExportNew", Schema = "schools")]
    public partial class ProductExcelExportNew
    {
        [Key]
        public long ProductDataIID { get; set; }
        [StringLength(100)]
        public string Category { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [StringLength(100)]
        public string Campus { get; set; }
        public int? IsPreviouslyAdded { get; set; }
    }
}
