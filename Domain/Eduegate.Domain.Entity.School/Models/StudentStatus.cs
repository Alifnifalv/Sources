namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentStatuses", Schema = "schools")]
    public partial class StudentStatus
    {
        [Key]
        public byte StudentStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
