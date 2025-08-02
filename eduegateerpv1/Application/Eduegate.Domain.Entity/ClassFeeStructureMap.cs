namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassFeeStructureMaps")]
    public partial class ClassFeeStructureMap
    {
        [Key]
        public long ClassFeeStructureMapIID { get; set; }

        public int? ClassID { get; set; }

        public long? FeeStructureID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool IsActive { get; set; }

        public int? AcadamicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual FeeStructure FeeStructure { get; set; }

        public virtual School School { get; set; }
    }
}
