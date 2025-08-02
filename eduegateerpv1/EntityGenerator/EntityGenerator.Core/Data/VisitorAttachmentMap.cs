using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VisitorAttachmentMaps", Schema = "mutual")]
    public partial class VisitorAttachmentMap
    {
        [Key]
        public long VisitorAttachmentMapIID { get; set; }
        public long? VisitorID { get; set; }
        public long? QIDFrontAttachmentID { get; set; }
        public long? QIDBackAttachmentID { get; set; }
        public long? PassportFrontAttachmentID { get; set; }
        public long? PassportBackAttachmentID { get; set; }
        public long? VisitorProfileID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("VisitorID")]
        [InverseProperty("VisitorAttachmentMaps")]
        public virtual Visitor Visitor { get; set; }
    }
}
