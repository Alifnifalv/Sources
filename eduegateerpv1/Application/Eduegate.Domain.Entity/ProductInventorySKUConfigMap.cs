namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductInventorySKUConfigMaps")]
    public partial class ProductInventorySKUConfigMap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductInventoryConfigID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductSKUMapID { get; set; }

        public decimal? Price { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }

        public virtual ProductInventorySKUConfigMap ProductInventorySKUConfigMaps1 { get; set; }

        public virtual ProductInventorySKUConfigMap ProductInventorySKUConfigMap1 { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
