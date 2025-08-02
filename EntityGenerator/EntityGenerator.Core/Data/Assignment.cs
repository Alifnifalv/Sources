using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Assignments", Schema = "schools")]
    [Index("AcademicYearID", "AssignmentStatusID", "SchoolID", Name = "IDX_Assignments_AcademicYearID__AssignmentStatusID__SchoolID_ClassID__SubjectID")]
    [Index("AcademicYearID", "SchoolID", Name = "IDX_Assignments_AcademicYearID__SchoolID_ClassID__SubjectID__AssignmentStatusID")]
    [Index("ClassID", "SubjectID", "SectionID", "IsActive", Name = "IDX_Assignments_ClassID__SubjectID__SectionID__IsActive_AcademicYearID__AssignmentTypeID__DateOfSub")]
    public partial class Assignment
    {
        public Assignment()
        {
            AssignmentAttachmentMaps = new HashSet<AssignmentAttachmentMap>();
            AssignmentDocuments = new HashSet<AssignmentDocument>();
            AssignmentSectionMaps = new HashSet<AssignmentSectionMap>();
            StudentAssignmentMaps = new HashSet<StudentAssignmentMap>();
        }

        [Key]
        public long AssignmentIID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? AssignmentTypeID { get; set; }
        public int? ClassID { get; set; }
        public int? SubjectID { get; set; }
        public int? SectionID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfSubmission { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FreezeDate { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public byte? AssignmentStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamaps { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(20)]
        public string AssignmentNumber { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Assignments")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AssignmentStatusID")]
        [InverseProperty("Assignments")]
        public virtual AssignmentStatus AssignmentStatus { get; set; }
        [ForeignKey("AssignmentTypeID")]
        [InverseProperty("Assignments")]
        public virtual AssignmentType AssignmentType { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Assignments")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Assignments")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("Assignments")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("Assignments")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("Assignment")]
        public virtual ICollection<AssignmentAttachmentMap> AssignmentAttachmentMaps { get; set; }
        [InverseProperty("Assignment")]
        public virtual ICollection<AssignmentDocument> AssignmentDocuments { get; set; }
        [InverseProperty("Assignment")]
        public virtual ICollection<AssignmentSectionMap> AssignmentSectionMaps { get; set; }
        [InverseProperty("Assignment")]
        public virtual ICollection<StudentAssignmentMap> StudentAssignmentMaps { get; set; }
    }
}
