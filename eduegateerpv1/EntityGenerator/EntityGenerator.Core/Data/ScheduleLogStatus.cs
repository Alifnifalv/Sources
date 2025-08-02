using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ScheduleLogStatuses", Schema = "schools")]
    public partial class ScheduleLogStatus
    {
        public ScheduleLogStatus()
        {
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
        }

        [Key]
        public int ScheduleLogStatusID { get; set; }
        [StringLength(250)]
        public string StatusNameEn { get; set; }
        [StringLength(250)]
        public string StatusNameAr { get; set; }
        [StringLength(100)]
        public string StatusTitleEn { get; set; }
        [StringLength(100)]
        public string StatusTitleAr { get; set; }

        [InverseProperty("SheduleLogStatus")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
    }
}
