using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeRelationTypes", Schema = "payroll")]
    public partial class EmployeeRelationType
    {
        public EmployeeRelationType()
        {
            EmployeeRelationsDetails = new HashSet<EmployeeRelationsDetail>();
        }

        [Key]
        public byte EmployeeRelationTypeID { get; set; }
        [StringLength(50)]
        public string EmployeeRelationTypeName { get; set; }

        [InverseProperty("EmployeeRelationType")]
        public virtual ICollection<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }
    }
}
