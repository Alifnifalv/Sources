using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{

    [Table("PassageTypes", Schema = "payroll")]
    public partial class PassageType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PassageType()
        {
            Employees = new HashSet<Employee>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PassageTypeID { get; set; }

        [Column("PassageType")]
        [StringLength(50)]
        public string PassageType1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}