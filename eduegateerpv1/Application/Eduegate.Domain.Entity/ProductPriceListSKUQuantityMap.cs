namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListSKUQuantityMaps")]
    public partial class ProductPriceListSKUQuantityMap
    {
        [Key]
        public long ProductPriceListSKUQuantityMapIID { get; set; }

        public long? ProductPriceListSKUMapID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? DiscountPrice { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CurrencyID { get; set; }

        public virtual ProductPriceListSKUMap ProductPriceListSKUMap { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
