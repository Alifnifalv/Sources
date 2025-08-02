namespace Eduegate.Domain.Entity
{
    using Eduegate.Domain.Entity.School.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentSkillGroupMaps", Schema = "schools")]
    public partial class StudentSkillGroupMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentSkillGroupMap()
        {
            StudentSkillMasterMaps = new HashSet<StudentSkillMasterMap>();
        }

        [Key]
        public long StudentSkillGroupMapsIID { get; set; }

        public long StudentSkillRegisterID { get; set; }

        public decimal? MinimumMark { get; set; }

        public decimal? MaximumMark { get; set; }

        public decimal? MarkObtained { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? MarksGradeMapID { get; set; }

        public int? SkillGroupMasterID { get; set; }

        public virtual MarkGradeMap MarkGradeMap { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }

        public virtual StudentSkillRegister StudentSkillRegister { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
    }
}
