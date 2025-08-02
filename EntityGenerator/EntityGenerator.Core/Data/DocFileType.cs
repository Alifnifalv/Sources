using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocFileTypes", Schema = "doc")]
    public partial class DocFileType
    {
        public DocFileType()
        {
            DocumentFiles = new HashSet<DocumentFile>();
        }

        [Key]
        public int DocFileTypeID { get; set; }
        [StringLength(50)]
        public string FileTypeName { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string FileExtension { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("DocFileType")]
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
