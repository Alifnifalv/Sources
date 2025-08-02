using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{

    public enum BooleanType
    {
        [DataMember]
        No = 0,
        [DataMember]
        Yes = 1,
    }
}
