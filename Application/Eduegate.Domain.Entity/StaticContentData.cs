namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.StaticContentDatas")]
    public partial class StaticContentData
    {
        [Key]
        [Column(Order = 0)]
        public long ContentDataIID { get; set; }

        public int? ContentTypeID { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string ImageFilePath { get; set; }

        public string SerializedJsonParameters { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamps { get; set; }

        public virtual StaticContentType StaticContentType { get; set; }
    }
}
