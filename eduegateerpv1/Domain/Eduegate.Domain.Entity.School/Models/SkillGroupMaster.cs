using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("SkillGroupMasters", Schema = "schools")]
    public partial class SkillGroupMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SkillGroupMaster()
        {
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
            SkillMasters = new HashSet<SkillMaster>();
            StudentSkillGroupMaps = new HashSet<StudentSkillGroupMap>();
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
            ExamSkillMaps = new HashSet<ExamSkillMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SkillGroupMasterID { get; set; }

        [Required]
        [StringLength(100)]
        public string SkillGroup { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkillMaster> SkillMasters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSkillMap> ExamSkillMaps { get; set; }
    }
}