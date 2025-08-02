using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("AccomodationTypes", Schema = "payroll")]
    public partial class AccomodationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccomodationType()
        {
            Employees = new HashSet<Employee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccomodationTypeID { get; set; }

        [Column("AccomodationType")]
        [StringLength(50)]
        public string AccomodationType1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}