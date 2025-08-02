using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeExcelExport", Schema = "schools")]
    public partial class EmployeeExcelExport
    {
        [Key]
        public long EmployeeDataIID { get; set; }
        [StringLength(100)]
        public string SequenceNumber { get; set; }
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        [StringLength(100)]
        public string EmployeeNumber { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        [StringLength(100)]
        public string Nationality { get; set; }
        [StringLength(100)]
        public string SponsoredBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoined { get; set; }
        [StringLength(100)]
        public string Status { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
        [StringLength(100)]
        public string Category { get; set; }
        [StringLength(100)]
        public string FunctionName { get; set; }
        [StringLength(100)]
        public string NationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDExpiryDate { get; set; }
        [StringLength(100)]
        public string Sponsor { get; set; }
        [StringLength(100)]
        public string IBANNumber { get; set; }
        [StringLength(100)]
        public string SWIFTCode { get; set; }
        [StringLength(100)]
        public string BankAccountNumber { get; set; }
        [StringLength(100)]
        public string BankShortName { get; set; }
        [StringLength(100)]
        public string BankName { get; set; }
        [StringLength(100)]
        public string Branch { get; set; }
    }
}
