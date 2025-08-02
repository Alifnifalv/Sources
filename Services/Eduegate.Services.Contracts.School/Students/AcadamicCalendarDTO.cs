using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class AcadamicCalendarDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AcadamicCalendarDTO()
        {
            AcademicYearCalendarEventDTO = new List<AcademicYearCalendarEventDTO>();
        }

        [DataMember]
        public long AcademicCalendarID { get; set; }

        [DataMember]
        public string CalenderName { get; set; }
        
        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public byte? AcademicCalendarStatusID { get; set; }
        
        [DataMember]
        public KeyValueDTO AcademicYearCalendarStatus { get; set; }
        
        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }
        
        [DataMember]
        public List<AcademicYearCalendarEventDTO> AcademicYearCalendarEventDTO { get; set; }
    }
}