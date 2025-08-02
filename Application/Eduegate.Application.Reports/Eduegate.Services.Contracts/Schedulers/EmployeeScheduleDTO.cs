using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class EmployeeScheduleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeScheduleDTO()
        {
            Location = new GeoLocationDTO();
        }

        [DataMember]
        public long ScheduleID { get; set; }
        [DataMember]
        public long DespatchID { get; set; }
        [DataMember]
        public string PickUpLocation { get; set; }
        [DataMember]
        public string PickUp { get; set; }
        [DataMember]
        public string MaidName { get; set; }
        [DataMember]
        public string MaidCode { get; set; }
        [DataMember]
        public string DropOff { get; set; }
        [DataMember]
        public string DropOffLocation { get; set; }
        [DataMember]
        public string DriverName { get; set; }
        [DataMember]
        public double? Duration { get; set; }
        [DataMember]
        public string CustomerDetails { get; set; }
        [DataMember]
        public string TimeFrom { get; set; }
        [DataMember]
        public string TimeTo { get; set; }
        [DataMember]
        public int? StatusID { get; set; }
        [DataMember]
        public decimal? ReceivedAmount { get; set; }
        [DataMember]
        public ScheduleTypes ScheduleType { get; set; }
        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public decimal? Rate { get; set; }
        [DataMember]
        public decimal? TaxAmount { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public decimal? AmountDue { get; set; }
        [DataMember]
        public long? CustomerID { get; set; }
        [DataMember]
        public long? AreaID { get; set; }

        [DataMember]
        public DateTime? DesptachDate { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string ItemsToCarry { get; set; }

        [DataMember]
        public string ScheduleNote { get; set; }

        [DataMember]
        public DateTime? StopDateTime { get; set; }
        [DataMember]
        public DateTime? StartDateTime { get; set; }
        [DataMember]
        public GeoLocationDTO Location {get;set;}
    }
}
