using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSKUMap
    {
        public ProductSKUMap()
        {
            this.CustomerProductReferences = new List<CustomerProductReference>();
            this.ProductBundles = new List<ProductBundle>();
            this.ProductImageMaps = new List<ProductImageMap>();
            this.ProductInventorySKUConfigMaps = new List<ProductInventorySKUConfigMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
            this.ProductPriceListSKUMaps = new List<ProductPriceListSKUMap>();
            this.ProductPriceListSKUQuantityMaps = new List<ProductPriceListSKUQuantityMap>();
            this.ProductPropertyMaps = new List<ProductPropertyMap>();
            this.ProductSKUCultureDatas = new List<ProductSKUCultureData>();
            this.InventoryVerifications = new List<InventoryVerification>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.JobEntryDetails = new List<JobEntryDetail>();
            this.Notifies = new List<Notify>();
            this.ProductDeliveryCountrySettings = new List<ProductDeliveryCountrySetting>();
            this.ProductDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            this.ProductInventories = new List<ProductInventory>();
            this.ProductInventorySerialMaps = new List<ProductInventorySerialMap>();
            this.ProductLocationMaps = new List<ProductLocationMap>();
            this.ProductSerialMaps = new List<ProductSerialMap>();
            this.ProductSKUSiteMaps = new List<ProductSKUSiteMap>();
            this.ProductSKUTagMaps = new List<ProductSKUTagMap>();
            this.ProductVideoMaps = new List<ProductVideoMap>();
            this.SKUPaymentMethodExceptionMaps = new List<SKUPaymentMethodExceptionMap>();
        }

        public long ProductSKUMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<int> Sequence { get; set; }
        public string ProductSKUCode { get; set; }
        public string PartNo { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string VariantsMap { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<bool> IsHiddenFromList { get; set; }
        public Nullable<bool> HideSKU { get; set; }
        public string SKUName { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<long> SeoMetadataID { get; set; }
        public string ProductSKUMapIIDTEXT { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }
        public virtual ICollection<ProductBundle> ProductBundles { get; set; }
        public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }
        public virtual ICollection<ProductInventorySKUConfigMap> ProductInventorySKUConfigMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual ICollection<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }
        public virtual ICollection<Notify> Notifies { get; set; }
        public virtual ICollection<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
        public virtual ICollection<ProductSerialMap> ProductSerialMaps { get; set; }
        public virtual ProductSKUMap ProductSKUMaps1 { get; set; }
        public virtual ProductSKUMap ProductSKUMap1 { get; set; }
        public virtual ProductStatu ProductStatu { get; set; }
        public virtual SeoMetadata SeoMetadata { get; set; }
        public virtual ICollection<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }
        public virtual ICollection<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
        public virtual ICollection<ProductVideoMap> ProductVideoMaps { get; set; }
        public virtual ICollection<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }
    }
}
