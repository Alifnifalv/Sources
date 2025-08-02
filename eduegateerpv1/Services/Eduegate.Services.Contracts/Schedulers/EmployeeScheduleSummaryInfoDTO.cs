using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class EmployeeScheduleSummaryInfoDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public List<ScheduleSummaryInfoDTO> SchedulesInfos { get; set; }
    }
}
