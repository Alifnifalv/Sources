using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Categories1 = new List<Category>();
            this.CategoryCultureDatas = new List<CategoryCultureData>();
            this.CategoryImageMaps = new List<CategoryImageMap>();
            this.CategorySettings = new List<CategorySetting>();
            this.CategoryTagMaps = new List<CategoryTagMap>();
            this.CategoryTags = new List<CategoryTag>();
            this.ProductCategoryMaps = new List<ProductCategoryMap>();
            this.ProductPriceListCategoryMaps = new List<ProductPriceListCategoryMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
        }

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
        public byte[] TimeStamps { get; set; }
        public Nullable<byte> CultureID { get; set; }
        public Nullable<decimal> Profit { get; set; }
        public Nullable<bool> IsReporting { get; set; }
        public virtual ICollection<Category> Categories1 { get; set; }
        public virtual Category Category1 { get; set; }
        public virtual SeoMetadata SeoMetadata { get; set; }
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }
        public virtual ICollection<CategoryTag> CategoryTags { get; set; }
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
    }
}
