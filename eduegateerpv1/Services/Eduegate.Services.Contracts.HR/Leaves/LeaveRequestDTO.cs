using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class LeaveRequestDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LeaveApplicationIID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public int? LeaveTypeID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public DateTime? RejoiningDate { get; set; }

        [DataMember]
        public byte? FromSessionID { get; set; }

        [DataMember]
        public byte? ToSessionID { get; set; }

        [DataMember]
        public string OtherReason { get; set; }

        [DataMember]
        public byte? LeaveStatusID { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string AppliedDateString { get; set; }

        [DataMember]
        public string FromDateString { get; set; }

        [DataMember]
        public string ToDateString { get; set; }

        [DataMember]
        public string RejoiningDateString { get; set; }

        [DataMember]
        public bool? IsLeaveWithoutPay { get; set; }


        [DataMember]
        public string LeaveType { get; set; }

        [DataMember]
        public DateTime? ExpectedRejoiningDate { get; set; }

        [DataMember]
        public string ExpectedRejoiningDateString { get; set; }

        [DataMember]
        public bool? IsHalfDay { get; set; }
        
    }
}
