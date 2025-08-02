using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Employees_Payroll_20220713
    {
        public double? SlNo { get; set; }
        public double? EmpNo { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Designation { get; set; }
        [StringLength(255)]
        public string Nationality { get; set; }
        [StringLength(255)]
        public string Sponsoredby { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateJoined { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
        [StringLength(255)]
        public string Location { get; set; }
        [StringLength(255)]
        public string Cat { get; set; }
        [StringLength(255)]
        public string Functional { get; set; }
        public double? Basic { get; set; }
        public double? HRA { get; set; }
        public double? TransAll { get; set; }
        public double? OtherAllow { get; set; }
        public double? Functional_Allowance { get; set; }
        public double? Total { get; set; }
        [StringLength(255)]
        public string LastDays { get; set; }
        public double? ESBDays { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date_Left { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Reason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Leave_Started { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Resumed_from_leave { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Leave_DueFrom { get; set; }
        public double? BalLastDays { get; set; }
        [StringLength(255)]
        public string PPNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PPExpiry { get; set; }
        public double? RPNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RPExpiry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Sponsor { get; set; }
        [StringLength(255)]
        public string IBAN_PAYCARD { get; set; }
        [StringLength(255)]
        public string SWIFT_Code { get; set; }
        public double? Bank_Account_No { get; set; }
        [StringLength(255)]
        public string Bank_Short_Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BankName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Branch { get; set; }
    }
}
