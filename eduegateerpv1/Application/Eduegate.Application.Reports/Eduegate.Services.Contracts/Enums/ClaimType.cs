using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "ClaimType")]
    public enum ClaimType
    {
        [EnumMember]
        AllowedOperations = 1,
        [EnumMember]
        Menu = 2,
        [EnumMember]
        Branches = 3,
        [EnumMember]
        DocumentTypes = 4,
        [EnumMember]
        ProductStatus = 5,
        [EnumMember]
        ProductPrice = 6,
        [EnumMember]
        Roles = 7,
        [EnumMember]
        Department = 8,
        [EnumMember]
        JobOpening = 9,
        [EnumMember]
        ERPDashbaord = 10,
        [EnumMember]
        Features = 11,
        [EnumMember]
        School = 12
    }
}
