namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PayrollFrequencies", Schema = "payroll")]
    public partial class PayrollFrequency
    {
        public PayrollFrequency()
        {
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructures = new HashSet<SalaryStructure>();
        }

        [Key]
        public byte PayrollFrequencyID { get; set; }
        [StringLength(50)]
        public string FrequencyName { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }
    }
}
