using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeesReportView
    {
        public long EmployeeIID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? DesignationID { get; set; }
        public long? DepartmentID { get; set; }
        public long? SchoolID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkEmail { get; set; }
        [StringLength(25)]
        public string WorkMobileNo { get; set; }
        public byte? GenderID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
    }
}
