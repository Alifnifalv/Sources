using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SkillGroupMasters", Schema = "schools")]
    public partial class SkillGroupMaster
    {
        public SkillGroupMaster()
        {
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            ExamSkillMaps = new HashSet<ExamSkillMap>();
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
            SkillMasters = new HashSet<SkillMaster>();
            StudentSkillGroupMaps = new HashSet<StudentSkillGroupMap>();
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
        }

        [Key]
        public int SkillGroupMasterID { get; set; }
        public string SkillGroup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }
        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<ExamSkillMap> ExamSkillMaps { get; set; }
        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }
        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<SkillMaster> SkillMasters { get; set; }
        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }
        [InverseProperty("SkillGroupMaster")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
    }
}
