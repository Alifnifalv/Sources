using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccomodationTypes", Schema = "payroll")]
    public partial class AccomodationType
    {
        public AccomodationType()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int AccomodationTypeID { get; set; }
        [Column("AccomodationType")]
        [StringLength(50)]
        public string AccomodationType1 { get; set; }

        [InverseProperty("AccomodationType")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
