using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SkillMasters", Schema = "schools")]
    public partial class SkillMaster
    {
        public SkillMaster()
        {
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
        }

        [Key]
        public int SkillMasterID { get; set; }
        public string SkillName { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? SkillGroupMasterID { get; set; }

        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("SkillMasters")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
        [InverseProperty("SkillMaster")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }
        [InverseProperty("SkillMaster")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }
        [InverseProperty("SkillMaster")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
    }
}
