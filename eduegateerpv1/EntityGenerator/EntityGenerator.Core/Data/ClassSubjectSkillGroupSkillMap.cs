using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassSubjectSkillGroupSkillMaps", Schema = "schools")]
    public partial class ClassSubjectSkillGroupSkillMap
    {
        [Key]
        public long ClassSubjectSkillGroupSkillMapID { get; set; }
        public long ClassSubjectSkillGroupMapID { get; set; }
        public int? SkillMasterID { get; set; }
        public int MarkGradeID { get; set; }
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
        public bool? IsEnableInput { get; set; }
        public int? SkillGroupMasterID { get; set; }

        [ForeignKey("ClassSubjectSkillGroupMapID")]
        [InverseProperty("ClassSubjectSkillGroupSkillMaps")]
        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }
        [ForeignKey("MarkGradeID")]
        [InverseProperty("ClassSubjectSkillGroupSkillMaps")]
        public virtual MarkGrade MarkGrade { get; set; }
        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("ClassSubjectSkillGroupSkillMaps")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
        [ForeignKey("SkillMasterID")]
        [InverseProperty("ClassSubjectSkillGroupSkillMaps")]
        public virtual SkillMaster SkillMaster { get; set; }
    }
}
