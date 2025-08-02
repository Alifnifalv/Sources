using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StaffAdv_April_2024
    {
        public double? EmpCode { get; set; }
        [StringLength(255)]
        public string EmpName { get; set; }
        public double? Amount { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string EmployeeCode { get; set; }
        public long? EmployeeID { get; set; }
        public long? AccountID { get; set; }
        public long? SL_AccountID { get; set; }
    }
}
