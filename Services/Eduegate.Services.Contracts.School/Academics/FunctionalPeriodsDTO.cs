using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class FunctionalPeriodsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FunctionalPeriodsDTO()
        {
        }

        [DataMember]
        public int FunctionalPeriodID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string AcademicYearString { get; set; }

        [DataMember]
        public string SchoolString { get; set; }

    }
}
