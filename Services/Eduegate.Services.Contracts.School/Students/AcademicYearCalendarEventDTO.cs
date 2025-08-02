using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class AcademicYearCalendarEventDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long AcademicYearCalendarEventIID { get; set; }
        
        [DataMember]
        public long AcademicCalendarID { get; set; }        
        
        [DataMember]
        public string EventTitle { get; set; }
        
        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public DateTime? StartDate { get; set; }
        
        [DataMember]
        public DateTime? EndDate { get; set; }
        
        [DataMember]
        public bool? IsThisAHoliday { get; set; }
        
        [DataMember]
        public bool IsEnableReminders { get; set; }
        
        [DataMember]
        public  AcadamicCalendarDTO AcadamicCalendar { get; set; }
        
        [DataMember]
        public string ColorCode { get; set; }

        [DataMember]
        public string AcademicYearCalendarEventType { get; set; }

        [DataMember]
        public byte? AcademicCalendarEventTypeID { get; set; }

        [DataMember]
        public decimal? NoofHours { get; set; }
    }
}