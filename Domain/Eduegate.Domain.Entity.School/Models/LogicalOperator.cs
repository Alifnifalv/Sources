using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("LogicalOperators", Schema = "mutual")]
    public partial class LogicalOperator
    {
        public LogicalOperator()
        {
            TimeTableExtClassTimings = new HashSet<TimeTableExtClassTiming>();
            TimeTableExtWeekDays = new HashSet<TimeTableExtWeekDay>();
        }

        [Key]
        public int LogicalOperatorID { get; set; }

        public string LogicalOperatorName { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public virtual ICollection<TimeTableExtClassTiming> TimeTableExtClassTimings { get; set; }

        public virtual ICollection<TimeTableExtWeekDay> TimeTableExtWeekDays { get; set; }
    }
}
