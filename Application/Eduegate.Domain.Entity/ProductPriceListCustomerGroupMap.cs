namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListCustomerGroupMaps")]
    public partial class ProductPriceListCustomerGroupMap
    {
        [Key]
        public long ProductPriceListCustomerGroupMapIID { get; set; }

        public long? ProductPriceListID { get; set; }

        public long? CustomerGroupID { get; set; }

        public long? ProductID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public long? CategoryID { get; set; }

        public long? BrandID { get; set; }

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

        public decimal? Price { get; set; }

        public int? CurrencyID { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual CustomerGroup CustomerGroup { get; set; }

        public virtual ProductPriceList ProductPriceList { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
