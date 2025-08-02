using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Categories", Schema = "catalog")]
    public partial class Category
    {
        public Category()
        {           
            this.CategoryCultureDatas = new List<CategoryCultureData>();
            this.CategoryImageMaps = new List<CategoryImageMap>();
            this.CategoryPriceListMaps = new List<CategoryPriceListMap>();
            this.MenuLinkCategoryMaps = new List<MenuLinkCategoryMap>();
            this.ProductCategoryMaps = new List<ProductCategoryMap>();
            this.ProductPriceListCategoryMaps = new List<ProductPriceListCategoryMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
            this.CategoryTagMaps = new List<CategoryTagMap>();
            this.CategorySettings = new List<CategorySetting>();
        }

        [Key]
        public long CategoryIID { get; set; }
        public Nullable<long> ParentCategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public string ThumbnailImageName { get; set; }
        public Nullable<long> SeoMetadataID { get; set; }
        public Nullable<bool> IsInNavigationMenu { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<byte> CultureID { get; set; }
        public Nullable<decimal> Profit { get; set; }
        public Nullable<bool> IsReporting { get; set; }
        //public virtual ICollection<Category> Categories1 { get; set; }
        //public virtual Category Category1 { get; set; }
        public virtual SeoMetadata SeoMetadata { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
        public virtual ICollection<CategoryPriceListMap> CategoryPriceListMaps { get; set; }
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }
        public int? SortOrder { get; set; }
    }
}