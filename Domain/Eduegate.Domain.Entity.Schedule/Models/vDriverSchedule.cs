using System;

namespace Eduegate.Domain.Entity.Schedule.Models
{
    public class vDriverSchedule
    {
        public long DriverScheduleIdty { get; set; }
        public DateTime ScheduleDate { get; set; }
        public long? ScheduleId { get; set; }
        public long? DriverId { get; set; }
        public string DriverCode { get; set; }
        public string DriverName { get; set; }
        public long? MaidId { get; set; }
        public DateTime PickUp { get; set; }
        public string PickUp_Location { get; set; }
        public DateTime DropOff { get; set; }
        public string DropOff_Location { get; set; }
        public long? AreaId { get; set; }
        public string MaidCode { get; set; }
        public string MaidName { get; set; }
        public int? StatusId { get; set; }
    }
}