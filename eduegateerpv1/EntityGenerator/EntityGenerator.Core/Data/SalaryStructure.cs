using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryStructure", Schema = "payroll")]
    public partial class SalaryStructure
    {
        public SalaryStructure()
        {
            EmployeePromotionNewSalaryStructures = new HashSet<EmployeePromotion>();
            EmployeePromotionOldSalaryStructures = new HashSet<EmployeePromotion>();
            EmployeePromotionSalaryStructures = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructureLeaveSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            EmployeeSalaryStructureSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructureComponentMaps = new HashSet<SalaryStructureComponentMap>();
        }

        [Key]
        public long SalaryStructureID { get; set; }
        [StringLength(500)]
        public string StructureName { get; set; }
        public bool? IsActive { get; set; }
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

        [ForeignKey("AccountID")]
        [InverseProperty("SalaryStructures")]
        public virtual Account Account { get; set; }
        [ForeignKey("PaymentModeID")]
        [InverseProperty("SalaryStructures")]
        public virtual SalaryPaymentMode PaymentMode { get; set; }
        [ForeignKey("PayrollFrequencyID")]
        [InverseProperty("SalaryStructures")]
        public virtual PayrollFrequency PayrollFrequency { get; set; }
        [ForeignKey("TimeSheetSalaryComponentID")]
        [InverseProperty("SalaryStructures")]
        public virtual SalaryComponent TimeSheetSalaryComponent { get; set; }
        [InverseProperty("NewSalaryStructure")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewSalaryStructures { get; set; }
        [InverseProperty("OldSalaryStructure")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldSalaryStructures { get; set; }
        [InverseProperty("SalaryStructure")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionSalaryStructures { get; set; }
        [InverseProperty("LeaveSalaryStructure")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructureLeaveSalaryStructures { get; set; }
        [InverseProperty("SalaryStructure")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructureSalaryStructures { get; set; }
        [InverseProperty("SalaryStructure")]
        public virtual ICollection<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
    }
}
