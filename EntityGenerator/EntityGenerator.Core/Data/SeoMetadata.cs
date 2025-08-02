using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SeoMetadatas", Schema = "mutual")]
    public partial class SeoMetadata
    {
        public SeoMetadata()
        {
            Brands = new HashSet<Brand>();
            Categories = new HashSet<Category>();
            ProductSKUMaps = new HashSet<ProductSKUMap>();
            Products = new HashSet<Product>();
            SeoMetadataCultureDatas = new HashSet<SeoMetadataCultureData>();
        }

        [Key]
        public long SEOMetadataIID { get; set; }
        [StringLength(900)]
        public string PageTitle { get; set; }
        [StringLength(900)]
        public string MetaKeywords { get; set; }
        [StringLength(900)]
        public string MetaDescription { get; set; }
        [StringLength(900)]
        public string UrlKey { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("SEOMetadata")]
        public virtual ICollection<Brand> Brands { get; set; }
        [InverseProperty("SeoMetadata")]
        public virtual ICollection<Category> Categories { get; set; }
        [InverseProperty("SeoMetadata")]
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }
        [InverseProperty("SeoMetadataI")]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty("SEOMetadata")]
        public virtual ICollection<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
    }
}
