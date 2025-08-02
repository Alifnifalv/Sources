using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_STUDENT_ATTENDANCE
    {
        public long? RowIndex { get; set; }
        public long StudentIID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendenceDate { get; set; }
        public int? PresentStatusID { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
    }
}
