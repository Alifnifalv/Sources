using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("HealthEntryStudentMaps", Schema = "schools")]
    [Index("HealthEntryID", Name = "IDX_HealthEntryStudentMaps_HealthEntryID_")]
    [Index("HealthEntryID", Name = "IDX_HealthEntryStudentMaps_HealthEntryID_StudentID__Height__Weight__BMS__Vision__Remarks__BMI")]
    [Index("StudentID", Name = "IDX_HealthEntryStudentMaps_StudentID")]
    public partial class HealthEntryStudentMap
    {
        [Key]
        public long HealthEntryStudentMapIID { get; set; }
        public long? HealthEntryID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Height { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Weight { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BMS { get; set; }
        public string Vision { get; set; }
        public string Remarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BMI { get; set; }

        [ForeignKey("HealthEntryID")]
        [InverseProperty("HealthEntryStudentMaps")]
        public virtual HealthEntry HealthEntry { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("HealthEntryStudentMaps")]
        public virtual Student Student { get; set; }
    }
}
