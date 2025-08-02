using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StudentGroups", Schema = "schools")]
    public partial class StudentGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentGroup()
        {
            PackageConfigStudentGroupMaps = new HashSet<PackageConfigStudentGroupMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentGroupMaps = new HashSet<StudentGroupMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentGroupID { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public bool? IsActive { get; set; }

        public int? GroupTypeID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGroupMap> StudentGroupMaps { get; set; }

        public virtual StudentGroupType StudentGroupType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
    }
}

