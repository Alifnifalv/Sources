using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum DocumentStatuses
    {
        [EnumMember]
        Draft = 1,
        [EnumMember]
        Submitted = 2,
        [EnumMember]
        Approved = 3,
        [EnumMember]
        Cancelled = 4,
        [EnumMember]
        Completed = 5,       
        [EnumMember]
        New = 6,
        [EnumMember]
        Reject = 7,
        [EnumMember]
        Accept = 8,
        [EnumMember]
        Returned = 9,
        [EnumMember]
        PartialReturn = 10,
    }
}
