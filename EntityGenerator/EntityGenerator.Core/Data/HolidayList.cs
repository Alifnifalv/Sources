using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("HolidayLists", Schema = "payroll")]
    public partial class HolidayList
    {
        public HolidayList()
        {
            Holidays = new HashSet<Holiday>();
        }

        [Key]
        public long HolidayListIID { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("HolidayList")]
        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
