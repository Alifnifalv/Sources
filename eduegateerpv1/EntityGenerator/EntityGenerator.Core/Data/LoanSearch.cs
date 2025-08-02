using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LoanSearch
    {
        public long LoanRequestIID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(555)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LoanRequestDate { get; set; }
        [StringLength(50)]
        public string LoanType { get; set; }
    }
}
