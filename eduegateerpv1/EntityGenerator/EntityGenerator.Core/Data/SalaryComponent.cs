using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryComponents", Schema = "payroll")]
    public partial class SalaryComponent
    {
        public SalaryComponent()
        {
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
            EmployeeSalaryStructureComponentMaps = new HashSet<EmployeeSalaryStructureComponentMap>();
            EmployeeSalaryStructureLeaveSalaryMaps = new HashSet<EmployeeSalaryStructureLeaveSalaryMap>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryComponentRelationMapRelatedComponents = new HashSet<SalaryComponentRelationMap>();
            SalaryComponentRelationMapSalaryComponents = new HashSet<SalaryComponentRelationMap>();
            SalarySlips = new HashSet<SalarySlip>();
            SalaryStructureComponentMaps = new HashSet<SalaryStructureComponentMap>();
            SalaryStructures = new HashSet<SalaryStructure>();
        }

        [Key]
        public int SalaryComponentID { get; set; }
        public byte? ComponentTypeID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Abbreviation { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SalaryComponentGroupID { get; set; }
        public int? ReportHeadGroupID { get; set; }
        public int? NoOfDaysApplicable { get; set; }

        [ForeignKey("ComponentTypeID")]
        [InverseProperty("SalaryComponents")]
        public virtual SalaryComponentType ComponentType { get; set; }
        [ForeignKey("ReportHeadGroupID")]
        [InverseProperty("SalaryComponents")]
        public virtual ReportHeadGroup ReportHeadGroup { get; set; }
        [ForeignKey("SalaryComponentGroupID")]
        [InverseProperty("SalaryComponents")]
        public virtual SalaryComponentGroup SalaryComponentGroup { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
        [InverseProperty("TimeSheetSalaryComponent")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeSalaryStructureLeaveSalaryMap> EmployeeSalaryStructureLeaveSalaryMaps { get; set; }
        [InverseProperty("TimeSheetSalaryComponent")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        [InverseProperty("RelatedComponent")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMapRelatedComponents { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMapSalaryComponents { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
        [InverseProperty("TimeSheetSalaryComponent")]
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }
    }
}
