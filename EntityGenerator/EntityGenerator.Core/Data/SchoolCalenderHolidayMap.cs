using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolCalenderHolidayMaps", Schema = "schools")]
    public partial class SchoolCalenderHolidayMap
    {
        [Key]
        public long SchoolCalenderHolidayMapIID { get; set; }
        [StringLength(50)]
        public string CalenderName { get; set; }
        public long? HolidayID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateDurationFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateDurationTo { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("HolidayID")]
        [InverseProperty("SchoolCalenderHolidayMaps")]
        public virtual Holiday Holiday { get; set; }
    }
}
