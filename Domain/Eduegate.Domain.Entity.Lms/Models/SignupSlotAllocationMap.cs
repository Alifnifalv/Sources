using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("SignupSlotAllocationMaps", Schema = "signup")]
    public partial class SignupSlotAllocationMap
    {
        public SignupSlotAllocationMap()
        {
            SignupSlotRemarkMaps = new HashSet<SignupSlotRemarkMap>();
        }

        [Key]
        public long SignupSlotAllocationMapIID { get; set; }

        public long? SignupSlotMapID { get; set; }

        public long? StudentID { get; set; }

        public long? EmployeeID { get; set; }

        public long? ParentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SlotMapStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual School School { get; set; }

        public virtual SignupSlotMap SignupSlotMap { get; set; }

        public virtual SlotMapStatus SlotMapStatus { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }

    }
}