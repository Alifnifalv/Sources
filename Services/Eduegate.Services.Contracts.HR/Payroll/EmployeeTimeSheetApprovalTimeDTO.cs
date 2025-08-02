using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeTimeSheetApprovalTimeDTO : BaseMasterDTO
    {
        public EmployeeTimeSheetApprovalTimeDTO()
        {
        }

        [DataMember]
        public long EmployeeTimeSheetIID { get; set; }

        [DataMember]
        public long? TaskID { get; set; }

        [DataMember]
        public string TaskName { get; set; }

        [DataMember]
        public decimal? SheetNormalHours { get; set; }

        [DataMember]
        public decimal? SheetOTHours { get; set; }

        [DataMember]
        public byte? TimesheetTimeTypeID { get; set; }

        [DataMember]
        public string TimesheetTimeTypeName { get; set; }

        [DataMember]
        public TimeSpan? FromTime { get; set; }

        [DataMember]
        public TimeSpan? ToTime { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public byte? TimesheetEntryStatusID { get; set; }
    }
}