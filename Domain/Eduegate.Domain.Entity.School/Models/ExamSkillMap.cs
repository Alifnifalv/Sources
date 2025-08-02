using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ExamSkillMaps", Schema = "schools")]
    public partial class ExamSkillMap
    {
        [Key]
        public long ExamSkillMapIID { get; set; }

        public long? ExamID { get; set; }

        public int? SkillGroupMasterID { get; set; }

        public long? ClassSubjectSkillGroupMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
    }
}