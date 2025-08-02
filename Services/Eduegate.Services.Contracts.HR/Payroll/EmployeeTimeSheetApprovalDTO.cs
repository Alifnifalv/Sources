using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeTimeSheetApprovalDTO : BaseMasterDTO
    {
        public EmployeeTimeSheetApprovalDTO()
        {
            TimeSheetDetails = new List<EmployeeTimeSheetApprovalTimeDTO>();
        }

        [DataMember]
        public long EmployeeTimeSheetApprovalIID { get; set; }

        [DataMember]
        public long? EmployeeTimeSheetID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public DateTime? TimesheetDateFrom { get; set; }

        [DataMember]
        public DateTime? TimesheetDateTo { get; set; }

        [DataMember]
        public byte? TimesheetTimeTypeID { get; set; }

        [DataMember]
        public string TimesheetTimeTypeName { get; set; }

        [DataMember]
        public decimal? OTHours { get; set; }

        [DataMember]
        public decimal? NormalHours { get; set; }

        [DataMember]
        public byte? TimesheetApprovalStatusID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public DateTime? TimeSheetDate { get; set; }

        [DataMember]
        public string TimeSheetDateString { get; set; }

        [DataMember]
        public decimal? TotalOTHours { get; set; }

        [DataMember]
        public decimal? TotalNormalHours { get; set; }

        [DataMember]
        public List<EmployeeTimeSheetApprovalTimeDTO> TimeSheetDetails { get; set; }

    }
}