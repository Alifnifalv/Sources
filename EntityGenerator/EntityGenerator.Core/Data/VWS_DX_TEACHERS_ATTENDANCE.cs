using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_TEACHERS_ATTENDANCE
    {
        public byte? SchoolID { get; set; }
        public long? RowIndex { get; set; }
        public long? TeacherID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendenceDate { get; set; }
        public byte? PresentStatusID { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
    }
}
