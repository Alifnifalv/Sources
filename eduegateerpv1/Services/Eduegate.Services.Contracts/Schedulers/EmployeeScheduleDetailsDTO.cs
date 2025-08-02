using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class EmployeeScheduleDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public decimal? AmountReceived { get; set; }
        [DataMember]
        public double? TotalHours { get; set; }
        [DataMember]
        public double? RemainingHours { get; set; }       
        [DataMember]
        public string PickUpDetail { get; set; }
        [DataMember]
        public List<EmployeeScheduleDTO> Schedules { get; set; }
    }
}
