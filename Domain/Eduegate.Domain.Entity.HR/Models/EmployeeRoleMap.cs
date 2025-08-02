namespace Eduegate.Domain.Entity.HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeRoleMaps", Schema = "payroll")]
    public partial class EmployeeRoleMap
    {
        [Key]
        public long EmployeeRoleMapIID { get; set; }

        public long? EmployeeID { get; set; }

        public int? EmployeeRoleID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
