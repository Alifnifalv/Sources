using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Warehouses
{
    [DataContract]
    public enum JobOperationTypes
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
        Created = 15
    }
}
