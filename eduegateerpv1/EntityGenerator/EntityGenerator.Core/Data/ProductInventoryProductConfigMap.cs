using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductInventoryProductConfigMaps", Schema = "catalog")]
    public partial class ProductInventoryProductConfigMap
    {
        [Key]
        public long ProductInventoryConfigID { get; set; }
        [Key]
        public long ProductID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductInventoryProductConfigMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductInventoryConfigID")]
        [InverseProperty("ProductInventoryProductConfigMaps")]
        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
    }
}
