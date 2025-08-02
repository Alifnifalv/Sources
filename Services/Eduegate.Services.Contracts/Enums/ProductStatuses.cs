using System.Runtime.Serialization;
using System.ComponentModel;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "ProductStatuses")]
    public enum ProductStatuses
    {
        [Description("All")]
        [EnumMember]
        All = 0,
        [Description("UnderReview")]
        [EnumMember]
        UnderReview = 1,
        [Description("Active")]
        [EnumMember]
        Active = 2,
        [Description("Suspended")]
        [EnumMember]
        Suspended = 3,
        [Description("Cancelled")]
        [EnumMember]
        Cancelled = 4,
        [Description("Draft")]
        [EnumMember]
        Draft = 5,
        [Description("InActive")]
        [EnumMember]
        Inactive = 6
    }
}
