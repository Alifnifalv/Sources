using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ParentPortalSchoolAcademicClassMap", Schema = "schools")]
    public partial class ParentPortalSchoolAcademicClassMap
    {
        [Key]
        public long ClassMapIID { get; set; }
        public int? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
    }
}
