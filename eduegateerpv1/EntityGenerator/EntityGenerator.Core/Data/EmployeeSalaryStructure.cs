using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSalaryStructures", Schema = "payroll")]
    public partial class EmployeeSalaryStructure
    {
        public EmployeeSalaryStructure()
        {
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
            EmployeeSalaryStructureComponentMaps = new HashSet<EmployeeSalaryStructureComponentMap>();
            EmployeeSalaryStructureLeaveSalaryMaps = new HashSet<EmployeeSalaryStructureLeaveSalaryMap>();
        }

        [Key]
        public long EmployeeSalaryStructureIID { get; set; }
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
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsActive { get; set; }
        public long? LeaveSalaryStructureID { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("EmployeeSalaryStructures")]
        public virtual Account Account { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeSalaryStructures")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LeaveSalaryStructureID")]
        [InverseProperty("EmployeeSalaryStructureLeaveSalaryStructures")]
        public virtual SalaryStructure LeaveSalaryStructure { get; set; }
        [ForeignKey("PaymentModeID")]
        [InverseProperty("EmployeeSalaryStructures")]
        public virtual SalaryPaymentMode PaymentMode { get; set; }
        [ForeignKey("PayrollFrequencyID")]
        [InverseProperty("EmployeeSalaryStructures")]
        public virtual PayrollFrequency PayrollFrequency { get; set; }
        [ForeignKey("SalaryStructureID")]
        [InverseProperty("EmployeeSalaryStructureSalaryStructures")]
        public virtual SalaryStructure SalaryStructure { get; set; }
        [ForeignKey("TimeSheetSalaryComponentID")]
        [InverseProperty("EmployeeSalaryStructures")]
        public virtual SalaryComponent TimeSheetSalaryComponent { get; set; }
        [InverseProperty("EmployeeSalaryStructure")]
        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
        [InverseProperty("EmployeeSalaryStructure")]
        public virtual ICollection<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }
        [InverseProperty("EmployeeSalaryStructure")]
        public virtual ICollection<EmployeeSalaryStructureLeaveSalaryMap> EmployeeSalaryStructureLeaveSalaryMaps { get; set; }
    }
}
