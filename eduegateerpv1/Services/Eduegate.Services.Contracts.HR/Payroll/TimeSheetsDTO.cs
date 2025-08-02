using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class TimeSheetsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TimeSheetsDTO()
        {
            Employee = new KeyValueDTO();
            Task = new KeyValueDTO();
        }
            [DataMember]
        public long EmployeeTimeSheetIID { get; set; }

        [DataMember]
        public long EmployeeID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

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
    }
}
