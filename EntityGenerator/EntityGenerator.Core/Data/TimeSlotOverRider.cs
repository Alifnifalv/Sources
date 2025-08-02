using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeSlotOverRider", Schema = "cms")]
    public partial class TimeSlotOverRider
    {
        [Key]
        public int OverrideID { get; set; }
        public int? SupplierID { get; set; }
        public int? Cutoff { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? Exclude { get; set; }
    }
}
