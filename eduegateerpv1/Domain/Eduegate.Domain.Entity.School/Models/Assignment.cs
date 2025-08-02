using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Assignments", Schema = "schools")]
    public partial class Assignment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? DateOfSubmission { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FreezeDate { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public byte? AssignmentStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        //public byte[] TimeStamaps { get; set; }

        public byte? SchoolID { get; set; }

        [StringLength(20)]
        public string AssignmentNumber { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignmentAttachmentMap> AssignmentAttachmentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignmentDocument> AssignmentDocuments { get; set; }

        public virtual AssignmentStatus AssignmentStatus { get; set; }

        public virtual AssignmentType AssignmentType { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignmentSectionMap> AssignmentSectionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAssignmentMap> StudentAssignmentMaps { get; set; }
    }
}