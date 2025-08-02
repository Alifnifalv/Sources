using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class RouteStopFeeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RouteStopFeeDTO()
        {
            Route = new KeyValueDTO();

            StopsStudentDetails = new List<StudentRouteStopMapDTO>();
            StopsStaffDetails = new List<StaffRouteStopMapDTO>();
        }

        [DataMember]
        public long RouteStopMapIID { get; set; }

        [DataMember]
        public int RouteID { get; set; }

        [DataMember]
        public KeyValueDTO Route { get; set; }

        [DataMember]
        public string StopName { get; set; }

        [DataMember]
        public decimal? RouteFareOneWay { get; set; }

        [DataMember]
        public decimal? RouteFareTwoWay { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        [StringLength(20)]
        public string StopCode { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public int? SequenceNo { get; set; }


        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        #region Mobile app use
        [DataMember]
        public List<StudentRouteStopMapDTO> StopsStudentDetails { get; set; }

        [DataMember]
        public List<StaffRouteStopMapDTO> StopsStaffDetails { get; set; }
        #endregion
    }
}
