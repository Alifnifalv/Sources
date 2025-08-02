using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Logging
{
    [DataContract(Name = "OperationType")]
    public enum OperationTypeDTO
    {
        [EnumMember]
        ADD = 1,
        [EnumMember]
        UPDATE = 2,
        [EnumMember]
        DELETE = 3
    }
}
