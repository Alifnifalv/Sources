using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MARKS_DATA_RPT_2021
    {
        public int? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? StudentID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "money")]
        public decimal? NoteBook { get; set; }
        [Column(TypeName = "money")]
        public decimal? SEA { get; set; }
        [Column(TypeName = "money")]
        public decimal? PT { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MarkGrade { get; set; }
    }
}
