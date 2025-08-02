using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeeIDCardReportView
    {
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }
        [StringLength(50)]
        public string EmergencyContactNo { get; set; }
        [Required]
        [StringLength(555)]
        public string Employee { get; set; }
        public long? BranchID { get; set; }
        public long EmployeeIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        public byte[] EmployeeImage { get; set; }
        [Required]
        [StringLength(502)]
        public string EmployeeName { get; set; }
        public int? DesignationID { get; set; }
        [StringLength(500)]
        public string EmployeePhoto { get; set; }
    }
}
