namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SkillGroupSubjectMaps")]
    public partial class SkillGroupSubjectMap
    {
        [Key]
        public long SkillGroupSubjectMapIID { get; set; }

        public long ClassSubjectSkillGroupMapID { get; set; }

        public int? SubjectID { get; set; }

        public int? MarkGradeID { get; set; }

        public decimal? MinimumMarks { get; set; }

        public decimal? MaximumMarks { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }

        public virtual MarkGrade MarkGrade { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
