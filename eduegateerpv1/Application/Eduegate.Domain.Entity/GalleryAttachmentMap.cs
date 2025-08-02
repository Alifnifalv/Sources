namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.GalleryAttachmentMaps")]
    public partial class GalleryAttachmentMap
    {
        [Key]
        public long GalleryAttachmentMapIID { get; set; }

        public long? GalleryID { get; set; }

        public long? AttachmentContentID { get; set; }

        [StringLength(50)]
        public string AttachmentName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ContentFile ContentFile { get; set; }

        public virtual Gallery Gallery { get; set; }
    }
}
