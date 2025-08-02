using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentAssignmentMaps", Schema = "schools")]
    public partial class StudentAssignmentMap
    {
        [Key]
        public long StudentAssignmentMapIID { get; set; }
        public long? AssignmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfSubmission { get; set; }
        public byte? AssignmentStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamaps { get; set; }
        public long? AttachmentReferenceId { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        public long? StudentId { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("AssignmentID")]
        [InverseProperty("StudentAssignmentMaps")]
        public virtual Assignment Assignment { get; set; }
        [ForeignKey("AssignmentStatusID")]
        [InverseProperty("StudentAssignmentMaps")]
        public virtual AssignmentStatus AssignmentStatus { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("StudentAssignmentMaps")]
        public virtual Student Student { get; set; }
    }
}
