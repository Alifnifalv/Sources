using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Brands", Schema = "catalog")]
    [Index("BrandCode", Name = "UQ_BrandCode", IsUnique = true)]
    public partial class Brand
    {
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
        [Unicode(false)]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? SEOMetadataID { get; set; }

        [ForeignKey("SEOMetadataID")]
        [InverseProperty("Brands")]
        public virtual SeoMetadata SEOMetadata { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Brands")]
        public virtual BrandStatus Status { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<BrandImageMap> BrandImageMaps { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<BrandTagMap> BrandTagMaps { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<BrandTag> BrandTags { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        [InverseProperty("Brand")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
