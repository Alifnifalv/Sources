using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DepartmentStatuses", Schema = "mutual")]
    public partial class DepartmentStatus
    {
        public DepartmentStatus()
        {
            this.Departments = new List<Department>();
        }

        [Key]
        public byte DepartmentStatusID { get; set; } 
        public string StatusName { get; set; }
        public virtual ICollection<Department> Departments { get; set; } 
    }
}
