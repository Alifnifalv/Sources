namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schedule.DriverSchedules")]
    public partial class DriverSchedule
    {
        [Key]
        public long DriverScheduleIdty { get; set; }

        public DateTime? ScheduleDate { get; set; }

        public long? ScheduleID { get; set; }

        public long? DriverID { get; set; }

        public long? MiadID { get; set; }

        public DateTime? Pickup { get; set; }

        [StringLength(500)]
        public string PickupLocation { get; set; }

        public DateTime? DropOff { get; set; }

        [StringLength(500)]
        public string DropLocation { get; set; }

        public long? AreaID { get; set; }

        public int? StatusID { get; set; }

        public long? ExternalReferenceID1 { get; set; }

        public long? ExternalReferenceID2 { get; set; }
    }
}
