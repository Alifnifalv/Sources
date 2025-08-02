namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SchoolDateSettingMaps")]
    public partial class SchoolDateSettingMap
    {
        [Key]
        public long SchoolDateSettingMapsIID { get; set; }

        public long? SchoolDateSettingID { get; set; }

        public int? TotalWorkingDays { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public byte? MonthID { get; set; }

        public int? YearID { get; set; }

        public DateTime? PayrollCutoffDate { get; set; }

        public int? TotalHoursInMonth { get; set; }

        public virtual SchoolDateSetting SchoolDateSetting { get; set; }
    }
}
