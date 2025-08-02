using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
   public  class AssetEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public AssetTransactionHeadDTO MasterViewModel { get; set; }

        [DataMember]
        public List<AssetTransactionDetailsDTO> DetailViewModel { get; set; }
    }
}