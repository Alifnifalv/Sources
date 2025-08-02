using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Departments", Schema = "hr")]
    public partial class Department
    {
        public Department()
        {
            //DepartmentTags = new HashSet<DepartmentTag>();
        }

        [Key]
        public int DepartmentID { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [StringLength(500)]
        public string Logo { get; set; }

        //[InverseProperty("Department")]
        //public virtual ICollection<DepartmentTag> DepartmentTags { get; set; }
    }
}
