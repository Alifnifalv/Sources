using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Payrolls
{
    [Table("EmployeeSalaryStructures", Schema = "payroll")]
    public partial class EmployeeSalaryStructure
    {
        public EmployeeSalaryStructure()
        {
        }

        [Key]
        public long EmployeeSalaryStructureIID { get; set; }

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

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public bool? IsActive { get; set; }

        public long? LeaveSalaryStructureID { get; set; }

        public virtual Account Account { get; set; }

        public virtual SalaryStructure LeaveSalaryStructure { get; set; }

        public virtual SalaryStructure SalaryStructure { get; set; }
    }
}