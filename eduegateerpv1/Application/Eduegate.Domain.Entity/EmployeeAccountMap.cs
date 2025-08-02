namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeAccountMaps")]
    public partial class EmployeeAccountMap
    {
        [Key]
        public long EmployeeAccountMapIID { get; set; }

        public long? EmployeeID { get; set; }

        public long? AccountID { get; set; }
    }
}
