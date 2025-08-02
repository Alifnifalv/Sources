using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{

    [Table("catalog.ProductSKUMaps")]
    public partial class ProductSKUMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductSKUMap()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            CustomerProductReferences = new HashSet<CustomerProductReference>();
            ProductBundles = new HashSet<ProductBundle>();
            ProductBundles1 = new HashSet<ProductBundle>();
            ProductImageMaps = new HashSet<ProductImageMap>();
            ProductInventorySKUConfigMaps = new HashSet<ProductInventorySKUConfigMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
            ProductPriceListSKUQuantityMaps = new HashSet<ProductPriceListSKUQuantityMap>();
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            ProductSKUCultureDatas = new HashSet<ProductSKUCultureData>();
            InventoryVerifications = new HashSet<InventoryVerification>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            JobEntryDetails = new HashSet<JobEntryDetail>();
            Notifies = new HashSet<Notify>();
            ProductDeliveryCountrySettings = new HashSet<ProductDeliveryCountrySetting>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductInventories = new HashSet<ProductInventory>();
            ProductInventorySerialMaps = new HashSet<ProductInventorySerialMap>();
            ProductLocationMaps = new HashSet<ProductLocationMap>();
            ProductSerialMaps = new HashSet<ProductSerialMap>();
            ProductSKURackMaps = new HashSet<ProductSKURackMap>();
            ProductSKUSiteMaps = new HashSet<ProductSKUSiteMap>();
            ProductSKUTagMaps = new HashSet<ProductSKUTagMap>();
            ProductVideoMaps = new HashSet<ProductVideoMap>();
            //SKUPaymentMethodExceptionMaps = new HashSet<SKUPaymentMethodExceptionMap>();
            TicketProductMaps = new HashSet<TicketProductMap>();
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        [Key]
        public long ProductSKUMapIID { get; set; }

        public long? ProductID { get; set; }

        public int? Sequence { get; set; }

        [StringLength(150)]
        public string ProductSKUCode { get; set; }

        [StringLength(50)]
        public string PartNo { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        public decimal? ProductPrice { get; set; }

        [StringLength(200)]
        public string VariantsMap { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? IsHiddenFromList { get; set; }

        public bool? HideSKU { get; set; }

        [StringLength(1000)]
        public string SKUName { get; set; }

        public byte? StatusID { get; set; }

        public long? SeoMetadataID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(20)]
        public string ProductSKUMapIIDTEXT { get; set; }

        public int? CompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBundle> ProductBundles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBundle> ProductBundles1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductInventorySKUConfigMap> ProductInventorySKUConfigMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notify> Notifies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSerialMap> ProductSerialMaps { get; set; }

        public virtual ProductStatu ProductStatu { get; set; }

        public virtual SeoMetadata SeoMetadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKURackMap> ProductSKURackMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKUTagMap> ProductSKUTagMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductVideoMap> ProductVideoMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
