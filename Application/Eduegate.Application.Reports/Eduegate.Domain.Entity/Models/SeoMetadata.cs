using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SeoMetadata
    {
        public SeoMetadata()
        {
            this.Brands = new List<Brand>();
            this.Categories = new List<Category>();
            this.Products = new List<Product>();
            this.SeoMetadataCultureDatas = new List<SeoMetadataCultureData>();
            this.ProductSKUMaps = new List<ProductSKUMap>();
        }

        public long SEOMetadataIID { get; set; }
        public string PageTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string UrlKey { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }
        public virtual ICollection<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
    }
}
