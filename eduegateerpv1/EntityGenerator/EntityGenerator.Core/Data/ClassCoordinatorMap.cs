using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassCoordinatorMaps", Schema = "schools")]
    public partial class ClassCoordinatorMap
    {
        [Key]
        public long ClassCoordinatorMapIID { get; set; }
        public long? CoordinatorID { get; set; }
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
        public bool? AllClass { get; set; }
        public bool? AllSection { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassCoordinatorMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassCoordinatorMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("CoordinatorID")]
        [InverseProperty("ClassCoordinatorMaps")]
        public virtual Employee Coordinator { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassCoordinatorMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ClassCoordinatorMaps")]
        public virtual Section Section { get; set; }
    }
}
