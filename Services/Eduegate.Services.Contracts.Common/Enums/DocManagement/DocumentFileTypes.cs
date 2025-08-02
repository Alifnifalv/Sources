using System.ComponentModel;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "DocumentFileTypes")]
    public enum DocumentFileTypes
    {
        [Description("JPG")]
        [EnumMember]
        JPG = 1,
        [Description("PNG")]
        [EnumMember]
        PNG = 2,
        [Description("PDF")]
        [EnumMember]
        PDF = 3,
        [Description("TXT")]
        [EnumMember]
        TXT = 4,
        [Description("OTHERS")]
        [EnumMember]
        OTHERS = 5,
    }
}
