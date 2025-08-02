using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Staff_Advance_20240831
    {
        [Column("Date of Loan", TypeName = "datetime")]
        public DateTime? Date_of_Loan { get; set; }
        [Column("Emp No#")]
        public double? Emp_No_ { get; set; }
        public double? vlookup { get; set; }
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
        public double? Jan_2024 { get; set; }
        public double? Feb_2024 { get; set; }
        public double? Mar_2024 { get; set; }
        public double? Apr_2024 { get; set; }
        public double? May_2024 { get; set; }
        public double? Jun_2024 { get; set; }
        public double? Jul_2024 { get; set; }
        public double? Aug_2024 { get; set; }
        public double? Sep_2024 { get; set; }
        public double? Oct_2024 { get; set; }
        public double? Nov_2024 { get; set; }
        public double? Dec_2024 { get; set; }
        [Column("Recovered_Till date")]
        public double? Recovered_Till_date { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string EMPCODE { get; set; }
        public long? LoanHeadIID { get; set; }
        public int LID { get; set; }
        public long? EmployeeIID { get; set; }
        [Column(TypeName = "money")]
        public decimal? InstallAmount { get; set; }
        public long? LoanRequestIID { get; set; }
    }
}
