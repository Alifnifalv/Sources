namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeLevels")]
    public partial class EmployeeLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeLevelID { get; set; }

        [StringLength(50)]
        public string LevelName { get; set; }
    }
}
