using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SeoMetadataCultureDatas", Schema = "mutual")]
    public partial class SeoMetadataCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long SEOMetadataID { get; set; }
        [StringLength(900)]
        public string PageTitle { get; set; }
        [StringLength(900)]
        public string MetaKeywords { get; set; }
        [StringLength(900)]
        public string MetaDescription { get; set; }
        [StringLength(100)]
        public string UrlKey { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("SeoMetadataCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("SEOMetadataID")]
        [InverseProperty("SeoMetadataCultureDatas")]
        public virtual SeoMetadata SEOMetadata { get; set; }
    }
}
