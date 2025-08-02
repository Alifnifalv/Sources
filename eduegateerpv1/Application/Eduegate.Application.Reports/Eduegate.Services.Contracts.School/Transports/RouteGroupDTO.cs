using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class RouteGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RouteGroupDTO()
        {
            AcademicYear = new KeyValueDTO();
        }

        [DataMember]
        public int RouteGroupID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

    }
}