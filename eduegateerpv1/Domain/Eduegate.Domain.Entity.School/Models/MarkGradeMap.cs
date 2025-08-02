using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("MarkGradeMaps", Schema = "schools")]
    public partial class MarkGradeMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public decimal? GradeFrom { get; set; }

        public decimal? GradeTo { get; set; }

        public bool? IsPercentage { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        public virtual MarkGrade MarkGrade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }
    }
}
