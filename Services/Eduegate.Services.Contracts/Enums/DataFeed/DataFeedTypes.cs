using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "DataFeedTypes")]
    public enum DataFeedTypes
    {
        [EnumMember]
        BankReconciliationOpening = 1,
        [EnumMember]
        AssetOpening = 2,
    }
}