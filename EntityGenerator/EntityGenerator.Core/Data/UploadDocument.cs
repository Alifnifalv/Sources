using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UploadDocuments", Schema = "schools")]
    public partial class UploadDocument
    {
        public UploadDocument()
        {
            ParentUploadDocumentMaps = new HashSet<ParentUploadDocumentMap>();
        }

        [Key]
        public long UploadDocumentIID { get; set; }
        public int? ContentTypeID { get; set; }
        [StringLength(50)]
        public string DocumentFileName { get; set; }
        public byte[] ContentData { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(25)]
        public string ReferenceID { get; set; }
        public byte? UploadDocumentTypeID { get; set; }

        [ForeignKey("ContentTypeID")]
        [InverseProperty("UploadDocuments")]
        public virtual ContentType ContentType { get; set; }
        [ForeignKey("UploadDocumentTypeID")]
        [InverseProperty("UploadDocuments")]
        public virtual UploadDocumentType UploadDocumentType { get; set; }
        [InverseProperty("UploadDocument")]
        public virtual ICollection<ParentUploadDocumentMap> ParentUploadDocumentMaps { get; set; }
    }
}
