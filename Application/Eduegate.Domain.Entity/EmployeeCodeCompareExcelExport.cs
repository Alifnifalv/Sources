namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.EmployeeCodeCompareExcelExport")]
    public partial class EmployeeCodeCompareExcelExport
    {
        [Key]
        public long CompareDataIID { get; set; }

        [StringLength(30)]
        public string EmployeeCode { get; set; }

        [StringLength(30)]
        public string OldCode { get; set; }

        [StringLength(100)]
        public string EmployeeName { get; set; }

        [StringLength(20)]
        public string EmployeeStatus { get; set; }
    }
}
