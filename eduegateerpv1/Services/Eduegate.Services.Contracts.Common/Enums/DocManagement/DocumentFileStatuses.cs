using System.ComponentModel;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "DocumentFileStatuses")]
    public enum DocumentFileStatuses
    {
        [Description("Draft")]
        [EnumMember]
        Draft = 1,
        [Description("Published")]
        [EnumMember]
        Published = 2,
        [Description("Expired")]
        [EnumMember]
        Expired = 3,
        [Description("Cancelled")]
        [EnumMember]
        Cancelled = 4,
    }
}
