using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class JobDepartment
    {
        public int DepartmentID { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
    }
}
