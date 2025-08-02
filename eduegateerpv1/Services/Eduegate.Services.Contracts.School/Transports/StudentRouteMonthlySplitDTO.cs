using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class StudentRouteMonthlySplitDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long StudentRouteMonthlySplitIID { get; set; }

        [DataMember]
        public long? StudentRouteStopMapID { get; set; }

        [DataMember]
        public long? PickupStopMapID { get; set; }
        [DataMember]
        public long? DropStopMapID { get; set; }
        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public int? MonthID { get; set; }
        [DataMember]
        public int? Year { get; set; }
        [DataMember]
        public bool? Status { get; set; }
        [DataMember]
        public bool? IsExcluded { get; set; }
        [DataMember]
        public bool? IsCollected { get; set; }
        [DataMember]
        public int? FeePeriodID { get; set; }
        [DataMember]
        public bool? IsRowSelected { get; set; }

    }
}
