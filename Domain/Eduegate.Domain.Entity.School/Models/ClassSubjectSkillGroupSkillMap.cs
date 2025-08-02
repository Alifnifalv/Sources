using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ClassSubjectSkillGroupSkillMaps", Schema = "schools")]
    public partial class ClassSubjectSkillGroupSkillMap
    {
        [Key]
        public long ClassSubjectSkillGroupSkillMapID { get; set; }

        public long ClassSubjectSkillGroupMapID { get; set; }

        public int? SkillMasterID { get; set; }

        public int MarkGradeID { get; set; }

        public decimal? MinimumMarks { get; set; }

        public decimal? MaximumMarks { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public bool? IsEnableInput { get; set; }

        public int? SkillGroupMasterID { get; set; }

        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }

        public virtual MarkGrade MarkGrade { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }

        public virtual SkillMaster SkillMaster { get; set; }
    }
}
