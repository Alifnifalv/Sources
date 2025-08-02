using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TimeTableExtensionView
    {
        public long TimeTableExtIID { get; set; }
        [StringLength(500)]
        public string TimeTableExtName { get; set; }
        [StringLength(1000)]
        public string TimeTableExtRemarks { get; set; }
        public int TimeTableID { get; set; }
        public byte? SubjectTypeID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? TimeTableExtParentID { get; set; }
        public int? MinPeriodCountDay { get; set; }
        public int? MaxPeriodCountDay { get; set; }
        public int? PeriodCountWeek { get; set; }
        public bool? IsPeriodContinues { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
