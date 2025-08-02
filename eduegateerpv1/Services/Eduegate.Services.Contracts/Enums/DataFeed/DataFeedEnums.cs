using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "DataFeedStatus")]
    public enum DataFeedStatus
    {
        [EnumMember]
        InProcess = 1,
        [EnumMember]
        Success = 2,
        [EnumMember]
        Failed = 3,
    }

    [DataContract(Name = "DataFeedOperations")]
    public enum DataFeedOperations
    {
        [EnumMember]
        Update = 1,
        [EnumMember]
        Insert = 2,
        
    }
}
