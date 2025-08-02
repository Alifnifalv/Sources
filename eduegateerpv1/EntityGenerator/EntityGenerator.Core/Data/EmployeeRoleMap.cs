using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeRoleMaps", Schema = "payroll")]
    public partial class EmployeeRoleMap
    {
        [Key]
        public long EmployeeRoleMapIID { get; set; }
        public long? EmployeeID { get; set; }
        public int? EmployeeRoleID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeRoleMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EmployeeRoleID")]
        [InverseProperty("EmployeeRoleMaps")]
        public virtual EmployeeRole EmployeeRole { get; set; }
    }
}
