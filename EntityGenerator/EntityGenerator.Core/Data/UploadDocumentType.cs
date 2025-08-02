using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UploadDocumentTypes", Schema = "schools")]
    public partial class UploadDocumentType
    {
        public UploadDocumentType()
        {
            ParentUploadDocumentMaps = new HashSet<ParentUploadDocumentMap>();
            UploadDocuments = new HashSet<UploadDocument>();
        }

        [Key]
        public byte UploadDocumentTypeID { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public bool? IsMandatory { get; set; }

        [InverseProperty("UploadDocumentType")]
        public virtual ICollection<ParentUploadDocumentMap> ParentUploadDocumentMaps { get; set; }
        [InverseProperty("UploadDocumentType")]
        public virtual ICollection<UploadDocument> UploadDocuments { get; set; }
    }
}
