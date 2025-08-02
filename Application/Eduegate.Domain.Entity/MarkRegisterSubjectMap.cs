namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.MarkRegisterSubjectMaps")]
    public partial class MarkRegisterSubjectMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarkRegisterSubjectMap()
        {
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
        }

        [Key]
        public long MarkRegisterSubjectMapIID { get; set; }

        public long? MarkRegisterID { get; set; }

        public int? SubjectID { get; set; }

        public decimal? Mark { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? IsAbsent { get; set; }

        public long? MarksGradeMapID { get; set; }

        public bool? IsPassed { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        public decimal? ConversionFactor { get; set; }

        public decimal? TotalMark { get; set; }

        public virtual MarkEntryStatus MarkEntryStatus { get; set; }

        public virtual MarkGradeMap MarkGradeMap { get; set; }

        public virtual MarkRegister MarkRegister { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
