using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum EntityPropertyTypes
    {
        [EnumMember]
        Email = 1,
        [EnumMember]
        Telephone = 2,
        [EnumMember]
        Fax = 3,
        [EnumMember]
        Title = 4,
        [EnumMember]
        Gender = 5,
        [EnumMember]
        MaritalStatus = 6,
        [EnumMember]
        ChequeType = 7,
    }
}
