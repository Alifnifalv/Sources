using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PassageTypes", Schema = "payroll")]
    public partial class PassageType
    {
        public PassageType()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int PassageTypeID { get; set; }
        [Column("PassageType")]
        [StringLength(50)]
        public string PassageType1 { get; set; }

        [InverseProperty("PassageType")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
