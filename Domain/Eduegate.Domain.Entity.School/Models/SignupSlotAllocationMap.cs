    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("SignupSlotAllocationMaps", Schema = "signup")]
    public partial class SignupSlotAllocationMap
    {
        [Key]
        public long SignupSlotAllocationMapIID { get; set; }

        public long? SignupSlotMapID { get; set; }

        public long? StudentID { get; set; }

        public long? EmployeeID { get; set; }

        public long? ParentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual Schools School { get; set; }

        public virtual Student Student { get; set; }

        public virtual SignupSlotMap SignupSlotMap { get; set; }
    }
}