using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class LeaveTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int LeaveTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? MaxDaysAllowed { get; set; }

        [DataMember]
        public bool? IsCarryForward { get; set; }

        [DataMember]
        public bool? IsLeaveWithoutPay { get; set; }

        [DataMember]
        public bool? AllowNegativeBalance { get; set; }

        [DataMember]
        public bool? IncludeHolidayWithinLeaves { get; set; }
    }
}
