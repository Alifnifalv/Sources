using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.UrlReWriter
{
    [DataContract]
    public enum UrlType
    {
        [EnumMember]
        Category,
        [EnumMember]
        Product,
        [EnumMember]
        Brand,
        [EnumMember]
        Other
    }

}
