using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolDateSettingMaps", Schema = "schools")]
    public partial class SchoolDateSettingMap
    {
        [Key]
        public long SchoolDateSettingMapsIID { get; set; }
        public long? SchoolDateSettingID { get; set; }
        public int? TotalWorkingDays { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public byte? MonthID { get; set; }
        public int? YearID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PayrollCutoffDate { get; set; }
        public int? TotalHoursInMonth { get; set; }

        [ForeignKey("SchoolDateSettingID")]
        [InverseProperty("SchoolDateSettingMaps")]
        public virtual SchoolDateSetting SchoolDateSetting { get; set; }
    }
}
