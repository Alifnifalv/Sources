using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeMasterClassMonthlySplitDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long FeeMasterClassMontlySplitMapIID { get; set; }

        [DataMember]
        public long? FeeMasterClassMapID { get; set; }

        [DataMember]
        public int MonthID { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public decimal? Tax { get; set; }
    }
}
