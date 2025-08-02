using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Accounting;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounting
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
