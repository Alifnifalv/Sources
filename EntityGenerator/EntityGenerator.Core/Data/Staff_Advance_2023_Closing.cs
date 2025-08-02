using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Staff_Advance_2023_Closing
    {
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("EMP Code")]
        public double? EMP_Code { get; set; }
        [StringLength(255)]
        public string F3 { get; set; }
        [Column("Amount Paid")]
        public double? Amount_Paid { get; set; }
        [Column("Ampunt Recovered")]
        public double? Ampunt_Recovered { get; set; }
        [StringLength(255)]
        public string F6 { get; set; }
        public double? Balance { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
        [StringLength(255)]
        public string Campus { get; set; }
        public long? SL_ACCOUNTID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string EmployeeCode { get; set; }
        public long? EmployeeID { get; set; }
        public long? AccountID { get; set; }
    }
}
