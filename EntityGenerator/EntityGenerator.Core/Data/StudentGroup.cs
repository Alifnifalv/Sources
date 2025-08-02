using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentGroups", Schema = "schools")]
    public partial class StudentGroup
    {
        public StudentGroup()
        {
            PackageConfigStudentGroupMaps = new HashSet<PackageConfigStudentGroupMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentGroupMaps = new HashSet<StudentGroupMap>();
        }

        [Key]
        public int StudentGroupID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        public bool? IsActive { get; set; }
        public int? GroupTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentGroups")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("GroupTypeID")]
        [InverseProperty("StudentGroups")]
        public virtual StudentGroupType GroupType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentGroups")]
        public virtual School School { get; set; }
        [InverseProperty("StudentGroup")]
        public virtual ICollection<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }
        [InverseProperty("StudentGroup")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
        [InverseProperty("StudentGroup")]
        public virtual ICollection<StudentGroupMap> StudentGroupMaps { get; set; }
    }
}
