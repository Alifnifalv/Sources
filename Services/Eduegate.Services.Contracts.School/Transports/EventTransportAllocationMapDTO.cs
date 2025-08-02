using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class EventTransportAllocationMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EventTransportAllocationMapDTO()
        {
            Student = new KeyValueDTO();
            Staff = new KeyValueDTO();
        }

        [DataMember]
        public long EventTransportAllocationMapIID { get; set; }

        [DataMember]
        public long? EventTransportAllocationID { get; set; }

        [DataMember]
        public long? StudentRouteStopMapID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public long? StaffRouteStopMapID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public bool? IsPickUp { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public string ClassSection { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public string PickUpRoute { get; set; }

        [DataMember]
        public string DropRoute { get; set; }

        [DataMember]
        public string PickupStop { get; set; }

        [DataMember]
        public string DropStop { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public KeyValueDTO Staff { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Designation { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DateFromString { get; set; }

        [DataMember]
        public string DateToString { get; set; }

        [DataMember]
        public int? PickupRouteID { get; set; } 

        [DataMember]
        public int? DropRouteID { get; set; }

        [DataMember]
        public int? ToRouteID { get; set; }
    }
}