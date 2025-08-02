using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeLeavingTypes", Schema = "payroll")]
    public partial class EmployeeLeavingType
    {
        public EmployeeLeavingType()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public byte LeavingTypeID { get; set; }
        [StringLength(50)]
        public string LeavingTypeName { get; set; }

        [InverseProperty("LeavingType")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
