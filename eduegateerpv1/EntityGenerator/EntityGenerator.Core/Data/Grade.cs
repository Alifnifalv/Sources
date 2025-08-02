using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Grades", Schema = "schools")]
    public partial class Grade
    {
        public Grade()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public byte GradeID { get; set; }
        [StringLength(50)]
        public string GradeName { get; set; }

        [InverseProperty("Grade")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
