using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("MaritalStatuses", Schema = "mutual")]
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            this.Employees = new List<Employee>();
        }

        [Key]
        public int MaritalStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
