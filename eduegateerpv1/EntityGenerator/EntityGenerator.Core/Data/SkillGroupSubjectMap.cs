using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SkillGroupSubjectMaps", Schema = "schools")]
    public partial class SkillGroupSubjectMap
    {
        [Key]
        public long SkillGroupSubjectMapIID { get; set; }
        public long ClassSubjectSkillGroupMapID { get; set; }
        public int? SubjectID { get; set; }
        public int? MarkGradeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumMarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassSubjectSkillGroupMapID")]
        [InverseProperty("SkillGroupSubjectMaps")]
        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }
        [ForeignKey("MarkGradeID")]
        [InverseProperty("SkillGroupSubjectMaps")]
        public virtual MarkGrade MarkGrade { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("SkillGroupSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
