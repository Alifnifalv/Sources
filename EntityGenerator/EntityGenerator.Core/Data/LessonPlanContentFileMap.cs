using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlanContentFileMaps", Schema = "schools")]
    public partial class LessonPlanContentFileMap
    {
        [Key]
        public long LessonPlanContentFileMapID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        public long? ContentID { get; set; }
        public byte[] JsonData { get; set; }
    }
}
