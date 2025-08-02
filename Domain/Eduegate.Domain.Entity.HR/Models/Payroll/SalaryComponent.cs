namespace Eduegate.Domain.Entity.HR.Payroll
{
    using Eduegate.Domain.Entity.HR.Payroll;
   // using Eduegate.Domain.Entity.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Eduegate.Domain.Entity.HR.Models;
    using EntityGenerator.Core.Data;

    [Table("SalaryComponents", Schema = "payroll")]
    public partial class SalaryComponent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryComponent()
        {            
            EmployeeDepartmentAccountMaps = new HashSet<EmployeeDepartmentAccountMap>();
            EmployeeESBProvisionHeads = new HashSet<EmployeeESBProvisionHead>();
            EmployeeLSProvisionHeads = new HashSet<EmployeeLSProvisionHead>();
            EmployeeSalaryStructureComponentMaps = new HashSet<EmployeeSalaryStructureComponentMap>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructures = new HashSet<SalaryStructure>();
            SalaryStructureComponentMaps = new HashSet<SalaryStructureComponentMap>();
            SalarySlips = new HashSet<SalarySlip>();
            SalaryComponentRelationMaps = new HashSet<SalaryComponentRelationMap>();
            SalaryComponentRelationMaps1 = new HashSet<SalaryComponentRelationMap>();
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
            SalaryComponentVariableMaps = new HashSet<SalaryComponentVariableMap>();
            EmployeeSalaryStructureVariableMaps = new HashSet<EmployeeSalaryStructureVariableMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalaryComponentID { get; set; }

        public byte? ComponentTypeID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Abbreviation { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SalaryComponentGroupID { get; set; }

        public int? ReportHeadGroupID { get; set; }

        public int? NoOfDaysApplicable { get; set; }

        public long? ProvisionLedgerAccountID { get; set; }
        public long? StaffLedgerAccountID { get; set; }
        public long? ExpenseLedgerAccountID { get; set; }

        public virtual ReportHeadGroup ReportHeadGroup { get; set; }
        public virtual SalaryComponentGroup SalaryComponentGroup { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        [ForeignKey("StaffLedgerAccountID")]
        [InverseProperty("SalaryComponentStaffLedgerAccounts")]
        public virtual Account StaffLedgerAccount { get; set; }
        [ForeignKey("ExpenseLedgerAccountID")]
        [InverseProperty("SalaryComponentExpenseLedgerAccounts")]
        public virtual Account ExpenseLedgerAccount { get; set; }
        [ForeignKey("ProvisionLedgerAccountID")]
        [InverseProperty("SalaryComponentProvisionLedgerAccounts")]
        public virtual Account ProvisionLedgerAccount { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeESBProvisionHead> EmployeeESBProvisionHeads { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeLSProvisionHead> EmployeeLSProvisionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }
        public virtual SalaryComponentType SalaryComponentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }

        public virtual ICollection<EmployeeSalaryStructureLeaveSalaryMap> EmployeeSalaryStructureLeaveSalaryMaps { get; set; }

        public virtual ICollection<SalaryComponentVariableMap> SalaryComponentVariableMaps { get; set; }
        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
        public virtual ICollection<EmployeeSalaryStructureVariableMap> EmployeeSalaryStructureVariableMaps { get; set; }
        [InverseProperty("SalaryComponent")]
        public virtual ICollection<EmployeeDepartmentAccountMap> EmployeeDepartmentAccountMaps { get; set; }

    }
}
