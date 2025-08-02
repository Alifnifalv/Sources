using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ClassSubjectSkillGroupMapID")]
        [InverseProperty("ExamSkillMaps")]
        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }
        [ForeignKey("ExamID")]
        [InverseProperty("ExamSkillMaps")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("ExamSkillMaps")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
    }
}
