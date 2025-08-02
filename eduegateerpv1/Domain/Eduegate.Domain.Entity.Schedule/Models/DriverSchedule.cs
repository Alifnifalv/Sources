using System;

namespace Eduegate.Domain.Entity.Schedule.Models
{
    public class DriverSchedule
    {
        public long DriverScheduleIdty { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public long? ScheduleId { get; set; }
        public long? DriverId { get; set; }
        public long? MaidID { get; set; }
        public DateTime? PickUp { get; set; }
        public string PickupLocation { get; set; }
        public DateTime? DropOff { get; set; }
        public string DropOffLocation { get; set; }
        public long? AreaId { get; set; }
        public int? StatusId { get; set; }

        //public Nullable<int> CreatedBy { get; set; }
        //public Nullable<int> UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}