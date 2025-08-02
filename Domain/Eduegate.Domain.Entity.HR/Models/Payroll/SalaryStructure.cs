namespace Eduegate.Domain.Entity.HR.Payroll
{
    using Eduegate.Domain.Entity.HR.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("SalaryStructure", Schema = "payroll")]
    public partial class SalaryStructure
    {
        public SalaryStructure()
        {
            EmployeePromotionNewSalaryStructures = new HashSet<EmployeePromotion>();
            EmployeePromotionOldSalaryStructures = new HashSet<EmployeePromotion>();
            EmployeePromotionSalaryStructures = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructureSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructureComponentMaps = new HashSet<SalaryStructureComponentMap>();
            EmployeeSalaryStructureLeaveSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructureScaleMaps = new HashSet<SalaryStructureScaleMap>();
        }

        [Key]
        public long SalaryStructureID { get; set; }
        [StringLength(500)]
        public string StructureName { get; set; }
        public bool? IsActive { get; set; }
        public byte? PayrollFrequencyID { get; set; }
        public bool? IsSalaryBasedOnTimeSheet { get; set; }
        public int? TimeSheetSalaryComponentID { get; set; }
        public decimal? TimeSheetHourRate { get; set; }
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }
        public decimal? TimeSheetMaximumBenefits { get; set; }
        public int? PaymentModeID { get; set; }
        public long? AccountID { get; set; }
        public string StructureCode { get; set; }

        public virtual Account Account { get; set; }
        public virtual SalaryPaymentMode PaymentMode { get; set; }
        public virtual PayrollFrequency PayrollFrequency { get; set; }
        public virtual SalaryComponent TimeSheetSalaryComponent { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewSalaryStructures { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldSalaryStructures { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionSalaryStructures { get; set; }
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructureSalaryStructures { get; set; }
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructureLeaveSalaryStructures { get; set; }
        public virtual ICollection<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
        public virtual ICollection<SalaryStructureScaleMap> SalaryStructureScaleMaps { get; set; }
    }
}
