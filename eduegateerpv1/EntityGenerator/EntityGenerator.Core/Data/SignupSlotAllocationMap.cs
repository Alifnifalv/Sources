using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? SlotMapStatusID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ParentID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SignupSlotMapID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual SignupSlotMap SignupSlotMap { get; set; }
        [ForeignKey("SlotMapStatusID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual SlotMapStatus SlotMapStatus { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("SignupSlotAllocationMaps")]
        public virtual Student Student { get; set; }
        [InverseProperty("SignupSlotAllocationMap")]
        public virtual ICollection<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }
    }
}
