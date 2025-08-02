using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [StringLength(250)]
        public string LogicalOperatorName { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("LogicalOperator")]
        public virtual ICollection<TimeTableExtClassTiming> TimeTableExtClassTimings { get; set; }
        [InverseProperty("LogicalOperator")]
        public virtual ICollection<TimeTableExtWeekDay> TimeTableExtWeekDays { get; set; }
    }
}
