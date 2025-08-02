using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("HolidayTypes", Schema = "payroll")]
    public partial class HolidayType
    {
        public HolidayType()
        {
            Holidays = new HashSet<Holiday>();
        }

        [Key]
        public byte HolidayTypeID { get; set; }
        [StringLength(50)]
        public string HolidayTypeDescription { get; set; }

        [InverseProperty("HolidayType")]
        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
