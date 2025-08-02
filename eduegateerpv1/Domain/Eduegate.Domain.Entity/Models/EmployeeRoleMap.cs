using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmployeeRoleMaps", Schema = "payroll")]
    public partial class EmployeeRoleMap
    {
        [Key]
        public long EmployeeRoleMapIID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<int> EmployeeRoleID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public virtual EmployeeRole EmployeeRole { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
