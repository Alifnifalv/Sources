using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCartActivitySKUMaps", Schema = "inventory")]
    public partial class ShoppingCartActivitySKUMap
    {
        [Key]
        public long ShoppingCartActivityLogID { get; set; }
        [Key]
        public long ProductSKUMapID { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        public byte? ActivityActionStatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
    }
}
