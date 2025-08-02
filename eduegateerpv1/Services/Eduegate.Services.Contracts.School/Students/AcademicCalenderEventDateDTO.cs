using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class AcademicCalenderEventDateDTO
    {
        [DataMember]
        public long AcademicYearCalendarEventID { get; set; }
        
        [DataMember]
        public long AcademicCalendarID { get; set; }
        
        [DataMember]
        public int? AcademicYearID { get; set; }

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
        public AcadamicCalendarDTO AcadamicCalendar { get; set; }
        
        [DataMember]
        public string ColorCode { get; set; }

        [DataMember]
        public DateTime? ActualDate { get; set; }
        
        [DataMember]
        public int? Day { get; set; }

        [DataMember]
        public int? Month { get; set; }

        [DataMember]
        public string MonthName { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public byte? AcademicCalendarStatusID { get; set; }

        [DataMember]
        public int? AcademicYearCalendarEventTypeID { get; set; }

        [DataMember]
        public string AcademicYearCalendarEventType { get; set; }

        [DataMember]
        public decimal? NoofHours { get; set; }

        [DataMember]
        public string EventStartDateString { get; set; }

        [DataMember]
        public string EventEndDateString { get; set; }

        [DataMember]
        public string ActualDateString { get; set; }

        [DataMember]
        public int? DayOfWeek { get; set; }

        
    }
}
