using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("CurrentYearDuplicateExamMarkBKP17122024", Schema = "schools")]
    public partial class CurrentYearDuplicateExamMarkBKP17122024
    {
        public long? StudentId { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? ExamGroupID { get; set; }
        public int? SubjectID { get; set; }
        public byte? MarkEntryStatusID { get; set; }
        public long? MarksGradeMapID { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public long? MaxID { get; set; }
        [StringLength(4000)]
        public string IIDs { get; set; }
        public int? DuplicateCounts { get; set; }
    }
}
