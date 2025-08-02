using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKUMaps", Schema = "catalog")]
    [Index("BarCode", "ProductSKUMapIID", Name = "IDX_ProductSKUMaps_BarCodeProductSKUMapIID_")]
    [Index("BarCode", Name = "IDX_ProductSKUMaps_BarCode_")]
    [Index("ProductID", Name = "idx_prodSkuPartBarCode")]
    public partial class ProductSKUMap
    {
        public ProductSKUMap()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            CustomerProductReferences = new HashSet<CustomerProductReference>();
            InventoryVerifications = new HashSet<InventoryVerification>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            JobEntryDetails = new HashSet<JobEntryDetail>();
            Notifies = new HashSet<Notify>();
            ProductBundleFromProductSKUMaps = new HashSet<ProductBundle>();
            ProductBundleToProductSKUMaps = new HashSet<ProductBundle>();
            ProductDeliveryCountrySettings = new HashSet<ProductDeliveryCountrySetting>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductImageMaps = new HashSet<ProductImageMap>();
            ProductInventories = new HashSet<ProductInventory>();
            ProductInventorySKUConfigMaps = new HashSet<ProductInventorySKUConfigMap>();
            ProductInventorySerialMaps = new HashSet<ProductInventorySerialMap>();
            ProductLocationMaps = new HashSet<ProductLocationMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
            ProductPriceListSKUQuantityMaps = new HashSet<ProductPriceListSKUQuantityMap>();
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            ProductSKUBranchMaps = new HashSet<ProductSKUBranchMap>();
            ProductSKUCultureDatas = new HashSet<ProductSKUCultureData>();
            ProductSKURackMaps = new HashSet<ProductSKURackMap>();
            ProductSKUSiteMaps = new HashSet<ProductSKUSiteMap>();
            ProductSKUTagMaps = new HashSet<ProductSKUTagMap>();
            ProductSerialMaps = new HashSet<ProductSerialMap>();
            ProductStudentMaps = new HashSet<ProductStudentMap>();
            ProductVideoMaps = new HashSet<ProductVideoMap>();
            SKUMapBarCodeMaps = new HashSet<SKUMapBarCodeMap>();
            SKUPaymentMethodExceptionMaps = new HashSet<SKUPaymentMethodExceptionMap>();
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductPrice { get; set; }
        [StringLength(200)]
        public string VariantsMap { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsHiddenFromList { get; set; }
        public bool? HideSKU { get; set; }
        [StringLength(1000)]
        public string SKUName { get; set; }
        public byte? StatusID { get; set; }
        public long? SeoMetadataID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ProductSKUMapIIDTEXT { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string ExternalCode { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? ProductNos { get; set; }
        public string WarningMessage { get; set; }
        public bool? IsOutOfStock { get; set; }
        public bool? IsBulk { get; set; }
        public int? ProductOptionGroupID { get; set; }
        [StringLength(50)]
        public string ProductNosText { get; set; }
        [StringLength(1000)]
        public string RelevenceSortName { get; set; }
        public string TooltipMessage { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductSKUMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("SeoMetadataID")]
        [InverseProperty("ProductSKUMaps")]
        public virtual SeoMetadata SeoMetadata { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("ProductSKUMaps")]
        public virtual ProductStatu Status { get; set; }
        [InverseProperty("ProductSKU")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("ProductSKU")]
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<Notify> Notifies { get; set; }
        [InverseProperty("FromProductSKUMap")]
        public virtual ICollection<ProductBundle> ProductBundleFromProductSKUMaps { get; set; }
        [InverseProperty("ToProductSKUMap")]
        public virtual ICollection<ProductBundle> ProductBundleToProductSKUMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductInventorySKUConfigMap> ProductInventorySKUConfigMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        [InverseProperty("ProductSKU")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        [InverseProperty("ProductSKU")]
        public virtual ICollection<ProductSKUBranchMap> ProductSKUBranchMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductSKURackMap> ProductSKURackMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }
        [InverseProperty("ProductSKuMap")]
        public virtual ICollection<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductSerialMap> ProductSerialMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductStudentMap> ProductStudentMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<ProductVideoMap> ProductVideoMaps { get; set; }
        [InverseProperty("SKUMap")]
        public virtual ICollection<SKUMapBarCodeMap> SKUMapBarCodeMaps { get; set; }
        [InverseProperty("SKU")]
        public virtual ICollection<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
        [InverseProperty("ProductSKUMap")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
