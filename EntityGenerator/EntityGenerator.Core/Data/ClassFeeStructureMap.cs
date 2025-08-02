using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassFeeStructureMaps", Schema = "schools")]
    public partial class ClassFeeStructureMap
    {
        [Key]
        public long ClassFeeStructureMapIID { get; set; }
        public int? ClassID { get; set; }
        public long? FeeStructureID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool IsActive { get; set; }
        public int? AcadamicYearID { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("ClassFeeStructureMaps")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassFeeStructureMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("FeeStructureID")]
        [InverseProperty("ClassFeeStructureMaps")]
        public virtual FeeStructure FeeStructure { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassFeeStructureMaps")]
        public virtual School School { get; set; }
    }
}
