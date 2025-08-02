namespace Eduegate.Domain.Entity
{
    using Eduegate.Domain.Entity.School.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GalleryAttachmentMaps", Schema = "mutual")]
    public partial class GalleryAttachmentMap
    {
        [Key]
        public long GalleryAttachmentMapIID { get; set; }
        public long? GalleryID { get; set; }
        public long? AttachmentContentID { get; set; }
        public string AttachmentName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ContentFile AttachmentContent { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
}
