namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.News")]
    public partial class News
    {
        [Key]
        public long NewsIID { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string NewsContentShort { get; set; }

        public string NewsContent { get; set; }

        [StringLength(500)]
        public string ThumbnailUrl { get; set; }

        public int? NewsTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? SiteID { get; set; }

        public virtual NewsType NewsType { get; set; }
    }
}
