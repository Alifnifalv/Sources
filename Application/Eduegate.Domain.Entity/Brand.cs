namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.Brands")]
    public partial class Brand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brand()
        {
            BrandCultureDatas = new HashSet<BrandCultureData>();
            BrandImageMaps = new HashSet<BrandImageMap>();
            BrandTagMaps = new HashSet<BrandTagMap>();
            BrandTags = new HashSet<BrandTag>();
            MenuLinkBrandMaps = new HashSet<MenuLinkBrandMap>();
            ProductPriceListBrandMaps = new HashSet<ProductPriceListBrandMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            Products = new HashSet<Product>();
        }

        [Key]
        public long BrandIID { get; set; }

        [StringLength(50)]
        public string BrandCode { get; set; }

        [StringLength(50)]
        public string BrandName { get; set; }

        [StringLength(1000)]
        public string Descirption { get; set; }

        [StringLength(300)]
        public string LogoFile { get; set; }

        public long? IsIncludeHomePage { get; set; }

        public byte? StatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? SEOMetadataID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandImageMap> BrandImageMaps { get; set; }

        public virtual BrandStatus BrandStatus { get; set; }

        public virtual SeoMetadata SeoMetadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandTagMap> BrandTagMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandTag> BrandTags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
