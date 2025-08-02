using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Warehouses
{
    [DataContract]
    public enum JobOperationStatuses
    {
        [EnumMember]
        NotStarted = 1,
        [EnumMember]
        InProcess = 2,
        [EnumMember]
        OnHold = 3,
        [EnumMember]
        Completed = 4,
        [EnumMember]
        Partail = 5,
        [EnumMember]
        Failed = 6,
        [EnumMember]
        ConfirmOrder = 7,
        [EnumMember]
        RejectOrder = 8,
        [EnumMember]
        Done = 9,
        [EnumMember]
        Deliveredtoshowroom = 10,
        [EnumMember]
        Giventodriver = 11,
        [EnumMember]
        DeliveredtoWarehouse = 12,
        [EnumMember]
        DeliveredtoCustomer = 13,
        [EnumMember]
        ReceivedatWarehouse = 14,
        [EnumMember]
        ReceivedbyCustomer = 15,
    }
}
