using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Categories", Schema = "catalog")]
    [Index("CategoryCode", Name = "IX_Product_CategoryCode", IsUnique = true)]
    public partial class Category
    {
        public Category()
        {
            CategoryCultureDatas = new HashSet<CategoryCultureData>();
            CategoryImageMaps = new HashSet<CategoryImageMap>();
            CategorySettings = new HashSet<CategorySetting>();
            CategoryTagMaps = new HashSet<CategoryTagMap>();
            CategoryTags = new HashSet<CategoryTag>();
            DeliveryType1 = new HashSet<DeliveryType1>();
            MenuLinkCategoryMaps = new HashSet<MenuLinkCategoryMap>();
            ProductCategoryMaps = new HashSet<ProductCategoryMap>();
            ProductPriceListCategoryMaps = new HashSet<ProductPriceListCategoryMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
        }

        [Key]
        public long CategoryIID { get; set; }
        public long? ParentCategoryID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CategoryCode { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        [StringLength(500)]
        public string ImageName { get; set; }
        [StringLength(500)]
        public string ThumbnailImageName { get; set; }
        public long? SeoMetadataID { get; set; }
        public bool? IsInNavigationMenu { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? CultureID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Profit { get; set; }
        public bool? IsReporting { get; set; }
        public string TooltipMessage { get; set; }
        public int? SortOrder { get; set; }
        public string WarningMessage { get; set; }
        public int? ClassID { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("Categories")]
        public virtual Class Class { get; set; }
        [ForeignKey("SeoMetadataID")]
        [InverseProperty("Categories")]
        public virtual SeoMetadata SeoMetadata { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<CategoryTag> CategoryTags { get; set; }
        [InverseProperty("DeliveryTypeCategory")]
        public virtual ICollection<DeliveryType1> DeliveryType1 { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
    }
}
