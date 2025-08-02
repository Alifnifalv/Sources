using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmployeeRelationTypes", Schema = "payroll")]
    public partial class EmployeeRelationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeRelationType()
        {
            EmployeeRelationsDetails = new HashSet<EmployeeRelationsDetail>();
        }

        public byte EmployeeRelationTypeID { get; set; }

        [StringLength(50)]
        public string EmployeeRelationTypeName { get; set; }

        public virtual ICollection<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }
    }
}
