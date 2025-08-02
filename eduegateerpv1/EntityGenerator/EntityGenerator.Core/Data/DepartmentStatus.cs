using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DepartmentStatuses", Schema = "mutual")]
    public partial class DepartmentStatus
    {
        [Key]
        public byte DepartmentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
