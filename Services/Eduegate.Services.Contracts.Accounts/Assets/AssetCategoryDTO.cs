using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetCategoryDTO : BaseMasterDTO
    {
        [DataMember]
        public long AssetCategoryID { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public decimal? DepreciationRate { get; set; }

        [DataMember]
        public int? DepreciationPeriodID { get; set; }

        [DataMember]
        public string CategoryPrefix { get; set; }

        [DataMember]
        public long? LastSequenceNumber { get; set; }

        [DataMember]
        public string OldCategoryPrefix { get; set; }
    }
}