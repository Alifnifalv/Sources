using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LoansHistory_20240523
    {
        [Column("Date of Loan", TypeName = "datetime")]
        public DateTime? Date_of_Loan { get; set; }
        [Column("Emp No#")]
        public double? Emp_No_ { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Column("Balance_01/01/2024")]
        public double? Balance_01_01_2024 { get; set; }
        public double? Amount_Given { get; set; }
        [Column("Ampunt Recovered")]
        public double? Ampunt_Recovered { get; set; }
        [Column("Balance_till date")]
        public double? Balance_till_date { get; set; }
        [StringLength(255)]
        public string Nature { get; set; }
        [Column("Jan-24")]
        public double? Jan_24 { get; set; }
        [Column("Feb-24")]
        public double? Feb_24 { get; set; }
        [Column("Mar-24")]
        public double? Mar_24 { get; set; }
        [Column("Apr-24")]
        [StringLength(255)]
        public string Apr_24 { get; set; }
        [Column("May-24")]
        public double? May_24 { get; set; }
        [Column("Jun-24")]
        [StringLength(255)]
        public string Jun_24 { get; set; }
        [Column("Jul-24")]
        [StringLength(255)]
        public string Jul_24 { get; set; }
        [Column("Aug-24")]
        [StringLength(255)]
        public string Aug_24 { get; set; }
        [Column("Sep-24")]
        [StringLength(255)]
        public string Sep_24 { get; set; }
        [Column("Oct-24")]
        [StringLength(255)]
        public string Oct_24 { get; set; }
        [Column("Nov-24")]
        [StringLength(255)]
        public string Nov_24 { get; set; }
        [Column("Dec-24")]
        [StringLength(255)]
        public string Dec_24 { get; set; }
        [Column("Recovered_Till date")]
        public double? Recovered_Till_date { get; set; }
        [StringLength(255)]
        public string F22 { get; set; }
        [StringLength(255)]
        public string F23 { get; set; }
        [StringLength(255)]
        public string F24 { get; set; }
    }
}
