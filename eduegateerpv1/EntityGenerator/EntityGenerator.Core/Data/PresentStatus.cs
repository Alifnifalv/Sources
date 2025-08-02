using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PresentStatuses", Schema = "schools")]
    public partial class PresentStatus
    {
        public PresentStatus()
        {
            MarkRegisters = new HashSet<MarkRegister>();
            StudentAttendences = new HashSet<StudentAttendence>();
        }

        [Key]
        public byte PresentStatusID { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
        [StringLength(10)]
        public string StatusTitle { get; set; }

        [InverseProperty("PresentStatus")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("PresentStatus")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
    }
}
