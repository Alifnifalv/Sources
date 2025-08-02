using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableExtClassTimings", Schema = "schools")]
    public partial class TimeTableExtClassTiming
    {
        [Key]
        public long TimeTableExtClassTimingIID { get; set; }
        public long TimeTableExtID { get; set; }
        public int? ClassTimingID { get; set; }
        public int? LogicalOperatorID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassTimingID")]
        [InverseProperty("TimeTableExtClassTimings")]
        public virtual ClassTiming ClassTiming { get; set; }
        [ForeignKey("LogicalOperatorID")]
        [InverseProperty("TimeTableExtClassTimings")]
        public virtual LogicalOperator LogicalOperator { get; set; }
        [ForeignKey("TimeTableExtID")]
        [InverseProperty("TimeTableExtClassTimings")]
        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
