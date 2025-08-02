namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentSkillRegisters")]
    public partial class StudentSkillRegister
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentSkillRegister()
        {
            StudentSkillGroupMaps = new HashSet<StudentSkillGroupMap>();
        }

        [Key]
        public long StudentSkillRegisterIID { get; set; }

        public long? ExamID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StudentId { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectID { get; set; }

        public bool? IsAbsent { get; set; }

        public long? MarksGradeMapID { get; set; }

        public bool? IsPassed { get; set; }

        public decimal? MarkObtained { get; set; }

        public virtual Class Class { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual MarkGradeMap MarkGradeMap { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
