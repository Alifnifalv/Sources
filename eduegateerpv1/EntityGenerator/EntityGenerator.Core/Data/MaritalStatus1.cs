using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MaritalStatuses", Schema = "mutual")]
    public partial class MaritalStatus1
    {
        public MaritalStatus1()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int MaritalStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("MaritalStatus")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
