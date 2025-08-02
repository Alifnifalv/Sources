using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR
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
        public DateTime? FromDate { get; set; }
        public decimal? Amount { get; set; }
        public byte? PayrollFrequencyID { get; set; }
        public bool? IsSalaryBasedOnTimeSheet { get; set; }
        public int? TimeSheetSalaryComponentID { get; set; }
        public decimal? TimeSheetHourRate { get; set; }
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? OldDepartmentID { get; set; }
        public int? NewLeaveGroupID { get; set; }
        public int? OldLeaveGroupID { get; set; }
        public long? OldSalaryStructureID { get; set; }
        public long? NewSalaryStructureID { get; set; }
        public bool? IsApplyImmediately { get; set; }

        public virtual Account Account { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Branch NewBranch { get; set; }

        public virtual Designation NewDesignation { get; set; }

        public virtual LeaveGroup NewLeaveGroup { get; set; }

        public virtual EmployeeRole NewRole { get; set; }

        public virtual SalaryStructure NewSalaryStructure { get; set; }

        public virtual Branch OldBranch { get; set; }

        public virtual Designation OldDesignation { get; set; }

        public virtual LeaveGroup OldLeaveGroup { get; set; }

        public virtual EmployeeRole OldRole { get; set; }

        public virtual SalaryStructure OldSalaryStructure { get; set; }

        public virtual SalaryPaymentMode PaymentMode { get; set; }

        public virtual PayrollFrequency PayrollFrequency { get; set; }

        public virtual SalaryStructure SalaryStructure { get; set; }

        public virtual SalaryComponent TimeSheetSalaryComponent { get; set; }

        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }

        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
    }
}
