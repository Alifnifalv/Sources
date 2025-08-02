using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Catogories", Schema = "mutual")]
    public partial class Catogory
    {
        public Catogory()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public byte CategoryID { get; set; }
        [StringLength(250)]
        public string CategoryName { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
