using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeAccountMaps", Schema = "payroll")]
    public partial class EmployeeAccountMap
    {
        [Key]
        public long EmployeeAccountMapIID { get; set; }
        public long? EmployeeID { get; set; }
        public long? AccountID { get; set; }
    }
}
