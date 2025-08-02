namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ExamGroupSubjectMaps", Schema = "schools")]
    public partial class ExamGroupSubjectMap
    {
        [Key]
        public long ExamGroupSubjectMapIID { get; set; }

        public int? ExamGroupID { get; set; }

        public int? SubjectID { get; set; }

        public int? MinimumMarks { get; set; }

        public int? MaximumMarks { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool? IsNoExam { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
