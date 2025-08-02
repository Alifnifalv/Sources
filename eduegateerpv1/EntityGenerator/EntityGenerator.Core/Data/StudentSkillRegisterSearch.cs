using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentSkillRegisterSearch
    {
        public long StudentSkillRegisterIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? ExamID { get; set; }
        public long? StudentId { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string IsAbsent { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string IsPassed { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [Column("class")]
        [StringLength(50)]
        public string _class { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        [StringLength(100)]
        public string Exam { get; set; }
        [StringLength(50)]
        public string GradeName { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
    }
}
