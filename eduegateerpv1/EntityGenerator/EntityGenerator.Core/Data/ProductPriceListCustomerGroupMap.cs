using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListCustomerGroupMaps", Schema = "catalog")]
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        public int? CurrencyID { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("ProductPriceListCustomerGroupMaps")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("CategoryID")]
        [InverseProperty("ProductPriceListCustomerGroupMaps")]
        public virtual Category Category { get; set; }
        [ForeignKey("CustomerGroupID")]
        [InverseProperty("ProductPriceListCustomerGroupMaps")]
        public virtual CustomerGroup CustomerGroup { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductPriceListCustomerGroupMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListCustomerGroupMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductPriceListCustomerGroupMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
