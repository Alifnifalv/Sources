using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("GalleryAttachmentMaps", Schema = "mutual")]
    public partial class GalleryAttachmentMap
    {
        [Key]
        public long GalleryAttachmentMapIID { get; set; }
        public long? GalleryID { get; set; }
        public long? AttachmentContentID { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AttachmentContentID")]
        [InverseProperty("GalleryAttachmentMaps")]
        public virtual ContentFile AttachmentContent { get; set; }
        [ForeignKey("GalleryID")]
        [InverseProperty("GalleryAttachmentMaps")]
        public virtual Gallery Gallery { get; set; }
    }
}
