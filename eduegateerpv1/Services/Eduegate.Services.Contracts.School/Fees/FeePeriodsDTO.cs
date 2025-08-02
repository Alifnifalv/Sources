using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeePeriodsDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int FeePeriodID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime PeriodFrom { get; set; }

        [DataMember]
        public DateTime PeriodTo { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public int? NumberOfPeriods { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }

    }
}
