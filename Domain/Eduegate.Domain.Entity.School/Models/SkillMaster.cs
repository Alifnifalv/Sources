using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("SkillMasters", Schema = "schools")]
    public partial class SkillMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SkillMaster()
        {
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
        }
        [Key]
        public int SkillMasterID { get; set; }

        [Required]
        public string SkillName { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? SkillGroupMasterID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
    }
}