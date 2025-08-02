namespace Eduegate.Domain.Entity.Contents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ContentFiles", Schema = "contents")]
    public partial class ContentFile
    {
        [Key]
        public long ContentFileIID { get; set; }

        public int? ContentTypeID { get; set; }

        public long? ReferenceID { get; set; }

        [StringLength(500)]
        public string ContentFileName { get; set; }

        public byte[] ContentData { get; set; }

        public bool? IsCompressed { get; set; }

        public byte[] ExtractedData { get; set; }


        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ContentType ContentType { get; set; }
        //public ICollection<object> GalleryAttachmentMaps { get; set; }
    }
}
