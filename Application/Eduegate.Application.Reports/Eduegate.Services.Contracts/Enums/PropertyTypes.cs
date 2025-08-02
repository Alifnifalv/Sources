using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "PropertyTypes")]
    public enum PropertyTypes : byte
    {
        [EnumMember]
        Variants = 1,
        [EnumMember]
        Fields = 2,
        FieldsForOnlineStore = 3,
        [EnumMember]
        Color = 4,
        [EnumMember]
        Size = 5,
        [EnumMember]
        Material = 6,
        [EnumMember]
        Metal = 7,
        [EnumMember]
        Stone = 8,
        [EnumMember]
        AgeGroup = 9,
        [EnumMember]
        RAMMemory = 10,
        [EnumMember]
        HardDiskMemory = 11,
        [EnumMember]
        Title = 15,
        [EnumMember]
        ProductImage = 16,
        [EnumMember]
        ProductTag = 14,
    }
}
