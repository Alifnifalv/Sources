using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentSkillGroupMaps", Schema = "schools")]
    public partial class StudentSkillGroupMap
    {
        public StudentSkillGroupMap()
        {
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
        }

        [Key]
        public long StudentSkillGroupMapsIID { get; set; }
        public long StudentSkillRegisterID { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MinimumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MaximumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MarkObtained { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? SkillGroupMasterID { get; set; }

        [ForeignKey("MarksGradeMapID")]
        [InverseProperty("StudentSkillGroupMaps")]
        public virtual MarkGradeMap MarksGradeMap { get; set; }
        [ForeignKey("SkillGroupMasterID")]
        [InverseProperty("StudentSkillGroupMaps")]
        public virtual SkillGroupMaster SkillGroupMaster { get; set; }
        [ForeignKey("StudentSkillRegisterID")]
        [InverseProperty("StudentSkillGroupMaps")]
        public virtual StudentSkillRegister StudentSkillRegister { get; set; }
        [InverseProperty("StudentSkillGroupMaps")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
    }
}
