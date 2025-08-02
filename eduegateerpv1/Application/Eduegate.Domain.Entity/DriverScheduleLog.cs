namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.DriverScheduleLogs")]
    public partial class DriverScheduleLog
    {
        [Key]
        public long DriverScheduleLogIID { get; set; }

        public long? StudentID { get; set; }

        public long? EmployeeID { get; set; }

        public DateTime? SheduleDate { get; set; }

        public int? RouteID { get; set; }

        public long? RouteStopMapID { get; set; }

        public long? VehicleID { get; set; }

        public int? SheduleLogStatusID { get; set; }

        public int? StopEntryStatusID { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string ScheduleLogType { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual RouteStopMap RouteStopMap { get; set; }

        public virtual ScheduleLogStatus ScheduleLogStatus { get; set; }

        public virtual StopEntryStatus StopEntryStatus { get; set; }

        public virtual Student Student { get; set; }
    }
}
