namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListProductQuantityMaps")]
    public partial class ProductPriceListProductQuantityMap
    {
        [Key]
        public long ProductPriceListProductQuantityMapIID { get; set; }

        public long? ProductPriceListProductMapID { get; set; }

        public long? ProductID { get; set; }

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

        public virtual ProductPriceListProductMap ProductPriceListProductMap { get; set; }

        public virtual Product Product { get; set; }
    }
}
