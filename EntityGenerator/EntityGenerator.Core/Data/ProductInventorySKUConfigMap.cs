using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductInventorySKUConfigMaps", Schema = "catalog")]
    public partial class ProductInventorySKUConfigMap
    {
        [Key]
        public long ProductInventoryConfigID { get; set; }
        [Key]
        public long ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductInventoryConfigID")]
        [InverseProperty("ProductInventorySKUConfigMaps")]
        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductInventorySKUConfigMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
