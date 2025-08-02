using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Loan
{
    [Keyless]
    public partial class LoanRequestSearch
    {
        public long LoanRequestIID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(555)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
    }
}
