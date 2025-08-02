using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListSKUQuantityMaps", Schema = "catalog")]
    public partial class ProductPriceListSKUQuantityMap
    {
        [Key]
        public long ProductPriceListSKUQuantityMapIID { get; set; }
        public long? ProductPriceListSKUMapID { get; set; }
        public long? ProductSKUMapID { get; set; }
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
        public int? CurrencyID { get; set; }

        [ForeignKey("ProductPriceListSKUMapID")]
        [InverseProperty("ProductPriceListSKUQuantityMaps")]
        public virtual ProductPriceListSKUMap ProductPriceListSKUMap { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductPriceListSKUQuantityMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
