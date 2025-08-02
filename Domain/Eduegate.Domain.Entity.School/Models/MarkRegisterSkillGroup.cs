namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MarkRegisterSkillGroups", Schema = "schools")]
    public partial class MarkRegisterSkillGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarkRegisterSkillGroup()
        {
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
        }

        [Key]
        public long MarkRegisterSkillGroupIID { get; set; }

        public long? MarkRegisterSubjectMapID { get; set; }

        public decimal? MinimumMark { get; set; }

        public decimal? MaximumMark { get; set; }

        public decimal? MarkObtained { get; set; }

        public long? MarksGradeMapID { get; set; }

        public int? SkillGroupMasterID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public bool? IsPassed { get; set; }

        public bool? IsAbsent { get; set; }

        public long? MarkRegisterID { get; set; }

        public long? ClassSubjectSkillGroupMapID { get; set; }

        public int? SubjectID { get; set; }

        public virtual ClassSubjectSkillGroupMap ClassSubjectSkillGroupMap { get; set; }

        public virtual MarkGradeMap MarkGradeMap { get; set; }

        public virtual MarkRegister MarkRegister { get; set; }

        public virtual MarkRegisterSubjectMap MarkRegisterSubjectMap { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
