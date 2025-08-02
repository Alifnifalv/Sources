using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeePromotions", Schema = "payroll")]
    public partial class EmployeePromotion
    {
        public EmployeePromotion()
        {
            EmployeePromotionLeaveAllocations = new HashSet<EmployeePromotionLeaveAllocation>();
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
        }

        [Key]
        public long EmployeePromotionIID { get; set; }
        public long? EmployeeID { get; set; }
        public long? SalaryStructureID { get; set; }
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
        public int? OldRoleID { get; set; }
        public int? OldDesignationID { get; set; }
        public long? OldBranchID { get; set; }
        public int? NewRoleID { get; set; }
        public int? NewDesignationID { get; set; }
        public long? NewBranchID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? OldDepartmentID { get; set; }
        public int? NewLeaveGroupID { get; set; }
        public int? OldLeaveGroupID { get; set; }
        public long? OldSalaryStructureID { get; set; }
        public long? NewSalaryStructureID { get; set; }
        public bool? IsApplyImmediately { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("EmployeePromotions")]
        public virtual Account Account { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeePromotions")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("NewBranchID")]
        [InverseProperty("EmployeePromotionNewBranches")]
        public virtual Branch NewBranch { get; set; }
        [ForeignKey("NewDesignationID")]
        [InverseProperty("EmployeePromotionNewDesignations")]
        public virtual Designation NewDesignation { get; set; }
        [ForeignKey("NewLeaveGroupID")]
        [InverseProperty("EmployeePromotionNewLeaveGroups")]
        public virtual LeaveGroup NewLeaveGroup { get; set; }
        [ForeignKey("NewRoleID")]
        [InverseProperty("EmployeePromotionNewRoles")]
        public virtual EmployeeRole NewRole { get; set; }
        [ForeignKey("NewSalaryStructureID")]
        [InverseProperty("EmployeePromotionNewSalaryStructures")]
        public virtual SalaryStructure NewSalaryStructure { get; set; }
        [ForeignKey("OldBranchID")]
        [InverseProperty("EmployeePromotionOldBranches")]
        public virtual Branch OldBranch { get; set; }
        [ForeignKey("OldDesignationID")]
        [InverseProperty("EmployeePromotionOldDesignations")]
        public virtual Designation OldDesignation { get; set; }
        [ForeignKey("OldLeaveGroupID")]
        [InverseProperty("EmployeePromotionOldLeaveGroups")]
        public virtual LeaveGroup OldLeaveGroup { get; set; }
        [ForeignKey("OldRoleID")]
        [InverseProperty("EmployeePromotionOldRoles")]
        public virtual EmployeeRole OldRole { get; set; }
        [ForeignKey("OldSalaryStructureID")]
        [InverseProperty("EmployeePromotionOldSalaryStructures")]
        public virtual SalaryStructure OldSalaryStructure { get; set; }
        [ForeignKey("PaymentModeID")]
        [InverseProperty("EmployeePromotions")]
        public virtual SalaryPaymentMode PaymentMode { get; set; }
        [ForeignKey("PayrollFrequencyID")]
        [InverseProperty("EmployeePromotions")]
        public virtual PayrollFrequency PayrollFrequency { get; set; }
        [ForeignKey("SalaryStructureID")]
        [InverseProperty("EmployeePromotionSalaryStructures")]
        public virtual SalaryStructure SalaryStructure { get; set; }
        [ForeignKey("TimeSheetSalaryComponentID")]
        [InverseProperty("EmployeePromotions")]
        public virtual SalaryComponent TimeSheetSalaryComponent { get; set; }
        [InverseProperty("EmployeePromotion")]
        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }
        [InverseProperty("EmployeePromotion")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
    }
}
