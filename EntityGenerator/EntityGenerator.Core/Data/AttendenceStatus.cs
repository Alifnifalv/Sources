using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AttendenceStatuses", Schema = "payroll")]
    public partial class AttendenceStatus
    {
        public AttendenceStatus()
        {
            Attendences = new HashSet<Attendence>();
        }

        [Key]
        public byte AttendenceStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("AttendenceStatus")]
        public virtual ICollection<Attendence> Attendences { get; set; }
    }
}
