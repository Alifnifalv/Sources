using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ProductBundles = new List<ProductBundle>();
            this.ProductBundles1 = new List<ProductBundle>();
            this.ProductCategoryMaps = new List<ProductCategoryMap>();
            this.ProductCultureDatas = new List<ProductCultureData>();
            this.ProductImageMaps = new List<ProductImageMap>();
            this.ProductInventoryProductConfigMaps = new List<ProductInventoryProductConfigMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
            this.ProductPriceListProductMaps = new List<ProductPriceListProductMap>();
            this.ProductPriceListProductQuantityMaps = new List<ProductPriceListProductQuantityMap>();
            this.ProductPropertyMaps = new List<ProductPropertyMap>();
            this.ProductDeliveryCountrySettings = new List<ProductDeliveryCountrySetting>();
            this.ProductDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            this.ProductLocationMaps = new List<ProductLocationMap>();
            this.ProductSKUMaps = new List<ProductSKUMap>();
            this.ProductTagMaps = new List<ProductTagMap>();
            this.ProductTags = new List<ProductTag>();
            this.ProductToProductMaps = new List<ProductToProductMap>();
            this.ProductToProductMaps1 = new List<ProductToProductMap>();
            this.ProductVideoMaps = new List<ProductVideoMap>();
        }

        public long ProductIID { get; set; }
        public string ProductCode { get; set; }
        public Nullable<long> ProductTypeID { get; set; }
        public Nullable<long> ProductFamilyID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<long> SeoMetadataIID { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<long> ManufactureID { get; set; }
        public Nullable<long> ManufactureCountryID { get; set; }
        public Nullable<long> UnitGroupID { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<bool> IsMultipleSKUEnabled { get; set; }
        public Nullable<bool> IsOnline { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<long> ProductOwnderID { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductBundle> ProductBundles { get; set; }
        public virtual ICollection<ProductBundle> ProductBundles1 { get; set; }
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }
        public virtual ProductFamily ProductFamily { get; set; }
        public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }
        public virtual ICollection<ProductInventoryProductConfigMap> ProductInventoryProductConfigMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        public virtual ICollection<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual ICollection<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
        public virtual ProductStatu ProductStatu { get; set; }
        public virtual SeoMetadata SeoMetadata { get; set; }
        public virtual UnitGroup UnitGroup { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }
        public virtual ICollection<ProductTagMap> ProductTagMaps { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual ICollection<ProductToProductMap> ProductToProductMaps { get; set; }
        public virtual ICollection<ProductToProductMap> ProductToProductMaps1 { get; set; }
        public virtual ICollection<ProductVideoMap> ProductVideoMaps { get; set; }
    }
}
