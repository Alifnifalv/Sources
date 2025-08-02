namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ClassSectionMaps", Schema = "schools")]
    public partial class ClassSectionMap
    {
        [Key]
        public long ClassSectionMapIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

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

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }

        public int? MinimumCapacity { get; set; }

        public int? MaximumCapacity { get; set; }
    }
}
