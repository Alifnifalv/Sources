using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Streams", Schema = "schools")]
    public partial class Stream
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stream()
        {
            StreamOptionalSubjectMaps = new HashSet<StreamOptionalSubjectMap>();
            StreamSubjectMaps = new HashSet<StreamSubjectMap>();
            StudentApplicationOptionalSubjectMaps = new HashSet<StudentApplicationOptionalSubjectMap>();
            StudentApplications = new HashSet<StudentApplication>();
            StudentStreamOptionalSubjectMaps = new HashSet<StudentStreamOptionalSubjectMap>();
            Students = new HashSet<Student>();
        }
        [Key]
        public byte StreamID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public byte? SchoolID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? StreamGroupID { get; set; }


        public bool? IsActive { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        public virtual StreamGroup StreamGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StreamOptionalSubjectMap> StreamOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StreamSubjectMap> StreamSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}