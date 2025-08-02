using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public  class FeeMasterClassMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long FeeMasterClassMapIID { get; set; }

        [DataMember]
        public long? ClassFeeMasterID { get; set; }

        [DataMember]

        public int? FeeMasterID { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]

        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]

        public decimal? TaxAmount { get; set; }
    }
}
