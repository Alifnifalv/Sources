namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.DepartmentStatuses")]
    public partial class DepartmentStatus
    {
        public byte DepartmentStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
