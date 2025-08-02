using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("TimeTableExtWeekDay", Schema = "schools")]
    public partial class TimeTableExtWeekDay
    {
        [Key]
        public long TimeTableExtWeekDayIID { get; set; }

        public long TimeTableExtID { get; set; }

        public int? WeekDayID { get; set; }

        public int? LogicalOperatorID { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual LogicalOperator LogicalOperator { get; set; }

        public virtual TimeTableExtension TimeTableExt { get; set; }

        public virtual WeekDay WeekDay { get; set; }
    }
}
