namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentStatuses")]
    public partial class StudentStatus
    {
        public byte StudentStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
