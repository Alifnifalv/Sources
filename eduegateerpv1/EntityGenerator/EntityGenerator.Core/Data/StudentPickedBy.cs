using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPickedBy", Schema = "schools")]
    public partial class StudentPickedBy
    {
        public StudentPickedBy()
        {
            StudentPickers = new HashSet<StudentPicker>();
        }

        [Key]
        public byte StudentPickedByID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("PickedBy")]
        public virtual ICollection<StudentPicker> StudentPickers { get; set; }
    }
}
