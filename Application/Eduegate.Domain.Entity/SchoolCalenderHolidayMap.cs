namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SchoolCalenderHolidayMaps")]
    public partial class SchoolCalenderHolidayMap
    {
        [Key]
        public long SchoolCalenderHolidayMapIID { get; set; }

        [StringLength(50)]
        public string CalenderName { get; set; }

        public long? HolidayID { get; set; }

        public DateTime? DateDurationFrom { get; set; }

        public DateTime? DateDurationTo { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Holiday Holiday { get; set; }
    }
}
