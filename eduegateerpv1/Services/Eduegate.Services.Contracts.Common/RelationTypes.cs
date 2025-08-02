using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum RelationTypes
    {
        [EnumMember]
        Product = 1,
        [EnumMember]
        Supplier = 2,
    }
}
