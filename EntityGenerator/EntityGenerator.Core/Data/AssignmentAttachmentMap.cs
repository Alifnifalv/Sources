using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssignmentAttachmentMaps", Schema = "schools")]
    [Index("AssignmentID", Name = "IDX_AssignmentAttachmentMaps_AssignmentID_")]
    [Index("AssignmentID", Name = "IDX_AssignmentAttachmentMaps_AssignmentID_AttachmentReferenceID__AttachmentName__AttachmentDescript")]
    public partial class AssignmentAttachmentMap
    {
        [Key]
        public long AssignmentAttachmentMapIID { get; set; }
        public long? AssignmentID { get; set; }
        public long? AttachmentReferenceID { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        [StringLength(1000)]
        public string AttachmentDescription { get; set; }
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AssignmentID")]
        [InverseProperty("AssignmentAttachmentMaps")]
        public virtual Assignment Assignment { get; set; }
    }
}
