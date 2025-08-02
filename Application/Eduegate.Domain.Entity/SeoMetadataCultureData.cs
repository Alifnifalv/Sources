namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.SeoMetadataCultureDatas")]
    public partial class SeoMetadataCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual SeoMetadata SeoMetadata { get; set; }
    }
}
