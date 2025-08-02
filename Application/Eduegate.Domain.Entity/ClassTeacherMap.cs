namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassTeacherMaps")]
    public partial class ClassTeacherMap
    {
        [Key]
        public long ClassTeacherMapIID { get; set; }

        public int? ClassID { get; set; }

        public long? ClassTeacherID { get; set; }

        public long? TeacherID { get; set; }

        public int? SectionID { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? ClassClassTeacherMapID { get; set; }

        public int? SubjectID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual ClassClassTeacherMap ClassClassTeacherMap { get; set; }

        public virtual Class Class { get; set; }

        public virtual ClassTeacherMap ClassTeacherMaps1 { get; set; }

        public virtual ClassTeacherMap ClassTeacherMap1 { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
