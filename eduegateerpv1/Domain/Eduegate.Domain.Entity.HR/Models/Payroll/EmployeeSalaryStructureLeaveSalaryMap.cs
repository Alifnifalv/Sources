namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeSalaryStructureLeaveSalaryMaps", Schema = "payroll")]
    public partial class EmployeeSalaryStructureLeaveSalaryMap
    {
        [Key]
        public long EmployeeSalaryStructureLeaveSalaryMapIID { get; set; }

        public long? EmployeeSalaryStructureID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        public string Formula { get; set; }

        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}