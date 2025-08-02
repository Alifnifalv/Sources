using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeeSalaryStructureViewSearch
    {
        public long EmployeeSalaryStructureIID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(555)]
        public string EmployeeName { get; set; }
        public long? SalaryStructureID { get; set; }
        [StringLength(500)]
        public string StructureName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public bool? IsSalaryBasedOnTimeSheet { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TimeSheetHourRate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TimeSheetMaximumBenefits { get; set; }
        public byte? PayrollFrequencyID { get; set; }
        [StringLength(50)]
        public string PayrollFrequency { get; set; }
        public int? TimeSheetSalaryComponentID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentName { get; set; }
        public long? AccountID { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
    }
}
