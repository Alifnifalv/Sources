using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeCodeCompareExcelExport", Schema = "schools")]
    public partial class EmployeeCodeCompareExcelExport
    {
        [Key]
        public long CompareDataIID { get; set; }
        [StringLength(30)]
        public string EmployeeCode { get; set; }
        [StringLength(30)]
        public string OldCode { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(20)]
        public string EmployeeStatus { get; set; }
    }
}
