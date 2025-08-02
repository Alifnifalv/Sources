using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkRegisterSkills", Schema = "schools")]
    [Index("MarkRegisterSkillGroupID", "SkillMasterID", Name = "IDX_MarkRegisterSkills_MarkRegisterSkillGroupID_SkillMasterID")]
    [Index("MarksGradeMapID", Name = "IDX_MarkRegisterSkills_MarksGradeMapID_MarkRegisterSkillGroupID__SkillMasterID__MarksObtained__Crea")]
    public partial class MarkRegisterSkill
    {
        [Key]
        public long MarkRegisterSkillIID { get; set; }
        public long MarkRegisterSkillGroupID { get; set; }
        public int? SkillMasterID { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MarksObtained { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? SkillGroupMasterID { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MinimumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MaximumMark { get; set; }
        public bool? IsPassed { get; set; }
        public bool? IsAbsent { get; set; }
        public byte? MarkEntryStatusID { get; set; }

        [ForeignKey("MarkEntryStatusID")]
        [InverseProperty("MarkRegisterSkills")]
        public virtual MarkEntryStatus MarkEntryStatus { get; set; }
        [ForeignKey("MarkRegisterSkillGroupID")]
        [InverseProperty("MarkRegisterSkills")]
        public virtual MarkRegisterSkillGroup MarkRegisterSkillGroup { get; set; }
        [ForeignKey("MarksGradeMapID")]
        [InverseProperty("MarkRegisterSkills")]
        public virtual MarkGradeMap MarksGradeMap { get; set; }
        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("MarkRegisterSkills")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
        [ForeignKey("SkillMasterID")]
        [InverseProperty("MarkRegisterSkills")]
        public virtual SkillMaster SkillMaster { get; set; }
    }
}
