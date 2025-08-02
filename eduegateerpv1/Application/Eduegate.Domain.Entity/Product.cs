namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.Products")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductAllergyMaps = new HashSet<ProductAllergyMap>();
            ProductBundles = new HashSet<ProductBundle>();
            ProductBundles1 = new HashSet<ProductBundle>();
            ProductCategoryMaps = new HashSet<ProductCategoryMap>();
            ProductCultureDatas = new HashSet<ProductCultureData>();
            ProductImageMaps = new HashSet<ProductImageMap>();
            ProductInventoryProductConfigMaps = new HashSet<ProductInventoryProductConfigMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            ProductPriceListProductMaps = new HashSet<ProductPriceListProductMap>();
            ProductPriceListProductQuantityMaps = new HashSet<ProductPriceListProductQuantityMap>();
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            ProductDeliveryCountrySettings = new HashSet<ProductDeliveryCountrySetting>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductLocationMaps = new HashSet<ProductLocationMap>();
            ProductSKUMaps = new HashSet<ProductSKUMap>();
            ProductSKURackMaps = new HashSet<ProductSKURackMap>();
            ProductStudentMaps = new HashSet<ProductStudentMap>();
            ProductTagMaps = new HashSet<ProductTagMap>();
            ProductTags = new HashSet<ProductTag>();
            ProductToProductMaps = new HashSet<ProductToProductMap>();
            ProductToProductMaps1 = new HashSet<ProductToProductMap>();
            ProductVideoMaps = new HashSet<ProductVideoMap>();
            TicketProductMaps = new HashSet<TicketProductMap>();
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        [Key]
        public long ProductIID { get; set; }

        [Required]
        [StringLength(150)]
        public string ProductCode { get; set; }

        public long? ProductTypeID { get; set; }

        public long? ProductFamilyID { get; set; }

        public long? BrandID { get; set; }

        [StringLength(1000)]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public byte? StatusID { get; set; }

        public long? SeoMetadataIID { get; set; }

        public decimal? Weight { get; set; }

        public long? ManufactureID { get; set; }

        public long? ManufactureCountryID { get; set; }

        public long? UnitGroupID { get; set; }

        public long? UnitID { get; set; }

        public bool? IsMultipleSKUEnabled { get; set; }

        public bool? IsOnline { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? ProductOwnderID { get; set; }

        public int? TaxTemplateID { get; set; }

        public long? BaseUnitID { get; set; }

        public bool? IsWeighingProduct { get; set; }

        public long? PurchaseUnitGroupID { get; set; }

        public long? SellingUnitGroupID { get; set; }

        public long? PurchaseUnitID { get; set; }

        public long? SellingUnitID { get; set; }

        public bool? IsActive { get; set; }

        public long? GLAccountID { get; set; }

        public decimal? Calorie { get; set; }

        public virtual Account Account { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAllergyMap> ProductAllergyMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBundle> ProductBundles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBundle> ProductBundles1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }

        public virtual ProductFamily ProductFamily { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductInventoryProductConfigMap> ProductInventoryProductConfigMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }

        public virtual ProductStatu ProductStatu { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual UnitGroup UnitGroup { get; set; }

        public virtual Unit Unit1 { get; set; }

        public virtual UnitGroup UnitGroup1 { get; set; }

        public virtual SeoMetadata SeoMetadata { get; set; }

        public virtual TaxTemplate TaxTemplate { get; set; }

        public virtual UnitGroup UnitGroup2 { get; set; }

        public virtual Unit Unit2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKURackMap> ProductSKURackMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductStudentMap> ProductStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTagMap> ProductTagMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTag> ProductTags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductToProductMap> ProductToProductMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductToProductMap> ProductToProductMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductVideoMap> ProductVideoMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
