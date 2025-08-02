using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkRegisterSkillGroups", Schema = "schools")]
    [Index("MarkRegisterID", "ClassSubjectSkillGroupMapID", Name = "IDX_MarkRegisterSkillGroups_MarkRegisterID_ClassSubjectSkillGroupMapID")]
    [Index("SkillGroupMasterID", "ClassSubjectSkillGroupMapID", Name = "IDX_MarkRegisterSkillGroups_SkillGroupMasterID__ClassSubjectSkillGroupMapID_MarkRegisterSubjectMapI")]
    public partial class MarkRegisterSkillGroup
    {
        public MarkRegisterSkillGroup()
        {
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
        }

        [Key]
        public long MarkRegisterSkillGroupIID { get; set; }
        public long? MarkRegisterSubjectMapID { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MinimumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MaximumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MarkObtained { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? SkillGroupMasterID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsPassed { get; set; }
        public bool? IsAbsent { get; set; }
        public long? MarkRegisterID { get; set; }
        public long? ClassSubjectSkillGroupMapID { get; set; }
        public int? SubjectID { get; set; }

        [ForeignKey("ClassSubjectSkillGroupMapID")]
        [InverseProperty("MarkRegisterSkillGroups")]
        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }
        [ForeignKey("MarkRegisterID")]
        [InverseProperty("MarkRegisterSkillGroups")]
        public virtual MarkRegister MarkRegister { get; set; }
        [ForeignKey("MarkRegisterSubjectMapID")]
        [InverseProperty("MarkRegisterSkillGroups")]
        public virtual MarkRegisterSubjectMap MarkRegisterSubjectMap { get; set; }
        [ForeignKey("MarksGradeMapID")]
        [InverseProperty("MarkRegisterSkillGroups")]
        public virtual MarkGradeMap MarksGradeMap { get; set; }
        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("MarkRegisterSkillGroups")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("MarkRegisterSkillGroups")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("MarkRegisterSkillGroup")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }
    }
}
