using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetGroupDTO : BaseMasterDTO
    {
        [DataMember]
        public int AssetGroupID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string AssetGroupName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}