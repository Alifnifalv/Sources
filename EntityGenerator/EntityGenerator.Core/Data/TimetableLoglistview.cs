using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TimetableLoglistview
    {
        public int? ClassId { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? TimeTableID { get; set; }
        [StringLength(100)]
        public string TimeTableDescription { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        public int? SubjectNos { get; set; }
        public int? StaffNos { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public byte? SchoolID { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
    }
}
