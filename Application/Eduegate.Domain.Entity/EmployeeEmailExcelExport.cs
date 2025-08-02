namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.EmployeeEmailExcelExport")]
    public partial class EmployeeEmailExcelExport
    {
        [Key]
        public long EmployeeEmailDataIID { get; set; }

        [StringLength(100)]
        public string EmployeeCode { get; set; }

        [StringLength(100)]
        public string EmployeeNumber { get; set; }

        [StringLength(100)]
        public string EmployeeName { get; set; }

        [StringLength(100)]
        public string WorkEmail { get; set; }
    }
}
