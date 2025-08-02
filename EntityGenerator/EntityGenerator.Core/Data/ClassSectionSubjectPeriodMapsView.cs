using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassSectionSubjectPeriodMapsView
    {
        public long? PeriodMapID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public int? SubjectCounts { get; set; }
    }
}
