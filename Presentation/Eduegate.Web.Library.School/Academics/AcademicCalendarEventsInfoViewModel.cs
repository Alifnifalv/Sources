namespace Eduegate.Web.Library.School.Academics
{
    public class AcademicCalendarEventsInfoViewModel
    {
        public long AcademicYearCalendarEventIID { get; set; }
       
        public long AcademicCalendarID { get; set; }
       
        public string EventTitle { get; set; }
        
        public string Description { get; set; }
        
        public string StartDateString { get; set; }
      
        public string EndDateString { get; set; }
     
        public bool? IsThisAHoliday { get; set; }
       
        public bool IsEnableReminders { get; set; }

        public string ColorCode { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? AcademicCalendarStatusID { get; set; }

        public string AcademicYearCalendarEventType { get; set; }
        
        public byte? AcademicCalendarEventTypeID { get; set; }

        public decimal? NoofHours { get; set; }
    }
}