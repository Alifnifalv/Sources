using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ContentFiles", Schema = "contents")]
    [Index("ContentFileIID", "ContentFileName", Name = "idx_ContentFile_FileIDName")]
    public partial class ContentFile
    {
        public ContentFile()
        {
            GalleryAttachmentMaps = new HashSet<GalleryAttachmentMap>();
        }

        [Key]
        public long ContentFileIID { get; set; }
        public int? ContentTypeID { get; set; }
        public long? ReferenceID { get; set; }
        [StringLength(500)]
        public string ContentFileName { get; set; }
        public byte[] ContentData { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsCompressed { get; set; }

        [ForeignKey("ContentTypeID")]
        [InverseProperty("ContentFiles")]
        public virtual ContentType ContentType { get; set; }
        [InverseProperty("AttachmentContent")]
        public virtual ICollection<GalleryAttachmentMap> GalleryAttachmentMaps { get; set; }
    }
}
