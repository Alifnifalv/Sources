using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeLevels", Schema = "payroll")]
    public partial class EmployeeLevel
    {
        [Key]
        public int EmployeeLevelID { get; set; }
        [StringLength(50)]
        public string LevelName { get; set; }
    }
}
