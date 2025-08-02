namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.TimeSlotOverRider")]
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
