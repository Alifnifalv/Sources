using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    public class CollectFeeAccountDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long? FeeMasterClassMapID { get; set; }

        [DataMember]
        public string ReceiptNo { get; set; }

        [DataMember]
        public bool IsPosted { get; set; }

        [DataMember]
        public string Reference { get; set; }


        [DataMember]
        public string CollectionDateString { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public string Student { get; set; }


        [DataMember]
        public long? FeeCollectionID { get; set; }
        [DataMember]
        public byte CollectionType { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public long? AccountTransactionHeadIID { get; set; }

        [DataMember]
        public string GroupTransactionNumber { get; set; }

        [DataMember]
        public virtual List<CollectFeeAccountSplitDTO> FeeAccountSplit { get; set; }


        [DataMember]
        public virtual List<CollectFeePaymentModeDTO> CollectFeePaymentModeList { get; set; }

    }
    public class CollectFeePaymentModeDTO 
    {
        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string PaymentModeName { get; set; }

        [DataMember]
        public int PaymentModeTypeID { get; set; }

    }
}
