using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "JobStatuses")]
    public enum JobStatuses
    {
        [EnumMember]
        Receiving = 1,
        [EnumMember]
        PutAway = 2,
        [EnumMember]
        Picking = 3,
        [EnumMember]
        StockOut = 4,
        [EnumMember]
        Packing = 5,
        [EnumMember]
        Packed = 6,
        [EnumMember]
        Assigned = 7,
        [EnumMember]
        Accepted = 8,
        [EnumMember]
        InTransit = 9,
        [EnumMember]
        Delivered = 10,
        [EnumMember]
        FailedReceiving = 11,
        [EnumMember]
        FailedReceived = 12,
        [EnumMember]
        Cancel = 13,
        [EnumMember]
        Declined = 14,
        [EnumMember]
        InProcess = 16,
        [EnumMember]
        Receivedinwarehouse = 17,
        [EnumMember]
        ServiceJob = 19,
    }
}
