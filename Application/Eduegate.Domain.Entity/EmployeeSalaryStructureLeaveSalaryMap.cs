namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeSalaryStructureLeaveSalaryMaps")]
    public partial class EmployeeSalaryStructureLeaveSalaryMap
    {
        [Key]
        public long EmployeeSalaryStructureLeaveSalaryMapIID { get; set; }

        public long? EmployeeSalaryStructureID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string Formula { get; set; }

        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}
