using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeTimeSheetsWeeklyDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeTimeSheetsWeeklyDTO()
        {
            Employee = new KeyValueDTO();
            TimeSheet = new List<EmployeeTimeSheetsTimeDTO>();
        }

        [DataMember]
        public long EmployeeTimeSheetIID { get; set; }

        [DataMember]
        public long EmployeeID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public List<EmployeeTimeSheetsTimeDTO> TimeSheet { get; set; }

        [DataMember]
        public string TimeSheetDateFromString { get; set; }

        [DataMember]
        public string TimeSheetDateToString { get; set; }

        [DataMember]
        public bool IsSelf { get; set; }

    }
}