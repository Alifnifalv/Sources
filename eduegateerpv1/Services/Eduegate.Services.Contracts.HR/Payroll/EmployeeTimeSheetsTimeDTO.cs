using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeTimeSheetsTimeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeTimeSheetsTimeDTO()
        {
            Task = new KeyValueDTO();
        }

        [DataMember]
        public long EmployeeTimeSheetIID { get; set; }

        [DataMember]
        public long TaskID { get; set; }

        [DataMember]
        public KeyValueDTO Task { get; set; }

        [DataMember]
        public DateTime TimesheetDate { get; set; }

        [DataMember]
        public decimal? OTHours { get; set; }

        [DataMember]
        public decimal? NormalHours { get; set; }

        [DataMember]
        public byte? TimesheetEntryStatusID { get; set; }

        [DataMember]
        public KeyValueDTO TimesheetEntryStatus { get; set; }

        [DataMember]
        public TimeSpan? FromTime { get; set; }

        [DataMember]
        public TimeSpan? ToTime { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public byte? TimesheetTimeTypeID { get; set; }

        [DataMember]
        public string TimesheetTimeType { get; set; }
    }
}