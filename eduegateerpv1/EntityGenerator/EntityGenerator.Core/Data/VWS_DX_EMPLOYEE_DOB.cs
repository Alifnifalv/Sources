using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_EMPLOYEE_DOB
    {
        [Required]
        [StringLength(500)]
        public string EmployeeProfile { get; set; }
        public long EmployeeIID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(502)]
        public string EmployeeName { get; set; }
        public long? DepartmentID { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public int DesignationID { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(25)]
        public string WorkMobileNo { get; set; }
        public int? Age { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkEmail { get; set; }
        [Required]
        [StringLength(1500)]
        public string ImageLink { get; set; }
    }
}
