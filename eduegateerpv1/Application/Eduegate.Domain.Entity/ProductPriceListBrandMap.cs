namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListBrandMaps")]
    public partial class ProductPriceListBrandMap
    {
        [Key]
        public long ProductPriceListBrandMapIID { get; set; }

        public long? ProductPriceListID { get; set; }

        public long? BrandID { get; set; }

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

        public decimal? Price { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
