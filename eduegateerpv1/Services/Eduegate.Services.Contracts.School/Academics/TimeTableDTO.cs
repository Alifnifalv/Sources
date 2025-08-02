using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class TimeTableDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public TimeTableDTO()
        {
            TimeTableAllocations = new List<TimeTableAllocationDTO>();
        }

        [DataMember]
        public int TimeTableID { get; set; }

        [DataMember]
        public string TimeTableDescription { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }
      
        [DataMember]
        public List<TimeTableAllocationDTO> TimeTableAllocations { get; set; }

        [DataMember]
        public int AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public Nullable<bool> IsActivice { get; set; }
    }
}
