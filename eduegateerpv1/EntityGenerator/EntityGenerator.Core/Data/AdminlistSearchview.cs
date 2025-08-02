using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AdminlistSearchview
    {
        public long SignupSlotAllocationMapIID { get; set; }
        public long? SignupSlotMapID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(502)]
        public string Student { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(555)]
        public string EmployeeName { get; set; }
        public long? ParentID { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public byte? SlotAllocationStatusID { get; set; }
        [StringLength(100)]
        public string SlotAllocationStatus { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public byte? SlotMapStatusID { get; set; }
        [StringLength(100)]
        public string SlotMapStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SlotDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal? Duration { get; set; }
        [StringLength(8000)]
        [Unicode(false)]
        public string SlotTime { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
