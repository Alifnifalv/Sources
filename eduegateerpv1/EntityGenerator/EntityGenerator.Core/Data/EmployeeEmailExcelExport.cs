using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeEmailExcelExport", Schema = "schools")]
    public partial class EmployeeEmailExcelExport
    {
        [Key]
        public long EmployeeEmailDataIID { get; set; }
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        [StringLength(100)]
        public string EmployeeNumber { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(100)]
        public string WorkEmail { get; set; }
    }
}
