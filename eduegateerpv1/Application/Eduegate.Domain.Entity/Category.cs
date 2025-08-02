namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.Categories")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            CategoryCultureDatas = new HashSet<CategoryCultureData>();
            CategoryImageMaps = new HashSet<CategoryImageMap>();
            CategorySettings = new HashSet<CategorySetting>();
            CategoryTagMaps = new HashSet<CategoryTagMap>();
            CategoryTags = new HashSet<CategoryTag>();
            DeliveryTypes1 = new HashSet<DeliveryTypes1>();
            MenuLinkCategoryMaps = new HashSet<MenuLinkCategoryMap>();
            ProductCategoryMaps = new HashSet<ProductCategoryMap>();
            ProductPriceListCategoryMaps = new HashSet<ProductPriceListCategoryMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
        }

        [Key]
        public long CategoryIID { get; set; }

        public long? ParentCategoryID { get; set; }

        [StringLength(50)]
        public string CategoryCode { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string ImageName { get; set; }

        [StringLength(500)]
        public string ThumbnailImageName { get; set; }

        public long? SeoMetadataID { get; set; }

        public bool? IsInNavigationMenu { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? CultureID { get; set; }

        public decimal? Profit { get; set; }

        public bool? IsReporting { get; set; }

        public string TooltipMessage { get; set; }

        public int? SortOrder { get; set; }

        public string WarningMessage { get; set; }

        public int? ClassID { get; set; }

        public virtual SeoMetadata SeoMetadata { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryTag> CategoryTags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypes1> DeliveryTypes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
    }
}
