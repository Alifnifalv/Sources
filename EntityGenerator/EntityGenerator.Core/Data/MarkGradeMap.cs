using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkGradeMaps", Schema = "schools")]
    public partial class MarkGradeMap
    {
        public MarkGradeMap()
        {
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
            MarkRegisterSubjectMaps = new HashSet<MarkRegisterSubjectMap>();
            StudentSkillGroupMaps = new HashSet<StudentSkillGroupMap>();
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
        }

        [Key]
        public long MarksGradeMapIID { get; set; }
        public int? MarkGradeID { get; set; }
        [StringLength(50)]
        public string GradeName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? GradeFrom { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? GradeTo { get; set; }
        public bool? IsPercentage { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("MarkGradeMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("MarkGradeID")]
        [InverseProperty("MarkGradeMaps")]
        public virtual MarkGrade MarkGrade { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("MarkGradeMaps")]
        public virtual School School { get; set; }
        [InverseProperty("MarksGradeMap")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        [InverseProperty("MarksGradeMap")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }
        [InverseProperty("MarksGradeMap")]
        public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
        [InverseProperty("MarksGradeMap")]
        public virtual ICollection<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }
        [InverseProperty("MarksGradeMap")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
        [InverseProperty("MarksGradeMap")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
    }
}
