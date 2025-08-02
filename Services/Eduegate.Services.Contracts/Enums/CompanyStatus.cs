using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "CompanyStatus")]
    public enum CompanyStatus
    {
        [EnumMember]
        Active = 1,
        [EnumMember]
        InActive = 2,
    }
}
