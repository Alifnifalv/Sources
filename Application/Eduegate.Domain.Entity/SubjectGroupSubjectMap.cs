namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SubjectGroupSubjectMaps")]
    public partial class SubjectGroupSubjectMap
    {
        [Key]
        public long SubjectGroupSubjectMapIID { get; set; }

        public int? ClassID { get; set; }

        public int? SubjectID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public byte? SubjectGroupID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual School School { get; set; }

        public virtual SubjectGroup SubjectGroup { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
