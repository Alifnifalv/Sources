namespace Eduegate.Domain.Entity.School.Models
{
    using System;
using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("StudentPromotionLogs", Schema = "schools")]
    public partial class StudentPromotionLog
    {
        [Key]
        public long StudentPromotionLogIID { get; set; }

        public string Remarks { get; set; }

        public int AcademicYearID { get; set; }

        public int ShiftFromAcademicYearID { get; set; }

        public long StudentID { get; set; }

        public bool Status { get; set; }

        public int ShiftFromClassID { get; set; }

        public int ShiftFromSectionID { get; set; }

        public int ClassID { get; set; }

        public int SectionID { get; set; }

        public byte? SchoolID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte? ShiftFromSchoolID { get; set; }
        public bool? IsPromoted { get; set; }
        public byte? PromotionStatusID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AcademicYear AcademicYear1 { get; set; }

        public virtual Class Class { get; set; }

        public virtual Class Class1 { get; set; }

        public virtual Schools School { get; set; }

        public virtual Schools School1 { get; set; }

        public virtual Section Section { get; set; }

        public virtual Section Section1 { get; set; }

        public virtual Student Student { get; set; }
    }
}
