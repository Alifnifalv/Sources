using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Products", Schema = "catalog")]
    public partial class Product
    {
        public Product()
        {
            ProductAllergyMaps = new HashSet<ProductAllergyMap>();
            ProductBundleFromProducts = new HashSet<ProductBundle>();
            ProductBundleToProducts = new HashSet<ProductBundle>();
            ProductCategoryMaps = new HashSet<ProductCategoryMap>();
            ProductCultureDatas = new HashSet<ProductCultureData>();
            ProductDeliveryCountrySettings = new HashSet<ProductDeliveryCountrySetting>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductImageMaps = new HashSet<ProductImageMap>();
            ProductInventoryProductConfigMaps = new HashSet<ProductInventoryProductConfigMap>();
            ProductLocationMaps = new HashSet<ProductLocationMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            ProductPriceListProductMaps = new HashSet<ProductPriceListProductMap>();
            ProductPriceListProductQuantityMaps = new HashSet<ProductPriceListProductQuantityMap>();
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            ProductSKUMaps = new HashSet<ProductSKUMap>();
            ProductSKURackMaps = new HashSet<ProductSKURackMap>();
            ProductStudentMaps = new HashSet<ProductStudentMap>();
            ProductTagMaps = new HashSet<ProductTagMap>();
            ProductTags = new HashSet<ProductTag>();
            ProductToProductMapProductIDToNavigations = new HashSet<ProductToProductMap>();
            ProductToProductMapProducts = new HashSet<ProductToProductMap>();
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Weight { get; set; }
        public long? ManufactureID { get; set; }
        public long? ManufactureCountryID { get; set; }
        public long? UnitGroupID { get; set; }
        public long? UnitID { get; set; }
        public bool? IsMultipleSKUEnabled { get; set; }
        public bool? IsOnline { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Calorie { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("Products")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("GLAccountID")]
        [InverseProperty("Products")]
        public virtual Account GLAccount { get; set; }
        [ForeignKey("ProductFamilyID")]
        [InverseProperty("Products")]
        public virtual ProductFamily ProductFamily { get; set; }
        [ForeignKey("PurchaseUnitID")]
        [InverseProperty("ProductPurchaseUnits")]
        public virtual Unit PurchaseUnit { get; set; }
        [ForeignKey("PurchaseUnitGroupID")]
        [InverseProperty("ProductPurchaseUnitGroups")]
        public virtual UnitGroup PurchaseUnitGroup { get; set; }
        [ForeignKey("SellingUnitID")]
        [InverseProperty("ProductSellingUnits")]
        public virtual Unit SellingUnit { get; set; }
        [ForeignKey("SellingUnitGroupID")]
        [InverseProperty("ProductSellingUnitGroups")]
        public virtual UnitGroup SellingUnitGroup { get; set; }
        [ForeignKey("SeoMetadataIID")]
        [InverseProperty("Products")]
        public virtual SeoMetadata SeoMetadataI { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Products")]
        public virtual ProductStatu Status { get; set; }
        [ForeignKey("TaxTemplateID")]
        [InverseProperty("Products")]
        public virtual TaxTemplate TaxTemplate { get; set; }
        [ForeignKey("UnitID")]
        [InverseProperty("ProductUnits")]
        public virtual Unit Unit { get; set; }
        [ForeignKey("UnitGroupID")]
        [InverseProperty("ProductUnitGroups")]
        public virtual UnitGroup UnitGroup { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductAllergyMap> ProductAllergyMaps { get; set; }
        [InverseProperty("FromProduct")]
        public virtual ICollection<ProductBundle> ProductBundleFromProducts { get; set; }
        [InverseProperty("ToProduct")]
        public virtual ICollection<ProductBundle> ProductBundleToProducts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductInventoryProductConfigMap> ProductInventoryProductConfigMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductSKURackMap> ProductSKURackMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductStudentMap> ProductStudentMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductTagMap> ProductTagMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        [InverseProperty("ProductIDToNavigation")]
        public virtual ICollection<ProductToProductMap> ProductToProductMapProductIDToNavigations { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductToProductMap> ProductToProductMapProducts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductVideoMap> ProductVideoMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
