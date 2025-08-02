using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    public class RouteVehicleMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RouteVehicleMapDTO()
        {
            Vehicle = new KeyValueDTO();
            Routes = new List<KeyValueDTO>();
        }

        [DataMember]
        public long RouteVehicleMapIID { get; set; }

        [DataMember]
        public int? RouteID { get; set; }

        [DataMember]
        public List<KeyValueDTO> Routes { get; set; }

        [DataMember]
        public long? VehicleID { get; set; }

        [DataMember]
        public KeyValueDTO Vehicle { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }
    }
}