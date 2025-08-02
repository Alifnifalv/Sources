namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.MarkRegisterSkills")]
    public partial class MarkRegisterSkill
    {
        [Key]
        public long MarkRegisterSkillIID { get; set; }

        public long MarkRegisterSkillGroupID { get; set; }

        public int SkillMasterID { get; set; }

        public decimal? MarksObtained { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? MarksGradeMapID { get; set; }

        public int? SkillGroupMasterID { get; set; }

        public decimal? MinimumMark { get; set; }

        public decimal? MaximumMark { get; set; }

        public bool? IsPassed { get; set; }

        public bool? IsAbsent { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public virtual MarkEntryStatus MarkEntryStatus { get; set; }

        public virtual MarkGradeMap MarkGradeMap { get; set; }

        public virtual MarkRegisterSkillGroup MarkRegisterSkillGroup { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }

        public virtual SkillMaster SkillMaster { get; set; }
    }
}
