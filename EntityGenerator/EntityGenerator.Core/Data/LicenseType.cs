using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LicenseTypes", Schema = "payroll")]
    public partial class LicenseType
    {
        public LicenseType()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public byte LicenseTypeID { get; set; }
        [StringLength(250)]
        public string LicenseTypeName { get; set; }

        [InverseProperty("LicenseType")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
