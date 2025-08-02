namespace Eduegate.Domain.Entity.HR.Payroll
{
    using Eduegate.Domain.Entity.HR.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeSalaryStructures", Schema = "payroll")]
    public partial class EmployeeSalaryStructure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeSalaryStructure()
        {
            EmployeeSalaryStructureComponentMaps = new HashSet<EmployeeSalaryStructureComponentMap>();
            EmployeeSalaryStructureLeaveSalaryMaps = new HashSet<EmployeeSalaryStructureLeaveSalaryMap>();
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
        }

        [Key]
        public long EmployeeSalaryStructureIID { get; set; }

        public bool? IsActive { get; set; }

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

        public long? LeaveSalaryStructureID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? JobInterviewMapID { get; set; }  

        public virtual Account Account { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }

        public virtual PayrollFrequency PayrollFrequency { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        public virtual SalaryPaymentMode SalaryPaymentMode { get; set; }

        public virtual SalaryStructure SalaryStructure { get; set; }

        public virtual SalaryStructure LeaveSalaryStructure { get; set; }

        public virtual ICollection<EmployeeSalaryStructureLeaveSalaryMap> EmployeeSalaryStructureLeaveSalaryMaps { get; set; }

        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }

    }
}
