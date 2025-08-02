namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.MarkRegisters")]
    public partial class MarkRegister
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarkRegister()
        {
           // MarkRegisterSubjectMaps = new HashSet<MarkRegisterSubjectMap>();
        }

        [Key]
        public long MarkRegisterIID { get; set; }

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

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? MarkEntryStatusID { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        //public virtual Class Class { get; set; }

        //public virtual Exam Exam { get; set; }

        public virtual MarkEntryStatus MarkEntryStatus { get; set; }

        //public virtual School School { get; set; }

        //public virtual Section Section { get; set; }

        //public virtual Student Student { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
    }
}
