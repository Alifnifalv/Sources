using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassSectionMaps", Schema = "schools")]
    public partial class ClassSectionMap
    {
        [Key]
        public long ClassSectionMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? MinimumCapacity { get; set; }
        public int? MaximumCapacity { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassSectionMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassSectionMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassSectionMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassSectionMaps")]
        public virtual Section Section { get; set; }
    }
}
