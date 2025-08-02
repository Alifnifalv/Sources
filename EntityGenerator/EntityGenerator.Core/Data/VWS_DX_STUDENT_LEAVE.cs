using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_STUDENT_LEAVE
    {
        [Column(TypeName = "datetime")]
        public DateTime? NotificationDate { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }
        public long StudentIID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
    }
}
