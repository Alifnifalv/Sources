namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListSKUMaps20230221DEL")]
    public partial class ProductPriceListSKUMaps20230221DEL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductPriceListItemMapIID { get; set; }

        public int? CompanyID { get; set; }

        public long? ProductPriceListID { get; set; }

        public long? ProductSKUID { get; set; }

        public long? UnitGroundID { get; set; }

        public long? CustomerGroupID { get; set; }

        public decimal? SellingQuantityLimit { get; set; }

        public decimal? Amount { get; set; }

        public byte? SortOrder { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? PricePercentage { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? Cost { get; set; }

        public bool? IsActive { get; set; }

        public int? DisplayOrder { get; set; }

        public int? CurrencyID { get; set; }
    }
}
