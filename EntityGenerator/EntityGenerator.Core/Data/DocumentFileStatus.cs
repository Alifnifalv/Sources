using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentFileStatuses", Schema = "doc")]
    public partial class DocumentFileStatus
    {
        public DocumentFileStatus()
        {
            DocumentFiles = new HashSet<DocumentFile>();
        }

        [Key]
        public long DocumentStatusID { get; set; }
        [Required]
        [StringLength(10)]
        public string StatusName { get; set; }

        [InverseProperty("DocumentStatus")]
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
