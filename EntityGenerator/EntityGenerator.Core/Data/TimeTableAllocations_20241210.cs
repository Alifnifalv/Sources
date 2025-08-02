using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TimeTableAllocations_20241210
    {
        public long TimeTableAllocationIID { get; set; }
        public int? TimeTableID { get; set; }
        public int? WeekDayID { get; set; }
        public int? ClassTimingID { get; set; }
        public int? SubjectID { get; set; }
        public long? StaffID { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? SectionID { get; set; }
        public int? ClassId { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
    }
}
