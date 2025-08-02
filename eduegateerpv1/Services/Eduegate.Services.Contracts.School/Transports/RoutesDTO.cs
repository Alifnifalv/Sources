using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class RoutesDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RoutesDTO()
        {
            StopFees = new List<RouteStopFeeDTO>();
            Stops = new List<RouteStopFeeDTO>();
            CostCenter = new KeyValueDTO();
        }
        
        [DataMember]
        public int RouteID { get; set; }

        [DataMember]
        public string RouteCode { get; set; }
        
        [DataMember]
        public byte? RouteTypeID { get; set; }

        [DataMember]
        public string RouteDescription { get; set; }
        
        [DataMember]
        public decimal? RouteFareOneWay { get; set; }
        
        [DataMember]
        public List<RouteStopFeeDTO> Stops { get; set; }

        [DataMember]
        public List<RouteStopFeeDTO> StopFees { get; set; }

        [DataMember]
        public int? CostCenterID { get; set; }

        [DataMember]
        public KeyValueDTO CostCenter { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }
    }
}