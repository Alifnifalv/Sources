using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class CalendarEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CalendarEntryID { get; set; }

        [DataMember]
        public long AcademicCalendarID { get; set; }

        [DataMember]
        public DateTime? CalendarDate { get; set; }

        [DataMember]
        public decimal? NoofHours { get; set; }

        [DataMember]
        public string CalendarDateString { get; set; }
    }
}