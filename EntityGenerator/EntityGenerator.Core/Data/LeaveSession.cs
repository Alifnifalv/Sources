using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveSessions", Schema = "payroll")]
    public partial class LeaveSession
    {
        public LeaveSession()
        {
            LeaveApplicationFromSessions = new HashSet<LeaveApplication>();
            LeaveApplicationToSessions = new HashSet<LeaveApplication>();
        }

        [Key]
        public byte LeaveSessionID { get; set; }
        [StringLength(50)]
        public string SesionName { get; set; }

        [InverseProperty("FromSession")]
        public virtual ICollection<LeaveApplication> LeaveApplicationFromSessions { get; set; }
        [InverseProperty("ToSession")]
        public virtual ICollection<LeaveApplication> LeaveApplicationToSessions { get; set; }
    }
}
