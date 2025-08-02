namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentAssignmentMaps", Schema = "schools")]
    public partial class StudentAssignmentMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StudentAssignmentMapIID { get; set; }

        public long? AssignmentID { get; set; }

        public DateTime? DateOfSubmission { get; set; }

        public byte? AssignmentStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        //public byte[] TimeStamaps { get; set; }

        public long? AttachmentReferenceId { get; set; }

        public string Notes { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string AttachmentName { get; set; }

        public long? StudentId { get; set; }

        public string Remarks { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual AssignmentStatus AssignmentStatus { get; set; }

        public virtual Student Student { get; set; }
    }
}
