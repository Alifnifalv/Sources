using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LogicalOperatorID")]
        [InverseProperty("TimeTableExtWeekDays")]
        public virtual LogicalOperator LogicalOperator { get; set; }
        [ForeignKey("TimeTableExtID")]
        [InverseProperty("TimeTableExtWeekDays")]
        public virtual TimeTableExtension TimeTableExt { get; set; }
        [ForeignKey("WeekDayID")]
        [InverseProperty("TimeTableExtWeekDays")]
        public virtual WeekDay WeekDay { get; set; }
    }
}
