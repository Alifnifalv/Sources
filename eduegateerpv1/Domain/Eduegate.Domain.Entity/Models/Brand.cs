using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Brands", Schema = "catalog")]
    public partial class Brand
    {
        public Brand()
        {
            this.BrandCultureDatas = new List<BrandCultureData>();
            this.BrandPriceListMaps = new List<BrandPriceListMap>();
            this.MenuLinkBrandMaps = new List<MenuLinkBrandMap>();
            this.Products = new List<Product>();
            this.ProductPriceListBrandMaps = new List<ProductPriceListBrandMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
            this.BrandImageMaps = new List<BrandImageMap>();
            this.BrandTagMaps = new List<BrandTagMap>();
        }

        [Key]
        public long BrandIID { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string Descirption { get; set; }
        public string LogoFile { get; set; }
        public Nullable<long> IsIncludeHomePage { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }

        public Nullable<long> SEOMetadataID { get; set; }
        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }
        public virtual ICollection<BrandPriceListMap> BrandPriceListMaps { get; set; }
        public virtual BrandStatus BrandStatus { get; set; }
        public virtual ICollection<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual ICollection<BrandImageMap> BrandImageMaps { get; set; }
        public virtual ICollection<BrandTagMap> BrandTagMaps { get; set; }
        public virtual SeoMetadata SeoMetadata { get; set; }
    }
}
