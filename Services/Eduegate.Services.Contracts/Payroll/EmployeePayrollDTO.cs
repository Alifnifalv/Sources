using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class EmployeePayrollDTO : BaseMasterDTO
    {
        [DataMember]
        public long EmployeePayrollIID { get; set; }

        [DataMember]
        public long? EmployeePayrollID { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public byte? CategoryID { get; set; }

        [DataMember]
        public bool? IsOTEligible { get; set; }
        [DataMember]
        public bool? IsLeaveSalaryEligible { get; set; }
        [DataMember]
        public bool? IsEoSBEligible { get; set; }
        [DataMember]
        public string AcademicCalendar { get; set; }

        [DataMember]
        public long? AcademicCalendarID { get; set; }

        [DataMember]
        public byte? CalendarTypeID { get; set; }

    }
}