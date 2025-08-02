using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Attachments", Schema = "mutual")]
    public partial class Attachment
    {
        [Key]
        public long AttachmentIID { get; set; }
        public int EntityTypeID { get; set; }
        public long ReferenceID { get; set; }
        [Required]
        [Unicode(false)]
        public string AttachmentName { get; set; }
        [Unicode(false)]
        public string FileName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? DepartmentID { get; set; }

        [ForeignKey("EntityTypeID")]
        [InverseProperty("Attachments")]
        public virtual EntityType EntityType { get; set; }
    }
}
