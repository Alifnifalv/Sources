using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LessonPlanSearch
    {
        public long LessonPlanIID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string SyllabusCompleted { get; set; }
        [StringLength(63)]
        [Unicode(false)]
        public string Academic { get; set; }
        [StringLength(15)]
        public string MonthNam { get; set; }
        [StringLength(500)]
        public string Subject { get; set; }
        [StringLength(50)]
        public string LessonPlanStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(4000)]
        public string Class { get; set; }
        [StringLength(4000)]
        public string Section { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
    }
}
