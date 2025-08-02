using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListSKUMaps", Schema = "catalog")]
    [Index("ProductSKUID", Name = "idx_ProductPriceListSKUMapsProductSKUID")]
    public partial class ProductPriceListSKUMap
    {
        public ProductPriceListSKUMap()
        {
            ProductPriceListSKUQuantityMaps = new HashSet<ProductPriceListSKUQuantityMap>();
        }

        [Key]
        public long ProductPriceListItemMapIID { get; set; }
        public int? CompanyID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? ProductSKUID { get; set; }
        public long? UnitGroundID { get; set; }
        public long? CustomerGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SellingQuantityLimit { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public byte? SortOrder { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PricePercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Cost { get; set; }
        public bool? IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public int? CurrencyID { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("ProductPriceListSKUMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("CustomerGroupID")]
        [InverseProperty("ProductPriceListSKUMaps")]
        public virtual CustomerGroup CustomerGroup { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListSKUMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
        [ForeignKey("ProductSKUID")]
        [InverseProperty("ProductPriceListSKUMaps")]
        public virtual ProductSKUMap ProductSKU { get; set; }
        [ForeignKey("UnitGroundID")]
        [InverseProperty("ProductPriceListSKUMaps")]
        public virtual UnitGroup UnitGround { get; set; }
        [InverseProperty("ProductPriceListSKUMap")]
        public virtual ICollection<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
    }
}
