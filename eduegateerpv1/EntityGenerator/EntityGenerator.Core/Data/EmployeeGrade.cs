using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeGrades", Schema = "payroll")]
    public partial class EmployeeGrade
    {
        [Key]
        public int EmployeeGradeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string GradeName { get; set; }
    }
}
