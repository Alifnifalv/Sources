using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class UnitsView1
    {
        public long UnitIID { get; set; }
        public long? ParentSubjectUnitID { get; set; }
        public long? ChapterID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(255)]
        public string UnitTitle { get; set; }
        public string Description { get; set; }
        public long? TotalPeriods { get; set; }
        public long? TotalHours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
