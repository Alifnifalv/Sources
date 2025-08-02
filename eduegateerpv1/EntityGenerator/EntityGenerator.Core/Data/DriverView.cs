using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DriverView
    {
        public long EmployeeIID { get; set; }
        [StringLength(50)]
        public string EmployeeAlias { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public int? EmployeeRoleID { get; set; }
        public int? DesignationID { get; set; }
        public long? BranchID { get; set; }
        [StringLength(500)]
        public string EmployeePhoto { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkEmail { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string WorkPhone { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string WorkMobileNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public int? JobTypeID { get; set; }
        public int? GenderID { get; set; }
        public long? DepartmentID { get; set; }
        public int? MaritalStatusID { get; set; }
        public long? ReportingEmployeeID { get; set; }
        public long? LoginID { get; set; }
    }
}
