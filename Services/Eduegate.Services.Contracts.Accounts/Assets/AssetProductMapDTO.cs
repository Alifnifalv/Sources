using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetProductMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long AssetProductMapIID { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        public long? ProductSKUMapID { get; set; }

        [DataMember]
        public string ProductSKUName { get; set; }
    }
}