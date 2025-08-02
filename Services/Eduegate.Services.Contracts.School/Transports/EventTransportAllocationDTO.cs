using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class EventTransportAllocationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EventTransportAllocationDTO()
        {
            StudentList = new List<EventTransportAllocationMapDTO>();
            StaffList = new List<EventTransportAllocationMapDTO>();
            Event = new KeyValueDTO();
            Class = new List<KeyValueDTO>();
            Route = new List<KeyValueDTO>();
            ToRoute = new KeyValueDTO();
        }

        [DataMember]
        public long EventTransportAllocationIID { get; set; }

        [DataMember]
        public DateTime? EventDate { get; set; }

        [DataMember]
        public int? RouteID { get; set; }

        [DataMember]
        public long? VehicleID { get; set; }

        [DataMember]
        public long? DriverID { get; set; }

        [DataMember]
        public long? AttendarID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Description { get; set; }

        [DataMember]
        public long? EventID { get; set; }

        [DataMember]
        public bool? IsPickUp { get; set; }

        [DataMember]
        public KeyValueDTO Event { get; set; }

        [DataMember]
        public List<KeyValueDTO> Route { get; set; }

        [DataMember]
        public List<KeyValueDTO> Class { get; set; }

        [DataMember]
        public KeyValueDTO Vehicle { get; set; }

        [DataMember]
        public KeyValueDTO Driver { get; set; }

        [DataMember]
        public KeyValueDTO Attendar { get; set; }

        [DataMember]
        public KeyValueDTO ToRoute { get; set; }

        [DataMember]
        public string IsRouteType { get; set; }

        [DataMember]
        public bool ListStudents { get; set; }

        [DataMember]
        public bool ListStaffs { get; set; }

        [DataMember]
        public List<EventTransportAllocationMapDTO> StudentList { get; set; }

        [DataMember]
        public List<EventTransportAllocationMapDTO> StaffList { get; set; }
    }
}