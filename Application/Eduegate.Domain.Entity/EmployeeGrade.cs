namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeGrades")]
    public partial class EmployeeGrade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeGradeID { get; set; }

        [StringLength(50)]
        public string GradeName { get; set; }
    }
}
