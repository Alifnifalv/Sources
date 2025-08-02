using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Holidays", Schema = "payroll")]
    public partial class Holiday
    {
        public Holiday()
        {
            SchoolCalenderHolidayMaps = new HashSet<SchoolCalenderHolidayMap>();
        }

        [Key]
        public long HolidayIID { get; set; }
        public long? HolidayListID { get; set; }
        public byte? HolidayTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HolidayDate { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("HolidayListID")]
        [InverseProperty("Holidays")]
        public virtual HolidayList HolidayList { get; set; }
        [ForeignKey("HolidayTypeID")]
        [InverseProperty("Holidays")]
        public virtual HolidayType HolidayType { get; set; }
        [InverseProperty("Holiday")]
        public virtual ICollection<SchoolCalenderHolidayMap> SchoolCalenderHolidayMaps { get; set; }
    }
}
