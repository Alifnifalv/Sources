using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class CollectFeeAccountSplitDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long FeeCollectionFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? FEECollectionID { get; set; }

     
        [DataMember]
        public long? FeeMasterClassMapID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

       

        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public byte CollectionType { get; set; }

        [DataMember]
        public string FeeMaster { get; set; }

        [DataMember]
        public string FeePeriod { get; set; }

        [DataMember]
        public long? AccountID { get; set; }


        [DataMember]
        public long? AccountTransactionHeadID { get; set; }

        [DataMember]
        public long? AccountTransactionDetailIID { get; set; }


    }
}
