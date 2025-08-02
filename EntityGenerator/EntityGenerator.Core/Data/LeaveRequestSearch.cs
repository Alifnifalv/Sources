using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LeaveRequestSearch
    {
        public long LeaveApplicationIID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FromDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ToDate { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string LeaveType { get; set; }
        [StringLength(500)]
        public string OtherReason { get; set; }
        [StringLength(50)]
        public string LeaveStatus { get; set; }
    }
}
