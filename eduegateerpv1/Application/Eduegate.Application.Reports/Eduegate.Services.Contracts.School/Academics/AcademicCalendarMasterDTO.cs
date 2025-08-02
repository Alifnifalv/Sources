using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AcademicCalendarMasterDTO : BaseMasterDTO
    {
        [DataMember]
        public long AcademicCalendarID { get; set; }

        [DataMember]
        public string CalenderName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte? AcademicCalendarStatusID { get; set; }

        [DataMember]
        public string AcademicYearCalendarStatus { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public byte? CalendarTypeID { get; set; }
    }
}