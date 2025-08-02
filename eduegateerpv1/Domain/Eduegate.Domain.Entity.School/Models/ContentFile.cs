using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ContentFiles", Schema = "contents")]
    public partial class ContentFile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ContentType ContentType { get; set; }
        public virtual ICollection<GalleryAttachmentMap> GalleryAttachmentMaps { get; set; }
    }
}