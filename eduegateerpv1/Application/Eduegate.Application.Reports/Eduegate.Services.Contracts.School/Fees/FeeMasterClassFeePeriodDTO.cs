using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeMasterClassFeePeriodDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    { public FeeMasterClassFeePeriodDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();
            FeeMasterClassMontlySplitMaps = new List<FeeMasterClassMonthlySplitDTO>();
        }

       
        [DataMember]
        public long FeeMasterClassMapIID { get; set; }

        [DataMember]
        public long? ClassFeeMasterID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }
        
        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }

        [DataMember]
        public byte? FeeCycleID { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public decimal? TaxAmount { get; set; }

        [DataMember]
        public bool IsFeePeriodDisabled { get; set; }

        [DataMember]
        public virtual List<FeeMasterClassMonthlySplitDTO> FeeMasterClassMontlySplitMaps { get; set; }
    }
}
