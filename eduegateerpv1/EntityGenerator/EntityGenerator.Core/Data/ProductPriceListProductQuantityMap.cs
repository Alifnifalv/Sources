using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListProductQuantityMaps", Schema = "catalog")]
    public partial class ProductPriceListProductQuantityMap
    {
        [Key]
        public long ProductPriceListProductQuantityMapIID { get; set; }
        public long? ProductPriceListProductMapID { get; set; }
        public long? ProductID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductPriceListProductQuantityMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductPriceListProductMapID")]
        [InverseProperty("ProductPriceListProductQuantityMaps")]
        public virtual ProductPriceListProductMap ProductPriceListProductMap { get; set; }
    }
}
