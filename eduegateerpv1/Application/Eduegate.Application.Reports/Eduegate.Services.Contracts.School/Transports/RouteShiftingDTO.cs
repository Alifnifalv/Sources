using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class RouteShiftingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RouteShiftingDTO()
        {
            StudentLists = new List<RouteShiftingStudentMapDTO>();
            StaffLists = new List<RouteShiftingStaffMapDTO>();
            Route = new KeyValueDTO();
            AcademicYear = new KeyValueDTO();
        }


        [DataMember]
        public long StudentRouteStopMapIID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? RouteID { get; set; }

        [DataMember]
        public KeyValueDTO Route { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }


        [DataMember]
        public List<RouteShiftingStudentMapDTO> StudentLists { get; set; }

        [DataMember]
        public List<RouteShiftingStaffMapDTO> StaffLists { get; set; }

        [DataMember]
        public int? RouteGroupID { get; set; }

        [DataMember]
        public int? ToRouteGroupID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

    }
}