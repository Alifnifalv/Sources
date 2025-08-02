using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetTransactionDTO : BaseMasterDTO
    {
        [DataMember]
        public AssetTransactionHeadDTO AssetTransactionHead { get; set; }

        [DataMember]
        public List<AssetTransactionDetailsDTO> AssetTransactionDetails { get; set; }

       
        [DataMember]
        public List<AssetTransactionHeadDTO> TransactionHeads { get; set; }

        [DataMember]
        public string ErrorCode { get; set; } 
        
        [DataMember]
        public string ReturnMessage { get; set; }

        [DataMember]
        public bool IsError { get; set; }
    }
}