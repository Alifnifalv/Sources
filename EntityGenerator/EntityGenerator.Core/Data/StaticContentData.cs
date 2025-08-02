using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StaticContentDatas", Schema = "cms")]
    public partial class StaticContentData
    {
        public long ContentDataIID { get; set; }
        public int? ContentTypeID { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(250)]
        public string ImageFilePath { get; set; }
        public string SerializedJsonParameters { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ContentTypeID")]
        public virtual StaticContentType ContentType { get; set; }
    }
}
