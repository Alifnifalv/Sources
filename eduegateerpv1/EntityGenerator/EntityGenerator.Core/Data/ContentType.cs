using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ContentTypes", Schema = "contents")]
    public partial class ContentType
    {
        public ContentType()
        {
            ContentFiles = new HashSet<ContentFile>();
            UploadDocuments = new HashSet<UploadDocument>();
        }

        [Key]
        public int ContentTypeID { get; set; }
        [StringLength(50)]
        public string ContentName { get; set; }

        [InverseProperty("ContentType")]
        public virtual ICollection<ContentFile> ContentFiles { get; set; }
        [InverseProperty("ContentType")]
        public virtual ICollection<UploadDocument> UploadDocuments { get; set; }
    }
}
