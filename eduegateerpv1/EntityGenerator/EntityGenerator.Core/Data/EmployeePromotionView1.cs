using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeePromotionView1
    {
        public long EmployeePromotionIID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public byte? PayrollFrequencyID { get; set; }
        public bool? IsSalaryBasedOnTimeSheet { get; set; }
        public int? TimeSheetSalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TimeSheetHourRate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TimeSheetMaximumBenefits { get; set; }
        public int? PaymentModeID { get; set; }
        public long? AccountID { get; set; }
        public int? OldDesignationID { get; set; }
        public long? OldBranchID { get; set; }
        public int? NewDesignationID { get; set; }
        public long? NewBranchID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? NewLeaveGroupID { get; set; }
        public int? OldLeaveGroupID { get; set; }
        [StringLength(50)]
        public string OldLeaveGroup { get; set; }
        [StringLength(50)]
        public string NewLeaveGroup { get; set; }
        [StringLength(555)]
        public string EmployeeName { get; set; }
        [StringLength(500)]
        public string OldSalaryStructure { get; set; }
        [StringLength(500)]
        public string NewSalaryStructure { get; set; }
        [StringLength(50)]
        public string PayrollFrequency { get; set; }
        [StringLength(50)]
        public string PaymentName { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [StringLength(50)]
        public string OldDesignation { get; set; }
        [StringLength(50)]
        public string NewDesignation { get; set; }
        [StringLength(50)]
        public string OldSchool { get; set; }
        [StringLength(50)]
        public string NewSchool { get; set; }
    }
}
