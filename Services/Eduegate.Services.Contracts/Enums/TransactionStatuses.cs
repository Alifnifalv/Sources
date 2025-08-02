using System.ComponentModel;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "TransactionStatuses")]
    public enum TransactionStatus
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        New = 1,
        [EnumMember]
        InProcess = 2,
        [EnumMember]
        Fraud = 3,
        [EnumMember]
        Hold = 4,
        [EnumMember]
        Complete = 5,
        [EnumMember]
        Cancelled = 6,
        [EnumMember]
        Confirmed = 7,
        [EnumMember]
        Failed = 8,
        [EnumMember]
        Packed = 9,
        [EnumMember]
        Despatch = 10,
        [EnumMember]
        Delivered = 10,
        [EnumMember]
        Edit = 12,
        [EnumMember]
        IntitiateReprecess = 18,
        [EnumMember]
        Immediate = 19,
        [EnumMember]
        Returned = 20,
    }
}
