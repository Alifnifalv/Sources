using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("News", Schema = "cms")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }

        [ForeignKey("NewsTypeID")]
        [InverseProperty("News")]
        public virtual NewsType NewsType { get; set; }
    }
}
