using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentSkillMasterMaps", Schema = "schools")]
    public partial class StudentSkillMasterMap
    {
        [Key]
        public long StudentSkillMasterMapIID { get; set; }
        public long StudentSkillGroupMapsID { get; set; }
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

        [ForeignKey("MarksGradeMapID")]
        [InverseProperty("StudentSkillMasterMaps")]
        public virtual MarkGradeMap MarksGradeMap { get; set; }
        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("StudentSkillMasterMaps")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
        [ForeignKey("SkillMasterID")]
        [InverseProperty("StudentSkillMasterMaps")]
        public virtual SkillMaster SkillMaster { get; set; }
        [ForeignKey("StudentSkillGroupMapsID")]
        [InverseProperty("StudentSkillMasterMaps")]
        public virtual StudentSkillGroupMap StudentSkillGroupMaps { get; set; }
    }
}
