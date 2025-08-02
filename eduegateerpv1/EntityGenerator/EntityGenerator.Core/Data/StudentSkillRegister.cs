using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentSkillRegisters", Schema = "schools")]
    public partial class StudentSkillRegister
    {
        public StudentSkillRegister()
        {
            StudentSkillGroupMaps = new HashSet<StudentSkillGroupMap>();
        }

        [Key]
        public long StudentSkillRegisterIID { get; set; }
        public long? ExamID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentId { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        public bool? IsAbsent { get; set; }
        public long? MarksGradeMapID { get; set; }
        public bool? IsPassed { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MarkObtained { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("StudentSkillRegisters")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamID")]
        [InverseProperty("StudentSkillRegisters")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("MarksGradeMapID")]
        [InverseProperty("StudentSkillRegisters")]
        public virtual MarkGradeMap MarksGradeMap { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentSkillRegisters")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("StudentSkillRegisters")]
        public virtual Student Student { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("StudentSkillRegisters")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("StudentSkillRegister")]
        public virtual ICollection<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }
    }
}
