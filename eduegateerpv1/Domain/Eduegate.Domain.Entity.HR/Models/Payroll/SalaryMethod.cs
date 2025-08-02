namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalaryMethod", Schema = "payroll")]
    public partial class SalaryMethod
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public SalaryMethod()
        //{
        //    //Employees = new HashSet<Employee>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalaryMethodID { get; set; }

        [StringLength(20)]
        public string SalaryMethodName { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Employee> Employees { get; set; }
    }
}
