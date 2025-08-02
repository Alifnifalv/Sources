using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("AgeCriteria", Schema = "schools")]
    public partial class AgeCriteria
    {
        [Key]
        public long AgeCriteriaIID { get; set; }

        public int? ClassID { get; set; }

        public byte? CurriculumID { get; set; }

        public decimal? MinAge { get; set; }

        public decimal? MaxAge { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? BirthFrom { get; set; }

        public DateTime? BirthTo { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? IsActive { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Syllabu Syllabu { get; set; }
    }
}
